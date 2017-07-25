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
	public class QuoteTranslator : EntityMapperTranslator<Quote, QuoteResponseType>
	{
		public override bool CanTranslate(Type targetType, Type sourceType)
		{
			return targetType == typeof(Quote) && sourceType == typeof(QuoteResponseType);
		}

		protected override QuoteResponseType BusinessToService(IEntityTranslatorService service, Quote value)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		protected override Quote ServiceToBusiness(IEntityTranslatorService service, QuoteResponseType value)
		{
			Quote quote = new Quote();
			quote.Amount = value.amount;
			quote.Duration = value.durationInMonths;
			quote.Rate = value.rate;
			return quote;
		}
	}
}
