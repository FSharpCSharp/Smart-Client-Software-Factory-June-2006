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
using EnvDTE80;
using Microsoft.Practices.ComponentModel;

namespace Microsoft.Practices.GuidanceAutomation.SmartClient.Library.CodeModel.Editors.Filters
{
	/// <summary>
	/// Filters element containing the attribute specifed in "HasAttribute"
	/// </summary>
	[ServiceDependency(typeof(IDictionaryService))]
	public class ContainsAttribute : SitedComponent, ICodeModelEditorFilter
	{
		#region Private Fields

		private Type _attributeType = null;
		private AttributeTargets _attributeUsage = (AttributeTargets)0;

		#endregion

		#region Overrides

		/// <summary>
		/// Get's the configured parameter "HasAttribute"
		/// </summary>
		protected override void OnSited()
		{
			base.OnSited();
			IDictionaryService dictService =
					(IDictionaryService)GetService(typeof(IDictionaryService));
			string attributeName = (string)dictService.GetValue("ContainsAttribute");
			ITypeResolutionService typeResService =
					(ITypeResolutionService)GetService(typeof(ITypeResolutionService));
			_attributeType = typeResService.GetType(attributeName, false);
			if (_attributeType != null)
			{
				AttributeUsageAttribute[] usages =
						(AttributeUsageAttribute[])Attribute.GetCustomAttributes(_attributeType, typeof(AttributeUsageAttribute));
				foreach (AttributeUsageAttribute usage in usages)
				{
					_attributeUsage |= usage.ValidOn;
				}
			}
		}

		#endregion

		#region Private implementation

		private AttributeTargets GetTarget(CodeElement codeElement)
		{
			if (codeElement is CodeClass)
			{
				return AttributeTargets.Class;
			}
			else if (codeElement is CodeStruct)
			{
				return AttributeTargets.Struct;
			}
			else if (codeElement is CodeInterface)
			{
				return AttributeTargets.Interface;
			}
			else if (codeElement is CodeEvent)
			{
				return AttributeTargets.Event;
			}
			else if (codeElement is CodeEnum)
			{
				return AttributeTargets.Enum;
			}
			else if (codeElement is CodeDelegate)
			{
				return AttributeTargets.Delegate;
			}
			else if (codeElement is CodeFunction)
			{
				return AttributeTargets.Method;
			}
			else if (codeElement is CodeProperty)
			{
				return AttributeTargets.Property;
			}
			else if (codeElement is CodeVariable)
			{
				return AttributeTargets.Field;
			}
			return (AttributeTargets)0;
		}

		#endregion

		#region ICodeModelEditorFilter Members

		bool ICodeModelEditorFilter.Filter(CodeElement codeElement)
		{
			AttributeTargets target = GetTarget(codeElement);
			if ((target & this._attributeUsage) != 0)
			{
				CodeElements attributes = new CodeElementEx(codeElement).Attributes;
				foreach (CodeElement attribute in attributes)
				{
					try
					{
						if (attribute.FullName.Equals(
										this._attributeType.FullName,
										StringComparison.InvariantCultureIgnoreCase))
						{
							return false;
						}
					}
					catch
					{
					}
				}
				return true;
			}
			return false;
		}

		#endregion
	}
}
