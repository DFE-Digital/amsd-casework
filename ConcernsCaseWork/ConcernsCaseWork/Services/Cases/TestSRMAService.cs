﻿using ConcernsCaseWork.Enums;
using ConcernsCaseWork.Models.CaseActions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConcernsCaseWork.Services.Cases
{
	public class TestSRMAService : ISRMAService
	{
		private readonly List<SRMA> SRMAs;

		public TestSRMAService()
		{
			SRMAs = new List<SRMA>();
		}

		public Task SaveSRMA(SRMA srma)
		{
			srma.Notes = "TEST DATA: " + srma.Notes;
			SRMAs.Add(srma);

			return Task.CompletedTask;
		}

		public Task<IEnumerable<SRMA>> GetSRMAsForCase(long caseUrn)
		{
			var srmaList = SRMAs?.Where(s => s.CaseUrn == caseUrn);
			return Task.FromResult(srmaList?.Count() > 0 ? srmaList: CreateTestData(caseUrn));
		}

		private SRMA[] CreateTestData(long caseUrn)
		{
			return new SRMA[] {
				new SRMA {
					CaseUrn = caseUrn,
					DateOffered = DateTime.Now,
					Notes = "Auto generated test data",
					Status = SRMAStatus.TrustConsidering
				}
			};
		}
	}
}
