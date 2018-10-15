using BadgeUp.Http;
using BadgeUp.Requests;
using BadgeUp.Responses;
using BadgeUp.Types;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BadgeUp.ResourceClients
{
	internal class AwardClient : IAwardClient
	{
		private const string ENDPOINT = "awards";
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
		public Task<AwardResponse> GetById(string id)
		{
			return this.m_httpClient.Get<AwardResponse>(ENDPOINT + "/" + id);
		}

		/// <summary>
		/// Retrieves a list of all awards.
		/// </summary>
		/// <returns></returns>
		public Task<List<AwardResponse>> GetAll()
		{
			return this.m_httpClient.GetAll<AwardResponse>(ENDPOINT);
		}

		/// <summary>
		/// Retrieves a list of all awards.
		/// </summary>
		/// <returns></returns>
		public Task<AwardResponse> Create(Award award)
		{
			if (award == null)
			{
				throw new ArgumentNullException(nameof(award));
			}

			var request = new AwardRequest(award);
			return this.m_httpClient.Post<AwardResponse>(request, ENDPOINT);
		}
	}
}
