namespace Unity.VisualScripting.Antlr3.Runtime.Tree
{
	public abstract class BaseTreeAdaptor : global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITreeAdaptor
	{
		protected global::System.Collections.IDictionary treeToUniqueIDMap;

		protected int uniqueNodeID = 1;

		public virtual object GetNilNode()
		{
			return Create(null);
		}

		public virtual object ErrorNode(global::Unity.VisualScripting.Antlr3.Runtime.ITokenStream input, global::Unity.VisualScripting.Antlr3.Runtime.IToken start, global::Unity.VisualScripting.Antlr3.Runtime.IToken stop, global::Unity.VisualScripting.Antlr3.Runtime.RecognitionException e)
		{
			return new global::Unity.VisualScripting.Antlr3.Runtime.CommonErrorNode(input, start, stop, e);
		}

		public virtual bool IsNil(object tree)
		{
			return ((global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITree)tree).IsNil;
		}

		public virtual object DupTree(object tree)
		{
			return DupTree(tree, null);
		}

		public virtual object DupTree(object t, object parent)
		{
			if (t == null)
			{
				return null;
			}
			object obj = DupNode(t);
			SetChildIndex(obj, GetChildIndex(t));
			SetParent(obj, parent);
			int childCount = GetChildCount(t);
			for (int i = 0; i < childCount; i++)
			{
				object child = GetChild(t, i);
				object child2 = DupTree(child, t);
				AddChild(obj, child2);
			}
			return obj;
		}

		public virtual void AddChild(object t, object child)
		{
			if (t != null && child != null)
			{
				((global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITree)t).AddChild((global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITree)child);
			}
		}

		public virtual object BecomeRoot(object newRoot, object oldRoot)
		{
			global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITree tree = (global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITree)newRoot;
			global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITree t = (global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITree)oldRoot;
			if (oldRoot == null)
			{
				return newRoot;
			}
			if (tree.IsNil)
			{
				int childCount = tree.ChildCount;
				if (childCount == 1)
				{
					tree = tree.GetChild(0);
				}
				else if (childCount > 1)
				{
					throw new global::System.SystemException("more than one node as root (TODO: make exception hierarchy)");
				}
			}
			tree.AddChild(t);
			return tree;
		}

		public virtual object RulePostProcessing(object root)
		{
			global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITree tree = (global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITree)root;
			if (tree != null && tree.IsNil)
			{
				if (tree.ChildCount == 0)
				{
					tree = null;
				}
				else if (tree.ChildCount == 1)
				{
					tree = tree.GetChild(0);
					tree.Parent = null;
					tree.ChildIndex = -1;
				}
			}
			return tree;
		}

		public virtual object BecomeRoot(global::Unity.VisualScripting.Antlr3.Runtime.IToken newRoot, object oldRoot)
		{
			return BecomeRoot(Create(newRoot), oldRoot);
		}

		public virtual object Create(int tokenType, global::Unity.VisualScripting.Antlr3.Runtime.IToken fromToken)
		{
			fromToken = CreateToken(fromToken);
			fromToken.Type = tokenType;
			return (global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITree)Create(fromToken);
		}

		public virtual object Create(int tokenType, global::Unity.VisualScripting.Antlr3.Runtime.IToken fromToken, string text)
		{
			fromToken = CreateToken(fromToken);
			fromToken.Type = tokenType;
			fromToken.Text = text;
			return (global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITree)Create(fromToken);
		}

		public virtual object Create(int tokenType, string text)
		{
			global::Unity.VisualScripting.Antlr3.Runtime.IToken param = CreateToken(tokenType, text);
			return (global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITree)Create(param);
		}

		public virtual int GetNodeType(object t)
		{
			return ((global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITree)t).Type;
		}

		public virtual void SetNodeType(object t, int type)
		{
			throw new global::System.NotImplementedException("don't know enough about Tree node");
		}

		public virtual string GetNodeText(object t)
		{
			return ((global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITree)t).Text;
		}

		public virtual void SetNodeText(object t, string text)
		{
			throw new global::System.NotImplementedException("don't know enough about Tree node");
		}

		public virtual object GetChild(object t, int i)
		{
			return ((global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITree)t).GetChild(i);
		}

		public virtual void SetChild(object t, int i, object child)
		{
			((global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITree)t).SetChild(i, (global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITree)child);
		}

		public virtual object DeleteChild(object t, int i)
		{
			return ((global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITree)t).DeleteChild(i);
		}

		public virtual int GetChildCount(object t)
		{
			return ((global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITree)t).ChildCount;
		}

		public abstract object DupNode(object param1);

		public abstract object Create(global::Unity.VisualScripting.Antlr3.Runtime.IToken param1);

		public abstract void SetTokenBoundaries(object param1, global::Unity.VisualScripting.Antlr3.Runtime.IToken param2, global::Unity.VisualScripting.Antlr3.Runtime.IToken param3);

		public abstract int GetTokenStartIndex(object t);

		public abstract int GetTokenStopIndex(object t);

		public abstract global::Unity.VisualScripting.Antlr3.Runtime.IToken GetToken(object treeNode);

		public int GetUniqueID(object node)
		{
			if (treeToUniqueIDMap == null)
			{
				treeToUniqueIDMap = new global::System.Collections.Hashtable();
			}
			object obj = treeToUniqueIDMap[node];
			if (obj != null)
			{
				return (int)obj;
			}
			int num = uniqueNodeID;
			treeToUniqueIDMap[node] = num;
			uniqueNodeID++;
			return num;
		}

		public abstract global::Unity.VisualScripting.Antlr3.Runtime.IToken CreateToken(int tokenType, string text);

		public abstract global::Unity.VisualScripting.Antlr3.Runtime.IToken CreateToken(global::Unity.VisualScripting.Antlr3.Runtime.IToken fromToken);

		public abstract object GetParent(object t);

		public abstract void SetParent(object t, object parent);

		public abstract int GetChildIndex(object t);

		public abstract void SetChildIndex(object t, int index);

		public abstract void ReplaceChildren(object parent, int startChildIndex, int stopChildIndex, object t);
	}
}
