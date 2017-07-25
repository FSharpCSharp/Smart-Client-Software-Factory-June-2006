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
using Microsoft.Practices.GuidanceAutomation.SmartClient.Library.Solution;
using EnvDTE;
using System.Runtime.Serialization;
using Microsoft.Practices.GuidanceAutomation.SmartClient.Library;

namespace Microsoft.Practices.GuidanceAutomation.SmartClient.References
{
	[Serializable]
	public class AddEventSubscriptionRecipeReference : UnboundRecipeReference
	{
		public AddEventSubscriptionRecipeReference(string recipe)
			: base(recipe) { }

		public AddEventSubscriptionRecipeReference(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		public override bool IsEnabledFor(object target)
		{
			CodeClass codeClass = null;
			if (ReferenceUtil.HaveAClass(target, out codeClass))
			{
				return !Utility.IsSealedOrStatic(codeClass);
			}

			return false;
		}

		public override string AppliesTo
		{
			get { return "Any non static or sealed C# Class"; }
		}
	}
}
