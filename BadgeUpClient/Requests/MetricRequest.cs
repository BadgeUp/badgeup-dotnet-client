using BadgeUp.Types;

namespace BadgeUp.Requests
{
	internal class MetricRequest : Request
	{
		public MetricRequest(Metric metric)
		{
			this.Metric = metric;
		}

		public Metric Metric { get; }

		public override string ToJson()
		{
			return Json.Serialize(this.Metric);
		}
	}
}
