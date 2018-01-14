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
	}
}
