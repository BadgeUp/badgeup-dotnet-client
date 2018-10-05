namespace BadgeUp.Types
{
	public class AchievementOptions
	{
		/// <summary>
		/// Defines the number of times the achievement can be earned. Defaults to 1 (can only be earned once). A value of -1 means there is no repetition limit.
		/// </summary>
		public decimal? EarnLimit { get; set; } = 1;

		/// <summary>
		/// Whether this achievement will be awarded to players.
		/// </summary>
		public bool Suspended { get; set; } = false;
	}
}
