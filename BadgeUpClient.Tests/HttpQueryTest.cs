using System;
using BadgeUpClient.Http;
using BadgeUpClient.ResourceClients;
using BadgeUpClient.Types;
using Xunit;

namespace BadgeUpClient.Tests
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

		[Fact]
		public void HttpQuery_GetQueryStringFromObject()
		{
			var param = new EarnedAchievementQueryParams
			{
				AchievementId = "foo",
				Since = new DateTime(2000, 1, 1),
				Until = new DateTime(2001, 1, 1),
				Subject = "bar"
			};
			var res = HttpQuery.GetQueryStringFromObject(param);

			Assert.Contains("subject=bar", res);
			Assert.Contains("achievementId=foo", res);
			Assert.Contains("since=2000-01-01T00:00:00", res);
			Assert.Contains("until=2001-01-01T00:00:00", res);

		}
	}
}
