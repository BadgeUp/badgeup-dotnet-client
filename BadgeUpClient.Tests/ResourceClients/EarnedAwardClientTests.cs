using BadgeUp.Http;
using BadgeUp.ResourceClients;
using BadgeUp.Types;
using RichardSzalay.MockHttp;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace BadgeUp.Tests
{
	public class EarnedAwardClientTests
	{
		private const string ApiKey = "eyJhY2NvdW50SWQiOiJmdmp0c2dsbjFmIiwiYXBwbGljYXRpb25JZCI6Imc2anRzaGxuNDgiLCJrZXkiOiJjamE3NGRjcGJqdHN0bGc3bmY0bW5ieTk5In0=";
		private const string Host = "https://api.useast1.badgeup.io";

		[Fact]
		public async Task WhenSetupWithResponseData_GetById_CallsUrlOnceAndReturnsCorrectResult()
		{
			Assert.False(string.IsNullOrEmpty(EarnedAwardClientTests.ApiKey));

			var apiKey = BadgeUp.ApiKey.Create(ApiKey);

			var responseJson =
			@"{
				'id': 'cjktcmn5o635wmyd0w4ps',
				'applicationId': 'g6jtshln48',
				'achievementId': 'cjktccaf8rsfdia9ea565npch',
				'earnedAchievementId': 'cjktcmn5n635w69dhfjp3',
				'awardId': 'cjktceks5dxfy1n8e04bcnl1g',
				'subject': 'subject-1',
				'state': 'APPROVED',
				'meta': {
					'created': '2016-09-12T06:51:35.453Z'
				}
			}".Replace("'", "\"");

			// setup the response action
			var url = $"{Host}/v2/apps/{apiKey.ApplicationId}/earnedawards/cjktcmn5o635wmyd0w4ps";
			var mockHttp = new MockHttpMessageHandler();
			var expectedRequest = mockHttp.Expect(HttpMethod.Get, url).Respond("application/json", responseJson);
			mockHttp.Fallback.Throw(new InvalidOperationException("No matching mock handler"));

			using (var badgeUpHttpClient = new BadgeUpHttpClient(apiKey, Host))
			{
				badgeUpHttpClient.SetHttpClient(mockHttp.ToHttpClient());
				var client = new EarnedAwardClient(badgeUpHttpClient);

				// act
				var result = await client.GetById("cjktcmn5o635wmyd0w4ps");

				// assert url was called only once
				mockHttp.VerifyNoOutstandingExpectation();

				// assert result was parsed correctly
				Assert.NotNull(result);
				Assert.Equal("cjktcmn5o635wmyd0w4ps", result.Id);
				Assert.Equal("g6jtshln48", result.ApplicationId);
				Assert.Equal("cjktccaf8rsfdia9ea565npch", result.AchievementId);
				Assert.Equal("cjktcmn5n635w69dhfjp3", result.EarnedAchievementId);
				Assert.Equal("cjktceks5dxfy1n8e04bcnl1g", result.AwardId);
				Assert.Equal("subject-1", result.Subject);
				Assert.Equal(EarnedAwardState.Approved, result.State);
				Assert.Equal(new DateTime(2016, 09, 12, 06, 51, 35, 453, DateTimeKind.Utc), result.Meta.Created);
			}
		}

		[Fact]
		public async Task WhenSetupWithResponseData_GetAllWithoutQueryParams_CallsUrlOnceAndReturnsCorrectResult()
		{
			Assert.False(string.IsNullOrEmpty(EarnedAwardClientTests.ApiKey));

			var apiKey = BadgeUp.ApiKey.Create(ApiKey);

			var responseJson =
			@"{
				'pages': {
					'previous': null,
					'next': null
				},
				'data': [
					{
						'id': 'cjktcmn5o635wmyd0w4ps',
						'applicationId': 'g6jtshln48',
						'achievementId': 'cjktccaf8rsfdia9ea565npch',
						'earnedAchievementId': 'cjktcmn5n635w69dhfjp3',
						'awardId': 'cjktceks5dxfy1n8e04bcnl1g',
						'subject': 'subject-1',
						'state': 'REDEEMED',
						'meta': {
							'created': '2016-07-12T06:51:35.453Z'
						}
					},
					{
						'id': 'cjktcmngk629suhbsp3ww',
						'applicationId': 'g6jtshln48',
						'achievementId': 'cjktccaf8rsfdia9ea565npch',
						'earnedAchievementId': 'cjktcmngk629ssk6jufko',
						'awardId': 'cjktceks5dxfy1n8e04bcnl1g',
						'subject': 'subject-2',
						'state': 'APPROVED',
						'meta': {
							'created': '2016-07-14T06:51:35.844Z'
						}
					},
					{
						'id': 'cjktcmyhl635wpg6qbjxf',
						'applicationId': 'g6jtshln48',
						'achievementId': 'cjktccaf8rsfdia9ea565npch',
						'earnedAchievementId': 'cjktcmyhl635wjshv2kc2',
						'awardId': 'cjktceks5dxfy1n8e04bcnl1g',
						'subject': 'subject-3',
						'state': 'CREATED',
						'meta': {
							'created': '2016-09-18T06:51:50.137Z'
						}
					}
				]
			}".Replace("'", "\"");

			// setup the response action
			var url = $"{Host}/v2/apps/{apiKey.ApplicationId}/earnedawards";
			var mockHttp = new MockHttpMessageHandler();
			var expectedRequest = mockHttp.Expect(HttpMethod.Get, url).Respond("application/json", responseJson);
			mockHttp.Fallback.Throw(new InvalidOperationException("No matching mock handler"));

			using (var badgeUpHttpClient = new BadgeUpHttpClient(apiKey, Host))
			{
				badgeUpHttpClient.SetHttpClient(mockHttp.ToHttpClient());
				var client = new EarnedAwardClient(badgeUpHttpClient);

				// act
				var result = await client.GetAll();

				// assert url was called only once
				mockHttp.VerifyNoOutstandingExpectation();

				// assert result was parsed correctly
				Assert.NotNull(result);
				Assert.Equal(3, result.Count);
				Assert.Equal("cjktcmn5o635wmyd0w4ps", result[0].Id);
				Assert.Equal("g6jtshln48", result[0].ApplicationId);
				Assert.Equal("cjktccaf8rsfdia9ea565npch", result[0].AchievementId);
				Assert.Equal("cjktcmn5n635w69dhfjp3", result[0].EarnedAchievementId);
				Assert.Equal("cjktceks5dxfy1n8e04bcnl1g", result[0].AwardId);
				Assert.Equal("subject-1", result[0].Subject);
				Assert.Equal(EarnedAwardState.Redeemed, result[0].State);
				Assert.Equal(new DateTime(2016, 07, 12, 06, 51, 35, 453, DateTimeKind.Utc), result[0].Meta.Created);
			}
		}

		[Fact]
		public async Task WhenPassedQueryParams_GetAll_CallsUrlOnceWithCorrectQueryParamsAndReturnsCorrectResult()
		{
			Assert.False(string.IsNullOrEmpty(EarnedAwardClientTests.ApiKey));

			var apiKey = BadgeUp.ApiKey.Create(ApiKey);

			var responseJson =
			@"{
				'pages': {
					'previous': null,
					'next': null
				},
				'data': [
					{
						'id': 'cjktcmn5o635wmyd0w4ps',
						'applicationId': 'g6jtshln48',
						'achievementId': 'cjktccaf8rsfdia9ea565npch',
						'earnedAchievementId': 'cjktcmn5n635w69dhfjp3',
						'awardId': 'cjktceks5dxfy1n8e04bcnl1g',
						'subject': 'subject-1',
						'state': 'CREATED',
						'meta': {
							'created': '2016-07-12T06:51:35.453Z'
						}
					}
				]
			}".Replace("'", "\"");

			// setup the response action
			var url = $"{Host}/v2/apps/{apiKey.ApplicationId}/earnedawards";
			var mockHttp = new MockHttpMessageHandler();
			var expectedRequest = mockHttp.Expect(HttpMethod.Get, url)
				.WithQueryString("subject", "subject-1")
				.Respond("application/json", responseJson);
			mockHttp.Fallback.Throw(new InvalidOperationException("No matching mock handler"));

			using (var badgeUpHttpClient = new BadgeUpHttpClient(apiKey, Host))
			{
				badgeUpHttpClient.SetHttpClient(mockHttp.ToHttpClient());
				var client = new EarnedAwardClient(badgeUpHttpClient);

				// act
				var result = await client.GetAll(new EarnedAwardQueryParams()
				{
					Subject = "subject-1"
				});

				// assert url was called only once
				mockHttp.VerifyNoOutstandingExpectation();

				// assert result was parsed correctly
				Assert.NotNull(result);
				Assert.Single(result);
				Assert.Equal("cjktcmn5o635wmyd0w4ps", result[0].Id);
				Assert.Equal("g6jtshln48", result[0].ApplicationId);
				Assert.Equal("cjktccaf8rsfdia9ea565npch", result[0].AchievementId);
				Assert.Equal("cjktcmn5n635w69dhfjp3", result[0].EarnedAchievementId);
				Assert.Equal("cjktceks5dxfy1n8e04bcnl1g", result[0].AwardId);
				Assert.Equal("subject-1", result[0].Subject);
				Assert.Equal(EarnedAwardState.Created, result[0].State);
				Assert.Equal(new DateTime(2016, 07, 12, 06, 51, 35, 453, DateTimeKind.Utc), result[0].Meta.Created);
			}
		}

		[Fact]
		public async Task WhenStateIsCreated_ChangeState_ThrowsArgumentException()
		{
			var client = new EarnedAwardClient(null);
			await Assert.ThrowsAsync<ArgumentException>(() => client.ChangeState("cjktcmn5o635wmyd0w4ps", EarnedAwardState.Created));
		}

		[Fact]
		public async Task WhenIdIsNull_ChangeState_ThrowsArgumentNullException()
		{
			var client = new EarnedAwardClient(null);
			await Assert.ThrowsAsync<ArgumentNullException>(() => client.ChangeState(null, EarnedAwardState.Approved));
		}

		[Fact]
		public async Task WhenSetupWithResponseData_ChangeState_CallsUrlOnceAndReturnsCorrectResult()
		{
			Assert.False(string.IsNullOrEmpty(EarnedAwardClientTests.ApiKey));

			var apiKey = BadgeUp.ApiKey.Create(ApiKey);

			var responseJson = @"{
				'id': 'cjktcmn5o635wmyd0w4ps',
				'applicationId': 'g6jtshln48',
				'achievementId': 'cjktccaf8rsfdia9ea565npch',
				'earnedAchievementId': 'cjktcmngk629ssk6jufko',
				'awardId': 'cjktceks5dxfy1n8e04bcnl1g',
				'subject': 'subject-2',
				'state': 'redeemed',
				'meta': {
					'created': '2016-07-14T06:51:35.844Z'
				}
			}".Replace("'","\"");

			// setup the response action
			var url = $"{Host}/v2/apps/{apiKey.ApplicationId}/earnedawards/cjktcmn5o635wmyd0w4ps/state";
			var mockHttp = new MockHttpMessageHandler();
			var expectedRequest = mockHttp.Expect(HttpMethod.Post, url).Respond("application/json", responseJson);
			mockHttp.Fallback.Throw(new InvalidOperationException("No matching mock handler"));

			using (var badgeUpHttpClient = new BadgeUpHttpClient(apiKey, Host))
			{
				badgeUpHttpClient.SetHttpClient(mockHttp.ToHttpClient());
				var client = new EarnedAwardClient(badgeUpHttpClient);

				// act
				var result = await client.ChangeState("cjktcmn5o635wmyd0w4ps", EarnedAwardState.Redeemed);

				// assert url was called only once
				mockHttp.VerifyNoOutstandingExpectation();

				// assert result was parsed correctly
				Assert.NotNull(result);
				Assert.Equal("cjktcmn5o635wmyd0w4ps", result.Id);
				Assert.Equal("g6jtshln48", result.ApplicationId);
				Assert.Equal("cjktccaf8rsfdia9ea565npch", result.AchievementId);
				Assert.Equal("cjktcmngk629ssk6jufko", result.EarnedAchievementId);
				Assert.Equal("cjktceks5dxfy1n8e04bcnl1g", result.AwardId);
				Assert.Equal("subject-2", result.Subject);
				Assert.Equal(EarnedAwardState.Redeemed, result.State);
				Assert.Equal(new DateTime(2016, 07, 14, 06, 51, 35, 844), result.Meta.Created);
			}
		}
	}
}
