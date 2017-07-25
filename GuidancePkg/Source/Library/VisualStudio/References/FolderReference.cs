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
using System.Runtime.Serialization;
using EnvDTE;
using Microsoft.Practices.Common;
using Microsoft.Practices.RecipeFramework;

namespace Microsoft.Practices.GuidanceAutomation.SmartClient.Library.VisualStudio.References
{
	/// <summary>
	/// UnBoundRecipe that allows to be executed only on Solution Folders
	/// </summary>
	[Serializable]
	public class FolderReference : UnboundRecipeReference, IAttributesConfigurable
	{
		private string _folderName;

		/// <summary>
		/// Constructor of the FolderReference that must specify the 
		/// recipe name that will be used by the reference
		/// </summary>
		/// <param name="recipe"></param>
		public FolderReference(string recipe)
			: base(recipe)
		{
		}

		/// <summary>
		/// Returns a friendly name as Any Folder
		/// </summary>
		public override string AppliesTo
		{
			get { return "Any Folder"; }
		}

		/// <summary>
		/// Performs the validation of the item passed as target
		/// Returns true if the reference is allowed to be executed in the target
		/// that is if the target is a solution folder
		/// </summary>
		/// <param name="target"></param>
		/// <returns></returns>
		public override bool IsEnabledFor(object target)
		{
			Project folder = target as Project;
			if (folder != null)
			{
				if (!string.IsNullOrEmpty(_folderName))
				{
					if (folder.Name == _folderName)
					{
						return true;
					}
				}
				else
				{
					return true;
				}
			}
			return false;
		}

		#region ISerializable Members

		/// <summary>
		/// Required constructor for deserialization.
		/// </summary>
		protected FolderReference(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		#endregion ISerializable Members

		#region IAttributesConfigurable Members

		/// <summary>
		/// Stores the set of user defined attributes
		/// </summary>
		/// <param name="attributes"></param>
		public void Configure(System.Collections.Specialized.StringDictionary attributes)
		{
			if (attributes.ContainsKey("FolderName"))
			{
				_folderName = attributes["FolderName"];
			}
		}

		#endregion
	}
}
