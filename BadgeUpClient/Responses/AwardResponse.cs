using BadgeUp.Types;

namespace BadgeUp.Responses
{
	public class AwardResponse : Response
	{
		public string Id { get; set; }
		public string ApplicationId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public Newtonsoft.Json.Linq.JObject Data { get; set; }
		public Meta Meta { get; set; }
	}
}
