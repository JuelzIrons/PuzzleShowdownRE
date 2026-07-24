namespace Unity.VisualScripting.Antlr3.Runtime.Tree
{
	public class UnBufferedTreeNodeStream : global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITreeNodeStream, global::Unity.VisualScripting.Antlr3.Runtime.IIntStream
	{
		protected class TreeWalkState
		{
			protected internal int currentChildIndex;

			protected internal int absoluteNodeIndex;

			protected internal object currentNode;

			protected internal object previousNode;

			protected internal int nodeStackSize;

			protected internal int indexStackSize;

			protected internal object[] lookahead;
		}

		public const int INITIAL_LOOKAHEAD_BUFFER_SIZE = 5;

		private global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITree currentEnumerationNode;

		protected bool uniqueNavigationNodes;

		protected internal object root;

		protected global::Unity.VisualScripting.Antlr3.Runtime.ITokenStream tokens;

		private global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITreeAdaptor adaptor;

		protected internal global::Unity.VisualScripting.Antlr3.Runtime.Collections.StackList nodeStack = new global::Unity.VisualScripting.Antlr3.Runtime.Collections.StackList();

		protected internal global::Unity.VisualScripting.Antlr3.Runtime.Collections.StackList indexStack = new global::Unity.VisualScripting.Antlr3.Runtime.Collections.StackList();

		protected internal object currentNode;

		protected internal object previousNode;

		protected internal int currentChildIndex;

		protected int absoluteNodeIndex;

		protected internal object[] lookahead = new object[5];

		protected internal int head;

		protected internal int tail;

		protected global::System.Collections.IList markers;

		protected int markDepth;

		protected int lastMarker;

		protected object down;

		protected object up;

		protected object eof;

		public virtual object TreeSource => root;

		public virtual object Current => currentEnumerationNode;

		public virtual int Count
		{
			get
			{
				global::Unity.VisualScripting.Antlr3.Runtime.Tree.CommonTreeNodeStream commonTreeNodeStream = new global::Unity.VisualScripting.Antlr3.Runtime.Tree.CommonTreeNodeStream(root);
				return commonTreeNodeStream.Count;
			}
		}

		public global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITreeAdaptor TreeAdaptor => adaptor;

		public string SourceName => TokenStream.SourceName;

		public global::Unity.VisualScripting.Antlr3.Runtime.ITokenStream TokenStream
		{
			get
			{
				return tokens;
			}
			set
			{
				tokens = value;
			}
		}

		public bool HasUniqueNavigationNodes
		{
			get
			{
				return uniqueNavigationNodes;
			}
			set
			{
				uniqueNavigationNodes = value;
			}
		}

		protected int LookaheadSize
		{
			get
			{
				if (tail >= head)
				{
					return tail - head;
				}
				return lookahead.Length - head + tail;
			}
		}

		public virtual void Reset()
		{
			currentNode = root;
			previousNode = null;
			currentChildIndex = -1;
			absoluteNodeIndex = -1;
			head = (tail = 0);
		}

		public virtual bool MoveNext()
		{
			if (currentNode == null)
			{
				AddLookahead(eof);
				currentEnumerationNode = null;
				return false;
			}
			if (currentChildIndex == -1)
			{
				currentEnumerationNode = (global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITree)handleRootNode();
				return true;
			}
			if (currentChildIndex < adaptor.GetChildCount(currentNode))
			{
				currentEnumerationNode = (global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITree)VisitChild(currentChildIndex);
				return true;
			}
			WalkBackToMostRecentNodeWithUnvisitedChildren();
			if (currentNode != null)
			{
				currentEnumerationNode = (global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITree)VisitChild(currentChildIndex);
				return true;
			}
			return false;
		}

		public UnBufferedTreeNodeStream(object tree)
			: this(new global::Unity.VisualScripting.Antlr3.Runtime.Tree.CommonTreeAdaptor(), tree)
		{
		}

		public UnBufferedTreeNodeStream(global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITreeAdaptor adaptor, object tree)
		{
			root = tree;
			this.adaptor = adaptor;
			Reset();
			down = adaptor.Create(2, "DOWN");
			up = adaptor.Create(3, "UP");
			eof = adaptor.Create(global::Unity.VisualScripting.Antlr3.Runtime.Token.EOF, "EOF");
		}

		public virtual object Get(int i)
		{
			throw new global::System.NotSupportedException("stream is unbuffered");
		}

		public virtual object LT(int k)
		{
			if (k == -1)
			{
				return previousNode;
			}
			if (k < 0)
			{
				throw new global::System.ArgumentNullException("tree node streams cannot look backwards more than 1 node", "k");
			}
			if (k == 0)
			{
				return global::Unity.VisualScripting.Antlr3.Runtime.Tree.Tree.INVALID_NODE;
			}
			fill(k);
			return lookahead[(head + k - 1) % lookahead.Length];
		}

		protected internal virtual void fill(int k)
		{
			int lookaheadSize = LookaheadSize;
			for (int i = 1; i <= k - lookaheadSize; i++)
			{
				MoveNext();
			}
		}

		protected internal virtual void AddLookahead(object node)
		{
			lookahead[tail] = node;
			tail = (tail + 1) % lookahead.Length;
			if (tail == head)
			{
				object[] destinationArray = new object[2 * lookahead.Length];
				int num = lookahead.Length - head;
				global::System.Array.Copy(lookahead, head, destinationArray, 0, num);
				global::System.Array.Copy(lookahead, 0, destinationArray, num, tail);
				lookahead = destinationArray;
				head = 0;
				tail += num;
			}
		}

		public virtual void Consume()
		{
			fill(1);
			absoluteNodeIndex++;
			previousNode = lookahead[head];
			head = (head + 1) % lookahead.Length;
		}

		public virtual int LA(int i)
		{
			object obj = (global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITree)LT(i);
			if (obj == null)
			{
				return 0;
			}
			return adaptor.GetNodeType(obj);
		}

		public virtual int Mark()
		{
			if (markers == null)
			{
				markers = new global::System.Collections.Generic.List<object>();
				markers.Add(null);
			}
			markDepth++;
			global::Unity.VisualScripting.Antlr3.Runtime.Tree.UnBufferedTreeNodeStream.TreeWalkState treeWalkState = null;
			if (markDepth >= markers.Count)
			{
				treeWalkState = new global::Unity.VisualScripting.Antlr3.Runtime.Tree.UnBufferedTreeNodeStream.TreeWalkState();
				markers.Add(treeWalkState);
			}
			else
			{
				treeWalkState = (global::Unity.VisualScripting.Antlr3.Runtime.Tree.UnBufferedTreeNodeStream.TreeWalkState)markers[markDepth];
			}
			treeWalkState.absoluteNodeIndex = absoluteNodeIndex;
			treeWalkState.currentChildIndex = currentChildIndex;
			treeWalkState.currentNode = currentNode;
			treeWalkState.previousNode = previousNode;
			treeWalkState.nodeStackSize = nodeStack.Count;
			treeWalkState.indexStackSize = indexStack.Count;
			int lookaheadSize = LookaheadSize;
			int num = 0;
			treeWalkState.lookahead = new object[lookaheadSize];
			int num2 = 1;
			while (num2 <= lookaheadSize)
			{
				treeWalkState.lookahead[num] = LT(num2);
				num2++;
				num++;
			}
			lastMarker = markDepth;
			return markDepth;
		}

		public virtual void Release(int marker)
		{
			markDepth = marker;
			markDepth--;
		}

		public virtual void Rewind(int marker)
		{
			if (markers != null)
			{
				global::Unity.VisualScripting.Antlr3.Runtime.Tree.UnBufferedTreeNodeStream.TreeWalkState treeWalkState = (global::Unity.VisualScripting.Antlr3.Runtime.Tree.UnBufferedTreeNodeStream.TreeWalkState)markers[marker];
				absoluteNodeIndex = treeWalkState.absoluteNodeIndex;
				currentChildIndex = treeWalkState.currentChildIndex;
				currentNode = treeWalkState.currentNode;
				previousNode = treeWalkState.previousNode;
				nodeStack.Capacity = treeWalkState.nodeStackSize;
				indexStack.Capacity = treeWalkState.indexStackSize;
				for (head = (tail = 0); tail < treeWalkState.lookahead.Length; tail++)
				{
					lookahead[tail] = treeWalkState.lookahead[tail];
				}
				Release(marker);
			}
		}

		public void Rewind()
		{
			Rewind(lastMarker);
		}

		public virtual void Seek(int index)
		{
			if (index < Index())
			{
				throw new global::System.ArgumentOutOfRangeException("can't seek backwards in node stream", "index");
			}
			while (Index() < index)
			{
				Consume();
			}
		}

		public virtual int Index()
		{
			return absoluteNodeIndex + 1;
		}

		[global::System.Obsolete("Please use property Count instead.")]
		public virtual int Size()
		{
			return Count;
		}

		protected internal virtual object handleRootNode()
		{
			object obj = currentNode;
			currentChildIndex = 0;
			if (adaptor.IsNil(obj))
			{
				obj = VisitChild(currentChildIndex);
			}
			else
			{
				AddLookahead(obj);
				if (adaptor.GetChildCount(currentNode) == 0)
				{
					currentNode = null;
				}
			}
			return obj;
		}

		protected internal virtual object VisitChild(int child)
		{
			object obj = null;
			nodeStack.Push(currentNode);
			indexStack.Push(child);
			if (child == 0 && !adaptor.IsNil(currentNode))
			{
				AddNavigationNode(2);
			}
			currentNode = adaptor.GetChild(currentNode, child);
			currentChildIndex = 0;
			obj = currentNode;
			AddLookahead(obj);
			WalkBackToMostRecentNodeWithUnvisitedChildren();
			return obj;
		}

		protected internal virtual void AddNavigationNode(int ttype)
		{
			object obj = null;
			obj = ((ttype == 2) ? ((!HasUniqueNavigationNodes) ? down : adaptor.Create(2, "DOWN")) : ((!HasUniqueNavigationNodes) ? up : adaptor.Create(3, "UP")));
			AddLookahead(obj);
		}

		protected internal virtual void WalkBackToMostRecentNodeWithUnvisitedChildren()
		{
			while (currentNode != null && currentChildIndex >= adaptor.GetChildCount(currentNode))
			{
				currentNode = nodeStack.Pop();
				if (currentNode == null)
				{
					break;
				}
				currentChildIndex = (int)indexStack.Pop();
				currentChildIndex++;
				if (currentChildIndex >= adaptor.GetChildCount(currentNode))
				{
					if (!adaptor.IsNil(currentNode))
					{
						AddNavigationNode(3);
					}
					if (currentNode == root)
					{
						currentNode = null;
					}
				}
			}
		}

		public void ReplaceChildren(object parent, int startChildIndex, int stopChildIndex, object t)
		{
			throw new global::System.NotSupportedException("can't do stream rewrites yet");
		}

		public override string ToString()
		{
			return ToString(root, null);
		}

		public virtual string ToString(object start, object stop)
		{
			if (start == null)
			{
				return null;
			}
			if (tokens != null)
			{
				int tokenStartIndex = adaptor.GetTokenStartIndex(start);
				int tokenStopIndex = adaptor.GetTokenStopIndex(stop);
				tokenStopIndex = ((stop == null || adaptor.GetNodeType(stop) != 3) ? (Count - 1) : adaptor.GetTokenStopIndex(start));
				return tokens.ToString(tokenStartIndex, tokenStopIndex);
			}
			global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder();
			ToStringWork(start, stop, stringBuilder);
			return stringBuilder.ToString();
		}

		protected internal virtual void ToStringWork(object p, object stop, global::System.Text.StringBuilder buf)
		{
			if (!adaptor.IsNil(p))
			{
				string text = adaptor.GetNodeText(p);
				if (text == null)
				{
					text = " " + adaptor.GetNodeType(p);
				}
				buf.Append(text);
			}
			if (p != stop)
			{
				int childCount = adaptor.GetChildCount(p);
				if (childCount > 0 && !adaptor.IsNil(p))
				{
					buf.Append(" ");
					buf.Append(2);
				}
				for (int i = 0; i < childCount; i++)
				{
					object child = adaptor.GetChild(p, i);
					ToStringWork(child, stop, buf);
				}
				if (childCount > 0 && !adaptor.IsNil(p))
				{
					buf.Append(" ");
					buf.Append(3);
				}
			}
		}
	}
}
