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
	/// Adds a new member varaible to a class
	/// </summary>
	public class AddFieldAction : ConfigurableAction
	{
		#region Input Properties

		/// <summary>
		/// The class where the new member variable is been added
		/// </summary>
		[Input(Required = true)]
		public CodeClass CodeClass
		{
			get { return codeClass; }
			set { codeClass = value; }
		} CodeClass codeClass;

		/// <summary>
		/// The name of the variable been added
		/// </summary>
		[Input(Required = true)]
		public string FieldName
		{
			get { return name; }
			set { name = value; }
		} string name;

		/// <summary>
		/// The type of the variable been added
		/// </summary>
		[Input(Required = false)]
		public object FieldType
		{
			get { return fieldType; }
			set { fieldType = value; }
		} object fieldType = vsCMTypeRef.vsCMTypeRefInt;

		/// <summary>
		/// The position in the class where the member is been added
		/// </summary>
		[Input(Required = false)]
		public object Position
		{
			get { return position; }
			set { position = value; }
		} object position = 0;

		/// <summary>
		/// The kind of visibility of the new variable
		/// </summary>
		/// <seealso cref="vsCMAccess"/>
		[Input(Required = false)]
		public vsCMAccess Access
		{
			get { return access; }
			set { access = value; }
		} vsCMAccess access = vsCMAccess.vsCMAccessPublic;

		#endregion

		#region Output Properties

		/// <summary>
		/// The newly created member variable
		/// </summary>
		[Output]
		public CodeVariable Field
		{
			get { return field; }
			set { field = value; }
		} CodeVariable field;

		#endregion

		#region Action members

		/// <summary>
		/// <seealso cref="IAction.Execute"/>
		/// </summary>
		public override void Execute()
		{
			this.Field = this.CodeClass.AddVariable(this.FieldName,
					this.FieldType,
					this.Position,
					this.Access,
					null);
		}

		/// <summary>
		/// <seealso cref="IAction.Undo"/>
		/// </summary>
		public override void Undo()
		{
			this.CodeClass.RemoveMember(this.Field);
		}

		#endregion
	}
}
