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
using System.IO;

namespace Microsoft.Practices.GuidanceAutomation.SmartClient.Actions
{
	public class CreateSolutionDirectoryAction : ConfigurableAction
	{
		private string _fullPath;
		private string _directoryName;

		[Input(Required=true)]
		public string DirectoryName
		{
			get
			{
				return _directoryName;
			}
			set
			{
				_directoryName = value;
			}
		}

		[Output]
		public string FullPath
		{
			get
			{
				return _fullPath;
			}
			set
			{
				_fullPath = value;
			}
		}

		protected virtual string GetSolutionPath()
		{
			DTE dte = GetService<DTE>();
			return Path.GetDirectoryName(dte.Solution.Properties.Item("Path").Value.ToString());
		}

		public override void Execute()
		{
			string solutionPath = GetSolutionPath();
			_fullPath = Path.Combine(solutionPath, _directoryName);
			if (_fullPath.Contains(".."))
			{
				throw new InvalidOperationException(
					String.Format(Properties.Resources.CreateSolutionDirectory_NoDots,_fullPath));
			}
			Directory.CreateDirectory(_fullPath);
		}

		public override void Undo()
		{
			throw new Exception("The method or operation is not implemented.");
		}
	}
}
