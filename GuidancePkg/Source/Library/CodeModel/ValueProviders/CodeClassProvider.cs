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
using Microsoft.Practices.Common;
using Microsoft.Practices.RecipeFramework;

namespace Microsoft.Practices.GuidanceAutomation.SmartClient.Library.CodeModel.ValueProviders
{
	/// <summary>
	/// Obtains the <see cref="CodeClass"/> for the paramter specified in "ClassName"
	/// </summary>
	public sealed class CodeClassProvider : ValueProvider, IAttributesConfigurable
	{
		private string _className;

		#region Overrides

		/// <summary>
		/// Obtains the <see cref="CodeClass"/> for the specified paramter "ClassName"
		/// </summary>
		/// <param name="currentValue"></param>
		/// <param name="newValue"></param>
		/// <returns></returns>
		/// <seealso cref="ValueProvider.OnBeginRecipe"/>
		public override bool OnBeginRecipe(object currentValue, out object newValue)
		{
			if (currentValue == null)
			{
				DTE dte = (DTE)GetService(typeof(DTE));
				try
				{
					newValue = CodeModelUtil.GetCodeModel(dte).CodeTypeFromFullName(this._className);
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

		/// <param name="currentValue"></param>
		/// <param name="newValue"></param>
		/// <returns></returns>
		/// <seealso cref="ValueProvider.OnBeforeActions"/>
		/// <seealso cref="OnBeginRecipe"/>
		public override bool OnBeforeActions(object currentValue, out object newValue)
		{
			return OnBeginRecipe(currentValue, out newValue);
		}

		#endregion

		#region IAttributesConfigurable Members

		void IAttributesConfigurable.Configure(System.Collections.Specialized.StringDictionary attributes)
		{
			_className = attributes["ClassName"];
		}

		#endregion
	}
}
