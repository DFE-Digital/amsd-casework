﻿using AutoMapper;
using ConcernsCaseWork.Mappers;
using ConcernsCaseWork.Models;
using ConcernsCaseWork.Pages.Case;
using ConcernsCaseWork.Services.Cases;
using ConcernsCaseWork.Services.Rating;
using ConcernsCaseWork.Services.Records;
using ConcernsCaseWork.Shared.Tests.Factory;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConcernsCaseWork.Tests.Pages
{
	[Parallelizable(ParallelScope.All)]
	public class EditRiskRatingPageModelTests
	{
		[Test]
		public async Task WhenOnGet_ReturnsPage()
		{
			// arrange
			var mockCaseModelService = new Mock<ICaseModelService>();
			var mockRatingModelService = new Mock<IRatingModelService>();
			var mockRecordModelService = new Mock<IRecordModelService>();
			var mockLogger = new Mock<ILogger<EditRiskRatingPageModel>>();

			var caseModel = CaseFactory.BuildCaseModel();
			var recordDto = RecordFactory.BuildRecordDto();

			var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapping>());
			var mapper = config.CreateMapper();

			var recordModel = mapper.Map<RecordModel>(recordDto);

			mockCaseModelService.Setup(c => c.GetCaseByUrn(It.IsAny<string>(), It.IsAny<long>()))
				.ReturnsAsync(caseModel);

			mockRecordModelService.Setup(r => r.GetRecordModelByUrn(It.IsAny<string>(), It.IsAny<long>(), It.IsAny<long>()))
				.ReturnsAsync(recordModel);

			var pageModel = SetupEditRiskRatingPageModel(mockCaseModelService.Object, mockRatingModelService.Object, mockRecordModelService.Object, mockLogger.Object);
			var routeData = pageModel.RouteData.Values;
			routeData.Add("urn", 1); 
			routeData.Add("recordUrn", 1); 
			pageModel.Request.Headers.Add("Referer", "https://returnto/thispage");

			// act
			var pageResponse = await pageModel.OnGet();

			// assert
			Assert.That(pageResponse, Is.InstanceOf<PageResult>());
			var page = pageResponse as PageResult;

			Assert.That(page, Is.Not.Null);
			Assert.That(pageModel.CaseModel.PreviousUrl, Is.EqualTo("https://returnto/thispage"));
		}

		[Test]
		public async Task WhenOnGet_RouteData_ReturnsPage()
		{
			// arrange
			var mockCaseModelService = new Mock<ICaseModelService>();
			var mockRatingModelService = new Mock<IRatingModelService>();
			var mockRecordModelService = new Mock<IRecordModelService>();
			var mockLogger = new Mock<ILogger<EditRiskRatingPageModel>>();

			var caseModel = CaseFactory.BuildCaseModel();
			var recordDto = RecordFactory.BuildRecordDto();

			var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapping>());
			var mapper = config.CreateMapper();

			var recordModel = mapper.Map<RecordModel>(recordDto);

			mockCaseModelService.Setup(c => c.GetCaseByUrn(It.IsAny<string>(), It.IsAny<long>()))
				.ReturnsAsync(caseModel);

			mockRecordModelService.Setup(r => r.GetRecordModelByUrn(It.IsAny<string>(), It.IsAny<long>(), It.IsAny<long>()))
				.ReturnsAsync(recordModel);

			var pageModel = SetupEditRiskRatingPageModel(mockCaseModelService.Object, mockRatingModelService.Object, mockRecordModelService.Object, mockLogger.Object);

			var routeData = pageModel.RouteData.Values;
			routeData.Add("urn", 1);
			routeData.Add("recordUrn", 1);
			pageModel.Request.Headers.Add("Referer", "https://returnto/thispage");

			// act
			var pageResponse = await pageModel.OnGet();

			// assert
			Assert.That(pageResponse, Is.InstanceOf<PageResult>());
			var page = pageResponse as PageResult;

			Assert.That(page, Is.Not.Null);
			Assert.That(pageModel.CaseModel.PreviousUrl, Is.EqualTo("https://returnto/thispage"));
		}

		[Test]
		public void WhenOnPostEditRiskRating_MissingRouteData_ThrowsException()
		{
			// arrange
			var mockCaseModelService = new Mock<ICaseModelService>();
			var mockRatingModelService = new Mock<IRatingModelService>();
			var mockRecordModelService = new Mock<IRecordModelService>();
			var mockLogger = new Mock<ILogger<EditRiskRatingPageModel>>();

			var pageModel = SetupEditRiskRatingPageModel(mockCaseModelService.Object, mockRatingModelService.Object, mockRecordModelService.Object, mockLogger.Object);
			
			// act/assert
			Assert.ThrowsAsync<Exception>(() => pageModel.OnPostEditRiskRating("https://returnto/thispage"));
		}

		[Test]
		public async Task WhenOnPostEditRiskRating_RouteData_MissingRequestForm_ReloadPage()
		{
			// arrange
			var mockCaseModelService = new Mock<ICaseModelService>();
			var mockRatingModelService = new Mock<IRatingModelService>();
			var mockRecordModelService = new Mock<IRecordModelService>();
			var mockLogger = new Mock<ILogger<EditRiskRatingPageModel>>();

			var caseModel = CaseFactory.BuildCaseModel();

			mockCaseModelService.Setup(c => c.GetCaseByUrn(It.IsAny<string>(), It.IsAny<long>()))
				.ReturnsAsync(caseModel);
			var pageModel = SetupEditRiskRatingPageModel(mockCaseModelService.Object, mockRatingModelService.Object, mockRecordModelService.Object, mockLogger.Object);

			var routeData = pageModel.RouteData.Values;
			routeData.Add("urn", 1);
			routeData.Add("recordUrn", 1);
			pageModel.HttpContext.Request.Form = new FormCollection(
				new Dictionary<string, StringValues>
				{
					{ "ragRating", new StringValues("") }
				});

			// act
			var pageResponse = await pageModel.OnPostEditRiskRating("https://returnto/thispage");

			// assert
			Assert.That(pageResponse, Is.InstanceOf<PageResult>());
			var page = pageResponse as PageResult;

			Assert.That(page, Is.Not.Null);
			Assert.That(pageModel.CaseModel, Is.Not.Null);
			Assert.That(pageModel.CaseModel.PreviousUrl, Is.EqualTo("https://returnto/thispage"));
		}

		[Test]
		public async Task WhenOnPostEditRiskRating_RouteData_RequestForm_ReturnsToPreviousUrl()
		{
			// arrange
			var mockCaseModelService = new Mock<ICaseModelService>();
			var mockRatingModelService = new Mock<IRatingModelService>();
			var mockRecordModelService = new Mock<IRecordModelService>();
			var mockLogger = new Mock<ILogger<EditRiskRatingPageModel>>();

			mockCaseModelService.Setup(c => c.PatchRiskRating(It.IsAny<PatchCaseModel>()));

			var pageModel = SetupEditRiskRatingPageModel(mockCaseModelService.Object, mockRatingModelService.Object, mockRecordModelService.Object, mockLogger.Object);

			var routeData = pageModel.RouteData.Values;
			routeData.Add("urn", 1);
			routeData.Add("recordUrn", 1);
			pageModel.HttpContext.Request.Form = new FormCollection(
				new Dictionary<string, StringValues>
				{
					{ "ragRating", new StringValues("123:ragRating") }
				});

			// act
			var pageResponse = await pageModel.OnPostEditRiskRating("https://returnto/thispage");

			// assert
			Assert.That(pageResponse, Is.InstanceOf<RedirectResult>());
			var page = pageResponse as RedirectResult;

			Assert.That(page, Is.Not.Null);
			Assert.That(pageModel.CaseModel, Is.Null);
			Assert.That(page.Url, Is.EqualTo("https://returnto/thispage"));
		}

		private static EditRiskRatingPageModel SetupEditRiskRatingPageModel(
			ICaseModelService mockCaseModelService, IRatingModelService mockRatingModelService, IRecordModelService mockRecordModelService, ILogger<EditRiskRatingPageModel> mockLogger, bool isAuthenticated = false)
		{
			(PageContext pageContext, TempDataDictionary tempData, ActionContext actionContext) = PageContextFactory.PageContextBuilder(isAuthenticated);

			return new EditRiskRatingPageModel(mockCaseModelService, mockRatingModelService, mockRecordModelService, mockLogger)
			{
				PageContext = pageContext,
				TempData = tempData,
				Url = new UrlHelper(actionContext),
				MetadataProvider = pageContext.ViewData.ModelMetadata
			};
		}
	}
}