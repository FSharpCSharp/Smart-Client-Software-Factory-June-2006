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
using Microsoft.VisualStudio.Shell.Interop;

namespace Microsoft.Practices.GuidanceAutomation.SmartClient.Library.Actions
{
	/// <summary>
	/// Sets the context of an project item, even if the project item is opened by Visual Studio
	/// </summary>
	[ServiceDependency(typeof(ILocalRegistry))]
	public sealed class GetProjectItemContentAction : Action
	{
		#region Input Properties

		/// <summary>
		/// The project item that is receiving the new content
		/// </summary>
		[Input(Required = true)]
		public ProjectItem ProjectItem
		{
			get { return projectItem; }
			set { projectItem = value; }
		} ProjectItem projectItem;

		#endregion

		#region Output Properties

		/// <summary>
		/// The content of the project item
		/// </summary>
		[Output]
		public string Content
		{
			get { return content; }
			set { content = value; }
		} string content;

		#endregion

		#region IAction Members

		/// <summary>
		/// Gets the content of the project item
		/// </summary>
		public override void Execute()
		{
			string fileName = this.ProjectItem.get_FileNames(0);
			using (StreamReader fileStream = new StreamReader(fileName))
			{
				this.Content = fileStream.ReadToEnd();
				return;
			}
		}

		/// <summary>
		/// Undoes the get
		/// </summary>
		public override void Undo()
		{
			this.Content = string.Empty;
		}

		#endregion
	}
}
