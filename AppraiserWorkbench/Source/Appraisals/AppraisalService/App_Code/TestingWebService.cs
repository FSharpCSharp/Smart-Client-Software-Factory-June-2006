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
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;


/// <summary>
/// Summary description for TestingWebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class TestingWebService : System.Web.Services.WebService
{

	public TestingWebService()
	{
	}

	[WebMethod]
	public void ResetToOriginalBackingStore()
	{
		AppraisalManagementService.ResetBackingStore();
	}

	[WebMethod]
	public void AppendAppraisalsToBackingStore(Appraisal[] appraisals)
	{
		BackingStore back = AppraisalManagementService.LoadBackingStore();
		foreach (Appraisal appraisal in appraisals)
		{
			back.Appraisals.Add(appraisal);
		}
		AppraisalManagementService.SaveBackingStore(back);
	}

	[WebMethod]
	public void RemoveAppraisalsFromBackingStore()
	{
		BackingStore back = AppraisalManagementService.LoadBackingStore();
		back.Appraisals.Clear();
		AppraisalManagementService.SaveBackingStore(back);
	}
}
