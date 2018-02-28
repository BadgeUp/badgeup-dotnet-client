using BadgeUp.Responses;

namespace BadgeUp.Types
{
	public class Progress
	{
		public bool IsComplete { get; set; }
		public float PercentComplete { get; set; }
		public ProgressGroup ProgressTree { get; set; }
		public string AchievementId { get; set; }
		public AchievementResponse Achievement { get; set; }
		public string EarnedAchievementId { get; set; }
		public bool IsNew { get; set; }
	}
}
