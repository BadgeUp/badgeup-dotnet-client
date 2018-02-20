using System.Collections.Generic;
using BadgeUp.Types;

namespace BadgeUp.Responses
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
		/// <summary>
		/// Created event object
		/// </summary>
		public Event Event { get; set; }
		/// <summary>
		/// ID of the event that caused this event, otherwise null
		/// </summary>
		public string Cause { get; set; }
		/// <summary>
		/// Current state of completion for any achievements were affected by the event 
		/// </summary>
		public Progress[] Progress { get; set; }
	}
}
