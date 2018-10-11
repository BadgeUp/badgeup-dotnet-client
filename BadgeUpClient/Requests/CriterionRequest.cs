using BadgeUp.Types;

namespace BadgeUp.Requests
{
	internal class CriterionRequest : Request
	{
		public CriterionRequest(Criterion criterion)
		{
			this.Criterion = criterion;
		}

		public Criterion Criterion { get; }

		public override string ToJson()
		{
			return Json.Serialize(this.Criterion);
		}
	}
}
