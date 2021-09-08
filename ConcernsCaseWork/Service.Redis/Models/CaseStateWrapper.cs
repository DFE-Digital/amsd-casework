﻿using Service.TRAMS.Dto;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace Service.Redis.Models
{
	[Serializable]
	public class CaseStateWrapper
	{
		public CaseDto Cases { get; set; }
		public IDictionary<BigInteger, RecordDto> Records { get; set; } = new Dictionary<BigInteger, RecordDto>();
		public IDictionary<BigInteger, RecordRatingHistoryDto> RecordsRatingHistory { get; set; } = new Dictionary<BigInteger, RecordRatingHistoryDto>();
	}
}