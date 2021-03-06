using ConcernsCaseWork.Models.CaseActions;
using Service.TRAMS.CaseActions;
using System;

namespace ConcernsCaseWork.Mappers
{
	public static class CaseActionsMapping
	{
		public static SRMAModel Map(SRMADto srmaDto)
		{
			return new SRMAModel
			{
				Id = srmaDto.Id,
				CaseUrn = srmaDto.CaseUrn,
				CreatedAt = srmaDto.CreatedAt,
				ClosedAt = srmaDto.ClosedAt,
				DateOffered = srmaDto.DateOffered,
				DateAccepted = srmaDto.DateAccepted,
				DateReportSentToTrust = srmaDto.DateReportSentToTrust,
				DateVisitEnd = srmaDto.DateVisitEnd,
				DateVisitStart = srmaDto.DateVisitStart,
				Notes = srmaDto.Notes,
				Reason = (Enums.SRMAReasonOffered)(srmaDto.Reason ?? SRMAReasonOffered.Unknown),
				Status = (Enums.SRMAStatus)srmaDto.Status
			};
		}

		public static SRMADto Map(SRMAModel srmaModel)
		{
			return new SRMADto
			{
				Id = Convert.ToInt32(srmaModel.Id),
				CaseUrn = Convert.ToInt32(srmaModel.CaseUrn),
				CreatedAt = srmaModel.CreatedAt,
				ClosedAt = srmaModel.ClosedAt,
				DateAccepted = srmaModel.DateAccepted,
				DateOffered = srmaModel.DateOffered,
				DateReportSentToTrust = srmaModel.DateReportSentToTrust,
				DateVisitEnd = srmaModel.DateVisitEnd,
				DateVisitStart = srmaModel.DateVisitStart,
				Notes = srmaModel.Notes,
				Reason = (SRMAReasonOffered)srmaModel.Reason,
				Status = (SRMAStatus)srmaModel.Status,

			};
		}
	}
}
