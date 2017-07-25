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

//----------------------------------------------------------------------------------------
// patterns & practices - Smart Client Software Factory - Guidance Package
//
// This file was generated by this guidance package as part of the solution template
//
// The ControlledWorkItem represents a WorkItem that is managed by another class. 
// The other class is the controller and contains the business logic 
// (the WorkItem provides the container.)
// 
// For more information see: 
// ms-help://MS.VSCC.v80/MS.VSIPCC.v80/ms.scsf.2006jun/SCSF/html/03-210-Creating%20a%20Smart%20Client%20Solution.htm
//
// Latest version of this Guidance Package: http://go.microsoft.com/fwlink/?LinkId=62182
//----------------------------------------------------------------------------------------

using Microsoft.Practices.CompositeUI;

namespace GlobalBank.Infrastructure.Interface
{
	/// <summary>
	/// Represents a WorkItem that uses a WorkItem controller to perform its business logic.
	/// </summary>
	/// <typeparam name="TController"></typeparam>
	public sealed class ControlledWorkItem<TController> : WorkItem
	{
		private TController _controller;

		/// <summary>
		/// Gets the controller.
		/// </summary>
		public TController Controller
		{
			get { return _controller; }
		}

		/// <summary>
		/// See <see cref="M:Microsoft.Practices.ObjectBuilder.IBuilderAware.OnBuiltUp(System.String)"/> for more information.
		/// </summary>
		public override void OnBuiltUp(string id)
		{
			base.OnBuiltUp(id);

			_controller = Items.AddNew<TController>();
		}
	}
}
