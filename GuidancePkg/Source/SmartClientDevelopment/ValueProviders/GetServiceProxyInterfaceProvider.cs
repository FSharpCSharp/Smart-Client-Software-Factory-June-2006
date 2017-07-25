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

#region Using Directives

using System;
using System.Text;
using Microsoft.Practices.RecipeFramework;
using Microsoft.Practices.Common;
using Microsoft.Practices.RecipeFramework.Services;
using EnvDTE;
using System.ComponentModel.Design;
using Microsoft.Practices.ComponentModel;
using EnvDTE80;

#endregion

namespace Microsoft.SmartClient.CBA.ServiceAgentPackage.ValueProviders
{
	[ServiceDependency(typeof(IDictionaryService))]
	public class GetServiceProxyInterfaceProvider : ValueProvider
	{
		public override bool OnBeginRecipe(object currentValue, out object newValue)
		{
			if (currentValue == null)
			{
				try
				{
					newValue = null;
					IDictionaryService dictService =
						(IDictionaryService)GetService(typeof(IDictionaryService));
					CodeClass saClass = (CodeClass)dictService.GetValue("ServiceProxyClass");
					if (saClass != null && saClass.ImplementedInterfaces.Count > 0)
					{
						newValue = (CodeInterface)saClass.ImplementedInterfaces.Item(1);
					}
				}
				catch
				{
					newValue = null;
				}
				if (newValue != null)
				{
					return true;
				}
			}
			newValue = currentValue;
			return false;
		}

		public override bool OnArgumentChanged(string changedArgumentName, object changedArgumentValue, object currentValue, out object newValue)
		{
			if (changedArgumentName == "ServiceProxyClass")
			{
				return OnBeginRecipe(currentValue, out newValue);
			}
			return base.OnArgumentChanged(changedArgumentName, changedArgumentValue, currentValue, out newValue);
		}

		public override bool OnBeforeActions(object currentValue, out object newValue)
		{
			return OnBeginRecipe(currentValue, out newValue);
		}
	}
}
