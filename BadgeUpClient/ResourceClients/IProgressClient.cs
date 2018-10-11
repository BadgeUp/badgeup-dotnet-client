using System.Collections.Generic;
using System.Threading.Tasks;
using BadgeUp.Types;

namespace BadgeUp.ResourceClients
{
	public interface IProgressClient
	{
		Task<List<Progress>> GetProgress(string subjectId, bool includeAchievements = false, bool includeCriteria = false, bool includeAwards = false);
	}
}
