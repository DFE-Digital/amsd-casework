﻿using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Service.TRAMS.Base;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace Service.TRAMS.FinancialPlan
{
	public sealed class FinancialPlanService : AbstractService, IFinancialPlanService
	{
		private readonly ILogger<FinancialPlanService> _logger;

		public FinancialPlanService(IHttpClientFactory clientFactory, ILogger<FinancialPlanService> logger) : base(clientFactory)
		{
			_logger = logger;
		}

		public async Task<IList<FinancialPlanDto>> GetFinancialPlansByCaseUrn(long caseUrn)
		{
			try
			{
				_logger.LogInformation("FinancialPlanService::GetFinancialPlansByCaseUrn");

				// Create a request
				var request = new HttpRequestMessage(HttpMethod.Get,
					$"/{EndpointsVersion}/record-srma/record/urn/{caseUrn}");

				// Create http client
				var client = ClientFactory.CreateClient(HttpClientName);

				// Execute request
				var response = await client.SendAsync(request);

				// Check status code
				response.EnsureSuccessStatusCode();

				// Read response content
				var content = await response.Content.ReadAsStringAsync();

				// Deserialize content to POJO
				var financialPlansDto = JsonConvert.DeserializeObject<IList<FinancialPlanDto>>(content);

				return financialPlansDto;
			}
			catch (Exception ex)
			{
				_logger.LogError("FinancialPlanService::GetFinancialPlansByCaseUrn::Exception message::{Message}", ex.Message);
			}

			return Array.Empty<FinancialPlanDto>();
		}

		public async Task<FinancialPlanDto> PostFinancialPlanByCaseUrn(CreateFinancialPlanDto createFinancialPlanDto)
		{
			try
			{
				_logger.LogInformation("FinancialPlanService::PostFinancialPlanByCaseUrn");

				// Create a request
				var request = new StringContent(
					JsonConvert.SerializeObject(createFinancialPlanDto),
					Encoding.UTF8,
					MediaTypeNames.Application.Json);

				// Create http client
				var client = ClientFactory.CreateClient(HttpClientName);

				// Execute request
				var response = await client.PostAsync(
					$"/{EndpointsVersion}/record-srma/record/urn/{createFinancialPlanDto.CaseUrn}", request);

				// Check status code
				response.EnsureSuccessStatusCode();

				// Read response content
				var content = await response.Content.ReadAsStringAsync();

				// Deserialize content to POJO
				var newFinancialPlanDto = JsonConvert.DeserializeObject<FinancialPlanDto>(content);

				return newFinancialPlanDto;
			}
			catch (Exception ex)
			{
				_logger.LogError("FinancialPlanService::PostFinancialPlanByCaseUrn::Exception message::{Message}", ex.Message);
			}

			return null;
		}

		public async Task<FinancialPlanDto> PatchFinancialPlanById(FinancialPlanDto financialPlanDto)
		{
			try
			{
				_logger.LogInformation("FinancialPlanService::PatchFinancialPlanById");

				// Create a request
				var request = new StringContent(
					JsonConvert.SerializeObject(financialPlanDto),
					Encoding.UTF8,
					MediaTypeNames.Application.Json);

				// Create http client
				var client = ClientFactory.CreateClient(HttpClientName);

				// Execute request
				var response = await client.PatchAsync(
					$"/{EndpointsVersion}/record-srma/urn/{financialPlanDto.CaseUrn}", request);

				// Check status code
				response.EnsureSuccessStatusCode();

				// Read response content
				var content = await response.Content.ReadAsStringAsync();

				// Deserialize content to POJO
				var updatedFinancialPlanDto = JsonConvert.DeserializeObject<FinancialPlanDto>(content);

				return updatedFinancialPlanDto;
			}
			catch (Exception ex)
			{
				_logger.LogError("FinancialPlanService::PatchFinancialPlanById::Exception message::{Message}", ex.Message);
			}

			return null;
		}
	}
}