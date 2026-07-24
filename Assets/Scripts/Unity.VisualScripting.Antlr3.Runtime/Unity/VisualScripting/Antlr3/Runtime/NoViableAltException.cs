namespace Unity.VisualScripting.Antlr3.Runtime
{
	[global::System.Serializable]
	public class NoViableAltException : global::Unity.VisualScripting.Antlr3.Runtime.RecognitionException
	{
		public string grammarDecisionDescription;

		public int decisionNumber;

		public int stateNumber;

		public NoViableAltException()
		{
		}

		public NoViableAltException(string grammarDecisionDescription, int decisionNumber, int stateNumber, global::Unity.VisualScripting.Antlr3.Runtime.IIntStream input)
			: base(input)
		{
			this.grammarDecisionDescription = grammarDecisionDescription;
			this.decisionNumber = decisionNumber;
			this.stateNumber = stateNumber;
		}

		public override string ToString()
		{
			if (input is global::Unity.VisualScripting.Antlr3.Runtime.ICharStream)
			{
				return "NoViableAltException('" + (char)UnexpectedType + "'@[" + grammarDecisionDescription + "])";
			}
			return "NoViableAltException(" + UnexpectedType + "@[" + grammarDecisionDescription + "])";
		}
	}
}
