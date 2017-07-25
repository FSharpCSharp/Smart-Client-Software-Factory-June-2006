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
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using GlobalBank.AppraiserWorkbench.AppraisalServiceAgent.BusinessEntities;
using GlobalBank.AppraiserWorkbench.AppraiserWorkbenchModule.Mappers;
using Microsoft.Practices.CompositeUI.Utility;

namespace GlobalBank.AppraiserWorkbench.AppraiserWorkbenchModule.Views.AppraisalDetail
{
	[SmartPart]
	public sealed partial class AppraisalDetailView : UserControl, IAppraisalDetailView
	{
		Appraisal _appraisal;
		private AppraisalDetailViewPresenter _presenter;

		public AppraisalDetailView()
		{
			InitializeComponent();
			_coverPanel.BringToFront();
			((IAppraisalDetailView)this).ResetViewToEmpty();
		}

		[CreateNew]
		public AppraisalDetailViewPresenter Presenter
		{
			set
			{
				Guard.ArgumentNotNull(value, "Presenter");
				_presenter = value;
				_presenter.View = this;
			}
			get
			{
				return _presenter;
			}
		}

		private delegate void ResetViewToEmptyDelegate();

		void IAppraisalDetailView.ResetViewToEmpty()
		{
			if (InvokeRequired)
				BeginInvoke(new ResetViewToEmptyDelegate(InnerResetViewToEmpty));
			else
				InnerResetViewToEmpty();
		}

		private void InnerResetViewToEmpty()
		{
			_appraisal = null;
			_coverPanel.Visible = true;
		}

		private delegate void UpdateAttachmentStatusDelegate(Attachment attachment);

		void IAppraisalDetailView.UpdateAttachmentStatus(Attachment attachment)
		{
			if (InvokeRequired)
				BeginInvoke(new UpdateAttachmentStatusDelegate(InnerUpdateAttachmentStatus), attachment);
			else
				InnerUpdateAttachmentStatus(attachment);
		}

		private void InnerUpdateAttachmentStatus(Attachment attachment)
		{
			foreach (ListViewItem item in _attachmentsListView.Items)
			{
				if (AttachmentMapper.FromListViewItem(item) == attachment)
				{
					((AttachmentListViewItem)item).UpdateAttachmentStatus();
					break;
				}
			}
		}

		private delegate void ShowAppraisalDelegate(Appraisal appraisal);

		void IAppraisalDetailView.ShowAppraisal(Appraisal appraisal)
		{
			Guard.ArgumentNotNull(appraisal, "appraisal");

			if (InvokeRequired)
				Invoke(new ShowAppraisalDelegate(InnerShowAppraisal), appraisal);
			else
				InnerShowAppraisal(appraisal);
		}

		private void InnerShowAppraisal(Appraisal appraisal)
		{
			_appraisal = appraisal;
			_coverPanel.Hide();

			_dateToCompleteLabel.Text = appraisal.DateToComplete.ToShortDateString();
			_descriptionTextBox.Text = appraisal.Description;
			_idLabel.Text = appraisal.Id;
			_notesTextBox.Text = appraisal.Notes;
			_propertyAddressTextBox.Text = AddressMapper.ToFormattedString(appraisal.PropertyAddress);
			_propertyTypeLabel.Text = PropertyTypeMapper.ToDisplayString(appraisal.PropertyType);

			_attachmentsListView.Items.Clear();

			if (appraisal.Attachments != null)
				foreach (Attachment attachment in appraisal.Attachments)
					_attachmentsListView.Items.Add(AttachmentMapper.ToListViewItem(attachment));
		}

		private void _coverLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			_presenter.ShowAppraisalSelection();
		}

		private void _attachmentsListView_DoubleClick(object sender, EventArgs e)
		{
			_presenter.ShowAttachment(AttachmentMapper.FromListViewItem(_attachmentsListView.SelectedItems[0]));
		}

		private void submitButton_Click(object sender, EventArgs e)
		{
			_presenter.SubmitAppraisal(_appraisal);
		}
	}
}
