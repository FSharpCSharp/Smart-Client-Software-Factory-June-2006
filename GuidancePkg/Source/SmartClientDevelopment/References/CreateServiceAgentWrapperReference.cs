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
using System.Runtime.Serialization;
using Microsoft.Practices.RecipeFramework.VisualStudio.Templates;
using EnvDTE;

namespace Microsoft.Practices.GuidanceAutomation.SmartClient.References
{              
	[Serializable]
	public class CreateServiceAgentWrapperReference : UnboundTemplateReference
	{
		public CreateServiceAgentWrapperReference(string recipe)
			: base(recipe) { }

		public CreateServiceAgentWrapperReference(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		public override bool IsEnabledFor(object target)
		{
			if (target is Project) return true;
			if (target is ProjectItem)
			{
				ProjectItem item = (ProjectItem)target;
				return item.Kind == "{6BB5F8EF-4483-11D3-8BCF-00C04F8EC28C}";
			}
			return false;
		}

		public override string AppliesTo
		{
			get { return "Any C# Project"; }
		}
	}
}
