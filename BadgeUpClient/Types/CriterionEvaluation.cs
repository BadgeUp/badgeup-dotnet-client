namespace BadgeUp.Types
{
	public class CriterionEvaluation
	{
		public CriterionEvaluationType Type { get; set; }
		public CriterionOperator Operator { get; set; }
		public int Threshold { get; set; }
		public CriterionRepeatOptions RepeatOptions { get; set; }
		public CriterionEvaluationPeriod Period { get; set; }
		public CriterionEvaluationMultiplicity Multiplicity { get; set; }
	}
}