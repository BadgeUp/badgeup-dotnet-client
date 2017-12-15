using System.Collections.Generic;
using System.Threading.Tasks;
using BadgeUpClient.Http;
using BadgeUpClient.Responses;

namespace BadgeUpClient.ResourceClients
{
	public class MetricClient
	{
		const string ENDPOINT = "metrics";
		protected BadgeUpHttpClient m_httpClient;

		public MetricClient(BadgeUpHttpClient httpClient)
		{
			this.m_httpClient = httpClient;
		}

		/// <summary>
		/// Retrieves an individual metric for a single subject
		/// </summary>
		/// <param name="subjectId">Unique subject the metrics is associated with</param>
		/// <param name="key">Unique metric key the subject's metric is associated with</param>
		/// <returns><see cref="MetricResponse"/></returns>
		public async Task<MetricResponse> GetIndividualBySubject(string subjectId, string key)
		{
			return await this.m_httpClient.Get<MetricResponse>(ENDPOINT + "/" + subjectId + "/" + key);
		}


		/// <summary>
		/// Retrieves a list of all metrics.
		/// </summary>
		/// <returns></returns>
		public async Task<List<MetricResponse>> GetAll()
		{
			var result = new List<MetricResponse>();
			string path = "/v1/apps/{applicationId}";

			string url = ENDPOINT;

			do
			{
				var response = await this.m_httpClient.Get<MetricMultipleResponse>(url, path);
				result.AddRange(response.Data);
				url = response.Pages?.Next;

				//reset path to empty string, as it is only needed in the first call
				path = "";
			} while (!string.IsNullOrEmpty(url));

			return result;
		}
	}
}
