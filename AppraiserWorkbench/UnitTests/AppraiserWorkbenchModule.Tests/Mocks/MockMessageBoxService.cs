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
using GlobalBank.Infrastructure.Interface.Services;

namespace GlobalBank.AppraiserWorkbench.AppraiserWorkbenchModule.Tests.Mocks
{
	class MockMessageBoxService : IMessageBoxService
	{
		#region IMessageBoxService Members

		public System.Windows.Forms.DialogResult Show(string text)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public System.Windows.Forms.DialogResult Show(string text, string caption)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public System.Windows.Forms.DialogResult Show(string text, string caption, System.Windows.Forms.MessageBoxButtons buttons)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public System.Windows.Forms.DialogResult Show(string text, string caption, System.Windows.Forms.MessageBoxButtons buttons, System.Windows.Forms.MessageBoxIcon icon)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		#endregion
	}
}
