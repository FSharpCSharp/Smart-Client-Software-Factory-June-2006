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

using System.Xml;
using EnvDTE;
using Microsoft.Practices.GuidanceAutomation.SmartClient.Library.Actions;
using Microsoft.Practices.RecipeFramework;

namespace Microsoft.Practices.GuidanceAutomation.SmartClient.Library.XML.Actions
{
	/// <summary>
	/// Loads an <see cref="XmlDocument"/> from an <see cref="EnvDTE.ProjectItem"/>
	/// </summary>
	public sealed class LoadXmlAction : ConfigurableAction
	{
		#region Input Properties

		/// <summary>
		/// The <see cref="ProjectItem"/> whose content is XML
		/// </summary>
		[Input(Required = true)]
		public ProjectItem ProjectItem
		{
			get { return projectItem; }
			set { projectItem = value; }
		} ProjectItem projectItem;

		#endregion

		#region Output properties

		/// <summary>
		/// The output <see cref="XmlDocument"/> object
		/// </summary>
		[Output]
		public XmlDocument XmlDoc
		{
			get { return xmlDoc; }
			set { xmlDoc = value; }
		} XmlDocument xmlDoc;

		#endregion

		#region Overrides

		/// <summary>
		/// Loads an <see cref="XmlDocument"/> from a project item
		/// </summary>
		public override void Execute()
		{
			using (GetProjectItemContentAction getContent = new GetProjectItemContentAction())
			{
				this.Container.Add(getContent);
				getContent.ProjectItem = this.ProjectItem;
				getContent.Execute();
				this.XmlDoc = new XmlDocument();
				this.XmlDoc.LoadXml(getContent.Content);
			}
		}

		/// <summary>
		/// Not supported
		/// </summary>
		public override void Undo()
		{
			this.XmlDoc = null;
		}

		#endregion
	}
}
