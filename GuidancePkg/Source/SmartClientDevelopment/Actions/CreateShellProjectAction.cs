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
using EnvDTE80;
using EnvDTE;
using System.IO;
using Microsoft.Practices.Common.Services;
using System.ComponentModel.Design;
using Microsoft.Practices.GuidanceAutomation.SmartClient.Library;

namespace Microsoft.Practices.GuidanceAutomation.SmartClient.Actions
{
	public class CreateShellProjectAction : ConfigurableAction
	{
		private object _root;
		private string _shellProjectName;
		private string _layoutProjectName;
		private bool _useExtendedShell;
		private string _basicShellTemplate;
		private string _extendedShellTemplate;
		private string _layoutTemplate;
		private string _destinationDir;

		private object _shellProject;
		private object _layoutProject;

		[Input(Required = true)]
		public string LayoutProjectName
		{
			get { return _layoutProjectName; }
			set { _layoutProjectName = value; }
		}

		[Input]
		public string DestinationDirectory
		{
			get { return _destinationDir; }
			set { _destinationDir = value; }
		}

		[Input]
		public string LayoutTemplate
		{
			get { return _layoutTemplate; }
			set { _layoutTemplate = value; }
		}

		[Input(Required = true)]
		public string BasicShellTemplate
		{
			get { return _basicShellTemplate; }
			set { _basicShellTemplate = value; }
		}

		[Input]
		public string ExtendedShellTemplate
		{
			get { return _extendedShellTemplate; }
			set { _extendedShellTemplate = value; }
		}


		[Input(Required = true)]
		public bool UseExtendedShell
		{
			get { return _useExtendedShell; }
			set { _useExtendedShell = value; }
		}

		[Input(Required = true)]
		public string ShellProjectName
		{
			get { return _shellProjectName; }
			set { _shellProjectName = value; }
		}

		[Input(Required = true)]
		public object Root
		{
			get { return _root; }
			set { _root = value; }
		}

		[Output]
		public object ShellProject
		{
			get { return _shellProject; }
			set { _shellProject = value; }
		}

		[Output]
		public object LayoutProject
		{
			get { return _layoutProject; }
			set { _layoutProject = value; }
		}

		public override void Execute()
		{
			DTE dte = GetService<DTE>();
			string solutionDirectory = Path.GetDirectoryName((string)dte.Solution.Properties.Item("Path").Value);
			string targetDirectory = Path.Combine(solutionDirectory, DestinationDirectory);

			if (Root == null)
			{
				if (UseExtendedShell)
				{
					AddTemplateToSolution(dte.Solution, ResolveTemplateDirectory(ExtendedShellTemplate), targetDirectory, LayoutProjectName);
					AddTemplateToSolution(dte.Solution, ResolveTemplateDirectory(LayoutTemplate), targetDirectory, LayoutProjectName);
				}
				else
				{
					AddTemplateToSolution(dte.Solution, ResolveTemplateDirectory(BasicShellTemplate), targetDirectory, ShellProjectName);
				}
			}
			if (Root is Project && ((Project)Root).Object is SolutionFolder)
			{
				SolutionFolder slnFolder = ((Project)Root).Object as SolutionFolder;
				if (UseExtendedShell)
				{
					AddTemplateToSolutionFolder(slnFolder, ResolveTemplateDirectory(ExtendedShellTemplate), targetDirectory, ShellProjectName);
					AddTemplateToSolutionFolder(slnFolder, ResolveTemplateDirectory(LayoutTemplate), targetDirectory, LayoutProjectName);
				}
				else
				{
					AddTemplateToSolutionFolder(slnFolder, ResolveTemplateDirectory(BasicShellTemplate), targetDirectory, ShellProjectName);
				}
			}

			ShellProject = DteHelper.FindProjectByName(dte, ShellProjectName);
			LayoutProject = DteHelper.FindProjectByName(dte, LayoutProjectName);
		}

		private void AddTemplateToSolutionFolder(SolutionFolder solutionFolder, string template, string destination, string name)
		{
			solutionFolder.AddFromTemplate(template, Path.Combine(destination, name), name);
		}

		private void AddTemplateToSolution(Solution solution, string template, string destination, string name)
		{
			solution.AddFromTemplate(template, Path.Combine(destination, name), name, false);
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

		public override void Undo()
		{
		}
	}
}
