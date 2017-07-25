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
using System.IO;
using EnvDTE;

namespace Microsoft.Practices.GuidanceAutomation.SmartClient.Actions
{
	public class AddItemFromStringToProjectItemsAction  : ConfigurableAction
	{
		private ProjectItems _targetCollection;
		private ProjectItems _createdProjectItems;
		private ProjectItem _createdItem;
		private string _content;
		private bool _open;
		private string _targetFileName;

		public AddItemFromStringToProjectItemsAction()
		{
			_open = true;
		}

		public override void Execute()
		{
			base.GetService<DTE>(true);
			string tempFilename = Path.GetTempFileName();
			using (StreamWriter sw = new StreamWriter(tempFilename, false))
			{
				sw.WriteLine(_content);
			}
			_createdItem = _targetCollection.AddFromTemplate(tempFilename, _targetFileName);
			_createdProjectItems = _createdItem.ProjectItems;
			if (_open)
			{
				Window window = _createdItem.Open("{00000000-0000-0000-0000-000000000000}");
				window.Visible = true;
				window.Activate();
			}
			File.Delete(tempFilename);
		}

		public override void Undo()
		{
			if (_createdItem != null)
			{
				_createdItem.Delete();
			}
		}

		[Input(Required = true)]
		public string Content
		{
			get { return _content; }
			set { _content = value; }
		}

		[Input]
		public bool Open
		{
			get { return _open; }
			set { _open = value; }
		}

		[Input(Required = true)]
		public ProjectItems TargetCollection
		{
			get { return _targetCollection; }
			set { _targetCollection = value; }
		}
	
		[Input(Required = true)]
		public string TargetFileName
		{
			get { return _targetFileName; }
			set { _targetFileName = value; }
		}

		[Output]
		public ProjectItems CreatedProjectItems
		{
			get { return _createdProjectItems; }
			set { _createdProjectItems = value; }
		}

	}
}
