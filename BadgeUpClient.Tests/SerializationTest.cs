using System.Collections.Generic;
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
}
