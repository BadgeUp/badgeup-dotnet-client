using System.Threading.Tasks;
using BadgeUpClient.Http;
using BadgeUpClient.Responses;

namespace BadgeUpClient.ResourceClients
{
	public class AwardClient
	{
		const string ENDPOINT = "awards";
		protected BadgeUpHttpClient m_httpClient;

		public AwardClient(BadgeUpHttpClient httpClient)
		{
			this.m_httpClient = httpClient;
		}

		/// <summary>
		/// Retrieves an award by ID
		/// </summary>
		/// <param name="id">A string that uniquely identifies this award</param>
		/// <returns><see cref="AwardResponse"/></returns>
		public async Task<AwardResponse> GetById(string id)
		{
			return await this.m_httpClient.Get<AwardResponse>(ENDPOINT + "/" + id);
		}
	}
}
