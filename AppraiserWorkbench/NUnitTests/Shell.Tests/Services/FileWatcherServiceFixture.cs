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
using System.Threading;
using GlobalBank.Infrastructure.Interface.Services;
using NUnit.Framework;
using GlobalBank.Infrastructure.Shell;
using GlobalBank.Infrastructure.Shell.Services;

namespace GlobalBank.Infrastructure.Shell.Tests.Services
{
	[TestFixture]
	public class FileWatcherServiceFixture
	{
		public const int TIMEOUT = 10000;

		[Test]
		public void CanSeeOverwriteStyleChanges()
		{
			// Tests overwrite-style file save, which:
			//   1. Overwrites the existing file with new contents

			string filename = Path.GetTempFileName();

			try
			{
				using (FileWatcherService service = new FileWatcherService())
				{
					File.WriteAllText(filename, "Initial contents");
					bool fileChanged = false;
					service.BeginWatchFile(filename, delegate { fileChanged = true; });

					File.WriteAllText(filename, "New contents");

					SleepWait(TIMEOUT, delegate { return fileChanged; });
					Assert.IsTrue(fileChanged);
				}
			}
			finally
			{
				File.Delete(filename);
			}
		}

		[Test]
		public void CanSeeRollingBackupChanges()
		{
			// Tests rolling backup file save, which:
			//   1. Creates a new file with the new contents
			//   2. Renames the original file to a backup file
			//   3. Renames the new file to the original file

			string originalFilename = Path.GetTempFileName();
			string temporaryFilename = Path.GetTempFileName();
			string backupFilename = Path.GetTempFileName();

			try
			{
				using (FileWatcherService service = new FileWatcherService())
				{
					File.WriteAllText(originalFilename, "Initial contents");
					bool fileChanged = false;
					service.BeginWatchFile(originalFilename, delegate { fileChanged = true; });

					File.WriteAllText(temporaryFilename, "New contents");
					File.Delete(backupFilename);
					File.Move(originalFilename, backupFilename);
					File.Move(temporaryFilename, originalFilename);

					SleepWait(TIMEOUT, delegate { return fileChanged; });
					Assert.IsTrue(fileChanged);
				}
			}
			finally
			{
				File.Delete(originalFilename);
				File.Delete(temporaryFilename);
				File.Delete(backupFilename);
			}
		}

		[Test]
		public void CanSeeDeleteCreateStyleChanges()
		{
			// Tests delete-create file save, which:
			//   1. Deletes the original file
			//   2. Creates a new file with the original name

			string filename = Path.GetTempFileName();

			try
			{
				using (FileWatcherService service = new FileWatcherService())
				{
					File.WriteAllText(filename, "Initial contents");
					bool fileChanged = false;
					service.BeginWatchFile(filename, delegate { fileChanged = true; });

					File.Delete(filename);
					File.WriteAllText(filename, "New contents");

					SleepWait(TIMEOUT, delegate { return fileChanged; });
					Assert.IsTrue(fileChanged);
				}
			}
			finally
			{
				File.Delete(filename);
			}
		}

		[Test]
		public void FilenamesAreCaseInsensitive()
		{
			string filename = Path.GetTempFileName().ToLowerInvariant();

			try
			{
				using (FileWatcherService service = new FileWatcherService())
				{
					File.WriteAllText(filename, "Initial contents");
					bool fileChanged = false;
					service.BeginWatchFile(filename, delegate { fileChanged = true; });

					File.Delete(filename);
					File.WriteAllText(filename.ToUpperInvariant(), "New contents");

					SleepWait(TIMEOUT, delegate { return fileChanged; });
					Assert.IsTrue(fileChanged);
				}
			}
			finally
			{
				File.Delete(filename);
			}
		}

		[Test]
		public void WatchingMultipleFilesInTheSameFolderIssuesCallbacksForTheRightFile()
		{
			string filename1 = Path.GetTempFileName();
			string filename2 = Path.GetTempFileName();

			try
			{
				using (FileWatcherService service = new FileWatcherService())
				{
					File.WriteAllText(filename1, "Initial contents");
					File.WriteAllText(filename2, "Initial contents");
					bool fileChanged = false;
					service.BeginWatchFile(filename1, delegate(string filename) { fileChanged = true; });
					service.BeginWatchFile(filename2, delegate(string filename) { Assert.Fail(); });

					File.WriteAllText(filename1, "New contents");

					SleepWait(TIMEOUT, delegate { return fileChanged; });
					Assert.IsTrue(fileChanged);
				}
			}
			finally
			{
				File.Delete(filename1);
				File.Delete(filename2);
			}
		}

		[Test]
		public void StoppingWatchOnFileStopsEvents()
		{
			string filename = Path.GetTempFileName();

			try
			{
				using (FileWatcherService service = new FileWatcherService())
				{
					File.WriteAllText(filename, "Initial contents");
					bool fileChanged = false;
					service.BeginWatchFile(filename, delegate { fileChanged = true; });
					service.EndWatchFile(filename);

					File.WriteAllText(filename, "New contents");

					SleepWait(TIMEOUT, delegate { return fileChanged; });
					Assert.IsFalse(fileChanged);
				}
			}
			finally
			{
				File.Delete(filename);
			}
		}

		[Test]
		public void DisposingStopsEvents()
		{
			string filename = Path.GetTempFileName();

			try
			{
				bool fileChanged = false;

				using (FileWatcherService service = new FileWatcherService())
				{
					File.WriteAllText(filename, "Initial contents");
					service.BeginWatchFile(filename, delegate { fileChanged = true; });
				}

				File.WriteAllText(filename, "New contents");

				SleepWait(TIMEOUT, delegate { return fileChanged; });
				Assert.IsFalse(fileChanged);
			}
			finally
			{
				File.Delete(filename);
			}
		}

		#region Helpers

		private void SleepWait(int maxWait, Predicate<int> predicate)
		{
			int waited = 0;

			while (waited < maxWait)
			{
				if (predicate(waited))
					return;

				waited += 50;
				Thread.Sleep(50);
			}
		}

		#endregion
	}
}
