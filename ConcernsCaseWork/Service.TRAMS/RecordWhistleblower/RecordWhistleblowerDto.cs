using Newtonsoft.Json;

namespace Service.TRAMS.RecordWhistleblower
{
	
	public sealed class RecordWhistleblowerDto
	{
		[JsonProperty("name")]
		public string Name { get; }
		
		[JsonProperty("details")]
		public string Details { get; }
		
		[JsonProperty("reason")]
		public string Reason { get; }
		
		[JsonProperty("record_urn")]
		public long RecordUrn { get; }
		
		[JsonProperty("urn")]
		public long Urn { get; }
		
		[JsonConstructor]
		public RecordWhistleblowerDto(string name, string details, string reason, long recordUrn, long urn) => 
			(Name, Details, Reason, RecordUrn, Urn) = (name, details, reason, recordUrn, urn);
	}
}