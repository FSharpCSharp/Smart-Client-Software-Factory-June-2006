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

using GlobalBank.Infrastructure.Interface;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.ObjectBuilder;

namespace GlobalBank.CreditCardAccounts.Module
{
	public class Module : ModuleInit
	{
		private WorkItem _rootWorkItem;

		[InjectionConstructor]
		public Module([ServiceDependency] WorkItem rootWorkItem)
		{
			_rootWorkItem = rootWorkItem;
		}

		public override void Load()
		{
			base.Load();

			ControlledWorkItem<ModuleController> workItem =
				_rootWorkItem.WorkItems.AddNew<ControlledWorkItem<ModuleController>>();
			workItem.Controller.Run();
		}
	}
}