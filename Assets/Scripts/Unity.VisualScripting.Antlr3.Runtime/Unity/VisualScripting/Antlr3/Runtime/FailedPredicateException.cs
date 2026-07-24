namespace Unity.VisualScripting.Antlr3.Runtime
{
	[global::System.Serializable]
	public class FailedPredicateException : global::Unity.VisualScripting.Antlr3.Runtime.RecognitionException
	{
		public string ruleName;

		public string predicateText;

		public FailedPredicateException()
		{
		}

		public FailedPredicateException(global::Unity.VisualScripting.Antlr3.Runtime.IIntStream input, string ruleName, string predicateText)
			: base(input)
		{
			this.ruleName = ruleName;
			this.predicateText = predicateText;
		}

		public override string ToString()
		{
			return "FailedPredicateException(" + ruleName + ",{" + predicateText + "}?)";
		}
	}
}
