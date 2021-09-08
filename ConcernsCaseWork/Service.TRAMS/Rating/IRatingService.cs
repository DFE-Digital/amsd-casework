﻿using Service.TRAMS.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.TRAMS.Rating
{
	public interface IRatingService
	{
		Task<IList<RatingDto>> GetRatings();
	}
}