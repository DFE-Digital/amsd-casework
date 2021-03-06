using ConcernsCaseWork.Pages.Base;
using ConcernsCaseWork.Services.Cases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using ConcernsCaseWork.Models.CaseActions;
using ConcernsCaseWork.Enums;
using System.Collections.Generic;

namespace ConcernsCaseWork.Pages.Case.Management.Action.SRMA
{
	[Authorize]
	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public class IndexPageModel : AbstractPageModel
	{
		private readonly ISRMAService _srmaModelService;
		private readonly ILogger<IndexPageModel> _logger;

		public readonly string ReasonErrorMessage = "Enter the reason";
		public readonly string DateAcceptedErrorMessage = "Enter date accepted";
		public readonly string DateVisitErrorMessage = "Enter date of visit";
		public readonly string DateReportErrorMessage = "Enter date report sent to trust";

		private readonly string SRMACompleteText = "SRMA complete";
		private readonly string SRMADeclineText = "SRMA declined";

		public SRMAModel SRMAModel { get; set; }
		public string DeclineCompleteButtonLabel { get; private set; }

		public IndexPageModel(ISRMAService srmaService, ILogger<IndexPageModel> logger)
		{
			_srmaModelService = srmaService;
			_logger = logger;
		}

		public async Task OnGetAsync()
		{
			long caseUrn = 0;
			long srmaId = 0;

			try
			{
				_logger.LogInformation("Case::Action::SRMA::IndexPageModel::OnGetAsync");

				(caseUrn, srmaId) = GetRouteData();

				await SetPageData(caseUrn, srmaId);
			}
			catch (Exception ex)
			{
				_logger.LogError("Case::Action::SRMA::IndexPageModel::OnGetAsync::Exception - {Message}", ex.Message);
				TempData["Error.Message"] = ErrorOnGetPage;
			}
		}

		public async Task<IActionResult> OnPostAsync()
		{
			return RedirectToPage("index");
		}

		public async Task<ActionResult> OnGetDeclineComplete()
		{
			long caseUrn = 0;
			long srmaId = 0;

			try
			{
				(caseUrn, srmaId) = GetRouteData();

				await SetPageData(caseUrn, srmaId);
				var srmaIndexPage = $"/case/{caseUrn}/management/action/srma/{srmaId}";

				if (SRMAModel.Status.Equals(SRMAStatus.Deployed))
				{
					var validationErrors = CreateValidationErrors();

					if (validationErrors.Count > 0)
					{
						TempData["SRMA.Message"] = validationErrors;
						return Redirect(srmaIndexPage);
					}
				}
				else
				{
					if (SRMAModel.Reason.Equals(SRMAReasonOffered.Unknown))
					{
						TempData["SRMA.Message"] = new List<string>() { ReasonErrorMessage };

						return Redirect(srmaIndexPage);
					}
				}

				var action = DeclineCompleteButtonLabel.Equals(SRMACompleteText) ? "complete" : "decline";


				return Redirect($"resolve/{action}");
			}
			catch (Exception ex)
			{
				_logger.LogError("Case::Action::SRMA::IndexPageModel::OnGetDeclineComplete::Exception - {Message}", ex.Message);
				TempData["Error.Message"] = ErrorOnGetPage;
				
				return Page();
			}
		}

		public async Task<ActionResult> OnGetCancel()
		{
			long caseUrn = 0;
			long srmaId = 0;

			try
			{
				(caseUrn, srmaId) = GetRouteData();
				await SetPageData(caseUrn, srmaId);

				if (SRMAModel.Reason.Equals(SRMAReasonOffered.Unknown))
				{
					TempData["SRMA.Message"] = new List<string>() { ReasonErrorMessage };

					return Redirect($"/case/{caseUrn}/management/action/srma/{srmaId}");
				}

				return Redirect("resolve/cancel");
			}
			catch (Exception ex)
			{
				_logger.LogError("Case::Action::SRMA::IndexPageModel::OnGetDeclineComplete::Exception - {Message}", ex.Message);
				TempData["Error.Message"] = ErrorOnGetPage;
			}

			return Page();
		}

		private (long caseUrn, long srmaId) GetRouteData()
		{
			var caseUrnValue = RouteData.Values["urn"];
			if (caseUrnValue == null || !long.TryParse(caseUrnValue.ToString(), out long caseUrn) || caseUrn == 0)
				throw new Exception("CaseUrn is null or invalid to parse");

			var srmaIdValue = RouteData.Values["srmaId"];
			if (srmaIdValue == null || !long.TryParse(srmaIdValue.ToString(), out long srmaId) || srmaId == 0)
				throw new Exception("srmaId is null or invalid to parse");

			return (caseUrn, srmaId);
		}
	
		private async Task SetPageData(long caseUrn, long srmaId)
		{
			// TODO - get SRMA by case ID and SRMA ID
			SRMAModel = await _srmaModelService.GetSRMAById(srmaId);

			if (SRMAModel == null)
			{
				throw new Exception("Could not load this SRMA");
			}

			switch (SRMAModel.Status)
			{
				case SRMAStatus.Deployed:
					DeclineCompleteButtonLabel = SRMACompleteText;
					break;
				default:
					DeclineCompleteButtonLabel = SRMADeclineText;
					break;
			}
		}
	
		private List<string> CreateValidationErrors()
		{
			List<string> validationErrors = new List<string>();

			if (SRMAModel.Reason.Equals(SRMAReasonOffered.Unknown))
			{
				validationErrors.Add(ReasonErrorMessage);
			}

			if (!SRMAModel.DateAccepted.HasValue)
			{
				validationErrors.Add(DateAcceptedErrorMessage);
			}

			if (!SRMAModel.DateVisitStart.HasValue || !SRMAModel.DateVisitEnd.HasValue)
			{
				validationErrors.Add(DateVisitErrorMessage);
			}

			if (!SRMAModel.DateReportSentToTrust.HasValue)
			{
				validationErrors.Add(DateReportErrorMessage);
			}

			return validationErrors;
		}
	
	}
}