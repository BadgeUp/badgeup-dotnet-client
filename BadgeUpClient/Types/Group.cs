using System.Collections.Generic;

namespace BadgeUpClient.Types
{
	public class Group
	{
		public string Type = "GROUP";
		public string Condition { get; set; }
		public Group[] Groups { get; set; }
		public Dictionary<string, float> Criteria { get; set; }
	}
}
