using ConcernsCaseWork.Shared.Tests.Factory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using NUnit.Framework;
using Service.TRAMS.CaseActions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Service.TRAMS.Base;
using Service.TRAMS.NtiUnderConsideration;

namespace Service.TRAMS.Tests.CaseActions
{
	public class NtiServiceTests
	{
		[Test]
		public void GetNtisByCaseId_Returns_ListOfNtiDto()
		{
			// Arrange
			var logger = new Mock<ILogger<NtiUnderConsiderationService>>();

			var ntis = new List<NtiUnderConsiderationDto> {
			new NtiUnderConsiderationDto
			{
				Id = 654,
				Notes = "Test1"
			},
			new NtiUnderConsiderationDto
			{
				Id = 667,
				Notes = "Test2"
			},
			new NtiUnderConsiderationDto
			{
				Id = 948,
				Notes = "Test3"
			}};

			var httpClientFactory = CreateMockFactory(ntis);

			var sut = new NtiUnderConsiderationService(httpClientFactory.Object, logger.Object);

			// Act
			var response = sut.GetNtisForCase(123).Result;

			// Assert
			Assert.IsNotNull(response);
			Assert.AreEqual(ntis.Count, response.Count);
			Assert.AreEqual(ntis.First().Id, response.First().Id);
			Assert.AreEqual(ntis.First().Notes, response.First().Notes);
		}

		[Test]
		public void CreateNti_Returns_New_NtiDto()
		{
			// Arrange
			var expectedNtiDto = new NtiUnderConsiderationDto
			{
				Id = 654,

				Notes = "Test"
			};

			var httpClientFactory = CreateMockFactory(expectedNtiDto);

			var logger = new Mock<ILogger<NtiUnderConsiderationService>>();

			var sut = new NtiUnderConsiderationService(httpClientFactory.Object, logger.Object);

			// Act
			var response = sut.CreateNti(expectedNtiDto).Result;

			// Assert
			Assert.AreEqual(expectedNtiDto.Id, response.Id);
			Assert.AreEqual(expectedNtiDto.Notes, response.Notes);
		}


		private Mock<IHttpClientFactory> CreateMockFactory<T>(T content)
		{
			var configuration = new ConfigurationBuilder().ConfigurationUserSecretsBuilder().Build();
			var tramsApiEndpoint = configuration["trams:api_endpoint"];

			var httpClientFactory = new Mock<IHttpClientFactory>();
			var mockMessageHandler = new Mock<HttpMessageHandler>();
			mockMessageHandler.Protected()
				.Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
				.ReturnsAsync(new HttpResponseMessage
				{
					StatusCode = HttpStatusCode.OK,
					Content = new ByteArrayContent(JsonSerializer.SerializeToUtf8Bytes(new ApiWrapper<T>(content)))
				});

			var httpClient = new HttpClient(mockMessageHandler.Object);
			httpClient.BaseAddress = new Uri(tramsApiEndpoint);
			httpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);

			return httpClientFactory;
		}
	}
}