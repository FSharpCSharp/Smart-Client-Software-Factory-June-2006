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

using Microsoft.Practices.CompositeUI;
using System;
using Microsoft.Practices.CompositeUI.SmartParts;

namespace GlobalBank.Infrastructure.Interface
{
	public abstract class Presenter<TView> : IDisposable
	{
		private TView _view;
		private WorkItem _workItem;

		public TView View
		{
			get { return _view; }
			set { _view = value; OnViewSet(); }
		}

		[ServiceDependency]
		public WorkItem WorkItem
		{
			get { return _workItem; }
			set { _workItem = value; }
		}

		protected virtual void CloseView()
		{
			IWorkspaceLocatorService locator = WorkItem.Services.Get<IWorkspaceLocatorService>();
			IWorkspace wks = locator.FindContainingWorkspace(WorkItem, View);
			if (wks != null)
				wks.Close(View);
		}

		public virtual void OnViewReady() { }
		protected virtual void OnViewSet() { }

		~Presenter()
		{
			Dispose(false);
		}

		/// <summary>
		/// See <see cref="System.IDisposable.Dispose"/> for more information.
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Called when the object is being disposed or finalized.
		/// </summary>
		/// <param name="disposing">True when the object is being disposed (and therefore can
		/// access managed members); false when the object is being finalized without first
		/// having been disposed (and therefore can only touch unmanaged members).</param>
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (_workItem != null)
					_workItem.Items.Remove(this);
			}
		}
	}
}

