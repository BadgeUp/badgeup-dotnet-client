using System;

namespace BadgeUpClient
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
