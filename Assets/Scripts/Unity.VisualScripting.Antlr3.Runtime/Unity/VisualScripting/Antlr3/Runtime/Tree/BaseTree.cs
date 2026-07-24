namespace Unity.VisualScripting.Antlr3.Runtime.Tree
{
	[global::System.Serializable]
	public abstract class BaseTree : global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITree
	{
		protected global::System.Collections.IList children;

		public virtual int ChildCount
		{
			get
			{
				if (children == null)
				{
					return 0;
				}
				return children.Count;
			}
		}

		public virtual bool IsNil => false;

		public virtual int Line => 0;

		public virtual int CharPositionInLine => 0;

		public global::System.Collections.IList Children => children;

		public virtual int ChildIndex
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		public virtual global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITree Parent
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public abstract int Type { get; }

		public abstract int TokenStartIndex { get; set; }

		public abstract int TokenStopIndex { get; set; }

		public abstract string Text { get; }

		public BaseTree()
		{
		}

		public BaseTree(global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITree node)
		{
		}

		public virtual global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITree GetChild(int i)
		{
			if (children == null || i >= children.Count)
			{
				return null;
			}
			return (global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITree)children[i];
		}

		public virtual void AddChild(global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITree t)
		{
			if (t == null)
			{
				return;
			}
			global::Unity.VisualScripting.Antlr3.Runtime.Tree.BaseTree baseTree = (global::Unity.VisualScripting.Antlr3.Runtime.Tree.BaseTree)t;
			if (baseTree.IsNil)
			{
				if (children != null && children == baseTree.children)
				{
					throw new global::System.InvalidOperationException("attempt to add child list to itself");
				}
				if (baseTree.children == null)
				{
					return;
				}
				if (children != null)
				{
					int count = baseTree.children.Count;
					for (int i = 0; i < count; i++)
					{
						global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITree tree = (global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITree)baseTree.Children[i];
						children.Add(tree);
						tree.Parent = this;
						tree.ChildIndex = children.Count - 1;
					}
				}
				else
				{
					children = baseTree.children;
					FreshenParentAndChildIndexes();
				}
			}
			else
			{
				if (children == null)
				{
					children = CreateChildrenList();
				}
				children.Add(t);
				baseTree.Parent = this;
				baseTree.ChildIndex = children.Count - 1;
			}
		}

		public void AddChildren(global::System.Collections.IList kids)
		{
			for (int i = 0; i < kids.Count; i++)
			{
				global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITree t = (global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITree)kids[i];
				AddChild(t);
			}
		}

		public virtual void SetChild(int i, global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITree t)
		{
			if (t != null)
			{
				if (t.IsNil)
				{
					throw new global::System.ArgumentException("Can't set single child to a list");
				}
				if (children == null)
				{
					children = CreateChildrenList();
				}
				children[i] = t;
				t.Parent = this;
				t.ChildIndex = i;
			}
		}

		public virtual object DeleteChild(int i)
		{
			if (children == null)
			{
				return null;
			}
			global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITree result = (global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITree)children[i];
			children.RemoveAt(i);
			FreshenParentAndChildIndexes(i);
			return result;
		}

		public virtual void ReplaceChildren(int startChildIndex, int stopChildIndex, object t)
		{
			if (children == null)
			{
				throw new global::System.ArgumentException("indexes invalid; no children in list");
			}
			int num = stopChildIndex - startChildIndex + 1;
			global::Unity.VisualScripting.Antlr3.Runtime.Tree.BaseTree baseTree = (global::Unity.VisualScripting.Antlr3.Runtime.Tree.BaseTree)t;
			global::System.Collections.IList list;
			if (baseTree.IsNil)
			{
				list = baseTree.Children;
			}
			else
			{
				list = new global::System.Collections.Generic.List<object>(1);
				list.Add(baseTree);
			}
			int count = list.Count;
			int count2 = list.Count;
			int num2 = num - count;
			if (num2 == 0)
			{
				int num3 = 0;
				for (int i = startChildIndex; i <= stopChildIndex; i++)
				{
					global::Unity.VisualScripting.Antlr3.Runtime.Tree.BaseTree baseTree2 = (global::Unity.VisualScripting.Antlr3.Runtime.Tree.BaseTree)list[num3];
					children[i] = baseTree2;
					baseTree2.Parent = this;
					baseTree2.ChildIndex = i;
					num3++;
				}
			}
			else if (num2 > 0)
			{
				for (int j = 0; j < count2; j++)
				{
					children[startChildIndex + j] = list[j];
				}
				int num4 = startChildIndex + count2;
				for (int k = num4; k <= stopChildIndex; k++)
				{
					children.RemoveAt(num4);
				}
				FreshenParentAndChildIndexes(startChildIndex);
			}
			else
			{
				int l;
				for (l = 0; l < num; l++)
				{
					children[startChildIndex + l] = list[l];
				}
				for (; l < count; l++)
				{
					children.Insert(startChildIndex + l, list[l]);
				}
				FreshenParentAndChildIndexes(startChildIndex);
			}
		}

		protected internal virtual global::System.Collections.IList CreateChildrenList()
		{
			return new global::System.Collections.Generic.List<object>();
		}

		public virtual void FreshenParentAndChildIndexes()
		{
			FreshenParentAndChildIndexes(0);
		}

		public virtual void FreshenParentAndChildIndexes(int offset)
		{
			int childCount = ChildCount;
			for (int i = offset; i < childCount; i++)
			{
				global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITree child = GetChild(i);
				child.ChildIndex = i;
				child.Parent = this;
			}
		}

		public virtual void SanityCheckParentAndChildIndexes()
		{
			SanityCheckParentAndChildIndexes(null, -1);
		}

		public virtual void SanityCheckParentAndChildIndexes(global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITree parent, int i)
		{
			if (parent != Parent)
			{
				throw new global::System.ArgumentException(string.Concat("parents don't match; expected ", parent, " found ", Parent));
			}
			if (i != ChildIndex)
			{
				throw new global::System.NotSupportedException("child indexes don't match; expected " + i + " found " + ChildIndex);
			}
			int childCount = ChildCount;
			for (int j = 0; j < childCount; j++)
			{
				global::Unity.VisualScripting.Antlr3.Runtime.Tree.CommonTree commonTree = (global::Unity.VisualScripting.Antlr3.Runtime.Tree.CommonTree)GetChild(j);
				commonTree.SanityCheckParentAndChildIndexes(this, j);
			}
		}

		public bool HasAncestor(int ttype)
		{
			return GetAncestor(ttype) != null;
		}

		public global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITree GetAncestor(int ttype)
		{
			global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITree tree = this;
			for (tree = tree.Parent; tree != null; tree = tree.Parent)
			{
				if (tree.Type == ttype)
				{
					return tree;
				}
			}
			return null;
		}

		public global::System.Collections.IList GetAncestors()
		{
			if (Parent == null)
			{
				return null;
			}
			global::System.Collections.IList list = new global::System.Collections.Generic.List<object>();
			global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITree tree = this;
			for (tree = tree.Parent; tree != null; tree = tree.Parent)
			{
				list.Insert(0, tree);
			}
			return list;
		}

		public virtual string ToStringTree()
		{
			if (children == null || children.Count == 0)
			{
				return ToString();
			}
			global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder();
			if (!IsNil)
			{
				stringBuilder.Append("(");
				stringBuilder.Append(ToString());
				stringBuilder.Append(' ');
			}
			int num = 0;
			while (children != null && num < children.Count)
			{
				global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITree tree = (global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITree)children[num];
				if (num > 0)
				{
					stringBuilder.Append(' ');
				}
				stringBuilder.Append(tree.ToStringTree());
				num++;
			}
			if (!IsNil)
			{
				stringBuilder.Append(")");
			}
			return stringBuilder.ToString();
		}

		public abstract override string ToString();

		public abstract global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITree DupNode();
	}
}
