using BadgeUp.Responses;
using System.Threading.Tasks;

namespace BadgeUp.ResourceClients
{
	public interface IAccountClient
	{
		Task<AccountResponse> GetById(string id);
	}
}
