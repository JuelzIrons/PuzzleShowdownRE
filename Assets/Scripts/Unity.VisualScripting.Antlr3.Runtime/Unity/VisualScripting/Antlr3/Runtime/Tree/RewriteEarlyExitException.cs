namespace Unity.VisualScripting.Antlr3.Runtime.Tree
{
	[global::System.Serializable]
	public class RewriteEarlyExitException : global::Unity.VisualScripting.Antlr3.Runtime.Tree.RewriteCardinalityException
	{
		public RewriteEarlyExitException()
			: base(null)
		{
		}

		public RewriteEarlyExitException(string elementDescription)
			: base(elementDescription)
		{
		}
	}
}
