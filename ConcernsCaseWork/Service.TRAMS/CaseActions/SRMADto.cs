using System;
using System.Collections.Generic;
using System.Text;

namespace Service.TRAMS.CaseActions
{
	[Serializable]
	public class SRMADto
	{
		public int Id { get; set; }
		public int CaseUrn { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime DateOffered { get; set; }
		public DateTime? DateAccepted { get; set; }
		public DateTime? DateReportSentToTrust { get; set; }
		public DateTime? DateVisitStart { get; set; }
		public DateTime? DateVisitEnd { get; set; }
		public SRMAStatus Status { get; set; }
		public string Notes { get; set; }
		public SRMAReasonOffered? Reason { get; set; }
		public DateTime? ClosedAt { get; set; }
	}
}
