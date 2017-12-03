using BadgeUpClient.Types;

namespace BadgeUpClient.Responses
{
	/// <summary>
	/// BadgeUp Event response.
	/// Provides information about the created event as well as progress towards any relevant achievements.
	/// </summary>
	public class EventResponse : Response
	{
		public Event Event { get; set; }
		public Progress[] Progress { get; set; }
	}
}
