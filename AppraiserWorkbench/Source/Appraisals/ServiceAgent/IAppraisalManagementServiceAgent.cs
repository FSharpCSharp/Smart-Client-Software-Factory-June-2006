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

using GlobalBank.AppraiserWorkbench.AppraisalServiceAgent.BusinessEntities;

namespace GlobalBank.AppraiserWorkbench.AppraisalServiceAgent
{
	public delegate void GetAppraisalsCallback(bool success, Appraisal[] appraisals);
	public delegate void LockAppraisalCallback(bool success, Appraisal appraisal, bool locked);
	public delegate void ReleaseAppraisalCallback(bool success);

	public interface IAppraisalManagementServiceAgent
	{
		Appraisal[] GetLocalAppraisals();
		void GetAppraisals(AppraisalFilter filter, GetAppraisalsCallback callback);
		void LockAppraisal(Appraisal appraisal, LockAppraisalCallback callback);
		void ReleaseAppraisal(Appraisal appraisal, ReleaseAppraisalCallback callback);
	}
}
