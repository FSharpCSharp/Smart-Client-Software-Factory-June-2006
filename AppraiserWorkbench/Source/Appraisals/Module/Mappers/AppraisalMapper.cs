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
using Microsoft.Practices.CompositeUI.Utility;

namespace GlobalBank.AppraiserWorkbench.AppraiserWorkbenchModule.Mappers
{
	public static class AppraisalMapper
	{
		public static ListViewItem ToListViewItem(Appraisal appraisal)
		{
			Guard.ArgumentNotNull(appraisal, "appraisal");
			return new AppraisalListViewItem(appraisal);
		}

		public static Appraisal FromListViewItem(ListViewItem listViewItem)
		{
			Guard.ArgumentNotNull(listViewItem, "listViewItem");

			AppraisalListViewItem appraisalListViewItem = listViewItem as AppraisalListViewItem;

			if (appraisalListViewItem == null)
				throw new ArgumentException(String.Format(CultureInfo.CurrentCulture,
					Properties.Resources.WrongTypeArgument,
					typeof(AppraisalListViewItem).Name),
					"listViewItem");

			return appraisalListViewItem.Appraisal;
		}

		private class AppraisalListViewItem : ListViewItem
		{
			private Appraisal _appraisal;

			public AppraisalListViewItem(Appraisal appraisal)
			{
				_appraisal = appraisal;
				Text = String.Format("{0} - {1}", appraisal.Id, appraisal.PropertyAddress.Street1);
				Name = appraisal.Id;
			}

			public Appraisal Appraisal
			{
				get { return _appraisal; }
			}
		}
	}
}
