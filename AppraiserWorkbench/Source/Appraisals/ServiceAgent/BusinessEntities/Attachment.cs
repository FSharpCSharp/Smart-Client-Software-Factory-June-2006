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

namespace GlobalBank.AppraiserWorkbench.AppraisalServiceAgent.BusinessEntities
{
	public class Attachment
	{
		public event EventHandler StatusChanged;

		public enum AttachmentStatus
		{
			NotAvailable,
			Downloading,
			AvailableNotModified,
			AvailableModified,
			ToBeUploaded,
			Uploading,
			Uploaded
		}

		private string _displayName;

		public string DisplayName
		{
			get { return _displayName; }
			set { _displayName = value; }
		}

		private string _fileName;

		public string FileName
		{
			get { return _fileName; }
			set { _fileName = value; }
		}

		private string _documentUrl;

		public Uri DocumentUrl
		{
			get { return new Uri(_documentUrl); }
		}

		public string DocumentUrlString
		{
			get { return _documentUrl; }
			set { _documentUrl = value; }
		}

		private AttachmentStatus _status;

		public AttachmentStatus Status
		{
			get { return _status; }
			set
			{
				_status = value;
				OnStatusChanged();
			}
		}

		private DateTime _localCreationTime;

		public DateTime LocalCreationTime
		{
			get { return _localCreationTime; }
			set { _localCreationTime = value; }
		}

		private void OnStatusChanged()
		{
			if (StatusChanged != null)
				StatusChanged(this, EventArgs.Empty);
		}
	}
}
