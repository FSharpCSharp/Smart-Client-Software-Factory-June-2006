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

using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI.UIElements;

namespace GlobalBank.AppraiserWorkbench.AppraiserWorkbenchModule.Tests.Mocks
{
	class MockUIAdapter : IUIElementAdapter
	{
		public List<object> AddedItems = new List<object>();

		public object Add(object uiElement)
		{
			AddedItems.Add(uiElement);
			return uiElement;
		}

		public void Remove(object uiElement)
		{
			AddedItems.Remove(uiElement);
		}

		public TItemType FindAddedToolStripItemByText<TItemType>(string text)
			where TItemType : ToolStripItem
		{
			foreach (object o in AddedItems)
			{
				TItemType ti = o as TItemType;

				if (ti != null && ti.Text == text)
					return ti;
			}

			return null;
		}
	}
}
