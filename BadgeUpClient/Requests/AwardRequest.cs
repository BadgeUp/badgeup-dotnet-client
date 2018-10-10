using BadgeUp.Types;

namespace BadgeUp.Requests
{
	public class AwardRequest : Request
	{
		public AwardRequest(Award award)
		{
			this.Award = award;
		}

		public Award Award { get; }

		public override string ToJson()
		{
			return Json.Serialize(this.Award);
		}
	}
}
