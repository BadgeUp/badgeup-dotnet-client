using BadgeUp.Types;

namespace BadgeUp.Requests
{
	public class AchievementRequest : Request
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
