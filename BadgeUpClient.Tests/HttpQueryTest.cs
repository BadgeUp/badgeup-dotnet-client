using BadgeUp.Http;
using Xunit;

namespace BadgeUp.Tests
{
	public class HttpQueryTest
	{
		[Fact]
		public void MultipleParameterPairs_ToString_ConcatenatesPairs()
		{
			var query = new HttpQuery();
			query.Add("key1", "value1");
			query.Add("key2", "value2");

			Assert.Equal("key1=value1&key2=value2", query.ToString());
		}

		[Fact]
		public void MultipleParameterPairs_ToString_DoesntProduceTrailingAmpersand()
		{
			var query = new HttpQuery();
			query.Add("key1", "value1");
			query.Add("key2", "value2");

			Assert.False(query.ToString().EndsWith("&"));
		}

		[Fact]
		public void ToString_UrlEncodesParameterKeysAndValues()
		{
			var query = new HttpQuery();
			query.Add("key 1", "value 1");

			Assert.Equal("key%201=value%201", query.ToString());
		}

		[Fact]
		public void ToString_UrlEncodesEmojiAndUnicodeKeysAndValues()
		{
			var query = new HttpQuery();
			query.Add("🔥key🔥", "тест 🔥value🔥 тест");

			Assert.Equal("%F0%9F%94%A5key%F0%9F%94%A5=%D1%82%D0%B5%D1%81%D1%82%20%F0%9F%94%A5value%F0%9F%94%A5%20%D1%82%D0%B5%D1%81%D1%82", query.ToString());
		}

		[Fact]
		public void ToString_UrlEncodesSpecialSymbolsInKeysAndValues()
		{
			var query = new HttpQuery();
			query.Add("key=", "value=");

			Assert.Equal("key%3D=value%3D", query.ToString());
		}
	}
}
