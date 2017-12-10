using BadgeUpClient.Types;

namespace BadgeUpClient.Responses
{
	public class AccountResponse : Response
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public AccountMeta Meta { get; set; }
	}

	public class AccountMeta : Meta
	{
	}

}
