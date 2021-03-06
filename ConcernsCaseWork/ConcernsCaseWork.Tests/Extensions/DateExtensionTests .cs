using ConcernsCaseWork.Extensions;
using NUnit.Framework;
using System;

namespace ConcernsCaseWork.Tests.Extensions
{
	[Parallelizable(ParallelScope.All)]
	public class DateExtensionTests
	{
		[TestCase("07/07/2021", "7 July 2021")]
		[TestCase("11/16/2021", "16 November 2021")]

		public void WhenToUserFriendlyDate_ReturnsExpected(DateTimeOffset actual, string expected)
		{
			// assert
			Assert.That(actual.ToUserFriendlyDate(), Is.EqualTo(expected));
		}


		[TestCase("07/07/2021", "07-07-2021")]
		[TestCase("11/16/2021", "16-11-2021")]

		public void WhenToDayMonthYear_ReturnsExpected(DateTimeOffset actual, string expected)
		{
			// assert
			Assert.That(actual.ToDayMonthYear(), Is.EqualTo(expected));
		}
	}
}