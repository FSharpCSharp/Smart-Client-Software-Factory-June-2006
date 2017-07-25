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
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Reflection;
using System.Text;
using Microsoft.Practices.RecipeFramework;
using EnvDTE;
using Microsoft.Practices.Common;
using Microsoft.Practices.RecipeFramework.Services;

namespace Microsoft.Practices.GuidanceAutomation.SmartClient.ValueProviders
{
	public class GetGlobalValueProvider : ValueProvider, IAttributesConfigurable
	{
		private string _variableName;

		public override bool OnBeforeActions(object currentValue, out object newValue)
		{
			return OnBeginRecipe(currentValue, out newValue);
		}

		public override bool OnBeginRecipe(object currentValue, out object newValue)
		{
			DTE dte = GetService<DTE>();
			newValue = currentValue;
			if (dte.Solution.Globals.get_VariableExists(_variableName))
			{
				IConfigurationService configService = GetService<IConfigurationService>(true);
				ITypeResolutionService typeLoader = GetService<ITypeResolutionService>(true);
				Type argType = typeLoader.GetType(configService.CurrentRecipe.ArgumentsByName[_variableName].Type);
				ConstructorInfo consInfo =  argType.GetConstructor(new Type[] {typeof (string)});
				if (consInfo != null)
				{
					newValue = consInfo.Invoke(new object[] {dte.Solution.Globals[_variableName]});
				}
				else
				{
					newValue = dte.Solution.Globals[_variableName];
				}
			}
			return currentValue != newValue;
		}

		public void Configure(System.Collections.Specialized.StringDictionary attributes)
		{
			_variableName = attributes["VariableName"];
		}
	}
}
