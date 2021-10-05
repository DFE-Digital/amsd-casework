﻿using AutoFixture;
using ConcernsCaseWork.Models;
using System;
using System.Collections.Generic;

namespace ConcernsCaseWork.Shared.Tests.Factory
{
	public static class HomePageFactory
	{
		private readonly static Fixture Fixture = new Fixture();
		
		public static List<HomeModel> BuildHomeModels()
		{
			var dateTimeNow = DateTimeOffset.Now;
			return new List<HomeModel>
			{
				new HomeModel(Fixture.Create<string>(), 
					dateTimeNow.ToString("dd-MM-yyyy"),
					dateTimeNow.ToString("dd-MM-yyyy"),
					dateTimeNow.ToString("dd-MM-yyyy"),
					dateTimeNow.ToString("dd-MM-yyyy"),
					Fixture.Create<string>(),
					Fixture.Create<string>(),
					Fixture.Create<string>(),
					Fixture.Create<string>(),
					new List<string> { Fixture.Create<string>() },
					new List<string> { Fixture.Create<string>() })
			};
		}
	}
}