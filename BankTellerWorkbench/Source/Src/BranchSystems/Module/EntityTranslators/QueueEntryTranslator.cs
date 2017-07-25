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

using GlobalBank.BranchSystems.ServiceProxies;
using GlobalBank.Infrastructure.Interface.Services;
using Customer=GlobalBank.Infrastructure.Interface.BusinessEntities.Customer;
using Walkin=GlobalBank.Infrastructure.Interface.BusinessEntities.Walkin;

namespace GlobalBank.BranchSystems.Module.EntityTranslators
{
	public class QueueEntryTranslator : EntityMapperTranslator<Infrastructure.Interface.BusinessEntities.QueueEntry, QueueEntry>
	{
		protected override QueueEntry BusinessToService(IEntityTranslatorService service, Infrastructure.Interface.BusinessEntities.QueueEntry value)
		{
			QueueEntry result = new QueueEntry();
			if (value.Person != null)
			{
				if (value.Person is Customer)
					result.CustomerReference = service.Translate<ServiceProxies.Customer>((Customer)value.Person);
				else if (value.Person is Walkin)
					result.WalkinReference = service.Translate<ServiceProxies.Walkin>((Walkin)value.Person);
			}
			result.Description = value.Description;
			result.QueueEntryID = new QueueEntryID();
			result.QueueEntryID.ID = value.QueueEntryID;
			result.ReasonCode = value.ReasonCode;
			result.Status = value.Status;
			result.TimeIn = value.TimeIn;
			result.VisitorName = value.VisitorName;
			return result;
		}

		protected override Infrastructure.Interface.BusinessEntities.QueueEntry ServiceToBusiness(IEntityTranslatorService service, QueueEntry value)
		{
			Infrastructure.Interface.BusinessEntities.QueueEntry result = new Infrastructure.Interface.BusinessEntities.QueueEntry();
			result.Description = value.Description;
			result.QueueEntryID = value.QueueEntryID.ID;
			result.ReasonCode = value.ReasonCode;
			result.Status = value.Status;
			result.TimeIn = value.TimeIn;
			result.VisitorName = value.VisitorName;

			if (value.CustomerReference != null)
				result.Person = service.Translate<Customer>(value.CustomerReference);
			else if (value.WalkinReference != null)
				result.Person = service.Translate<Walkin>(value.WalkinReference);

			return result;
		}
	}

}
