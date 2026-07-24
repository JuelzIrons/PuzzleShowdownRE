namespace Unity.VisualScripting.Antlr3.Runtime
{
	[global::System.Serializable]
	public class MismatchedSetException : global::Unity.VisualScripting.Antlr3.Runtime.RecognitionException
	{
		public global::Unity.VisualScripting.Antlr3.Runtime.BitSet expecting;

		public MismatchedSetException()
		{
		}

		public MismatchedSetException(global::Unity.VisualScripting.Antlr3.Runtime.BitSet expecting, global::Unity.VisualScripting.Antlr3.Runtime.IIntStream input)
			: base(input)
		{
			this.expecting = expecting;
		}

		public override string ToString()
		{
			return string.Concat("MismatchedSetException(", UnexpectedType, "!=", expecting, ")");
		}
	}
}
