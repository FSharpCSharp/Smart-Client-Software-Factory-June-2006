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
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Microsoft.Practices.ObjectBuilder;
using GlobalBank.AppraiserWorkbench.AppraisalServiceAgent.BusinessEntities;
using NUnit.Framework;
using GlobalBank.Infrastructure.Interface.Commands;
using GlobalBank.Infrastructure.Interface;
using GlobalBank.Infrastructure.Shell.Tests.Mocks;

namespace GlobalBank.AppraiserWorkbench.AppraisalServiceAgent.Tests
{
	[TestFixture]
	public class AppraisalManagementServiceAgentFixture
	{
		[Test]
		public void RetrievingAppraisalCausesCallToWebServiceToRetrieveAppraisal()
		{
			bool delegateCalled = false;

			using (TestableAppraisalManagementServiceAgent service = new TestableAppraisalManagementServiceAgent())
			{
				service.GetAppraisals(AppraisalFilter.Unassigned, delegate(bool success, Appraisal[] appraisals)
				{
					foreach (Appraisal appraisal in appraisals)
						Assert.IsTrue(service.AppraisalService.RetrievedAppraisals.Contains(appraisal.Id));

					delegateCalled = true;
				});
			}

			Assert.IsTrue(delegateCalled);
		}

		[Test]
		public void RetrievingAppraisalAlreadyInBackingStoreUsesBackingStoreVersion()
		{
			using (TestableAppraisalManagementServiceAgent service = new TestableAppraisalManagementServiceAgent())
			{
				Appraisal localAppraisal = new Appraisal();
				localAppraisal.Id = service.AppraisalService.MyAppraisal.Id;
				localAppraisal.Notes = "Not " + service.AppraisalService.MyAppraisal.Notes;
				service.Save(localAppraisal);

				service.GetAppraisals(AppraisalFilter.MyAppraisals, delegate(bool success, Appraisal[] results)
				{
					Assert.AreEqual(localAppraisal.Notes, results[0].Notes);
				});
			}
		}

		[Test]
		public void LockingAppraisalCausesServiceAgentToLockAppraisalInWebService()
		{
			bool delegateCalled = false;

			using (TestableAppraisalManagementServiceAgent service = new TestableAppraisalManagementServiceAgent())
			{
				service.GetAppraisals(AppraisalFilter.Unassigned, delegate(bool success, Appraisal[] results)
				{
					service.LockAppraisal(results[0], delegate
					{
						delegateCalled = true;
						Assert.IsTrue(service.AppraisalService.LockedAppraisals.Contains(results[0].Id));
					});
				});
			}

			Assert.IsTrue(delegateCalled);
		}

		[Test]
		public void GettingAppraisalsGetsThemInNotEditableStatus()
		{
			using (TestableAppraisalManagementServiceAgent service = new TestableAppraisalManagementServiceAgent())
			{
				service.GetAppraisals(AppraisalFilter.Unassigned, delegate(bool success, Appraisal[] results)
				{
					Assert.AreEqual(Appraisal.AppraisalStatus.NonEditable, results[0].Status);
				});
			}
		}

		[Test]
		public void LockingAppraisalSetAppraisalStatusToEditing()
		{
			using (TestableAppraisalManagementServiceAgent service = new TestableAppraisalManagementServiceAgent())
			{
				service.GetAppraisals(AppraisalFilter.Unassigned, delegate(bool success, Appraisal[] results)
				{
					service.LockAppraisal(results[0], delegate
					{
						Assert.AreEqual(Appraisal.AppraisalStatus.Editable, results[0].Status);
					});
				});
			}
		}

		[Test]
		public void LockingAppraisalCausesAppraisalToBeSavedIntoLocalCache()
		{
			using (TestableAppraisalManagementServiceAgent service = new TestableAppraisalManagementServiceAgent())
			{
				service.GetAppraisals(AppraisalFilter.Unassigned, delegate(bool success, Appraisal[] results)
				{
					service.LockAppraisal(results[0], delegate
					{
						Assert.IsTrue(service.Contains(results[0].Id));
					});
				});
			}
		}

		[Test]
		public void LockingAppraisalCausesAttachmentsToBeDownloaded()
		{
			using (TestableAppraisalManagementServiceAgent service = new TestableAppraisalManagementServiceAgent())
			{
				service.GetAppraisals(AppraisalFilter.Unassigned, delegate(bool success, Appraisal[] results)
				{
					service.LockAppraisal(results[0], delegate
					{
						Assert.IsTrue(service.FileTransferService.DownloadedUrls.Contains(results[0].Attachments[0].DocumentUrl));
					});
				});
			}
		}

