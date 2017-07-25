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
using Microsoft.Practices.CompositeUI.EventBroker;
using GlobalBank.AppraiserWorkbench.AppraisalServiceAgent;
using GlobalBank.AppraiserWorkbench.AppraisalServiceAgent.BusinessEntities;
using GlobalBank.AppraiserWorkbench.AppraiserWorkbenchModule.Constants;
using GlobalBank.AppraiserWorkbench.AppraiserWorkbenchModule.Tests.Mocks;
using GlobalBank.AppraiserWorkbench.AppraiserWorkbenchModule.Views.AppraisalDetail;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GlobalBank.Infrastructure.Interface.Services;
using GlobalBank.Infrastructure.Interface;
using UnitTest.Library;

namespace GlobalBank.AppraiserWorkbench.AppraiserWorkbenchModule.Tests.Views.AppraisalDetail
{
	[TestClass]
	public class AppraisalDetailViewPresenterFixture
	{
		[TestMethod]
		public void ShowAppraisalPassesAppraisalToView()
		{
			TestableRootWorkItem wi = new TestableRootWorkItem();
			wi.Services.AddNew<MockAppraisalManagementServiceAgent, IAppraisalManagementServiceAgent>();
			wi.Services.AddNew<MockMessageBoxService, IMessageBoxService>();

			AppraisalDetailViewPresenter presenter = wi.Items.AddNew<AppraisalDetailViewPresenter>();
			MockView view = new MockView();
			presenter.View = view;

			Appraisal originalAppraisal = new Appraisal();
			presenter.ShowAppraisal(originalAppraisal);

			Assert.AreEqual(originalAppraisal, view.Appraisal);
		}

		[TestMethod]
		public void ShowAppraisalCalledInResponseToEventBrokerEvent()
		{
			TestableRootWorkItem wi = new TestableRootWorkItem();
			wi.Services.AddNew<MockAppraisalManagementServiceAgent, IAppraisalManagementServiceAgent>();
			wi.Services.AddNew<MockMessageBoxService, IMessageBoxService>();

			AppraisalDetailViewPresenter presenter = wi.Items.AddNew<AppraisalDetailViewPresenter>();
			MockView view = new MockView();
			presenter.View = view;

			wi.EventTopics[EventTopicNames.WorkingAppraisalSelected].Fire(this, new EventArgs<Appraisal>(new Appraisal()), wi, PublicationScope.Global);

			Assert.IsTrue(view.ShowAppraisalWasCalled);
		}

		[TestMethod]
		public void SubmittingAppraisalCausesPresenterToTellServiceAgentToReleaseAppraisal()
		{
			TestableRootWorkItem wi = new TestableRootWorkItem();
			MockAppraisalManagementServiceAgent mockService = wi.Services.AddNew<MockAppraisalManagementServiceAgent, IAppraisalManagementServiceAgent>();
			wi.Services.AddNew<MockMessageBoxService, IMessageBoxService>();

			AppraisalDetailViewPresenter presenter = wi.Items.AddNew<AppraisalDetailViewPresenter>();
			Appraisal appraisal = new Appraisal();

			presenter.SubmitAppraisal(appraisal);

			Assert.AreEqual(appraisal, mockService.ReleasedAppraisal);
		}

		[TestMethod]
		public void PresenterUpdatesViewWhenStatusChangeOnAttachment()
		{
			TestableRootWorkItem wi = new TestableRootWorkItem();
			MockAppraisalManagementServiceAgent mockService = wi.Services.AddNew<MockAppraisalManagementServiceAgent, IAppraisalManagementServiceAgent>();
			wi.Services.AddNew<MockMessageBoxService, IMessageBoxService>();

			AppraisalDetailViewPresenter presenter = wi.Items.AddNew<AppraisalDetailViewPresenter>();
			MockView view = new MockView();
			presenter.View = view;

			Appraisal appraisal = new Appraisal();
			Attachment attachment = new Attachment();
			appraisal.Attachments.Add(attachment);

			presenter.ShowAppraisal(appraisal);

			attachment.Status = Attachment.AttachmentStatus.Uploaded;

			Assert.AreSame(attachment, view.UpdatedAttachment);

		}

		public class MockView : IAppraisalDetailView
		{
			public Appraisal Appraisal;
			public bool ShowAppraisalWasCalled = false;
			public Attachment UpdatedAttachment = null;

			public void ShowAppraisal(Appraisal appraisal)
			{
				ShowAppraisalWasCalled = true;
				Appraisal = appraisal;
			}

			public void ResetViewToEmpty()
			{
				throw new Exception("The method or operation is not implemented.");
			}

			public void UpdateAttachmentStatus(Attachment attachment)
			{
				UpdatedAttachment = attachment;
			}
		}
	}
}
