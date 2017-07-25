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

using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using EnvDTE;
using Microsoft.Practices.ComponentModel;
using Microsoft.Practices.GuidanceAutomation.SmartClient.Library;
using Microsoft.Practices.RecipeFramework.Services;
using Microsoft.Practices.RecipeFramework.VisualStudio.Library.Templates;
using Microsoft.VisualStudio.TextTemplating;

namespace Microsoft.Practices.GuidanceAutomation.SmartClient
{
	public class T4TemplateProcessor : SitedComponent
	{
		DTE _dte;
		IConfigurationService _configurationService;

		public T4TemplateProcessor(DTE dte, IConfigurationService configurationService)
		{
			_dte = dte;
			_configurationService = configurationService;
		}

		protected DTE DTE
		{
			get { return _dte; }
		}

		protected IConfigurationService ConfigurationService
		{
			get { return _configurationService; }
		}

		public void ProcessTemplateToFile(string templateFilename, string outputFilename, IDictionary<string, object> args)
		{
			string content = ProcessTemplate(templateFilename, args);
			File.WriteAllText(outputFilename, content);
			GetSelectedProject().ProjectItems.AddFromFile(outputFilename);
		}

		private string ProcessTemplate(string templateFilename, IDictionary<string, object> args)
		{
			string content = File.ReadAllText(Path.Combine(ConfigurationService.BasePath, templateFilename));

			IDictionary<string, PropertyData> templateArgs = new Dictionary<string, PropertyData>();

			foreach (string key in args.Keys)
				templateArgs.Add(key, new PropertyData(args[key], args[key].GetType()));

			ITextTemplatingEngine engine = new Engine();
			TemplateHost host = new TemplateHost(".", templateArgs);
			host.TemplateFile = templateFilename;
		
			string result = engine.ProcessTemplate(content, host);

			foreach (CompilerError error in host.Errors)
				Trace.WriteLine("error: " + error.ErrorText);

			return result;
		}

		private Project GetSelectedProject()
		{
			return DteHelper.GetSelectedProject(DTE);
		}
	}
}
