using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Service.TRAMS.Base;
using Service.TRAMS.Configuration;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Service.TRAMS.Trusts
{
	public sealed class TrustSearchService : ITrustSearchService
	{
		private readonly ITrustService _trustService;
		private readonly ILogger<TrustSearchService> _logger;
		private readonly int _hardLimitTrustsByPagination;

		public TrustSearchService(ITrustService trustService, IOptions<TrustSearchOptions> options, ILogger<TrustSearchService> logger)
		{
			_trustService = trustService;
			_logger = logger;
			_hardLimitTrustsByPagination = options.Value.TrustsLimitByPage;
		}
		
		public async Task<IList<TrustSearchDto>> GetTrustsBySearchCriteria(TrustSearch trustSearch)
		{
			_logger.LogInformation("TrustSearchService::GetTrustsBySearchCriteria execution");
			
			var stopwatch = Stopwatch.StartNew();
			var trustList = new List<TrustSearchDto>();
			
			try
			{
				ApiListWrapper<TrustSearchDto> apiListWrapperTrusts;
				var nrRequests = 0;
				
				do
				{
					apiListWrapperTrusts = await _trustService.GetTrustsByPagination(trustSearch);
					
					// The following condition will break the loop.
					if (apiListWrapperTrusts?.Data is null || !apiListWrapperTrusts.Data.Any()) continue;
					
					trustList.AddRange(apiListWrapperTrusts.Data);
					trustSearch.PageIncrement();
					
					// Safe guard in case we have more than 10 pages.
					// We don't have a scenario at the moment, but feels like we need a limit.
					if ((nrRequests = Interlocked.Increment(ref nrRequests)) > _hardLimitTrustsByPagination)
					{
						break;
					}

				} while (apiListWrapperTrusts?.Data != null && apiListWrapperTrusts.Data.Any() && apiListWrapperTrusts.Paging?.NextPageUrl != null);
			}
			finally
			{
				stopwatch.Stop();
				_logger.LogInformation("TrustSearchService::GetTrustsBySearchCriteria execution time {ElapsedMilliseconds} ms", stopwatch.ElapsedMilliseconds);
			}
			
			return trustList;
		}
	}
}