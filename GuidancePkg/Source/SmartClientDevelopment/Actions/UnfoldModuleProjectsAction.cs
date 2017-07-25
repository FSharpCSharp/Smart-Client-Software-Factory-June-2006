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
using EnvDTE80;
using System.IO;
using Microsoft.Practices.Common.Services;
using System.ComponentModel.Design;
using VSLangProj;
using Microsoft.Practices.GuidanceAutomation.SmartClient.Library;
using System.Collections;
using System.Xml;

namespace Microsoft.Practices.GuidanceAutomation.SmartClient.Actions
{
	public class UnfoldModuleProjectsAction : ConfigurableAction
	{

		private const string CABProfileNamespaceV1 = "http://schemas.microsoft.com/pag/cab-profile";
		private const string CABProfileNamespaceV2 = "http://schemas.microsoft.com/pag/cab-profile/2.0";

		private string _layoutTemplate;
		private string _interfaceTemplate;
		private bool _createInterface;
		private bool _createLayout;
		private Project _shellProject;
		private Project _interfaceProject;
		private Project _moduleProject;
		private string _baseTemplate;
		private Project _moduleInterfaceProject;

		[Input]
		public string BaseTemplate
		{
			get { return _baseTemplate; }
			set { _baseTemplate = value; }
		}
	
		[Input]
		[Output]
		public Project ModuleProject
		{
			get { return _moduleProject; }
			set { _moduleProject = value; }
		}

		[Output]
		public Project ModuleInterfaceProject
		{
			get { return _moduleInterfaceProject; }
			set { _moduleInterfaceProject = value; }
		}
	
		[Input]
		public Project InterfaceProject
		{
			get { return _interfaceProject; }
			set { _interfaceProject = value; }
		}


		[Input]
		public Project ShellProject
		{
			get { return _shellProject; }
			set { _shellProject = value; }
		}

		[Input]
		public bool CreateLayout
		{
			get { return _createLayout; }
			set { _createLayout = value; }
		}

		[Input]
		public bool CreateInterface
		{
			get { return _createInterface; }
			set { _createInterface = value; }
		}

		[Input]
		public string InterfaceTemplate
		{
			get { return _interfaceTemplate; }
			set { _interfaceTemplate = value; }
		}

		[Input]
		public string LayoutTemplate
		{
			get { return _layoutTemplate; }
			set { _layoutTemplate = value; }
		}

	
		public override void Execute()
		{
			DTE dte = GetService<DTE>();

			// TODO: Here we can move the project to the "Source"  folder
			string projectName = ModuleProject.Name;
			object target = ModuleProject.ParentProjectItem == null ? ModuleProject.DTE.Solution : ModuleProject.ParentProjectItem.ContainingProject.Object;
			string targetDirectory = Path.GetDirectoryName(ModuleProject.FileName);

			// Remove original (handle) project
			dte.Solution.Remove(ModuleProject);

			if (CreateLayout)
			{
				UnfoldTemplateOnTarget(target, LayoutTemplate, targetDirectory, projectName);
			}
			else
			{
				UnfoldTemplateOnTarget(target, BaseTemplate, targetDirectory, projectName);
			}
			ModuleProject = DteHelper.FindProjectByName(dte, projectName);


			// Unfold required projects
			if (CreateInterface)
			{
				string interfaceProjectName = String.Format("{0}.Interface", ModuleProject.Name);
				UnfoldTemplateOnTarget(target, InterfaceTemplate, targetDirectory, interfaceProjectName);
				ModuleInterfaceProject = DteHelper.FindProjectByName(dte, interfaceProjectName);
			}


			// Update projects references
			if (CreateInterface)
			{
				((VSProject)ModuleInterfaceProject.Object).References.AddProject(InterfaceProject);
				((VSProject)ModuleProject.Object).References.AddProject(ModuleInterfaceProject);
			}
			else
			{
				((VSProject)ModuleProject.Object).References.AddProject(InterfaceProject);
			}
		}

		public override void Undo()
		{
		}


		private void UnfoldTemplateOnTarget(object target, string template, string destination, string name)
		{
			if (target is SolutionFolder)
			{
				((SolutionFolder)target).AddFromTemplate(ResolveTemplateDirectory(template), Path.Combine(destination, name), name);
			}
			else if (target is Solution)
			{
				((Solution)target).AddFromTemplate(ResolveTemplateDirectory(template), Path.Combine(destination, name), name, false);
			}
		}

		private string ResolveTemplateDirectory(string template)
		{
			if (!File.Exists(template))
			{
				TypeResolutionService typeResService = (TypeResolutionService)GetService(typeof(ITypeResolutionService));
				if (typeResService != null)
				{
					template = new FileInfo(Path.Combine(
						typeResService.BasePath + @"\Templates\", template)).FullName;
				}
			}
			return template;
		}

	}
}
