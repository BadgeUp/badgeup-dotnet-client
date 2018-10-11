using BadgeUp.Responses;
using BadgeUp.Types;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BadgeUp.ResourceClients
{
	public interface IEarnedAwardClient
	{
		Task<EarnedAwardResponse> ChangeState(string id, EarnedAwardState state);
		Task<List<EarnedAwardResponse>> GetAll(EarnedAwardQueryParams param = null);
		Task<EarnedAwardResponse> GetById(string id);
	}
}
