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
using System.Collections.Generic;
using System.IO;
using System.Security.Permissions;
using System.Diagnostics;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Microsoft.Practices.CompositeUI.Utility;
using GlobalBank.Infrastructure.Interface.Services;

namespace GlobalBank.Infrastructure.Shell.Services
{
	public class FileWatcherService : IFileWatcherService
	{
		// Locals

		private Dictionary<string, FileSystemWatcher> _folderWatchers;
		private Dictionary<FileSystemWatcher, List<string>> _fileWatchers;
		private Dictionary<string, FileWatcherServiceCallback> _fileCallbacks;
		private object _lockObject = new object();

		// Lifetime

		public FileWatcherService()
		{
			CaseInsensitiveStringComparer comparer = new CaseInsensitiveStringComparer();

			_folderWatchers = new Dictionary<string, FileSystemWatcher>(comparer);
			_fileWatchers = new Dictionary<FileSystemWatcher, List<string>>();
			_fileCallbacks = new Dictionary<string, FileWatcherServiceCallback>(comparer);
		}

		~FileWatcherService()
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
				lock (_lockObject)
				{
					foreach (FileSystemWatcher watcher in _folderWatchers.Values)
						watcher.Dispose();

					_fileWatchers.Clear();
					_fileCallbacks.Clear();
					_folderWatchers.Clear();
				}
			}
		}

		// Methods

		public void BeginWatchFile(string filename, FileWatcherServiceCallback callback)
		{
			Guard.ArgumentNotNullOrEmptyString(filename, "filename");
			Guard.ArgumentNotNull(callback, "callback");

			lock (_lockObject)
			{
				string folder = Path.GetDirectoryName(filename);
				FileSystemWatcher folderWatcher = GetFolderWatcher(folder);
				_fileWatchers[folderWatcher].Add(filename);
				_fileCallbacks[filename] = callback;
			}
		}

		public void EndWatchFile(string filename)
		{
			Guard.ArgumentNotNullOrEmptyString(filename, "filename");

			lock (_lockObject)
			{
				string folder = Path.GetDirectoryName(filename);

				if (!_folderWatchers.ContainsKey(folder))
					return;

				FileSystemWatcher folderWatcher = _folderWatchers[folder];
				_fileCallbacks.Remove(filename);
				_fileWatchers[folderWatcher].Remove(filename);

				if (_fileWatchers[folderWatcher].Count == 0)
				{
					_fileWatchers.Remove(folderWatcher);
					_folderWatchers.Remove(folder);
					folderWatcher.Dispose();
				}
			}
		}

		[SecurityPermission( SecurityAction.Demand, Unrestricted=true)]
		private FileSystemWatcher GetFolderWatcher(string folder)
		{
			if (!_folderWatchers.ContainsKey(folder))
			{
				FileSystemWatcher newWatcher = new FileSystemWatcher(folder);
				newWatcher.EnableRaisingEvents = true;
				newWatcher.Changed += new FileSystemEventHandler(FolderChangeHandler);
				newWatcher.Created += new FileSystemEventHandler(FolderChangeHandler);
				newWatcher.Deleted += new FileSystemEventHandler(FolderChangeHandler);
				newWatcher.Renamed += new RenamedEventHandler(FolderChangeHandler);

				_folderWatchers.Add(folder, newWatcher);
				_fileWatchers.Add(newWatcher, new List<string>());
			}

			return _folderWatchers[folder];
		}

		private void FolderChangeHandler(object sender, FileSystemEventArgs e)
		{
			lock (_lockObject)
			{
				FileSystemWatcher watcher = (FileSystemWatcher)sender;

				if (_fileWatchers[watcher].Find(delegate(string value) { return String.Equals(e.FullPath, value, StringComparison.InvariantCultureIgnoreCase); }) != null)
				{
					string changedFilePath = e.FullPath;
					if (_fileCallbacks[changedFilePath] != null)
					{
						try
						{
							_fileCallbacks[changedFilePath](changedFilePath);
						}
						catch (Exception ex)
						{
							Debug.WriteLine(ex.ToString());
							ExceptionPolicy.HandleException(ex, "Default Policy");
						}
					}
				}
			}
		}

		// Inner classes

		private class CaseInsensitiveStringComparer : StringComparer
		{
			public override int Compare(string x, string y)
			{
				return string.Compare(x, y, true);
			}

			public override bool Equals(string x, string y)
			{
				return string.Equals(x, y, StringComparison.InvariantCultureIgnoreCase);
			}

			public override int GetHashCode(string obj)
			{
				return obj.ToLowerInvariant().GetHashCode();
			}
		}
	}

}
