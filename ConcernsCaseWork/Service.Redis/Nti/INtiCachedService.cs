﻿using Service.TRAMS.Nti;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Redis.Nti
{
	public interface INtiCachedService
	{
		Task<NtiDto> CreateNti(NtiDto nti);
		Task<NtiDto> GetNti(long ntiId);
		Task<ICollection<NtiDto>> GetNtisForCase(long caseUrn);
		Task<NtiDto> GetNTIUnderConsiderationById(long underConsiderationId);
		Task<NtiDto> PatchNti(NtiDto nti);
	}
}
