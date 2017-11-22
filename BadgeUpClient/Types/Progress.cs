namespace BadgeUpClient.Types
{
	public class Progress
	{
		public bool IsComplete { get; set; }
		public int PercentComplete { get; set; }
		public Group ProgressTree { get; set; }
		public string AchievementId { get; set; }
		public string EarnedAchievementId { get; set; }
		public bool IsNew { get; set; }
	}
}
