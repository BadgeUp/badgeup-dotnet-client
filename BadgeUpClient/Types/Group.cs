namespace BadgeUpClient.Types
{
	public class Group
	{
		public string Type { get; set; }
		public string Condition { get; set; }
		public Group[] Groups { get; set; }
		public Criteria Criteria { get; set; }
	}
}
