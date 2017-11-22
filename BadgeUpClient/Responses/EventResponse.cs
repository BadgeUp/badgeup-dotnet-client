using BadgeUpClient.Types;

namespace BadgeUpClient.Responses
{
	public class EventResponse : Response
	{
		public Event Event { get; set; }
		public Progress[] Progress { get; set; }
	}
}
