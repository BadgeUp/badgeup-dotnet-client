using BadgeUpClient.Types;

namespace BadgeUpClient.Requests
{
	public class EventRequest : Request
	{
		public Event Event { get; set; }


		public EventRequest( Event @event )
		{
			this.Event = @event;
		}

		public override string ToJson()
		{
			return Json.Serialize( this.Event );
		}
	}
}
