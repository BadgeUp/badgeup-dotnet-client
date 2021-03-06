using System;
using System.Reflection;

namespace BadgeUp.Types
{
	public class EarnedAchievementQueryParams : QueryParams
	{

		public string Subject { get; set; }
		public string AchievementId { get; set; }
		public DateTime? Since { get; set; }
		public DateTime? Until { get; set; }
	}
}
