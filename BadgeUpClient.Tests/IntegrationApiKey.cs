namespace BadgeUp.Tests
{
	/// <summary>
	/// Helper class for exposing a single point of access for the integration API key.
	/// </summary>
	internal static class IntegrationApiKey
	{
		public static string Get()
		{
			return System.Environment.GetEnvironmentVariable("INTEGRATION_API_KEY");
		}
	}
}
