﻿using System;

namespace ConcernsCaseWork.Extensions
{
	public static class DateExtension
	{
		public static string ToUserFriendlyDate(this DateTime value)
		{
			return value.ToString("d MMMM yyyy");
		}
	}
}