using System.Collections.Generic;
using System.Linq;
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
		/// <param name="onlyNew">Include only new progress objects </param>
		/// <returns>Returns a <see cref="EventRequest"/> object with the <see cref="Event"/> and <see cref="Progress"/> towards any relevant achievements</returns>
		public async Task<EventResponse> Send(Event @event, bool? showIncomplete = null, bool onlyNew = false)
		{
			HttpQuery query = new HttpQuery();

			if (showIncomplete.HasValue)
			{
				query.Add("showIncomplete", showIncomplete.Value.ToString().ToLower());
			}

			var result = await this.m_httpClient.Post<EventResponse>(new EventRequest(@event), "events", query: query.ToString());
			if (onlyNew)
				result.Progress = result.Progress.Where(x => x.IsNew).ToArray();
			return result;
		}

		/// <summary>
		/// V2 Preview to send an event to BadgeUp to be processed, returning array of results containing achievement progress records for any achievement affected.
		/// </summary>
		/// <param name="event">Event object to send to BadgeUp</param>
		/// <param name="showIncomplete">Show progress towards an achievement, even if it is incomplete</param>
		/// <param name="onlyNew">Include only new progress objects </param>
		/// <returns>Returns a <see cref="EventResponseV2Preview"/> object with the <see cref="Event"/> and <see cref="Progress"/> towards any relevant achievements</returns>
		public async Task<EventResponseV2Preview> SendV2Preview(Event @event, bool? showIncomplete = null, bool onlyNew = false)
		{
			HttpQuery query = new HttpQuery();

			if (showIncomplete.HasValue)	
			{
				query.Add("showIncomplete", showIncomplete.Value.ToString().ToLower());
			}

			var result = await this.m_httpClient.Post<EventResponseV2Preview>(new EventRequest(@event), "events", query: query.ToString(), headers: new Dictionary<string, string>{{ "X-V2-PREVIEW", "true" } });
			if (onlyNew)
				result.Results.ForEach(res => res.Progress = res.Progress.Where(x => x.IsNew).ToArray());
			return result;
		}
	}
}
