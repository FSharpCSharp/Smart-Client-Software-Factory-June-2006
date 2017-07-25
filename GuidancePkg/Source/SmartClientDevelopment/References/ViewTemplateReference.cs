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
using System.Runtime.Serialization;
using Microsoft.Practices.RecipeFramework;
using Microsoft.Practices.RecipeFramework.VisualStudio.Templates;
using EnvDTE;
using Microsoft.Practices.GuidanceAutomation.SmartClient.Library;
using VSLangProj;

namespace Microsoft.Practices.GuidanceAutomation.SmartClient.References
{
	[Serializable]
	public class ViewTemplateReference : UnboundRecipeReference
	{
		public ViewTemplateReference(string recipe)
			: base(recipe)
		{
		}

		public ViewTemplateReference(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		public override bool IsEnabledFor(object target)
		{
			if (target is Project)
			{
				return ContainsRequiredReferences((Project)target);
			}
			if (target is ProjectItem)
			{
				ProjectItem item = (ProjectItem)target;
				if (item.Kind == "{6BB5F8EF-4483-11D3-8BCF-00C04F8EC28C}")
				{
					return ContainsRequiredReferences(item.ContainingProject);
				}
				return false;
			}
			return false;
		}

		private bool ContainsRequiredReferences(Project project)
		{
			DTE dte = GetService<DTE>();
			if (!dte.Solution.Globals.get_VariableExists("CommonProjectGuid"))
				return false;

			Guid commonProjectGuid = new Guid((string)dte.Solution.Globals["CommonProjectGuid"]);
			Project prjCommon = Utility.GetProjectFromGuid(dte, GetService<IServiceProvider>(), commonProjectGuid);
			
			return ContainsReference(project, "Microsoft.Practices.CompositeUI") &&
				ContainsReference(project, "Microsoft.Practices.ObjectBuilder") &&
				ContainsReference(project, prjCommon.Name);
		}

		private bool ContainsReference(Project project, string referenceIdentity)
		{
			if (project.Name == referenceIdentity) return true;
			VSProject vsProject = (VSProject)project.Object;
			vsProject.Refresh();
			foreach (Reference reference in vsProject.References)
			{
				if (reference.Identity == referenceIdentity) return true;
			}
			return false;
		}

		public override string AppliesTo
		{
			get { return "Any C# project referencing CAB Libraries and the Infrastructure.Interface library."; }
		}
	}
}
