using BadgeUp.Types;

namespace BadgeUp.Requests
{
	internal class EarnedAwardRequest : Request
	{
		public EarnedAwardRequest(EarnedAwardState state)
		{
			this.State = state;
		}

		public EarnedAwardState State { get; }

		public override string ToJson()
		{
			return Json.Serialize<EarnedAwardRequest>(this);
		}
	}
}
