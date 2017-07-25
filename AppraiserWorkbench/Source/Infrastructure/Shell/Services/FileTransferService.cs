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
using System.Net;
using System.Threading;
using Microsoft.Practices.CompositeUI.Utility;
using GlobalBank.Infrastructure.Interface.Services;

namespace GlobalBank.Infrastructure.Shell.Services
{
	public class FileTransferService : IFileTransferService
	{

		public void BeginDownload(Uri url, string filename, FileTransferFinishedDelegate callback)
		{
			Guard.ArgumentNotNull(url, "url");
			Guard.ArgumentNotNullOrEmptyString(filename, "filename");
			Guard.ArgumentNotNull(callback, "callback");

			JobState state = new JobState(JobType.Download, url, filename, callback);
			ThreadPool.QueueUserWorkItem(new WaitCallback(DoTransfer), state);
		}

		public void BeginUpload(Uri url, string filename, FileTransferFinishedDelegate callback)
		{
			Guard.ArgumentNotNull(url, "url");
			Guard.ArgumentNotNullOrEmptyString(filename, "filename");
			Guard.ArgumentNotNull(callback, "callback");

			JobState state = new JobState(JobType.Upload, url, filename, callback);
			ThreadPool.QueueUserWorkItem(new WaitCallback(DoTransfer), state);
		}

		private static void DoTransfer(object state)
		{
			JobState jobState = (JobState)state;
			try
			{
				WebClient client = new WebClient();
				if (jobState.JobType == JobType.Upload)
					client.UploadFile(jobState.Url, jobState.Filename);
				else
					client.DownloadFile(jobState.Url, jobState.Filename);

				jobState.Callback(jobState.Url, true);
			}
			catch (Exception)
			{
				jobState.Callback(jobState.Url, false);
			}
		}

		private class JobState
		{
			public Uri Url;
			public string Filename;
			public JobType JobType;
			public FileTransferFinishedDelegate Callback;

			public JobState(JobType jobType, Uri url, string filename, FileTransferFinishedDelegate callback)
			{
				Url = url;
				Filename = filename;
				Callback = callback;
				JobType = jobType;
			}
		}

		enum JobType
		{
			Upload,
			Download
		}

	}
}
