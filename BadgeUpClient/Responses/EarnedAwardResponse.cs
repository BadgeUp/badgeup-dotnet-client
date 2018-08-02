using BadgeUp.Types;

namespace BadgeUp.Responses
{
	public class EarnedAwardResponse : Response
	{
		public string Id { get; set; }
		public string ApplicationId { get; set; }
		public string AwardId { get; set; }
		public string EarnedAchievementId { get; set; }
		public string Subject { get; set; }
		public Meta Meta { get; set; }
	}
}
