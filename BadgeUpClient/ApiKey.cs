using System;
using System.Text;

namespace BadgeUpClient
{
	public class ApiKey
	{
		public string AccountId { get; set; }
		public string ApplicationId { get; set; }
		public string Key { get; set; }

		public string Value { get; protected set; }
		public string Auth { get; protected set; }


		public static ApiKey Create( string v )
		{
			var result = Json.Deserialize<ApiKey>( Convert.FromBase64String( v ) );
			result.Value = v;
			result.Auth = Convert.ToBase64String( Encoding.UTF8.GetBytes( v + ':' ) );

			return result;
		}
	}
}
