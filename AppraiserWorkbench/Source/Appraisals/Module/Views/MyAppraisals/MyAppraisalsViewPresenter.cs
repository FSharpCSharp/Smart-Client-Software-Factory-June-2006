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
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.EventBroker;
using Microsoft.Practices.ObjectBuilder;
using GlobalBank.AppraiserWorkbench.AppraisalServiceAgent;
using GlobalBank.AppraiserWorkbench.AppraisalServiceAgent.BusinessEntities;
using GlobalBank.AppraiserWorkbench.AppraiserWorkbenchModule.Constants;
using GlobalBank.Infrastructure.Interface;

namespace GlobalBank.AppraiserWorkbench.AppraiserWorkbenchModule.Views.MyAppraisals
{
	public class MyAppraisalsViewPresenter: Presenter<IMyAppraisalsView>
	{
		private IAppraisalManagementServiceAgent _appraisalService;

		[InjectionConstructor]
		public MyAppraisalsViewPresenter
			(
			[ServiceDependency] IAppraisalManagementServiceAgent appraisalService
			)
		{
			_appraisalService = appraisalService;
		}

		[EventPublication(EventTopicNames.WorkingAppraisalSelected, PublicationScope.Global)]
		public event EventHandler<EventArgs<Appraisal>> WorkingAppraisalSelected;

		[EventPublication(EventTopicNames.StatusUpdate, PublicationScope.Global)]
		public event EventHandler<EventArgs<string>> StatusUpdate;

		public override void OnViewReady()
		{
			LoadAppraisals();
		}

		public void AppraisalSelected(Appraisal appraisal)
		{
			if (WorkingAppraisalSelected != null)
				WorkingAppraisalSelected(this, new EventArgs<Appraisal>(appraisal));
		}

		[EventSubscription(EventTopicNames.MyAppraisalsUpdated, ThreadOption.UserInterface)]
		public void MyAppraisalsUpdatedHandler(object sender, EventArgs e)
		{
			LoadAppraisals();
		}

		protected void OnStatusUpdate(EventArgs<string> args)
		{
			if (StatusUpdate != null)
				StatusUpdate(this, args);
		}

		private void LoadAppraisals()
		{
			View.SetAppraisals(_appraisalService.GetLocalAppraisals());
		}

	}
}
