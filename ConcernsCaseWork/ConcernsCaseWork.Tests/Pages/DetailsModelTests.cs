﻿using ConcernsCaseWork.Models;
using ConcernsCaseWork.Pages.Case;
using ConcernsCaseWork.Services.Trusts;
using ConcernsCaseWork.Shared.Tests.Factory;
using ConcernsCaseWork.Shared.Tests.Shared;
using ConcernsCaseWork.Tests.Factory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Service.Redis.Cases;
using Service.Redis.Models;
using System;
using System.Threading.Tasks;

namespace ConcernsCaseWork.Tests.Pages
{
	[Parallelizable(ParallelScope.All)]
	public class DetailsModelTests
	{
		[Test]
		public async Task WhenOnGetAsync_ReturnsTrustDetailsModel()
		{
			// arrange
			var mockLogger = new Mock<ILogger<DetailsModel>>();
			var mockTrustModelService = new Mock<ITrustModelService>();
			var mockCasesCachedService = new Mock<ICasesCachedService>();
			var expected = TrustFactory.CreateTrustDetailsModel();

			mockCasesCachedService.Setup(c => c.GetCaseData<CaseStateData>(It.IsAny<string>())).ReturnsAsync(new CaseStateData { TrustUkPrn = "trustukprn" });
			mockTrustModelService.Setup(s => s.GetTrustByUkPrn(It.IsAny<string>())).ReturnsAsync(expected);
			
			var pageModel = SetupDetailsModel(mockTrustModelService.Object, mockCasesCachedService.Object, mockLogger.Object, true);
			
			// act
			await pageModel.OnGetAsync();
			var trustsDetailsModel = pageModel.TrustDetailsModel;
			
			// assert
			Assert.That(pageModel.TempData["Error.Message"], Is.Null);
			Assert.IsAssignableFrom<TrustDetailsModel>(trustsDetailsModel);
			Assert.That(trustsDetailsModel, Is.Not.Null);
			Assert.That(trustsDetailsModel.GiasData, Is.Not.Null);
			Assert.That(trustsDetailsModel.GiasData.GroupId, Is.EqualTo(expected.GiasData.GroupId));
			Assert.That(trustsDetailsModel.GiasData.GroupName, Is.EqualTo(expected.GiasData.GroupName));
			Assert.That(trustsDetailsModel.GiasData.UkPrn, Is.EqualTo(expected.GiasData.UkPrn));
			Assert.That(trustsDetailsModel.GiasData.CompaniesHouseNumber, Is.EqualTo(expected.GiasData.CompaniesHouseNumber));
			Assert.That(trustsDetailsModel.GiasData.GroupContactAddress, Is.Not.Null);
			Assert.That(trustsDetailsModel.GiasData.GroupContactAddress.County, Is.EqualTo(expected.GiasData.GroupContactAddress.County));
			Assert.That(trustsDetailsModel.GiasData.GroupContactAddress.Locality, Is.EqualTo(expected.GiasData.GroupContactAddress.Locality));
			Assert.That(trustsDetailsModel.GiasData.GroupContactAddress.Postcode, Is.EqualTo(expected.GiasData.GroupContactAddress.Postcode));
			Assert.That(trustsDetailsModel.GiasData.GroupContactAddress.Street, Is.EqualTo(expected.GiasData.GroupContactAddress.Street));
			Assert.That(trustsDetailsModel.GiasData.GroupContactAddress.Town, Is.EqualTo(expected.GiasData.GroupContactAddress.Town));
			Assert.That(trustsDetailsModel.GiasData.GroupContactAddress.AdditionalLine, Is.EqualTo(expected.GiasData.GroupContactAddress.AdditionalLine));
			Assert.That(trustsDetailsModel.GiasData.GroupContactAddress.DisplayAddress, Is.EqualTo(SharedBuilders.BuildDisplayAddress(expected.GiasData.GroupContactAddress)));
			
			// Verify ILogger
			mockLogger.Verify(
				m => m.Log(
					LogLevel.Information,
					It.IsAny<EventId>(),
					It.Is<It.IsAnyType>((v, _) => v.ToString().Contains("DetailsModel")),
					null,
					It.IsAny<Func<It.IsAnyType, Exception, string>>()),
				Times.Once);
		}
		
