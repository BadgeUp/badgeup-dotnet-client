using BadgeUp.Responses;
using BadgeUp.Types;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BadgeUp.ResourceClients
{
	public interface IEarnedAchievementClient
	{
		Task<List<EarnedAchievementResponse>> GetAll(EarnedAchievementQueryParams param = null);
		Task<EarnedAchievementResponse> GetById(string id);
	}
}
