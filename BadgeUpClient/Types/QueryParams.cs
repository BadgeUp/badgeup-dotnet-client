using System;
using System.Linq;
using System.Reflection;

namespace BadgeUp.Types
{
	public class QueryParams
	{
		public string ToQueryString()
		{	
			var properties = this.GetType()
				.GetTypeInfo()
				.GetProperties()
				.Where(p => p.GetValue(this, null) != null)
				.Select(p => FirstCharacterToLower(p.Name) + "=" + (p.PropertyType == typeof(DateTime?) ? ((DateTime?)p.GetValue(this, null)).Value.ToString("O") : p.GetValue(this, null)).ToString());

			return String.Join("&", properties.ToArray());
		}

		private static string FirstCharacterToLower(string str)
		{
			if (String.IsNullOrEmpty(str) || Char.IsLower(str, 0))
				return str;

			return Char.ToLowerInvariant(str[0]) + str.Substring(1);
		}
	}
}
