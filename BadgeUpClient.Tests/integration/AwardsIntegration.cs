using BadgeUp.Types;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BadgeUp.Tests
{
	public class AwardsIntegration
	{
		private readonly string API_KEY = IntegrationApiKey.Get();

		[SkippableFact]
		public async Task AwardsIntegration_GetAll()
		{
			if (string.IsNullOrEmpty(this.API_KEY))
				throw new SkipException("Tests skipped on environments without API_KEY variable configured");

			var client = new BadgeUpClient(this.API_KEY);

			var awards = await client.Award.GetAll();
			Assert.NotEmpty(awards);
		}

		[SkippableFact]
		public async Task AwardsIntegration_GetById()
		{
			if (string.IsNullOrEmpty(this.API_KEY))
				throw new SkipException("Tests skipped on environments without API_KEY variable configured");

			var client = new BadgeUpClient(this.API_KEY);

			// Verify we have any awards.
			var allAwards = await client.Award.GetAll();
			Assert.NotEmpty(allAwards);

			// Get the first award.
			var firstAward = allAwards.First();
			var result = await client.Award.GetById(firstAward.Id);

			// Search for the first award by Id and verify it's equal as the one returned from .GetAll().
			Assert.NotNull(result);
			Assert.NotSame(firstAward, result);
			Assert.Equal(firstAward.Id, result.Id);
			Assert.Equal(firstAward.Name, result.Name);
			Assert.Equal(firstAward.Description, result.Description);
			Assert.Equal(firstAward.Meta.Created, result.Meta.Created);
		}

		[SkippableFact]
		public async Task AwardsIntegration_Create()
		{
			if (string.IsNullOrEmpty(this.API_KEY))
				throw new SkipException("Tests skipped on environments without API_KEY variable configured");

			var client = new BadgeUpClient(this.API_KEY);

			// Create a new award
			var result = await client.Award.Create(new Award()
			{
				Name = "Test Award",
				Description = "Test Award Description",
				Data = JObject.Parse(@"{
					'test-number' : 1.5,
					'test-string' : 'test'
				}".Replace("'", "\""))
			});

			// Verify the award has been created with the same parameters
			Assert.NotNull(result);
			Assert.NotNull(result.Id);
			Assert.NotNull(result.Meta?.Created);

			// Verify we can get the award by id.
			var award = await client.Award.GetById(result.Id);
			Assert.NotNull(award);
			Assert.Equal(result.Id, award.Id);
			Assert.Equal(result.ApplicationId, award.ApplicationId);
			Assert.Equal("Test Award", award.Name);
			Assert.Equal("Test Award Description", award.Description);
			Assert.Equal(1.5m, award.Data["test-number"].Value<decimal>());
			Assert.Equal("test", award.Data["test-string"].Value<string>());

			// TODO: Delete the award
			// TODO: Verify deletion has completed successfully.
		}
	}
}
