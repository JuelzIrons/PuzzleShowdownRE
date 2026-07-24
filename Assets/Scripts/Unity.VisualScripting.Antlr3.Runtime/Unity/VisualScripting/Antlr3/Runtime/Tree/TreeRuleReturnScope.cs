namespace Unity.VisualScripting.Antlr3.Runtime.Tree
{
	public class TreeRuleReturnScope : global::Unity.VisualScripting.Antlr3.Runtime.RuleReturnScope
	{
		private object start;

		public override object Start
		{
			get
			{
				return start;
			}
			set
			{
				start = value;
			}
		}
	}
}
