//===============================================================================
// Microsoft patterns & practices
// Smart Client Software Factory
//===============================================================================
// Copyright  Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================
// The example companies, organizations, products, domain names,
// e-mail addresses, logos, people, places, and events depicted
// herein are fictitious.  No association with any real company,
// organization, product, domain name, email address, logo, person,
// places, or events is intended or should be inferred.
//===============================================================================

using System;
using System.IO;
using System.Xml;
using System.Text;
using System.Threading;
using System.Globalization;
using System.Xml.Serialization;
using System.Collections.Generic;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using GlobalBank.Infrastructure.Interface.Services;
using GlobalBank.AppraiserWorkbench.AppraisalServiceAgent.BusinessEntities;
using GlobalBank.Infrastructure.Interface;
using Microsoft.Practices.CompositeUI.Utility;
using AppraisalManagementServiceProxy=
	GlobalBank.AppraiserWorkbench.WebServiceProxies.AppraisalService.AppraisalManagementServiceProxy;
using AttachmentMetadata=
	GlobalBank.AppraiserWorkbench.WebServiceProxies.AppraisalService.AttachmentMetadata;
using IAppraisalManagementServiceProxy=
	GlobalBank.AppraiserWorkbench.WebServiceProxies.AppraisalService.IAppraisalManagementServiceProxy;

namespace GlobalBank.AppraiserWorkbench.AppraisalServiceAgent
{
	public class AppraisalManagementServiceAgent : IAppraisalManagementServiceAgent, IBuilderAware, IDisposable
	{
		// Locals

		IAppraisalManagementServiceProxy _appraisalServiceProxy;
		private IFileTransferService _fileTransferService;
		private IFileWatcherService _fileWatcherService;
		private string _baseStoragePath = "";
		private string _directoryPrepend = "Appraisal ";
		private Dictionary<string, Appraisal> _loadedAppraisals = new Dictionary<string, Appraisal>();
		private object _lockObject = new object();
		private List<Attachment> _watchedAttachments = new List<Attachment>();
		private readonly int LOCK_APPRAISALS_TIMEOUT = Properties.Settings.Default.LOCK_APPRAISALS_TIMEOUT;
		private readonly int GET_APPRAISALS_TIMEOUT = Properties.Settings.Default.GET_APPRAISALS_TIMEOUT;
		private readonly int SERVICE_POLLING_INTERVAL = Properties.Settings.Default.SERVICE_POLLING_INTERVAL;
		private ManualResetEvent _shutdownEvent = new ManualResetEvent(false);
		private Thread _workerThread = null;
		private bool _lastSuccessCode;

		// Events

		public event EventHandler MyAppraisalsUpdated;

		public event EventHandler<EventArgs<bool>> ServiceOperationNotification;

		// Lifetime

		[InjectionConstructor]
		public AppraisalManagementServiceAgent
			(
				[ServiceDependency] IFileTransferService fileTransferService,
				[ServiceDependency] IFileWatcherService fileWatcherService
			)
			: this(new AppraisalManagementServiceProxy(),
			fileTransferService,
			fileWatcherService)
		{
		}

		public AppraisalManagementServiceAgent
			(
				IAppraisalManagementServiceProxy appraisalServiceProxy,
				IFileTransferService fileTransferService,
				IFileWatcherService fileWatcherService
			)
		{
			Guard.ArgumentNotNull(appraisalServiceProxy, "appraisalServiceProxy");
			Guard.ArgumentNotNull(fileTransferService, "fileTransferService");
			Guard.ArgumentNotNull(fileWatcherService, "fileWatcherService");

			_appraisalServiceProxy = appraisalServiceProxy;
			_fileTransferService = fileTransferService;
			_fileWatcherService = fileWatcherService;
			_baseStoragePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Microsoft\\Appraiser Workbench");

			if (!Directory.Exists(_baseStoragePath))
				Directory.CreateDirectory(_baseStoragePath);

			LoadLocalAppraisals();
		}

