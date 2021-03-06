using Service.TRAMS.Cases;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Redis.Cases
{
	public interface ICaseCachedService
	{
		Task<IList<CaseDto>> GetCasesByCaseworkerAndStatus(string caseworker, long statusUrn);
		Task<CaseDto> GetCaseByUrn(string caseworker, long urn);
		Task<CaseDto> PostCase(CreateCaseDto createCaseDto);
		Task PatchCaseByUrn(CaseDto caseDto);
	}
}