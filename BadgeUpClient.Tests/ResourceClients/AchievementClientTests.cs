using System;
using System.Net.Http;
using System.Threading.Tasks;
using BadgeUp.Http;
using BadgeUp.ResourceClients;
using BadgeUp.Types;
using RichardSzalay.MockHttp;
using Xunit;

namespace BadgeUp.Tests
{
	public class AchievementClientTests
	{
		private const string ApiKey = "eyJhY2NvdW50SWQiOiJmdmp0c2dsbjFmIiwiYXBwbGljYXRpb25JZCI6Imc2anRzaGxuNDgiLCJrZXkiOiJjamE3NGRjcGJqdHN0bGc3bmY0bW5ieTk5In0=";
		private const string Host = "https://api.useast1.badgeup.io";

		[Fact]
		public async Task WhenAchievementIsNull_CreateThrowsException()
		{
			var client = new AchievementClient(null);
			await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create(null));
		}

		[Fact]
		public async Task WhenSetupWithResponseData_Create_CallsUrlOnceAndReturnsCorrectResult()
		{
			var apiKey = BadgeUp.ApiKey.Create(ApiKey);

			var responseJson =
			@"{
				'id': 'cjktcmn5o635wmyd0w4ps',
				'applicationId': 'g6jtshln48',
				'name': 'Achievement name',
				'description': 'Achievement description',
				'awards': ['award-1','award-2','award-3'],
				'options': {
					'earnLimit': 5,
					'suspended': true
				},
				'meta': {
					'created': '2016-09-12T06:51:35.453Z',
					'icon': 'https://example.com/favicon.ico'
				}
			}".Replace("'", "\"");

			// setup the response action
			var url = $"{Host}/v1/apps/{apiKey.ApplicationId}/achievements";
			var mockHttp = new MockHttpMessageHandler();
			var expectedRequest = mockHttp.Expect(HttpMethod.Post, url).Respond("application/json", responseJson);
			mockHttp.Fallback.Throw(new InvalidOperationException("No matching mock handler"));

			using (var badgeUpHttpClient = new BadgeUpHttpClient(apiKey, Host))
			{
				badgeUpHttpClient._SetHttpClient(mockHttp.ToHttpClient());
				var client = new AchievementClient(badgeUpHttpClient);

				var result = await client.Create(new Achievement()
				{
					Name = "Achievement name",
					Description = "Achievement description",
					Awards = new[] { "award-1", "award-2", "award-3" },
					// TODO: Add simple EvalTree test once client.Criterion.GetAll() is completed
					EvalTree = null,
					Options = new AchievementOptions()
					{
						EarnLimit = 5,
						Suspended = true
					},
					Meta = new AchievementMeta()
					{
						Icon = "https://example.com/favicon.ico"
					}
				});

				Assert.Equal("Achievement name", result.Name);
				Assert.Equal("Achievement description", result.Description);
				Assert.Equal("cjktcmn5o635wmyd0w4ps", result.Id);
				Assert.Equal(new[] { "award-1", "award-2", "award-3" }, result.Awards);
				Assert.True(result.Options.Suspended);
				Assert.Equal(5, result.Options.EarnLimit);
				Assert.Equal(new DateTime(2016, 09, 12, 06, 51, 35, 453), result.Meta.Created);
				Assert.Equal("https://example.com/favicon.ico", result.Meta.Icon);
			}

			mockHttp.VerifyNoOutstandingExpectation();
		}
	}
}
