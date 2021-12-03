﻿using ConcernsCaseWork.Pages.Admin;
using ConcernsCaseWork.Security;
using ConcernsCaseWork.Shared.Tests.Factory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Service.Redis.Users;
using System;
using System.Threading.Tasks;

namespace ConcernsCaseWork.Tests.Pages
{
	[Parallelizable(ParallelScope.All)]
	public class AdminPageModelTests
	{
		[Test]
		public async Task WhenOnGetAsync_Return_Page()
		{
			// arrange
			var mockRbacManager = new Mock<IRbacManager>();
			var mockUserRoleCachedService = new Mock<IUserRoleCachedService>();
			var mockLogger = new Mock<ILogger<IndexPageModel>>();

			var rolesEnum = RoleFactory.BuildListRoleEnum();
			var usersRoles = RoleFactory.BuildDicUsersRoles();

			mockRbacManager.Setup(r => r.GetUserRoles(It.IsAny<string>()))
				.ReturnsAsync(rolesEnum);
			mockRbacManager.Setup(r => r.GetUsersRoles())
				.ReturnsAsync(usersRoles);
			
			var pageModel = SetupAdminPageModel(mockRbacManager.Object, mockUserRoleCachedService.Object, mockLogger.Object);
			
			// act
			await pageModel.OnGetAsync();

			// assert
			Assert.That(pageModel.UsersRole, Is.Not.Null);
			Assert.That(pageModel.TempData, Is.Empty);
			
			mockLogger.Verify(
				m => m.Log(
					LogLevel.Information,
					It.IsAny<EventId>(),
					It.Is<It.IsAnyType>((v, _) => v.ToString().Contains("Admin::IndexPageModel::OnGetAsync")),
					null,
					It.IsAny<Func<It.IsAnyType, Exception, string>>()),
				Times.Once);
			
			mockRbacManager.Verify(r => r.GetUserRoles(It.IsAny<string>()), Times.Once);
			mockRbacManager.Verify(r => r.GetUsersRoles(), Times.Once);
		}

		[Test]
		public async Task WhenOnGetAsync_UserNotAdmin_Return_ErrorOnPage()
		{
			// arrange
			var mockRbacManager = new Mock<IRbacManager>();
			var mockUserRoleCachedService = new Mock<IUserRoleCachedService>();
			var mockLogger = new Mock<ILogger<IndexPageModel>>();

			var rolesEnum = RoleFactory.BuildPartialListRoleEnum();
			var usersRoles = RoleFactory.BuildDicUsersRoles();

			mockRbacManager.Setup(r => r.GetUserRoles(It.IsAny<string>()))
				.ReturnsAsync(rolesEnum);
			mockRbacManager.Setup(r => r.GetUsersRoles())
				.ReturnsAsync(usersRoles);
			
			var pageModel = SetupAdminPageModel(mockRbacManager.Object, mockUserRoleCachedService.Object, mockLogger.Object);
			
			// act
			await pageModel.OnGetAsync();

			// assert
			Assert.That(pageModel.UsersRole, Is.Null);
			Assert.That(pageModel.TempData, Is.Not.Empty);
			
			mockLogger.Verify(
				m => m.Log(
					LogLevel.Information,
					It.IsAny<EventId>(),
					It.Is<It.IsAnyType>((v, _) => v.ToString().Contains("Admin::IndexPageModel::OnGetAsync")),
					null,
					It.IsAny<Func<It.IsAnyType, Exception, string>>()),
				Times.Once);
			
			mockRbacManager.Verify(r => r.GetUserRoles(It.IsAny<string>()), Times.Once);
			mockRbacManager.Verify(r => r.GetUsersRoles(), Times.Never);
		}		
		
		[Test]
		public async Task WhenOnGetClearCache_Return_Page()
		{
			// arrange
			var mockRbacManager = new Mock<IRbacManager>();
			var mockUserRoleCachedService = new Mock<IUserRoleCachedService>();
			var mockLogger = new Mock<ILogger<IndexPageModel>>();

			var rolesEnum = RoleFactory.BuildListRoleEnum();
			var usersRoles = RoleFactory.BuildDicUsersRoles();

			mockUserRoleCachedService.Setup(urc => urc.ClearData());
			mockRbacManager.Setup(r => r.GetUserRoles(It.IsAny<string>()))
				.ReturnsAsync(rolesEnum);
			mockRbacManager.Setup(r => r.GetUsersRoles())
				.ReturnsAsync(usersRoles);
			
			var pageModel = SetupAdminPageModel(mockRbacManager.Object, mockUserRoleCachedService.Object, mockLogger.Object);
			
			// act
			await pageModel.OnGetClearCache();

			// assert
			Assert.That(pageModel.UsersRole, Is.Not.Null);
			Assert.That(pageModel.TempData, Is.Empty);
			
			mockLogger.Verify(
				m => m.Log(
					LogLevel.Information,
					It.IsAny<EventId>(),
					It.Is<It.IsAnyType>((v, _) => v.ToString().Contains("Admin::IndexPageModel::OnGetClearCache")),
					null,
					It.IsAny<Func<It.IsAnyType, Exception, string>>()),
				Times.Once);
			
			mockRbacManager.Verify(r => r.GetUserRoles(It.IsAny<string>()), Times.Once);
			mockRbacManager.Verify(r => r.GetUsersRoles(), Times.Once);
			mockUserRoleCachedService.Verify(urc => urc.ClearData(), Times.Once);
		}		
		
		private static IndexPageModel SetupAdminPageModel(IRbacManager rbacManager, IUserRoleCachedService userRoleCachedService, ILogger<IndexPageModel> logger, bool isAuthenticated = false)
		{
			(PageContext pageContext, TempDataDictionary tempData, ActionContext actionContext) = PageContextFactory.PageContextBuilder(isAuthenticated);
			
			return new IndexPageModel(rbacManager, userRoleCachedService, logger)
			{
				PageContext = pageContext,
				TempData = tempData,
				Url = new UrlHelper(actionContext),
				MetadataProvider = pageContext.ViewData.ModelMetadata
			};
		}
	}
}