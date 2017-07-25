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
using EnvDTE80;
using Microsoft.Practices.RecipeFramework;

namespace Microsoft.Practices.GuidanceAutomation.SmartClient.Library.CodeModel.Actions
{
	/// <summary>
	/// Adds an event to a <see cref="CodeClass2"/> object
	/// </summary>
	public sealed class AddEventAction : ConfigurableAction
	{
		private CodeClass2 _codeClass;
		private string _eventName;
		private string _delegateName;
		private bool _createProperyStyle = false;
		private CodeEvent _codeEvent;
		private vsCMAccess _access = vsCMAccess.vsCMAccessPublic;
		private object _position = 0;

		#region Input Properties

		/// <summary>
		/// The class where the event will be acced
		/// </summary>
		[Input(Required = true)]
		public CodeClass2 CodeClass
		{
			get { return _codeClass; }
			set { _codeClass = value; }
		}

		/// <summary>
		/// The name of the event been added
		/// </summary>
		[Input(Required = true)]
		public string EventName
		{
			get { return _eventName; }
			set { _eventName = value; }
		}

		/// <summary>
		/// The delate for the new event
		/// </summary>
		[Input(Required = true)]
		public string DelegateName
		{
			get { return _delegateName; }
			set { _delegateName = value; }
		}

		/// <summary>
		/// Creates the event using a property
		/// </summary>
		[Input(Required = false)]
		public bool CreatePropertyStyle
		{
			get { return _createProperyStyle; }
			set { _createProperyStyle = value; }
		}

		/// <summary>
		/// The position in the class where the member is been added
		/// </summary>
		[Input(Required = false)]
		public object Position
		{
			get { return _position; }
			set { _position = value; }
		} 

		/// <summary>
		/// The kind of visibility of the new variable
		/// </summary>
		/// <seealso cref="vsCMAccess"/>
		[Input(Required = false)]
		public vsCMAccess Access
		{
			get { return _access; }
			set { _access = value; }
		} 

		#endregion

		#region Output properties


		/// <summary>
		/// The created event
		/// </summary>
		[Output]
		public CodeEvent Event
		{
			get { return _codeEvent; }
			set { _codeEvent = value; }
		}


		#endregion

		#region Overrides

		/// <summary>
		/// Adds the event
		/// </summary>
		public override void Execute()
		{
			this.Event = this.CodeClass.AddEvent(this.EventName,
					this.DelegateName, this.CreatePropertyStyle,
					this.Position, this.Access);
		}

		/// <summary>
		/// Removes the event
		/// </summary>
		public override void Undo()
		{
			this.CodeClass.RemoveMember(this.Event);
			this.Event = null;
		}

		#endregion
	}
}
