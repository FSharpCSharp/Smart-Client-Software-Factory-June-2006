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
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI.EventBroker;
using GlobalBank.Infrastructure.Interface.Constants;
using System.Deployment.Application;
using GlobalBank.Infrastructure.Interface.Services;
using Microsoft.Practices.CompositeUI;
using GlobalBank.Infrastructure.Interface;
using GlobalBank.Infrastructure.Shell.Services;

namespace GlobalBank.Infrastructure.Shell
{
	/// <summary>
	/// Main application shell view.
	/// </summary>
	public partial class ShellForm : Form
	{
		IMessageBoxService _messageBoxService;

		/// <summary>
		/// Default class initializer.
		/// </summary>
		public ShellForm()
		{
			InitializeComponent();
			_messageBoxService = new ShellMessageBoxService(this);
			_leftWorkspace.Name = WorkspaceNames.LeftWorkspace;
			_rightWorkspace.Name = WorkspaceNames.RightWorkspace;
		}

		/// <summary>
		/// Gets a reference for the application statusbar (<see cref="StatusStrip"/>) object.
		/// </summary>
		internal StatusStrip MainStatusStrip
		{
			get { return _mainStatusStrip; }
		}

		/// <summary>
		/// Gets a reference for the default application toolbar (<see cref="ToolStrip"/>) object.
		/// </summary>
		internal ToolStrip MainToolBarStrip
		{
			get { return _mainToolStrip; }
		}

		/// <summary>
		/// Status update handler. Updates the status strip on the main form.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
		[EventSubscription(EventTopicNames.StatusUpdate, ThreadOption.UserInterface)]
		public void StatusUpdateHandler(object sender, EventArgs<string> e)
		{
			_statusLabel.Text = e.Data;
		}

		/// <summary>
		/// Close the application.
		/// </summary>
		private void OnFileExit(object sender, EventArgs e)
		{
			Close();
		}

		delegate void UpdateAvailableDelegate(UpdateCheckInfo info);

		public IMessageBoxService MessageBoxService
		{
			get { return _messageBoxService; }
		}

		internal void UpdateAvailable(UpdateCheckInfo info)
		{
			if (InvokeRequired)
			{
				BeginInvoke(new UpdateAvailableDelegate(UpdateAvailable), info);
			}
			else
			{
				_messageBoxService.Show(Properties.Resource.ApplicationUpdateDetected,
					Properties.Resource.ApplicationUpdateTitle);
			}
		}
	}
}
