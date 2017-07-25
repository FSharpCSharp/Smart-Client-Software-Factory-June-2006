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
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using GlobalBank.AppraiserWorkbench.AppraisalServiceAgent.BusinessEntities;
using GlobalBank.AppraiserWorkbench.AppraiserWorkbenchModule.Mappers;
using System.Text;
using Microsoft.Practices.CompositeUI;
using GlobalBank.Infrastructure.Interface.Services;
using Microsoft.Practices.CompositeUI.Utility;

namespace GlobalBank.AppraiserWorkbench.AppraiserWorkbenchModule.Views.AvailableAppraisals
{
	[SmartPart]
	public sealed partial class AvailableAppraisalsView : Form, IAvailableAppraisalsView
	{
		private AvailableAppraisalsViewPresenter _presenter;

		public AvailableAppraisalsView()
		{
			InitializeComponent();
			UpdateOkButton();
			ShowCoverPanel();
			InitializeProgressBar();
		}

		[CreateNew]
		public AvailableAppraisalsViewPresenter Presenter
		{
			set
			{
				Guard.ArgumentNotNull(value, "Presenter");

				_presenter = value;
				_presenter.View = this;
				_presenter.ShowAvailableAppraisals();
			}
			get
			{
				return _presenter;
			}
		}

		private delegate void SetAppraisalsDelegate(Appraisal[] appraisals);

		void IAvailableAppraisalsView.SetAppraisals(Appraisal[] appraisals)
		{
			Guard.ArgumentNotNull(appraisals, "appraisals");

			if (InvokeRequired)
				BeginInvoke(new SetAppraisalsDelegate(InnerSetAppraisals), new object[] { appraisals });
			else
				InnerSetAppraisals(appraisals);
		}

		IMessageBoxService _messageBoxService;

		[ServiceDependency]
		public IMessageBoxService MessageBoxService
		{
			get { return _messageBoxService; }
			set { _messageBoxService = value; }
		}

		private void InnerSetAppraisals(Appraisal[] appraisals)
		{
			if (appraisals == null || appraisals.Length == 0)
			{
				_messageBoxService.Show(
					Properties.Resources.NoAvailableAppraisals, 
					Properties.Resources.ApplicationTitle, 
					MessageBoxButtons.OK, 
					MessageBoxIcon.Information);
				_cancelButton.PerformClick();
			}

			HideCoverPanel();

			_listView.Columns[0].Width = (_listView.Width - 4);

			foreach (Appraisal appraisal in appraisals)
				_listView.Items.Add(AppraisalMapper.ToListViewItem(appraisal));

			if (_listView.Items.Count > 0)
				_listView.Items[0].Selected = true;
		}

		private void SelectedListViewItemChanged(object sender, EventArgs e)
		{
			_bindingSource.Clear();

			if (_listView.SelectedItems.Count > 0)
				_bindingSource.Add(AppraisalMapper.FromListViewItem(_listView.SelectedItems[0]));
		}

		private void OkButtonClicked(object sender, EventArgs e)
		{
			List<Appraisal> selected = new List<Appraisal>();

			foreach (ListViewItem item in _listView.Items)
				if (item.Checked)
					selected.Add(AppraisalMapper.FromListViewItem(item));

			_presenter.SelectAppraisals(selected.ToArray());
			Close();
		}

		private delegate void CloseDelegate();

		void IAvailableAppraisalsView.CloseView()
		{
			if (InvokeRequired)
				BeginInvoke(new CloseDelegate(Close), new object[] { });
			else
				Close();
		}

		private void ListViewItemChecked(object sender, ItemCheckedEventArgs e)
		{
			UpdateOkButton();
		}

		private void UpdateOkButton()
		{
			_okButton.Enabled = (_listView.CheckedItems.Count > 0);
		}

		private void ShowCoverPanel()
		{
			_coverPanel.Visible = true;
			_coverPanel.BringToFront();
		}

		private void HideCoverPanel()
		{
			_coverPanel.Visible = false;
		}

		private void InitializeProgressBar()
		{
			_progressBar.Style = ProgressBarStyle.Marquee;
			_progressBar.MarqueeAnimationSpeed = 50;
			_progressBar.Minimum = 0;
			_progressBar.Maximum = 100;
			_progressBar.Value = 100;
		}
	}
}
