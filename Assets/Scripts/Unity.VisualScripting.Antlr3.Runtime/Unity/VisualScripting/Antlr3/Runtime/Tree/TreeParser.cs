namespace Unity.VisualScripting.Antlr3.Runtime.Tree
{
	public class TreeParser : global::Unity.VisualScripting.Antlr3.Runtime.BaseRecognizer
	{
		public const int DOWN = 2;

		public const int UP = 3;

		private static readonly string dotdot = ".*[^.]\\.\\.[^.].*";

		private static readonly string doubleEtc = ".*\\.\\.\\.\\s+\\.\\.\\..*";

		private static readonly string spaces = "\\s+";

		private static readonly global::System.Text.RegularExpressions.Regex dotdotPattern = new global::System.Text.RegularExpressions.Regex(dotdot, global::System.Text.RegularExpressions.RegexOptions.Compiled);

		private static readonly global::System.Text.RegularExpressions.Regex doubleEtcPattern = new global::System.Text.RegularExpressions.Regex(doubleEtc, global::System.Text.RegularExpressions.RegexOptions.Compiled);

		private static readonly global::System.Text.RegularExpressions.Regex spacesPattern = new global::System.Text.RegularExpressions.Regex(spaces, global::System.Text.RegularExpressions.RegexOptions.Compiled);

		protected internal global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITreeNodeStream input;

		public virtual global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITreeNodeStream TreeNodeStream
		{
			get
			{
				return input;
			}
			set
			{
				input = value;
			}
		}

		public override string SourceName => input.SourceName;

		public override global::Unity.VisualScripting.Antlr3.Runtime.IIntStream Input => input;

		public TreeParser(global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITreeNodeStream input)
		{
			TreeNodeStream = input;
		}

		public TreeParser(global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITreeNodeStream input, global::Unity.VisualScripting.Antlr3.Runtime.RecognizerSharedState state)
			: base(state)
		{
			TreeNodeStream = input;
		}

		protected override object GetCurrentInputSymbol(global::Unity.VisualScripting.Antlr3.Runtime.IIntStream input)
		{
			return ((global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITreeNodeStream)input).LT(1);
		}

		protected override object GetMissingSymbol(global::Unity.VisualScripting.Antlr3.Runtime.IIntStream input, global::Unity.VisualScripting.Antlr3.Runtime.RecognitionException e, int expectedTokenType, global::Unity.VisualScripting.Antlr3.Runtime.BitSet follow)
		{
			string text = "<missing " + TokenNames[expectedTokenType] + ">";
			return new global::Unity.VisualScripting.Antlr3.Runtime.Tree.CommonTree(new global::Unity.VisualScripting.Antlr3.Runtime.CommonToken(expectedTokenType, text));
		}

		public override void Reset()
		{
			base.Reset();
			if (input != null)
			{
				input.Seek(0);
			}
		}

		public override void MatchAny(global::Unity.VisualScripting.Antlr3.Runtime.IIntStream ignore)
		{
			state.errorRecovery = false;
			state.failed = false;
			object t = input.LT(1);
			if (input.TreeAdaptor.GetChildCount(t) == 0)
			{
				input.Consume();
				return;
			}
			int num = 0;
			int nodeType = input.TreeAdaptor.GetNodeType(t);
			while (nodeType != global::Unity.VisualScripting.Antlr3.Runtime.Token.EOF && (nodeType != 3 || num != 0))
			{
				input.Consume();
				t = input.LT(1);
				nodeType = input.TreeAdaptor.GetNodeType(t);
				switch (nodeType)
				{
				case 2:
					num++;
					break;
				case 3:
					num--;
					break;
				}
			}
			input.Consume();
		}

		protected internal override object RecoverFromMismatchedToken(global::Unity.VisualScripting.Antlr3.Runtime.IIntStream input, int ttype, global::Unity.VisualScripting.Antlr3.Runtime.BitSet follow)
		{
			throw new global::Unity.VisualScripting.Antlr3.Runtime.MismatchedTreeNodeException(ttype, (global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITreeNodeStream)input);
		}

		public override string GetErrorHeader(global::Unity.VisualScripting.Antlr3.Runtime.RecognitionException e)
		{
			return GrammarFileName + ": node from " + (e.approximateLineInfo ? "after " : "") + "line " + e.Line + ":" + e.CharPositionInLine;
		}

		public override string GetErrorMessage(global::Unity.VisualScripting.Antlr3.Runtime.RecognitionException e, string[] tokenNames)
		{
			if (this != null)
			{
				global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITreeAdaptor treeAdaptor = ((global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITreeNodeStream)e.Input).TreeAdaptor;
				e.Token = treeAdaptor.GetToken(e.Node);
				if (e.Token == null)
				{
					e.Token = new global::Unity.VisualScripting.Antlr3.Runtime.CommonToken(treeAdaptor.GetNodeType(e.Node), treeAdaptor.GetNodeText(e.Node));
				}
			}
			return base.GetErrorMessage(e, tokenNames);
		}

		public virtual void TraceIn(string ruleName, int ruleIndex)
		{
			base.TraceIn(ruleName, ruleIndex, input.LT(1));
		}

		public virtual void TraceOut(string ruleName, int ruleIndex)
		{
			base.TraceOut(ruleName, ruleIndex, input.LT(1));
		}
	}
}
