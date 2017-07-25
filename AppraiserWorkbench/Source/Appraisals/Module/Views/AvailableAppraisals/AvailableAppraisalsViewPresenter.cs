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
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.EventBroker;
using Microsoft.Practices.ObjectBuilder;
using GlobalBank.AppraiserWorkbench.AppraisalServiceAgent;
using GlobalBank.AppraiserWorkbench.AppraisalServiceAgent.BusinessEntities;
using GlobalBank.AppraiserWorkbench.AppraiserWorkbenchModule.Constants;
using GlobalBank.Infrastructure.Interface.Services;
using GlobalBank.Infrastructure.Interface;
using Microsoft.Practices.CompositeUI.Utility;

namespace GlobalBank.AppraiserWorkbench.AppraiserWorkbenchModule.Views.AvailableAppraisals
{
	public class AvailableAppraisalsViewPresenter : Presenter<IAvailableAppraisalsView>
	{
		private IAppraisalManagementServiceAgent _appraisalService;
		private IMessageBoxService _messageBoxSvc;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:AvailableAppraisalsPresenter"/> class.
		/// </summary>
		/// <param name="svc">The SVC.</param>
		[InjectionConstructor]
		public AvailableAppraisalsViewPresenter
			(
				[ServiceDependency] IAppraisalManagementServiceAgent appraisalService,
				[ServiceDependency] IMessageBoxService messageBoxService
			)
		{
			_appraisalService = appraisalService;
			_messageBoxSvc = messageBoxService;
		}

		[EventPublication(EventTopicNames.StatusUpdate, PublicationScope.Global)]
		public event EventHandler<EventArgs<string>> StatusUpdate;

		/// <summary>
		/// Shows the available appraisals.
		/// </summary>
		public void ShowAvailableAppraisals()
		{
			OnStatusUpdate(String.Empty);

			_appraisalService.GetAppraisals(AppraisalFilter.Unassigned, delegate(bool success, Appraisal[] appraisals)
			{
				if (success)
				{
					View.SetAppraisals(appraisals);
				}
				else
				{
					OnStatusUpdate(Properties.Resources.CannotGetListUnAppraisals);
					_messageBoxSvc.Show(Properties.Resources.CannotGetListUnAppraisals);
					View.CloseView();
				}
			});
		}

		/// <summary>
		/// Selects the appraisals.
		/// </summary>
		public void SelectAppraisals(Appraisal[] appraisals)
		{
			Guard.ArgumentNotNull(appraisals, "appraisals");

			List<Appraisal> rejectedAppraisals = new List<Appraisal>();
			int processedCount = 0;
			bool failureNotified = false;
			foreach (Appraisal appraisal in appraisals)
			{
				_appraisalService.LockAppraisal(appraisal, delegate(bool success, Appraisal requested, bool locked)
				{
					if (success)
					{
						if (locked == false)
							rejectedAppraisals.Add(requested);
						if (++processedCount == appraisals.Length && rejectedAppraisals.Count > 0)
							ShowRejectedAppraisals(rejectedAppraisals.ToArray());
					}
					if (success == false && failureNotified == false)
					{
						failureNotified = true;
						_messageBoxSvc.Show(Properties.Resources.ServiceWentDownWhenSelectingAppraisals, Properties.Resources.ServiceRequestErrorTitle);
					}
				});
			}
		}

		private void ShowRejectedAppraisals(Appraisal[] appraisals)
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendLine(Properties.Resources.DeniedAppraisalsTitle);
			foreach (Appraisal rejected in appraisals)
			{
				sb.AppendFormat(Properties.Resources.DeniedAppraisalsLine, rejected.Id, rejected.Description);
				sb.AppendLine();
			}
			_messageBoxSvc.Show(sb.ToString(), Properties.Resources.ServiceResponse);
		}

		protected virtual void OnStatusUpdate(string text)
		{
			if (StatusUpdate != null)
				StatusUpdate(this, new EventArgs<string>(text));
		}
	}
}
