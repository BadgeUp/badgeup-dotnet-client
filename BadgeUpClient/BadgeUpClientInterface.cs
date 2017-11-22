using System.Threading.Tasks;
using BadgeUpClient.Responses;
using BadgeUpClient.Types;

namespace BadgeUpClient
{
	public abstract class BadgeUpClientInterface
	{
		public abstract Task<EventResponse> SendEvent( Event @event, bool? showIncomplete = null, bool? discard = null );
	}
}
