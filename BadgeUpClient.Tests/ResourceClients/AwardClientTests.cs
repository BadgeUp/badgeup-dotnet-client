using BadgeUp.Http;
using BadgeUp.ResourceClients;
using RichardSzalay.MockHttp;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace BadgeUp.Tests
{
	public class AwardClientTests
	{
		private const string ApiKey = "eyJhY2NvdW50SWQiOiJmdmp0c2dsbjFmIiwiYXBwbGljYXRpb25JZCI6Imc2anRzaGxuNDgiLCJrZXkiOiJjamE3NGRjcGJqdHN0bGc3bmY0bW5ieTk5In0=";
		private const string Host = "https://api.useast1.badgeup.io";

		[Fact]
		public async Task WhenAwardIsNull_CreateThrowsException()
		{
			var client = new AwardClient(null);
			await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create(null));
		}

		[Fact]
		public async Task WhenSetupWithResponseData_GetById_CallsUrlOnceAndReturnsCorrectResult()
		{
			Assert.False(string.IsNullOrEmpty(AwardClientTests.ApiKey));

			var apiKey = BadgeUp.ApiKey.Create(ApiKey);

			var responseJson =
			@"{
				'id': 'cjktcmn5o635wmyd0w4ps',
				'applicationId': 'g6jtshln48',
				'name': '5 points',
				'description': '5 Points Awarded',
				'meta': {
					'created': '2016-09-17T06:50:11.426Z',
					'meta_key': 'meta_value'
				},
				'data': {
					'key': 'value',
					'points': 5
				}
			}".Replace("'", "\"");

			// setup the response action
			var url = $"{Host}/v2/apps/{apiKey.ApplicationId}/awards/cjktcmn5o635wmyd0w4ps";
			var mockHttp = new MockHttpMessageHandler();
			var expectedRequest = mockHttp.Expect(HttpMethod.Get, url).Respond("application/json", responseJson);
			mockHttp.Fallback.Throw(new InvalidOperationException("No matching mock handler"));

			using (var badgeUpHttpClient = new BadgeUpHttpClient(apiKey, Host))
			{
				badgeUpHttpClient._SetHttpClient(mockHttp.ToHttpClient());
				var client = new AwardClient(badgeUpHttpClient);

				// act
				var result = await client.GetById("cjktcmn5o635wmyd0w4ps");

				// assert url was called only once
				mockHttp.VerifyNoOutstandingExpectation();

				// assert result was parsed correctly
				Assert.NotNull(result);
				Assert.Equal("cjktcmn5o635wmyd0w4ps", result.Id);
				Assert.Equal("g6jtshln48", result.ApplicationId);
				Assert.Equal("value", result.Data.Value<string>("key"));
				Assert.Equal(5, result.Data.Value<int>("points"));
				Assert.Equal("5 points", result.Name);
				Assert.Equal("5 Points Awarded", result.Description);
				Assert.Equal(new DateTime(2016, 09, 17, 06, 50, 11, 426, DateTimeKind.Utc), result.Meta.Created);
			}
		}
	}
}
