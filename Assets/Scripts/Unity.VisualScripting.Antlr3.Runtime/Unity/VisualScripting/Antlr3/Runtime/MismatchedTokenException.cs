namespace Unity.VisualScripting.Antlr3.Runtime
{
	[global::System.Serializable]
	public class MismatchedTokenException : global::Unity.VisualScripting.Antlr3.Runtime.RecognitionException
	{
		private int expecting;

		public int Expecting
		{
			get
			{
				return expecting;
			}
			set
			{
				expecting = value;
			}
		}

		public MismatchedTokenException()
		{
		}

		public MismatchedTokenException(int expecting, global::Unity.VisualScripting.Antlr3.Runtime.IIntStream input)
			: base(input)
		{
			this.expecting = expecting;
		}

		public override string ToString()
		{
			return "MismatchedTokenException(" + UnexpectedType + "!=" + expecting + ")";
		}
	}
}
