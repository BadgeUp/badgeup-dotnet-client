using BadgeUp;
using BadgeUp.Http;
using BadgeUp.ResourceClients;
using RichardSzalay.MockHttp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace BadgeUp.Tests
{
	public class EventClientTests
	{
		[Fact]
		public async Task WhenShowIncompleteIsFalse_Send_MustNotAppendShowIncompleteQueryParam()
		{
			using (var badgeUpClient = new BadgeUpHttpClient(ApiKey.Create("eyJhY2NvdW50SWQiOiJ0aGViZXN0IiwiYXBwbGljYXRpb25JZCI6IjEzMzciLCJrZXkiOiJpY2VjcmVhbWFuZGNvb2tpZXN5dW0ifQ=="), "https://api.useast1.badgeup.io"))
			{
				// Setup mock response
				// When there's a showIncomplete=false query param, throw an exception.
				var mockHttpHandler = new MockHttpMessageHandler();
				mockHttpHandler.When(HttpMethod.Post, "https://api.useast1.badgeup.io/v2/apps/1337/events*")
							.WithQueryString("showIncomplete", "false")
							.Throw(new InvalidOperationException("URL shouldn't have the showIncomplete param appended"));
				mockHttpHandler.Fallback.Respond("application/json",
				@"{
					'event': {
						'id': 'cja92jvpj1gummf5lf3jj5fx3',
						'applicationId': '1337',
						'subject': 'subject-1',
						'key': 'jump',
						'timestamp': '2017-11-21T03:37:03.559Z',
						'modifier': {
							'@inc': 1
						}
					},
					'progress': [
						{
							'achievementId': 'cj1sp5nse02j9zkruwhb3zwik',
							'earnedAchievementId': 'cj1ss153y02k1zkrun39g8itq',
							'isComplete': true,
							'isNew': true,
							'percentComplete': 1,
							'progressTree': {
								'type': 'GROUP',
								'groups': [],
								'criteria': {
									'cj1sp461o02imzkruqkqi8amh': 1
								},
								'condition': 'AND'
							}
						}
					]
				}".Replace("'", "\""));

				// arrange
				badgeUpClient._SetHttpClient(mockHttpHandler.ToHttpClient());
				var client = new EventClient(badgeUpClient);

				// act
				await client.Send(new BadgeUp.Types.Event("subject-1", "jump"), showIncomplete: false);
				await client.Send(new BadgeUp.Types.Event("subject-1", "jump"), showIncomplete: true);
				await client.Send(new BadgeUp.Types.Event("subject-1", "jump"), showIncomplete: null);
			}
		}

		[Fact]
		public async Task WhenShowIncompleteIsFalse_SendV2Preview_MustNotAppendShowIncompleteQueryParam()
		{
			using (var badgeUpClient = new BadgeUpHttpClient(ApiKey.Create("eyJhY2NvdW50SWQiOiJ0aGViZXN0IiwiYXBwbGljYXRpb25JZCI6IjEzMzciLCJrZXkiOiJpY2VjcmVhbWFuZGNvb2tpZXN5dW0ifQ=="), "https://api.useast1.badgeup.io"))
			{
				// Setup mock response
				// When there's a showIncomplete=false query param, throw an exception.
				var mockHttpHandler = new MockHttpMessageHandler();
				mockHttpHandler.When(HttpMethod.Post, "https://api.useast1.badgeup.io/v2/apps/1337/events*")
							.WithQueryString("showIncomplete", "false")
							.Throw(new InvalidOperationException("URL shouldn't have the showIncomplete param appended"));
				mockHttpHandler.Fallback.Respond("application/json",
				@"{
					'event': {
						'id': 'cja92jvpj1gummf5lf3jj5fx3',
						'applicationId': '1337',
						'subject': 'subject-1',
						'key': 'jump',
						'timestamp': '2017-11-21T03:37:03.559Z',
						'modifier': {
							'@inc': 1
						}
					},
					'progress': [
						{
							'achievementId': 'cj1sp5nse02j9zkruwhb3zwik',
							'earnedAchievementId': 'cj1ss153y02k1zkrun39g8itq',
							'isComplete': true,
							'isNew': true,
							'percentComplete': 1,
							'progressTree': {
								'type': 'GROUP',
								'groups': [],
								'criteria': {
									'cj1sp461o02imzkruqkqi8amh': 1
								},
								'condition': 'AND'
							}
						}
					]
				}".Replace("'", "\""));

				// arrange
				badgeUpClient._SetHttpClient(mockHttpHandler.ToHttpClient());
				var client = new EventClient(badgeUpClient);

				// act
				await client.Send(new BadgeUp.Types.Event("subject-1", "jump"), showIncomplete: false);
				await client.Send(new BadgeUp.Types.Event("subject-1", "jump"), showIncomplete: true);
				await client.Send(new BadgeUp.Types.Event("subject-1", "jump"), showIncomplete: null);
			}
		}
	}
}
