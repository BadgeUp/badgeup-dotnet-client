using System;
using System.Net.Http;
using System.Threading.Tasks;
using BadgeUp.Http;
using BadgeUp.ResourceClients;
using BadgeUp.Types;
using RichardSzalay.MockHttp;
using Xunit;

namespace BadgeUp.Tests
{
	public class CriterionClientTests
	{
		private const string ApiKey = "eyJhY2NvdW50SWQiOiJmdmp0c2dsbjFmIiwiYXBwbGljYXRpb25JZCI6Imc2anRzaGxuNDgiLCJrZXkiOiJjamE3NGRjcGJqdHN0bGc3bmY0bW5ieTk5In0=";
		private const string Host = "https://api.useast1.badgeup.io";

		[Fact]
		public async Task WhenCriterionIsNull_CreateThrowsException()
		{
			var client = new CriterionClient(null);
			await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create(null));
		}

		[Fact]
		public async Task WhenSetupWithResponseData_Create_CallsUrlOnceAndReturnsCorrectResult()
		{
			var apiKey = BadgeUp.ApiKey.Create(ApiKey);

			var responseJson =
			@"{
				id: '12345',
				name: 'Criterion',
				description: 'criterion description',
				key: 'a:crit',
				evaluation: {
					type: 'standard',
					operator: '@gte',
					threshold: 5,
					repeatOptions: {
						carryOver: true
					}
				},
				'meta': {
					'created': '2016-09-12T06:51:35.453Z',
				}
			}".Replace("'", "\"");

			// setup the response action
			var url = $"{Host}/v1/apps/{apiKey.ApplicationId}/criteria";
			var mockHttp = new MockHttpMessageHandler();
			var expectedRequest = mockHttp.Expect(HttpMethod.Post, url).Respond("application/json", responseJson);
			mockHttp.Fallback.Throw(new InvalidOperationException("No matching mock handler"));

			using (var badgeUpHttpClient = new BadgeUpHttpClient(apiKey, Host))
			{
				badgeUpHttpClient._SetHttpClient(mockHttp.ToHttpClient());
				var client = new CriterionClient(badgeUpHttpClient);

				var result = await client.Create(new Criterion()
				{
					Name = "Criterion",
					Description = "criterion description",
					Key = "a:crit",
					Evaluation = new CriterionEvaluation()
					{
						Type = CriterionEvaluationType.Standard,
						Operator = CriterionOperator.GreaterOrEqual,
						Threshold = 5,
						RepeatOptions = new CriterionRepeatOptions()
						{
							CarryOver = true,
						},
						Period = null, // Period is only used for Timeseries type
						Multiplicity = null, // Multiplicity is only used for Timeseries type
					}
				});

				Assert.Equal("Criterion", result.Name);
				Assert.Equal("criterion description", result.Description);
				Assert.Equal("a:crit", result.Key);
				Assert.Equal(CriterionEvaluationType.Standard, result.Evaluation.Type);
				Assert.Equal(CriterionOperator.GreaterOrEqual, result.Evaluation.Operator);
				Assert.Equal(5, result.Evaluation.Threshold);
				Assert.True(result.Evaluation.RepeatOptions.CarryOver);
				Assert.Null(result.Evaluation.Period);
				Assert.Null(result.Evaluation.Multiplicity);
				Assert.Equal(new DateTime(2016, 09, 12, 06, 51,35,453), result.Meta.Created);
			}

			mockHttp.VerifyNoOutstandingExpectation();
		}
	}
}
