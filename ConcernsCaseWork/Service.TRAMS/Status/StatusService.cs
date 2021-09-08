﻿using Microsoft.Extensions.Logging;
using Service.TRAMS.Base;
using Service.TRAMS.Dto;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Service.TRAMS.Status
{
	public sealed class StatusService : AbstractService, IStatusService
	{
		private readonly ILogger<StatusService> _logger;
		
		public StatusService(IHttpClientFactory clientFactory, ILogger<StatusService> logger) : base(clientFactory)
		{
			_logger = logger;
		}

		public async Task<IList<StatusDto>> GetStatuses()
		{
			try
			{
				_logger.LogInformation("StatusService::GetStatuses");
				
				// Create a request
				var request = new HttpRequestMessage(HttpMethod.Get, "/statuses");
				
				// Create http client
				var client = ClientFactory.CreateClient("TramsClient");
				
				// Execute request
				var response = await client.SendAsync(request);

				// Check status code
				response.EnsureSuccessStatusCode();
				
				// Read response content
				var content = await response.Content.ReadAsStringAsync();
				
				// Deserialize content to POJO
				var options = new JsonSerializerOptions(JsonSerializerDefaults.Web);
				var statusDto = JsonSerializer.Deserialize<IList<StatusDto>>(content, options);

				return statusDto;
			}
			catch (Exception ex)
			{
				_logger.LogError($"StatusService::GetStatuses::Exception message::{ex.Message}");
			}
			
			return Array.Empty<StatusDto>();
		}
	}
}