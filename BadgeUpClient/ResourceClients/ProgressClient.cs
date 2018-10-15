using System.Collections.Generic;
using System.Threading.Tasks;
using BadgeUp.Http;
using BadgeUp.Types;
using BadgeUp.Responses;

namespace BadgeUp.ResourceClients
{
	internal class ProgressClient : IProgressClient
	{
		const string ENDPOINT = "progress";
		protected BadgeUpHttpClient m_httpClient;

		internal ProgressClient(BadgeUpHttpClient httpClient)
		{
			this.m_httpClient = httpClient;
		}

		/// <summary>
		/// Retrieves a list of achievements and the completion a subject has towards the achievements. The returned list will also contain earned achievement records when present.
		/// </summary>
		/// <param name="subjectId">Unique subject the progress is associated with</param>
		/// <param name="includeAchievements">Include related achievements</param>
		/// <param name="includeCriteria">Include related criteria</param>
		/// <param name="includeAwards">Include related awards</param>
		/// <returns></returns>
		public Task<List<Progress>> GetProgress(string subjectId, bool includeAchievements = false, bool includeCriteria = false, bool includeAwards = false)
		{
			var query = new HttpQuery();
			query.Add("subject", subjectId);

			//Including criteria or awards requires also including achievements
			if (includeAwards || includeCriteria)
				includeAchievements = true;
			if(includeAchievements)
				query.Add("include", "achievement");
			if (includeCriteria)
				query.Add("include", "criterion");
			if (includeAwards)
				query.Add("include", "award");
			return this.m_httpClient.GetAll<Progress>(ENDPOINT, query:query.ToString());
		}
	}
}
