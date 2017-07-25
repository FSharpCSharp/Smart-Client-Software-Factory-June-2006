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

using System.Collections.Generic;
using GlobalBank.AppraiserWorkbench.AppraisalServiceAgent;
using GlobalBank.AppraiserWorkbench.AppraisalServiceAgent.BusinessEntities;

namespace GlobalBank.AppraiserWorkbench.AppraiserWorkbenchModule.Tests.Mocks
{
	public class MockAppraisalManagementServiceAgent : IAppraisalManagementServiceAgent
	{
		public class MockAppraisal : Appraisal
		{
			public string AssignedTo;
		}

		Dictionary<string, MockAppraisal> mockData = new Dictionary<string, MockAppraisal>();
		public Appraisal ReleasedAppraisal = null;
		public List<Appraisal> lockedAppraisals = new List<Appraisal>();

		public MockAppraisalManagementServiceAgent()
		{
			MockAppraisal a;

			a = new MockAppraisal();
			a.Id = "1";
			a.PropertyAddress.Street1 = "1 Microsoft Way";
			a.Description = "Foo";
			a.AssignedTo = "Somebody";
			mockData[a.Id] = a;
			lockedAppraisals.Add(a);

			a = new MockAppraisal();
			a.Id = "2";
			a.PropertyAddress.Street1 = "2 Microsoft Way";
			a.Description = "Bar";
			a.AssignedTo = "Somebody";
			mockData[a.Id] = a;
			lockedAppraisals.Add(a);

			a = new MockAppraisal();
			a.Id = "3";
			a.PropertyAddress.Street1 = "3 Microsoft Way";
			a.Description = "Baz";
			a.AssignedTo = "Somebody";
			mockData[a.Id] = a;
			lockedAppraisals.Add(a);

			a = new MockAppraisal();
			a.Id = "4";
			a.PropertyAddress.Street1 = "4 Microsoft Way";
			a.Description = "Fup";
			mockData[a.Id] = a;

			a = new MockAppraisal();
			a.Id = "5";
			a.PropertyAddress.Street1 = "5 Microsoft Way";
			a.Description = "Boz";
			mockData[a.Id] = a;
		}

		public bool GetLocalAppraisalsCalled = false;

		public Appraisal[] GetLocalAppraisals()
		{
			GetLocalAppraisalsCalled = true;
			return lockedAppraisals.ToArray();
		}

		public Appraisal[] GetAppraisals(AppraisalFilter filter)
		{
			List<Appraisal> result = new List<Appraisal>();

			foreach (MockAppraisal appraisal in mockData.Values)
			{
				if (filter == AppraisalFilter.MyAppraisals)
				{
					if (appraisal.AssignedTo != null)
						result.Add(appraisal);
				}
				else
				{
					if (appraisal.AssignedTo == null)
						result.Add(appraisal);
				}
			}

			return result.ToArray();
		}

		public void LockAppraisal(Appraisal appraisal)
		{
			mockData[appraisal.Id].AssignedTo = "Me";
			lockedAppraisals.Add(appraisal);
		}

		public void ReleaseAppraisal(Appraisal appraisal,ReleaseAppraisalCallback callback)
		{
			ReleasedAppraisal = appraisal;

			if (callback != null)
				callback(true);
		}

		public void GetAppraisals(AppraisalFilter filter, GetAppraisalsCallback callback)
		{
			callback(true, GetAppraisals(filter));
		}

		public void LockAppraisal(Appraisal appraisal, LockAppraisalCallback callback)
		{
			LockAppraisal(appraisal);

			if (callback != null)
				callback(true, appraisal, true);
		}
	}
}
