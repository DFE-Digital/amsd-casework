using ConcernsCaseWork.Models;
using ConcernsCaseWork.Pages.Case;
using ConcernsCaseWork.Services.Ratings;
using ConcernsCaseWork.Services.Trusts;
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
using Service.Redis.Base;
using Service.Redis.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConcernsCaseWork.Tests.Pages.Case
{
	[Parallelizable(ParallelScope.All)]
	public class RatingPageModelTests
	{
		[Test]
		public async Task WhenOnGetAsync_Return_Page()
		{
			// arrange
			var mockLogger = new Mock<ILogger<RatingPageModel>>();
			var mockTrustModelService = new Mock<ITrustModelService>();
			var mockCachedService = new Mock<ICachedService>();
			var mockRatingModelService = new Mock<IRatingModelService>();

			var createCaseModel = CaseFactory.BuildCreateCaseModel();
			createCaseModel.CreateRecordsModel = RecordFactory.BuildListCreateRecordModel();
			var userState = new UserState { TrustUkPrn = "trust-ukprn", CreateCaseModel = createCaseModel };
			var expected = TrustFactory.BuildTrustDetailsModel();
			var ratingsModel = RatingFactory.BuildListRatingModel();
			
			mockCachedService.Setup(c => c.GetData<UserState>(It.IsAny<string>())).ReturnsAsync(userState);
			mockTrustModelService.Setup(s => s.GetTrustByUkPrn(It.IsAny<string>())).ReturnsAsync(expected);
			mockRatingModelService.Setup(r => r.GetRatingsModel()).ReturnsAsync(ratingsModel);
			
			var pageModel = SetupRatingPageModel(mockTrustModelService.Object, 
				mockCachedService.Object, 
				mockRatingModelService.Object,
				mockLogger.Object, true);
			
			// act
			await pageModel.OnGetAsync();
			
			// assert
			Assert.That(pageModel, Is.Not.Null);
			Assert.That(pageModel.TrustDetailsModel, Is.Not.Null);
			Assert.That(pageModel.CreateRecordsModel, Is.Not.Null);
			Assert.That(pageModel.RatingsModel, Is.Not.Null);
			Assert.IsNull(pageModel.TempData["Error.Message"]);
			
			var trustDetailsPageModel = pageModel.TrustDetailsModel;
			var createRecordsPageModel = pageModel.CreateRecordsModel;
			var ratingsPageModel = pageModel.RatingsModel;
			
			Assert.IsAssignableFrom<TrustDetailsModel>(trustDetailsPageModel);
			Assert.IsAssignableFrom<List<CreateRecordModel>>(createRecordsPageModel);
			Assert.IsAssignableFrom<List<RatingModel>>(ratingsPageModel);
			
			// Verify ILogger
			mockLogger.Verify(
				m => m.Log(
					LogLevel.Information,
					It.IsAny<EventId>(),
					It.Is<It.IsAnyType>((v, _) => v.ToString().Contains("RatingPageModel")),
					null,
					It.IsAny<Func<It.IsAnyType, Exception, string>>()),
				Times.Once);
			
			mockCachedService.Verify(c => c.GetData<UserState>(It.IsAny<string>()), Times.Once);
			mockTrustModelService.Verify(s => s.GetTrustByUkPrn(It.IsAny<string>()), Times.Once);
			mockRatingModelService.Verify(r => r.GetRatingsModel(), Times.Once);
		}
		
		[Test]
		public async Task WhenOnGetAsync_TrustUkprnIsNullOrEmpty_Return_Page()
		{
			// arrange
			var mockLogger = new Mock<ILogger<RatingPageModel>>();
			var mockTrustModelService = new Mock<ITrustModelService>();
			var mockCachedService = new Mock<ICachedService>();
			var mockRatingModelService = new Mock<IRatingModelService>();

			var createCaseModel = CaseFactory.BuildCreateCaseModel();
			createCaseModel.CreateRecordsModel = RecordFactory.BuildListCreateRecordModel();
			var userState = new UserState { CreateCaseModel = createCaseModel };
			var expected = TrustFactory.BuildTrustDetailsModel();
			var ratingsModel = RatingFactory.BuildListRatingModel();
			
			mockCachedService.Setup(c => c.GetData<UserState>(It.IsAny<string>())).ReturnsAsync(userState);
			mockTrustModelService.Setup(s => s.GetTrustByUkPrn(It.IsAny<string>())).ReturnsAsync(expected);
			mockRatingModelService.Setup(r => r.GetRatingsModel()).ReturnsAsync(ratingsModel);
			
			var pageModel = SetupRatingPageModel(mockTrustModelService.Object, 
				mockCachedService.Object, 
				mockRatingModelService.Object,
				mockLogger.Object, true);
			
			// act
			await pageModel.OnGetAsync();
			
			// assert
			Assert.That(pageModel, Is.Not.Null);
			Assert.IsNull(pageModel.TrustDetailsModel);
			Assert.IsNull(pageModel.CreateRecordsModel);
			Assert.IsNull(pageModel.RatingsModel);
			Assert.IsNotNull(pageModel.TempData["Error.Message"]);
			
			// Verify ILogger
			mockLogger.Verify(
				m => m.Log(
					LogLevel.Error,
					It.IsAny<EventId>(),
					It.Is<It.IsAnyType>((v, _) => v.ToString().Contains("RatingPageModel")),
					null,
					It.IsAny<Func<It.IsAnyType, Exception, string>>()),
				Times.Once);
			
			mockCachedService.Verify(c => c.GetData<UserState>(It.IsAny<string>()), Times.Once);
			mockTrustModelService.Verify(s => s.GetTrustByUkPrn(It.IsAny<string>()), Times.Never);
			mockRatingModelService.Verify(r => r.GetRatingsModel(), Times.Never);
		}		
		
		[Test]
		public async Task WhenOnGetAsync_UserStateIsNull_Return_Page()
		{
			// arrange
			var mockLogger = new Mock<ILogger<RatingPageModel>>();
			var mockTrustModelService = new Mock<ITrustModelService>();
			var mockCachedService = new Mock<ICachedService>();
			var mockRatingModelService = new Mock<IRatingModelService>();
			
			mockCachedService.Setup(c => c.GetData<UserState>(It.IsAny<string>())).ReturnsAsync((UserState)null);
			
			var pageModel = SetupRatingPageModel(mockTrustModelService.Object, 
				mockCachedService.Object, 
				mockRatingModelService.Object,
				mockLogger.Object, true);
			
			// act
			await pageModel.OnGetAsync();
			
			// assert
			Assert.That(pageModel, Is.Not.Null);
			Assert.IsNull(pageModel.TrustDetailsModel);
			Assert.IsNull(pageModel.CreateRecordsModel);
			Assert.IsNotNull(pageModel.TempData["Error.Message"]);
			
			// Verify ILogger
			mockLogger.Verify(
				m => m.Log(
					LogLevel.Error,
					It.IsAny<EventId>(),
					It.Is<It.IsAnyType>((v, _) => v.ToString().Contains("RatingPageModel")),
					null,
					It.IsAny<Func<It.IsAnyType, Exception, string>>()),
				Times.Once);
			
			mockCachedService.Verify(c => c.GetData<UserState>(It.IsAny<string>()), Times.Once);
			mockTrustModelService.Verify(s => s.GetTrustByUkPrn(It.IsAny<string>()), Times.Never);
			mockRatingModelService.Verify(r => r.GetRatingsModel(), Times.Never);
		}		
		
		[Test]
		public async Task WhenOnGetCancel_Return_HomePage()
		{
			// arrange
			var mockLogger = new Mock<ILogger<RatingPageModel>>();
			var mockTrustModelService = new Mock<ITrustModelService>();
			var mockCachedService = new Mock<ICachedService>();
			var mockRatingModelService = new Mock<IRatingModelService>();
			
			mockCachedService.Setup(c => c.GetData<UserState>(It.IsAny<string>())).ReturnsAsync(new UserState());
			mockCachedService.Setup(c => c.StoreData(It.IsAny<string>(), It.IsAny<UserState>(), It.IsAny<int>()));
			
			var pageModel = SetupRatingPageModel(mockTrustModelService.Object, 
				mockCachedService.Object, 
				mockRatingModelService.Object,
				mockLogger.Object, true);
			
			// act
			var pageResponse = await pageModel.OnGetCancel();
			var pageResponseInstance = pageResponse as RedirectResult;
			
			// assert
			Assert.That(pageResponse, Is.InstanceOf<RedirectResult>());
			Assert.IsNotNull(pageResponseInstance);
			Assert.That(pageResponseInstance.Url, Is.EqualTo("/"));
		}
		
		[Test]
		public async Task WhenOnGetCancel_UserStateIsNull_Return_Page()
		{
			// arrange
			var mockLogger = new Mock<ILogger<RatingPageModel>>();
			var mockTrustModelService = new Mock<ITrustModelService>();
			var mockCachedService = new Mock<ICachedService>();
			var mockRatingModelService = new Mock<IRatingModelService>();
			
			mockCachedService.Setup(c => c.GetData<UserState>(It.IsAny<string>())).ReturnsAsync((UserState)null);
			mockCachedService.Setup(c => c.StoreData(It.IsAny<string>(), It.IsAny<UserState>(), It.IsAny<int>()));
			
			var pageModel = SetupRatingPageModel(mockTrustModelService.Object, 
				mockCachedService.Object, 
				mockRatingModelService.Object,
				mockLogger.Object, true);
			
			// act
			var pageResponse = await pageModel.OnGetCancel();
			var pageResponseInstance = pageResponse as PageResult;
			
			// assert
			Assert.That(pageResponse, Is.InstanceOf<PageResult>());
			Assert.IsNotNull(pageResponseInstance);
			Assert.IsNotNull(pageModel.TempData["Error.Message"]);
		}

		[Test]
		public async Task WhenOnPostAsync_RedirectToPage_Details()
		{
			// arrange
			var mockLogger = new Mock<ILogger<RatingPageModel>>();
			var mockTrustModelService = new Mock<ITrustModelService>();
			var mockCachedService = new Mock<ICachedService>();
			var mockRatingModelService = new Mock<IRatingModelService>();
			
			var expected = CaseFactory.BuildCreateCaseModel();
			var userState = new UserState { TrustUkPrn = "trust-ukprn", CreateCaseModel = expected };
			
			mockCachedService.Setup(c => c.GetData<UserState>(It.IsAny<string>())).ReturnsAsync(userState);
			
			var pageModel = SetupRatingPageModel(mockTrustModelService.Object, 
				mockCachedService.Object, 
				mockRatingModelService.Object,
				mockLogger.Object, true);
			
			pageModel.HttpContext.Request.Form = new FormCollection(
				new Dictionary<string, StringValues>
				{
					{ "rating", new StringValues("123:red") }
				});
			
			// act
			var pageResponse = await pageModel.OnPostAsync();
			var pageResponseInstance = pageResponse as RedirectToPageResult;
			
			// assert
			Assert.NotNull(pageResponseInstance);
			Assert.That(pageResponseInstance.PageName, Is.EqualTo("details"));

			mockCachedService.Verify(c => c.GetData<UserState>(It.IsAny<string>()), Times.Once);
			mockCachedService.Verify(c => c.StoreData(It.IsAny<string>(), It.IsAny<UserState>(), It.IsAny<int>()), Times.Once);
		}
		
		[Test]
		public async Task WhenOnPostAsync_MissingFormData_ReloadPage()
		{
			// arrange
			var mockLogger = new Mock<ILogger<RatingPageModel>>();
			var mockTrustModelService = new Mock<ITrustModelService>();
			var mockCachedService = new Mock<ICachedService>();
			var mockRatingModelService = new Mock<IRatingModelService>();
			
			var createCaseModel = CaseFactory.BuildCreateCaseModel();
			createCaseModel.CreateRecordsModel = RecordFactory.BuildListCreateRecordModel();
			var userState = new UserState { TrustUkPrn = "trust-ukprn", CreateCaseModel = createCaseModel };
			var trustDetailsModel = TrustFactory.BuildTrustDetailsModel();
			var ratingsModel = RatingFactory.BuildListRatingModel();

			mockTrustModelService.Setup(t => t.GetTrustByUkPrn(It.IsAny<string>())).ReturnsAsync(trustDetailsModel);
			mockRatingModelService.Setup(r => r.GetRatingsModel()).ReturnsAsync(ratingsModel);
			mockCachedService.Setup(c => c.GetData<UserState>(It.IsAny<string>())).ReturnsAsync(userState);
			
			var pageModel = SetupRatingPageModel(mockTrustModelService.Object, 
				mockCachedService.Object, 
				mockRatingModelService.Object,
				mockLogger.Object, true);
			
			// act
			var pageResponse = await pageModel.OnPostAsync();
			var pageResponseInstance = pageResponse as PageResult;
			
			// assert
			Assert.NotNull(pageResponseInstance);
			Assert.NotNull(pageModel.RatingsModel);
			Assert.NotNull(pageModel.CreateRecordsModel);
			Assert.NotNull(pageModel.TrustDetailsModel);

			mockCachedService.Verify(c => c.GetData<UserState>(It.IsAny<string>()), Times.Once);
			mockCachedService.Verify(c => c.StoreData(It.IsAny<string>(), It.IsAny<UserState>(), It.IsAny<int>()), Times.Never);
			mockTrustModelService.Verify(t => t.GetTrustByUkPrn(It.IsAny<string>()), Times.Once);
			mockRatingModelService.Verify(r => r.GetRatingsModel(), Times.Once);
		}		
		
		[Test]
		public async Task WhenOnPostAsync_IncorrectFormData_ReloadPage()
		{
			// arrange
			var mockLogger = new Mock<ILogger<RatingPageModel>>();
			var mockTrustModelService = new Mock<ITrustModelService>();
			var mockCachedService = new Mock<ICachedService>();
			var mockRatingModelService = new Mock<IRatingModelService>();
			
			var createCaseModel = CaseFactory.BuildCreateCaseModel();
			createCaseModel.CreateRecordsModel = RecordFactory.BuildListCreateRecordModel();
			var userState = new UserState { TrustUkPrn = "trust-ukprn", CreateCaseModel = createCaseModel };
			var trustDetailsModel = TrustFactory.BuildTrustDetailsModel();
			var ratingsModel = RatingFactory.BuildListRatingModel();

			mockTrustModelService.Setup(t => t.GetTrustByUkPrn(It.IsAny<string>())).ReturnsAsync(trustDetailsModel);
			mockRatingModelService.Setup(r => r.GetRatingsModel()).ReturnsAsync(ratingsModel);
			mockCachedService.Setup(c => c.GetData<UserState>(It.IsAny<string>())).ReturnsAsync(userState);
			
			var pageModel = SetupRatingPageModel(mockTrustModelService.Object, 
				mockCachedService.Object, 
				mockRatingModelService.Object,
				mockLogger.Object, true);
			
			pageModel.HttpContext.Request.Form = new FormCollection(
				new Dictionary<string, StringValues>
				{
					{ "rating", new StringValues("") }
				});
			
			// act
			var pageResponse = await pageModel.OnPostAsync();
			var pageResponseInstance = pageResponse as PageResult;
			
			// assert
			Assert.NotNull(pageResponseInstance);
			Assert.NotNull(pageModel.RatingsModel);
			Assert.NotNull(pageModel.CreateRecordsModel);
			Assert.NotNull(pageModel.TrustDetailsModel);

			mockCachedService.Verify(c => c.GetData<UserState>(It.IsAny<string>()), Times.Once);
			mockCachedService.Verify(c => c.StoreData(It.IsAny<string>(), It.IsAny<UserState>(), It.IsAny<int>()), Times.Never);
			mockTrustModelService.Verify(t => t.GetTrustByUkPrn(It.IsAny<string>()), Times.Once);
			mockRatingModelService.Verify(r => r.GetRatingsModel(), Times.Once);
		}
		
		private static RatingPageModel SetupRatingPageModel(
			ITrustModelService mockTrustModelService, 
			ICachedService mockCachedService,
			IRatingModelService mockRatingModelService,
			ILogger<RatingPageModel> mockLogger, bool isAuthenticated = false)
		{
			(PageContext pageContext, TempDataDictionary tempData, ActionContext actionContext) = PageContextFactory.PageContextBuilder(isAuthenticated);
			
			return new RatingPageModel(mockTrustModelService, mockCachedService, mockRatingModelService, mockLogger)
			{
				PageContext = pageContext,
				TempData = tempData,
				Url = new UrlHelper(actionContext),
				MetadataProvider = pageContext.ViewData.ModelMetadata
			};
		}
	}
}