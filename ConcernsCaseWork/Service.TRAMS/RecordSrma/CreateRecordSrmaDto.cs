﻿using System.Numerics;
using System.Text.Json.Serialization;

namespace Service.TRAMS.Status
{
	public sealed class CreateRecordSrmaDto
	{
		[JsonPropertyName("name")]
		public string Name { get; }
		
		[JsonPropertyName("details")]
		public string Details { get; }
		
		[JsonPropertyName("reason")]
		public string Reason { get; }
		
		[JsonPropertyName("record_urn")]
		public BigInteger RecordUrn { get; }
		
		[JsonConstructor]
		public CreateRecordSrmaDto(int id, string name, string details, string reason, BigInteger recordUrn) => 
			(Name, Details, Reason, RecordUrn) = (name, details, reason, recordUrn);
	}
}