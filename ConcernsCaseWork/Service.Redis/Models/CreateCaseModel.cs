﻿using System;
using System.Collections.Generic;

namespace Service.Redis.Models
{
	[Serializable]
	public sealed class CreateCaseModel
	{
		public DateTimeOffset CreatedAt { get; set; }

		public DateTimeOffset UpdatedAt { get; set; }

		public DateTimeOffset ReviewAt { get; set; }

		public DateTimeOffset ClosedAt { get; set; }

		public string CreatedBy { get; set; }

		public string Description { get; set; }
		
		public string CrmEnquiry { get; set; }
		
		public string TrustName { get; set; }
		
		public string TrustUkPrn { get; set; }
		
		public string ReasonAtReview { get; set; }
		
		public DateTimeOffset DeEscalation { get; set; }

		public string Issue { get; set; }

		public string CurrentStatus { get; set; }

		public string CaseAim { get; set; }
		
		public string DeEscalationPoint { get; set; }
		
		public string NextSteps { get; set; }
		
		public string DirectionOfTravel { get; set; }
		
		public long Urn { get; set; }
		
		public long Status { get; set; }
		
		public string RecordType { get; set; }
		
		public string RecordSubType { get; set; }
		
		public string RagRatingName { get; set; }
		
		public IList<string> RagRating { get; set; }

		public IList<string> RagRatingCss { get; set; }
	}
}