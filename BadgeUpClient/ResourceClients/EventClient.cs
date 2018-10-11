using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BadgeUp.Http;
using BadgeUp.Requests;
using BadgeUp.Responses;
using BadgeUp.Types;

namespace BadgeUp.ResourceClients
{
	internal class EventClient : IEventClient
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
		/// <param name="onlyNew">Include only new progress objects </param>
		/// <returns>Returns a <see cref="EventResponse"/> object with the <see cref="Event"/> and <see cref="Progress"/> towards any relevant achievements</returns>
		public async Task<EventResponse> Send(Event @event, bool? showIncomplete = null, bool onlyNew = false)
		{
			HttpQuery query = new HttpQuery();

			if (showIncomplete.GetValueOrDefault(false))
			{
				query.Add("showIncomplete", showIncomplete.Value.ToString().ToLower());
			}

			var result = await this.m_httpClient.Post<EventResponse>(new EventRequest(@event), "events", query: query.ToString());
			if (onlyNew)
				result.Results.ForEach(res => res.Progress = res.Progress.Where(x => x.IsNew).ToArray());
			return result;
		}
	}
}
