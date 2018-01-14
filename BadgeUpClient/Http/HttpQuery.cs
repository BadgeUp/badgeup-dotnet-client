using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BadgeUpClient.ResourceClients;

namespace BadgeUpClient.Http
{
	public class HttpQuery
	{
		List<KeyValuePair<string, string>> m_values = new List<KeyValuePair<string, string>>();


		public void Add<T>( string name, T value )
		{
			m_values.Add( new KeyValuePair<string, string>( name, value.ToString() ) );
		}

		public override string ToString()
		{
			if (m_values.Count > 0)
			{
				return m_values.Select( a => a.Key + '=' + a.Value ).Aggregate( ( a, b ) => a + "&" + b );
			}

			return null;
		}


		public static string GetQueryStringFromObject(object param)
		{
			if (param == null)
				return "";
			var properties = param.GetType()
				.GetTypeInfo()
				.GetProperties()
				.Where(p => p.GetValue(param, null) != null)
				.Select(p => FirstCharacterToLower(p.Name) + "=" + (p.PropertyType == typeof(DateTime?) ? ((DateTime?)p.GetValue(param, null)).Value.ToString("O") : p.GetValue(param, null)).ToString());

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
