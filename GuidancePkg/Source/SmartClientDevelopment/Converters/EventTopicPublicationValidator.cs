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
using System.ComponentModel;
using EnvDTE;

namespace Microsoft.Practices.GuidanceAutomation.SmartClient.Converters
{
	public class EventTopicPublicationValidator : TypeConverter
	{
		public override bool IsValid(ITypeDescriptorContext context, object value)
		{
			CodeClass cc = FindCodeClass(context);
			if (cc != null)
			{
				foreach (CodeElement ce in cc.Members)
				{
					if (ce.Name == (string)value)
						return false;
				}
			}
			return true;
		}



		private CodeClass FindCodeClass(IServiceProvider context)
		{
			DTE dte = context.GetService(typeof(DTE)) as DTE;

			if (dte.SelectedItems.Count == 1)
			{
				ProjectItem item = dte.SelectedItems.Item(1).ProjectItem;
				foreach (CodeElement element in item.FileCodeModel.CodeElements)
				{
					if (!(element is CodeNamespace)) continue;
					CodeNamespace cn = (CodeNamespace)element;
					if (cn.Members.Count > 0 && cn.Members.Item(1) is CodeClass)
						return (CodeClass)cn.Members.Item(1);
				}
			}
			return null;
		}

	}
}
