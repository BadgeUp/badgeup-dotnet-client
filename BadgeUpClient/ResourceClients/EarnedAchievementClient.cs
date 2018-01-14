using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using BadgeUpClient.Http;
using BadgeUpClient.Responses;
using BadgeUpClient.Types;

namespace BadgeUpClient.ResourceClients
{
	public class EarnedAchievementClient
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
		public async Task<EarnedAchievementResponse> GetById(string id)
		{
			return await this.m_httpClient.Get<EarnedAchievementResponse>(ENDPOINT + "/" + id);
		}


		/// <summary>
		/// Retrieves all earned achievements
		/// </summary>
		/// <param name="param">Optional QueryParams object, to filter the earned achievements by AchievementId, Subject, Since and Until parameters </param>
		/// <returns><see cref="EarnedAchievementResponse"/></returns>
		public async Task<List<EarnedAchievementResponse>> GetAll(EarnedAchievementQueryParams param = null)
		{
			return await this.m_httpClient.GetAll<EarnedAchievementResponse>(ENDPOINT, query: HttpQuery.GetQueryStringFromObject(param));
		}

	}
}
