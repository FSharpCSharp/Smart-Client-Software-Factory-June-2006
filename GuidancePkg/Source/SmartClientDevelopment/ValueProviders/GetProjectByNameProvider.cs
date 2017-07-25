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
using System.Text;
using Microsoft.Practices.RecipeFramework;
using Microsoft.Practices.Common;
using Microsoft.Practices.GuidanceAutomation.SmartClient.Library;
using EnvDTE;

namespace Microsoft.Practices.GuidanceAutomation.SmartClient.ValueProviders
{
	public class GetProjectByNameProvider : ValueProvider, IAttributesConfigurable
	{
		private string _projectName;

		public override bool OnBeginRecipe(object currentValue, out object newValue)
		{
			newValue = currentValue;
			DTE dte = GetService<DTE>();
			newValue = DteHelper.FindProjectByName(dte, _projectName);
			return newValue != currentValue;
		}

		public override bool OnBeforeActions(object currentValue, out object newValue)
		{
			return OnBeginRecipe(currentValue, out newValue);
		}

		public void Configure(System.Collections.Specialized.StringDictionary attributes)
		{
			_projectName = attributes["ProjectName"];
		}
	}
}
