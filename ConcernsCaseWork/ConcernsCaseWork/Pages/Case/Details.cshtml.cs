using ConcernsCaseWork.Models;
using ConcernsCaseWork.Pages.Base;
using ConcernsCaseWork.Services.Cases;
using ConcernsCaseWork.Services.Trusts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Service.Redis.Base;
using Service.Redis.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConcernsCaseWork.Pages.Case
{
	[Authorize]
	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public class DetailsPageModel : AbstractPageModel
	{
		private readonly ITrustModelService _trustModelService;
		private readonly ICaseModelService _caseModelService;
		private readonly ILogger<DetailsPageModel> _logger;
		private readonly ICachedService _cachedService;
		
		public CreateCaseModel CreateCaseModel { get; private set; }
		public TrustDetailsModel TrustDetailsModel { get; private set; }
		public IList<CreateRecordModel> CreateRecordsModel { get; private set; }
		
		public DetailsPageModel(ICaseModelService caseModelService, 
			ITrustModelService trustModelService,
			ICachedService cachedService, 
			ILogger<DetailsPageModel> logger)
		{
			_trustModelService = trustModelService;
			_caseModelService = caseModelService;
			_cachedService = cachedService;
			_logger = logger;
		}
		
		public async Task OnGetAsync()
		{
			_logger.LogInformation("Case::DetailsPageModel::OnGetAsync");
			
			// Fetch UI data
			await LoadPage();
		}
		
		public async Task<IActionResult> OnPostAsync()
		{
			try
			{
				_logger.LogInformation("Case::DetailsPageModel::OnPostAsync");
				
				var issue = Request.Form["issue"];
				var currentStatus = Request.Form["current-status"];
				var nextSteps = Request.Form["next-steps"];
				var caseAim = Request.Form["case-aim"];
				var deEscalationPoint = Request.Form["de-escalation-point"];

				if (string.IsNullOrEmpty(issue)) 
					throw new Exception("Missing form values");
				
				// Complete create case model
				var userState = await GetUserState();
				
				var createCaseModel = userState.CreateCaseModel;
				createCaseModel.Issue = issue;
				createCaseModel.CurrentStatus = currentStatus;
				createCaseModel.NextSteps = nextSteps;
				createCaseModel.CaseAim = caseAim;
				createCaseModel.DeEscalationPoint = deEscalationPoint;
				createCaseModel.TrustUkPrn = userState.TrustUkPrn;
					
				var caseUrn = await _caseModelService.PostCase(createCaseModel);
				
				return RedirectToPage("management/index", new { urn = caseUrn });
			}
			catch (Exception ex)
			{
				_logger.LogError("Case::DetailsPageModel::OnPostAsync::Exception - {Message}", ex.Message);
				
				TempData["Error.Message"] = ErrorOnPostPage;
			}
			
			return await LoadPage();
		}
		
		private async Task<ActionResult> LoadPage()
		{
			try
			{
				var userState = await GetUserState();
				var trustUkPrn = userState.TrustUkPrn;

				if (string.IsNullOrEmpty(trustUkPrn))
					throw new Exception("Cache TrustUkprn is null");
		
				CreateCaseModel = userState.CreateCaseModel;
				CreateRecordsModel = userState.CreateCaseModel.CreateRecordsModel;
				TrustDetailsModel = await _trustModelService.GetTrustByUkPrn(trustUkPrn);
		
				return Page();
			}
			catch (Exception ex)
			{
				_logger.LogError("Case::DetailsPageModel::LoadPage::Exception - {Message}", ex.Message);
				
				TempData["Error.Message"] = ErrorOnGetPage;
				return Page();
			}
		}
		
		private async Task<UserState> GetUserState()
		{
			var userState = await _cachedService.GetData<UserState>(User.Identity.Name);
			if (userState is null)
				throw new Exception("Cache CaseStateData is null");
			
			return userState;
		}
	}
}