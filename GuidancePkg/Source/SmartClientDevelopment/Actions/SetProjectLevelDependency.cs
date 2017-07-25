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

using Microsoft.Practices.RecipeFramework;
using EnvDTE;
using VSLangProj;
using VSLangProj80;

namespace Microsoft.Practices.GuidanceAutomation.SmartClient.Actions
{
	public class SetProjectLevelDependency : ConfigurableAction
	{
		private Project _shellProject;
		private Project _moduleProject;

		[Input(Required = true)]
		public Project ShellProject
		{
			get { return _shellProject; }
			set { _shellProject = value; }
		}

		[Input(Required = true)]
		public Project ModuleProject
		{
			get { return _moduleProject; }
			set { _moduleProject = value; }
		}

		public override void Execute()
		{
			DTE dte = GetService<DTE>();
			foreach (BuildDependency bd in dte.Solution.SolutionBuild.BuildDependencies)
			{
				if (bd.Project == _shellProject)
				{
					bd.AddProject(_moduleProject.UniqueName);
					return;
				}
			}
		}

		public override void Undo()
		{
		}
	}
}
