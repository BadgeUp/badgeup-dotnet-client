using Xunit;

namespace BadgeUpClient.Tests
{
	public class ApiKeyTest
	{
		// fake API key
		string key = "eyJhY2NvdW50SWQiOiJ0aGViZXN0IiwiYXBwbGljYXRpb25JZCI6IjEzMzciLCJrZXkiOiJpY2VjcmVhbWFuZGNvb2tpZXN5dW0ifQ==";


		[Fact]
		public void ApiKey_ApplicationId()
		{
			var apiKey = ApiKey.Create( key );
			Assert.Equal( "1337", apiKey.ApplicationId );
		}

		[Fact]
		public void ApiKey_AccountId()
		{
			var apiKey = ApiKey.Create( key );
			Assert.Equal( "thebest", apiKey.AccountId );
		}

		[Fact]
		public void ApiKey_Key()
		{
			var apiKey = ApiKey.Create( key );
			Assert.Equal( "icecreamandcookiesyum", apiKey.Key );
		}
	}
}
