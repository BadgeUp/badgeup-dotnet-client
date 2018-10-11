using BadgeUp.Responses;
using System.Threading.Tasks;

namespace BadgeUp.ResourceClients
{
	public interface IApplicationClient
	{
		Task<ApplicationResponse> GetById(string id);
	}
}
