using BadgeUpClient.Types;
using Xunit;

namespace BadgeUpClient.Tests
{
	public class SerializationTest
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
}
