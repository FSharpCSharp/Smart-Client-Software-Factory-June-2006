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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace GlobalBank.Infrastructure.Shell
{
	public partial class ShowException : Form
	{
		private Exception _exception;
		public ShowException(Exception ex)
		{
			_exception = ex;
			InitializeComponent();
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			StringBuilder sb = new StringBuilder();
			StringWriter writer = new StringWriter(sb);
			TextExceptionFormatter formatter = new TextExceptionFormatter(writer, _exception);
			formatter.Format();
			txtException.Text = sb.ToString();
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}