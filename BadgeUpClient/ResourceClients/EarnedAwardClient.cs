using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BadgeUp.Http;
using BadgeUp.Requests;
using BadgeUp.Responses;
using BadgeUp.Types;

namespace BadgeUp.ResourceClients
{
	internal class EarnedAwardClient : IEarnedAwardClient
	{
		private const string ENDPOINT = "earnedawards";
		public BadgeUpHttpClient m_httpClient;

		internal EarnedAwardClient(BadgeUpHttpClient httpClient)
		{
			this.m_httpClient = httpClient;
		}

		/// <summary>
		/// Retrieves an earned award by ID
		/// </summary>
		/// <param name="id">A string that uniquely identifies this award</param>
		/// <returns><see cref="EarnedAwardResponse"/></returns>
		public Task<EarnedAwardResponse> GetById(string id)
		{
			return this.m_httpClient.Get<EarnedAwardResponse>(ENDPOINT + "/" + id);
		}

		/// <summary>
		/// Retrieves all earned awards
		/// </summary>
		/// <param name="param">Optional QueryParams object, to filter the earned awards by AwardId, EarnedAchievementId, Subject, Since and Until parameters</param>
		/// <returns><see cref="EarnedAwardResponse"/></returns>
		public Task<List<EarnedAwardResponse>> GetAll(EarnedAwardQueryParams param = null)
		{
			return this.m_httpClient.GetAll<EarnedAwardResponse>(ENDPOINT, query: param?.ToQueryString());
		}

		public async Task<EarnedAwardResponse> ChangeState(string id, EarnedAwardState state)
		{
			if (id == null)
			{
				throw new ArgumentNullException(nameof(id));
			}

			if (state == EarnedAwardState.Created)
			{
				throw new ArgumentException("State must be one of [APPROVED, REJECTED, REDEEMED].");
			}

			var request = new EarnedAwardRequest(state);
			var result = await this.m_httpClient.Post<EarnedAwardResponse>(request, ENDPOINT + "/" + id + "/state");

			// Return null when the REST endpoint returns an empty JSON object ( {} ).
			if(string.IsNullOrEmpty(result?.Id))
			{
				return null;
			}

			return result;
		}
	}
}
