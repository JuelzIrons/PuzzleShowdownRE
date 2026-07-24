namespace Unity.VisualScripting.Antlr3.Runtime
{
	public class RecognizerSharedState
	{
		public global::Unity.VisualScripting.Antlr3.Runtime.BitSet[] following = new global::Unity.VisualScripting.Antlr3.Runtime.BitSet[100];

		public int followingStackPointer = -1;

		public bool errorRecovery;

		public int lastErrorIndex = -1;

		public bool failed;

		public int syntaxErrors;

		public int backtracking;

		public global::System.Collections.IDictionary[] ruleMemo;

		public global::Unity.VisualScripting.Antlr3.Runtime.IToken token;

		public int tokenStartCharIndex = -1;

		public int tokenStartLine;

		public int tokenStartCharPositionInLine;

		public int channel;

		public int type;

		public string text;
	}
}