		~AppraisalManagementServiceAgent()
		{
			Dispose(false);
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				_shutdownEvent.Set();
				// Wait at the most for the triple amount of time for the worker thread to finish
				if (_workerThread!=null && !_workerThread.Join(Properties.Settings.Default.SERVICE_POLLING_INTERVAL * 3))
				{
					// Worker thread did not respond to the shutdown event, Abort the thread.
					_workerThread.Abort();
				}
				((IDisposable)_shutdownEvent).Dispose();
				_shutdownEvent = null;
				_workerThread = null;
				lock (_lockObject)
				{
					foreach (Attachment attachment in _watchedAttachments)
						_fileWatcherService.EndWatchFile(attachment.FileName);
				}

				_appraisalServiceProxy.Dispose();
			}
		}

		// Properties

		private string UserId
		{
			get { return Thread.CurrentPrincipal.Identity.Name; }
		}

		public string BaseStoragePath
		{
			get { return _baseStoragePath; }
			set { _baseStoragePath = value; }
		}

		protected IFileTransferService FileTransferService
		{
			get { return _fileTransferService; }
		}

		protected IAppraisalManagementServiceProxy AppraisalService
		{
			get { return _appraisalServiceProxy; }
		}

		// Methods

		public Appraisal[] GetLocalAppraisals()
		{
			Appraisal[] result = new Appraisal[_loadedAppraisals.Count];
			_loadedAppraisals.Values.CopyTo(result, 0);
			return result;
		}

		public void GetAppraisals(AppraisalFilter filter, GetAppraisalsCallback callback)
		{
			Guard.ArgumentNotNull(callback, "callback");

			if (filter == AppraisalFilter.MyAppraisals)
			{
				callback(true, GetLocalAppraisals());
				return;
			}

			_appraisalServiceProxy.GetAppraisals
				(
					GET_APPRAISALS_TIMEOUT, UserId, ClientFilterToSvcFilter(filter),
					delegate(bool success, GlobalBank.AppraiserWorkbench.WebServiceProxies.AppraisalService.Appraisal[] appraisals)
					{
						OnGetAppraisalsComplete(success, appraisals, filter, callback);
					}
				);
		}

		private void OnGetAppraisalsComplete(bool success, GlobalBank.AppraiserWorkbench.WebServiceProxies.AppraisalService.Appraisal[] appraisals, AppraisalFilter filter, GetAppraisalsCallback callback)
		{
			Appraisal[] result = null;

			if (success)
			{
				List<Appraisal> appraisalsResult = new List<Appraisal>();

				foreach (GlobalBank.AppraiserWorkbench.WebServiceProxies.AppraisalService.Appraisal wsAppraisal in appraisals)
				{
					if (Contains(wsAppraisal.Id))
						appraisalsResult.Add(Load(wsAppraisal.Id));
					else
					{
						Appraisal appraisal = SvcAppraisalToClientAppraisal(wsAppraisal);
						appraisalsResult.Add(appraisal);

						if (filter == AppraisalFilter.MyAppraisals)
							Save(appraisal);
					}
				}

				result = appraisalsResult.ToArray();
			}

			OnServiceOperationNotification(success);
			callback(success, result);
		}

		public void LockAppraisal(Appraisal appraisal, LockAppraisalCallback callback)
		{
			Guard.ArgumentNotNull(appraisal, "appraisal");

			Logger.Write(new AppraisalServiceAuditEntry(appraisal.Id, AppraisalServiceAuditEntry.Reason.AssignmentRequest));

			_appraisalServiceProxy.LockAppraisal
				(
					LOCK_APPRAISALS_TIMEOUT, UserId, appraisal.Id,
					delegate(bool success, bool locked)
					{
						OnLockAppraisalComplete(success, locked, appraisal, callback);
					}
				);
		}

		private void OnLockAppraisalComplete(bool success, bool locked, Appraisal appraisal, LockAppraisalCallback callback)
		{
			if (success)
			{
				if (locked)
				{
					Logger.Write(new AppraisalServiceAuditEntry(appraisal.Id, AppraisalServiceAuditEntry.Reason.AssignmentAccepted));
					appraisal.Status = Appraisal.AppraisalStatus.Editable;
					Save(appraisal);
					ResumeAttachmentTasks(appraisal, null);
				}
				else
					Logger.Write(new AppraisalServiceAuditEntry(appraisal.Id, AppraisalServiceAuditEntry.Reason.AssignmentRejected));
			}

			OnServiceOperationNotification(success);
			if (callback != null)
				callback(success, appraisal, locked);
		}

