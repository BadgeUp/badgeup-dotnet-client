using System.Collections.Generic;
using System.Threading.Tasks;
using BadgeUp.Http;
using BadgeUp.Responses;

namespace BadgeUp.ResourceClients
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
			return await this.m_httpClient.GetAll<MetricResponse>(ENDPOINT);
		}
	}
}
