using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BadgeUp.Types
{
	// Override the default StringEnumConverter specified in JSON.cs and forcing a non-camel case serialization.
	[JsonConverter(typeof(StringEnumConverter), false)]
	public enum EarnedAwardState
	{
		[EnumMember(Value = "CREATED")]
		Created = 0,

		[EnumMember(Value = "APPROVED")]
		Approved = 1,

		[EnumMember(Value = "REJECTED")]
		Rejected = 2,

		[EnumMember(Value = "REDEEMED")]
		Redeemed = 3
	}
}
