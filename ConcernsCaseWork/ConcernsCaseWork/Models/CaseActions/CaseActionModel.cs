using System;

namespace ConcernsCaseWork.Models.CaseActions
{
	public class CaseActionModel
	{
		public long Id { get; set; }
		public long CaseUrn { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }
		public DateTime? ClosedAt { get; set; }
	}
}
