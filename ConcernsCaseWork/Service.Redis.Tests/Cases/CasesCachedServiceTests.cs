﻿using Moq;
using NUnit.Framework;
using Service.Redis.Base;
using Service.Redis.Cases;
using Service.Redis.Models;
using System.Threading.Tasks;

namespace Service.Redis.Tests.Cases
{
	[Parallelizable(ParallelScope.All)]
	public class CasesCachedServiceTests
	{
		[Test]
		public async Task WhenCreateCaseDataCache_IsSuccessful()
		{
			// arrange
			var mockCacheProvider = new Mock<ICacheProvider>();
			var casesCachedService = new CasesCachedService(mockCacheProvider.Object);
			var caseStateData = new CaseStateData
			{
				TrustUkPrn = "999999"
			};
			
			mockCacheProvider.Setup(c => c.CacheTimeToLive()).Returns(120);
			mockCacheProvider.Setup(c => c.GetFromCache<CaseStateData>(It.IsAny<string>())).
				Returns(Task.FromResult(caseStateData));

			// act
			await casesCachedService.CreateCaseData("username", caseStateData);
			var cachedCaseStateData = await casesCachedService.GetCaseData<CaseStateData>("username");

			// assert
			Assert.That(cachedCaseStateData, Is.Not.Null);
			Assert.That(cachedCaseStateData, Is.InstanceOf<CaseStateData>());
			Assert.That(cachedCaseStateData.TrustUkPrn, Is.EqualTo(caseStateData.TrustUkPrn));
		}
	}
}