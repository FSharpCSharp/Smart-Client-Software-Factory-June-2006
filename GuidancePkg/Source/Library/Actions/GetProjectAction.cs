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

using EnvDTE;
using Microsoft.Practices.RecipeFramework;

namespace Microsoft.Practices.GuidanceAutomation.SmartClient.Library.Actions
{
	/// <summary>
	/// The action returns a reference to a solution project.The reference
	/// is returned in the output property called Project. The action has one input 
	/// property ProjecName - name of the project to find in the solution. 
	/// If the project with the given name is not found, the recipe returns Null
	/// reference. 
	/// </summary>
	public sealed class GetProjectAction : ConfigurableAction
	{
		#region Input Properties

		/// <summary>
		/// Name of the project to find in the solution
		/// </summary>
		[Input(Required = true)]
		public string ProjectName
		{
			get { return projectName; }
			set { projectName = value; }
		} string projectName;

		#endregion

		#region Output properties

		/// <summary>
		/// The object project element of the solution
		/// </summary>
		[Output]
		public Project Project
		{
			get { return project; }
			set { project = value; }
		} Project project;

		#endregion

		#region IAction Members

		/// <summary>
		/// Sets the property output Project to the appropiate dte element
		/// </summary>
		public override void Execute()
		{
			DTE dte = GetService<DTE>(true);
			this.Project = DteHelper.FindProjectByName(dte, ProjectName);
		}

		/// <summary>
		/// Undoes the set
		/// </summary>
		public override void Undo()
		{
		}

		#endregion

	}
}