		public void ReleaseAppraisal(Appraisal appraisal, ReleaseAppraisalCallback callback)
		{
			Guard.ArgumentNotNull(appraisal, "appraisal");
			appraisal.Status = Appraisal.AppraisalStatus.Submitted;
			Save(appraisal);
			Logger.Write(new AppraisalServiceAuditEntry(appraisal.Id, AppraisalServiceAuditEntry.Reason.Submitted));
			EnsureAttachmentsAreClosed(appraisal);
			UploadAttachments(appraisal);
			ReleaseAppraisalIfUploadsFinished(appraisal, callback);
		}

		private void EnsureAttachmentsAreClosed(Appraisal appraisal)
		{
			foreach (Attachment attachment in appraisal.Attachments)
			{
				if (attachment.FileName != null && File.Exists(attachment.FileName))
				{
					try
					{
						using (FileStream stream = File.Open(attachment.FileName, FileMode.Open, FileAccess.Read))
						{ }
					}
					catch (Exception)
					{
						throw new InvalidOperationException();
					}
				}
			}
		}

		private void UploadAttachments(Appraisal appraisal)
		{
			foreach (Attachment attachment in appraisal.Attachments)
			{
				StopWatchingAttachmentForChanges(attachment);

				if (attachment.Status == Attachment.AttachmentStatus.AvailableModified)
				{
					attachment.Status = Attachment.AttachmentStatus.Uploading;
					_fileTransferService.BeginUpload(attachment.DocumentUrl, attachment.FileName, UploadFinishedHandler);
				}
				else
					attachment.Status = Attachment.AttachmentStatus.Uploaded;
			}
		}

		private Appraisal SvcAppraisalToClientAppraisal(GlobalBank.AppraiserWorkbench.WebServiceProxies.AppraisalService.Appraisal appraisal)
		{
			Appraisal converted = new Appraisal();

			converted.DateToComplete = appraisal.DateToComplete;
			converted.Description = appraisal.Description;
			converted.Id = appraisal.Id;
			converted.Notes = appraisal.Notes;
			converted.PropertyType = SvcPropertyTypeToClientPropertyType(appraisal.PropertyType);
			converted.PropertyAddress = SvcPropertyAddressToClientPropertyAddress(appraisal.PropertyAddress);
			converted.Attachments = SvcAttachmentsToClientAttachments(appraisal.Attachments);
			return converted;
		}

		private List<Attachment> SvcAttachmentsToClientAttachments(AttachmentMetadata[] attachments)
		{
			List<Attachment> result = new List<Attachment>();

			if (attachments != null)
			{
				foreach (AttachmentMetadata attachment in attachments)
				{
					Attachment converted = new Attachment();
					converted.DocumentUrlString = attachment.Url;
					converted.DisplayName = attachment.FileName;
					result.Add(converted);
				}
			}

			return result;
		}

		private Address SvcPropertyAddressToClientPropertyAddress(GlobalBank.AppraiserWorkbench.WebServiceProxies.AppraisalService.Address address)
		{
			Address result = new Address();

			if (address != null)
			{
				result.Street1 = address.Street1;
				result.Street2 = address.Street2;
				result.City = address.City;
				result.State = address.State;
				result.Zip = address.Zip;
			}

			return result;
		}

		private PropertyType SvcPropertyTypeToClientPropertyType(GlobalBank.AppraiserWorkbench.WebServiceProxies.AppraisalService.PropertyType propertyType)
		{
			return ConvertEnum<PropertyType>(propertyType);
		}

		private GlobalBank.AppraiserWorkbench.WebServiceProxies.AppraisalService.AppraisalFilter ClientFilterToSvcFilter(AppraisalFilter filter)
		{
			return ConvertEnum<GlobalBank.AppraiserWorkbench.WebServiceProxies.AppraisalService.AppraisalFilter>(filter);
		}

		private TNewEnum ConvertEnum<TNewEnum>(object input)
		{
			return (TNewEnum)Enum.Parse(typeof(TNewEnum), input.ToString());
		}

