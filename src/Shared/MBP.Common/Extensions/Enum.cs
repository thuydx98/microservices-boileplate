using System;
using System.ComponentModel;
using System.Linq;

namespace MBP.Common.Extensions
{
	public static class Enum
	{
		public static string GetDescription(this System.Enum value)
		{
			var field = value.GetType().GetField(value.ToString());
			var attributes = field.GetCustomAttributes(typeof(DescriptionAttribute), false);

			return attributes.Any() ? ((DescriptionAttribute)attributes.ElementAt(0)).Description : string.Empty;
		}
	}
}
