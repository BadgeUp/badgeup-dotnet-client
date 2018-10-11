namespace BadgeUp
{
	internal abstract class Request
	{
		public virtual string ToJson()
		{
			return Json.Serialize( this );
		}
	}
}
