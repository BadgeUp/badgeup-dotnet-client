using System.Linq;
using System.Threading.Tasks;
using BadgeUp.Types;
using Xunit;

namespace BadgeUp.Tests
{
	public class CriteriaIntegration
	{
		private readonly string API_KEY = IntegrationApiKey.Get();

		[SkippableFact]
		public async Task CriteriaIntegration_GetAll()
		{
			if (string.IsNullOrEmpty(this.API_KEY))
				throw new SkipException("Tests skipped on environments without API_KEY variable configured");

			var client = new BadgeUpClient(this.API_KEY);

			var criteria = await client.Criterion.GetAll();
			Assert.NotEmpty(criteria);
		}

		[SkippableFact]
		public async Task CriteriaIntegration_GetById()
		{
			if (string.IsNullOrEmpty(this.API_KEY))
				throw new SkipException("Tests skipped on environments without API_KEY variable configured");

			var client = new BadgeUpClient(this.API_KEY);

			// Verify we have any criteria.
			var allCriteria = await client.Criterion.GetAll();
			Assert.NotEmpty(allCriteria);

			// Get the first criterion.
			var firstCriterion = allCriteria.First();
			var firstCriterionId = firstCriterion.Id;
			var result = await client.Criterion.GetById(firstCriterionId);

			// Search for the first criterion by Id and verify it's equal as the one returned from .GetAll().
			Assert.NotNull(result);
			Assert.NotSame(firstCriterion, result);
			Assert.Equal(firstCriterionId, result.Id);
			Assert.Equal(firstCriterion.ApplicationId, result.ApplicationId);
			Assert.Equal(firstCriterion.Key, result.Key);
			Assert.Equal(firstCriterion.Name, result.Name);
			Assert.Equal(firstCriterion.Description, result.Description);
			Assert.Equal(firstCriterion.Meta.Created, result.Meta.Created);
		}

		[SkippableFact]
		public async Task CriteriaIntegration_CreateStandardCriterion()
		{
			if (string.IsNullOrEmpty(this.API_KEY))
				throw new SkipException("Tests skipped on environments without API_KEY variable configured");

			var client = new BadgeUpClient(this.API_KEY);

			// Create a new criterion
			var result = await client.Criterion.Create(new Criterion()
			{
				Name = "Test Criterion",
				Description = "Test Criterion Description",
				Key = "a:criterion",
				Evaluation = new CriterionEvaluation()
				{
					Type = CriterionEvaluationType.Standard,
					Operator = CriterionOperator.LessOrEqual,
					Threshold = 10,
					RepeatOptions = new CriterionRepeatOptions
					{
						CarryOver = true
					},
					Period = null,// Period is only used for Timeseries type
					Multiplicity = null, // Multiplicity is only used for Timeseries type
				}
			});

			// Verify the criterion has been created with the same parameters
			Assert.NotNull(result);
			Assert.Equal("Test Criterion", result.Name);
			Assert.Equal("Test Criterion Description", result.Description);
			Assert.Equal("a:criterion", result.Key);
			Assert.Equal(CriterionEvaluationType.Standard, result.Evaluation.Type);
			Assert.Equal(CriterionOperator.LessOrEqual, result.Evaluation.Operator);
			Assert.Equal(10, result.Evaluation.Threshold);
			Assert.True(result.Evaluation.RepeatOptions.CarryOver);
			Assert.Null(result.Evaluation.Period);
			Assert.Null(result.Evaluation.Multiplicity);
			Assert.NotNull(result.Meta.Created);

			// Verify we can get the criterion by id.
			var criterion = await client.Criterion.GetById(result.Id);
			Assert.NotNull(criterion);

			// TODO: Delete the criterion
			// TODO: Verify deletion has completed successfully.
		}

		[SkippableFact]
		public async Task CriteriaIntegration_CreateTimeseriesCriterion()
		{
			if (string.IsNullOrEmpty(this.API_KEY))
				throw new SkipException("Tests skipped on environments without API_KEY variable configured");

			var client = new BadgeUpClient(this.API_KEY);

			// Create a new criterion
			var result = await client.Criterion.Create(new Criterion()
			{
				Name = "Test Criterion",
				Description = "Test Criterion Description",
				Key = "a:criterion",
				Evaluation = new CriterionEvaluation()
				{
					Type = CriterionEvaluationType.Timeseries,
					Operator = CriterionOperator.Greater,
					Threshold = 15,
					RepeatOptions = null, // Repeat Options are only used for Standard types
					Period = new CriterionEvaluationPeriod()
					{
						Value = 2,
						Unit = CriterionTimeseriesPeriodUnits.Days
					},
					Multiplicity = new CriterionEvaluationMultiplicity()
					{
						Consecutive = false,
						Lookback = 5,
						Periods = 3
					}
				}
			});

			// Verify the criterion has been created with the same parameters
			Assert.NotNull(result);
			Assert.Equal("Test Criterion", result.Name);
			Assert.Equal("Test Criterion Description", result.Description);
			Assert.Equal("a:criterion", result.Key);
			Assert.Equal(CriterionEvaluationType.Timeseries, result.Evaluation.Type);
			Assert.Equal(CriterionOperator.Greater, result.Evaluation.Operator);
			Assert.Equal(15, result.Evaluation.Threshold);
			Assert.Null(result.Evaluation.RepeatOptions?.CarryOver);
			Assert.Equal(2, result.Evaluation.Period.Value);
			Assert.Equal(CriterionTimeseriesPeriodUnits.Days, result.Evaluation.Period.Unit);
			Assert.False(result.Evaluation.Multiplicity.Consecutive);
			Assert.Equal(5, result.Evaluation.Multiplicity.Lookback);
			Assert.Equal(3, result.Evaluation.Multiplicity.Periods);
			Assert.NotNull(result.Meta.Created);

			// Verify we can get the criterion by id.
			var criterion = await client.Criterion.GetById(result.Id);
			Assert.NotNull(criterion);

			// TODO: Delete the criterion
			// TODO: Verify deletion has completed successfully.
		}
	}
}
