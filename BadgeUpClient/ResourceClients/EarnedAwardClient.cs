using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using BadgeUp.Http;
using BadgeUp.Responses;
using BadgeUp.Types;

namespace BadgeUp.ResourceClients
{
	public class EarnedAwardClient
	{
		const string ENDPOINT = "earnedawards";
		protected BadgeUpHttpClient m_httpClient;

		public EarnedAwardClient(BadgeUpHttpClient httpClient)
		{
			this.m_httpClient = httpClient;
		}

		/// <summary>
		/// Retrieves an earned achievement by ID
		/// </summary>
		/// <param name="id">A string that uniquely identifies this achievement</param>
		/// <returns><see cref="EarnedAwardResponse"/></returns>
		public async Task<EarnedAwardResponse> GetById(string id)
		{
			return await this.m_httpClient.Get<EarnedAwardResponse>(ENDPOINT + "/" + id);
		}

		/// <summary>
		/// Retrieves all earned achievements
		/// </summary>
		/// <param name="param">Optional QueryParams object, to filter the earned achievements by AwardId, EarnedAchievementId, Subject, Since and Until parameters</param>
		/// <returns><see cref="EarnedAwardResponse"/></returns>
		public async Task<List<EarnedAwardResponse>> GetAll(EarnedAwardQueryParams param = null)
		{
			return await this.m_httpClient.GetAll<EarnedAwardResponse>(ENDPOINT, query: param?.ToQueryString());
		}

	}
}
