using System;
using System.Reflection;

namespace BadgeUp.Types
{
	public class EarnedAwardQueryParams : QueryParams
	{

		public string Subject { get; set; }
		public string AwardId { get; set; }
		public string EarnedAchievementId { get; set; }
		public DateTime? Since { get; set; }
		public DateTime? Until { get; set; }
	}
}
