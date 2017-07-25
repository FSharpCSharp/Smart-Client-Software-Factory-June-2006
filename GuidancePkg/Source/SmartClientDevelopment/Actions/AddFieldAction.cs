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
using EnvDTE;
using Microsoft.Practices.RecipeFramework.Configuration;
using Microsoft.Practices.RecipeFramework;

namespace Microsoft.Practices.GuidanceAutomation.SmartClient.Actions
{
	public class AddFieldAction : Microsoft.Practices.GuidanceAutomation.SmartClient.Library.CodeModel.Actions.AddFieldAction
	{
		private string _fieldValue;

		[Input()]
		public string FieldValue
		{
			get { return _fieldValue; }
			set { _fieldValue = value; }
		}
	
		public override void Execute()
		{
			foreach (CodeElement element in CodeClass.Members)
			{
				if (element is CodeVariable && element.Name == FieldName) return;
			}
			base.Execute();
			if (_fieldValue != null)
			{
				Field.IsConstant = true;
				EditPoint ep = Field.EndPoint.CreateEditPoint();
				ep.CharLeft(1);
				ep.Insert(String.Format(" = \"{0}\"", _fieldValue));
			}
		}
	}
}