		void IBuilderAware.OnBuiltUp(string id)
		{
			OnBuiltUp(id);
		}

		void IBuilderAware.OnTearingDown()
		{
			OnTearingDown();
		}

		protected virtual void OnBuiltUp(string id)
		{
			_workerThread = new Thread(new ThreadStart(WorkThreadProc));
			_workerThread.IsBackground = true;
			_workerThread.Priority = ThreadPriority.BelowNormal;
			_workerThread.Name = "AppraisalManagerServiceAgent Worker Thread";
			_workerThread.Start();
		}

		protected virtual void OnTearingDown()
		{
		}

		protected bool Contains(string appraisalId)
		{
			return _loadedAppraisals.ContainsKey(appraisalId);
		}

		protected Appraisal Load(string appraisalId)
		{
			lock (_lockObject)
			{
				if (_loadedAppraisals.ContainsKey(appraisalId))
					return _loadedAppraisals[appraisalId];

				string filename = FilenameForAppraisalXml(appraisalId);

				if (!File.Exists(filename))
					throw new ArgumentException(
						String.Format(CultureInfo.CurrentCulture,
						Properties.Resources.AppraisalDoesNotExist, appraisalId));

				Appraisal result = null;
				using (FileStream stream = new FileStream(filename, FileMode.Open, FileAccess.Read))
				{
					result = (Appraisal)new XmlSerializer(typeof(Appraisal)).Deserialize(stream);
					_loadedAppraisals[result.Id] = result;
					OnMyAppraisalsUpdated();
				}

				ResumeAttachmentTasks(result, null);
				return result;
			}
		}

		private void ResumeAttachmentTasks(Appraisal appraisal, ReleaseAppraisalCallback callback)
		{
			foreach (Attachment attachment in appraisal.Attachments)
			{
				switch (attachment.Status)
				{
					case Attachment.AttachmentStatus.NotAvailable:
						attachment.Status = Attachment.AttachmentStatus.Downloading;
						_fileTransferService.BeginDownload(attachment.DocumentUrl, FilenameForAttachment(appraisal.Id, attachment.DisplayName), DownloadFinishedHandler);
						break;
					case Attachment.AttachmentStatus.AvailableNotModified:
						WatchAttachmentForChanges(attachment);
						break;
					case Attachment.AttachmentStatus.ToBeUploaded:
						attachment.Status = Attachment.AttachmentStatus.Uploading;
						_fileTransferService.BeginUpload(attachment.DocumentUrl, attachment.FileName, UploadFinishedHandler);
						break;
				}
			}
			if (appraisal.Status == Appraisal.AppraisalStatus.Submitted)
				ReleaseAppraisalIfUploadsFinished(appraisal, callback);
		}

		protected void Save(Appraisal appraisal)
		{
			lock (_lockObject)
			{
				_loadedAppraisals[appraisal.Id] = appraisal;

				string extendedPath = PathForAppraisal(appraisal.Id);

				if (!Directory.Exists(extendedPath))
					Directory.CreateDirectory(extendedPath);

				using (FileStream stream = new FileStream(FilenameForAppraisalXml(appraisal.Id), FileMode.Create, FileAccess.Write))
				using (XmlTextWriter writer = new XmlTextWriter(stream, Encoding.UTF8))
				{
					writer.Formatting = Formatting.Indented;
					XmlSerializer se = new XmlSerializer(typeof(Appraisal));
					se.Serialize(writer, appraisal);
				}

				OnMyAppraisalsUpdated();
			}
		}

		protected void Delete(string appraisalId)
		{
			lock (_lockObject)
			{
				string extendedPath = PathForAppraisal(appraisalId);

				if (Directory.Exists(extendedPath))
					Directory.Delete(extendedPath, true);

				_loadedAppraisals.Remove(appraisalId);
			}
		}

