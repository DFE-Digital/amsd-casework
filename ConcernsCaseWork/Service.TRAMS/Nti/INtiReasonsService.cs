﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.TRAMS.Nti
{
	public interface INtiReasonsService
	{
		Task<ICollection<NtiReasonDto>> GetAllReasons();
	}
}