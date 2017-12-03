using System.Text;
using Newtonsoft.Json;

namespace BadgeUpClient
{
	public class Json
	{
		public static T Deserialize<T>( byte[] b )
		{
			return Deserialize<T>( Encoding.UTF8.GetString( b ) );
		}

		public static T Deserialize<T>( string v )
		{
			//return JsonConvert.DeserializeObject<T>( v, new JsonSerializerSettings { ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver() } );
			return JsonConvert.DeserializeObject<T>( v );
		}

		public static string Serialize<T>( T o )
		{
			return JsonConvert.SerializeObject(
				o,
				new JsonSerializerSettings
				{
					ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
					,
					NullValueHandling = NullValueHandling.Ignore
				} );
		}
	}
}
