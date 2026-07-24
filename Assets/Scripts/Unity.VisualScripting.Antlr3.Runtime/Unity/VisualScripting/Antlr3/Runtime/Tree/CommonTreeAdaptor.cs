namespace Unity.VisualScripting.Antlr3.Runtime.Tree
{
	public class CommonTreeAdaptor : global::Unity.VisualScripting.Antlr3.Runtime.Tree.BaseTreeAdaptor
	{
		public override object DupNode(object t)
		{
			if (t == null)
			{
				return null;
			}
			return ((global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITree)t).DupNode();
		}

		public override object Create(global::Unity.VisualScripting.Antlr3.Runtime.IToken payload)
		{
			return new global::Unity.VisualScripting.Antlr3.Runtime.Tree.CommonTree(payload);
		}

		public override global::Unity.VisualScripting.Antlr3.Runtime.IToken CreateToken(int tokenType, string text)
		{
			return new global::Unity.VisualScripting.Antlr3.Runtime.CommonToken(tokenType, text);
		}

		public override global::Unity.VisualScripting.Antlr3.Runtime.IToken CreateToken(global::Unity.VisualScripting.Antlr3.Runtime.IToken fromToken)
		{
			return new global::Unity.VisualScripting.Antlr3.Runtime.CommonToken(fromToken);
		}

		public override void SetTokenBoundaries(object t, global::Unity.VisualScripting.Antlr3.Runtime.IToken startToken, global::Unity.VisualScripting.Antlr3.Runtime.IToken stopToken)
		{
			if (t != null)
			{
				int tokenStartIndex = 0;
				int tokenStopIndex = 0;
				if (startToken != null)
				{
					tokenStartIndex = startToken.TokenIndex;
				}
				if (stopToken != null)
				{
					tokenStopIndex = stopToken.TokenIndex;
				}
				((global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITree)t).TokenStartIndex = tokenStartIndex;
				((global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITree)t).TokenStopIndex = tokenStopIndex;
			}
		}

		public override int GetTokenStartIndex(object t)
		{
			if (t == null)
			{
				return -1;
			}
			return ((global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITree)t).TokenStartIndex;
		}

		public override int GetTokenStopIndex(object t)
		{
			if (t == null)
			{
				return -1;
			}
			return ((global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITree)t).TokenStopIndex;
		}

		public override string GetNodeText(object t)
		{
			if (t == null)
			{
				return null;
			}
			return ((global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITree)t).Text;
		}

		public override int GetNodeType(object t)
		{
			if (t == null)
			{
				return 0;
			}
			return ((global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITree)t).Type;
		}

		public override global::Unity.VisualScripting.Antlr3.Runtime.IToken GetToken(object treeNode)
		{
			if (treeNode is global::Unity.VisualScripting.Antlr3.Runtime.Tree.CommonTree)
			{
				return ((global::Unity.VisualScripting.Antlr3.Runtime.Tree.CommonTree)treeNode).Token;
			}
			return null;
		}

		public override object GetChild(object t, int i)
		{
			if (t == null)
			{
				return null;
			}
			return ((global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITree)t).GetChild(i);
		}

		public override int GetChildCount(object t)
		{
			if (t == null)
			{
				return 0;
			}
			return ((global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITree)t).ChildCount;
		}

		public override object GetParent(object t)
		{
			if (t == null)
			{
				return null;
			}
			return ((global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITree)t).Parent;
		}

		public override void SetParent(object t, object parent)
		{
			if (t == null)
			{
				((global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITree)t).Parent = (global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITree)parent;
			}
		}

		public override int GetChildIndex(object t)
		{
			if (t == null)
			{
				return 0;
			}
			return ((global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITree)t).ChildIndex;
		}

		public override void SetChildIndex(object t, int index)
		{
			if (t == null)
			{
				((global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITree)t).ChildIndex = index;
			}
		}

		public override void ReplaceChildren(object parent, int startChildIndex, int stopChildIndex, object t)
		{
			if (parent != null)
			{
				((global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITree)parent).ReplaceChildren(startChildIndex, stopChildIndex, t);
			}
		}
	}
}
