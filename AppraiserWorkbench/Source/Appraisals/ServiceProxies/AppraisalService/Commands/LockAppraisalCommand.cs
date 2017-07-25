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
using GlobalBank.Infrastructure.Interface.Commands;

namespace GlobalBank.AppraiserWorkbench.WebServiceProxies.AppraisalService.Commands
{
	public class LockAppraisalCommand : CommandWithCallback<IAppraisalManagementService, bool>
	{
		string _userId;
		string _appraisalId;

		public LockAppraisalCommand(IAppraisalManagementService service, int timeout, string userId,
			string appraisalId, CallbackType callback)
			: base(service, timeout, callback)
		{
			_userId = userId;
			_appraisalId = appraisalId;
		}

		protected override bool DoCallService()
		{
			return Service.LockAppraisal(_userId, _appraisalId);
		}
	}
}
