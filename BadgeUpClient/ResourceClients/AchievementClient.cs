using System.Threading.Tasks;
using BadgeUp.Http;
using BadgeUp.Responses;

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
	}
}
