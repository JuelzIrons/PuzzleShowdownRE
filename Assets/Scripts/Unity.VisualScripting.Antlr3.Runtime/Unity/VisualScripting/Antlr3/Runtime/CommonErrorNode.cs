namespace Unity.VisualScripting.Antlr3.Runtime
{
	[global::System.Serializable]
	public class CommonErrorNode : global::Unity.VisualScripting.Antlr3.Runtime.Tree.CommonTree
	{
		public global::Unity.VisualScripting.Antlr3.Runtime.IIntStream input;

		public global::Unity.VisualScripting.Antlr3.Runtime.IToken start;

		public global::Unity.VisualScripting.Antlr3.Runtime.IToken stop;

		[global::System.NonSerialized]
		public global::Unity.VisualScripting.Antlr3.Runtime.RecognitionException trappedException;

		public override bool IsNil => false;

		public override int Type => 0;

		public override string Text
		{
			get
			{
				string text = null;
				if (start != null)
				{
					int tokenIndex = start.TokenIndex;
					int num = stop.TokenIndex;
					if (stop.Type == global::Unity.VisualScripting.Antlr3.Runtime.Token.EOF)
					{
						num = ((global::Unity.VisualScripting.Antlr3.Runtime.ITokenStream)input).Count;
					}
					return ((global::Unity.VisualScripting.Antlr3.Runtime.ITokenStream)input).ToString(tokenIndex, num);
				}
				if (start is global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITree)
				{
					return ((global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITreeNodeStream)input).ToString(start, stop);
				}
				return "<unknown>";
			}
		}

		public CommonErrorNode(global::Unity.VisualScripting.Antlr3.Runtime.ITokenStream input, global::Unity.VisualScripting.Antlr3.Runtime.IToken start, global::Unity.VisualScripting.Antlr3.Runtime.IToken stop, global::Unity.VisualScripting.Antlr3.Runtime.RecognitionException e)
		{
			if (stop == null || (stop.TokenIndex < start.TokenIndex && stop.Type != global::Unity.VisualScripting.Antlr3.Runtime.Token.EOF))
			{
				stop = start;
			}
			this.input = input;
			this.start = start;
			this.stop = stop;
			trappedException = e;
		}

		public override string ToString()
		{
			if (trappedException is global::Unity.VisualScripting.Antlr3.Runtime.MissingTokenException)
			{
				return "<missing type: " + ((global::Unity.VisualScripting.Antlr3.Runtime.MissingTokenException)trappedException).MissingType + ">";
			}
			if (trappedException is global::Unity.VisualScripting.Antlr3.Runtime.UnwantedTokenException)
			{
				return string.Concat("<extraneous: ", ((global::Unity.VisualScripting.Antlr3.Runtime.UnwantedTokenException)trappedException).UnexpectedToken, ", resync=", Text, ">");
			}
			if (trappedException is global::Unity.VisualScripting.Antlr3.Runtime.MismatchedTokenException)
			{
				return string.Concat("<mismatched token: ", trappedException.Token, ", resync=", Text, ">");
			}
			if (trappedException is global::Unity.VisualScripting.Antlr3.Runtime.NoViableAltException)
			{
				return string.Concat("<unexpected: ", trappedException.Token, ", resync=", Text, ">");
			}
			return "<error: " + Text + ">";
		}
	}
}
