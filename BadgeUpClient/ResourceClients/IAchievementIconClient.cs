using System.Threading.Tasks;
using BadgeUp.Responses;

namespace BadgeUp.ResourceClients
{
	public interface IAchievementIconClient
	{
		Task<AchievementIconResponse[]> GetAll();
	}
}