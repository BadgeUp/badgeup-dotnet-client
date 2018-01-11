using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BadgeUpClient.Http;
using BadgeUpClient.Responses;

namespace BadgeUpClient.ResourceClients
{
	public class EarnedAchievementClient
	{
		const string ENDPOINT = "earnedachievements";
		protected BadgeUpHttpClient m_httpClient;
		private QueryParams _params;

		public EarnedAchievementClient(BadgeUpHttpClient httpClient)
		{
			this.m_httpClient = httpClient;
		}

		/// <summary>
		/// Retrieves an achievement by ID
		/// </summary>
		/// <param name="id">A string that uniquely identifies this achievement</param>
		/// <returns><see cref="EarnedAchievementResponse"/></returns>
		public async Task<EarnedAchievementResponse> GetById(string id)
		{
			return await this.m_httpClient.Get<EarnedAchievementResponse>(ENDPOINT + "/" + id);
		}

		private Task<List<EarnedAchievementResponse>> GetAll(bool useQueryParams = false)
		{
			throw new NotImplementedException();
		}

		public IQueryParams Query()
		{
			return this._params = new QueryParams(this);
		}

		private class QueryParams : IQueryParams
		{
			private readonly EarnedAchievementClient _client;

			public QueryParams(EarnedAchievementClient client)
			{
				_client = client;
			}

			public string Subject { get; set; }
			public string AchievementId { get; set; }
			public DateTimeOffset Since { get; set; }
			public DateTimeOffset Until { get; set; }

			IQueryParams IQueryParams.Subject(string subject)
			{
				Subject = subject;
				return this;
			}

			IQueryParams IQueryParams.AchievementId(string achievementId)
			{
				AchievementId = achievementId;
				return this;
			}

			IQueryParams IQueryParams.Since(DateTimeOffset since)
			{
				Since = since;
				return this;
			}

			IQueryParams IQueryParams.Until(DateTimeOffset until)
			{
				Until = until;
				return this;
			}

			public Task<List<EarnedAchievementResponse>> GetAll()
			{
				return _client.GetAll(true);
			}
		}

		public interface IQueryParams
		{
			IQueryParams Subject(string subject);
			IQueryParams AchievementId(string achievementId);
			IQueryParams Since(DateTimeOffset since);
			IQueryParams Until(DateTimeOffset until);
			Task<List<EarnedAchievementResponse>> GetAll();
		}
	}
}

//bup.EarnedAchievements.Query().Subject("bob").AchievementId("a1b2c3").GetAll()
