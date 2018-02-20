namespace BadgeUp
{
	public class Request
	{
		public virtual string ToJson()
		{
			return Json.Serialize( this );
		}
	}
}
