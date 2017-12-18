using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using BadgeUpClient.Responses;

namespace BadgeUpClient.Http
{
	public class BadgeUpHttpClient : System.IDisposable
	{
		protected string m_host;
		protected ApiKey m_apiKey;
		protected HttpClient m_httpClient;

		public BadgeUpHttpClient(ApiKey apiKey, string host)
		{
			this.m_apiKey = apiKey;
			this.m_host = host;

			HttpClient httpClient = new HttpClient();
			httpClient.DefaultRequestHeaders.Accept.Clear();
			httpClient.DefaultRequestHeaders.Accept.Add( new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue( "application/json" ) );
			httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue( "Basic", this.m_apiKey.Auth );
			httpClient.DefaultRequestHeaders.Add("User-Agent", "BadgeUp .NET Client/0.1.0");

			this.m_httpClient = httpClient;
		}

		public async Task<TResponse> Get<TResponse>( string endpointName, string path = "/v1/apps/{applicationId}", string query = null )
		{
			path = path.Replace( "{applicationId}", this.m_apiKey.ApplicationId );


			var response = await m_httpClient.GetAsync(
				m_host + path.TrimEnd('/') + "/" + endpointName.TrimStart('/') + (query != null ? '?' + query : "") );

			var responseContent = await response.Content.ReadAsStringAsync();

			if (response.StatusCode != System.Net.HttpStatusCode.OK)
			{
				throw new BadgeUpClientException( responseContent );
			}

			return Json.Deserialize<TResponse>(responseContent);
		}

		public async Task<List<TResponse>> GetAll<TResponse>(string endpoint, string path = "/v1/apps/{applicationId}", string query = null)
		{
			var result = new List<TResponse>();
			string url = endpoint;
			do
			{
				var response = await this.Get<MultipleResponse<TResponse>>(url, path);
				result.AddRange(response.Data);
				url = response.Pages?.Next;

				//reset path to empty string, as it is only needed in the first call
				path = "";
			} while (!string.IsNullOrEmpty(url));

			return result;
		}

		public async Task<TResponse> Post<TResponse>( Request data, string endpointName, string path = "/v1/apps/{applicationId}", string query = null )
		{
			path = path.Replace( "{applicationId}", this.m_apiKey.ApplicationId );

			var content = new StringContent( data.ToJson(), System.Text.Encoding.UTF8, "application/json" );

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
