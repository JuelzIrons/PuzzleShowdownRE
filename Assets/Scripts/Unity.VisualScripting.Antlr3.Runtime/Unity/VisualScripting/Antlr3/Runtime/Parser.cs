namespace Unity.VisualScripting.Antlr3.Runtime
{
	public class Parser : global::Unity.VisualScripting.Antlr3.Runtime.BaseRecognizer
	{
		protected internal global::Unity.VisualScripting.Antlr3.Runtime.ITokenStream input;

		public virtual global::Unity.VisualScripting.Antlr3.Runtime.ITokenStream TokenStream
		{
			get
			{
				return input;
			}
			set
			{
				input = null;
				Reset();
				input = value;
			}
		}

		public override string SourceName => input.SourceName;

		public override global::Unity.VisualScripting.Antlr3.Runtime.IIntStream Input => input;

		public Parser(global::Unity.VisualScripting.Antlr3.Runtime.ITokenStream input)
		{
			TokenStream = input;
		}

		public Parser(global::Unity.VisualScripting.Antlr3.Runtime.ITokenStream input, global::Unity.VisualScripting.Antlr3.Runtime.RecognizerSharedState state)
			: base(state)
		{
			TokenStream = input;
		}

		public override void Reset()
		{
			base.Reset();
			if (input != null)
			{
				input.Seek(0);
			}
		}

		protected override object GetCurrentInputSymbol(global::Unity.VisualScripting.Antlr3.Runtime.IIntStream input)
		{
			return ((global::Unity.VisualScripting.Antlr3.Runtime.ITokenStream)input).LT(1);
		}

		protected override object GetMissingSymbol(global::Unity.VisualScripting.Antlr3.Runtime.IIntStream input, global::Unity.VisualScripting.Antlr3.Runtime.RecognitionException e, int expectedTokenType, global::Unity.VisualScripting.Antlr3.Runtime.BitSet follow)
		{
			string text = null;
			text = ((expectedTokenType != global::Unity.VisualScripting.Antlr3.Runtime.Token.EOF) ? ("<missing " + TokenNames[expectedTokenType] + ">") : "<missing EOF>");
			global::Unity.VisualScripting.Antlr3.Runtime.CommonToken commonToken = new global::Unity.VisualScripting.Antlr3.Runtime.CommonToken(expectedTokenType, text);
			global::Unity.VisualScripting.Antlr3.Runtime.IToken token = ((global::Unity.VisualScripting.Antlr3.Runtime.ITokenStream)input).LT(1);
			if (token.Type == global::Unity.VisualScripting.Antlr3.Runtime.Token.EOF)
			{
				token = ((global::Unity.VisualScripting.Antlr3.Runtime.ITokenStream)input).LT(-1);
			}
			commonToken.line = token.Line;
			commonToken.CharPositionInLine = token.CharPositionInLine;
			commonToken.Channel = 0;
			return commonToken;
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
