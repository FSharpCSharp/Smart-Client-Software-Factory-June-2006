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
using VSLangProj;

namespace Microsoft.Practices.GuidanceAutomation.SmartClient.Actions
{
	public class AddReferenceIfMissingAction : ConfigurableAction
	{
		private string _referenceName;
		private Project _project;
		private Reference _addedReference;

		public override void Execute()
		{
			VSProject vsProject = (VSProject)_project.Object;
			foreach(Reference reference in vsProject.References)
			{
				if (reference.Name == _referenceName)
					return;
			}
			_addedReference = vsProject.References.Add(_referenceName);
		}

		public override void Undo()
		{
			if (_addedReference != null)
				_addedReference.Remove();
		}


		[Input(Required=true)]
		public string ReferenceName
		{
			get { return _referenceName; }
			set { _referenceName = value; }
		}


		[Input(Required=true)]
		public Project Project
		{
			get { return _project; }
			set { _project = value; }
		}
	
	
	}
}
