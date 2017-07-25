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

namespace GlobalBank.AppraiserWorkbench.AppraisalServiceAgent.BusinessEntities
{
	public class Appraisal
	{
		private string _id;
		private string _description;
		private DateTime _dateToComplete;
		private Address _address = new Address();
		private PropertyType _propertyType;
		private string _notes;
		private AppraisalStatus _status;
		private List<Attachment> _attachments = new List<Attachment>();

		public enum AppraisalStatus
		{
			NonEditable,
			Editable,
			Submitted,
			Rejected
		}

		public Appraisal()
		{
		}

		public string Id
		{
			get { return _id; }
			set { _id = value; }
		}

		public string Description
		{
			get { return _description; }
			set { _description = value; }
		}

		public DateTime DateToComplete
		{
			get { return _dateToComplete; }
			set { _dateToComplete = value; }
		}

		public Address PropertyAddress
		{
			get { return _address; }
			set { _address = value; }
		}

		public PropertyType PropertyType
		{
			get { return _propertyType; }
			set { _propertyType = value; }
		}

		public string Notes
		{
			get { return _notes; }
			set { _notes = value; }
		}

		public List<Attachment> Attachments
		{
			get { return _attachments; }
			set { _attachments = value; }
		}

		public AppraisalStatus Status
		{
			get { return _status; }
			set { _status = value; }
		}
	}
}
