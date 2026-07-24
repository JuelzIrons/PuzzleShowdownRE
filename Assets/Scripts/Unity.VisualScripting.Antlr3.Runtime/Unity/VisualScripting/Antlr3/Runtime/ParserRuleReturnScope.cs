namespace Unity.VisualScripting.Antlr3.Runtime
{
	public class ParserRuleReturnScope : global::Unity.VisualScripting.Antlr3.Runtime.RuleReturnScope
	{
		private global::Unity.VisualScripting.Antlr3.Runtime.IToken start;

		private global::Unity.VisualScripting.Antlr3.Runtime.IToken stop;

		public override object Start
		{
			get
			{
				return start;
			}
			set
			{
				start = (global::Unity.VisualScripting.Antlr3.Runtime.IToken)value;
			}
		}

		public override object Stop
		{
			get
			{
				return stop;
			}
			set
			{
				stop = (global::Unity.VisualScripting.Antlr3.Runtime.IToken)value;
			}
		}
	}
}
