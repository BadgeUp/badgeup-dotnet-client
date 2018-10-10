using System;
using System.Linq;
using BadgeUp.Responses;
using BadgeUp.Types;
using Xunit;

namespace BadgeUp.Tests
{
	public class BasicIntegration
	{
		// get a real API Key for integration testing
		string API_KEY = IntegrationApiKey.Get();

		string RandomSubject() {
			return "dotnet-ci-" + Guid.NewGuid().ToString("N");
		}

		[SkippableFact]
		public async void BasicIntegration_SendEvent()
		{
			if (string.IsNullOrEmpty(API_KEY))
				throw new SkipException("Tests skipped on environments without API_KEY variable configured");

			var client = new BadgeUpClient(API_KEY);
			string subject = RandomSubject();
			string key = "test";
			Event @event = new Event(subject, key, new Modifier { Inc = 5 });

			var result = await client.Event.Send(@event);

			// TODO: Expand this test with code from V1 event tests.

			// sanity check inputs
			Assert.True(result.Results.Count > 0);
			Assert.Equal(key, result.Results[0].Event.Key);
			Assert.Equal(subject, result.Results[0].Event.Subject);
			Assert.Equal(5, result.Results[0].Event.Modifier.Inc);
		}

		[SkippableFact]
		public async void BasicIntegration_GetApplication()
		{
			if (string.IsNullOrEmpty(API_KEY))
				throw new SkipException("Tests skipped on environments without API_KEY variable configured");

			var client = new BadgeUpClient(API_KEY);
			var apiKey = ApiKey.Create(API_KEY);

			var application = await client.Application.GetById(apiKey.ApplicationId);

			Assert.Equal(apiKey.ApplicationId, application.Id);
			Assert.Equal(apiKey.AccountId, application.AccountId);
		}

		[SkippableFact]
		public async void BasicIntegration_GetAccount()
		{
			if (string.IsNullOrEmpty(API_KEY))
				throw new SkipException("Tests skipped on environments without API_KEY variable configured");

			var client = new BadgeUpClient(API_KEY);
			var apiKey = ApiKey.Create(API_KEY);

			var account = await client.Account.GetById(apiKey.AccountId);

			Assert.Equal(apiKey.AccountId, account.Id);
		}

		[SkippableFact]
		public async void BasicIntegration_GetIcons()
		{
			if (string.IsNullOrEmpty(API_KEY))
				throw new SkipException("Tests skipped on environments without API_KEY variable configured");

			var client = new BadgeUpClient(API_KEY);
			var apiKey = ApiKey.Create(API_KEY);

			//some achievement icons have to be uploaded for the test to pass.
			var icons = await client.AchievementIcon.GetAll();
			Assert.False(icons.Length == 0);
		}

		[SkippableFact]
		public async void BasicIntegration_GetAllMetrics()
		{
			if (string.IsNullOrEmpty(API_KEY))
				throw new SkipException("Tests skipped on environments without API_KEY variable configured");

			var client = new BadgeUpClient(API_KEY);

			//there should be more then 50 metrics created, for the default page size of API is 50 elements, and we want to check multiple page retrieval
			var metrics = await client.Metric.GetAll();
			Assert.True(metrics.Count > 50);
		}

		[SkippableFact]
		public async void BasicIntegration_GetAllCriteria()
		{
			if (string.IsNullOrEmpty(API_KEY))
				throw new SkipException("Tests skipped on environments without API_KEY variable configured");

			var client = new BadgeUpClient(API_KEY);

			// TODO: Test multiple page retrieval for criteria
			var criteria = await client.Criterion.GetAll();
			Assert.NotEmpty(criteria);
		}

		[SkippableFact]
		public async void BasicIntegration_GetAllEarnedAchiements()
		{
			if (string.IsNullOrEmpty(API_KEY))
				throw new SkipException("Tests skipped on environments without API_KEY variable configured");

			var client = new BadgeUpClient(API_KEY);

			var achievements = await client.EarnedAchievement.GetAll(new EarnedAchievementQueryParams() { Since = DateTime.UtcNow.AddYears(1) });
			//There should be no achievements made in future
			Assert.Empty(achievements);

			achievements = await client.EarnedAchievement.GetAll(new EarnedAchievementQueryParams() { AchievementId = "foo" });
			//There should be no achievements with invalid AchievementId
			Assert.Empty(achievements);

			achievements = await client.EarnedAchievement.GetAll(new EarnedAchievementQueryParams() { Subject = "bar" });
			//There should be no achievements with invalid subject
			Assert.Empty(achievements);

			achievements = await client.EarnedAchievement.GetAll();
			//There should be some achievements
			Assert.NotEmpty(achievements);
		}

