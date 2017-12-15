using System.Collections.Generic;
using BadgeUpClient.Types;

namespace BadgeUpClient.Responses
{
	public class MetricMultipleResponse
	{
		public List<MetricResponse> Data { get; set; }
		public Pagination Pages { get; set; }

	}
}
