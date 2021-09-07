﻿using ConcernsCaseWork.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConcernsCaseWork.Services.Cases
{
	public interface ICaseModelService
	{
		Task<(IList<HomeModel>, IList<HomeModel>)> GetCasesByCaseworker(string caseworker);
		
	}
}