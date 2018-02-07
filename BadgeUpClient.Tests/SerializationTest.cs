using System;
using System.Collections.Generic;
using System.Globalization;
using BadgeUpClient.Responses;
using BadgeUpClient.Types;
using Xunit;

namespace BadgeUpClient.Tests
{
	public class EventSerializationTest
	{
		const string EventJson = "{\"subject\":\"subject_foo\",\"key\":\"key_foo\",\"timestamp\":\"2017-01-01T18:00:00+05:30\",\"modifier\":{\"@dec\":2}}";

		[Fact]
		public void Serialization_EventSerialize()
		{
			var @event = new Types.Event( "subject_foo", "key_foo", new Types.Modifier { Dec = 2 } ){ Timestamp = DateTimeOffset.Parse("2017-01-01T18:00:00+05:30") };

			var json = Json.Serialize( @event );
			Assert.Equal( EventJson, json );
		}

		[Fact]
		public void Serialization_EventDeserialize()
		{
			var @event = Json.Deserialize<Event>( EventJson );

			Assert.Equal( "subject_foo", @event.Subject );
			Assert.Equal( "key_foo", @event.Key );
			Assert.NotNull( @event.Modifier );
			Assert.Equal( 2, @event.Modifier.Dec );
		}

		private const string EventResponseV2PreviewJson =
			@"{
				""results"": [
					{
						""event"": {
							""subject"": ""dotnet-ci-1567"",
							""key"": ""test"",
							""modifier"": {
								""@inc"": 5
							},
							""timestamp"": ""2018-02-07T15:30:20.703Z"",
							""data"": null,
							""options"": null,
							""applicationId"": ""9hk14dln35"",
							""id"": ""cjdd8dm743o0qm2blmaddzrmi""
						},
						""cause"": ""CauseStr"",
						""progress"": [
							{
								""achievementId"": ""cjb0ls0fdxk75y06r03erl494"",
								""earnedAchievementId"": ""cjdd8dm7n3o0sm2blbx2jheuj"",
								""isComplete"": true,
								""isNew"": true,
								""percentComplete"": 1,
								""progressTree"": {
									""type"": ""GROUP"",
									""groups"": [],
									""criteria"": {
										""cjb0ln006esruzz6gymu12c6f"": 1
									},
									""condition"": ""AND""
								}
							}
						]
					}]}";

