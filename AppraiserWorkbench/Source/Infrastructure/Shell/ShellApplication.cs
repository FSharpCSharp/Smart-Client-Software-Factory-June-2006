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
using System.IO;
using System.Text;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.WinForms;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using GlobalBank.Infrastructure.Interface;
using GlobalBank.Infrastructure.Interface.Constants;
using GlobalBank.Infrastructure.Interface.Services;
using System.Deployment.Application;
using System.Threading;
using GlobalBank.Infrastructure.Shell.Services;

namespace GlobalBank.Infrastructure.Shell
{
	/// <summary>
	/// Main application entry point class.
	/// </summary>
	public class ShellApplication : FormShellApplication<WorkItem, ShellForm>
	{
		private const int _checkForUpdatesInterval = 30000;

		/// <summary>
		/// Application entry point.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.ThreadException += new ThreadExceptionEventHandler(ApplicationThreadException);
			Application.SetCompatibleTextRenderingDefault(false);

			try
			{
				new ShellApplication().Run();
			}
			catch (Exception ex)
			{
				HandleException(ex);
			}
		}

		protected override void Start()
		{
			InitializeApplicationUpdatesDetection();
			base.Start();
		}

		private void InitializeApplicationUpdatesDetection()
		{
			if (ApplicationDeployment.IsNetworkDeployed)
			{
				Thread checkThread = new Thread(new ThreadStart(CheckForUpdates));
				checkThread.IsBackground = true;
				checkThread.Name = "Check for updates";
				checkThread.Start();
			}
		}

		private void CheckForUpdates()
		{
			while (true)
			{
				try
				{
					Thread.Sleep(_checkForUpdatesInterval);
					UpdateCheckInfo info = ApplicationDeployment.CurrentDeployment.CheckForDetailedUpdate();

					if (info.UpdateAvailable)
					{
						Shell.UpdateAvailable(info);
						break;
					}
				}
				catch (InvalidOperationException)
				{
				}
			}
		}

		/// <summary>
		/// See <see cref="M:Microsoft.Practices.CompositeUI.CabApplication`1.AddServices"/>
		/// </summary>
		protected override void AddServices()
		{
			base.AddServices();

			RootWorkItem.Services.AddNew<FileTransferService, IFileTransferService>();
			RootWorkItem.Services.AddNew<FileWatcherService, IFileWatcherService>();
			RootWorkItem.Services.AddNew<WorkspaceLocatorService, IWorkspaceLocatorService>();
		}

		/// <summary>
		/// Sets the extension site registration after the shell been created.
		/// </summary>
		protected override void AfterShellCreated()
		{
			base.AfterShellCreated();

			RootWorkItem.Services.Add<IMessageBoxService>(this.Shell.MessageBoxService);
			RootWorkItem.UIExtensionSites.RegisterSite(UIExtensionSiteNames.MainMenu, this.Shell.MainMenuStrip);
			RootWorkItem.UIExtensionSites.RegisterSite(UIExtensionSiteNames.MainStatus, this.Shell.MainStatusStrip);
			RootWorkItem.UIExtensionSites.RegisterSite(UIExtensionSiteNames.MainToolBar, this.Shell.MainToolBarStrip);
		}

		public override void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			if (e != null)
				HandleException(e.ExceptionObject as Exception);
		}

		private static void ApplicationThreadException(object sender, ThreadExceptionEventArgs e)
		{
			HandleException(e.Exception);
		}

		private static void HandleException(Exception ex)
		{
			if (ex == null)
				return;

			ExceptionPolicy.HandleException(ex, "Default Policy");

#if DEBUG
			ShowException se = new ShowException(ex);
			se.ShowDialog();
#else
			MessageBox.Show(Properties.Resource.ExceptionThrown, Properties.Resource.ApplicationTitle,
				MessageBoxButtons.OK, MessageBoxIcon.Error);
#endif

			Application.Exit();
		}
	}
}
