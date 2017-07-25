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
using System.Windows.Forms;
using EnvDTE;

namespace Microsoft.Practices.GuidanceAutomation.SmartClient.Actions
{
	public class MessageBoxAction : ConfigurableAction
	{
		private object stuff;

		[Input(Required=false)]
		public object Stuff
		{
			get { return stuff; }
			set { stuff = value; }
		}


		public override void Execute()
		{
			DTE dte = GetService<DTE>();
			if (stuff != null)
				MessageBox.Show(String.Format("{0}:{1}", stuff.GetType().FullName, stuff.ToString()));
			else
				MessageBox.Show("The value is null");
		}

		public override void Undo()
		{
		}

	
	}
}
