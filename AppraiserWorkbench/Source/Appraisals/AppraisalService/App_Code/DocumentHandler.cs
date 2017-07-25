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
using System.Web;

public class DocumentHandler : IHttpHandler
{
	public bool IsReusable
	{
		get { return true; }
	}

	public void ProcessRequest(HttpContext context)
	{
		Thread.Sleep(5000);

		switch (context.Request.HttpMethod.ToUpperInvariant())
		{
			case "GET":
				ProcessGetRequest(context);
				break;

			case "POST":
				ProcessPostRequest(context);
				break;

			default:
				ProcessUnknownVerb(context);
				break;
		}

		context.Response.End();
	}

	private static void ProcessGetRequest(HttpContext context)
	{
		int attachmentId;

		if (!int.TryParse(context.Request.QueryString["attachmentId"], out attachmentId))
		{
			context.Response.ContentType = "text/plain";
			context.Response.Write("This REST endpoint is used to retrieve appraisal documents from the server. You must provide an attachmentId parameter in the HTTP Request Query String.");
			context.Response.StatusCode = 200;
			context.Response.Status = "200 OK";
			context.Response.End();
		}
		else
		{
			switch (attachmentId)
			{
				case 1:
				case 2:
					SendFile(context, "Sample Document.doc");
					break;

				case 3:
					SendFile(context, "Sample Presentation.ppt");
					break;

				case 4:
				case 5:
					SendFile(context, "Sample Text.txt");
					break;

				default:
					context.Response.StatusCode = 500;
					context.Response.Status = "500 Server Error";
					context.Response.Write("Invalid attachment ID");
					break;
			}

			context.Response.End();
		}
	}

	private void ProcessPostRequest(HttpContext context)
	{
		context.Response.StatusCode = 200;
	}

	private void ProcessUnknownVerb(HttpContext context)
	{
		context.Response.StatusCode = 500;
		context.Response.Status = "500 Unsupported verb";
		context.Response.Write("<html><head><title>Unsupported verb</title></head><body><h1>The requested verb is not supported.</h1></body></html>");
	}

	private static void SendFile(HttpContext context, string file)
	{
		context.Response.ContentType = "binary/octet";
		context.Response.AddHeader("Content-Disposition", "attachment; filename=" + file);
		context.Response.TransmitFile(Path.Combine(context.Server.MapPath("App_Data"), file));
	}
}
