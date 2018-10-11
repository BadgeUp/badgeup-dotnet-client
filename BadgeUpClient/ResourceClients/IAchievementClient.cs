using BadgeUp.Responses;
using BadgeUp.Types;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BadgeUp.ResourceClients
{
	public interface IAchievementClient
	{
		Task<AchievementResponse> Create(Achievement achievement);
		Task<List<AwardResponse>> GetAchievementAwards(string achievementId);
		Task<List<CriterionResponse>> GetAchievementCriteria(string achievementId);
		Task<List<AchievementResponse>> GetAll();
		Task<AchievementResponse> GetById(string id);
	}
}
