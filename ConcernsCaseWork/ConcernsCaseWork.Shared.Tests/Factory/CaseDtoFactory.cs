﻿using Service.TRAMS.Models;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace ConcernsCaseWork.Shared.Tests.Factory
{
	public static class CaseDtoFactory
	{
		public static List<CaseDto> CreateListCaseDto()
		{
			var dateTimeNow = DateTimeOffset.Now;
			return new List<CaseDto>
			{
				new CaseDto(
					1, dateTimeNow, dateTimeNow, dateTimeNow, dateTimeNow, "testing","description", "crmenquiry",
					"trustukprn", "reasonatreview", dateTimeNow, "issue", "currentstatus", "nextSteps", 
					"resolutionstrategy", "directionoftravel", BigInteger.One, Int32.MinValue
				),
				new CaseDto(
					2, dateTimeNow, dateTimeNow, dateTimeNow, dateTimeNow, "testing","description", "crmenquiry",
					"trustukprn", "reasonatreview", dateTimeNow, "issue", "currentstatus", "nextSteps", 
					"resolutionstrategy", "directionoftravel", BigInteger.One, Int32.MinValue
				),
				new CaseDto(
					3, dateTimeNow, dateTimeNow, dateTimeNow, dateTimeNow, "testing","description", "crmenquiry",
					"trustukprn", "reasonatreview", dateTimeNow, "issue", "currentstatus", "nextSteps", 
					"resolutionstrategy", "directionoftravel", BigInteger.One, Int32.MinValue
				),
				new CaseDto(
					4, dateTimeNow, dateTimeNow, dateTimeNow, dateTimeNow, "testing","description", "crmenquiry",
					"trustukprn", "reasonatreview", dateTimeNow, "issue", "currentstatus", "nextSteps", 
					"resolutionstrategy", "directionoftravel", BigInteger.One, Int32.MinValue
				),
				new CaseDto(
					5, dateTimeNow, dateTimeNow, dateTimeNow, dateTimeNow, "testing","description", "crmenquiry",
					"trustukprn", "reasonatreview", dateTimeNow, "issue", "currentstatus", "nextSteps", 
					"resolutionstrategy", "directionoftravel", BigInteger.One, Int32.MinValue
				)
			};
		}

		public static CaseDto CreateCaseDto()
		{
			var dateTimeNow = DateTimeOffset.Now;
			return new CaseDto(
				1, dateTimeNow, dateTimeNow, dateTimeNow, dateTimeNow, "testing", "description", "crmenquiry",
				"trustukprn", "reasonatreview", dateTimeNow, "issue", "currentstatus", "nextSteps",
				"resolutionstrategy", "directionoftravel", BigInteger.One, Int32.MinValue
			);
		}
	}
}