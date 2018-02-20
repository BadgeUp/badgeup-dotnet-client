using BadgeUp.Types;

namespace BadgeUp.Responses
{
	public class AchievementResponse : Response
	{
		public string Id { get; set; }
		public string ApplicationId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public AchievementGroup EvalTree { get; set; }
		public string[] Awards { get; set; }
		public AchievementMeta Meta { get; set; }
		public AchievementOptions Options { get; set; }
	}

	public class AchievementMeta : Meta
	{
		public string Icon { get; set; }
	}

	public class AchievementOptions
	{
		public bool Suspended { get; set; } = false;
	}
}
