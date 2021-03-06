using Newtonsoft.Json;
using System;

namespace Service.TRAMS.Cases
{
	public sealed class CreateCaseDto
	{
		[JsonProperty("createdAt")]
		public DateTimeOffset CreatedAt { get; }

		[JsonProperty("updatedAt")]
		public DateTimeOffset UpdatedAt { get; }
		
		[JsonProperty("reviewAt")]
		public DateTimeOffset ReviewAt { get; }
		
		[JsonProperty("closedAt")]
		public DateTimeOffset ClosedAt { get; }
		
		[JsonProperty("createdBy")]
		public string CreatedBy { get; }
		
		[JsonProperty("crmEnquiry")]
		public string CrmEnquiry { get; }
		
		[JsonProperty("trustUkprn")]
		public string TrustUkPrn { get; }
		
		[JsonProperty("reasonAtReview")]
		public string ReasonAtReview { get; }

		[JsonProperty("deEscalation")]
		public DateTimeOffset DeEscalation { get; }
		
		[JsonProperty("issue")]
		public string Issue { get; }

		[JsonProperty("currentStatus")]
		public string CurrentStatus { get; }

		[JsonProperty("caseAim")]
		public string CaseAim { get; }
		
		[JsonProperty("deEscalationPoint")]
		public string DeEscalationPoint { get; }
		
		[JsonProperty("nextSteps")]
		public string NextSteps { get; }
		
		/// <summary>
		/// Deteriorating, unchanged, improved
		/// </summary>
		[JsonProperty("directionOfTravel")]
		public string DirectionOfTravel { get; }
		
		[JsonProperty("statusUrn")]
		public long StatusUrn { get; }
		
		[JsonProperty("ratingUrn")]
		public long RatingUrn { get; }

		[JsonConstructor]
		public CreateCaseDto(DateTimeOffset createdAt, DateTimeOffset updatedAt, DateTimeOffset reviewAt, 
			DateTimeOffset closedAt, string createdBy, string crmEnquiry, string trustUkPrn, 
			string reasonAtReview, DateTimeOffset deEscalation, string issue, string currentStatus, 
			string nextSteps, string caseAim, string deEscalationPoint, string directionOfTravel, long statusUrn,
			long ratingUrn) => 
			(CreatedAt, UpdatedAt, ReviewAt, ClosedAt, CreatedBy, CrmEnquiry, TrustUkPrn,
				ReasonAtReview, DeEscalation, Issue, CurrentStatus, NextSteps, CaseAim, DeEscalationPoint, DirectionOfTravel, 
				StatusUrn, RatingUrn) = 
			(createdAt, updatedAt, reviewAt, closedAt, createdBy, crmEnquiry, trustUkPrn,
				reasonAtReview, deEscalation, issue, currentStatus, nextSteps, caseAim, deEscalationPoint, directionOfTravel, 
				statusUrn, ratingUrn);
	}
}