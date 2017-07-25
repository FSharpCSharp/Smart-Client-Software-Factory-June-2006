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
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using Microsoft.Practices.CompositeUI.EventBroker;
using GlobalBank.Infrastructure.Interface;
using GlobalBank.AppraiserWorkbench.AppraisalServiceAgent;
using GlobalBank.AppraiserWorkbench.AppraiserWorkbenchModule.Constants;
using GlobalBank.AppraiserWorkbench.AppraiserWorkbenchModule.Views.AppraisalDetail;
using GlobalBank.AppraiserWorkbench.AppraiserWorkbenchModule.Views.AvailableAppraisals;
using GlobalBank.AppraiserWorkbench.AppraiserWorkbenchModule.Views.MyAppraisals;

namespace GlobalBank.AppraiserWorkbench.AppraiserWorkbenchModule
{
	public class ModuleController : WorkItemController, IDisposable
	{
		private ToolStripStatusLabel _offlineNotificationLabel;

		[EventPublication(EventTopicNames.MyAppraisalsUpdated, PublicationScope.Global)]
		public event EventHandler MyAppraisalsUpdated;

		public void Run()
		{
			AddServices();
			ExtendMenu();
			ExtendToolStrip();
			ExtendStatusStrip();
			AddViews();
		}

		protected virtual void AddServices()
		{
			AppraisalManagementServiceAgent agent = WorkItem.Services.AddNew<AppraisalManagementServiceAgent, IAppraisalManagementServiceAgent>();
			agent.ServiceOperationNotification += OnServiceOperationNotification;
			agent.MyAppraisalsUpdated += OnMyAppraisalsUpdated;
		}

		[CommandHandler(CommandNames.ViewAvailableAppraisals)]
		public void ViewAvailableAppraisalsHandler(object sender, EventArgs e)
		{
			using (AvailableAppraisalsView view = WorkItem.Items.AddNew<AvailableAppraisalsView>())
			{
				view.ShowDialog();
				WorkItem.Items.Remove(view);
			}
		}

		private void AddViews()
		{
			MyAppraisalsView myView = WorkItem.Items.AddNew<MyAppraisalsView>();
			WorkItem.Workspaces[WorkspaceNames.LeftWorkspace].Show(myView);

			AppraisalDetailView detailView = WorkItem.Items.AddNew<AppraisalDetailView>();
			WorkItem.Workspaces[WorkspaceNames.RightWorkspace].Show(detailView);
		}

		private void ExtendMenu()
		{
			ToolStripMenuItem appraisalsMenuItem = new ToolStripMenuItem(Properties.Resources.Appraisals);
			WorkItem.UIExtensionSites[UIExtensionSiteNames.MainMenu].Add(appraisalsMenuItem);

			AddMenuItem(appraisalsMenuItem, Properties.Resources.ViewAvailableAppraisals, CommandNames.ViewAvailableAppraisals, 0);
		}

		private void AddMenuItem(ToolStripMenuItem parent, string text, string command, Keys shortcutKeys)
		{
			ToolStripMenuItem item = new ToolStripMenuItem(text);
			item.ShortcutKeys = shortcutKeys;
			parent.DropDownItems.Add(item);

			WorkItem.Commands[command].AddInvoker(item, "Click");
		}

		private void ExtendToolStrip()
		{
			UIExtensionSite toolstrip = WorkItem.UIExtensionSites[UIExtensionSiteNames.MainToolBar];
			AddToolStripButton(toolstrip, Properties.Resources.ViewAvailableAppraisals, CommandNames.ViewAvailableAppraisals);
		}

		private void AddToolStripButton(UIExtensionSite toolstrip, string text, string command)
		{
			ToolStripButton button = new ToolStripButton(text);
			toolstrip.Add(button);
			WorkItem.Commands[command].AddInvoker(button, "Click");
		}

		private void ExtendStatusStrip()
		{
			UIExtensionSite status = WorkItem.UIExtensionSites[UIExtensionSiteNames.MainStatus];
			_offlineNotificationLabel = new ToolStripStatusLabel();
			_offlineNotificationLabel.BorderStyle = Border3DStyle.Sunken;
			_offlineNotificationLabel.Spring = true;
			_offlineNotificationLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			status.Add(_offlineNotificationLabel);
		}

		private delegate void UpdateNotificationLabelsDelegate(string text);

		private void UpdateNotificationLabels(string text)
		{
			_offlineNotificationLabel.Text = text;
		}

		private void OnServiceOperationNotification(object sender, EventArgs<bool> args)
		{
			if (_offlineNotificationLabel != null && _offlineNotificationLabel.IsDisposed == false)
			{
				string text = (args.Data) ? string.Empty : Properties.Resources.ServiceNotAvailable;
				if (_offlineNotificationLabel.Owner.InvokeRequired)
					_offlineNotificationLabel.Owner.BeginInvoke(new UpdateNotificationLabelsDelegate(UpdateNotificationLabels), text);
				else
					UpdateNotificationLabels(text);
			}
		}

		private void OnMyAppraisalsUpdated(object sender, EventArgs args)
		{
			if (MyAppraisalsUpdated != null)
				MyAppraisalsUpdated(sender, args);
		}


		~ModuleController()
		{
			Dispose(false);
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				_offlineNotificationLabel.Dispose();
			}
		}
	}
}
