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
using Microsoft.Practices.GuidanceAutomation.SmartClient.Library;
using Microsoft.Practices.Common;
using System.ComponentModel.Design;
using System.Collections.Specialized;
using System.Text.RegularExpressions;

namespace Microsoft.Practices.GuidanceAutomation.SmartClient.Converters
{
	public class EventTopicSubscriptionValidator : TypeConverter, IAttributesConfigurable
	{
		private string _codeClassArgumentName;
		private string _commonClass;

		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			DTE dte = (DTE)context.GetService(typeof(DTE));
			IDictionaryService dict = (IDictionaryService)context.GetService(typeof(IDictionaryService));
			List<string> members = new List<string>();

			if (dict != null)
			{
				CodeClass target = (CodeClass)dict.GetValue(_codeClassArgumentName);
				if (target != null)
					members.AddRange(GetDefinedConstants(target));
				target = (CodeClass)dict.GetValue(_commonClass);
				members.AddRange(GetDefinedConstants(target));
			}
			return new StandardValuesCollection(members);
		}

		private List<string> GetDefinedConstants(CodeClass target)
		{
			List<string> result = new List<string>();
			EditPoint startPoint = target.StartPoint.CreateEditPoint();
			string text = startPoint.GetText(target.EndPoint);
			Regex exp = new Regex(@"const\s*\w*\s*(\w*)", RegexOptions.Multiline);
			foreach (Match match in exp.Matches(text))
			{
				result.Add(match.Groups[1].Value);
			}

			return result;
		}

		public override bool IsValid(ITypeDescriptorContext context, object value)
		{
			CodeClass cc = FindCodeClass(context);
			if (cc != null)
			{
				foreach (CodeElement ce in cc.Members)
				{
					if (ce.Name == "On" + value)
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


		public void Configure(StringDictionary attributes)
		{
			_codeClassArgumentName = attributes["CodeClass"];
			_commonClass = attributes["CommonCodeClass"];
		}
	}
}
