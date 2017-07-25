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
using GlobalBank.Infrastructure.Interface.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GlobalBank.Infrastructure.Library.Tests.Commands
{
	[TestClass]
	public class CommandQueueFixture
	{
		[TestMethod]
		public void RunningCausesCommandToExecute()
		{
			CommandQueue queue = new CommandQueue();
			MockCommand command = new MockCommand();

			queue.Add(command);
			queue.Run();

			Assert.IsTrue(command.WasExecuted);
		}

		[TestMethod]
		public void RunningCausesAllCommandsToExecute()
		{
			CommandQueue queue = new CommandQueue();
			MockCommand command1 = new MockCommand();
			MockCommand command2 = new MockCommand();

			queue.Add(command1);
			queue.Add(command2);
			queue.Run();

			Assert.IsTrue(command1.WasExecuted);
			Assert.IsTrue(command2.WasExecuted);
		}

		[TestMethod]
		public void CommandsAreRunnedInOrder()
		{
			CommandQueue queue = new CommandQueue();
			MockCommand command1 = new MockCommand();
			MockCommand command3 = new MockCommand();
			DependantMockCommand command2 = new DependantMockCommand(command1, command3);

			queue.Add(command1);
			queue.Add(command2);
			queue.Add(command3);
			queue.Run();

			Assert.IsTrue(command1.WasExecuted);
			Assert.IsTrue(command2.WasExecuted);
			Assert.IsTrue(command3.WasExecuted);
		}

		[TestMethod]
		public void CommandQueueTriggersWaitHandleWhenItemsAreAdded()
		{
			CommandQueue queue = new CommandQueue();

			Assert.IsFalse(queue.NewCommandWaitHandle.WaitOne(100, false));

			queue.Add(new MockCommand());

			Assert.IsTrue(queue.NewCommandWaitHandle.WaitOne(100, false));
		}

		[TestMethod]
		public void CommandIsDisposedAfterItIsExecuted()
		{
			CommandQueue queue = new CommandQueue();
			DisposableCommand command = new DisposableCommand();
			queue.Add(command);

			queue.Run();

			Assert.IsTrue(command.WasExecuted);
			Assert.IsTrue(command.WasDisposed);
		}

		[TestMethod]
		public void CommandsAreDisposedWhenQueueIsDisposed()
		{
			CommandQueue queue = new CommandQueue();
			DisposableCommand command = new DisposableCommand();
			queue.Add(command);

			queue.Dispose();

			Assert.IsTrue(command.WasDisposed);
		}
	}

	#region Helpers

	public class MockCommand : ICommand
	{
		public bool WasExecuted = false;

		public virtual void Execute()
		{
			WasExecuted = true;
		}
	}

	public class DependantMockCommand : MockCommand
	{
		MockCommand _prevCommand;
		MockCommand _nextCommand;

		public DependantMockCommand(MockCommand prevCommand, MockCommand nextCommand)
		{
			_prevCommand = prevCommand;
			_nextCommand = nextCommand;
		}

		public override void Execute()
		{
			Assert.IsTrue(_prevCommand.WasExecuted);
			Assert.IsFalse(_nextCommand.WasExecuted);
			base.Execute();
		}
	}

	public class DisposableCommand : MockCommand, IDisposable
	{
		public bool WasDisposed = false;

		public void Dispose()
		{
			WasDisposed = true;
		}
	}

	#endregion
}