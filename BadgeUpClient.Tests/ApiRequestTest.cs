using System;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using RichardSzalay.MockHttp;
using Xunit;

namespace BadgeUp.Tests
{
	public class ApiRequestTest
	{
		const string ApiKey = "eyJhY2NvdW50SWQiOiJmdmp0c2dsbjFmIiwiYXBwbGljYXRpb25JZCI6Imc2anRzaGxuNDgiLCJrZXkiOiJjamE3NGRjcGJqdHN0bGc3bmY0bW5ieTk5In0=";

		[Fact]
		public void ApiRequest_EventPost()
		{
			Assert.False( string.IsNullOrEmpty( ApiRequestTest.ApiKey ) );

			using (var client = new BadgeUpClient( ApiKey ))
			{
				var result = client.Event.Send( new Types.Event( "subject_foo", "eat:apple", new Types.Modifier { Inc = 1 } ) ).Result;

				Assert.Equal( "subject_foo", result.Results[0].Event.Subject );
				Assert.Equal( "eat:apple", result.Results[0].Event.Key );
			}
		}

		[Fact]
		public async void ApiRequest_Uri()
		{
			var mockHttp = new MockHttpMessageHandler();

			var apiKey = global::BadgeUp.ApiKey.Create( ApiRequestTest.ApiKey );
			string url = "https://api.useast1.badgeup.io/v2/apps/" + apiKey.ApplicationId + "/events";

			string responseJson =
@"{results: [{
	'event': {
		'id': 'cja92jvpj1gummf5lf3jj5fx3',
		'applicationId': '1337',
		'subject': '100',
		'key': 'eat:apple',
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
}]}".Replace("'", "\"");

			// setup the response
			var response = new HttpResponseMessage(System.Net.HttpStatusCode.Created)
				{
					Content = new StringContent(responseJson)
				};
			response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

			// setup the response action
			mockHttp.When(HttpMethod.Post, url).WithContent("{\"subject\":\"subject_foo\",\"key\":\"eat:apple\",\"timestamp\":\"2017-01-01T18:00:00+05:30\",\"data\":{\"test-key\":5},\"modifier\":{\"@inc\":1}}").Respond(req => response);

			using (var client = new BadgeUpClient(ApiRequestTest.ApiKey ))
			{

				client._SetHttpClient(mockHttp.ToHttpClient());

				// create event and set properties
				var e = new Types.Event("subject_foo", "eat:apple", new Types.Modifier { Inc = 1 });
				e.Timestamp = new DateTimeOffset(2017, 1, 1, 18, 0, 0, new TimeSpan(5, 30, 0));
				e.Data = new JObject { { "test-key", 5 } };

				// send the event
				var result = await client.Event.Send(e);

				Assert.NotNull(result);
				Assert.NotEmpty(result.Results);
				Assert.NotNull(result.Results[0].Event);
				Assert.Single(result.Results[0].Progress);
			}
		}
	}
}
