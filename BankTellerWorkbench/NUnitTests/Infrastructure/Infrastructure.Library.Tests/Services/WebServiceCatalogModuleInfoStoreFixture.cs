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
using System.Reflection;
using GlobalBank.Infrastructure.Library.Services;
using GlobalBank.UnitTest.Library;
using Microsoft.Practices.CompositeUI;
using NUnit.Framework;
using ProfileCatalogServiceImplementation;

namespace GlobalBank.Infrastructure.Library.Tests.Services
{
	[TestFixture]
	public class WebServiceCatalogModuleInfoStoreFixture
	{
		WorkItem mockContainer;

		[SetUp]
		public void Init()
		{
			mockContainer = new TestableRootWorkItem();
			mockContainer.Services.AddNew<MockProfileCatalogService, IProfileCatalogService>();
		}

		[Test]
		[ExpectedException(typeof (ArgumentNullException))]
		public void NullFileThrows()
		{
			WebServiceCatalogModuleInfoStore store =
				mockContainer.Services.AddNew<WebServiceCatalogModuleInfoStore, IModuleInfoStore>();

			store.CatalogUrl = null;
		}

		[Test]
		[ExpectedException(typeof (ArgumentException))]
		public void EmptyStringThrows()
		{
			WebServiceCatalogModuleInfoStore store =
				mockContainer.Services.AddNew<WebServiceCatalogModuleInfoStore, IModuleInfoStore>();

			store.CatalogUrl = "";
		}

		[Test]
		public void DefaultCatalogUrlIsCorrect()
		{
			WebServiceCatalogModuleInfoStore store =
				mockContainer.Services.AddNew<WebServiceCatalogModuleInfoStore, IModuleInfoStore>();

			Assert.AreEqual("http://localhost:54092/profilecatalogservices/profilecatalog.asmx",
			                store.CatalogUrl.ToLowerInvariant());
		}

		[Test]
		public void RequestCatalogContentsFromWebServiceAsString()
		{
			string contents = "<SolutionProfile xmlns=\"http://schemas.microsoft.com/pag/cab-profile/2.0\">"
			                  + "</SolutionProfile>";

			WebServiceCatalogModuleInfoStore store =
				mockContainer.Services.AddNew<WebServiceCatalogModuleInfoStore, IModuleInfoStore>();

			store.Roles = new string[] {"tester"};

			string results = store.GetModuleListXml();

			Assert.AreEqual(contents, results);
		}

		[Test]
		[ExpectedException(typeof (Exception))]
		public void ThrowExWhenWebServiceFails()
		{
			string filename = "http://localhost/unexisting/no.asmx";
			WebServiceCatalogModuleInfoStore store =
				mockContainer.Services.AddNew<WebServiceCatalogModuleInfoStore, IModuleInfoStore>();

			store.CatalogUrl = filename;
			store.Roles = new string[] {"null"};
			string results = store.GetModuleListXml();

			Assert.IsNull(results);
		}
	}

	class MockProfileCatalogService : IProfileCatalogService
	{
		#region IProfileCatalogService Members

		public string GetProfileCatalog(string[] roles)
		{
			if (_url == "http://localhost/unexisting/no.asmx")
				throw new Exception();
			string baseDir = new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath;
			int iFolder = baseDir.LastIndexOf('\\');
			baseDir = baseDir.Substring(0, iFolder);
			ProfileCatalog catalog = new ProfileCatalog(baseDir);
			return catalog.GetProfileCatalog(roles);
			;
		}

		private string _url;

		public string Url
		{
			get { return _url; }
			set { _url = value; }
		}

		#endregion
	}
}