using System.Collections.Generic;
using System.Threading.Tasks;
using BadgeUpClient.Http;
using BadgeUpClient.Responses;
using BadgeUpClient.Types;

namespace BadgeUpClient.ResourceClients
{
	public class ProgressClient
	{
		const string ENDPOINT = "progress";
		protected BadgeUpHttpClient m_httpClient;

		public ProgressClient(BadgeUpHttpClient httpClient)
		{
			this.m_httpClient = httpClient;
		}

		/// <summary>
		/// Retrieves a list of achievements and the completion a subject has towards the achievements. The returned list will also contain earned achievement records when present.
		/// </summary>
		/// <param name="subjectId">Unique subject the progress is associated with</param>
		/// <returns></returns>
		public async Task<List<Progress>> GetProgress(string subjectId)
		{
			var query = new HttpQuery();
			query.Add("subject", subjectId);
			return await this.m_httpClient.GetAll<Progress>(ENDPOINT, query:query.ToString());
		}
	}
}
