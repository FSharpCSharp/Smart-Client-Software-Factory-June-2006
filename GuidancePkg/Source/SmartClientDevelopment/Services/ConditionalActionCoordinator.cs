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
using Microsoft.Practices.ComponentModel;
using Microsoft.Practices.RecipeFramework.Services;
using Microsoft.Practices.RecipeFramework.Configuration;
using System.Xml;
using System.ComponentModel.Design;
using Microsoft.Practices.GuidanceAutomation.SmartClient.Library;

namespace Microsoft.Practices.GuidanceAutomation.SmartClient.Services
{
	public class ConditionalActionCoordinator : SitedComponent, IActionCoordinationService
	{
		private const string CONDITIONAL_ATT_NAME = "Execute";

		public void Run(Dictionary<string, Action> declaredActions, XmlElement coordinationData)
		{
			IActionExecutionService exec = GetService<IActionExecutionService>(true);

			foreach (Action action in declaredActions.Values)
			{
				IDictionaryService dictservice = (IDictionaryService)GetService(typeof(IDictionaryService));
				ExpressionEvaluationService evaluator = new ExpressionEvaluationService();

				if (action.AnyAttr == null || action.AnyAttr.Length == 0)
				{
					exec.Execute(action.Name);
				}
				else
				{
					bool execute = true;

					foreach (XmlAttribute att in action.AnyAttr)
					{
						if (att.Name == CONDITIONAL_ATT_NAME)
						{
							execute = (bool)evaluator.Evaluate(att.Value,
								new ServiceAdapterDictionary(dictservice));
							break;
						}
					}

					if (execute)
					{
						exec.Execute(action.Name);
					}
				}
			}
		}
	}
}
