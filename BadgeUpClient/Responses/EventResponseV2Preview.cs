using System.Collections.Generic;
using BadgeUpClient.Types;

namespace BadgeUpClient.Responses
{
	/// <summary>
	/// BadgeUp Event V2 preview response.
	/// Provides information about the created event as well as progress towards any relevant achievements.
	/// </summary>
	public class EventResponseV2Preview : Response
	{
		public List<EventResponseResultV2Preview> Results { get; set; }
	}

	public class EventResponseResultV2Preview
	{
		public Event Event { get; set; }
		public string Cause { get; set; }
		public Progress[] Progress { get; set; }
	}
}
