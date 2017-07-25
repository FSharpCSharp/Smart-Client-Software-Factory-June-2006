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
using System.ComponentModel.Design;
using EnvDTE;
using Microsoft.Practices.ComponentModel;

namespace Microsoft.Practices.GuidanceAutomation.SmartClient.Library.CodeModel.Editors.Filters
{
	/// <summary>
	/// Filter for the CodeModelEditor, configure using the "InheritsFrom" parameter in 
	/// your xml configuration file
	/// </summary>
	[ServiceDependency(typeof(IDictionaryService))]
	public sealed class InheritsFrom : SitedComponent, ICodeModelEditorFilter
	{
		#region Private Implementation

		private string _inheritsFromClassName = string.Empty;

		internal static string NormalizeName(string className)
		{
			int leftParam = className.IndexOf('<');
			if (leftParam != -1)
			{
				className = className.Substring(0, leftParam + 1) + '>';
			}
			return className;
		}

		private bool CheckInheritsFrom(CodeElementEx codeClass)
		{
			string classFullName = NormalizeName(codeClass.RealObject.FullName);
			if (classFullName.Equals(this._inheritsFromClassName, StringComparison.InvariantCultureIgnoreCase))
			{
				return true;
			}
			if (codeClass.Bases == null)
			{
				return false;
			}
			foreach (CodeElement codeBase in codeClass.Bases)
			{
				string fullBaseName = NormalizeName(codeBase.FullName);
				if (fullBaseName.Equals(this._inheritsFromClassName, StringComparison.InvariantCultureIgnoreCase))
				{
					return true;
				}
				else if (CheckInheritsFrom(new CodeElementEx(codeBase)))
				{
					return true;
				}
			}
			return false;
		}

		private bool HasClassInheritFrom(CodeNamespace codeNamespace)
		{
			foreach (CodeElement codeElement in codeNamespace.Members)
			{
				CodeElementEx codeElementEx = new CodeElementEx(codeElement);
				if (codeElementEx.Bases != null)
				{
					if (CheckInheritsFrom(codeElementEx))
					{
						return true;
					}
				}
				else if (codeElement is CodeNamespace)
				{
					if (HasClassInheritFrom((CodeNamespace)codeElement))
					{
						return true;
					}
				}
			}
			return false;
		}

		#endregion

		#region Overrides

		/// <summary>
		/// Get's the configured parameter "InheritsFrom"
		/// </summary>
		protected override void OnSited()
		{
			base.OnSited();
			IDictionaryService dictService =
					(IDictionaryService)GetService(typeof(IDictionaryService));
			_inheritsFromClassName = (string)dictService.GetValue("InheritsFrom");
		}

		#endregion

		#region ICodeModelEditorFilter Members

		bool ICodeModelEditorFilter.Filter(CodeElement codeElement)
		{
			CodeElementEx codeElementEx = new CodeElementEx(codeElement);
			if (codeElementEx.Bases != null)
			{
				return !CheckInheritsFrom(codeElementEx);
			}
			else if (codeElement is CodeNamespace)
			{
				return !HasClassInheritFrom((CodeNamespace)codeElement);
			}
			// Filter everything else
			return true;
		}

		#endregion
	}
}
