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
	public class CreditCardTranslator : EntityMapperTranslator<CreditCard, creditCardType>
	{
		public override bool CanTranslate(Type targetType, Type sourceType)
		{
			return targetType == typeof(CreditCard) && sourceType == typeof(creditCardType);
		}

		protected override creditCardType BusinessToService(IEntityTranslatorService service, CreditCard value)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		protected override CreditCard ServiceToBusiness(IEntityTranslatorService service, creditCardType value)
		{
			CreditCard result = new CreditCard();
			result.AvailableBalance = (float)value.availableBalance;
			result.CardCreditLimit = (float)value.cardCreditLimit;
			result.CreditCardNumber = value.accountNumber;
			result.CreditCardTypeId = value.accountType.id;
			result.CreditCardTypeName = value.accountType.type;
			result.CustomerId = value.customerId;
			result.DateOpened = value.dateOpened;
			result.Fees = value.accountType.fees;
			result.InterestRate = value.accountType.interestRate;
			result.LastPaymentDate = value.lastPaymentDue;
			result.MaxCreditLimit = (float)value.accountType.maxCreditLimit;
			result.PaymentDue = (float)value.paymentDue;
			return result;
		}
	}

}
