﻿using ConcernsCaseWork.Services.Trusts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Service.Redis.Services;

namespace ConcernsCaseWork.Pages.Case
{
	[Authorize]
	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public class ManagementModel : PageModel
	{
		private readonly ITrustModelService _trustModelService;
		private readonly ICachedService _cachedService;
		private readonly ILogger<DetailsModel> _logger;
		
		public ManagementModel(ITrustModelService trustModelService, ICachedService cachedService, ILogger<DetailsModel> logger)
		{
			_trustModelService = trustModelService;
			_cachedService = cachedService;
			_logger = logger;
		}
		
		public void OnGet(string id)
		{
			_logger.LogInformation($"ID - {id}");
		}
	}
}