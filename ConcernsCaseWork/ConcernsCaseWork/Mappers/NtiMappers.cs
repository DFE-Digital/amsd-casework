﻿using ConcernsCaseWork.Enums;
using ConcernsCaseWork.Models.CaseActions;
using Service.TRAMS.Nti;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConcernsCaseWork.Mappers
{
	public static class NtiMappers
	{
		public static NtiDto ToDBModel(NtiModel ntiModel)
		{
			return new NtiDto
			{
				Id = ntiModel.Id,
				CaseUrn = ntiModel.CaseUrn,
				Reasons = ntiModel.NtiReasonsForConsidering?.Select(r => ToDBModel(r)).ToArray(),
				UnderConsiderationReasonsMapping = ntiModel.NtiReasonsForConsidering?.Select(r => r.Id).ToArray(),
				Notes = ntiModel.Notes,
				CreatedAt = ntiModel.CreatedAt,
				UpdatedAt = ntiModel.UpdatedAt
			};
		}

		public static NtiModel ToServiceModel(NtiDto ntiDto)
		{
			return new NtiModel
			{
				Id = ntiDto.Id,
				CaseUrn = ntiDto.CaseUrn,
				NtiReasonsForConsidering = ntiDto.Reasons?.Select(r => ToServiceModel(r)).ToArray(),	
				CreatedAt = ntiDto.CreatedAt.Date,
				Notes = ntiDto.Notes,
				UpdatedAt = ntiDto.UpdatedAt.Date,
				ClosedAt = ntiDto.ClosedAt
			};
		}

		public static NtiReasonDto ToDBModel(NtiReasonForConsideringModel ntiReasonModel)
		{
			return new NtiReasonDto
			{
				Id = ntiReasonModel.Id,
				Name = ntiReasonModel.Name,
			};
		}

		public static  NtiReasonForConsideringModel ToServiceModel(NtiReasonDto ntiDto)
		{
			return new NtiReasonForConsideringModel
			{
				Id = ntiDto.Id,
				Name = ntiDto.Name
			};
		}



	}
}
