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
using GlobalBank.AppraiserWorkbench.AppraisalServiceAgent;
using GlobalBank.AppraiserWorkbench.AppraisalServiceAgent.BusinessEntities;
using GlobalBank.AppraiserWorkbench.AppraiserWorkbenchModule.Constants;
using GlobalBank.AppraiserWorkbench.AppraiserWorkbenchModule.Tests.Mocks;
using GlobalBank.AppraiserWorkbench.AppraiserWorkbenchModule.Views.MyAppraisals;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GlobalBank.Infrastructure.Interface;
using UnitTest.Library;

namespace GlobalBank.AppraiserWorkbench.AppraiserWorkbenchModule.Tests.Views.MyAppraisals
{
	[TestClass]
	public class MyAppraisalsViewPresenterFixture
	{
		[TestMethod]
		public void PresenterPassesBusinessEntitiesFromServiceToView()
		{
			MyAppraisalsViewPresenter presenter = new MyAppraisalsViewPresenter(new MockAppraisalManagementServiceAgent());
			MockView view = new MockView();
			presenter.View = view;

			presenter.OnViewReady();

			Assert.AreEqual(3, view.Items.Count);
			Assert.AreEqual("Foo", view.Items[0].Description);
			Assert.AreEqual("Bar", view.Items[1].Description);
			Assert.AreEqual("Baz", view.Items[2].Description);
		}

		[TestMethod]
		public void LoadAppraisalsGetTheLocalAppraisalsAndCallsServiceAgentForAvailableAppraisals()
		{
			MockAppraisalManagementServiceAgent mockSA = new MockAppraisalManagementServiceAgent();
			MyAppraisalsViewPresenter presenter = new MyAppraisalsViewPresenter(mockSA);
			MockView view = new MockView();
			presenter.View = view;

			presenter.OnViewReady();

			Assert.IsTrue(mockSA.GetLocalAppraisalsCalled);
		}

		[TestMethod]
		public void SelectingAppraisalReturnsSelectedIdToEvent()
		{
			MockAppraisalManagementServiceAgent service = new MockAppraisalManagementServiceAgent();
			MyAppraisalsViewPresenter presenter = new MyAppraisalsViewPresenter(service);

			Appraisal selectedAppraisal = null;
			presenter.WorkingAppraisalSelected += delegate(object sender, EventArgs<Appraisal> e) { selectedAppraisal = e.Data; };

			Appraisal originalAppraisal = new Appraisal();
			presenter.AppraisalSelected(originalAppraisal);

			Assert.AreEqual(originalAppraisal, selectedAppraisal);
		}

		[TestMethod]
		public void SelectingAppraisalFiresEventBrokerEvent()
		{
			TestableRootWorkItem wi = new TestableRootWorkItem();
			wi.Services.AddNew<MockAppraisalManagementServiceAgent, IAppraisalManagementServiceAgent>();

			MyAppraisalsViewPresenter presenter = wi.Items.AddNew<MyAppraisalsViewPresenter>();
			MockSubscriber subscriber = wi.Items.AddNew<MockSubscriber>();

			Appraisal originalAppraisal = new Appraisal();
			presenter.AppraisalSelected(originalAppraisal);

			Assert.AreEqual(originalAppraisal, subscriber.Appraisal);
		}

		[TestMethod]
		public void PresenterRefreshesViewWhenEventBrokerFired()
		{
			TestableRootWorkItem wi = new TestableRootWorkItem();
			wi.Services.AddNew<MockAppraisalManagementServiceAgent, IAppraisalManagementServiceAgent>();
			MyAppraisalsViewPresenter presenter = wi.Items.AddNew<MyAppraisalsViewPresenter>();
			MockView view = new MockView();
			presenter.View = view;

			wi.EventTopics[EventTopicNames.MyAppraisalsUpdated].Fire(this, EventArgs.Empty, wi, PublicationScope.Global);

			Assert.IsTrue(view.Items.Count > 0);
		}

		class MockSubscriber
		{
			public Appraisal Appraisal;

			[EventSubscription(EventTopicNames.WorkingAppraisalSelected)]
			public void TheEventHandler(object sender, EventArgs<Appraisal> e)
			{
				Appraisal = e.Data;
			}
		}

		class MockView : IMyAppraisalsView
		{
			public List<Appraisal> Items = new List<Appraisal>();

			public void SetAppraisals(Appraisal[] appraisals)
			{
				Items.AddRange(appraisals);
			}
		}
	}
}
