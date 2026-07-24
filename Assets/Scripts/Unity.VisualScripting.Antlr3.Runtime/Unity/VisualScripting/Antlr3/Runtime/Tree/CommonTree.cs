namespace Unity.VisualScripting.Antlr3.Runtime.Tree
{
	[global::System.Serializable]
	public class CommonTree : global::Unity.VisualScripting.Antlr3.Runtime.Tree.BaseTree
	{
		public int startIndex = -1;

		public int stopIndex = -1;

		protected global::Unity.VisualScripting.Antlr3.Runtime.IToken token;

		public global::Unity.VisualScripting.Antlr3.Runtime.Tree.CommonTree parent;

		public int childIndex = -1;

		public virtual global::Unity.VisualScripting.Antlr3.Runtime.IToken Token => token;

		public override bool IsNil => token == null;

		public override int Type
		{
			get
			{
				if (token == null)
				{
					return 0;
				}
				return token.Type;
			}
		}

		public override string Text
		{
			get
			{
				if (token == null)
				{
					return null;
				}
				return token.Text;
			}
		}

		public override int Line
		{
			get
			{
				if (token == null || token.Line == 0)
				{
					if (ChildCount > 0)
					{
						return GetChild(0).Line;
					}
					return 0;
				}
				return token.Line;
			}
		}

		public override int CharPositionInLine
		{
			get
			{
				if (token == null || token.CharPositionInLine == -1)
				{
					if (ChildCount > 0)
					{
						return GetChild(0).CharPositionInLine;
					}
					return 0;
				}
				return token.CharPositionInLine;
			}
		}

		public override int TokenStartIndex
		{
			get
			{
				if (startIndex == -1 && token != null)
				{
					return token.TokenIndex;
				}
				return startIndex;
			}
			set
			{
				startIndex = value;
			}
		}

		public override int TokenStopIndex
		{
			get
			{
				if (stopIndex == -1 && token != null)
				{
					return token.TokenIndex;
				}
				return stopIndex;
			}
			set
			{
				stopIndex = value;
			}
		}

		public override int ChildIndex
		{
			get
			{
				return childIndex;
			}
			set
			{
				childIndex = value;
			}
		}

		public override global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITree Parent
		{
			get
			{
				return parent;
			}
			set
			{
				parent = (global::Unity.VisualScripting.Antlr3.Runtime.Tree.CommonTree)value;
			}
		}

		public CommonTree()
		{
		}

		public CommonTree(global::Unity.VisualScripting.Antlr3.Runtime.Tree.CommonTree node)
			: base(node)
		{
			token = node.token;
			startIndex = node.startIndex;
			stopIndex = node.stopIndex;
		}

		public CommonTree(global::Unity.VisualScripting.Antlr3.Runtime.IToken t)
		{
			token = t;
		}

		public void SetUnknownTokenBoundaries()
		{
			if (children == null)
			{
				if (startIndex < 0 || stopIndex < 0)
				{
					startIndex = (stopIndex = token.TokenIndex);
				}
				return;
			}
			for (int i = 0; i < children.Count; i++)
			{
				((global::Unity.VisualScripting.Antlr3.Runtime.Tree.CommonTree)children[i]).SetUnknownTokenBoundaries();
			}
			if ((startIndex < 0 || stopIndex < 0) && children.Count > 0)
			{
				global::Unity.VisualScripting.Antlr3.Runtime.Tree.CommonTree commonTree = (global::Unity.VisualScripting.Antlr3.Runtime.Tree.CommonTree)children[0];
				global::Unity.VisualScripting.Antlr3.Runtime.Tree.CommonTree commonTree2 = (global::Unity.VisualScripting.Antlr3.Runtime.Tree.CommonTree)children[children.Count - 1];
				startIndex = commonTree.TokenStartIndex;
				stopIndex = commonTree2.TokenStopIndex;
			}
		}

		public override global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITree DupNode()
		{
			return new global::Unity.VisualScripting.Antlr3.Runtime.Tree.CommonTree(this);
		}

		public override string ToString()
		{
			if (IsNil)
			{
				return "nil";
			}
			if (Type == 0)
			{
				return "<errornode>";
			}
			if (token == null)
			{
				return null;
			}
			return token.Text;
		}
	}
}
