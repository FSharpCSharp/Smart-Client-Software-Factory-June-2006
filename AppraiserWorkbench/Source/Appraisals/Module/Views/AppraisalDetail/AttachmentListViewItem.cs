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
using System.Text;
using System.Windows.Forms;
using GlobalBank.AppraiserWorkbench.AppraisalServiceAgent.BusinessEntities;
using Microsoft.Practices.CompositeUI.Utility;

namespace GlobalBank.AppraiserWorkbench.AppraiserWorkbenchModule.Views.AppraisalDetail
{
	[Serializable]
	public class AttachmentListViewItem : ListViewItem
	{
		private Attachment _attachment;

		public AttachmentListViewItem(Attachment attachment)
		{
			Guard.ArgumentNotNull(attachment, "attachment");

			Name = attachment.DisplayName;
			Text = attachment.DisplayName;
			SubItems.Add(AttachmentStatusToDisplayText(attachment));
			_attachment = attachment;
		}

		private static string AttachmentStatusToDisplayText(Attachment attachment)
		{
			switch (attachment.Status)
			{
				case Attachment.AttachmentStatus.AvailableModified:
					return Properties.Resources.Modified;

				case Attachment.AttachmentStatus.AvailableNotModified:
					return Properties.Resources.Available;

				case Attachment.AttachmentStatus.Downloading:
					return Properties.Resources.Downloading;

				case Attachment.AttachmentStatus.NotAvailable:
					return Properties.Resources.NotAvailable;

				case Attachment.AttachmentStatus.ToBeUploaded:
					return Properties.Resources.ToBeUploaded;

				case Attachment.AttachmentStatus.Uploaded:
					return Properties.Resources.Uploaded;

				case Attachment.AttachmentStatus.Uploading:
					return Properties.Resources.Uploading;
			}

			return attachment.Status.ToString();
		}

		public Attachment Attachment
		{
			get { return _attachment; }
		}

		public void UpdateAttachmentStatus()
		{
			SubItems[1].Text = AttachmentStatusToDisplayText(_attachment);
		}
	}
}