		[Test]
		public async Task WhenOnGetAsync_ReturnsErrorLoadingPage_ExceptionTrustByUkPrnService()
		{
			// arrange
			var mockLogger = new Mock<ILogger<DetailsModel>>();
			var mockTrustModelService = new Mock<ITrustModelService>();
			var mockCasesCachedService = new Mock<ICasesCachedService>();
			
			mockCasesCachedService.Setup(c => c.GetCaseData<CaseStateData>(It.IsAny<string>())).ReturnsAsync(new CaseStateData { TrustUkPrn = "trustukprn" });
			mockTrustModelService.Setup(s => s.GetTrustByUkPrn(It.IsAny<string>())).ThrowsAsync(new Exception("some error"));
			
			var pageModel = SetupDetailsModel(mockTrustModelService.Object, mockCasesCachedService.Object, mockLogger.Object, true);
			
			// act
			await pageModel.OnGetAsync();
			
			// assert
			Assert.That(pageModel.TempData["Error.Message"], Is.EqualTo("An error occurred loading the page, please try again. If the error persists contact the service administrator."));
			
			// Verify ILogger
			mockLogger.Verify(
				m => m.Log(
					LogLevel.Information,
					It.IsAny<EventId>(),
					It.Is<It.IsAnyType>((v, _) => v.ToString().Contains("DetailsModel")),
					null,
					It.IsAny<Func<It.IsAnyType, Exception, string>>()),
				Times.Once);
			
			mockLogger.Verify(
				m => m.Log(
					LogLevel.Error,
					It.IsAny<EventId>(),
					It.Is<It.IsAnyType>((v, _) => v.ToString().Contains("DetailsModel")),
					null,
					It.IsAny<Func<It.IsAnyType, Exception, string>>()),
				Times.Once);
		}
		
		[Test]
		public async Task WhenOnGetAsync_ReturnsErrorLoadingPage_MissingRedisCaseStateData()
		{
			// arrange
			var mockLogger = new Mock<ILogger<DetailsModel>>();
			var mockTrustModelService = new Mock<ITrustModelService>();
			var mockCasesCachedService = new Mock<ICasesCachedService>();
			
			var pageModel = SetupDetailsModel(mockTrustModelService.Object, mockCasesCachedService.Object, mockLogger.Object, true);
			
			// act
			await pageModel.OnGetAsync();
			
			// assert
			Assert.That(pageModel.TempData["Error.Message"], Is.EqualTo("An error occurred loading the page, please try again. If the error persists contact the service administrator."));
			
			// Verify ILogger
			mockLogger.Verify(
				m => m.Log(
					LogLevel.Information,
					It.IsAny<EventId>(),
					It.Is<It.IsAnyType>((v, _) => v.ToString().Contains("DetailsModel")),
					null,
					It.IsAny<Func<It.IsAnyType, Exception, string>>()),
				Times.Once);
			
			mockLogger.Verify(
				m => m.Log(
					LogLevel.Error,
					It.IsAny<EventId>(),
					It.Is<It.IsAnyType>((v, _) => v.ToString().Contains("DetailsModel")),
					null,
					It.IsAny<Func<It.IsAnyType, Exception, string>>()),
				Times.Once);
		}

		[TestCase("record", "Record")]
		[TestCase("safeguarding", "Safeguarding")]
		[TestCase("concern", "Concern")]
		public void WhenOnPost_RedirectToPageSuccess(string caseType, string expectedRedirect)
		{
			// arrange
			var mockLogger = new Mock<ILogger<DetailsModel>>();
			var mockTrustModelService = new Mock<ITrustModelService>();
			var mockCasesCachedService = new Mock<ICasesCachedService>();
			
			var pageModel = SetupDetailsModel(mockTrustModelService.Object, mockCasesCachedService.Object, mockLogger.Object, true);
			
			// act
			var pageResponse = pageModel.OnPost(caseType);

			// assert
			Assert.That(pageResponse, Is.InstanceOf<RedirectToPageResult>());
			var page = pageResponse as RedirectToPageResult;
			
			Assert.That(page, Is.Not.Null);
			Assert.That(page.PageName, Is.EqualTo(expectedRedirect));
		}

		[TestCase("")]
		[TestCase(null)]
		public void WhenOnPost_ReturnPageWhenCaseTypeInput_IsEmptyOrNull(string caseType)
		{
			// arrange
			var mockLogger = new Mock<ILogger<DetailsModel>>();
			var mockTrustModelService = new Mock<ITrustModelService>();
			var mockCasesCachedService = new Mock<ICasesCachedService>();
			
			var pageModel = SetupDetailsModel(mockTrustModelService.Object, mockCasesCachedService.Object, mockLogger.Object, true);
			
			// act
			var pageResponse = pageModel.OnPost(caseType);

			// assert
			Assert.That(pageModel.TempData["Error.Message"], Is.EqualTo("An error occurred posting the form, please try again. If the error persists contact the service administrator."));
			Assert.That(pageResponse, Is.InstanceOf<PageResult>());
			var page = pageResponse as PageResult;
			
			Assert.That(page, Is.Not.Null);
		}
		
		private static DetailsModel SetupDetailsModel(ITrustModelService mockTrustModelService, ICasesCachedService mockCasesCachedService, ILogger<DetailsModel> mockLogger, bool isAuthenticated = false)
		{
			(PageContext pageContext, TempDataDictionary tempData, ActionContext actionContext) = PageContextFactory.PageContextBuilder(isAuthenticated);
			
			return new DetailsModel(mockTrustModelService, mockCasesCachedService, mockLogger)
			{
				PageContext = pageContext,
				TempData = tempData,
				Url = new UrlHelper(actionContext),
				MetadataProvider = pageContext.ViewData.ModelMetadata
			};
		}
	}
}