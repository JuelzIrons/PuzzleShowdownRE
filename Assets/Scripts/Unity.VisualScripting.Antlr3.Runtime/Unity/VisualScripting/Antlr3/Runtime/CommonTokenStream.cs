namespace Unity.VisualScripting.Antlr3.Runtime
{
	public class CommonTokenStream : global::Unity.VisualScripting.Antlr3.Runtime.ITokenStream, global::Unity.VisualScripting.Antlr3.Runtime.IIntStream
	{
		protected global::Unity.VisualScripting.Antlr3.Runtime.ITokenSource tokenSource;

		protected global::System.Collections.IList tokens;

		protected global::System.Collections.IDictionary channelOverrideMap;

		protected global::Unity.VisualScripting.Antlr3.Runtime.Collections.HashList discardSet;

		protected int channel;

		protected bool discardOffChannelTokens;

		protected int lastMarker;

		protected int p = -1;

		public virtual global::Unity.VisualScripting.Antlr3.Runtime.ITokenSource TokenSource
		{
			get
			{
				return tokenSource;
			}
			set
			{
				tokenSource = value;
				tokens.Clear();
				p = -1;
				channel = 0;
			}
		}

		public virtual string SourceName => TokenSource.SourceName;

		public virtual int Count => tokens.Count;

		public CommonTokenStream()
		{
			channel = 0;
			tokens = new global::System.Collections.Generic.List<object>(500);
		}

		public CommonTokenStream(global::Unity.VisualScripting.Antlr3.Runtime.ITokenSource tokenSource)
			: this()
		{
			this.tokenSource = tokenSource;
		}

		public CommonTokenStream(global::Unity.VisualScripting.Antlr3.Runtime.ITokenSource tokenSource, int channel)
			: this(tokenSource)
		{
			this.channel = channel;
		}

		public virtual global::Unity.VisualScripting.Antlr3.Runtime.IToken LT(int k)
		{
			if (p == -1)
			{
				FillBuffer();
			}
			if (k == 0)
			{
				return null;
			}
			if (k < 0)
			{
				return LB(-k);
			}
			if (p + k - 1 >= tokens.Count)
			{
				return global::Unity.VisualScripting.Antlr3.Runtime.Token.EOF_TOKEN;
			}
			int num = p;
			for (int i = 1; i < k; i++)
			{
				num = SkipOffTokenChannels(num + 1);
			}
			if (num >= tokens.Count)
			{
				return global::Unity.VisualScripting.Antlr3.Runtime.Token.EOF_TOKEN;
			}
			return (global::Unity.VisualScripting.Antlr3.Runtime.IToken)tokens[num];
		}

		public virtual global::Unity.VisualScripting.Antlr3.Runtime.IToken Get(int i)
		{
			return (global::Unity.VisualScripting.Antlr3.Runtime.IToken)tokens[i];
		}

		public virtual string ToString(int start, int stop)
		{
			if (start < 0 || stop < 0)
			{
				return null;
			}
			if (p == -1)
			{
				FillBuffer();
			}
			if (stop >= tokens.Count)
			{
				stop = tokens.Count - 1;
			}
			global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder();
			for (int i = start; i <= stop; i++)
			{
				global::Unity.VisualScripting.Antlr3.Runtime.IToken token = (global::Unity.VisualScripting.Antlr3.Runtime.IToken)tokens[i];
				stringBuilder.Append(token.Text);
			}
			return stringBuilder.ToString();
		}

		public virtual string ToString(global::Unity.VisualScripting.Antlr3.Runtime.IToken start, global::Unity.VisualScripting.Antlr3.Runtime.IToken stop)
		{
			if (start != null && stop != null)
			{
				return ToString(start.TokenIndex, stop.TokenIndex);
			}
			return null;
		}

		public virtual void Consume()
		{
			if (p < tokens.Count)
			{
				p++;
				p = SkipOffTokenChannels(p);
			}
		}

		public virtual int LA(int i)
		{
			return LT(i).Type;
		}

		public virtual int Mark()
		{
			if (p == -1)
			{
				FillBuffer();
			}
			lastMarker = Index();
			return lastMarker;
		}

		public virtual int Index()
		{
			return p;
		}

		public virtual void Rewind(int marker)
		{
			Seek(marker);
		}

		public virtual void Rewind()
		{
			Seek(lastMarker);
		}

		public virtual void Reset()
		{
			p = 0;
			lastMarker = 0;
		}

		public virtual void Release(int marker)
		{
		}

		public virtual void Seek(int index)
		{
			p = index;
		}

		[global::System.Obsolete("Please use the property Count instead.")]
		public virtual int Size()
		{
			return Count;
		}

		protected virtual void FillBuffer()
		{
			int num = 0;
			global::Unity.VisualScripting.Antlr3.Runtime.IToken token = tokenSource.NextToken();
			while (token != null && token.Type != -1)
			{
				bool flag = false;
				if (channelOverrideMap != null)
				{
					object obj = channelOverrideMap[token.Type];
					if (obj != null)
					{
						token.Channel = (int)obj;
					}
				}
				if (discardSet != null && discardSet.Contains(token.Type.ToString()))
				{
					flag = true;
				}
				else if (discardOffChannelTokens && token.Channel != channel)
				{
					flag = true;
				}
				if (!flag)
				{
					token.TokenIndex = num;
					tokens.Add(token);
					num++;
				}
				token = tokenSource.NextToken();
			}
			p = 0;
			p = SkipOffTokenChannels(p);
		}

		protected virtual int SkipOffTokenChannels(int i)
		{
			int count = tokens.Count;
			while (i < count && ((global::Unity.VisualScripting.Antlr3.Runtime.IToken)tokens[i]).Channel != channel)
			{
				i++;
			}
			return i;
		}

		protected virtual int SkipOffTokenChannelsReverse(int i)
		{
			while (i >= 0 && ((global::Unity.VisualScripting.Antlr3.Runtime.IToken)tokens[i]).Channel != channel)
			{
				i--;
			}
			return i;
		}

		public virtual void SetTokenTypeChannel(int ttype, int channel)
		{
			if (channelOverrideMap == null)
			{
				channelOverrideMap = new global::System.Collections.Hashtable();
			}
			channelOverrideMap[ttype] = channel;
		}

		public virtual void DiscardTokenType(int ttype)
		{
			if (discardSet == null)
			{
				discardSet = new global::Unity.VisualScripting.Antlr3.Runtime.Collections.HashList();
			}
			discardSet.Add(ttype.ToString(), ttype);
		}

		public virtual void DiscardOffChannelTokens(bool discardOffChannelTokens)
		{
			this.discardOffChannelTokens = discardOffChannelTokens;
		}

		public virtual global::System.Collections.IList GetTokens()
		{
			if (p == -1)
			{
				FillBuffer();
			}
			return tokens;
		}

		public virtual global::System.Collections.IList GetTokens(int start, int stop)
		{
			return GetTokens(start, stop, (global::Unity.VisualScripting.Antlr3.Runtime.BitSet)null);
		}

		public virtual global::System.Collections.IList GetTokens(int start, int stop, global::Unity.VisualScripting.Antlr3.Runtime.BitSet types)
		{
			if (p == -1)
			{
				FillBuffer();
			}
			if (stop >= tokens.Count)
			{
				stop = tokens.Count - 1;
			}
			if (start < 0)
			{
				start = 0;
			}
			if (start > stop)
			{
				return null;
			}
			global::System.Collections.IList list = new global::System.Collections.Generic.List<object>();
			for (int i = start; i <= stop; i++)
			{
				global::Unity.VisualScripting.Antlr3.Runtime.IToken token = (global::Unity.VisualScripting.Antlr3.Runtime.IToken)tokens[i];
				if (types == null || types.Member(token.Type))
				{
					list.Add(token);
				}
			}
			if (list.Count == 0)
			{
				list = null;
			}
			return list;
		}

		public virtual global::System.Collections.IList GetTokens(int start, int stop, global::System.Collections.IList types)
		{
			return GetTokens(start, stop, new global::Unity.VisualScripting.Antlr3.Runtime.BitSet(types));
		}

		public virtual global::System.Collections.IList GetTokens(int start, int stop, int ttype)
		{
			return GetTokens(start, stop, global::Unity.VisualScripting.Antlr3.Runtime.BitSet.Of(ttype));
		}

		protected virtual global::Unity.VisualScripting.Antlr3.Runtime.IToken LB(int k)
		{
			if (p == -1)
			{
				FillBuffer();
			}
			if (k == 0)
			{
				return null;
			}
			if (p - k < 0)
			{
				return null;
			}
			int num = p;
			for (int i = 1; i <= k; i++)
			{
				num = SkipOffTokenChannelsReverse(num - 1);
			}
			if (num < 0)
			{
				return null;
			}
			return (global::Unity.VisualScripting.Antlr3.Runtime.IToken)tokens[num];
		}

		public override string ToString()
		{
			if (p == -1)
			{
				FillBuffer();
			}
			return ToString(0, tokens.Count - 1);
		}
	}
}
