using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BadgeUp.Tests;
using BadgeUp.Types;
using Xunit;

namespace BadgeUp.Tests
{
	public class MetricsIntegration
	{
		private readonly string API_KEY = IntegrationApiKey.Get();

		[SkippableFact]
		public async Task MetricsIntegration_GetAll()
		{
			if (string.IsNullOrEmpty(this.API_KEY))
				throw new SkipException("Tests skipped on environments without API_KEY variable configured");

			var client = new BadgeUpClient(this.API_KEY);

			var metrics = await client.Metric.GetAll();
			Assert.NotEmpty(metrics);
		}

		[SkippableFact]
		public async Task MetricsIntegration_GetAllBySubject()
		{
			if (string.IsNullOrEmpty(this.API_KEY))
				throw new SkipException("Tests skipped on environments without API_KEY variable configured");

			var client = new BadgeUpClient(this.API_KEY);

			// Verify we have any metrics.
			var allMetrics = await client.Metric.GetAll();
			Assert.NotEmpty(allMetrics);

			// Get the metrics for a subject.
			var groupedMetrics = allMetrics.GroupBy(m => m.Subject).OrderByDescending(ig => ig.Count()).ToList();
			var subject = groupedMetrics.FirstOrDefault()?.Key;
			var metricsForTestedSubject = groupedMetrics.FirstOrDefault().ToList();
			Assert.NotNull(subject);
			Assert.NotEmpty(metricsForTestedSubject);

			var result = await client.Metric.GetAllBySubject(subject);

			// Verify the result
			Assert.NotNull(result);
			Assert.NotSame(metricsForTestedSubject, result);
			Assert.Equal(metricsForTestedSubject.Count(), result.Count());

			for (int i = 0; i < metricsForTestedSubject.Count(); i++)
			{
				var expectedMetric = metricsForTestedSubject[i];
				var actualMetric = result[i];

				Assert.Equal(expectedMetric.Id, actualMetric.Id);
				Assert.Equal(expectedMetric.ApplicationId, actualMetric.ApplicationId);
				Assert.Equal(expectedMetric.Key, actualMetric.Key);
				Assert.Equal(expectedMetric.Subject, actualMetric.Subject);
				Assert.Equal(expectedMetric.Value, actualMetric.Value);
			}
		}

		[SkippableFact]
		public async Task MetricsIntegration_GetIndividualBySubject()
		{
			if (string.IsNullOrEmpty(this.API_KEY))
				throw new SkipException("Tests skipped on environments without API_KEY variable configured");

			var client = new BadgeUpClient(this.API_KEY);

			// Verify we have any metrics.
			var allMetrics = await client.Metric.GetAll();
			Assert.NotEmpty(allMetrics);

			// Get the first metric.
			var firstMetric = allMetrics.First();
			var result = await client.Metric.GetIndividualBySubject(firstMetric.Subject, firstMetric.Key);

			// Verify the result
			Assert.NotNull(result);
			Assert.NotSame(firstMetric, result);
			Assert.Equal(firstMetric.Id, result.Id);
			Assert.Equal(firstMetric.ApplicationId, result.ApplicationId);
			Assert.Equal(firstMetric.Key, result.Key);
			Assert.Equal(firstMetric.Subject, result.Subject);
			Assert.Equal(firstMetric.Value, result.Value);
		}

		[SkippableFact]
		public async Task MetricsIntegration_Create()
		{
			if (string.IsNullOrEmpty(this.API_KEY))
				throw new SkipException("Tests skipped on environments without API_KEY variable configured");

			var client = new BadgeUpClient(this.API_KEY);

			// Create a new metric
			var result = await client.Metric.Create(new Metric()
			{
				Key = "test:metric",
				Subject = "randomsubject",
				Value = 5
			});

			// Verify the metric has been created with the same parameters
			Assert.NotNull(result);
			Assert.Equal("test:metric", result.Key);
			Assert.Equal("randomsubject", result.Subject);
			Assert.Equal(5, result.Value);

			// Verify we can get the metric by id.
			var metric = await client.Metric.GetIndividualBySubject(result.Subject, "test:metric");
			Assert.NotNull(metric);

			Assert.Equal("test:metric", metric.Key);
			Assert.Equal("randomsubject", metric.Subject);
			Assert.Equal(5, metric.Value);

			// TODO: Delete the metric
			// TODO: Verify deletion has completed successfully.
		}
	}
}
