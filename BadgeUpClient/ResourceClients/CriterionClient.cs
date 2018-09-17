using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BadgeUp.Http;
using BadgeUp.Requests;
using BadgeUp.Responses;
using BadgeUp.Types;

namespace BadgeUp.ResourceClients
{
	public class CriterionClient
	{
		private const string ENDPOINT = "criteria";
		protected BadgeUpHttpClient m_httpClient;

		public CriterionClient(BadgeUpHttpClient httpClient)
		{
			this.m_httpClient = httpClient;
		}

		/// <summary>
		/// Retrieves a criterion by ID.
		/// </summary>
		/// <param name="id">A string that uniquely identifies this criterion</param>
		/// <returns><see cref="CriterionResponse"/></returns>
		public async Task<CriterionResponse> GetById(string id)
		{
			return await this.m_httpClient.Get<CriterionResponse>(ENDPOINT + "/" + id);
		}

		/// <summary>
		/// Retrieves a list of all criteria.
		/// </summary>
		/// <returns>The list of all criteria.</returns>
		public async Task<List<CriterionResponse>> GetAll()
		{
			return await this.m_httpClient.GetAll<CriterionResponse>(ENDPOINT);
		}

		/// <summary>
		/// Creates a single criterion with the given parameters.
		/// </summary>
		/// <param name="criterion">The criterion to create.</param>
		/// <returns>The created criterion.</returns>
		public async Task<CriterionResponse> CreateCriterion(Criterion criterion)
		{
			if (criterion == null)
			{
				throw new ArgumentNullException(nameof(criterion));
			}

			var request = new CriterionRequest(criterion);
			var result = await this.m_httpClient.Post<CriterionResponse>(request, ENDPOINT);
			return result;
		}
	}
}
