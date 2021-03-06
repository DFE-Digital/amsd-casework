using ConcernsCaseWork.Integration.Tests.Factory;
using ConcernsCaseWork.Shared.Tests.Factory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Service.TRAMS.Cases;
using Service.TRAMS.Ratings;
using Service.TRAMS.Records;
using Service.TRAMS.Trusts;
using Service.TRAMS.Types;
using System;
using System.Threading.Tasks;

namespace ConcernsCaseWork.Integration.Tests.Trams
{
	[TestFixture]
	public class RecordServiceIntegrationTests
	{
		/// Testing the class requires a running Redis,
		/// startup is configured to use Redis with session storage.
		private IConfigurationRoot _configuration;
		private WebAppFactory _factory;

		/// Variables for caseworker and trustukprn, creates cases on Academies API.
		/// Future work can be to delete the records from the SQLServer.
		
		private const string CaseWorker = "case.service.integration";

		[OneTimeSetUp]
		public void OneTimeSetup()
		{
			_configuration = new ConfigurationBuilder().ConfigurationUserSecretsBuilder().Build();
			_factory = new WebAppFactory(_configuration);
		}
		
		[OneTimeTearDown]
		public void OneTimeTearDown()
		{
			_factory.Dispose();
		}

		[Test]
		public async Task WhenGetRecordsByCaseUrn_ReturnsListRecordDto()
		{
			// arrange
			var recordService = _factory.Services.GetRequiredService<IRecordService>();
			var caseUrn = await FetchRandomCaseUrn();
			var typeUrn = await FetchRandomTypeUrn();
			var ratingUrn = await FetchRandomRatingUrn();
			var createRecordDto = RecordFactory.BuildCreateRecordDto(caseUrn, typeUrn, ratingUrn);
			await PostRecord(createRecordDto);

			//act 
			var recordsDto = await recordService.GetRecordsByCaseUrn(caseUrn);

			// Assert
			Assert.That(recordsDto, Is.Not.Null);
			Assert.That(recordsDto.Count, Is.EqualTo(1));
		}

		[Test]
		public async Task WhenPatchRecordByUrn_UpdatesRecord()
		{
			// arrange 
			var recordService = _factory.Services.GetRequiredService<IRecordService>();
			var caseUrn = await FetchRandomCaseUrn();
			var typeUrn = await FetchRandomTypeUrn();
			var ratingUrn = await FetchRandomRatingUrn();
			var updatedRatingUrn = await FetchRandomRatingUrn();
			var createRecordDto = RecordFactory.BuildCreateRecordDto(caseUrn, typeUrn, ratingUrn);
			var postRecordDto = await PostRecord(createRecordDto);

			// Update record properties
			var timeNow = DateTimeOffset.Now;
			var toUpdateRecordDto = new RecordDto(
				postRecordDto.CreatedAt,
				timeNow,
				timeNow,
				timeNow,
				postRecordDto.Name,
				"some updated description",
				"some updated reason",
				postRecordDto.CaseUrn,
				postRecordDto.TypeUrn,
				updatedRatingUrn,
				postRecordDto.Urn,
				postRecordDto.StatusUrn
				);

			// act 
			var updatedRecordDto = await recordService.PatchRecordByUrn(toUpdateRecordDto);

			// assert
			Assert.That(updatedRecordDto, Is.Not.Null);
			Assert.That(updatedRecordDto.CreatedAt, Is.EqualTo(postRecordDto.CreatedAt));
			Assert.That(updatedRecordDto.UpdatedAt, Is.EqualTo(timeNow));
			Assert.That(updatedRecordDto.ReviewAt, Is.EqualTo(timeNow));
			Assert.That(updatedRecordDto.ClosedAt, Is.EqualTo(timeNow));
			Assert.That(updatedRecordDto.Name, Is.EqualTo(postRecordDto.Name));
			Assert.That(updatedRecordDto.Description, Is.EqualTo("some updated description"));
			Assert.That(updatedRecordDto.Reason, Is.EqualTo("some updated reason"));
			Assert.That(updatedRecordDto.CaseUrn, Is.EqualTo(postRecordDto.CaseUrn));
			Assert.That(updatedRecordDto.TypeUrn, Is.EqualTo(postRecordDto.TypeUrn));
			Assert.That(updatedRecordDto.RatingUrn, Is.EqualTo(updatedRatingUrn));
			Assert.That(updatedRecordDto.Urn, Is.EqualTo(postRecordDto.Urn));
			Assert.That(updatedRecordDto.StatusUrn, Is.EqualTo(postRecordDto.StatusUrn));
		}

		private async Task<RecordDto> PostRecord(CreateRecordDto createRecordDto)
		{
			var recordService = _factory.Services.GetRequiredService<IRecordService>();
			return await recordService.PostRecordByCaseUrn(createRecordDto);
		}

		private async Task<string> FetchRandomTrustUkprn()
		{
			const string searchParameter = "Senior";
			var trustService = _factory.Services.GetRequiredService<ITrustService>();
			var apiWrapperTrusts = await trustService.GetTrustsByPagination(
				TrustFactory.BuildTrustSearch(searchParameter, searchParameter, searchParameter));

			Assert.That(apiWrapperTrusts, Is.Not.Null);
			Assert.That(apiWrapperTrusts.Data, Is.Not.Null);

			var random = new Random();
			int index = random.Next(apiWrapperTrusts.Data.Count);

			return apiWrapperTrusts.Data[index].UkPrn;
		}
		
		private async Task<long> FetchRandomCaseUrn()
		{
			var caseService = _factory.Services.GetRequiredService<ICaseService>();
			var trustUkprn = await FetchRandomTrustUkprn();
			var createCaseDto = CaseFactory.BuildCreateCaseDto(CaseWorker, trustUkprn);
			var caseDto = await caseService.PostCase(createCaseDto);

			return caseDto.Urn;
		}

		private async Task<long> FetchRandomTypeUrn()
		{
			var typeService = _factory.Services.GetRequiredService<ITypeService>();
			var types = await typeService.GetTypes();

			var random = new Random();
			var index = random.Next(types.Count);

			return types[index].Urn;
		}

		private async Task<long> FetchRandomRatingUrn()
		{
			var ratingService = _factory.Services.GetRequiredService<IRatingService>();
			var ratings = await ratingService.GetRatings();

			var random = new Random();
			var index = random.Next(ratings.Count);

			return ratings[index].Urn;
		}
	}
}