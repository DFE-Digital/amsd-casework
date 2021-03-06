using Service.TRAMS.Base;

namespace Service.TRAMS.Trusts
{
	public sealed class TrustSearch : PageSearch
	{
		public string GroupName { get; }
		public string Ukprn { get; }
		public string CompaniesHouseNumber { get; }

		public TrustSearch(string groupName, string ukprn, string companiesHouseNumber) => 
			(GroupName, Ukprn, CompaniesHouseNumber) = (groupName, ukprn, companiesHouseNumber);
	}
}