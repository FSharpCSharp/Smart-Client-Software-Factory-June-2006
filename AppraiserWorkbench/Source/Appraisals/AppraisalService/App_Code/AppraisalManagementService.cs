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
using System.IO.IsolatedStorage;
using System.Text;
using System.Web.Services;
using System.Xml;
using System.Xml.Serialization;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class AppraisalManagementService : WebService
{
	public const string BaseUrl = "http://localhost:1428/AppraisalService/";
	public const string DownloadUrl = BaseUrl + "Document.ashx?attachmentId=";

	[WebMethod]
	public Appraisal[] GetAppraisals(string userId, AppraisalFilter filter)
	{
		System.Threading.Thread.Sleep(1000);

		BackingStore store = LoadBackingStore();
		List<Appraisal> result = new List<Appraisal>();

		foreach (Appraisal appraisal in store.Appraisals)
		{
			if (filter == AppraisalFilter.MyAppraisals)
			{
				if (appraisal.AssignedTo == userId)
					result.Add(appraisal);
			}
			else
			{
				if (String.IsNullOrEmpty(appraisal.AssignedTo))
					result.Add(appraisal);
			}
		}

		return result.ToArray();
	}

	[WebMethod]
	public bool LockAppraisal(string userId, string appraisalId)
	{
		if (appraisalId.StartsWith("R"))
			return false;

		BackingStore store = LoadBackingStore();

		foreach (Appraisal appraisal in store.Appraisals)
			if (appraisal.Id == appraisalId)
				appraisal.AssignedTo = userId;

		SaveBackingStore(store);

		return true;
	}

	[WebMethod]
	public void ReleaseAppraisal(string userId, string appraisalId)
	{
		BackingStore store = LoadBackingStore();

		foreach (Appraisal appraisal in store.Appraisals)
			if (appraisal.Id == appraisalId)
				appraisal.AssignedTo = "$FINISHED$";

		SaveBackingStore(store);
	}

	internal static void ResetBackingStore()
	{
		BackingStore back = new BackingStore();

		Appraisal a = new Appraisal();
		a.Id = "14353";
		a.PropertyType = PropertyType.ResidentialSingleFamily;
		a.PropertyAddress = new Address();
		a.PropertyAddress.Street1 = "825 228th Ave. NE";
		a.PropertyAddress.City = "Sammamish";
		a.PropertyAddress.State = "WA";
		a.PropertyAddress.Zip = "98074";
		a.DateToComplete = DateTime.Now.AddDays(4);
		a.Description = "2003 detached house, 5 bedroom, 3.5 bathrooms, 2 car garage, Territorial view";
		a.Notes = "Year Built: 2003, Lot Size: 1.21 Acres, Fireplace: 2, Views: Yes, Garage: Garage-Attached, Roof: Composition, Lot Description: 1.21 Acre Greenbelt Home Site  Heat: Forced Air, Radiant, Fuel: Natural Gas, A/C: Yes, Taxes: $8,000, Floors: Ceramic Tile, Hardwood, Vinyl, Wall to Wall Carpet, Interior: Bath Off Master, Built-In Vacuum, Dining Room, Disabled Access, Dble Pane/Strm Windw, High Tech Cabling, Pantry, Security System, Vaulted Ceilings, Walk-in Closet, Exterior: Stone, Wood, See Remarks, Site: Cable TV, Disabled Access, Patio, Sprinkler System";
		a.Attachments = new List<AttachmentMetadata>();
		a.Attachments.Add(new AttachmentMetadata("Sample Document.doc", new Uri(DownloadUrl + "1")));
		back.Appraisals.Add(a);

		a = new Appraisal();
		a.Id = "23656";
		a.PropertyType = PropertyType.ResidentialCondoOrTownhouse;
		a.PropertyAddress = new Address();
		a.PropertyAddress.Street1 = "14250 S.E. Newport Way";
		a.PropertyAddress.City = "Bellevue";
		a.PropertyAddress.State = "WA";
		a.PropertyAddress.Zip = "98006";
		a.DateToComplete = DateTime.Now.AddDays(5);
		a.Description = "1999 Townhouse, recently remodeled, 3 bedrooms, 2.5 bathrooms, individual garage, mountains view";
		a.Notes = "Year Built: 1999, Lot Size: 3049.00 SQFT, Fireplace: 1  Garage: Individual Garage, Roof: Composition,  Heat: Forced Air, Fuel: Natural Gas, Taxes: $2,067, Floors: Vinyl, Wall to Wall Carpet, Interior: Insulated Windows, Master Bath, Walk-in Closet, Yard, Exterior: Metal/Vinyl, Site: Cable TV, Security Gate";
		a.Attachments.Add(new AttachmentMetadata("Sample Document.doc", new Uri(DownloadUrl + "2")));
		a.Attachments.Add(new AttachmentMetadata("Sample Presentation.ppt", new Uri(DownloadUrl + "3")));
		a.Attachments.Add(new AttachmentMetadata("Sample Text.txt", new Uri(DownloadUrl + "4")));
		back.Appraisals.Add(a);

		a = new Appraisal();
		a.Id = "74839";
		a.PropertyType = PropertyType.CommercialIndustrial;
		a.PropertyAddress = new Address();
		a.PropertyAddress.Street1 = "18138 73rd N.E.";
		a.PropertyAddress.City = "Kenmore";
		a.PropertyAddress.State = "WA";
		a.PropertyAddress.Zip = "98028";
		a.DateToComplete = DateTime.Now.AddDays(6);
		a.Description = "Four commercial builings on 1/2 acre parcel that consists of a 4-plex, one small house, large commercial building with five comm. tenants on main level and five apts above. There is also a 1/2 acre parking lot";
		a.Notes = "Property Type:Retail, Building Sq Ft:14,000, Leasable Sq Ft:14,000, Lot Size (Sq Ft):45,270, Lot Size (Acres): 1.01, Year Built: 1949, Zoning: commercial, Total Units: 17, Construction Type: Framed, masonry, tiles";
		a.Attachments.Add(new AttachmentMetadata("Sample Text.txt", new Uri(DownloadUrl + "5")));
		back.Appraisals.Add(a);

		a = new Appraisal();
		a.Id = "R0000";
		a.PropertyType = PropertyType.ResidentialCondoOrTownhouse;
		a.PropertyAddress = new Address();
		a.PropertyAddress.Street1 = "19601 21st Ave N.W.";
		a.PropertyAddress.City = "Shoreline";
		a.PropertyAddress.State = "WA";
		a.PropertyAddress.Zip = "98177";
		a.DateToComplete = DateTime.Now.AddDays(6);
		a.Description = "THIS APPRAISAL WILL FAIL - 2004 Condo, Bedrooms: 1, Bathrooms: 1.00. Great views, 6-panel doors. Great kitchen features hardwood floor, stainless appliance package, granite countertops";
		a.Notes = "SQFT: 910, Year Built: 2004, Fireplace: 0 Views: Yes, Garage: Uncovered, Roof: Flat, Heat: Forced Air, Central , A/C, Fuel: Electric, Taxes: $1,980, Floors: Ceramic Tile, Hardwood, Vinyl, Wall to Wall Carpet Interior: Disabled Access, Insulated Windows, Walk-in Closet Exterior: Cement/Concrete, Metal/Vinyl Site: Disabled Access, Elevator, Exercise Room, Fire Sprinklers, Game/Rec Rm, Lobby Entrance";
		back.Appraisals.Add(a);

		a = new Appraisal();
		a.Id = "R0001";
		a.PropertyType = PropertyType.ResidentialCondoOrTownhouse;
		a.PropertyAddress = new Address();
		a.PropertyAddress.Street1 = "212 2nd Avenue N.";
		a.PropertyAddress.City = "Kent";
		a.PropertyAddress.State = "WA";
		a.PropertyAddress.Zip = "98032";
		a.DateToComplete = DateTime.Now.AddDays(6);
		a.Description = "THIS APPRAISAL WILL FAIL - 2001 Condo, Bedrooms: 1, Bathrooms: 1.00. ";
		a.Notes = "SQFT: 700, Year Built: 2001, Fireplace: 1 Views: Yes, Garage: Common Garage, Heat: Baseboard, Forced Air, Fuel: Electric, Floors: Ceramic Tile, Vinyl, Wall to Wall Carpet Interior: Balcony/Deck/Patio, Insulated Windows, Walk-in Closet Site: Elevator, Fire Sprinklers, Lobby Entrance, Security Gate";
		back.Appraisals.Add(a);

		SaveBackingStore(back);
	}

	internal static BackingStore LoadBackingStore()
	{
		IsolatedStorageFile isf = IsolatedStorageFile.GetMachineStoreForAssembly();
		if (isf.GetFileNames("BackingStore.xml").Length == 0)
			ResetBackingStore();

		using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream("BackingStore.xml", FileMode.Open, FileAccess.Read, isf))
		using (XmlTextReader reader = new XmlTextReader(stream))
		{
			XmlSerializer se = new XmlSerializer(typeof(BackingStore));
			return (BackingStore)se.Deserialize(reader);
		}
	}

	internal static void SaveBackingStore(BackingStore back)
	{
		IsolatedStorageFile isf = IsolatedStorageFile.GetMachineStoreForAssembly();

		using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream("BackingStore.xml", FileMode.Create, FileAccess.Write, isf))
		using (XmlTextWriter writer = new XmlTextWriter(stream, Encoding.UTF8))
		{
			writer.Formatting = Formatting.Indented;
			XmlSerializer se = new XmlSerializer(typeof(BackingStore));
			se.Serialize(writer, back);
		}
	}
}
