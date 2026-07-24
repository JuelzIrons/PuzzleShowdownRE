namespace Unity.VisualScripting.Antlr3.Runtime
{
	[global::System.Serializable]
	public class MismatchedTreeNodeException : global::Unity.VisualScripting.Antlr3.Runtime.RecognitionException
	{
		public int expecting;

		public MismatchedTreeNodeException()
		{
		}

		public MismatchedTreeNodeException(int expecting, global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITreeNodeStream input)
			: base(input)
		{
			this.expecting = expecting;
		}

		public override string ToString()
		{
			return "MismatchedTreeNodeException(" + UnexpectedType + "!=" + expecting + ")";
		}
	}
}
