namespace BadgeUpClient.Types
{
	public class Event
	{
		public string Id { get; set; }
		public string ApplicationId { get; set; }
		public string Subject { get; set; }
		public string Key { get; set; }
		public string Timestamp { get; set; }
		public string Data { get; set; }

		public Modifier Modifier { get; set; }
		public Options Options { get; set; }

		public Event( string subject, string key, Modifier modifier = null, Options options = null )
		{
			this.Subject = subject;
			this.Key = key;

			this.Modifier = modifier ?? new Modifier();
			this.Options = options;
		}
	}
}
