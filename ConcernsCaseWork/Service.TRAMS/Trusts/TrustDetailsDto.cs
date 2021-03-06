using Newtonsoft.Json;
using System.Collections.Generic;

namespace Service.TRAMS.Trusts
{
	public sealed class TrustDetailsDto
	{
		[JsonProperty("giasData")]
		public GiasDataDto GiasData { get; }
		
		[JsonProperty("ifdData")]
		public IfdDataDto IfdData { get; }
		
		[JsonProperty("establishments")]
		public List<EstablishmentDto> Establishments { get; } 

		[JsonConstructor]
		public TrustDetailsDto(GiasDataDto giasData, IfdDataDto ifdData, List<EstablishmentDto> establishments) => 
			(GiasData, IfdData, Establishments) = (giasData, ifdData, establishments);
	}
}