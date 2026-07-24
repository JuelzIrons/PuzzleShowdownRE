namespace Unity.VisualScripting.Antlr3.Runtime.Tree
{
	public class ParseTree : global::Unity.VisualScripting.Antlr3.Runtime.Tree.BaseTree
	{
		public object payload;

		public global::System.Collections.IList hiddenTokens;

		public override int Type => 0;

		public override string Text => ToString();

		public override int TokenStartIndex
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		public override int TokenStopIndex
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		public ParseTree(object label)
		{
			payload = label;
		}

		public override global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITree DupNode()
		{
			return null;
		}

		public override string ToString()
		{
			if (payload is global::Unity.VisualScripting.Antlr3.Runtime.IToken)
			{
				global::Unity.VisualScripting.Antlr3.Runtime.IToken token = (global::Unity.VisualScripting.Antlr3.Runtime.IToken)payload;
				if (token.Type == global::Unity.VisualScripting.Antlr3.Runtime.Token.EOF)
				{
					return "<EOF>";
				}
				return token.Text;
			}
			return payload.ToString();
		}

		public string ToStringWithHiddenTokens()
		{
			global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder();
			if (hiddenTokens != null)
			{
				for (int i = 0; i < hiddenTokens.Count; i++)
				{
					global::Unity.VisualScripting.Antlr3.Runtime.IToken token = (global::Unity.VisualScripting.Antlr3.Runtime.IToken)hiddenTokens[i];
					stringBuilder.Append(token.Text);
				}
			}
			string text = ToString();
			if (text != "<EOF>")
			{
				stringBuilder.Append(text);
			}
			return stringBuilder.ToString();
		}

		public string ToInputString()
		{
			global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder();
			_ToStringLeaves(stringBuilder);
			return stringBuilder.ToString();
		}

		public void _ToStringLeaves(global::System.Text.StringBuilder buf)
		{
			if (payload is global::Unity.VisualScripting.Antlr3.Runtime.IToken)
			{
				buf.Append(ToStringWithHiddenTokens());
				return;
			}
			int num = 0;
			while (children != null && num < children.Count)
			{
				global::Unity.VisualScripting.Antlr3.Runtime.Tree.ParseTree parseTree = (global::Unity.VisualScripting.Antlr3.Runtime.Tree.ParseTree)children[num];
				parseTree._ToStringLeaves(buf);
				num++;
			}
		}
	}
}
