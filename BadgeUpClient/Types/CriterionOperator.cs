using System.Runtime.Serialization;

namespace BadgeUp.Types
{
	public enum CriterionOperator
	{
		[EnumMember(Value = "@gt")]
		Greater,
		[EnumMember(Value = "@gte")]
		GreaterOrEqual,
		[EnumMember(Value = "@lt")]
		Less,
		[EnumMember(Value = "@lte")]
		LessOrEqual,
		[EnumMember(Value = "@eq")]
		Equal
	}
}
