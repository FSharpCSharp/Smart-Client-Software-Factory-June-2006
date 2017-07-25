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
using GlobalBank.Infrastructure.Interface;
using GlobalBank.AppraiserWorkbench.WebServiceProxies.AppraisalService;

namespace GlobalBank.AppraiserWorkbench.WebServiceProxies.AppraisalService
{
	public class AppraisalManagementServiceProxy : ServiceProxy<IAppraisalManagementService>, IAppraisalManagementServiceProxy
	{
		public AppraisalManagementServiceProxy()
			: this(new AppraisalManagementService())
		{
		}

		public AppraisalManagementServiceProxy(IAppraisalManagementService service)
			: base(service)
		{
		}

		public void GetAppraisals(int timeout, string userId, AppraisalFilter filter, Commands.GetAppraisalsCommand.CallbackType callback)
		{
			Queue.Add(new WebServiceProxies.AppraisalService.Commands.GetAppraisalsCommand(Service, timeout, userId, filter, callback));
		}

		public void LockAppraisal(int timeout, string userId, string appraisalId, Commands.LockAppraisalCommand.CallbackType callback)
		{
			Queue.Add(new WebServiceProxies.AppraisalService.Commands.LockAppraisalCommand(Service, timeout, userId, appraisalId, callback));
		}

		public void ReleaseAppraisal(int timeout, string userId, string appraisalId, Commands.ReleaseAppraisalCommand.CallbackType callback)
		{
			Queue.Add(new WebServiceProxies.AppraisalService.Commands.ReleaseAppraisalCommand(Service, timeout, userId, appraisalId, callback));
		}
	}
}
