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
using System.Drawing.Design;
using System.Windows.Forms.Design;
using Microsoft.VisualStudio.Shell.Interop;
using System.ComponentModel;
using Microsoft.VisualStudio;
using Microsoft.Practices.GuidanceAutomation.SmartClient.Library;

namespace Microsoft.Practices.GuidanceAutomation.SmartClient.Editors
{
	public class WebServiceUrlEditor : UITypeEditor
	{
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.Modal;
		}

		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			Guard.ArgumentNotNull(provider, "provider");
			IVsAddWebReferenceDlg2 dlg = provider.GetService(typeof(IVsAddWebReferenceDlg)) as IVsAddWebReferenceDlg2;
			System.Diagnostics.Trace.Assert(dlg != null);
			string serviceURL = null;
			string referenceName = null;
			IDiscoveryResult discoveryResult = null;
			int fCanceled = 0;

			int hResult = dlg.AddWebReferenceDlg(null, out serviceURL, out referenceName, out discoveryResult, out fCanceled);
			if ((dlg != null) && ErrorHandler.Succeeded(hResult) && (fCanceled == 0))
			{
				return serviceURL;
			}
			else
			{
				if (value != null)
				{
					return value.ToString();
				}
				else
				{
					return string.Empty;
				}
			}
		}
	}
}
