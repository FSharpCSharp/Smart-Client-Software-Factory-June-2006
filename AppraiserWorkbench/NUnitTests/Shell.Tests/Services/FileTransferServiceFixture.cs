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

using System.IO;
using System.Threading;
using GlobalBank.Infrastructure.Interface.Services;
using NUnit.Framework;
using System;
using GlobalBank.Infrastructure.Shell;
using GlobalBank.Infrastructure.Shell.Services;

namespace GlobalBank.Infrastructure.Shell.Tests.Services
{
	[TestFixture]
	public class FileTransferServiceFixture
	{
		[Test]
		public void DownloadsFromUrlInBackground()
		{
			AutoResetEvent finishedEvent = new AutoResetEvent(false);
			string textInFile = "This is test text";
			string originalFile = Path.GetTempFileName();
			string downloadedFile = Path.GetTempFileName();

			try
			{
				File.WriteAllText(originalFile, textInFile);

				Uri downloadUrl = new Uri("file:///" + originalFile.Replace('\\', '/').Replace(" ", "%20"));

				FileTransferService svc = new FileTransferService();

				Uri delegateUrl = null;
				bool delegateSuccess = false;

				svc.BeginDownload(downloadUrl, downloadedFile, delegate(Uri url, bool success)
				{
					delegateUrl = downloadUrl;
					delegateSuccess = success;
					finishedEvent.Set();
				});

				bool signaledEvent = finishedEvent.WaitOne(10000, false);

				Assert.IsTrue(signaledEvent);
				Assert.IsTrue(delegateSuccess);
				Assert.AreEqual(downloadUrl, delegateUrl);
				Assert.AreEqual(textInFile, File.ReadAllText(downloadedFile));
			}
			finally
			{
				File.Delete(originalFile);
				File.Delete(downloadedFile);
			}
		}

		[Test]
		public void UploadWithARealUrl()
		{
			AutoResetEvent finishedEvent = new AutoResetEvent(false);
			string textInFile = "This is test text";
			string originalFile = Path.GetTempFileName();
			string uploadedFile = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

			try
			{
				File.WriteAllText(originalFile, textInFile);

				Uri uploadUrl = new Uri("file:///" + uploadedFile.Replace('\\', '/').Replace(" ", "%20"));

				FileTransferService svc = new FileTransferService();

				Uri delegateUrl = null;
				bool delegateSuccess = false;

				svc.BeginUpload(uploadUrl, originalFile, delegate(Uri url, bool success)
				{
					delegateUrl = uploadUrl;
					delegateSuccess = success;
					finishedEvent.Set();
				});

				bool signaledEvent = finishedEvent.WaitOne(10000, false);

				Assert.IsTrue(signaledEvent);
				Assert.IsTrue(delegateSuccess);
				Assert.AreEqual(uploadUrl, delegateUrl);
			}
			finally
			{
				File.Delete(originalFile);
				File.Delete(uploadedFile);
			}
		}

		//[Test]
		//public void DownloadWithARealUrl()
		//{
		//   AutoResetEvent finishedEvent = new AutoResetEvent(false);
		//   string downloadedFile = Path.GetTempFileName();

		//   try
		//   {
		//      FileTransferService svc = new FileTransferService();

		//      bool delegateSuccess = false;

		//      svc.BeginDownload("http://www.microsoft.com/", downloadedFile, delegate(string url, bool success)
		//      {
		//         delegateSuccess = success;
		//         finishedEvent.Set();
		//      });

		//      bool signaledEvent = finishedEvent.WaitOne(30000, false);

		//      Assert.IsTrue(signaledEvent);
		//      Assert.IsTrue(delegateSuccess);
		//   }
		//   finally
		//   {
		//      File.Delete(downloadedFile);
		//   }
		//}

		//[Test]
		//public void UploadWithARealUrl()
		//{
		//   AutoResetEvent finishedEvent = new AutoResetEvent(false);
		//   string textInFile = "This is test text";
		//   string originalFile = Path.GetTempFileName();

		//   try
		//   {
		//      File.WriteAllText(originalFile, textInFile);

		//      string uploadUrl = "http://localhost:1428/AppraisalService/Document.ashx?attachmentId=1";

		//      FileTransferService svc = new FileTransferService();

		//      string delegateUrl = null;
		//      bool delegateSuccess = false;

		//      svc.BeginUpload(uploadUrl, originalFile, delegate(string url, bool success)
		//      {
		//         delegateUrl = uploadUrl;
		//         delegateSuccess = success;
		//         finishedEvent.Set();
		//      });

		//      bool signaledEvent = finishedEvent.WaitOne(10000, false);

		//      Assert.IsTrue(signaledEvent);
		//      Assert.IsTrue(delegateSuccess);
		//      Assert.AreEqual(uploadUrl, delegateUrl);
		//   }
		//   finally
		//   {
		//      File.Delete(originalFile);
		//   }
		//}
	}
}
