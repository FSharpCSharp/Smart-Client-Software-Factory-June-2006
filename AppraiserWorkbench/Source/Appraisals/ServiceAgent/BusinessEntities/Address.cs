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

namespace GlobalBank.AppraiserWorkbench.AppraisalServiceAgent.BusinessEntities
{
	public class Address
	{
		private string _street1;
		private string _street2;
		private string _city;
		private string _state;
		private string _zip;

		public string Street1
		{
			get { return _street1; }
			set { _street1 = value; }
		}

		public string Street2
		{
			get { return _street2; }
			set { _street2 = value; }
		}

		public string City
		{
			get { return _city; }
			set { _city = value; }
		}

		public string State
		{
			get { return _state; }
			set { _state = value; }
		}

		public string Zip
		{
			get { return _zip; }
			set { _zip = value; }
		}

		public override string ToString()
		{
			return string.Format("{0}, {1}, {2} {3}", Street1, City, State, Zip);
		}
	}
}
