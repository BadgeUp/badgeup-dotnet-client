using System.Collections.Generic;

namespace BadgeUp.Types
{
	public abstract class BaseGroup<T>
	{
		public string Type = "GROUP";
		public string Condition { get; set; }
		public BaseGroup<T>[] Groups { get; set; }
		public T Criteria { get; set; }
	}
}
