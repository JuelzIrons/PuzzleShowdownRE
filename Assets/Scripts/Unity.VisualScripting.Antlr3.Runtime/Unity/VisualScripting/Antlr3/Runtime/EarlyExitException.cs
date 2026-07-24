namespace Unity.VisualScripting.Antlr3.Runtime
{
	[global::System.Serializable]
	public class EarlyExitException : global::Unity.VisualScripting.Antlr3.Runtime.RecognitionException
	{
		public int decisionNumber;

		public EarlyExitException()
		{
		}

		public EarlyExitException(int decisionNumber, global::Unity.VisualScripting.Antlr3.Runtime.IIntStream input)
			: base(input)
		{
			this.decisionNumber = decisionNumber;
		}
	}
}
