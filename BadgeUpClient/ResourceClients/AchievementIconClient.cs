using System.Threading.Tasks;
using BadgeUp.Http;
using BadgeUp.Responses;

namespace BadgeUp.ResourceClients
{
	public class AchievementIconClient
	{
		const string ENDPOINT = "achievementicons";
		protected BadgeUpHttpClient m_httpClient;

		public AchievementIconClient(BadgeUpHttpClient httpClient)
		{
			this.m_httpClient = httpClient;
		}

		/// <summary>
		/// Retrieves all uploaded achievement icons
		/// </summary>
		/// <returns><see cref="AchievementIconResponse"/></returns>
		public async Task<AchievementIconResponse[]> GetAll()
		{
			return await this.m_httpClient.Get<AchievementIconResponse[]>(ENDPOINT);
		}
	}
}
