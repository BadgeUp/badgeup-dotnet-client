using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using BadgeUp.Responses;

namespace BadgeUp.Http
{
	public class BadgeUpHttpClient : System.IDisposable
	{
		protected string m_host;
		protected ApiKey m_apiKey;
		protected HttpClient m_httpClient;
		private int _retryCount = 3;

		public BadgeUpHttpClient(ApiKey apiKey, string host)
		{
			this.m_apiKey = apiKey;
			this.m_host = host;

			HttpClient httpClient = new HttpClient();
			httpClient.DefaultRequestHeaders.Accept.Clear();
			httpClient.DefaultRequestHeaders.Accept.Add( new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue( "application/json" ) );
			httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue( "Basic", this.m_apiKey.Auth );
			httpClient.DefaultRequestHeaders.Add("User-Agent", "BadgeUp .NET Client/0.2.0");

			this.m_httpClient = httpClient;
		}

		public async Task<TResponse> Get<TResponse>( string endpointName, string path = "/v1/apps/{applicationId}", string query = null )
		{
			path = path.Replace( "{applicationId}", this.m_apiKey.ApplicationId );
			string responseContent = "";
			for (int i = 0; i < _retryCount; i++)
			{
				try
				{
					var response = await m_httpClient.GetAsync(
						m_host + path.TrimEnd('/') + "/" + endpointName.TrimStart('/') + (query != null ? '?' + query : ""));
					responseContent = await response.Content.ReadAsStringAsync();

					if (response.IsSuccessStatusCode)
					{
						return Json.Deserialize<TResponse>(responseContent);
					}
					//Ignore only 5XX errors
					else if (((int)response.StatusCode).ToString()[0] != '5')
					{
						throw new BadgeUpClientException(responseContent);
					}
				}
				catch (TaskCanceledException)
				{
					//thrown on timeout, ignore.
				}
			}
			throw new BadgeUpClientException(responseContent);

		}

		public async Task<List<TResponse>> GetAll<TResponse>(string endpoint, string path = "/v1/apps/{applicationId}", string query = null)
		{
			var result = new List<TResponse>();
			string url = endpoint;
			do
			{
				var response = await this.Get<MultipleResponse<TResponse>>(url, path, query);
				result.AddRange(response.Data);
				url = response.Pages?.Next;

				//reset path and query to empty string, as they are only needed in the first call
				path = "";
				query = null;
			} while (!string.IsNullOrEmpty(url));

			return result;
		}

		public async Task<TResponse> Post<TResponse>( Request data, string endpointName, string path = "/v1/apps/{applicationId}", string query = null, Dictionary<string, string> headers = null )
		{
			path = path.Replace( "{applicationId}", this.m_apiKey.ApplicationId );

			var content = new StringContent( data.ToJson(), System.Text.Encoding.UTF8, "application/json" );
			if (headers != null)
			{
				foreach (var header in headers)
				{
					content.Headers.Add(header.Key, header.Value);
				}
			}

			string responseContent = "";
			for (int i = 0; i < _retryCount; i++)
			{
				try
				{
					var response = await m_httpClient.PostAsync(
						m_host + path + "/" + endpointName + (query != null ? '?' + query : ""),
						content);
					responseContent = await response.Content.ReadAsStringAsync();

					if (response.IsSuccessStatusCode)
					{
						return Json.Deserialize<TResponse>(responseContent);
					}
					//Ignore only 5XX errors
					else if (((int)response.StatusCode).ToString()[0] != '5')
					{
						throw new BadgeUpClientException(responseContent);
					}
				}
				catch (TaskCanceledException)
				{
					//thrown on timeout, ignore
				}
			}

			throw new BadgeUpClientException(responseContent);

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
