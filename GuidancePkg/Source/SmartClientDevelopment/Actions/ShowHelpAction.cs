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
using Microsoft.Practices.RecipeFramework.Services;
using System.IO;

namespace Microsoft.Practices.GuidanceAutomation.SmartClient.Actions
{
	public class ShowHelpAction : ConfigurableAction
	{
		private string _templateFile;

		[Input(Required = true)]
		public string TemplateFile
		{
			get { return _templateFile; }
			set { _templateFile = value; }
		}

		public override void Execute()
		{
			IConfigurationService configSvc = GetService<IConfigurationService>();
			object customOut = null;
			DTE dte = GetService<DTE>();
			dte.ExecuteCommand("View.WebBrowser", String.Empty);
			object customIn = Path.Combine(configSvc.BasePath, _templateFile);
			dte.Commands.Raise("{E8B06F44-6D01-11D2-AA7D-00C04F990343}", 207, ref customIn, ref customOut);
		}

		public override void Undo()
		{
		}

	}
}
