using System.Threading.Tasks;
using BadgeUpClient.Http;
using BadgeUpClient.Responses;

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
		/// Retrieves an achievement by ID
		/// </summary>
		/// <param name="id">A string that uniquely identifies this achievement</param>
		/// <returns><see cref="EarnedAchievementResponse"/></returns>
		public async Task<EarnedAchievementResponse> GetById(string id)
		{
			return await this.m_httpClient.Get<EarnedAchievementResponse>(ENDPOINT + "/" + id);
		}
	}
}
