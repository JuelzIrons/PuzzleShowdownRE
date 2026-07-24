namespace Unity.VisualScripting.Antlr3.Runtime
{
	[global::System.Serializable]
	public class RecognitionException : global::System.Exception
	{
		[global::System.NonSerialized]
		protected global::Unity.VisualScripting.Antlr3.Runtime.IIntStream input;

		protected int index;

		protected global::Unity.VisualScripting.Antlr3.Runtime.IToken token;

		protected object node;

		protected int c;

		protected int line;

		protected int charPositionInLine;

		public bool approximateLineInfo;

		public global::Unity.VisualScripting.Antlr3.Runtime.IIntStream Input
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

		public int Index
		{
			get
			{
				return index;
			}
			set
			{
				index = value;
			}
		}

		public global::Unity.VisualScripting.Antlr3.Runtime.IToken Token
		{
			get
			{
				return token;
			}
			set
			{
				token = value;
			}
		}

		public object Node
		{
			get
			{
				return node;
			}
			set
			{
				node = value;
			}
		}

		public int Char
		{
			get
			{
				return c;
			}
			set
			{
				c = value;
			}
		}

		public int CharPositionInLine
		{
			get
			{
				return charPositionInLine;
			}
			set
			{
				charPositionInLine = value;
			}
		}

		public int Line
		{
			get
			{
				return line;
			}
			set
			{
				line = value;
			}
		}

		public virtual int UnexpectedType
		{
			get
			{
				if (input is global::Unity.VisualScripting.Antlr3.Runtime.ITokenStream)
				{
					return token.Type;
				}
				if (input is global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITreeNodeStream)
				{
					global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITreeNodeStream treeNodeStream = (global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITreeNodeStream)input;
					global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITreeAdaptor treeAdaptor = treeNodeStream.TreeAdaptor;
					return treeAdaptor.GetNodeType(node);
				}
				return c;
			}
		}

		public RecognitionException()
			: this(null, null, null)
		{
		}

		public RecognitionException(string message)
			: this(message, null, null)
		{
		}

		public RecognitionException(string message, global::System.Exception inner)
			: this(message, inner, null)
		{
		}

		public RecognitionException(global::Unity.VisualScripting.Antlr3.Runtime.IIntStream input)
			: this(null, null, input)
		{
		}

		public RecognitionException(string message, global::Unity.VisualScripting.Antlr3.Runtime.IIntStream input)
			: this(message, null, input)
		{
		}

		public RecognitionException(string message, global::System.Exception inner, global::Unity.VisualScripting.Antlr3.Runtime.IIntStream input)
			: base(message, inner)
		{
			this.input = input;
			index = input.Index();
			if (input is global::Unity.VisualScripting.Antlr3.Runtime.ITokenStream)
			{
				token = ((global::Unity.VisualScripting.Antlr3.Runtime.ITokenStream)input).LT(1);
				line = token.Line;
				charPositionInLine = token.CharPositionInLine;
			}
			if (input is global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITreeNodeStream)
			{
				ExtractInformationFromTreeNodeStream(input);
			}
			else if (input is global::Unity.VisualScripting.Antlr3.Runtime.ICharStream)
			{
				c = input.LA(1);
				line = ((global::Unity.VisualScripting.Antlr3.Runtime.ICharStream)input).Line;
				charPositionInLine = ((global::Unity.VisualScripting.Antlr3.Runtime.ICharStream)input).CharPositionInLine;
			}
			else
			{
				c = input.LA(1);
			}
		}

		protected void ExtractInformationFromTreeNodeStream(global::Unity.VisualScripting.Antlr3.Runtime.IIntStream input)
		{
			global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITreeNodeStream treeNodeStream = (global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITreeNodeStream)input;
			node = treeNodeStream.LT(1);
			global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITreeAdaptor treeAdaptor = treeNodeStream.TreeAdaptor;
			global::Unity.VisualScripting.Antlr3.Runtime.IToken token = treeAdaptor.GetToken(node);
			if (token != null)
			{
				this.token = token;
				if (token.Line <= 0)
				{
					int num = -1;
					for (object obj = treeNodeStream.LT(num); obj != null; obj = treeNodeStream.LT(num))
					{
						global::Unity.VisualScripting.Antlr3.Runtime.IToken token2 = treeAdaptor.GetToken(obj);
						if (token2 != null && token2.Line > 0)
						{
							line = token2.Line;
							charPositionInLine = token2.CharPositionInLine;
							approximateLineInfo = true;
							break;
						}
						num--;
					}
				}
				else
				{
					line = token.Line;
					charPositionInLine = token.CharPositionInLine;
				}
			}
			else if (node is global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITree)
			{
				line = ((global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITree)node).Line;
				charPositionInLine = ((global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITree)node).CharPositionInLine;
				if (node is global::Unity.VisualScripting.Antlr3.Runtime.Tree.CommonTree)
				{
					this.token = ((global::Unity.VisualScripting.Antlr3.Runtime.Tree.CommonTree)node).Token;
				}
			}
			else
			{
				int nodeType = treeAdaptor.GetNodeType(node);
				string nodeText = treeAdaptor.GetNodeText(node);
				this.token = new global::Unity.VisualScripting.Antlr3.Runtime.CommonToken(nodeType, nodeText);
			}
		}
	}
}
