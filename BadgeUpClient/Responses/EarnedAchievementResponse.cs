using BadgeUpClient.Types;

namespace BadgeUpClient.Responses
{
	public class EarnedAchievementResponse : Response
	{
		public string Id { get; set; }
		public string ApplicationId { get; set; }
		public string AchievementId { get; set; }
		public string Subject { get; set; }
		public Meta Meta { get; set; }
	}
}
