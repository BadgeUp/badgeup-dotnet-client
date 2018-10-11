using System.Threading.Tasks;
using BadgeUp.Http;
using BadgeUp.Responses;

namespace BadgeUp.ResourceClients
{
	internal class AccountClient : IAccountClient
	{
		const string ENDPOINT = "accounts";
		protected BadgeUpHttpClient m_httpClient;

		public AccountClient(BadgeUpHttpClient httpClient)
		{
			this.m_httpClient = httpClient;
		}

		/// <summary>
		/// Retrieves an account by ID
		/// </summary>
		/// <param name="id">A string that uniquely identifies this account</param>
		/// <returns><see cref="AccountResponse"/></returns>
		public async Task<AccountResponse> GetById(string id)
		{
			return await this.m_httpClient.Get<AccountResponse>(ENDPOINT + "/" + id, "/v2");
		}
	}
}
