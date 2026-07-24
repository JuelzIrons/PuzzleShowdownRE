namespace Unity.VisualScripting.Antlr3.Runtime
{
	public class TokenRewriteStream : global::Unity.VisualScripting.Antlr3.Runtime.CommonTokenStream
	{
		private class RewriteOpComparer : global::System.Collections.IComparer
		{
			public virtual int Compare(object o1, object o2)
			{
				global::Unity.VisualScripting.Antlr3.Runtime.TokenRewriteStream.RewriteOperation rewriteOperation = (global::Unity.VisualScripting.Antlr3.Runtime.TokenRewriteStream.RewriteOperation)o1;
				global::Unity.VisualScripting.Antlr3.Runtime.TokenRewriteStream.RewriteOperation rewriteOperation2 = (global::Unity.VisualScripting.Antlr3.Runtime.TokenRewriteStream.RewriteOperation)o2;
				if (rewriteOperation.index < rewriteOperation2.index)
				{
					return -1;
				}
				if (rewriteOperation.index > rewriteOperation2.index)
				{
					return 1;
				}
				return 0;
			}
		}

		protected internal class RewriteOperation
		{
			protected internal int instructionIndex;

			protected internal int index;

			protected internal object text;

			protected internal global::Unity.VisualScripting.Antlr3.Runtime.TokenRewriteStream parent;

			protected internal RewriteOperation(int index, object text, global::Unity.VisualScripting.Antlr3.Runtime.TokenRewriteStream parent)
			{
				this.index = index;
				this.text = text;
				this.parent = parent;
			}

			public virtual int Execute(global::System.Text.StringBuilder buf)
			{
				return index;
			}

			public override string ToString()
			{
				string fullName = GetType().FullName;
				int num = fullName.IndexOf('$');
				fullName = fullName.Substring(num + 1, fullName.Length - (num + 1));
				return string.Concat("<", fullName, "@", index, ":\"", text, "\">");
			}
		}

		protected internal class InsertBeforeOp : global::Unity.VisualScripting.Antlr3.Runtime.TokenRewriteStream.RewriteOperation
		{
			public InsertBeforeOp(int index, object text, global::Unity.VisualScripting.Antlr3.Runtime.TokenRewriteStream parent)
				: base(index, text, parent)
			{
			}

			public override int Execute(global::System.Text.StringBuilder buf)
			{
				buf.Append(text);
				buf.Append(parent.Get(index).Text);
				return index + 1;
			}
		}

		protected internal class ReplaceOp : global::Unity.VisualScripting.Antlr3.Runtime.TokenRewriteStream.RewriteOperation
		{
			protected internal int lastIndex;

			public ReplaceOp(int from, int to, object text, global::Unity.VisualScripting.Antlr3.Runtime.TokenRewriteStream parent)
				: base(from, text, parent)
			{
				lastIndex = to;
			}

			public override int Execute(global::System.Text.StringBuilder buf)
			{
				if (text != null)
				{
					buf.Append(text);
				}
				return lastIndex + 1;
			}

			public override string ToString()
			{
				return string.Concat("<ReplaceOp@", index, "..", lastIndex, ":\"", text, "\">");
			}
		}

		protected internal class DeleteOp : global::Unity.VisualScripting.Antlr3.Runtime.TokenRewriteStream.ReplaceOp
		{
			public DeleteOp(int from, int to, global::Unity.VisualScripting.Antlr3.Runtime.TokenRewriteStream parent)
				: base(from, to, null, parent)
			{
			}

			public override string ToString()
			{
				return "<DeleteOp@" + index + ".." + lastIndex + ">";
			}
		}

		public const string DEFAULT_PROGRAM_NAME = "default";

		public const int PROGRAM_INIT_SIZE = 100;

		public const int MIN_TOKEN_INDEX = 0;

		protected global::System.Collections.IDictionary programs;

		protected global::System.Collections.IDictionary lastRewriteTokenIndexes;

		public TokenRewriteStream()
		{
			Init();
		}

		public TokenRewriteStream(global::Unity.VisualScripting.Antlr3.Runtime.ITokenSource tokenSource)
			: base(tokenSource)
		{
			Init();
		}

		public TokenRewriteStream(global::Unity.VisualScripting.Antlr3.Runtime.ITokenSource tokenSource, int channel)
			: base(tokenSource, channel)
		{
			Init();
		}

		protected internal virtual void Init()
		{
			programs = new global::System.Collections.Hashtable();
			programs["default"] = new global::System.Collections.Generic.List<object>(100);
			lastRewriteTokenIndexes = new global::System.Collections.Hashtable();
		}

		public virtual void Rollback(int instructionIndex)
		{
			Rollback("default", instructionIndex);
		}

		public virtual void Rollback(string programName, int instructionIndex)
		{
			global::System.Collections.IList list = (global::System.Collections.IList)programs[programName];
			if (list != null)
			{
				programs[programName] = ((global::System.Collections.Generic.List<object>)list).GetRange(0, instructionIndex);
			}
		}

		public virtual void DeleteProgram()
		{
			DeleteProgram("default");
		}

		public virtual void DeleteProgram(string programName)
		{
			Rollback(programName, 0);
		}

		public virtual void InsertAfter(global::Unity.VisualScripting.Antlr3.Runtime.IToken t, object text)
		{
			InsertAfter("default", t, text);
		}

		public virtual void InsertAfter(int index, object text)
		{
			InsertAfter("default", index, text);
		}

		public virtual void InsertAfter(string programName, global::Unity.VisualScripting.Antlr3.Runtime.IToken t, object text)
		{
			InsertAfter(programName, t.TokenIndex, text);
		}

		public virtual void InsertAfter(string programName, int index, object text)
		{
			InsertBefore(programName, index + 1, text);
		}

		public virtual void InsertBefore(global::Unity.VisualScripting.Antlr3.Runtime.IToken t, object text)
		{
			InsertBefore("default", t, text);
		}

		public virtual void InsertBefore(int index, object text)
		{
			InsertBefore("default", index, text);
		}

		public virtual void InsertBefore(string programName, global::Unity.VisualScripting.Antlr3.Runtime.IToken t, object text)
		{
			InsertBefore(programName, t.TokenIndex, text);
		}

		public virtual void InsertBefore(string programName, int index, object text)
		{
			global::Unity.VisualScripting.Antlr3.Runtime.TokenRewriteStream.RewriteOperation value = new global::Unity.VisualScripting.Antlr3.Runtime.TokenRewriteStream.InsertBeforeOp(index, text, this);
			global::System.Collections.IList program = GetProgram(programName);
			program.Add(value);
		}

		public virtual void Replace(int index, object text)
		{
			Replace("default", index, index, text);
		}

		public virtual void Replace(int from, int to, object text)
		{
			Replace("default", from, to, text);
		}

		public virtual void Replace(global::Unity.VisualScripting.Antlr3.Runtime.IToken indexT, object text)
		{
			Replace("default", indexT, indexT, text);
		}

		public virtual void Replace(global::Unity.VisualScripting.Antlr3.Runtime.IToken from, global::Unity.VisualScripting.Antlr3.Runtime.IToken to, object text)
		{
			Replace("default", from, to, text);
		}

		public virtual void Replace(string programName, int from, int to, object text)
		{
			if (from > to || from < 0 || to < 0 || to >= tokens.Count)
			{
				throw new global::System.ArgumentOutOfRangeException("replace: range invalid: " + from + ".." + to + "(size=" + tokens.Count + ")");
			}
			global::Unity.VisualScripting.Antlr3.Runtime.TokenRewriteStream.RewriteOperation rewriteOperation = new global::Unity.VisualScripting.Antlr3.Runtime.TokenRewriteStream.ReplaceOp(from, to, text, this);
			global::System.Collections.IList program = GetProgram(programName);
			rewriteOperation.instructionIndex = program.Count;
			program.Add(rewriteOperation);
		}

		public virtual void Replace(string programName, global::Unity.VisualScripting.Antlr3.Runtime.IToken from, global::Unity.VisualScripting.Antlr3.Runtime.IToken to, object text)
		{
			Replace(programName, from.TokenIndex, to.TokenIndex, text);
		}

		public virtual void Delete(int index)
		{
			Delete("default", index, index);
		}

		public virtual void Delete(int from, int to)
		{
			Delete("default", from, to);
		}

		public virtual void Delete(global::Unity.VisualScripting.Antlr3.Runtime.IToken indexT)
		{
			Delete("default", indexT, indexT);
		}

		public virtual void Delete(global::Unity.VisualScripting.Antlr3.Runtime.IToken from, global::Unity.VisualScripting.Antlr3.Runtime.IToken to)
		{
			Delete("default", from, to);
		}

		public virtual void Delete(string programName, int from, int to)
		{
			Replace(programName, from, to, null);
		}

		public virtual void Delete(string programName, global::Unity.VisualScripting.Antlr3.Runtime.IToken from, global::Unity.VisualScripting.Antlr3.Runtime.IToken to)
		{
			Replace(programName, from, to, null);
		}

		public virtual int GetLastRewriteTokenIndex()
		{
			return GetLastRewriteTokenIndex("default");
		}

		protected virtual int GetLastRewriteTokenIndex(string programName)
		{
			object obj = lastRewriteTokenIndexes[programName];
			if (obj == null)
			{
				return -1;
			}
			return (int)obj;
		}

		protected virtual void SetLastRewriteTokenIndex(string programName, int i)
		{
			lastRewriteTokenIndexes[programName] = i;
		}

		protected virtual global::System.Collections.IList GetProgram(string name)
		{
			global::System.Collections.IList list = (global::System.Collections.IList)programs[name];
			if (list == null)
			{
				list = InitializeProgram(name);
			}
			return list;
		}

		private global::System.Collections.IList InitializeProgram(string name)
		{
			global::System.Collections.IList list = new global::System.Collections.Generic.List<object>(100);
			programs[name] = list;
			return list;
		}

		public virtual string ToOriginalString()
		{
			return ToOriginalString(0, Count - 1);
		}

		public virtual string ToOriginalString(int start, int end)
		{
			global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder();
			for (int i = start; i >= 0 && i <= end && i < tokens.Count; i++)
			{
				stringBuilder.Append(Get(i).Text);
			}
			return stringBuilder.ToString();
		}

		public override string ToString()
		{
			return ToString(0, Count - 1);
		}

		public virtual string ToString(string programName)
		{
			return ToString(programName, 0, Count - 1);
		}

		public override string ToString(int start, int end)
		{
			return ToString("default", start, end);
		}

		public virtual string ToString(string programName, int start, int end)
		{
			global::System.Collections.IList list = (global::System.Collections.IList)programs[programName];
			if (end > tokens.Count - 1)
			{
				end = tokens.Count - 1;
			}
			if (start < 0)
			{
				start = 0;
			}
			if (list == null || list.Count == 0)
			{
				return ToOriginalString(start, end);
			}
			global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder();
			global::System.Collections.IDictionary dictionary = ReduceToSingleOperationPerIndex(list);
			int num = start;
			while (num <= end && num < tokens.Count)
			{
				global::Unity.VisualScripting.Antlr3.Runtime.TokenRewriteStream.RewriteOperation rewriteOperation = (global::Unity.VisualScripting.Antlr3.Runtime.TokenRewriteStream.RewriteOperation)dictionary[num];
				dictionary.Remove(num);
				global::Unity.VisualScripting.Antlr3.Runtime.IToken token = (global::Unity.VisualScripting.Antlr3.Runtime.IToken)tokens[num];
				if (rewriteOperation == null)
				{
					stringBuilder.Append(token.Text);
					num++;
				}
				else
				{
					num = rewriteOperation.Execute(stringBuilder);
				}
			}
			if (end == tokens.Count - 1)
			{
				global::System.Collections.IEnumerator enumerator = dictionary.Values.GetEnumerator();
				while (enumerator.MoveNext())
				{
					global::Unity.VisualScripting.Antlr3.Runtime.TokenRewriteStream.InsertBeforeOp insertBeforeOp = (global::Unity.VisualScripting.Antlr3.Runtime.TokenRewriteStream.InsertBeforeOp)enumerator.Current;
					if (insertBeforeOp.index >= tokens.Count - 1)
					{
						stringBuilder.Append(insertBeforeOp.text);
					}
				}
			}
			return stringBuilder.ToString();
		}

		protected global::System.Collections.IDictionary ReduceToSingleOperationPerIndex(global::System.Collections.IList rewrites)
		{
			for (int i = 0; i < rewrites.Count; i++)
			{
				global::Unity.VisualScripting.Antlr3.Runtime.TokenRewriteStream.RewriteOperation rewriteOperation = (global::Unity.VisualScripting.Antlr3.Runtime.TokenRewriteStream.RewriteOperation)rewrites[i];
				if (rewriteOperation == null || !(rewriteOperation is global::Unity.VisualScripting.Antlr3.Runtime.TokenRewriteStream.ReplaceOp))
				{
					continue;
				}
				global::Unity.VisualScripting.Antlr3.Runtime.TokenRewriteStream.ReplaceOp replaceOp = (global::Unity.VisualScripting.Antlr3.Runtime.TokenRewriteStream.ReplaceOp)rewrites[i];
				global::System.Collections.IList kindOfOps = GetKindOfOps(rewrites, typeof(global::Unity.VisualScripting.Antlr3.Runtime.TokenRewriteStream.InsertBeforeOp), i);
				for (int j = 0; j < kindOfOps.Count; j++)
				{
					global::Unity.VisualScripting.Antlr3.Runtime.TokenRewriteStream.InsertBeforeOp insertBeforeOp = (global::Unity.VisualScripting.Antlr3.Runtime.TokenRewriteStream.InsertBeforeOp)kindOfOps[j];
					if (insertBeforeOp.index >= replaceOp.index && insertBeforeOp.index <= replaceOp.lastIndex)
					{
						rewrites[insertBeforeOp.instructionIndex] = null;
					}
				}
				global::System.Collections.IList kindOfOps2 = GetKindOfOps(rewrites, typeof(global::Unity.VisualScripting.Antlr3.Runtime.TokenRewriteStream.ReplaceOp), i);
				for (int k = 0; k < kindOfOps2.Count; k++)
				{
					global::Unity.VisualScripting.Antlr3.Runtime.TokenRewriteStream.ReplaceOp replaceOp2 = (global::Unity.VisualScripting.Antlr3.Runtime.TokenRewriteStream.ReplaceOp)kindOfOps2[k];
					if (replaceOp2.index >= replaceOp.index && replaceOp2.lastIndex <= replaceOp.lastIndex)
					{
						rewrites[replaceOp2.instructionIndex] = null;
						continue;
					}
					bool flag = replaceOp2.lastIndex < replaceOp.index || replaceOp2.index > replaceOp.lastIndex;
					bool flag2 = replaceOp2.index == replaceOp.index && replaceOp2.lastIndex == replaceOp.lastIndex;
					if (!flag && !flag2)
					{
						throw new global::System.ArgumentOutOfRangeException(string.Concat("replace op boundaries of ", replaceOp, " overlap with previous ", replaceOp2));
					}
				}
			}
			for (int l = 0; l < rewrites.Count; l++)
			{
				global::Unity.VisualScripting.Antlr3.Runtime.TokenRewriteStream.RewriteOperation rewriteOperation2 = (global::Unity.VisualScripting.Antlr3.Runtime.TokenRewriteStream.RewriteOperation)rewrites[l];
				if (rewriteOperation2 == null || !(rewriteOperation2 is global::Unity.VisualScripting.Antlr3.Runtime.TokenRewriteStream.InsertBeforeOp))
				{
					continue;
				}
				global::Unity.VisualScripting.Antlr3.Runtime.TokenRewriteStream.InsertBeforeOp insertBeforeOp2 = (global::Unity.VisualScripting.Antlr3.Runtime.TokenRewriteStream.InsertBeforeOp)rewrites[l];
				global::System.Collections.IList kindOfOps3 = GetKindOfOps(rewrites, typeof(global::Unity.VisualScripting.Antlr3.Runtime.TokenRewriteStream.InsertBeforeOp), l);
				for (int m = 0; m < kindOfOps3.Count; m++)
				{
					global::Unity.VisualScripting.Antlr3.Runtime.TokenRewriteStream.InsertBeforeOp insertBeforeOp3 = (global::Unity.VisualScripting.Antlr3.Runtime.TokenRewriteStream.InsertBeforeOp)kindOfOps3[m];
					if (insertBeforeOp3.index == insertBeforeOp2.index)
					{
						insertBeforeOp2.text = CatOpText(insertBeforeOp2.text, insertBeforeOp3.text);
						rewrites[insertBeforeOp3.instructionIndex] = null;
					}
				}
				global::System.Collections.IList kindOfOps4 = GetKindOfOps(rewrites, typeof(global::Unity.VisualScripting.Antlr3.Runtime.TokenRewriteStream.ReplaceOp), l);
				for (int n = 0; n < kindOfOps4.Count; n++)
				{
					global::Unity.VisualScripting.Antlr3.Runtime.TokenRewriteStream.ReplaceOp replaceOp3 = (global::Unity.VisualScripting.Antlr3.Runtime.TokenRewriteStream.ReplaceOp)kindOfOps4[n];
					if (insertBeforeOp2.index == replaceOp3.index)
					{
						replaceOp3.text = CatOpText(insertBeforeOp2.text, replaceOp3.text);
						rewrites[l] = null;
					}
					else if (insertBeforeOp2.index >= replaceOp3.index && insertBeforeOp2.index <= replaceOp3.lastIndex)
					{
						throw new global::System.ArgumentOutOfRangeException(string.Concat("insert op ", insertBeforeOp2, " within boundaries of previous ", replaceOp3));
					}
				}
			}
			global::System.Collections.IDictionary dictionary = new global::System.Collections.Hashtable();
			for (int num = 0; num < rewrites.Count; num++)
			{
				global::Unity.VisualScripting.Antlr3.Runtime.TokenRewriteStream.RewriteOperation rewriteOperation3 = (global::Unity.VisualScripting.Antlr3.Runtime.TokenRewriteStream.RewriteOperation)rewrites[num];
				if (rewriteOperation3 != null)
				{
					if (dictionary[rewriteOperation3.index] != null)
					{
						throw new global::System.Exception("should only be one op per index");
					}
					dictionary[rewriteOperation3.index] = rewriteOperation3;
				}
			}
			return dictionary;
		}

		protected string CatOpText(object a, object b)
		{
			string text = "";
			string text2 = "";
			if (a != null)
			{
				text = a.ToString();
			}
			if (b != null)
			{
				text2 = b.ToString();
			}
			return text + text2;
		}

		protected global::System.Collections.IList GetKindOfOps(global::System.Collections.IList rewrites, global::System.Type kind)
		{
			return GetKindOfOps(rewrites, kind, rewrites.Count);
		}

		protected global::System.Collections.IList GetKindOfOps(global::System.Collections.IList rewrites, global::System.Type kind, int before)
		{
			global::System.Collections.IList list = new global::System.Collections.Generic.List<object>();
			for (int i = 0; i < before && i < rewrites.Count; i++)
			{
				global::Unity.VisualScripting.Antlr3.Runtime.TokenRewriteStream.RewriteOperation rewriteOperation = (global::Unity.VisualScripting.Antlr3.Runtime.TokenRewriteStream.RewriteOperation)rewrites[i];
				if (rewriteOperation != null && (object)rewriteOperation.GetType() == kind)
				{
					list.Add(rewriteOperation);
				}
			}
			return list;
		}

		public virtual string ToDebugString()
		{
			return ToDebugString(0, Count - 1);
		}

		public virtual string ToDebugString(int start, int end)
		{
			global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder();
			for (int i = start; i >= 0 && i <= end && i < tokens.Count; i++)
			{
				stringBuilder.Append(Get(i));
			}
			return stringBuilder.ToString();
		}
	}
}
