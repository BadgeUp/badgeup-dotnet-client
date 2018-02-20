using System.Threading.Tasks;
using BadgeUp.Http;
using BadgeUp.Responses;

namespace BadgeUp.ResourceClients
{
	public class CriterionClient
	{
		const string ENDPOINT = "criteria";
		protected BadgeUpHttpClient m_httpClient;

		public CriterionClient(BadgeUpHttpClient httpClient)
		{
			this.m_httpClient = httpClient;
		}

		/// <summary>
		/// Retrieves a criterion by ID
		/// </summary>
		/// <param name="id">A string that uniquely identifies this criterion</param>
		/// <returns><see cref="CriterionResponse"/></returns>
		public async Task<CriterionResponse> GetById(string id)
		{
			return await this.m_httpClient.Get<CriterionResponse>(ENDPOINT + "/" + id);
		}
	}
}
