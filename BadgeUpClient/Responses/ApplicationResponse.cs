using BadgeUp.Types;

namespace BadgeUp.Responses
{
	public class ApplicationResponse : Response
	{
		public string Id { get; set; }
		public string AccountId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public ApplicationMeta Meta { get; set; }
	}
}
