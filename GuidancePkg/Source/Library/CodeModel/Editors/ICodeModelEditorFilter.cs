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

namespace Microsoft.Practices.GuidanceAutomation.SmartClient.Library.CodeModel.Editors
{
	/// <summary>
	/// Interface implemeted by a custom filter object used in a <see cref="CodeModelEditor"/>
	/// </summary>
	public interface ICodeModelEditorFilter
	{
		/// <summary>
		/// Filters a <see cref="CodeElement"/> object
		/// </summary>
		/// <param name="codeElement"></param>
		/// <returns>Returns True is the object is been filtered out, false if not</returns>
		bool Filter(CodeElement codeElement);
	}
}
