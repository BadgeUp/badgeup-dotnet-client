using System;
using BadgeUp.Types;
using Xunit;

namespace BadgeUp.Tests
{
	public class QueryParamTests
	{
		[Fact]
		public void EarnedAchievementQueryParams_ToQueryString()
		{
			var param = new EarnedAchievementQueryParams
			{
				AchievementId = "foo",
				Since = new DateTime(2000, 1, 1),
				Until = new DateTime(2001, 1, 1),
				Subject = "bar 1 2=3"
			};
			var res = param.ToQueryString();

			Assert.Contains("subject=bar%201%202%3D3", res);
			Assert.Contains("achievementId=foo", res);
			Assert.Contains("since=2000-01-01T00%3A00%3A00", res);
			Assert.Contains("until=2001-01-01T00%3A00%3A00", res);
		}
	}
}
