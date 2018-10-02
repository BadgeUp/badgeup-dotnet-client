using System.Runtime.Serialization;
using BadgeUp.Types;

namespace BadgeUp.Responses
{
	public class CriterionResponse : Response
	{
		public string Id { get; set; }
		public string ApplicationId { get; set; }
		public string Key { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public Meta Meta { get; set; }
		public CriterionEvaluation Evaluation { get; set; }
	}
}
