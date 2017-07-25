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
using Microsoft.Practices.CompositeUI.EventBroker;
using GlobalBank.AppraiserWorkbench.AppraisalServiceAgent.BusinessEntities;
using GlobalBank.AppraiserWorkbench.AppraiserWorkbenchModule.Constants;
using GlobalBank.AppraiserWorkbench.AppraiserWorkbenchModule.Tests.Mocks;
using GlobalBank.AppraiserWorkbench.AppraiserWorkbenchModule.Views.AvailableAppraisals;
using NUnit.Framework;

namespace GlobalBank.AppraiserWorkbench.AppraiserWorkbenchModule.Tests.Views.AvailableAppraisals
{
	[TestFixture]
	public class AvailableAppraisalsViewPresenterFixture
	{
		[Test]
		public void ShowAvailableAppraisalsPassesAppraisalsToView()
		{
			MockAppraisalManagementServiceAgent svc = new MockAppraisalManagementServiceAgent();
			MockMessageBoxService msg = new MockMessageBoxService();
			MockView view = new MockView();
			AvailableAppraisalsViewPresenter presenter = new AvailableAppraisalsViewPresenter(svc, msg);
			presenter.View = view;

			presenter.ShowAvailableAppraisals();

			Assert.AreEqual(2, view.Appraisals.Count);
		}

		[Test]
		public void ViewTellsPresenterAboutSelectedAppraisals()
		{
			MockAppraisalManagementServiceAgent svc = new MockAppraisalManagementServiceAgent();
			MockMessageBoxService msg = new MockMessageBoxService();
			AvailableAppraisalsViewPresenter presenter = new AvailableAppraisalsViewPresenter(svc, msg);
			List<Appraisal> preSelect = new List<Appraisal>();
			preSelect.AddRange(svc.GetAppraisals(AppraisalFilter.MyAppraisals));
			Appraisal[] available = svc.GetAppraisals(AppraisalFilter.Unassigned);

			presenter.SelectAppraisals(new Appraisal[] { available[0] });

			List<Appraisal> postSelect = new List<Appraisal>();
			postSelect.AddRange(svc.GetAppraisals(AppraisalFilter.MyAppraisals));
			Assert.IsFalse(preSelect.Contains(available[0]));
			Assert.IsTrue(postSelect.Contains(available[0]));
		}

		private class EventListener
		{
			public bool MyAppraisalsUpdatedFired = false;

			[EventSubscription(EventTopicNames.MyAppraisalsUpdated)]
			public void MyAppraisalsUpdatedHandler(object sender, EventArgs e)
			{
				MyAppraisalsUpdatedFired = true;
			}
		}

		public class MockView : IAvailableAppraisalsView
		{
			public List<Appraisal> Appraisals = new List<Appraisal>();

			public void SetAppraisals(Appraisal[] appraisals)
			{
				Appraisals.AddRange(appraisals);
			}

			public void ShowRejectedAppraisals(Appraisal[] appraisals)
			{
			}

			public void CloseView()
			{
			}
		}
	}
}
