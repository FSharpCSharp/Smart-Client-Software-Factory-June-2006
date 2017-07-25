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
using GlobalBank.AppraiserWorkbench.AppraisalServiceAgent.BusinessEntities;
using Microsoft.Practices.CompositeUI.Utility;

namespace GlobalBank.AppraiserWorkbench.AppraiserWorkbenchModule.Mappers
{
	public static class AddressMapper
	{
		public static string ToFormattedString(Address address)
		{
			Guard.ArgumentNotNull(address, "address");
			return String.Format(@"{0}
{1}, {2} {3}", address.Street1, address.City, address.State, address.Zip);
		}
	}
}
