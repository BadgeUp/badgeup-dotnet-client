using System;
using BadgeUp.Http;
using BadgeUp.ResourceClients;
using BadgeUp.Types;
using Xunit;

namespace BadgeUp.Tests
{
	public class HttpQueryTest
	{
		[Fact]
		public void HttpQuery_ToString()
		{
			var query = new HttpQuery();
			query.Add( "key1", "value1" );
			query.Add( "key2", "value2" );

			Assert.Equal( "key1=value1&key2=value2", query.ToString() );
		}
	}
}
