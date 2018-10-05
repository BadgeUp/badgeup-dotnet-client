using System;
using System.Linq;
using System.Threading.Tasks;
using BadgeUp.Types;
using Xunit;

namespace BadgeUp.Tests
{
	public class AchievementsIntegration
	{
		private readonly string API_KEY = IntegrationApiKey.Get();

		private string RandomSubject()
		{
			return "dotnet-ci-" + Guid.NewGuid().ToString("N");
		}

		[SkippableFact]
		public async Task AchievementsIntegration_Create()
		{
			if (string.IsNullOrEmpty(this.API_KEY))
				throw new SkipException("Tests skipped on environments without API_KEY variable configured");

			var client = new BadgeUpClient(this.API_KEY);

			// Get existing awards. We need some awards for this integration test.
			var awards = await client.Award.GetAll();
			Assert.NotNull(awards);
			Assert.NotEmpty(awards);

			// Create a new achievement
			var result = await client.Achievement.Create(new Achievement()
			{
				Name = "Test Achievement",
				Description = "Test Description",
				Awards = awards.Select(a => a.Id).ToArray(),
				// TODO: Add simple EvalTree test once client.Criterion.GetAll() is completed
				EvalTree = null,
				Options = new AchievementOptions()
				{
					EarnLimit = -1,
					Suspended = true
				},
				Meta = new AchievementMeta()
				{
					Icon = "https://example.com/favicon.ico"
				}
			});

			// Verify the achievement has been created with the same parameters
			Assert.NotNull(result);
			Assert.False(string.IsNullOrEmpty(result.Id));
			Assert.Equal("Test Achievement", result.Name);
			Assert.Equal("Test Description", result.Description);
			Assert.Equal(awards.Select(a => a.Id), result.Awards);
			Assert.True(result.Options.Suspended);
			Assert.Equal(-1, result.Options.EarnLimit);
			Assert.NotNull(result.Meta.Created);
			Assert.Equal("https://example.com/favicon.ico", result.Meta.Icon);

			// Verify we can get the achievement by id.
			var achievement = await client.Achievement.GetById(result.Id);
			Assert.NotNull(achievement);

			// TODO: Delete the achievement
			// TODO: Verify deletion has completed successfully.
		}
	}
}
