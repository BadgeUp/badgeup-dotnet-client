using System.Collections.Generic;
using BadgeUpClient.Responses;
using BadgeUpClient.Types;
using Xunit;

namespace BadgeUpClient.Tests
{
	public class EventSerializationTest
	{
		const string EventJson = "{\"subject\":\"subject_foo\",\"key\":\"key_foo\",\"modifier\":{\"@dec\":2}}";

		[Fact]
		public void Serialization_EventSerialize()
		{
			var @event = new Types.Event( "subject_foo", "key_foo", new Types.Modifier { Dec = 2 } );

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
	}

	public class ProgressSerializationTest
	{
		string ProgressJson =
@"{
	'achievementId': 'cj1sp5nse02j9zkruwhb3zwik',
	'earnedAchievementId': 'cj1ss153y02k1zkrun39g8itq',
	'isComplete': true,
	'isNew': true,
	'percentComplete': 0.81,
	'progressTree': {
		'type': 'GROUP',
		'groups': [],
		'criteria': {
			'cj1sp461o02imzkruqkqi8amh': 1
		},
		'condition': 'AND'
	}
}".Replace("'", "\"").Replace("(\"(?:[^\"\\\\]|\\\\.)*\")|\\s+", "$1");

		[Fact]
		public void Serialization_ProgressDeserialize()
		{
			var progress = Json.Deserialize<Progress>( ProgressJson );

			Assert.Equal( "cj1sp5nse02j9zkruwhb3zwik", progress.AchievementId );
			Assert.True( progress.IsComplete );
			Assert.Equal( 0.81f, progress.PercentComplete );
			Assert.NotNull( progress.ProgressTree );
			Assert.Equal( 1, progress.ProgressTree.Criteria.Count );
		}
	}

	public class AchievementSerializationTest
	{
		string AchievementJson =
@"{
	'id': 'cj1sp5nse02j9zkruwhb3zwik',
	'applicationId': 'y70ujss',
	'name': 'Anger Management',
	'description': 'Relentlessly punish inanimate objects',
	'evalTree': {
		'type': 'GROUP',
		'groups': [],
		'criteria': [
			'cirjx77kw0004113jlb0h5l51'
		],
		'condition': 'AND'
	},
	'awards': [
		'ciqjx77kw2684513jlb0p5l51'
	],
	'meta': {
		'created': '2016-08-07T01:18:19.061Z',
		'icon': 'https://example.com/image'
	},
	'options': {
		'suspended': true
	}
}".Replace("'", "\"").Replace("(\"(?:[^\"\\\\]|\\\\.)*\")|\\s+", "$1");

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
			Assert.Equal( System.DateTime.Parse("2016-08-07T01:18:19.061Z").ToUniversalTime(), achievement.Meta.Created );
			Assert.Equal( "https://example.com/image", achievement.Meta.Icon );
		}
	}

	public class AwardSerializationTest
	{
		string awardJson =
@"{
	'id': 'cj1syiekxnxrb6kovpsxglcdx',
	'applicationId': 'y70ujss',
	'name': '20 Gold',
	'description': '20 Gold reward from the king!',
	'data': {
		'gold': 20,
		'otherNestedData': {
			'bool': true
		}
	}
}".Replace("'", "\"").Replace("(\"(?:[^\"\\\\]|\\\\.)*\")|\\s+", "$1");

		[Fact]
		public void Serialization_AwardDeserialize()
		{
			var award = Json.Deserialize<AwardResponse>( awardJson );

			Assert.Equal( "cj1syiekxnxrb6kovpsxglcdx", award.Id );
			Assert.Equal( "y70ujss", award.ApplicationId );
			Assert.Equal( "20 Gold", award.Name );
			Assert.Equal( "20 Gold reward from the king!", award.Description );

			Assert.Equal( 20, award.Data["gold"] );
			Assert.Equal( true, award.Data["otherNestedData"]["bool"] );
		}
	}
}