		[Test]
		public void ServiceGetsMyAppraisalsOnStartup()
		{
			using (TestableAppraisalManagementServiceAgent service = new TestableAppraisalManagementServiceAgent())
			{
				((IBuilderAware)service).OnBuiltUp(null);

				Assert.IsTrue(service.AppraisalService.RetrievedAppraisals.Contains(service.AppraisalService.MyAppraisal.Id));
			}
		}

		[Test]
		public void ReleaseAppraisalSetsItsStatusToSubmitted()
		{
			using (TestableAppraisalManagementServiceAgent service = new TestableAppraisalManagementServiceAgent())
			{
				Appraisal appraisal = new Appraisal();
				appraisal.Id = "1";
				service.LockAppraisal(appraisal, null);
				service.ReleaseAppraisal(appraisal,null);

				Assert.AreEqual(Appraisal.AppraisalStatus.Submitted, appraisal.Status);
			}
		}

		[Test]
		public void ReleasingAnAppraisalCausesUploadReleaseAndEvent()
		{
			using (TestableAppraisalManagementServiceAgent service = new TestableAppraisalManagementServiceAgent())
			{
				Appraisal appraisal = null;

				service.GetAppraisals(AppraisalFilter.MyAppraisals, delegate(bool success, Appraisal[] myAppraisals)
				{
					appraisal = myAppraisals[0];
				});

				Attachment attachment = appraisal.Attachments[0];
				attachment.Status = Attachment.AttachmentStatus.AvailableModified;

				bool eventFired = false;
				service.MyAppraisalsUpdated += delegate(object sender, EventArgs e)
				{
					eventFired = true;
				};

				service.Save(appraisal);
				service.ReleaseAppraisal(appraisal,null);

				WaitForCondition(10000, delegate() { return service.Contains(appraisal.Id); });

				Assert.IsTrue(service.FileTransferService.UploadedUrls.Contains(attachment.DocumentUrl));
				Assert.IsTrue(service.AppraisalService.ReleasedAppraisals.Contains(appraisal.Id));
				Assert.IsTrue(eventFired);
			}
		}

		[Test]
		public void ReleasingAppraisalDoesNotUploadsUnmodifiedAttachments()
		{
			using (TestableAppraisalManagementServiceAgent service = new TestableAppraisalManagementServiceAgent())
			{
				Appraisal appraisal = null;

				service.GetAppraisals(AppraisalFilter.MyAppraisals, delegate(bool success, Appraisal[] myAppraisals)
				{
					appraisal = myAppraisals[0];
				});

				Attachment attachment = appraisal.Attachments[0];
				attachment.Status = Attachment.AttachmentStatus.AvailableNotModified;
				service.Save(appraisal);
				service.ReleaseAppraisal(appraisal,null);

				WaitForCondition(10000, delegate() { return service.Contains(appraisal.Id); });

				Assert.IsFalse(service.FileTransferService.UploadedUrls.Contains(attachment.DocumentUrl));
			}
		}

		[Test]
		[ExpectedException(typeof(InvalidOperationException))]
		public void LockedAttachmentFilesAreNotUploaded()
		{
			using (TestableAppraisalManagementServiceAgent service = new TestableAppraisalManagementServiceAgent())
			{
				Appraisal appraisal = null;
				service.GetAppraisals(AppraisalFilter.MyAppraisals, delegate(bool success, Appraisal[] myAppraisals)
				{
					appraisal = myAppraisals[0];
				});

				Attachment attachment = appraisal.Attachments[0];
				attachment.Status = Attachment.AttachmentStatus.AvailableNotModified;
				attachment.FileName = Path.GetTempFileName();
				service.Save(appraisal);

				try
				{
					using (FileStream stream = File.Open(attachment.FileName, FileMode.OpenOrCreate, FileAccess.Read, FileShare.None))
					{
						service.ReleaseAppraisal(appraisal,null);
					}
				}
				finally
				{
					File.Delete(attachment.FileName);
				}
			}
		}

