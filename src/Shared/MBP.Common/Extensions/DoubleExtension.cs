﻿using System;

namespace MBP.Common.Extensions
{
	public static class DoubleExtension
	{
		/// <summary>
		/// Convert time in UNIX Stamp to DateTime
		/// </summary>
		/// <param name="unixTimeStamp">Date & time in UNIX</param>
		/// <returns>DateTime in UTC</returns>
		public static DateTime ToDateTime(this double unixTimeStamp)
		{
			return new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(unixTimeStamp);
		}
	}
}