		[Fact]
		public void Serialization_EventResponseV2_Deserialize()
		{
			var @event = Json.Deserialize<EventResponseV2Preview>(EventResponseV2PreviewJson);

			Assert.Equal(1, @event.Results.Count);
			Assert.Equal("cjdd8dm743o0qm2blmaddzrmi", @event.Results[0].Event.Id);
			Assert.Equal("dotnet-ci-1567", @event.Results[0].Event.Subject);
			Assert.Equal("test", @event.Results[0].Event.Key);
			Assert.Equal("CauseStr", @event.Results[0].Cause);
			Assert.Equal("cjb0ls0fdxk75y06r03erl494", @event.Results[0].Progress[0].AchievementId);
		}
	}

	public class ProgressSerializationTest
	{
		string ProgressJson =
			@"{
				""achievementId"": ""cj1sp5nse02j9zkruwhb3zwik"",
				""earnedAchievementId"": ""cj1ss153y02k1zkrun39g8itq"",
				""isComplete"": true,
				""isNew"": true,
				""percentComplete"": 0.81,
				""progressTree"": {
					""type"": ""GROUP"",
					""groups"": [],
					""criteria"": {
						""cj1sp461o02imzkruqkqi8amh"": 1
					},
					""condition"": ""AND""
				}
			}";

		[Fact]
		public void Serialization_ProgressDeserialize()
		{
			var progress = Json.Deserialize<Progress>( ProgressJson );

			Assert.Equal( "cj1sp5nse02j9zkruwhb3zwik", progress.AchievementId );
			Assert.True( progress.IsComplete );
			Assert.Equal( 0.81f, progress.PercentComplete );
			Assert.NotNull( progress.ProgressTree );
			Assert.Single( progress.ProgressTree.Criteria);
		}
	}

	public class AchievementSerializationTest
	{
		private string AchievementJson =
			@"{
				""id"": ""cj1sp5nse02j9zkruwhb3zwik"",
				""applicationId"": ""y70ujss"",
				""name"": ""Anger Management"",
				""description"": ""Relentlessly punish inanimate objects"",
				""evalTree"": {
					""type"": ""GROUP"",
					""groups"": [],
					""criteria"": [
						""cirjx77kw0004113jlb0h5l51""
					],
					""condition"": ""AND""
				},
				""awards"": [
					""ciqjx77kw2684513jlb0p5l51""
				],
				""meta"": {
					""created"": ""2016-08-07T01:18:19.061Z"",
					""icon"": ""https://example.com/image""
				},
				""options"": {
					""suspended"": true
				}
			}";

		[Fact]
		public void Serialization_AchievementDeserialize()
		{
			var achievement = Json.Deserialize<AchievementResponse>( AchievementJson );

			Assert.Equal( "cj1sp5nse02j9zkruwhb3zwik", achievement.Id );
			Assert.Equal( "y70ujss", achievement.ApplicationId );
			Assert.Equal( "Anger Management", achievement.Name );
			Assert.Equal( "Relentlessly punish inanimate objects", achievement.Description );

			// eval tree
			Assert.Equal( "AND", achievement.EvalTree.Condition );
			Assert.Equal( new string[] { "cirjx77kw0004113jlb0h5l51" }, achievement.EvalTree.Criteria );
			Assert.Empty(achievement.EvalTree.Groups);

			// options
			Assert.True( achievement.Options.Suspended );

			// awards
			Assert.Equal( new string[] { "ciqjx77kw2684513jlb0p5l51" }, achievement.Awards );

			// meta
			Assert.Equal( System.DateTimeOffset.Parse("2016-08-07T01:18:19.061Z"), achievement.Meta.Created );
			Assert.Equal( "https://example.com/image", achievement.Meta.Icon );
		}
	}

	public class AwardSerializationTest
	{
		string awardJson =
			@"{
				""id"": ""cj1syiekxnxrb6kovpsxglcdx"",
				""applicationId"": ""y70ujss"",
				""name"": ""20 Gold"",
				""description"": ""20 Gold reward from the king!"",
				""data"": {
					""gold"": 20,
					""otherNestedData"": {
						""bool"": true
					}
				}
			}";

		[Fact]
		public void Serialization_AwardDeserialize()
		{
			var award = Json.Deserialize<AwardResponse>( awardJson );

			Assert.Equal( "cj1syiekxnxrb6kovpsxglcdx", award.Id );
			Assert.Equal( "y70ujss", award.ApplicationId );
			Assert.Equal( "20 Gold", award.Name );
			Assert.Equal( "20 Gold reward from the king!", award.Description );

			Assert.Equal( 20, award.Data["gold"] );
			Assert.True( (bool) award.Data["otherNestedData"]["bool"] );
		}
	}

	public class AccountSerializationTest
	{
		string accountJson =
			@"{
				""id"": ""3eknqblf51"",
				""name"": ""Account name with sybmols !@#$%^&*()_+"",
				""description"": ""Account description content"",
				""meta"" : {
					""created"": ""2017-11-19T20:08:33.48""
				}
			}";

		[Fact]
		public void Serialization_AccountDeserialize()
		{
			var account = Json.Deserialize<AccountResponse>(accountJson);

			Assert.Equal("3eknqblf51", account.Id);
			Assert.Equal(@"Account name with sybmols !@#$%^&*()_+", account.Name);
			Assert.Equal("Account description content", account.Description);
			Assert.Equal(DateTimeOffset.Parse("2017-11-19T20:08:33.48"), account.Meta.Created);
		}
	}

	public class ApplicationSerializationTest
	{
		string applicationJson =
			@"{
				""id"": ""9hk14dln35"",
				""accountId"": ""3eknqblf51"",
				""name"": ""vagabond volcano"",
				""description"": ""Application description content"",
				""meta"": {
					""created"": ""2017-11-19T20:08:59.62""
				}
			}";

		[Fact]
		public void Serialization_ApplicationDeserialize()
		{
			var application = Json.Deserialize<ApplicationResponse>(applicationJson);

			Assert.Equal("9hk14dln35", application.Id);
			Assert.Equal("3eknqblf51", application.AccountId);
			Assert.Equal("vagabond volcano", application.Name);
			Assert.Equal("Application description content", application.Description);
			Assert.Equal(DateTimeOffset.Parse("2017-11-19T20:08:59.62"), application.Meta.Created);
		}
	}

	public class CriterionSerializationTest
	{
		string criterionJson =
			@"{
				""id"": ""cjb0ln006esruzz6gymu12c6f"",
				""applicationId"": ""9hk14dln35"",
				""name"": ""criterion name"",
				""description"": ""criterion description"",
				""key"": ""key content"",
				""evaluation"": {
					""type"": ""standard"",
					""operator"": ""@gte"",
					""threshold"": 5
				},
				""meta"": {
					""created"": ""2017-12-10T10:01:08.55""
				}
			}";

		[Fact]
		public void Serialization_CriterionDeserialize()
		{
			var criterion = Json.Deserialize<CriterionResponse>(criterionJson);

			Assert.Equal("cjb0ln006esruzz6gymu12c6f", criterion.Id);
			Assert.Equal("9hk14dln35", criterion.ApplicationId);
			Assert.Equal("criterion name", criterion.Name);
			Assert.Equal("criterion description", criterion.Description);
			Assert.Equal("key content", criterion.Key);

			Assert.Equal("standard", criterion.Evaluation.Type);
			Assert.Equal(CriterionOperator.GreaterOrEqual, criterion.Evaluation.Operator);
			Assert.Equal(5, criterion.Evaluation.Threshold);

			Assert.Equal(DateTimeOffset.Parse("2017-12-10T10:01:08.55"), criterion.Meta.Created);
		}
	}
 
	public class MetricSerializationTest
	{
		string metricJson =
			@"{
				""id"": ""cjb2jszpz6r08y06rxhmbku3p"",
				""applicationId"": ""9hk14dln35"",
				""key"": ""metric Key"",
				""subject"": ""dotnet-ci-96512"",
				""value"":5
			}";

		[Fact]
		public void Serialization_MetricDeserialize()
		{
			var metric = Json.Deserialize<MetricResponse>(metricJson);

			Assert.Equal("cjb2jszpz6r08y06rxhmbku3p", metric.Id);
			Assert.Equal("9hk14dln35", metric.ApplicationId);
			Assert.Equal("metric Key", metric.Key);
			Assert.Equal("dotnet-ci-96512", metric.Subject);
			Assert.Equal(5, metric.Value);
		}

		private string multipleMetricJson =
			@"{
				""pages"": {
					""previous"": ""/v1/apps/9hk14dln35/metrics?before=cjb2kndex7us2y06rvu1441fs"",
					""next"": null
				},
				""data"": [
					{
						""id"": ""cjb2kndex7us2y06rvu1441fs"",
						""applicationId"": ""9hk14dln35"",
						""key"": ""testtest"",
						""subject"": ""dotnet-ci-42157"",
						""value"": 5
					},
					{
						""id"": ""cjb2knfsz7q54zz6gvgoglz7u"",
						""applicationId"": ""9hk14dln35"",
						""key"": ""test"",
						""subject"": ""dotnet-ci-61897"",
						""value"": 5
					},
					{
						""id"": ""cjb2ko6ssfzvrz349zhi0xp1v"",
						""applicationId"": ""9hk14dln35"",
						""key"": ""test"",
						""subject"": ""dotnet-ci-6226"",
						""value"": 5
					}
				]
			}";

		[Fact]
		public void Serialization_MetricMultipleDeserialize()
		{
			var metricMultiple = Json.Deserialize<MultipleResponse<MetricResponse>>(multipleMetricJson);

			Assert.Equal(@"/v1/apps/9hk14dln35/metrics?before=cjb2kndex7us2y06rvu1441fs", metricMultiple.Pages.Previous);
			Assert.Null(metricMultiple.Pages.Next);

			Assert.Equal(3, metricMultiple.Data.Count);

			Assert.Equal("cjb2kndex7us2y06rvu1441fs", metricMultiple.Data[0].Id);
			Assert.Equal("9hk14dln35", metricMultiple.Data[0].ApplicationId);
			Assert.Equal("testtest", metricMultiple.Data[0].Key);
			Assert.Equal("dotnet-ci-42157", metricMultiple.Data[0].Subject);
			Assert.Equal(5, metricMultiple.Data[0].Value);

			Assert.Equal("cjb2knfsz7q54zz6gvgoglz7u", metricMultiple.Data[1].Id);
			Assert.Equal("9hk14dln35", metricMultiple.Data[1].ApplicationId);
			Assert.Equal("test", metricMultiple.Data[1].Key);
			Assert.Equal("dotnet-ci-61897", metricMultiple.Data[1].Subject);
			Assert.Equal(5, metricMultiple.Data[1].Value);

			Assert.Equal("cjb2ko6ssfzvrz349zhi0xp1v", metricMultiple.Data[2].Id);
			Assert.Equal("9hk14dln35", metricMultiple.Data[2].ApplicationId);
			Assert.Equal("test", metricMultiple.Data[2].Key);
			Assert.Equal("dotnet-ci-6226", metricMultiple.Data[2].Subject);
			Assert.Equal(5, metricMultiple.Data[2].Value);

		}
	}
  
	public class AchievementIconSerializationTest
	{
		string achievementIconJson =
			@"[
				{
					""url"": ""https://storage.googleapis.com/badgeup-achievement-icons-useast1/3eknqblf51/9hk14dln35/8Cb9AKAfIWNz2AYkEKoQtnxMrmI.jpeg"",
					""fileName"":""image.jpg""
				},
				{
					""url"": ""https://storage.googleapis.com/badgeup-achievement-icons-useast1/3eknqblf51/9hk14dln35/EimzU11jvWBcGaEYU6LbuOh6wo.png"",
					""fileName"":""Untitled.png""
				}
			]";

		[Fact]
		public void Serialization_AchievementIconDeserialize()
		{
			var achievementIcons = Json.Deserialize<AchievementIconResponse[]>(achievementIconJson);

			Assert.Equal(2, achievementIcons.Length);

			Assert.Equal(@"https://storage.googleapis.com/badgeup-achievement-icons-useast1/3eknqblf51/9hk14dln35/8Cb9AKAfIWNz2AYkEKoQtnxMrmI.jpeg", achievementIcons[0].Url);
			Assert.Equal(@"image.jpg", achievementIcons[0].FileName);

			Assert.Equal(@"https://storage.googleapis.com/badgeup-achievement-icons-useast1/3eknqblf51/9hk14dln35/EimzU11jvWBcGaEYU6LbuOh6wo.png", achievementIcons[1].Url);
			Assert.Equal(@"Untitled.png", achievementIcons[1].FileName);
		}
	}

	public class DateTimeSerializationTest
	{
		[Fact]
		public void Serialization_DateTimeSerialize()
		{
			DateTimeOffset date = DateTimeOffset.Parse("2017-01-01T18:00:00+05:30");
			string dateSerialized = Json.Serialize(date);

			Assert.Equal("\"2017-01-01T18:00:00+05:30\"", dateSerialized);
		}
	}
}