		private void CleanupLocalStorage(List<string> appraisalIds)
		{
			if (Directory.Exists(BaseStoragePath) == false) return;

			foreach (string directory in Directory.GetDirectories(BaseStoragePath))
			{
				string pathPart = Path.GetFileName(directory);

				if (pathPart.StartsWith(_directoryPrepend))
				{
					string id = pathPart.Substring(_directoryPrepend.Length);

					if (!appraisalIds.Contains(id))
					{
						if (_loadedAppraisals.ContainsKey(id))
						{
							lock (_lockObject)
							{
								foreach (Attachment attachment in _loadedAppraisals[id].Attachments)
									StopWatchingAttachmentForChanges(attachment);
								_loadedAppraisals.Remove(id);
							}
							OnMyAppraisalsUpdated();
							try
							{
								Directory.Delete(directory, true);
							}
							catch (IOException e)
							{
								ExceptionPolicy.HandleException(e, "Default Policy");
							}
							catch
							{
								throw;
							}
						}
					}
				}
			}
		}

		public IEnumerator<Appraisal> GetEnumerator()
		{
			lock (_lockObject)
			{
				return _loadedAppraisals.Values.GetEnumerator();
			}
		}

		protected void LoadLocalAppraisals()
		{
			_loadedAppraisals.Clear();

			foreach (string directory in Directory.GetDirectories(BaseStoragePath))
			{
				string relativePath = Path.GetFileName(directory);

				if (relativePath.StartsWith(_directoryPrepend))
				{
					string appraisalId = relativePath.Substring(_directoryPrepend.Length);
					try
					{
						Load(appraisalId);
					}
					catch (ArgumentException)
					{
						// Something's wrong whith this appraisal.
						// Tipically some needed file was deleted deliberately, so we kill the appraisal.
						Delete(appraisalId);
					}
				}
			}
		}


		protected virtual void OnServiceOperationNotification(bool success)
		{
			_lastSuccessCode = success;

			if (ServiceOperationNotification != null)
				ServiceOperationNotification(this, new EventArgs<bool>(success));
		}

		private string FilenameForAppraisalXml(string appraisalId)
		{
			return Path.Combine(PathForAppraisal(appraisalId), "Appraisal.xml");
		}

		private string PathForAppraisal(string appraisalId)
		{
			return Path.Combine(BaseStoragePath, String.Format("{0}{1}", _directoryPrepend, appraisalId));
		}

		private string FilenameForAttachment(string appraisalId, string displayName)
		{
			return Path.Combine(PathForAppraisal(appraisalId), displayName);
		}

		private void DownloadFinishedHandler(Uri url, bool success)
		{
			lock (_lockObject)
			{
				Appraisal appraisal;
				Attachment attachment;

				OnServiceOperationNotification(success);

				if (!TryFindAppraisalAndAttachmentForUrl(url, out appraisal, out attachment))
					return;

				if (success)
				{
					attachment.Status = Attachment.AttachmentStatus.AvailableNotModified;
					attachment.FileName = FilenameForAttachment(appraisal.Id, attachment.DisplayName);
					attachment.LocalCreationTime = File.GetLastWriteTimeUtc(attachment.FileName);
					WatchAttachmentForChanges(attachment);
					Save(appraisal);
				}
				else
					attachment.Status = Attachment.AttachmentStatus.NotAvailable;

				return;
			}
		}

		private void UploadFinishedHandler(Uri url, bool success)
		{
			lock (_lockObject)
			{
				Appraisal appraisal;
				Attachment attachment;

				OnServiceOperationNotification(success);

				if (!TryFindAppraisalAndAttachmentForUrl(url, out appraisal, out attachment))
					return;

				// TODO: Log failure?
				attachment.Status = success ? Attachment.AttachmentStatus.Uploaded : Attachment.AttachmentStatus.ToBeUploaded;
				Save(appraisal);

				ReleaseAppraisalIfUploadsFinished(appraisal, null);

			}
		}

		private void ReleaseAppraisalIfUploadsFinished(Appraisal appraisal, ReleaseAppraisalCallback callback)
		{
			if (appraisal.Status != Appraisal.AppraisalStatus.Submitted)
			{
				// We don't call ReleaseAppraisal if there is an attachment to be uploaded
				foreach (Attachment attachment in appraisal.Attachments)
					if (attachment.Status != Attachment.AttachmentStatus.Uploaded)
						return;
			}

			_appraisalServiceProxy.ReleaseAppraisal(GET_APPRAISALS_TIMEOUT, UserId, appraisal.Id, delegate(bool success, object result)
			{
				OnServiceOperationNotification(success);
				if (success)
				{
					Logger.Write(new AppraisalServiceAuditEntry(appraisal.Id, AppraisalServiceAuditEntry.Reason.Released));
					Delete(appraisal.Id);
					OnMyAppraisalsUpdated();
				}
				if (callback != null)
					callback(success);
			});
		}

