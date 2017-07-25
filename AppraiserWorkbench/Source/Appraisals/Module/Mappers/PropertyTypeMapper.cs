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

namespace GlobalBank.AppraiserWorkbench.AppraiserWorkbenchModule.Mappers
{
	public static class PropertyTypeMapper
	{
		public static string ToDisplayString(PropertyType propertyType)
		{
			switch (propertyType)
			{
				case PropertyType.CommercialIndustrial:
					return Properties.Resources.CommercialIndustrial;

				case PropertyType.CommercialLand:
					return Properties.Resources.CommercialLand;

				case PropertyType.ResidentialCondoOrTownhouse:
					return Properties.Resources.ResidentialCondoOrTownhouse;

				case PropertyType.ResidentialLand:
					return Properties.Resources.ResidentialLand;

				case PropertyType.ResidentialMobileHome:
					return Properties.Resources.ResidentialMobileHome;

				case PropertyType.ResidentialMultiFamily:
					return Properties.Resources.ResidentialMultiFamily;

				case PropertyType.ResidentialSingleFamily:
					return Properties.Resources.ResidentialSingleFamily;
			}

			return propertyType.ToString();
		}
	}
}
