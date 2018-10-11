using BadgeUp.Types;

namespace BadgeUp.Requests
{
	internal class AchievementRequest : Request
	{
		public AchievementRequest(Achievement achievement)
		{
			this.Achievement = achievement;
		}

		public Achievement Achievement { get; }

		public override string ToJson()
		{
			return Json.Serialize(this.Achievement);
		}
	}
}
