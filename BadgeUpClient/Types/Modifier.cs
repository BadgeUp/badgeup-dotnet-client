using System.Runtime.Serialization;

namespace BadgeUpClient.Types
{
	/// <summary>
	/// Metric modifier key/value pair
	/// </summary>
	[DataContract]
	public class Modifier
	{
		/// <summary>
		/// Increments the metric by the specified amount
		/// </summary>
		[DataMember( Name = "@inc" )]
		public int? Inc { get; set; }
		/// <summary>
		/// Decrements the metric by the specified amount
		/// </summary>
		[DataMember( Name = "@dec" )]
		public int? Dec { get; set; }
		/// <summary>
		/// Multiplies the metric by the specified amount
		/// </summary>
		[DataMember( Name = "@mult" )]
		public int? Mult { get; set; }
		/// <summary>
		/// Divides the metric by the specified amount
		/// </summary>
		[DataMember( Name = "@div" )]
		public int? Div { get; set; }
		/// <summary>
		/// Sets the metric to the specified value
		/// </summary>
		[DataMember( Name = "@set" )]
		public int? Set { get; set; }
		/// <summary>
		/// Sets the metric to the minimum of the current value and the specified value
		/// </summary>
		[DataMember( Name = "@min" )]
		public int? Min { get; set; }
		/// <summary>
		/// Sets the metric to the maximum of the current value and the specified value
		/// </summary>
		[DataMember( Name = "@max" )]
		public int? Max { get; set; }
	}
}
