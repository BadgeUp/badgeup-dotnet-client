using System.Threading.Tasks;
using BadgeUp.Http;
using BadgeUp.Responses;

namespace BadgeUp.ResourceClients
{
	public class ApplicationClient
	{
		const string ENDPOINT = "v2/apps";
		protected BadgeUpHttpClient m_httpClient;

		public ApplicationClient(BadgeUpHttpClient httpClient)
		{
			this.m_httpClient = httpClient;
		}

		/// <summary>
		/// Retrieves an application by ID
		/// </summary>
		/// <param name="id">A string that uniquely identifies this application</param>
		/// <returns><see cref="ApplicationResponse"/></returns>
		public async Task<ApplicationResponse> GetById(string id)
		{
			return await this.m_httpClient.Get<ApplicationResponse>(ENDPOINT + "/" + id, "");
		}
	}
}
