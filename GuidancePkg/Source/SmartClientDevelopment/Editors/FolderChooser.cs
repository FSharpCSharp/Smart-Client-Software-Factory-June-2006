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
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;

namespace Microsoft.Practices.GuidanceAutomation.SmartClient.Editors
{
	/// <summary>
	/// Editor that will choose a file when it is used
	/// It has properties that can be overrided 
	/// to use different filter, title and initialdirectory
	/// </summary>
	public class FolderChooser : UITypeEditor
	{
		public string SelectedPath
		{
			get { return "C:\\"; }
		}

		public string Description
		{
			get { return "Please choose a folder"; }
		}

		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.Modal;
		}

		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
			folderBrowser.Description = Description;
			folderBrowser.SelectedPath = SelectedPath;

			if (folderBrowser.ShowDialog() == DialogResult.OK)
			{
				return folderBrowser.SelectedPath;
			}
			else
			{
				return string.Empty;
			}
		}
	}
}
