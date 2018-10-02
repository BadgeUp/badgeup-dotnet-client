using System.Threading.Tasks;
using BadgeUp.Types;
using Xunit;

namespace BadgeUp.Tests
{
	public class EarnedAwardIntegration
	{
		private readonly string API_KEY = IntegrationApiKey.Get();

		[SkippableFact]
		public async Task EarnedAwardIntegration_ChangeState()
		{
			if (string.IsNullOrEmpty(this.API_KEY))
				throw new SkipException("Tests skipped on environments without API_KEY variable configured");

			var client = new BadgeUpClient(this.API_KEY);

			// Get all earned awards
			var earnedAwards = await client.EarnedAward.GetAll();
			Assert.NotEmpty(earnedAwards);

			var award = earnedAwards[0];
			var result = await client.EarnedAward.ChangeState(award.Id, EarnedAwardState.Redeemed);

			// Verify the result
			Assert.Null(result);
		}
	}
}
