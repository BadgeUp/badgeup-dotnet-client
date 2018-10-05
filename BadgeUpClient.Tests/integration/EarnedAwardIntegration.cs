using System.Linq;
using System.Threading.Tasks;
using BadgeUp.Types;
using Xunit;

namespace BadgeUp.Tests
{
	public class EarnedAwardIntegration
	{
		private readonly string API_KEY = IntegrationApiKey.Get();

		[SkippableFact]
		public async Task EarnedAwardIntegration_ChangeStateFromCreatedToRedeemed()
		{
			if (string.IsNullOrEmpty(this.API_KEY))
				throw new SkipException("Tests skipped on environments without API_KEY variable configured");

			var client = new BadgeUpClient(this.API_KEY);

			// Get all earned awards
			var earnedAwards = await client.EarnedAward.GetAll();
			Assert.NotEmpty(earnedAwards);

			var award = earnedAwards.FirstOrDefault(ea => ea.State == EarnedAwardState.Created);
			Assert.NotNull(award);

			var result = await client.EarnedAward.ChangeState(award.Id, EarnedAwardState.Redeemed);

			// Verify the result
			Assert.Equal(award.Id, result.Id);
			Assert.Equal(award.AchievementId, result.AchievementId);
			Assert.Equal(award.ApplicationId, result.ApplicationId);
			Assert.Equal(award.AwardId, result.AwardId);
			Assert.Equal(award.EarnedAchievementId, result.EarnedAchievementId);
			Assert.Equal(award.Meta.Created, result.Meta.Created);
			Assert.Equal(EarnedAwardState.Redeemed, result.State);
			Assert.Equal(award.Subject, result.Subject);
		}
	}
}
