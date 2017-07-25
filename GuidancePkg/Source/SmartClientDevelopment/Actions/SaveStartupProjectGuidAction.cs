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
using Microsoft.Practices.GuidanceAutomation.SmartClient.Library;
using Microsoft.VisualStudio.Shell.Interop;

namespace Microsoft.Practices.GuidanceAutomation.SmartClient.Actions
{
	public class SaveShellProjectGuidAction : ConfigurableAction
	{
		private const string ShellProjectGuid = "ShellProjectGuid";

		public override void Execute()
		{
			DTE dte = GetService<DTE>();
			Solution sln = dte.Solution;
			Project project = DteHelper.FindProjectByName(dte, sln.Properties.Item("StartupProject").Value.ToString());
			if (project != null)
			{
				Guid projectGuid = Utility.GetProjectGuid(GetService<IServiceProvider>(), project);
				sln.Globals[ShellProjectGuid] = projectGuid.ToString("B");
				sln.Globals.set_VariablePersists(ShellProjectGuid, true);
			}
		}

		public override void Undo()
		{
		}
	}
}
