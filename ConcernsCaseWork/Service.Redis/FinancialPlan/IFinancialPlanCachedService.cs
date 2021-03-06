using Service.TRAMS.FinancialPlan;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Redis.FinancialPlan
{
	public interface IFinancialPlanCachedService
	{
		Task<IList<FinancialPlanDto>> GetFinancialPlansByCaseUrn(long caseUrn, string caseworker);
		Task<FinancialPlanDto> GetFinancialPlansById(long caseUrn, long financialPlanId, string caseworker);
		Task<FinancialPlanDto> PostFinancialPlanByCaseUrn(CreateFinancialPlanDto createFinancialPlanDto, string caseworker);
		Task PatchFinancialPlanById(FinancialPlanDto financialPlanDto, string caseworker);
	}
}
