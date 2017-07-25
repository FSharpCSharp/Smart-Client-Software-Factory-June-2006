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
using System.IO;
using System.Text;
using Microsoft.Practices.RecipeFramework;
using Microsoft.Practices.RecipeFramework.Services;

namespace Microsoft.Practices.GuidanceAutomation.SmartClient.ValueProviders
{
	public class FolderProvider: ValueProvider
	{
		private const int SEARCHLEVEL = 4;
		
		public override bool OnBeginRecipe(object currentValue, out object newValue)
		{
			if ( string.IsNullOrEmpty(currentValue as string) )
			{
				string basePath = GetService<IConfigurationService>(true).BasePath;
				string libPath = @"Lib";
				string probePath = Path.Combine(basePath, libPath);
				int i = 0;
				while (i++ < SEARCHLEVEL && !Directory.Exists(probePath))
				{
					libPath = @"..\" + libPath;
					probePath = Path.Combine(basePath, libPath);
				}
				if ( Directory.Exists(probePath) )
				{
					DirectoryInfo info = new DirectoryInfo(probePath);
					newValue = info.FullName;
					return true;
				}
			}
			return base.OnBeginRecipe(currentValue, out newValue);
		}
	}
}
