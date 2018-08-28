namespace BadgeUp.Types
{
	public class Criterion
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public string Key { get; set; }
		public CriterionEvaluation Evaluation { get; set; }
		public Meta Meta { get; set; }
	}
}
