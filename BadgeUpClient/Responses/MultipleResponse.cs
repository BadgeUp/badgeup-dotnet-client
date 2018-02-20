using System.Collections.Generic;
using BadgeUp.Types;

namespace BadgeUp.Responses
{
	public class MultipleResponse<TResponse>
	{
		public List<TResponse> Data { get; set; }
		public Pagination Pages { get; set; }
	}
}
