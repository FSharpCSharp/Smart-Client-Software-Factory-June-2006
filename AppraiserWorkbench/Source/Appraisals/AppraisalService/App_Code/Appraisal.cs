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

public class Appraisal
{
	private string _id;
	private string _description;
	private DateTime _dateToComplete;
	private Address _address;
	private PropertyType _propertyType;
	private string _assignedTo;
	private string _notes;
	private List<AttachmentMetadata> _attachments = new List<AttachmentMetadata>();

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

	public string AssignedTo
	{
		get { return _assignedTo; }
		set { _assignedTo = value; }
	}

	public string Notes
	{
		get { return _notes; }
		set { _notes = value; }
	}

	public List<AttachmentMetadata> Attachments
	{
		get { return _attachments; }
		set { _attachments = value; }
	}
}
