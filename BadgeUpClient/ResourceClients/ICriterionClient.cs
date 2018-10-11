using BadgeUp.Responses;
using BadgeUp.Types;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BadgeUp.ResourceClients
{
	public interface ICriterionClient
	{
		Task<CriterionResponse> Create(Criterion criterion);
		Task<List<CriterionResponse>> GetAll();
		Task<CriterionResponse> GetById(string id);
	}
}
