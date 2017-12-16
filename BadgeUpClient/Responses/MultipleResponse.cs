using System.Collections.Generic;
using BadgeUpClient.Types;

namespace BadgeUpClient.Responses
{
	public class MultipleResponse<TResponse>
	{
		public List<TResponse> Data { get; set; }
		public Pagination Pages { get; set; }
	}
}
