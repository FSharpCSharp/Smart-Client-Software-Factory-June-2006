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

//----------------------------------------------------------------------------------------
// patterns & practices - Smart Client Software Factory - Guidance Package
//
// This file was generated by the "Add View" recipe.
//
// A presenter calls methods of a view to update the information that the view displays. 
// The view exposes its methods through an interface definition, and the presenter contains
// a reference to the view interface. This allows you to test the presenter with different 
// implementations of a view (for example, a mock view).
//
// For more information see:
// ms-help://MS.VSCC.v80/MS.VSIPCC.v80/ms.scsf.2006jun/SCSF/html/03-030-Model%20View%20Presenter%20%20MVP%20.htm
//
// Latest version of this Guidance Package: http://go.microsoft.com/fwlink/?LinkId=62182
//----------------------------------------------------------------------------------------

using System;
using GlobalBank.BranchSystems.Interface.Constants;
using GlobalBank.BranchSystems.Interface.Services;
using GlobalBank.Infrastructure.Interface;
using GlobalBank.Infrastructure.Interface.BusinessEntities;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.EventBroker;
using Microsoft.Practices.ObjectBuilder;

namespace GlobalBank.BranchSystems.Module.Views
{
	public class CustomerSummaryViewPresenter : Presenter<ICustomerSummaryView>
	{
		[EventPublication(EventTopicNames.CreditCardAccountOpen, PublicationScope.Global)]
		public event EventHandler<EventArgs<CreditCard>> CreditCardAccountOpen;

		private QueueEntry _queueEntry = null;
		private ICustomerAccountService _accountService;
		private ICustomerAlertService _alertService;
		private ICustomerQueueService _queueService;

		[InjectionConstructor]
		public CustomerSummaryViewPresenter
			(
			[ComponentDependency("QueueEntry")] QueueEntry queueEntry,
			[ServiceDependency] ICustomerAccountService accountService,
			[ServiceDependency] ICustomerAlertService alertService,
			[ServiceDependency] ICustomerQueueService queueService
			)
		{
			_queueEntry = queueEntry;
			_accountService = accountService;
			_alertService = alertService;
			_queueService = queueService;
		}

		protected Customer Customer
		{
			get { return _queueEntry.Person as Customer; }
		}

		protected QueueEntry QueueEntry
		{
			get { return _queueEntry; }
		}

		protected virtual Account[] GetCustomerAccounts()
		{
			if (Customer != null)
			{
				return _accountService.GetCustomerAccounts(Customer);
			}
			return new Account[0];
		}

		protected virtual CreditCard[] GetCustomerCreditCards()
		{
			if (Customer != null)
			{
				return _accountService.GetCustomerCreditCards(Customer);
			}
			return new CreditCard[0];
		}

		protected virtual Alert[] GetCustomerAlerts()
		{
			if (Customer != null)
			{
				Alert[] result = _alertService.GetCustomerAlerts(Customer);
				return result;
			}
			return new Alert[0];
		}

		public void ServiceComplete()
		{
			_queueService.RemoveFromQueue(_queueEntry);
			CloseView();
			WorkItem.Terminate();
		}

		public void OpenCreditCard(CreditCard creditCard)
		{
			OnCreditCardAccountOpen(creditCard);
		}

		public override void OnViewReady()
		{
			View.UpdateCustomerInfo(QueueEntry.Person);
			View.UpdateQueueInfo(_queueEntry);
			if (Customer != null)
			{
				View.UpdateCustomerAccounts(GetCustomerAccounts());
				View.UpdateCustomerAlerts(GetCustomerAlerts());
				View.UpdateCustomerCreditCards(GetCustomerCreditCards());
			}
		}

		protected virtual void OnCreditCardAccountOpen(CreditCard creditCard)
		{
			if (CreditCardAccountOpen != null)
			{
				CreditCardAccountOpen(this, new EventArgs<CreditCard>(creditCard));
			}
		}

		[EventSubscription(EventTopicNames.CustomerProductsUpdated, ThreadOption.UserInterface)]
		public void OnCustomerProductsUpdated(object sender, EventArgs eventArgs)
		{
			View.UpdateCustomerAccounts(GetCustomerAccounts());
			View.UpdateCustomerCreditCards(GetCustomerCreditCards());
		}
	}
}
