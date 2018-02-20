using System;

namespace BadgeUp
{
	public class BadgeUpClientException : Exception
	{
		public BadgeUpClientException()
		{
		}

		public BadgeUpClientException( string message )
			: base( message )
		{
		}
	}
}
