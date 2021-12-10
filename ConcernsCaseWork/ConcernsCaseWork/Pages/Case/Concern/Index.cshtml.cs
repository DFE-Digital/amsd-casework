﻿using ConcernsCaseWork.Extensions;
using ConcernsCaseWork.Mappers;
using ConcernsCaseWork.Models;
using ConcernsCaseWork.Pages.Base;
using ConcernsCaseWork.Pages.Validators;
using ConcernsCaseWork.Services.Ratings;
using ConcernsCaseWork.Services.Trusts;
using ConcernsCaseWork.Services.Types;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Service.Redis.Base;
using Service.Redis.Models;
using Service.TRAMS.Cases;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConcernsCaseWork.Pages.Case.Concern
{
	[Authorize]
	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public class IndexPageModel : AbstractPageModel
	{
		private readonly IRatingModelService _ratingModelService;
		private readonly ILogger<IndexPageModel> _logger;
		private readonly ITrustModelService _trustModelService;
		private readonly ITypeModelService _typeModelService;
		private readonly ICachedService _cachedService;
		
		public TypeModel TypeModel { get; private set; }
		public IList<RatingModel> RatingsModel { get; private set; }
		public TrustDetailsModel TrustDetailsModel { get; private set; }
		public IList<CreateRecordModel> CreateRecordsModel { get; private set; }

		public IndexPageModel(ITrustModelService trustModelService, 
			ICachedService cachedService, 
			ITypeModelService typeModelService, 
			IRatingModelService ratingModelService,
			ILogger<IndexPageModel> logger)
		{
			_ratingModelService = ratingModelService;
			_trustModelService = trustModelService;
			_typeModelService = typeModelService;
			_cachedService = cachedService;
			_logger = logger;
		}
		
		public async Task OnGetAsync()
		{
			try
			{
				_logger.LogInformation("Case::Concern::IndexPageModel::OnGetAsync");
				
				// Fetch UI data
				await LoadPage();
			}
			catch (Exception ex)
			{
				_logger.LogError("Case::Concern::IndexPageModel::OnGetAsync::Exception - {Message}", ex.Message);
				
				TempData["Error.Message"] = ErrorOnGetPage;
			}
		}
		
		public async Task<IActionResult> OnPostAsync()
		{
			try
			{
				_logger.LogInformation("Case::Concern::IndexPageModel::OnPostAsync");

				if (!ConcernTypeValidator.IsValid(Request.Form))
					throw new Exception("Case::Concern::IndexPageModel::Missing form values");
				
				string typeUrn;
				
				// Form
				var type = Request.Form["type"].ToString();
				var subType = Request.Form["sub-type"].ToString();
				var ragRating = Request.Form["rating"].ToString();
				var trustUkPrn = Request.Form["trust-ukprn"].ToString();
				
				// Type
				(typeUrn, type, subType) = type.SplitType(subType);

				// Rating
				var splitRagRating = ragRating.Split(":");
				var ragRatingUrn = splitRagRating[0];
				var ragRatingName = splitRagRating[1];
				
				// Redis state
				var userState = await GetUserState();

				// Create a case model
				var currentDate = DateTimeOffset.Now;
				var existingRecords = userState.CreateCaseModel.CreateRecordsModel;
				userState.CreateCaseModel = new CreateCaseModel
				{
					CreatedAt = currentDate,
					ReviewAt = currentDate,
					UpdatedAt = currentDate,
					ClosedAt = currentDate,
					CreatedBy = User.Identity.Name,
					DeEscalation = currentDate,
					RagRatingName = ragRatingName,
					RagRating = RatingMapping.FetchRag(ragRatingName),
					RagRatingCss = RatingMapping.FetchRagCss(ragRatingName),
					TrustUkPrn = trustUkPrn,
					DirectionOfTravel = DirectionOfTravelEnum.Deteriorating.ToString(),
					RatingUrn = long.Parse(ragRatingUrn),			// Remove or fix when multiple concerns is done
					CreateRecordsModel = existingRecords
				};

				var createRecordModel = new CreateRecordModel
				{
					TypeUrn = long.Parse(typeUrn),
					Type = type,
					SubType = subType,
					RatingUrn = long.Parse(ragRatingUrn),
					RatingName = ragRatingName,
					RagRating = RatingMapping.FetchRag(ragRatingName),
					RagRatingCss = RatingMapping.FetchRagCss(ragRatingName)
				};
				userState.CreateCaseModel.CreateRecordsModel.Add(createRecordModel);

				// Store case model in cache for the details page
				await _cachedService.StoreData(User.Identity.Name, userState);
				
				return RedirectToPage("add");
			}
			catch (Exception ex)
			{
				_logger.LogError("Case::Concern::IndexPageModel::OnPostAsync::Exception - {Message}", ex.Message);
				
				TempData["Error.Message"] = ErrorOnPostPage;
			}
			
			return await LoadPage();
		}

		public async Task<ActionResult> OnGetCancelCase()
		{
			var userState = await GetUserState();
			userState.CreateCaseModel = new CreateCaseModel();
			await _cachedService.StoreData(User.Identity.Name, userState);
			
			return Redirect("/");
		}

		private async Task<ActionResult> LoadPage()
		{
			var userState = await GetUserState();
			var trustUkPrn = userState.TrustUkPrn;
			
			if (string.IsNullOrEmpty(trustUkPrn)) return Page();
			
			CreateRecordsModel = userState.CreateCaseModel.CreateRecordsModel;
			TrustDetailsModel = await _trustModelService.GetTrustByUkPrn(trustUkPrn);
			RatingsModel = await _ratingModelService.GetRatingsModel();
			TypeModel = await _typeModelService.GetTypeModel();
			
			return Page();
		}
		
		private async Task<UserState> GetUserState()
		{
			var userState = await _cachedService.GetData<UserState>(User.Identity.Name);
			if (userState is null)
				throw new Exception("Case::Concern::IndexPageModel::Cache CaseStateData is null");
			
			return userState;
		}
	}
}