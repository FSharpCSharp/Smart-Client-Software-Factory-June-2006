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

using GlobalBank.Infrastructure.Interface.BusinessEntities;
using GlobalBank.Infrastructure.Interface.Services;
using Address=GlobalBank.BranchSystems.ServiceProxies.Address;
using EmailAddress=GlobalBank.BranchSystems.ServiceProxies.EmailAddress;
using PhoneNumber=GlobalBank.BranchSystems.ServiceProxies.PhoneNumber;

namespace GlobalBank.BranchSystems.Module.EntityTranslators
{
	public class WalkinTranslator : EntityMapperTranslator<Walkin, ServiceProxies.Walkin>
	{
		protected override ServiceProxies.Walkin BusinessToService(IEntityTranslatorService service, Walkin value)
		{
			ServiceProxies.Walkin result = new ServiceProxies.Walkin();
			result.Addresses = service.Translate<Address[]>(value.Addresses);
			result.CustomerLevel = value.CustomerLevel;
			result.EmailAddresses = service.Translate<EmailAddress[]>(value.EmailAddresses);
			result.FirstName = value.FirstName;
			result.LastName = value.LastName;
			result.MiddleInitial = value.MiddleInitial;
			result.MotherMaidenName = value.MotherMaidenName;
			result.PhoneNumbers = service.Translate<PhoneNumber[]>(value.PhoneNumbers);
			result.SSNumber = value.SocialSecurityNumber;
			return result;
		}

		protected override Walkin ServiceToBusiness(IEntityTranslatorService service, ServiceProxies.Walkin value)
		{
			Walkin result = new Walkin();
			result.Addresses = service.Translate<Infrastructure.Interface.BusinessEntities.Address[]>(value.Addresses);
			result.CustomerLevel = value.CustomerLevel;
			result.EmailAddresses = service.Translate<Infrastructure.Interface.BusinessEntities.EmailAddress[]>(value.EmailAddresses);
			result.FirstName = value.FirstName;
			result.LastName = value.LastName;
			result.MiddleInitial = value.MiddleInitial;
			result.MotherMaidenName = value.MotherMaidenName;
			result.PhoneNumbers = service.Translate<Infrastructure.Interface.BusinessEntities.PhoneNumber[]>(value.PhoneNumbers);
			result.SocialSecurityNumber = value.SSNumber;
			return result;
		}
	}

}
