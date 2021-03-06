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
using Microsoft.Practices.CompositeUI.UIElements;

namespace GlobalBank.Infrastructure.UI
{
	public class LinkLabelPanelUIAdapterFactory : IUIElementAdapterFactory
	{
		public IUIElementAdapter GetAdapter(object uiElement)
		{
			if (uiElement is LinkLabelPanel) return new LinkLabelPanelUIAdapter((LinkLabelPanel)uiElement);

			throw new ArgumentException("uiElement");
		}

		public bool Supports(object uiElement)
		{
			return uiElement is LinkLabelPanel;
		}
	}
}
