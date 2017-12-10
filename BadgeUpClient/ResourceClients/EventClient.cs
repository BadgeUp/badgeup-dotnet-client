using System.Threading.Tasks;
using BadgeUpClient.Http;
using BadgeUpClient.Responses;
using BadgeUpClient.Requests;
using BadgeUpClient.Types;

namespace BadgeUpClient.ResourceClients
{
	public class EventClient
	{
		const string ENDPOINT = "events";
		protected BadgeUpHttpClient m_httpClient;

		public EventClient(BadgeUpHttpClient httpClient)
		{
			this.m_httpClient = httpClient;
		}

		/// <summary>
		/// Send an event to BadgeUp to be processed, returning achievement progress status
		/// </summary>
		/// <param name="event">Event object to send to BadgeUp</param>
		/// <param name="showIncomplete">Show progress towards an achievement, even if it is incomplete</param>
		/// <returns>Returns a <see cref="EventRequest"/> object with the <see cref="Event"/> and <see cref="Progress"/> towards any relevant achievements</returns>
		public async Task<EventResponse> Send(Event @event, bool? showIncomplete = null)
		{
			HttpQuery query = new HttpQuery();

			if (showIncomplete.HasValue)
			{
			    query.Add("showIncomplete", showIncomplete.Value.ToString().ToLower());
			}

			return await this.m_httpClient.Post<EventResponse>(new EventRequest(@event), "events", query: query.ToString());
		}
	}
}
