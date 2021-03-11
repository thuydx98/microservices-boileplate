using System;
using System.ComponentModel;
using System.Linq;

namespace MBP.Common.Extensions
{
	public static class EnumExtension
	{
		public static string GetDescription(this Enum value)
		{
			var field = value.GetType().GetField(value.ToString());
			var attributes = field.GetCustomAttributes(typeof(DescriptionAttribute), false);

			return attributes.Any() ? ((DescriptionAttribute)attributes.ElementAt(0)).Description : string.Empty;
		}
	}
}
