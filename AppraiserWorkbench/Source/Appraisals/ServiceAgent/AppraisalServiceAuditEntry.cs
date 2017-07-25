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
using System.Text;
using System.Globalization;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using GlobalBank.AppraiserWorkbench.AppraisalServiceAgent.BusinessEntities;
using Microsoft.Practices.CompositeUI.Utility;

namespace GlobalBank.AppraiserWorkbench.AppraisalServiceAgent
{
	internal class AppraisalServiceAuditEntry : LogEntry
	{
		internal enum Reason
		{
			AssignmentRequest,
			AssignmentAccepted,
			AssignmentRejected,
			Submitted,
			Released
		}

		public AppraisalServiceAuditEntry(string appraisalId, Reason reason)
		{
			Guard.ArgumentNotNullOrEmptyString(appraisalId, "appraisalId");

			Categories.Add("AppraisalServiceAgent");
			switch (reason)
			{
				case Reason.AssignmentRequest:
					Message = String.Format(CultureInfo.CurrentCulture,
						Properties.Resources.AuditAssignmentRequest, appraisalId);
					break;
				case Reason.AssignmentAccepted:
					Message = String.Format(CultureInfo.CurrentCulture,
						Properties.Resources.AuditAssigmentGranted, appraisalId);
					break;
				case Reason.AssignmentRejected:
					Message = String.Format(CultureInfo.CurrentCulture,
						Properties.Resources.AuditAssignmentRejected, appraisalId);
					break;
				case Reason.Submitted:
					Message = String.Format(CultureInfo.CurrentCulture,
						Properties.Resources.AuditSubmitted, appraisalId);
					break;
				case Reason.Released:
					Message = String.Format(CultureInfo.CurrentCulture,
						Properties.Resources.AuditReleased, appraisalId);
					break;
			}
		}
	}
}
