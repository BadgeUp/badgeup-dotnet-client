using System.Runtime.Serialization;

namespace BadgeUpClient.Types
{
	[DataContract]
	public class Modifier
	{
		[DataMember( Name = "@inc" )]
		public int? Inc { get; set; }

		[DataMember( Name = "@dec" )]
		public int? Dec { get; set; }

		[DataMember( Name = "@mult" )]
		public int? Mult { get; set; }

		[DataMember( Name = "@div" )]
		public int? Div { get; set; }

		[DataMember( Name = "@set" )]
		public int? Set { get; set; }

		[DataMember( Name = "@min" )]
		public int? Min { get; set; }

		[DataMember( Name = "@max" )]
		public int? Max { get; set; }
	}
}
