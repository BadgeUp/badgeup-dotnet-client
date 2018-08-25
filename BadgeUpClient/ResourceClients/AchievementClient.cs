using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BadgeUp.Http;
using BadgeUp.Requests;
using BadgeUp.Responses;
using BadgeUp.Types;

namespace BadgeUp.ResourceClients
{
	public class AchievementClient
	{
		const string ENDPOINT = "achievements";
		protected BadgeUpHttpClient m_httpClient;

		public AchievementClient(BadgeUpHttpClient httpClient)
		{
			this.m_httpClient = httpClient;
		}

		/// <summary>
		/// Retrieves an achievement by ID
		/// </summary>
		/// <param name="id">A string that uniquely identifies this achievement</param>
		/// <returns><see cref="AchievementResponse"/></returns>
		public async Task<AchievementResponse> GetById(string id)
		{
			return await this.m_httpClient.Get<AchievementResponse>(ENDPOINT + "/" + id);
		}

		/// <summary>
		/// Creates a single achievement with the given parameters.
		/// </summary>
		/// <param name="achievement">The achievement to create.</param>
		/// <returns>The created achievement.</returns>
		public async Task<AchievementResponse> CreateAchievement(Achievement achievement)
		{
			var request = new AchievementRequest(achievement);
			var result = await this.m_httpClient.Post<AchievementResponse>(request, ENDPOINT);
			return result;
		}

		/// <summary>
		/// Retrieves a list of all achievements.
		/// </summary>
		/// <returns>The list of all achievements.</returns>
		public async Task<List<AchievementResponse>> GetAll()
		{
			return await this.m_httpClient.GetAll<AchievementResponse>(ENDPOINT);
		}
	}
}