		protected void OnMyAppraisalsUpdated()
		{
			if (MyAppraisalsUpdated != null)
				MyAppraisalsUpdated(this, EventArgs.Empty);
		}

		private void WatchAttachmentForChanges(Attachment attachment)
		{
			if (attachment.Status == Attachment.AttachmentStatus.AvailableNotModified && !_watchedAttachments.Contains(attachment))
			{
				_watchedAttachments.Add(attachment);
				_fileWatcherService.BeginWatchFile(attachment.FileName, FileChangedHandler);
			}
		}

		private void StopWatchingAttachmentForChanges(Attachment attachment)
		{
			if (_watchedAttachments.Contains(attachment))
			{
				_watchedAttachments.Remove(attachment);
				_fileWatcherService.EndWatchFile(attachment.FileName);
			}
		}

		private void FileChangedHandler(string filename)
		{
			List<Attachment> toRemove = new List<Attachment>();

			foreach (Attachment attachment in _watchedAttachments)
			{
				if (filename.Equals(attachment.FileName, StringComparison.InvariantCultureIgnoreCase) &&
						attachment.LocalCreationTime != File.GetLastWriteTimeUtc(attachment.FileName))
				{
					toRemove.Add(attachment);
					attachment.Status = Attachment.AttachmentStatus.AvailableModified;

					foreach (Appraisal appraisal in this)
					{
						if (appraisal.Attachments.Contains(attachment))
						{
							Save(appraisal);
							break;
						}
					}
				}
			}

			foreach (Attachment att in toRemove)
				StopWatchingAttachmentForChanges(att);
		}

		private bool TryFindAppraisalAndAttachmentForUrl(Uri url, out Appraisal appraisal, out Attachment attachment)
		{
			appraisal = null;
			attachment = null;

			foreach (Appraisal app in this)
			{
				foreach (Attachment att in app.Attachments)
				{
					if (att.DocumentUrl == url)
					{
						appraisal = app;
						attachment = att;
						return true;
					}
				}
			}
			return false;
		}

		private void WorkThreadProc()
		{
			do
			{
				PollForMyAppraisals();
			} while (!_shutdownEvent.WaitOne(SERVICE_POLLING_INTERVAL, false));
			Thread.CurrentThread.Join();
		}

		protected void PollForMyAppraisals()
		{
			_appraisalServiceProxy.GetAppraisals(GET_APPRAISALS_TIMEOUT,
				UserId,
				ClientFilterToSvcFilter(AppraisalFilter.MyAppraisals),
				delegate(bool success, GlobalBank.AppraiserWorkbench.WebServiceProxies.AppraisalService.Appraisal[] appraisals)
				{
					if (success)
					{
						CleanupLocalStorage(GetAppraisalIds(appraisals));
						foreach (GlobalBank.AppraiserWorkbench.WebServiceProxies.AppraisalService.Appraisal appraisal in appraisals)
						{
							if (!Contains(appraisal.Id))
							{
								Logger.Write(new AppraisalServiceAuditEntry(appraisal.Id, AppraisalServiceAuditEntry.Reason.AssignmentAccepted));
								Save(SvcAppraisalToClientAppraisal(appraisal));
							}
						}
						ResumePendingTasks();
					}
					OnServiceOperationNotification(success);
				});
		}

		private void ResumePendingTasks()
		{
			Appraisal[] locals = GetLocalAppraisals();
			foreach (Appraisal appraisal in locals)
			{
				ResumeAttachmentTasks(appraisal, null);
			}
		}

		private List<string> GetAppraisalIds(GlobalBank.AppraiserWorkbench.WebServiceProxies.AppraisalService.Appraisal[] appraisals)
		{
			List<string> result = new List<string>();
			foreach (GlobalBank.AppraiserWorkbench.WebServiceProxies.AppraisalService.Appraisal appraisal in appraisals)
				result.Add(appraisal.Id);
			return result;
		}
	}
}
