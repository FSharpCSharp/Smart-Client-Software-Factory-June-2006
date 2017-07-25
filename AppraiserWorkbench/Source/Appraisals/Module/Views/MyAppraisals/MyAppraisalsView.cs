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
using Microsoft.Practices.CompositeUI.Utility;

namespace GlobalBank.AppraiserWorkbench.AppraiserWorkbenchModule.Views.MyAppraisals
{
	[SmartPart]
	public sealed partial class MyAppraisalsView : UserControl, IMyAppraisalsView
	{
		private MyAppraisalsViewPresenter _presenter;

		public MyAppraisalsView()
		{
			InitializeComponent();
		}

		[CreateNew]
		public MyAppraisalsViewPresenter Presenter
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

		private delegate void SetAppraisalsDelegate(Appraisal[] appraisals);

		void IMyAppraisalsView.SetAppraisals(Appraisal[] appraisals)
		{
			Guard.ArgumentNotNull(appraisals, "appraisals");

			if (InvokeRequired)
				BeginInvoke(new SetAppraisalsDelegate(InnerSetAppraisals), new object[] { appraisals });
			else
				InnerSetAppraisals(appraisals);
		}

		private void InnerSetAppraisals(Appraisal[] appraisals)
		{
			List<ListViewItem> toRemove = new List<ListViewItem>();
			List<Appraisal> toAdd = new List<Appraisal>(appraisals);

			foreach (ListViewItem item in listView1.Items)
			{
				Appraisal listAppraisal = AppraisalMapper.FromListViewItem(item);
				Appraisal existingAppraisal = Array.Find<Appraisal>(appraisals,
					delegate(Appraisal match) { return match.Id == listAppraisal.Id; });
				if (existingAppraisal == null) toRemove.Add(item);
				else toAdd.Remove(existingAppraisal);
			}

			toRemove.ForEach(delegate(ListViewItem lvi) { listView1.Items.Remove(lvi); });
			toAdd.ForEach(delegate(Appraisal appraisal) { listView1.Items.Add(AppraisalMapper.ToListViewItem(appraisal)); });
		}

		private void listView1_SelectedIndexChanged(object sender, EventArgs e)
		{
			ListView.SelectedListViewItemCollection items = listView1.SelectedItems;
			Appraisal selectedAppraisal = null;

			if (items.Count > 0)
				selectedAppraisal = AppraisalMapper.FromListViewItem(items[0]);

			_presenter.AppraisalSelected(selectedAppraisal);
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			_presenter.OnViewReady();
		}

	}
}
