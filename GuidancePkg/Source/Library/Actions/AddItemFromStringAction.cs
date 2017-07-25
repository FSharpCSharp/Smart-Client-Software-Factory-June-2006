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

using System.IO;
using EnvDTE;
using Microsoft.Practices.ComponentModel;
using Microsoft.Practices.RecipeFramework;

namespace Microsoft.Practices.GuidanceAutomation.SmartClient.Library.Actions
{
	/// <summary>
	/// The action creates a project item from a string passed to the action
	/// in the Content input property. The other two input properties of the
	/// action are (a) targetFileName - provides the name of the item file 
	/// and (b) Project - identifies the project where the item is created. 
	/// The action is designed to follow the T3Action. 
	/// </summary>
	[ServiceDependency(typeof(DTE))]
	public class AddItemFromStringAction : ConfigurableAction
	{
		#region Input Properties

		private string _content;
		private string _targetFileName;
		private Project _project;
		private bool _open = true;

		/// <summary>
		/// The string with the content of the item. In most cases it is a class
		/// generated using the T3 engine.
		/// </summary>
		[Input(Required = true)]
		public string Content
		{
			get { return _content; }
			set { _content = value; }
		}

		/// <summary>
		/// Name of the file where the item is to be stored.
		/// </summary>
		[Input(Required = true)]
		public string TargetFileName
		{
			get { return _targetFileName; }
			set { _targetFileName = value; }
		}

		/// <summary>
		/// Project where the item it to be inserted.
		/// </summary>
		[Input(Required = true)]
		public Project Project
		{
			get { return _project; }
			set { _project = value; }
		}

		/// <summary>
		/// A flag to indicate if the newly created item should be shown
		/// in a window.
		/// </summary>
		[Input]
		public bool Open
		{
			get { return _open; }
			set { _open = value; }
		}


		#endregion Input Properties

		#region Output Properties

		private ProjectItem projectItem;
		/// <summary>
		/// A property that can be used to pass the creted item to
		/// a following action.
		/// </summary>
		[Output]
		public ProjectItem ProjectItem
		{
			get { return projectItem; }
			set { projectItem = value; }
		}

		#endregion Output Properties

		/// <summary>
		/// The method that creates a new item from the intput string.
		/// </summary>
		public override void Execute()
		{
			DTE vs = GetService<DTE>(true);
			string tempfile = Path.GetTempFileName();
			using (StreamWriter sw = new StreamWriter(tempfile, false))
			{
				sw.WriteLine(_content);
			}

			projectItem = _project.ProjectItems.AddFromTemplate(tempfile, _targetFileName);
			if (_open)
			{
				Window wnd = projectItem.Open(Constants.vsViewKindPrimary);
				wnd.Visible = true;
				wnd.Activate();
			}
			File.Delete(tempfile);
		}

		/// <summary>
		/// Undoes the creation of the item, then deletes the item
		/// </summary>
		public override void Undo()
		{
			if (projectItem != null)
			{
				projectItem.Delete();
			}
		}
	}
}
