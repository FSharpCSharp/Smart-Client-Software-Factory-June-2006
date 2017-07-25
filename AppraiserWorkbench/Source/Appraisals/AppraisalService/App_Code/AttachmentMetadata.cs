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
using System.Runtime.Serialization;

public class AttachmentMetadata
{
	/// <summary>
	/// Initializes a new instance of the <see cref="T:Attachment"/> class.
	/// </summary>
	public AttachmentMetadata()
	{
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="T:SvcAttachment"/> class.
	/// </summary>
	/// <param name="id">The id.</param>
	/// <param name="displayName">Name of the display.</param>
	/// <param name="mimeType">Type of the MIME.</param>
	/// <param name="bytes">The bytes.</param>
	public AttachmentMetadata(string filename, Uri url)
	{
		_filename = filename;
		_url = url.AbsoluteUri;
	}

	private string _filename;

	public string FileName
	{
		get { return _filename; }
		set { _filename = value; }
	}

	private string _url;

	public string Url
	{
		get { return _url; }
		set { _url = value; }
	}
}
