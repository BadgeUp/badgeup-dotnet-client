using BadgeUp.Http;
using BadgeUp.ResourceClients;

namespace BadgeUp
{
	/// <summary>
	/// BadgeUp Client
	/// </summary>
	public class BadgeUpClient : System.IDisposable
	{
		private const string DEFAULT_HOST = "https://api.useast1.badgeup.io";
		private string m_host;
		private ApiKey m_apiKey;
		private BadgeUpHttpClient m_httpClient;

		// resource clients


		/// <summary>
		/// Interact with BadgeUp accounts
		/// </summary>
		public IAccountClient Account { get; }

		/// <summary>
		/// Interact with BadgeUp achievements
		/// </summary>
		public IAchievementClient Achievement { get; }
		
		/// <summary>
		/// Interact with BadgeUp achievementIcons
		/// </summary>
		public IAchievementIconClient AchievementIcon { get; }

		/// <summary>
		/// Interact with BadgeUp applications
		/// </summary>
		public IApplicationClient Application { get; }

		/// <summary>
		/// Interact with BadgeUp awards
		/// </summary>
		public IAwardClient Award { get; }

		/// <summary>
		/// Interact with BadgeUp criteria
		/// </summary>
		public ICriterionClient Criterion { get; }

		/// <summary>
		/// Interact with BadgeUp earned achievements
		/// </summary>
		public IEarnedAchievementClient EarnedAchievement { get; }

		/// <summary>
		/// Interact with BadgeUp earned awards
		/// </summary>
		public IEarnedAwardClient EarnedAward { get; }

		/// <summary>
		/// Interact with BadgeUp events
		/// </summary>
		public IEventClient Event { get; }

		/// <summary>
		/// Interact with BadgeUp metrics
		/// </summary>
		public IMetricClient Metric { get; }

		/// <summary>
		/// Interact with BadgeUp progress
		/// </summary>
		public IProgressClient Progress { get; }

		/// <summary>
		/// Instantiate the BadgeUpClient, providing an instance of <see cref="ApiKey"/>
		/// </summary>
		/// <param name="apiKey">API key generated from the BadgeUp dashboard</param>
		/// <param name="host">Optional. BadgeUp instance to use.</param>
		public BadgeUpClient(ApiKey apiKey, string host)
		{
			this.m_apiKey = apiKey;
			this.m_host = host;

			this.m_httpClient = new BadgeUpHttpClient(apiKey, host);

			this.Account = new AccountClient(this.m_httpClient);
			this.Achievement = new AchievementClient(this.m_httpClient);
			this.AchievementIcon = new AchievementIconClient(this.m_httpClient);
			this.Application = new ApplicationClient(this.m_httpClient);
			this.Award = new AwardClient(this.m_httpClient);
			this.Criterion = new CriterionClient(this.m_httpClient);
			this.EarnedAchievement = new EarnedAchievementClient(this.m_httpClient);
			this.EarnedAward = new EarnedAwardClient(this.m_httpClient);
			this.Event = new EventClient(this.m_httpClient);
			this.Metric = new MetricClient(this.m_httpClient);
			this.Progress = new ProgressClient(this.m_httpClient);
		}

		/// <summary>
		/// Instantiate the BadgeUpClient, providing an apiKey
		/// </summary>
		/// <param name="apiKey">API key generated from the BadgeUp dashboard</param>
		/// <param name="host">Optional. BadgeUp instance to use.</param>
		public BadgeUpClient(string apiKey, string host = DEFAULT_HOST)
			: this(ApiKey.Create(apiKey), host)
		{
		}

		// for test purposes only
		internal void SetHttpClient(System.Net.Http.HttpClient h)
		{
			this.m_httpClient.SetHttpClient(h);
		}

		public void Dispose()
		{
			m_httpClient.Dispose();
		}
	}
}
