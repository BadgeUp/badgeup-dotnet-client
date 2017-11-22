using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BadgeUpClient.Http;
using BadgeUpClient.Requests;
using BadgeUpClient.Responses;
using BadgeUpClient.Types;

namespace BadgeUpClient
{
	public class BadgeUpClient : BadgeUpClientInterface, IDisposable
	{
		protected string m_host;
		protected ApiKey m_apiKey;
		protected HttpClient m_httpClient;

		public BadgeUpClient( ApiKey apiKey, string host )
		{
			m_apiKey = apiKey;
			m_host = host;

			m_httpClient = CreateHttpClient();
		}

		public BadgeUpClient( string apiKey, string host = "https://api.useast1.badgeup.io" )
			: this( ApiKey.Create( apiKey ), host )
		{
		}

		public override async Task<EventResponse> SendEvent( Event @event, bool? showIncomplete = null, bool? discard = null )
		{
			HttpQuery query = new HttpQuery();

			if (showIncomplete.HasValue)
			{
				query.Add( "showIncomplete", showIncomplete.Value );
			}

			if (discard.HasValue)
			{
				query.Add( "showIncomplete", discard.Value );
			}

			return await Post<EventResponse>( new EventRequest( @event ), "events", query: query.ToString() );
		}

		protected HttpClient CreateHttpClient()
		{
			var result = new HttpClient();
			result.DefaultRequestHeaders.Accept.Clear();
			result.DefaultRequestHeaders.Accept.Add( new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue( "application/json" ) );
			result.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue( "Basic", m_apiKey.Auth );

			return result;
		}

		protected async Task<TResponse> Post<TResponse>( Request data, string endpointName, string path = "/v1/apps/{applicationId}", string query = null )
		{
			path = path.Replace( "{applicationId}", m_apiKey.ApplicationId );

			var content = new StringContent( data.ToJson(), Encoding.UTF8, "application/json" );

			var response = await m_httpClient.PostAsync(
				m_host + path + "/" + endpointName + (query != null ? '?' + query : ""),
				content );

			var responseContent = await response.Content.ReadAsStringAsync();

			if (response.StatusCode != System.Net.HttpStatusCode.Created)
			{
				throw new BadgeUpClientException( responseContent );
			}

			return Json.Deserialize<TResponse>( responseContent );
		}

		// for test purposes only
		public void _SetHttpClient(HttpClient h)
		{
			this.m_httpClient = h;
		}

		public void Dispose()
		{
			m_httpClient.Dispose();
		}
	}
}
