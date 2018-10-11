using System;
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
		public async Task WhenMetricIsNull_CreateThrowsException()
		{
			var client = new MetricClient(null);
			await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create(null));
		}

		[Fact]
		public async Task WhenSubjectAndKeyContainRestrictedCharacters_GetIndividualBySubject_UrlEncodesRestrictedCharacters()
		{
			// Setup mock expectation for a encoded url call
			var mockHttpHandler = new MockHttpMessageHandler();
			var response = @"{
				'id': 'ca98dfajl3kja9kjh34589734',
				'applicationId': 'ci123doiu3',
				'key': 'testKey 123',
				'subject': 'test:subject=123',
				'value': 5
			}".Replace("'", "\"");
			mockHttpHandler.Expect(HttpMethod.Get, "https://api.useast1.badgeup.io/v2/apps/1337/metrics/test%3Asubject%3D123/testKey%20123").Respond("application/json", response);
			using (var badgeUpClient = new BadgeUpHttpClient(ApiKey.Create("eyJhY2NvdW50SWQiOiJ0aGViZXN0IiwiYXBwbGljYXRpb25JZCI6IjEzMzciLCJrZXkiOiJpY2VjcmVhbWFuZGNvb2tpZXN5dW0ifQ=="), "https://api.useast1.badgeup.io"))
			{
				badgeUpClient.SetHttpClient(mockHttpHandler.ToHttpClient());

				// pass subject and key with restricted characters
				var metricClient = new MetricClient(badgeUpClient);
				var metric = await metricClient.GetIndividualBySubject("test:subject=123", "testKey 123");
				Assert.Equal("test:subject=123", metric.Subject);
				Assert.Equal("testKey 123", metric.Key);
			}

			// expect encoded url to have been called
			mockHttpHandler.VerifyNoOutstandingExpectation();
		}

		[Fact]
		public async Task WhenSubjectAndKeyContainRestrictedCharacters_GetAllBySubject_UrlEncodesRestrictedCharacters()
		{
			// Setup mock expectation for a encoded url call
			var mockHttpHandler = new MockHttpMessageHandler();
			var response = @"{
				'pages': {
					'previous': null,
					'next': null
				},
				'data': [
					{
						'id': 'cja8a0980j3jkajhaaa345810',
						'applicationId': 'e7kmr6m0ob',
						'key': 'test',
						'subject': 'test:subject=123',
						'value': 5
					},
					{
						'id': 'cjmx29083hjkaaa325usgaitx',
						'applicationId': 'e7kmr6m0ob',
						'key': 'test:key',
						'subject': 'test:subject=123',
						'value': 1
					},
					{
						'id': 'cjmy215tq87hdo1b9hrb2qlsj',
						'applicationId': 'e7kmr6m0ob',
						'key': 'test:metric',
						'subject': 'test:subject=123',
						'value': 5
					}
				]
			}".Replace("'","\"");
			mockHttpHandler.Expect(HttpMethod.Get, "https://api.useast1.badgeup.io/v2/apps/1337/metrics/test%3Asubject%3D123").Respond("application/json", response);
			using (var badgeUpClient = new BadgeUpHttpClient(ApiKey.Create("eyJhY2NvdW50SWQiOiJ0aGViZXN0IiwiYXBwbGljYXRpb25JZCI6IjEzMzciLCJrZXkiOiJpY2VjcmVhbWFuZGNvb2tpZXN5dW0ifQ=="), "https://api.useast1.badgeup.io"))
			{
				badgeUpClient.SetHttpClient(mockHttpHandler.ToHttpClient());

				// pass subject and key with restricted characters
				var metricClient = new MetricClient(badgeUpClient);
				var metrics = await metricClient.GetAllBySubject("test:subject=123");
				Assert.Equal(3, metrics.Count);
				Assert.Equal("test:subject=123", metrics[0].Subject);
				Assert.Equal("test:subject=123", metrics[1].Subject);
				Assert.Equal("test:subject=123", metrics[2].Subject);
				Assert.Equal("test", metrics[0].Key);
				Assert.Equal("test:key", metrics[1].Key);
				Assert.Equal("test:metric", metrics[2].Key);
			}

			// expect encoded url to have been called
			mockHttpHandler.VerifyNoOutstandingExpectation();
		}
	}
}
