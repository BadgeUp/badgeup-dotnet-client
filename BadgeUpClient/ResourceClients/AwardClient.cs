using System.Collections.Generic;
using System.Threading.Tasks;
using BadgeUp.Http;
using BadgeUp.Responses;

namespace BadgeUp.ResourceClients
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

		/// <summary>
		/// Retrieves a list of all awards.
		/// </summary>
		/// <returns></returns>
		public async Task<List<AwardResponse>> GetAll()
		{
			return await this.m_httpClient.GetAll<AwardResponse>(ENDPOINT);
		}
	}
}
