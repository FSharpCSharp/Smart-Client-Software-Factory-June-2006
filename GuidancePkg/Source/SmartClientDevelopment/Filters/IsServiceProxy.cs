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
using EnvDTE;
using Microsoft.Practices.GuidanceAutomation.SmartClient.Library.CodeModel.Editors;

namespace Microsoft.Practices.GuidanceAutomation.SmartClient.Filters
{
	public class IsServiceProxyFilter : ICodeModelEditorFilter
	{
		public bool Filter(EnvDTE.CodeElement codeElement)
		{
			if (codeElement is CodeClass)
			{
				return !IsServiceProxy((CodeClass)codeElement);
			}
			else if (codeElement is CodeNamespace)
			{
				return !HasServiceProxy((CodeNamespace)codeElement);
			}
			return false;
		}

		private bool HasServiceProxy(CodeNamespace codeNamespace)
		{
			foreach (CodeElement codeElement in codeNamespace.Members)
			{
				if (codeElement is CodeClass)
				{
					if (IsServiceProxy((CodeClass)codeElement))
					{
						return true;
					}
				}
				else if (codeElement is CodeNamespace)
				{
					if (HasServiceProxy((CodeNamespace)codeElement))
					{
						return true;
					}
				}
			}
			return false;
		}

		private bool IsServiceProxy(CodeClass codeClass)
		{
			if (codeClass.Bases.Count > 0)
			{
				CodeElement baseClass = codeClass.Bases.Item(1);
				if (baseClass.Name.Equals("ServiceProxy", StringComparison.InvariantCulture))
				{
					return true;
				}
			}
			return false;
		}
	}
}
