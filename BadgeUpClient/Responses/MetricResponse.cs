namespace BadgeUp.Responses
{
	public class MetricResponse : Response
	{
		public string Id { get; set; }
		public string ApplicationId { get; set; }
		public string Key { get; set; }
		public string Subject { get; set; }
		public int Value { get; set; }
	}
}
