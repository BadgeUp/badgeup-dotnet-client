using BadgeUp.Responses;
using BadgeUp.Types;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BadgeUp.ResourceClients
{
	public interface IAwardClient
	{
		Task<AwardResponse> Create(Award award);
		Task<List<AwardResponse>> GetAll();
		Task<AwardResponse> GetById(string id);
	}
}
