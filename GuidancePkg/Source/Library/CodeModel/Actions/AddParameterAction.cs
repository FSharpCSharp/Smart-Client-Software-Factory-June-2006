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
	/// Adds a parameter to a <see cref="EnvDTE.CodeFunction"/>
	/// </summary>
	public class AddParameterAction : ConfigurableAction
	{
		#region Input Properties

		/// <summary>
		/// The method where the new parameter is been added
		/// </summary>
		[Input(Required = true)]
		public CodeFunction Method
		{
			get { return method; }
			set { method = value; }
		} CodeFunction method;

		/// <summary>
		/// The name of the new parameter
		/// </summary>
		[Input(Required = true)]
		public string ParameterName
		{
			get { return name; }
			set { name = value; }
		} string name;

		/// <summary>
		/// The type of the new parameter
		/// </summary>
		[Input(Required = false)]
		public object ParameterType
		{
			get { return retType; }
			set { retType = value; }
		} object retType = vsCMTypeRef.vsCMTypeRefInt;

		/// <summary>
		/// Position in the method where the new parameter will be added
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
		/// The CodeElemnt object of the new parameter
		/// </summary>
		[Output]
		public CodeParameter Parameter
		{
			get { return parameter; }
			set { parameter = value; }
		} CodeParameter parameter;

		#endregion

		#region Action members

		/// <summary>
		/// <see cref="IAction.Execute"/>
		/// </summary>
		public override void Execute()
		{
			this.Parameter = this.Method.AddParameter(
					this.ParameterName,
					this.ParameterType,
					this.Position);
		}

		/// <summary>
		/// <see cref="IAction.Undo"/>
		/// </summary>
		public override void Undo()
		{
			this.Method.RemoveParameter(this.Parameter);
		}

		#endregion
	}
}
