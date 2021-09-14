﻿using System;
using System.Collections.Generic;

namespace Service.Redis.Models
{
	[Serializable]
	public sealed class UserState
	{
		public string TrustUkPrn { get; set; }
		public IDictionary<long, CaseWrapper> CasesDetails { get; } = new Dictionary<long, CaseWrapper>();
	}
}