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

using GlobalBank.Infrastructure.Interface.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GlobalBank.Infrastructure.Library.Tests.Commands
{
	[TestClass]
	public class CommandFixture
	{
		[TestMethod]
		public void SetsTimeoutDuringExecutionAndResetsAfterCompleted()
		{
			TestableCommand command = new TestableCommand();

			command.Execute();

			Assert.IsTrue(command.ExecuteCalled);
			Assert.AreEqual(command.OriginalServiceTimeout, command.CurrentServiceTimeout);
			Assert.AreEqual(TestableCommand.CustomTimeoutValue, command.TimeoutDuringExecute);
		}

		private class MockService : ISupportsTimeout
		{
			private int _timeout = -1234;

			public int Timeout
			{
				get { return _timeout; }
				set { _timeout = value; }
			}
		}

		private class TestableCommand : Command<MockService>
		{
			public const int CustomTimeoutValue = 12345;
			public bool ExecuteCalled = false;
			public int OriginalServiceTimeout;
			public int TimeoutDuringExecute = -100;

			public TestableCommand()
				: base(new MockService(), CustomTimeoutValue)
			{
				OriginalServiceTimeout = Service.Timeout;
			}

			public int CurrentServiceTimeout
			{
				get { return Service.Timeout; }
			}

			protected override void DoExecute()
			{
				ExecuteCalled = true;
				TimeoutDuringExecute = Service.Timeout;
			}
		}
	}
}