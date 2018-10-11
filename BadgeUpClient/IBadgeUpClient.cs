using BadgeUp.ResourceClients;
using System;

namespace BadgeUp
{
	public interface IBadgeUpClient
	{
		IAccountClient Account { get; }
		IAchievementClient Achievement { get; }
		IAchievementIconClient AchievementIcon { get; }
		IApplicationClient Application { get; }
		IAwardClient Award { get; }
		ICriterionClient Criterion { get; }
		IEarnedAchievementClient EarnedAchievement { get; }
		IEarnedAwardClient EarnedAward { get; }
		IEventClient Event { get; }
		IMetricClient Metric { get; }
		IProgressClient Progress { get; }
	}
}
