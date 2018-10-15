using System.Collections.Generic;
using System.Threading.Tasks;
using BadgeUp.Http;
using BadgeUp.Responses;
using BadgeUp.Extensions;
using BadgeUp.Requests;
using BadgeUp.Types;
using System;

namespace BadgeUp.ResourceClients
{
	internal class MetricClient : IMetricClient
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
		public Task<MetricResponse> GetIndividualBySubject(string subjectId, string key)
		{
			return this.m_httpClient.Get<MetricResponse>(ENDPOINT + "/" + subjectId.UrlEncode() + "/" + key.UrlEncode());
		}

		/// <summary>
		/// Retrieves a list of all metrics.
		/// </summary>
		/// <returns></returns>
		public Task<List<MetricResponse>> GetAll()
		{
			return this.m_httpClient.GetAll<MetricResponse>(ENDPOINT);
		}

		/// <summary>
		/// Retrieves a list of metrics for a single subject.
		/// </summary>
		/// <param name="subjectId">Unique subject the metrics is associated with.</param>
		/// <returns></returns>
		public Task<List<MetricResponse>> GetAllBySubject(string subjectId)
		{
			return this.m_httpClient.GetAll<MetricResponse>(ENDPOINT + "/" + subjectId.UrlEncode());
		}

		/// <summary>
		/// Creates a single metric.
		/// </summary>
		/// <param name="key">States what the metric represents, such as a player action or other occurrence. You may want to create your keys in groups, e.g. "player:jump".</param>
		/// <param name="subject">Identifies the subject this metric is for. This would commonly be a unique user identifier.</param>
		/// <param name="value">Value of this metric.</param>
		/// <returns></returns>
		public Task<MetricResponse> Create(Metric metric)
		{
			if (metric == null)
			{
				throw new ArgumentNullException(nameof(metric));
			}

			var request = new MetricRequest(metric);

			return this.m_httpClient.Post<MetricResponse>(request, ENDPOINT + "/" + metric.Subject.UrlEncode());
		}
	}
}
