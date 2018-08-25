namespace BadgeUp.Types
{
	public class Achievement
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public string[] Awards { get; set; }
		public AchievementGroup EvalTree { get; set; }
		public AchievementOptions Options { get; set; }
		public AchievementMeta Meta { get; set; }
	}
}
