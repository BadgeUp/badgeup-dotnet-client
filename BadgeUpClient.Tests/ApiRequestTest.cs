using System.Net.Http;
using RichardSzalay.MockHttp;
using Xunit;

namespace BadgeUpClient.Tests
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
				var result = client.SendEvent( new Types.Event( "subject_foo", "eat:apple", new Types.Modifier { Inc = 1 } ) ).Result;

				Assert.Equal( "subject_foo", result.Event.Subject );
				Assert.Equal( "eat:apple", result.Event.Key );
			}
		}

		[Fact]
		public async void ApiRequest_Uri()
		{
			var mockHttp = new MockHttpMessageHandler();

			var apiKey = global::BadgeUpClient.ApiKey.Create( ApiRequestTest.ApiKey );
			string url = "https://api.useast1.badgeup.io/v1/apps/" + apiKey.ApplicationId + "/events";

			string responseJson =
@"{
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
}".Replace("'", "\"");

			// setup the response
			var response = new HttpResponseMessage(System.Net.HttpStatusCode.Created)
				{
					Content = new StringContent(responseJson)
				};
			response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

			// setup the response action
			mockHttp.When(HttpMethod.Post, url).WithContent("{\"subject\":\"subject_foo\",\"key\":\"eat:apple\",\"modifier\":{\"@inc\":1}}").Respond(req => response);

			using (var client = new BadgeUpClient( ApiRequestTest.ApiKey ))
			{

				client._SetHttpClient(mockHttp.ToHttpClient());

				var result = await client.SendEvent( new Types.Event( "subject_foo", "eat:apple", new Types.Modifier { Inc = 1 } ) );

				Assert.NotNull(result.Event);
				Assert.Single(result.Progress);
			}
		}
	}
}
