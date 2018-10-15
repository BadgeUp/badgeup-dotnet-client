using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using BadgeUp.Http;
using BadgeUp.Responses;
using BadgeUp.Types;

namespace BadgeUp.ResourceClients
{
	internal class EarnedAchievementClient : IEarnedAchievementClient
	{
		const string ENDPOINT = "earnedachievements";
		protected BadgeUpHttpClient m_httpClient;

		public EarnedAchievementClient(BadgeUpHttpClient httpClient)
		{
			this.m_httpClient = httpClient;
		}

		/// <summary>
		/// Retrieves an earned achievement by ID
		/// </summary>
		/// <param name="id">A string that uniquely identifies this achievement</param>
		/// <returns><see cref="EarnedAchievementResponse"/></returns>
		public Task<EarnedAchievementResponse> GetById(string id)
		{
			return this.m_httpClient.Get<EarnedAchievementResponse>(ENDPOINT + "/" + id);
		}

		/// <summary>
		/// Retrieves all earned achievements
		/// </summary>
		/// <param name="param">Optional QueryParams object, to filter the earned achievements by AchievementId, Subject, Since and Until parameters </param>
		/// <returns><see cref="EarnedAchievementResponse"/></returns>
		public Task<List<EarnedAchievementResponse>> GetAll(EarnedAchievementQueryParams param = null)
		{
			return this.m_httpClient.GetAll<EarnedAchievementResponse>(ENDPOINT, query: param?.ToQueryString());
		}
	}
}
