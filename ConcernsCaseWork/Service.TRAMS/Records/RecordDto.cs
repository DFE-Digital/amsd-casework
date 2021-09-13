﻿using Newtonsoft.Json;
using System;

namespace Service.TRAMS.Records
{
	public sealed class RecordDto
	{
		[JsonProperty("created_at")]
		public DateTimeOffset CreatedAt { get; }

		[JsonProperty("updated_at")]
		public DateTimeOffset UpdateAt { get; }
		
		[JsonProperty("review_at")]
		public DateTimeOffset ReviewAt { get; }
		
		[JsonProperty("closed_at")]
		public DateTimeOffset ClosedAt { get; }
		
		[JsonProperty("name")]
		public string Name { get; }
		
		[JsonProperty("description")]
		public string Description { get; }
		
		[JsonProperty("reason")]
		public string Reason { get; }
		
		[JsonProperty("case_urn")]
		public long CaseUrn { get; }
		
		[JsonProperty("type_urn")]
		public long TypeUrn { get; }

		[JsonProperty("rating_urn")]
		public long RatingUrn { get; }
		
		[JsonProperty("primary")]
		public bool Primary { get; }
		
		[JsonProperty("urn")]
		public long Urn { get; set; } // TODO Remove setter when TRAMS API is live
		
		[JsonProperty("status")]
		public long Status { get; }
		
		[JsonConstructor]
		public RecordDto(DateTimeOffset createdAt, DateTimeOffset updatedAt, DateTimeOffset reviewAt, DateTimeOffset closedAt, 
			string name, string description, string reason, long caseUrn, long typeUrn, 
			long ratingUrn, bool primary, long urn, long status) => 
			(CreatedAt, UpdateAt, ReviewAt, ClosedAt, Name, Description, Reason, CaseUrn, TypeUrn, RatingUrn, Primary, Urn, Status) = 
			(createdAt, updatedAt, reviewAt, closedAt, name, description, reason, caseUrn, typeUrn, ratingUrn, primary, urn, status);
	}
}