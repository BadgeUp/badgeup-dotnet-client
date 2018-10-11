using BadgeUp.Responses;
using BadgeUp.Types;
using System.Threading.Tasks;

namespace BadgeUp.ResourceClients
{
	public interface IEventClient
	{
		Task<EventResponse> Send(Event @event, bool? showIncomplete = null, bool onlyNew = false);
	}
}
