using System.Net.Http;
using System.Threading.Tasks;
using BadgeUp.Http;
using BadgeUp.ResourceClients;
using RichardSzalay.MockHttp;
using Xunit;

namespace BadgeUp.Tests
{
	public class MetricClientTests
	{
		[Fact]
		public async Task WhenSubjectAndKeyContainRestrictedCharacters_GetIndividualBySubject_UrlEncodesRestrictedCharacters()
		{
			// Setup mock expectation for a encoded url call
			var mockHttpHandler = new MockHttpMessageHandler();
			mockHttpHandler.Expect(HttpMethod.Get, "https://api.useast1.badgeup.io/v1/apps/1337/metrics/test%3Asubject%3D123/testKey%20123").Respond("application/json", "{}");
			using (var badgeUpClient = new BadgeUpHttpClient(ApiKey.Create("eyJhY2NvdW50SWQiOiJ0aGViZXN0IiwiYXBwbGljYXRpb25JZCI6IjEzMzciLCJrZXkiOiJpY2VjcmVhbWFuZGNvb2tpZXN5dW0ifQ=="), "https://api.useast1.badgeup.io"))
			{
				badgeUpClient._SetHttpClient(mockHttpHandler.ToHttpClient());

				// pass subject and key with restricted characters
				var metricClient = new MetricClient(badgeUpClient);
				var metric = await metricClient.GetIndividualBySubject("test:subject=123", "testKey 123");
			}

			// expect encoded url to have been called
			mockHttpHandler.VerifyNoOutstandingExpectation();
		}
	}
}
