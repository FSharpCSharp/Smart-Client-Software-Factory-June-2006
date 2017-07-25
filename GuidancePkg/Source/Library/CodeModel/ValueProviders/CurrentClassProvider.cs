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

using EnvDTE;
using Microsoft.Practices.ComponentModel;
using Microsoft.Practices.RecipeFramework;

namespace Microsoft.Practices.GuidanceAutomation.SmartClient.Library.CodeModel.ValueProviders
{
	/// <summary>
	/// Provides the first defined class in the current selected project item in the solution explorer
	/// </summary>
	[ServiceDependency(typeof(DTE))]
	public class CurrentClassProvider : ValueProvider
	{
		private CodeClass GetCurrentClass(ProjectItem prItem)
		{
			foreach (CodeElement codeElement in prItem.FileCodeModel.CodeElements)
			{
				if (codeElement is CodeNamespace)
				{
					CodeNamespace codeNamepace = (CodeNamespace)codeElement;
					if (codeNamepace.Members.Count > 0 && codeNamepace.Members.Item(1) is CodeClass)
					{
						return (CodeClass)codeNamepace.Members.Item(1);
					}
				}
			}
			return null;
		}

		/// <summary>
		/// <seealso cref="ValueProvider.OnBeginRecipe"/>
		/// </summary>
		/// <param name="currentValue"></param>
		/// <param name="newValue"></param>
		/// <returns></returns>
		public override bool OnBeginRecipe(object currentValue, out object newValue)
		{
			if (currentValue == null)
			{
				DTE dte = (DTE)GetService(typeof(DTE));
				if (dte.SelectedItems.Count == 1)
				{
					ProjectItem prItem = dte.SelectedItems.Item(1).ProjectItem;
					if (prItem != null)
					{
						newValue = GetCurrentClass(prItem);
						if (newValue != null)
						{
							return true;
						}
					}
				}
			}
			newValue = currentValue;
			return false;
		}
	}
}
