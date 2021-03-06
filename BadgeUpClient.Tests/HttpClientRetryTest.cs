using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BadgeUp.Http;
using BadgeUp.Requests;
using BadgeUp.Types;
using RichardSzalay.MockHttp;
using Xunit;

namespace BadgeUp.Tests
{
	public class HttpClientRetryTest
	{
		// fake API key
		private const string Key = "eyJhY2NvdW50SWQiOiJ0aGViZXN0IiwiYXBwbGljYXRpb25JZCI6IjEzMzciLCJrZXkiOiJpY2VjcmVhbWFuZGNvb2tpZXN5dW0ifQ==";

		[Fact]
		public async void HttpClient_Get_Retry_success_with_two_errors()
		{
			// setup the responses
			var errorResponse = new HttpResponseMessage(HttpStatusCode.InternalServerError)
			{
				Content = new StringContent("{content}")
			};
			var successResponse = new HttpResponseMessage(HttpStatusCode.OK)
			{
				Content = new StringContent("{foo: \"bar\"}")
			};

			//setup mock httpClient
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Get, "http://www.x.badgeup.io/v2/apps/1337/").Respond(r => errorResponse);
			mockHttp.Expect(HttpMethod.Get, "http://www.x.badgeup.io/v2/apps/1337/").Throw(new TaskCanceledException("Simulated timeout exception"));
			mockHttp.Expect(HttpMethod.Get, "http://www.x.badgeup.io/v2/apps/1337/").Respond(r => successResponse);

			//Test
			var client = new BadgeUpHttpClient(ApiKey.Create(Key), "http://www.x.badgeup.io");
			client.SetHttpClient(mockHttp.ToHttpClient());
			await client.Get<object>("");

			mockHttp.VerifyNoOutstandingExpectation();
		}

		[Fact]
		public async void HttpClient_Get_Retry_fails_after_three_errors()
		{
			// setup the responses
			var errorResponse = new HttpResponseMessage(HttpStatusCode.InternalServerError)
			{
				Content = new StringContent("{content}")
			};

			//setup mock httpClient
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Get, "http://www.x.badgeup.io/v2/apps/1337/").Respond(r => errorResponse);
			mockHttp.Expect(HttpMethod.Get, "http://www.x.badgeup.io/v2/apps/1337/").Throw(new TaskCanceledException("Simulated timeout exception"));
			mockHttp.Expect(HttpMethod.Get, "http://www.x.badgeup.io/v2/apps/1337/").Respond(r => errorResponse);

			//Test
			var client = new BadgeUpHttpClient(ApiKey.Create(Key), "http://www.x.badgeup.io");
			client.SetHttpClient(mockHttp.ToHttpClient());

			await Assert.ThrowsAsync<BadgeUpClientException>(async () => await client.Get<object>(""));
			mockHttp.VerifyNoOutstandingExpectation();
		}

		[Fact]
		public async void HttpClient_Post_Retry_success_with_two_errors()
		{
			// setup the responses
			var errorResponse = new HttpResponseMessage(HttpStatusCode.InternalServerError)
			{
				Content = new StringContent("{content}")
			};
			var successResponse = new HttpResponseMessage(HttpStatusCode.Created)
			{
				Content = new StringContent("{foo: \"bar\"}")
			};

			//setup mock httpClient
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, "http://www.x.badgeup.io/v2/apps/1337/testEndpoint").Respond(r => errorResponse);
			mockHttp.Expect(HttpMethod.Post, "http://www.x.badgeup.io/v2/apps/1337/testEndpoint").Throw(new TaskCanceledException("Simulated timeout exception"));
			mockHttp.Expect(HttpMethod.Post, "http://www.x.badgeup.io/v2/apps/1337/testEndpoint").Respond(r => successResponse);

			//Test
			var client = new BadgeUpHttpClient(ApiKey.Create(Key), "http://www.x.badgeup.io");
			client.SetHttpClient(mockHttp.ToHttpClient());
			await client.Post<object>(new EventRequest(new Event("subject","key")), "testEndpoint");

			mockHttp.VerifyNoOutstandingExpectation();
		}

		[Fact]
		public async void HttpClient_Post_Retry_fails_after_three_errors()
		{
			// setup the responses
			var errorResponse = new HttpResponseMessage(HttpStatusCode.InternalServerError)
			{
				Content = new StringContent("{content}")
			};

			//setup mock httpClient
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, "http://www.x.badgeup.io/v2/apps/1337/testEndpoint").Respond(r => errorResponse);
			mockHttp.Expect(HttpMethod.Post, "http://www.x.badgeup.io/v2/apps/1337/testEndpoint").Throw(new TaskCanceledException("Simulated timeout exception"));
			mockHttp.Expect(HttpMethod.Post, "http://www.x.badgeup.io/v2/apps/1337/testEndpoint").Respond(r => errorResponse);

			//Test
			var client = new BadgeUpHttpClient(ApiKey.Create(Key), "http://www.x.badgeup.io");
			client.SetHttpClient(mockHttp.ToHttpClient());

			await Assert.ThrowsAsync<BadgeUpClientException>(async () => await client.Post<object>(new EventRequest(new Event("subject", "key")), "testEndpoint"));
			mockHttp.VerifyNoOutstandingExpectation();
		}
	}
}
