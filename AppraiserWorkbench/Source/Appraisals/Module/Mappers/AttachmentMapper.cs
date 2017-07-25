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
using System.Globalization;
using System.Windows.Forms;
using GlobalBank.AppraiserWorkbench.AppraisalServiceAgent.BusinessEntities;
using GlobalBank.AppraiserWorkbench.AppraiserWorkbenchModule.Views.AppraisalDetail;
using Microsoft.Practices.CompositeUI.Utility;

namespace GlobalBank.AppraiserWorkbench.AppraiserWorkbenchModule.Mappers
{
	public static class AttachmentMapper
	{
		public static ListViewItem ToListViewItem(Attachment attachment)
		{
			Guard.ArgumentNotNull(attachment, "attachment");

			return new AttachmentListViewItem(attachment);
		}

		public static Attachment FromListViewItem(ListViewItem listViewItem)
		{
			Guard.ArgumentNotNull(listViewItem, "listViewItem");

			AttachmentListViewItem attachmentListViewItem = listViewItem as AttachmentListViewItem;

			if (attachmentListViewItem == null)
				throw new ArgumentException(String.Format(CultureInfo.CurrentCulture,
					Properties.Resources.WrongTypeArgument,
					typeof(AttachmentListViewItem).Name),
					"listViewItem");

			return attachmentListViewItem.Attachment;
		}
	}
}
