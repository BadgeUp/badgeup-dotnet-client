using BadgeUp.Types;
using System;
using System.Linq;
using System.Threading.Tasks;
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

		[SkippableFact]
		public async Task AchievementsIntegration_GetAchievementCriteria()
		{
			if (string.IsNullOrEmpty(this.API_KEY))
				throw new SkipException("Tests skipped on environments without API_KEY variable configured");

			var client = new BadgeUpClient(this.API_KEY);

			// Verify there are some achievements
			var achievements = await client.Achievement.GetAll();
			Assert.NotEmpty(achievements);

			// Get the criteria for the first achievement
			var achievementCriteria = await client.Achievement.GetAchievementCriteria(achievements.First().Id);
			Assert.NotNull(achievementCriteria);
			Assert.NotEmpty(achievementCriteria);

			// Verify that each criterion retrieved from .GetAchievementCriteria matches the data from /criteria/:id.
			// Limit to first 10 so that we don't run test for too long.
			foreach (var criterion in achievementCriteria.Take(10))
			{
				var result = await client.Criterion.GetById(criterion.Id);
				Assert.NotSame(result, criterion);
				Assert.Equal(result.Id, criterion.Id);
				Assert.Equal(result.ApplicationId, criterion.ApplicationId);
				Assert.Equal(result.Description, criterion.Description);
				Assert.Equal(result.Key, criterion.Key);
				Assert.Equal(result.Name, criterion.Name);
				Assert.Equal(result.Evaluation.Multiplicity?.Consecutive, criterion.Evaluation.Multiplicity?.Consecutive);
				Assert.Equal(result.Evaluation.Multiplicity?.Lookback, criterion.Evaluation.Multiplicity?.Lookback);
				Assert.Equal(result.Evaluation.Multiplicity?.Periods, criterion.Evaluation.Multiplicity?.Periods);
				Assert.Equal(result.Evaluation.RepeatOptions?.CarryOver, criterion.Evaluation.RepeatOptions?.CarryOver);
				Assert.Equal(result.Evaluation.Threshold, criterion.Evaluation.Threshold);
				Assert.Equal(result.Evaluation.Type, criterion.Evaluation.Type);
				Assert.Equal(result.Evaluation.Operator, criterion.Evaluation.Operator);
				Assert.Equal(result.Meta.Created, criterion.Meta.Created);
			}
		}

		[SkippableFact]
		public async Task AchievementsIntegration_GetAchievementAwards()
		{
			if (string.IsNullOrEmpty(this.API_KEY))
				throw new SkipException("Tests skipped on environments without API_KEY variable configured");

			var client = new BadgeUpClient(this.API_KEY);

			// Verify there are some achievements
			var achievements = await client.Achievement.GetAll();
			Assert.NotEmpty(achievements);

			// Get the awards for the first achievement
			var achievementAwards = await client.Achievement.GetAchievementAwards(achievements.First().Id);
			Assert.NotNull(achievementAwards);
			Assert.NotEmpty(achievementAwards);

			// Verify that each award retrieved from .GetAchievementAwards matches the data from /awards/:id.
			// Limit to first 10 so that we don't run test for too long.
			foreach (var award in achievementAwards.Take(10))
			{
				var result = await client.Award.GetById(award.Id);
				Assert.NotSame(result, award);
				Assert.Equal(result.Id, award.Id);
				Assert.Equal(result.ApplicationId, award.ApplicationId);
				Assert.Equal(result.Description, award.Description);
				Assert.Equal(result.Data.ToString(), award.Data.ToString());
				Assert.Equal(result.Name, award.Name);
				Assert.Equal(result.Meta.Created, award.Meta.Created);
			}
		}
	}
}
