using Newtonsoft.Json.Linq;

namespace BadgeUp.Types
{
	public class Award
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public JObject Data { get; set; }
		public Meta Meta { get; set; }
	}
}
