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
using EnvDTE;
using Microsoft.Practices.GuidanceAutomation.SmartClient.Library.Solution;
using Microsoft.Practices.GuidanceAutomation.SmartClient.Library;

namespace Microsoft.Practices.GuidanceAutomation.SmartClient.Actions
{
	public class AddUsingForEventTopicNames : ConfigurableAction
	{
		private string _localUsingNamespace;
		private string _globalUsingNamespace;
		private object _target;

		[Input(Required = true)]
		public string LocalUsingNamespace
		{
			get { return _localUsingNamespace; }
			set { _localUsingNamespace = value; }
		}

		[Input(Required = true)]
		public string GlobalUsingNamespace
		{
			get { return _globalUsingNamespace; }
			set { _globalUsingNamespace = value; }
		}

		[Input(Required = true)]
		public object Target
		{
			get { return _target; }
			set { _target = value; }
		}


		public override void Execute()
		{
			CodeClass codeClass = _target as CodeClass;

			// Determine which using sentence to add
			Project prjContainer = codeClass.ProjectItem.ContainingProject;
			ProjectItem constantsFolder = DteHelper.FindItemByName(prjContainer.ProjectItems, "Constants", false);
			string usingNamespace = constantsFolder == null ? GlobalUsingNamespace : LocalUsingNamespace;

			if (codeClass == null && !ReferenceUtil.HaveAClass(_target, out codeClass)) return;
			if (codeClass != null)
			{
				TextPoint tp = codeClass.StartPoint;
				EditPoint ep = tp.CreateEditPoint();
				ep.StartOfDocument();

				int lastUsing = -1;
				string usingText = String.Format("using {0}", usingNamespace);
				while (!ep.AtEndOfDocument)
				{
					int length = ep.LineLength;
					string line = ep.GetText(ep.LineLength);
					if (line.Contains(usingText)) return;
					if (line.StartsWith("using")) lastUsing = ep.Line;
					ep.LineDown(1);
				}
				ep.StartOfDocument();
				if (lastUsing > 0) ep.LineDown(lastUsing);
				ep.Insert(usingText);
				ep.Insert(";");
				ep.Insert(Environment.NewLine);
				if (ep.LineLength != 0) ep.Insert(Environment.NewLine);
			}
		}

		public override void Undo()
		{
		}
	}
}
