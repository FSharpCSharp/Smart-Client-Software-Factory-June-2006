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

namespace Microsoft.Practices.GuidanceAutomation.SmartClient.Actions
{
	public class InitializeFieldAction : ConfigurableAction
	{
		private CodeVariable _field;
		private string _fieldValue = String.Empty;

		[Input(Required=true)]
		public CodeVariable Field
		{
			get { return _field; }
			set { _field = value; }
		}


		[Input()]
		public string FieldValue
		{
			get { return _fieldValue; }
			set { _fieldValue = value; }
		}
	
	
		public override void Execute()
		{
			_field.IsConstant = true;
			EditPoint ep = _field.EndPoint.CreateEditPoint();
			ep.CharLeft(1);
			ep.Insert(String.Format(" = \"{0}\"", _fieldValue));
		}

		public override void Undo()
		{
		}
	}
}
