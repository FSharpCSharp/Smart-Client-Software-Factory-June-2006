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
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.EventBroker;
using Microsoft.Practices.ObjectBuilder;
using GlobalBank.AppraiserWorkbench.AppraisalServiceAgent;
using GlobalBank.AppraiserWorkbench.AppraisalServiceAgent.BusinessEntities;
using GlobalBank.AppraiserWorkbench.AppraiserWorkbenchModule.Constants;
using GlobalBank.Infrastructure.Interface.Services;
using System.Security.Permissions;
using GlobalBank.Infrastructure.Interface;
using Microsoft.Practices.CompositeUI.Utility;

namespace GlobalBank.AppraiserWorkbench.AppraiserWorkbenchModule.Views.AppraisalDetail
{
	public class AppraisalDetailViewPresenter : Presenter<IAppraisalDetailView>
	{
		private IAppraisalManagementServiceAgent _appraisalService;
		private IMessageBoxService _messageService;
		private Appraisal _current;

		[InjectionConstructor]
		public AppraisalDetailViewPresenter
			(
				[ServiceDependency] IAppraisalManagementServiceAgent appraisalService,
				[ServiceDependency] IMessageBoxService messageService
			)
		{
			_appraisalService = appraisalService;
			_messageService = messageService;
		}

		[EventSubscription(EventTopicNames.WorkingAppraisalSelected, Thread = ThreadOption.UserInterface)]
		public void WorkingAppraisalSelectedHandler(object sender, EventArgs<Appraisal> e)
		{
			ShowAppraisal(e.Data);
		}

		public void ShowAppraisal(Appraisal appraisal)
		{
			if (_current != null)
			{
				foreach (Attachment attachment in _current.Attachments)
				{
					attachment.StatusChanged -= OnAttachmentStatusChanged;
				}
			}

			if (appraisal == null)
				View.ResetViewToEmpty();
			else
			{
				foreach (Attachment attachment in appraisal.Attachments)
				{
					attachment.StatusChanged += OnAttachmentStatusChanged;
				}
				View.ShowAppraisal(appraisal);
			}

			_current = appraisal;
		}

		private void OnAttachmentStatusChanged(object sender, EventArgs e)
		{
			Attachment attachment = sender as Attachment;
			View.UpdateAttachmentStatus(attachment);
		}

		internal void ShowAppraisalSelection()
		{
			WorkItem.Commands[CommandNames.ViewAvailableAppraisals].Execute();
		}

		[SecurityPermission(SecurityAction.Demand, Unrestricted=true)]
		public void ShowAttachment(Attachment attachment)
		{
			Guard.ArgumentNotNull(attachment, "attachment");

			switch (attachment.Status)
			{
				case Attachment.AttachmentStatus.AvailableNotModified:
				case Attachment.AttachmentStatus.AvailableModified:
					Process.Start(attachment.FileName);
					break;

				case Attachment.AttachmentStatus.Downloading:
					_messageService.Show(Properties.Resources.MessageAttachmentIsDownloading);
					break;

				default:
					_messageService.Show(Properties.Resources.MessageAttachmentIsNotAvailable);
					break;
			}
		}

		public void SubmitAppraisal(Appraisal appraisal)
		{
			Guard.ArgumentNotNull(appraisal, "appraisal");

			if (appraisal.Attachments.Count > 0)
				foreach (Attachment attachment in appraisal.Attachments)
				{
					if (attachment.Status == Attachment.AttachmentStatus.NotAvailable ||
						attachment.Status == Attachment.AttachmentStatus.Downloading)
					{
						_messageService.Show(Properties.Resources.CantSubmitAppraisalWhenDownloading);
						return;
					}
				}
			try
			{
				_appraisalService.ReleaseAppraisal(appraisal, delegate(bool success)
				{
					if (!success)
						_messageService.Show(Properties.Resources.MessageAppraisalSubmitFailed);
				});
			}
			catch (InvalidOperationException)
			{
				_messageService.Show(Properties.Resources.MessageAttachmentsAreBeingEdited);
			}
		}
	}
}
