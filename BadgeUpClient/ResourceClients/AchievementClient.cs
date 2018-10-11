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
		private const string ENDPOINT = "achievements";
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
		public Task<AchievementResponse> GetById(string id)
		{
			return this.m_httpClient.Get<AchievementResponse>(ENDPOINT + "/" + id);
		}

		/// <summary>
		/// Creates a single achievement with the given parameters.
		/// </summary>
		/// <param name="achievement">The achievement to create.</param>
		/// <returns>The created achievement.</returns>
		public Task<AchievementResponse> Create(Achievement achievement)
		{
			if (achievement == null)
			{
				throw new ArgumentNullException(nameof(achievement));
			}

			var request = new AchievementRequest(achievement);
			return this.m_httpClient.Post<AchievementResponse>(request, ENDPOINT);
		}

		/// <summary>
		/// Retrieves a list of all achievements.
		/// </summary>
		/// <returns>The list of all achievements.</returns>
		public Task<List<AchievementResponse>> GetAll()
		{
			return this.m_httpClient.GetAll<AchievementResponse>(ENDPOINT);
		}

		/// <summary>
		/// Retrieves a list of criteria associated with an achievement.
		/// </summary>
		/// <param name="achievementId">Unique achievement ID.</param>
		/// <returns></returns>
		public Task<List<CriterionResponse>> GetAchievementCriteria(string achievementId)
		{
			return this.m_httpClient.GetAll<CriterionResponse>(ENDPOINT + "/" + achievementId + "/criteria");
		}

		/// <summary>
		/// Retrieves a list of awards associated with an achievement.
		/// </summary>
		/// <param name="achievementId">Unique achievement ID.</param>
		/// <returns></returns>
		public Task<List<AwardResponse>> GetAchievementAwards(string achievementId)
		{
			return this.m_httpClient.GetAll<AwardResponse>(ENDPOINT + "/" + achievementId + "/awards");
		}
	}
}
