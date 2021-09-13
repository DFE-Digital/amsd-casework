﻿using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Service.TRAMS.Base;
using Service.TRAMS.Sequence;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Service.TRAMS.Type
{
	public sealed class TypeService : AbstractService, ITypeService
	{
		private readonly ILogger<TypeService> _logger;
		
		public TypeService(IHttpClientFactory clientFactory, ILogger<TypeService> logger) : base(clientFactory)
		{
			_logger = logger;
		}

		public async Task<IList<TypeDto>> GetTypes()
		{
			try
			{
				_logger.LogInformation("TypeService::GetTypes");
				
				// Create a request
				var request = new HttpRequestMessage(HttpMethod.Get, $"{EndpointsVersion}/types");
				
				// Create http client
				var client = ClientFactory.CreateClient("TramsClient");
				
				// Execute request
				var response = await client.SendAsync(request);

				// Check status code
				response.EnsureSuccessStatusCode();
				
				// Read response content
				var content = await response.Content.ReadAsStringAsync();
				
				// Deserialize content to POJO
				var typesDto = JsonConvert.DeserializeObject<IList<TypeDto>>(content);

				return typesDto;
			}
			catch (Exception ex)
			{
				_logger.LogError($"TypeService::GetTypes::Exception message::{ex.Message}");
			}
			
			// TODO replace return when TRAMS API endpoints are live
			var currentDate = DateTimeOffset.Now;
			return new List<TypeDto>
			{
				new TypeDto("Compliance", "Compliance: Financial reporting", currentDate, 
					currentDate, LongSequence.Generator()),
				new TypeDto("Compliance", "Compliance: Financial returns", currentDate, 
					currentDate, LongSequence.Generator()),
				new TypeDto("Financial", "Financial: Deficit", currentDate, 
					currentDate, LongSequence.Generator()),
				new TypeDto("Financial", "Financial: Projected deficit / Low future surplus", currentDate, 
					currentDate, LongSequence.Generator()),
				new TypeDto("Financial", "Financial: Cash flow shortfall", currentDate, 
					currentDate, LongSequence.Generator()),
				new TypeDto("Financial", "Financial: Clawback", currentDate, 
					currentDate, LongSequence.Generator()),
				new TypeDto("Force Majeure", "", currentDate, 
					currentDate, LongSequence.Generator()),
				new TypeDto("Governance", "Governance: Governance", currentDate, 
					currentDate, LongSequence.Generator()),
				new TypeDto("Governance", "Governance: Closure", currentDate, 
					currentDate, LongSequence.Generator()),
				new TypeDto("Governance", "Governance: Executive Pay", currentDate, 
					currentDate, LongSequence.Generator()),
				new TypeDto("Governance", "Governance: Safeguarding", currentDate, 
					currentDate, LongSequence.Generator()),
				new TypeDto("Irregularity", "Irregularity: Allegations and self reported concerns", currentDate, 
					currentDate, LongSequence.Generator()),
				new TypeDto("Irregularity", "Irregularity: Related party transactions - in year", currentDate, 
					currentDate, LongSequence.Generator())
			};
			
			//return Array.Empty<TypeDto>();
		}
	}
}