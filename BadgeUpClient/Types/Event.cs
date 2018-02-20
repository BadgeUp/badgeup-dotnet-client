using System;

namespace BadgeUp.Types
{
	/// <summary>
	/// BadgeUp Event object.
	/// Contains information about the key, subject, and metric modifier
	/// </summary>
	public class Event
	{
		public string Id { get; set; }
		public string ApplicationId { get; set; }
		public string Subject { get; set; }
		public string Key { get; set; }
		public DateTimeOffset? Timestamp { get; set; }
		public string Data { get; set; }

		public Modifier Modifier { get; set; }
		public Options Options { get; set; }

		/// <summary>
		/// Create a new Event
		/// </summary>
		/// <param name="subject">The subject or end-user you are creating this event for</param>
		/// <param name="key">The metric that this event will modify. This key will trigger matching criteria and associated achievements to be evaluated.</param>
		/// <param name="modifier">How the metric should be modified. This will usually be increment (Inc) by some value</param>
		/// <param name="options">Options that affect the state and operability of this event.</param>
		public Event( string subject, string key, Modifier modifier = null, Options options = null )
		{
			this.Subject = subject;
			this.Key = key;

			this.Modifier = modifier ?? new Modifier { Inc = 1 };
			this.Options = options;
		}
	}
}
