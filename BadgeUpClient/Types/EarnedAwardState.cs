using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BadgeUp.Types
{
	public enum EarnedAwardState
	{
		Created = 0,
		Approved = 1,
		Rejected = 2,
		Redeemed = 3
	}
}
