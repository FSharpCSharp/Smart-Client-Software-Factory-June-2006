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
	public class CommandWithCallbackFixture
	{
		[TestMethod]
		public void CallsCallbackWithValueAndTrueWhenExceptionNotThrown()
		{
			bool delegateCalled = false;
			bool returnedSuccessValue = false;
			int returnedReturnValue = 0;

			TestableCommand command = new TestableCommand(delegate(bool success, int returnValue)
			                                              	{
			                                              		delegateCalled = true;
			                                              		returnedSuccessValue = success;
			                                              		returnedReturnValue = returnValue;
			                                              	});

			command.Execute();

			Assert.IsTrue(delegateCalled);
			Assert.IsTrue(returnedSuccessValue);
			Assert.AreEqual(TestableCommand.IntendedReturnValue, returnedReturnValue);
		}

		[TestMethod]
		public void CallsCallbackWithFalseWhenExceptionThrown()
		{
			bool delegateCalled = false;
			bool returnedSuccessValue = false;

			TestableCommand command = new TestableCommand(delegate(bool success, int returnValue)
			                                              	{
			                                              		delegateCalled = true;
			                                              		returnedSuccessValue = success;
			                                              	});

			command.ThrowException = true;
			command.Execute();

			Assert.IsTrue(delegateCalled);
			Assert.IsFalse(returnedSuccessValue);
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

		private class TestableCommand : CommandWithCallback<MockService, int>
		{
			public const int IntendedReturnValue = 17534;
			public bool ThrowException = false;

			public TestableCommand(CallbackType callback)
				: base(new MockService(), 0, callback)
			{
			}

			protected override int DoCallService()
			{
				if (ThrowException)
					throw new Exception();

				return IntendedReturnValue;
			}
		}
	}
}