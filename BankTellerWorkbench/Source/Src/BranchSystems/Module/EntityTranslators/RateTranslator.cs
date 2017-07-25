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
using GlobalBank.BranchSystems.ServiceProxies;
using GlobalBank.Infrastructure.Interface.BusinessEntities;
using GlobalBank.Infrastructure.Interface.Services;

namespace GlobalBank.BranchSystems.Module.EntityTranslators
{
	public class RateTranslator : EntityMapperTranslator<Rate, RateTableEntryType>
	{
		public override bool CanTranslate(Type targetType, Type sourceType)
		{
			return targetType == typeof(Rate) && sourceType == typeof(RateTableEntryType);
		}

		protected override RateTableEntryType BusinessToService(IEntityTranslatorService service, Rate value)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		protected override Rate ServiceToBusiness(IEntityTranslatorService service, RateTableEntryType value)
		{
			Rate result = new Rate();
			result.InterestRate = value.rate;
			result.MinimunAmount = value.minimumAmount;
			result.MaximunAmount = value.maximumAmount;
			result.Start = value.start;
			result.End = value.end;
			return result;
		}
	}
}