		[Test]
		public void GettingAppraisalsAssignedToMeSavesThemToLocalCache()
		{
			using (TestableAppraisalManagementServiceAgent service = new TestableAppraisalManagementServiceAgent())
			{
				service.GetAppraisals(AppraisalFilter.MyAppraisals, delegate(bool success, Appraisal[] myAppraisals)
				{
					foreach (Appraisal appraisal in myAppraisals)
						Assert.IsTrue(service.Contains(appraisal.Id));
				});
			}
		}

		[Test]
		public void GettingUnassignedAppraisalDoesntSavesThemToLocalCache()
		{
			using (TestableAppraisalManagementServiceAgent service = new TestableAppraisalManagementServiceAgent())
			{
				service.GetAppraisals(AppraisalFilter.Unassigned, delegate(bool success, Appraisal[] unassignedAppraisals)
				{
					foreach (Appraisal appraisal in unassignedAppraisals)
						Assert.IsFalse(service.Contains(appraisal.Id));
				});
			}
		}

		[Test]
		public void GetMyAppraisalsReturnsMyLocalCachedAppraisals()
		{
			using (TestableAppraisalManagementServiceAgent service = new TestableAppraisalManagementServiceAgent())
			{
				Appraisal myAppraisal = null;
				Appraisal localAppraisal = new Appraisal();
				localAppraisal.Id = service.AppraisalService.MyAppraisal.Id;
				localAppraisal.Notes = "Not " + service.AppraisalService.MyAppraisal.Notes;
				service.Save(localAppraisal);
				service.GetAppraisals(AppraisalFilter.MyAppraisals, delegate(bool success, Appraisal[] appraisals)
				{
					myAppraisal = appraisals[0];
				});

				Appraisal[] locals = service.GetLocalAppraisals();
				Assert.AreEqual(myAppraisal, locals[0]);
			}
		}

		[Test]
		public void WhenAttachmentUploadingFailsServiceAgentFiresServiceOperationNotification()
		{
			using (TestableAppraisalManagementServiceAgent service = new TestableAppraisalManagementServiceAgent())
			{
				service.FileTransferService.FailCalls = true;
				Appraisal appraisal = new Appraisal();
				appraisal.Id = "123";
				Attachment att = new Attachment();
				att.DocumentUrlString = "file:///dummy";
				att.DisplayName = "some display name";
				att.FileName = "some file name";
				att.Status = Attachment.AttachmentStatus.AvailableModified;
				appraisal.Attachments.Add(att);

				bool eventFired = false;
				service.ServiceOperationNotification += delegate(object sender, EventArgs<bool> args)
				{
					Assert.IsFalse(args.Data);
					eventFired = true;
				};

				service.ReleaseAppraisal(appraisal,null);

				Assert.IsTrue(eventFired);
			}
		}

		[Test]
		public void WhenAttachmentDownloadingFailsServiceAgentFiresServiceOperationNotification()
		{
			using (TestableAppraisalManagementServiceAgent service = new TestableAppraisalManagementServiceAgent())
			{
				service.FileTransferService.FailCalls = true;
				Appraisal appraisal = new Appraisal();
				appraisal.Id = "123";
				Attachment att = new Attachment();
				att.DocumentUrlString = "file:///dummy";
				att.DisplayName = "some display name";
				att.FileName = "some file name";
				att.Status = Attachment.AttachmentStatus.NotAvailable;
				appraisal.Attachments.Add(att);

				bool eventFired = false;

				service.ServiceOperationNotification += delegate(object sender, EventArgs<bool> args)
				{
					if (!eventFired)
					{
						Assert.IsFalse(args.Data);
						eventFired = true;
					}
				};

				service.LockAppraisal(appraisal, null);

				Assert.IsTrue(eventFired);
			}
		}

		private delegate bool WaitForConditionDelegate();
		private static void WaitForCondition(int maxWait, WaitForConditionDelegate condition)
		{
			int waited = 0;
			while (waited < maxWait && !condition())
			{
				Thread.Sleep(50);
				waited += 50;
			}
		}

		public class TestableAppraisalManagementServiceAgent : AppraisalManagementServiceAgent
		{
			public TestableAppraisalManagementServiceAgent()
				: base(new MockAppraisalWebService(), new MockFileTransferService(), new MockFileWatcherService())
			{
				BaseStoragePath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
				Directory.CreateDirectory(BaseStoragePath);
				base.LoadLocalAppraisals();
				base.PollForMyAppraisals();
			}

			public new MockAppraisalWebService AppraisalService
			{
				get { return base.AppraisalService as MockAppraisalWebService; }
			}