		[SkippableFact]
		public async void BasicIntegration_GetAllEarnedAwards()
		{
			if (string.IsNullOrEmpty(API_KEY))
				throw new SkipException("Tests skipped on environments without API_KEY variable configured");

			var client = new BadgeUpClient(API_KEY);

			var achievements = await client.EarnedAward.GetAll(new EarnedAwardQueryParams() { Since = DateTime.UtcNow.AddYears(1) });
			//There should be no earned awards made in future
			Assert.Empty(achievements);

			achievements = await client.EarnedAward.GetAll(new EarnedAwardQueryParams() { EarnedAchievementId = "foo" });
			//There should be no earned awards with invalid EarnedAchievementId
			Assert.Empty(achievements);

			achievements = await client.EarnedAward.GetAll(new EarnedAwardQueryParams() { Subject = "bar" });
			//There should be no earned awards with invalid subject
			Assert.Empty(achievements);

			achievements = await client.EarnedAward.GetAll();
			//There should be some earned awards
			Assert.NotEmpty(achievements);
		}

		[SkippableFact]
		public async void BasicIntegration_GetAllAwards()
		{
			if (string.IsNullOrEmpty(API_KEY))
				throw new SkipException("Tests skipped on environments without API_KEY variable configured");

			var client = new BadgeUpClient(API_KEY);

			var awards = await client.Award.GetAll();

			//There should be some awards
			Assert.NotEmpty(awards);
		}

		[SkippableFact]
		public async void BasicIntegration_GetAllAChievements()
		{
			if (string.IsNullOrEmpty(API_KEY))
				throw new SkipException("Tests skipped on environments without API_KEY variable configured");

			var client = new BadgeUpClient(API_KEY);

			var achievements = await client.Achievement.GetAll();

			//There should be some achievements
			Assert.NotEmpty(achievements);
		}

		[SkippableFact]
		public async void BasicIntegration_GetProgress_IncludeParameters()
		{
			if (string.IsNullOrEmpty(API_KEY))
				throw new SkipException("Tests skipped on environments without API_KEY variable configured");

			var client = new BadgeUpClient(API_KEY);

			var subject = (await client.Metric.GetAll()).First().Subject;
			var achievements = await client.Achievement.GetAll();
			var progress = await client.Progress.GetProgress(subject);

			Assert.Equal(achievements.Count, progress.Count);
			progress.ForEach(p => Assert.Null(p.Achievement));

			progress = await client.Progress.GetProgress(subject, includeAwards: true);
			progress.ForEach(p => Assert.Null(p.Achievement.Resources.Criteria));
			progress.ForEach(p => Assert.NotNull(p.Achievement.Resources.Awards));

			progress = await client.Progress.GetProgress(subject, includeCriteria: true);
			progress.ForEach(p => Assert.NotNull(p.Achievement.Resources.Criteria));
			progress.ForEach(p => Assert.Null(p.Achievement.Resources.Awards));

			progress = await client.Progress.GetProgress(subject, includeAchievements: true);
			progress.ForEach(p => Assert.NotNull(p.Achievement));
			progress.ForEach(p => Assert.Null(p.Achievement.Resources.Criteria));
			progress.ForEach(p => Assert.Null(p.Achievement.Resources.Awards));
		}

		[SkippableFact]
		public async void BasicIntegration_GetEarnedAwardById()
		{
			if (string.IsNullOrEmpty(API_KEY))
				throw new SkipException("Tests skipped on environments without API_KEY variable configured");

			var client = new BadgeUpClient(API_KEY);

			var earnedAwards = await client.EarnedAward.GetAll();

			// There should be some earned awards
			Assert.NotEmpty(earnedAwards);

			// Use the first earned award and look it up by id as a second request
			var firstEarnedAward = earnedAwards.First();
			var earnedAwardId = firstEarnedAward.Id;
			var earnedAward = await client.EarnedAward.GetById(earnedAwardId);

			// The same earned award should be returned
			Assert.NotNull(earnedAward);

			// Verify that all the fields of both awards are the same.
			Assert.Equal(firstEarnedAward.AchievementId, earnedAward.AchievementId);
			Assert.Equal(firstEarnedAward.ApplicationId, earnedAward.ApplicationId);
			Assert.Equal(firstEarnedAward.AwardId, earnedAward.AwardId);
			Assert.Equal(firstEarnedAward.EarnedAchievementId, earnedAward.EarnedAchievementId);
			Assert.Equal(firstEarnedAward.Id, earnedAward.Id);
			Assert.Equal(firstEarnedAward.Meta.Created, earnedAward.Meta.Created);
			Assert.Equal(firstEarnedAward.State, earnedAward.State);
			Assert.Equal(firstEarnedAward.Subject, earnedAward.Subject);
		}
	}
}
