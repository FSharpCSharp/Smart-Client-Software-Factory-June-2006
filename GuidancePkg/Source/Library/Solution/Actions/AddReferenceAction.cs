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
using System.Collections;
using System.ComponentModel;
using System.IO;
using EnvDTE;
using VSLangProj;
using Microsoft.Practices.RecipeFramework;
using Microsoft.Practices.ComponentModel;

namespace Microsoft.Practices.GuidanceAutomation.SmartClient.Library.Solution.Actions
{
	/// <summary>
	/// Adds a reference to a project pointing to another 
	/// project in the same solution. 
	/// </summary>
	[System.ComponentModel.DesignerCategory("Code")]
	[ServiceDependency(typeof(DTE))]
	public class AddReferenceAction : ConfigurableAction
	{
		Project _referringProject;
		string _referenceName;
		Reference _reference;

		/// <summary>
		/// The project where the reference is been added
		/// </summary>
		[Input(Required = true)]
		public Project ReferringProject
		{
			get { return _referringProject; }
			set { _referringProject = value; }
		}

		/// <summary>
		/// The file name reference
		/// </summary>
		[Input(Required = true)]
		public string ReferenceName
		{
			get { return _referenceName; }
			set { _referenceName = value; }
		}

		/// <summary>
		/// Adds the reference to the project
		/// </summary>
		public override void Execute()
		{
			VSProject vsProject = ((VSProject)_referringProject.Object);
			if (!Path.IsPathRooted(_referenceName))
			{
				string solutionDir = Path.GetDirectoryName(vsProject.DTE.Solution.FullName);
				_referenceName = Path.Combine(
					Path.Combine(solutionDir,"Lib"),
					_referenceName);
			}
			string keyName = Path.GetFileNameWithoutExtension(_referenceName);
			if (vsProject.References.Find(keyName) == null)
			{
				_reference = vsProject.References.Add(this._referenceName);
			}
		}

		/// <summary>
		/// Removes the added reference
		/// </summary>
		public override void Undo()
		{
			if (_reference != null)
			{
				VSProject vsProject = ((VSProject)_referringProject.Object);
				_reference.Remove();
				_reference = null;
			}
		}
	}
}

