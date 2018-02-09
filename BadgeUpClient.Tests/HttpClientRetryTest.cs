using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BadgeUpClient.Http;
using RichardSzalay.MockHttp;
using Xunit;

namespace BadgeUpClient.Tests
{
    public class HttpClientRetryTest
    {
	    const string Key = "eyJhY2NvdW50SWQiOiJmdmp0c2dsbjFmIiwiYXBwbGljYXRpb25JZCI6Imc2anRzaGxuNDgiLCJrZXkiOiJjamE3NGRjcGJqdHN0bGc3bmY0bW5ieTk5In0=";
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
			mockHttp.Expect(HttpMethod.Get, "http://www.test.com/v1/apps/g6jtshln48/").Respond(r => errorResponse);
			mockHttp.Expect(HttpMethod.Get, "http://www.test.com/v1/apps/g6jtshln48/").Respond(r => errorResponse);
			mockHttp.Expect(HttpMethod.Get, "http://www.test.com/v1/apps/g6jtshln48/").Respond(r => successResponse);

			//Test
			var client = new BadgeUpHttpClient(ApiKey.Create(Key), "http://www.test.com");
			client._SetHttpClient(mockHttp.ToHttpClient());
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
			mockHttp.Expect(HttpMethod.Get, "http://www.test.com/v1/apps/g6jtshln48/").Respond(r => errorResponse);
			mockHttp.Expect(HttpMethod.Get, "http://www.test.com/v1/apps/g6jtshln48/").Respond(r => errorResponse);
			mockHttp.Expect(HttpMethod.Get, "http://www.test.com/v1/apps/g6jtshln48/").Respond(r => errorResponse);

			//Test
			var client = new BadgeUpHttpClient(ApiKey.Create(Key), "http://www.test.com");
			client._SetHttpClient(mockHttp.ToHttpClient());

		    await Assert.ThrowsAsync<BadgeUpClientException>(async () => await client.Get<object>(""));
			mockHttp.VerifyNoOutstandingExpectation();
		}
    }
}
