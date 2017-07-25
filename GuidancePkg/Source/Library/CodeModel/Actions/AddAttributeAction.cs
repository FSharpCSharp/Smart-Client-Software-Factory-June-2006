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

using EnvDTE;
using Microsoft.Practices.RecipeFramework;

namespace Microsoft.Practices.GuidanceAutomation.SmartClient.Library.CodeModel.Actions
{
	/// <summary>
	/// Adds an attribute to the specified <see cref="AddAttributeAction.CodeElement"/>
	/// </summary>
	public class AddAttributeAction : ConfigurableAction
	{
		#region Input Properties

		/// <summary>
		/// The <see cref="EnvDTE.CodeElement"/> where the attribute is been added
		/// </summary>
		[Input(Required = true)]
		public CodeElement CodeElement
		{
			get { return codeElement.RealObject; }
			set { codeElement = new CodeElementEx(value); }
		} CodeElementEx codeElement;

		/// <summary>
		/// The name if the attributte been added
		/// </summary>
		[Input(Required = true)]
		public string AttributeName
		{
			get { return name; }
			set { name = value; }
		} string name;

		/// <summary>
		/// The parameters of the attributte been added
		/// </summary>
		[Input(Required = false)]
		public string AttributeValue
		{
			get { return attributeValue; }
			set { attributeValue = value; }
		} string attributeValue = "";

		/// <summary>
		/// The position in the <see cref="AddAttributeAction.CodeElement"/> where the attribute is been added
		/// </summary>
		[Input(Required = false)]
		public object Position
		{
			get { return position; }
			set { position = value; }
		} object position = 0;

		#endregion

		#region Output Properties

		/// <summary>
		/// The attributte added to the <see cref="AddAttributeAction.CodeElement"/>
		/// </summary>
		[Output]
		public CodeAttribute Attribute
		{
			get { return attribute; }
			set { attribute = value; }
		} CodeAttribute attribute;

		#endregion

		#region Action members

		/// <summary>
		/// Adds the attributte
		/// </summary>
		/// <seealso cref="IAction.Execute"/>
		public override void Execute()
		{
			if (!string.IsNullOrEmpty(this.AttributeValue))
			{
				if (this.AttributeValue[0] != '\"')
				{
					this.AttributeValue = "\"" + this.AttributeValue + "\"";
				}
			}
			this.Attribute = this.codeElement.AddAttribute(
									this.AttributeName,
									this.AttributeValue,
									this.Position);
		}

		/// <summary>
		/// <seealso cref="IAction.Undo"/>
		/// </summary>
		public override void Undo()
		{
			// No undo support
		}

		#endregion
	}
}
