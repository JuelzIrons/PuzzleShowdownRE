namespace Unity.VisualScripting.Antlr3.Runtime
{
	[global::System.Serializable]
	public class MismatchedNotSetException : global::Unity.VisualScripting.Antlr3.Runtime.MismatchedSetException
	{
		public MismatchedNotSetException()
		{
		}

		public MismatchedNotSetException(global::Unity.VisualScripting.Antlr3.Runtime.BitSet expecting, global::Unity.VisualScripting.Antlr3.Runtime.IIntStream input)
			: base(expecting, input)
		{
		}

		public override string ToString()
		{
			return string.Concat("MismatchedNotSetException(", UnexpectedType, "!=", expecting, ")");
		}
	}
}