			public new MockFileTransferService FileTransferService
			{
				get { return base.FileTransferService as MockFileTransferService; }
			}

			public new bool Contains(string appraisalId)
			{
				return base.Contains(appraisalId);
			}

			public new void Save(Appraisal appraisal)
			{
				base.Save(appraisal);
			}

			protected override void Dispose(bool disposing)
			{
				base.Dispose(disposing);

				if (Directory.Exists(BaseStoragePath))
					Directory.Delete(BaseStoragePath, true);
			}
		}

		public class MockAppraisalWebService : WebServiceProxies.AppraisalService.IAppraisalManagementServiceProxy
		{
			public WebServiceProxies.AppraisalService.Appraisal UnassignedAppraisal;
			public WebServiceProxies.AppraisalService.Appraisal MyAppraisal;
			public List<string> LockedAppraisals = new List<string>();
			public List<string> RetrievedAppraisals = new List<string>();
			public List<string> ReleasedAppraisals = new List<string>();
			public bool Fail = false;	// is set to true, every web method should fail

			public MockAppraisalWebService()
			{
				UnassignedAppraisal = new WebServiceProxies.AppraisalService.Appraisal();
				UnassignedAppraisal.Id = "123";
				UnassignedAppraisal.Notes = "Remote Notes";
				UnassignedAppraisal.Attachments = new WebServiceProxies.AppraisalService.AttachmentMetadata[1];
				UnassignedAppraisal.Attachments[0] = new WebServiceProxies.AppraisalService.AttachmentMetadata();
				UnassignedAppraisal.Attachments[0].FileName = "Dummy File.txt";
				UnassignedAppraisal.Attachments[0].Url = "file:///dummy";

				MyAppraisal = new WebServiceProxies.AppraisalService.Appraisal();
				MyAppraisal.Id = "456";
				MyAppraisal.Notes = "Other Remote Notes";
				MyAppraisal.Attachments = new WebServiceProxies.AppraisalService.AttachmentMetadata[1];
				MyAppraisal.Attachments[0] = new WebServiceProxies.AppraisalService.AttachmentMetadata();
				MyAppraisal.Attachments[0].FileName = "Other Dummy File.txt";
				MyAppraisal.Attachments[0].Url = "file:///dummy";
			}

			private WebServiceProxies.AppraisalService.Appraisal[] GetAppraisals(string userId,
				WebServiceProxies.AppraisalService.AppraisalFilter filter)
			{
				WebServiceProxies.AppraisalService.Appraisal[] result;

				if (filter == WebServiceProxies.AppraisalService.AppraisalFilter.Unassigned)
					result = new WebServiceProxies.AppraisalService.Appraisal[1] { UnassignedAppraisal };
				else
					result = new WebServiceProxies.AppraisalService.Appraisal[1] { MyAppraisal };

				foreach (WebServiceProxies.AppraisalService.Appraisal appraisal in result)
					RetrievedAppraisals.Add(appraisal.Id);

				return result;
			}

			private bool LockAppraisal(string userId, string appraisalId)
			{
				LockedAppraisals.Add(appraisalId);
				return true;
			}

			public void GetAppraisals(int timeout, string userId,
				WebServiceProxies.AppraisalService.AppraisalFilter filter,
				CommandWithCallback<WebServiceProxies.AppraisalService.IAppraisalManagementService, WebServiceProxies.AppraisalService.Appraisal[]>.CallbackType callback)
			{
				if (Fail) callback(false, new WebServiceProxies.AppraisalService.Appraisal[0]);
				else callback(true, GetAppraisals(userId, filter));
			}

			public void LockAppraisal(int timeout, string userId, string appraisalId,
				CommandWithCallback<WebServiceProxies.AppraisalService.IAppraisalManagementService, bool>.CallbackType callback)
			{
				if (Fail) callback(false, false);
				else callback(LockAppraisal(userId, appraisalId), true);
			}

			public void ReleaseAppraisal(string userId, string appraisalId)
			{
				if (Fail) throw new Exception("Ask what to do!");
				else ReleasedAppraisals.Add(appraisalId);
			}

			public void Dispose()
			{
			}

			#region IAppraisalManagementServiceProxy Members


			public void ReleaseAppraisal(int timeout, string userId, string appraisalId, CommandWithCallback<WebServiceProxies.AppraisalService.IAppraisalManagementService, object>.CallbackType callback)
			{
				ReleaseAppraisal(userId, appraisalId);
			}

			#endregion
		}
	}
}
