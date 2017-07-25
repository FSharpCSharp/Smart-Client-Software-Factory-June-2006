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

using System.Windows.Forms;
using Microsoft.Practices.CompositeUI.Commands;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.CompositeUI.WinForms;
using GlobalBank.AppraiserWorkbench.AppraisalServiceAgent;
using GlobalBank.AppraiserWorkbench.AppraiserWorkbenchModule;
using GlobalBank.AppraiserWorkbench.AppraiserWorkbenchModule.Constants;
using GlobalBank.AppraiserWorkbench.AppraiserWorkbenchModule.Tests.Mocks;
using GlobalBank.AppraiserWorkbench.AppraiserWorkbenchModule.Views.AppraisalDetail;
using GlobalBank.AppraiserWorkbench.AppraiserWorkbenchModule.Views.MyAppraisals;
using GlobalBank.Infrastructure.Interface.Services;
using NUnit.Framework;
using UnitTest.Library;
using GlobalBank.Infrastructure.Shell.Tests.Mocks;

namespace GlobalBank.AppraiserWorkbench.AppraiserWorkbenchModule.Tests
{
	[TestFixture]
	public class ModuleControllerFixture
	{
		[Test]
		public void ControllerAddsRequiredMenuItemsWhenRun()
		{
			TestableModuleController controller = new TestableModuleController();

			controller.Run();

			ToolStripMenuItem item = controller.MockUIAdapter.FindAddedToolStripItemByText<ToolStripMenuItem>("Appraisals");
			Assert.IsNotNull(item);
			Assert.AreEqual("Available", item.DropDownItems[0].Text);
		}

		[Test]
		public void ControllerAddsRequiredMenuCommandsWhenRun()
		{
			TestableModuleController controller = new TestableModuleController();
			bool viewAvailableWasCalled = false;
			controller.WorkItem.Commands[CommandNames.ViewAvailableAppraisals].ExecuteAction += delegate { viewAvailableWasCalled = true; };

			controller.Run();

			controller.MockUIAdapter.FindAddedToolStripItemByText<ToolStripMenuItem>("Appraisals").DropDownItems[0].PerformClick();
			Assert.IsTrue(viewAvailableWasCalled);
		}

		[Test]
		public void ControllerAddsRequireToolStripItemsWhenRun()
		{
			TestableModuleController controller = new TestableModuleController();

			controller.Run();

			Assert.IsNotNull(controller.MockUIAdapter.FindAddedToolStripItemByText<ToolStripButton>("Available"));
		}

		[Test]
		public void ControllerAddsRequiredToolStripCommandsWhenRun()
		{
			TestableModuleController controller = new TestableModuleController();
			bool viewAvailableWasCalled = false;
			controller.WorkItem.Commands[CommandNames.ViewAvailableAppraisals].ExecuteAction += delegate { viewAvailableWasCalled = true; };

			controller.Run();

			controller.MockUIAdapter.FindAddedToolStripItemByText<ToolStripButton>("Available").PerformClick();
			Assert.IsTrue(viewAvailableWasCalled);
		}

		[Test]
		public void ControllerAddsMyAppraisalsViewToLeftWorkspace()
		{
			TestableModuleController controller = new TestableModuleController();
			IWorkspace ws = controller.WorkItem.Workspaces[WorkspaceNames.LeftWorkspace];

			controller.Run();

			Assert.AreEqual(1, ws.SmartParts.Count);
			Assert.IsTrue(ws.SmartParts[0] is MyAppraisalsView);
		}

		[Test]
		public void ControllerAddsAppraisalDetailViewToRightWorkspace()
		{
			TestableModuleController controller = new TestableModuleController();
			IWorkspace ws = controller.WorkItem.Workspaces[WorkspaceNames.RightWorkspace];

			controller.Run();

			Assert.AreEqual(1, ws.SmartParts.Count);
			Assert.IsTrue(ws.SmartParts[0] is AppraisalDetailView);
		}

		private class TestableModuleController : ModuleController
		{
			public MockUIAdapter MockUIAdapter = new MockUIAdapter();

			public TestableModuleController()
			{
				WorkItem = new TestableRootWorkItem();
				WorkItem.UIExtensionSites.RegisterSite(UIExtensionSiteNames.MainMenu, MockUIAdapter);
				WorkItem.UIExtensionSites.RegisterSite(UIExtensionSiteNames.MainToolBar, MockUIAdapter);
				WorkItem.UIExtensionSites.RegisterSite(UIExtensionSiteNames.MainStatus, MockUIAdapter);
				WorkItem.Workspaces.AddNew<DeckWorkspace>(WorkspaceNames.LeftWorkspace);
				WorkItem.Workspaces.AddNew<DeckWorkspace>(WorkspaceNames.RightWorkspace);
				WorkItem.Services.AddNew<MockFileTransferService, IFileTransferService>();
				WorkItem.Services.AddNew<MockAppraisalManagementServiceAgent, IAppraisalManagementServiceAgent>();
				WorkItem.Services.AddNew<MockMessageBoxService, IMessageBoxService>();

				ICommandAdapterMapService svc = WorkItem.Services.Get<ICommandAdapterMapService>();
				svc.Register(typeof(ToolStripItem), typeof(ToolStripItemCommandAdapter));
			}

			protected override void AddServices()
			{
			}
		}
	}
}
