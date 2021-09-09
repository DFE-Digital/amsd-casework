﻿using ConcernsCaseWork.Shared.Tests.Factory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using Service.TRAMS.Configuration;
using Service.TRAMS.RecordAcademy;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.TRAMS.Tests.Trusts
{
	[Parallelizable(ParallelScope.All)]
	public class TrustSearchServiceTests
	{
		[Test]
		public async Task WhenGetTrustsBySearchCriteria_ReturnsTrustsFromTrams()
		{
			// arrange
			var mockTrustService = new Mock<ITrustService>();
			var mockIOptionsTrustSearch = new Mock<IOptions<TrustSearchOptions>>();
			var mockLogger = new Mock<ILogger<TrustSearchService>>();

			var expectedTrusts = TrustFactory.CreateListTrustSummaryDto();
			IList<TrustSummaryDto> emptyList = Array.Empty<TrustSummaryDto>();

			mockIOptionsTrustSearch.Setup(o => o.Value).Returns(new TrustSearchOptions { TrustsLimitByPage = 10});
			mockTrustService.SetupSequence(t => t.GetTrustsByPagination(It.IsAny<TrustSearch>()))
				.Returns(Task.FromResult(expectedTrusts))
				.Returns(Task.FromResult(emptyList));
			
			var trustSearchService = new TrustSearchService(mockTrustService.Object, mockIOptionsTrustSearch.Object, mockLogger.Object);

			// act
			var trusts = await trustSearchService.GetTrustsBySearchCriteria(TrustFactory.CreateTrustSearch());

			// assert
			Assert.That(trusts, Is.Not.Null);
			Assert.That(trusts.Count, Is.EqualTo(expectedTrusts.Count));

			foreach (var trust in trusts)
			{
				foreach (var expectedTrust in expectedTrusts)
				{
					Assert.That(trust.Establishments.Count, Is.EqualTo(expectedTrust.Establishments.Count));
					Assert.That(trust.Urn, Is.EqualTo(expectedTrust.Urn));
					Assert.That(trust.GroupName, Is.EqualTo(expectedTrust.GroupName));
					Assert.That(trust.UkPrn, Is.EqualTo(expectedTrust.UkPrn));
					Assert.That(trust.CompaniesHouseNumber, Is.EqualTo(expectedTrust.CompaniesHouseNumber));

					foreach (var establishment in trust.Establishments)
					{
						foreach (var expectedEstablishment in expectedTrust.Establishments)
						{
							Assert.That(establishment.Name, Is.EqualTo(expectedEstablishment.Name));
							Assert.That(establishment.Urn, Is.EqualTo(expectedEstablishment.Urn));
							Assert.That(establishment.UkPrn, Is.EqualTo(expectedEstablishment.UkPrn));
						}
					}
				}
			}
		}
		
		[Test]
		public async Task WhenGetTrustsBySearchCriteria_ReturnsTrustsFromTrams_LimitedByMaxPages()
		{
			// arrange
			var mockTrustService = new Mock<ITrustService>();
			var mockIOptionsTrustSearch = new Mock<IOptions<TrustSearchOptions>>();
			var mockLogger = new Mock<ILogger<TrustSearchService>>();

			var expectedTrusts = TrustFactory.CreateListTrustSummaryDto();

			mockIOptionsTrustSearch.Setup(o => o.Value).Returns(new TrustSearchOptions { TrustsLimitByPage = 10});
			mockTrustService.SetupSequence(t => t.GetTrustsByPagination(It.IsAny<TrustSearch>()))
				.Returns(Task.FromResult(expectedTrusts))
				.Returns(Task.FromResult(expectedTrusts))
				.Returns(Task.FromResult(expectedTrusts))
				.Returns(Task.FromResult(expectedTrusts))
				.Returns(Task.FromResult(expectedTrusts))
				.Returns(Task.FromResult(expectedTrusts))
				.Returns(Task.FromResult(expectedTrusts))
				.Returns(Task.FromResult(expectedTrusts))
				.Returns(Task.FromResult(expectedTrusts))
				.Returns(Task.FromResult(expectedTrusts))
				.Returns(Task.FromResult(expectedTrusts));
			
			var trustSearchService = new TrustSearchService(mockTrustService.Object, mockIOptionsTrustSearch.Object, mockLogger.Object);

			// act
			var trusts = await trustSearchService.GetTrustsBySearchCriteria(TrustFactory.CreateTrustSearch());

			// assert
			Assert.That(trusts, Is.Not.Null);
			Assert.That(trusts.Count, Is.EqualTo(expectedTrusts.Count * 11));
		}
	}
}