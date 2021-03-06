using ConcernsCaseWork.Models.CaseActions;
using Service.TRAMS.FinancialPlan;
using Service.TRAMS.Status;
using System.Collections.Generic;
using System.Linq;

namespace ConcernsCaseWork.Mappers
{
	public static class FinancialPlanStatusMapping
	{
		public static FinancialPlanStatusModel MapDtoToModel(IList<FinancialPlanStatusDto> statusesDto, long? id)
		{
			var selectedStatusDto = statusesDto.FirstOrDefault(s => s.Id.CompareTo(id) == 0) ?? null;

			return selectedStatusDto is null ? null : new FinancialPlanStatusModel(selectedStatusDto.Name, selectedStatusDto.Id);
		}
	}
}