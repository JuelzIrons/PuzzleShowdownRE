namespace Unity.VisualScripting.Antlr3.Runtime.Tree
{
	public class TreeWizard
	{
		public interface ContextVisitor
		{
			void Visit(object t, object parent, int childIndex, global::System.Collections.IDictionary labels);
		}

		public abstract class Visitor : global::Unity.VisualScripting.Antlr3.Runtime.Tree.TreeWizard.ContextVisitor
		{
			public void Visit(object t, object parent, int childIndex, global::System.Collections.IDictionary labels)
			{
				Visit(t);
			}

			public abstract void Visit(object t);
		}

		private sealed class RecordAllElementsVisitor : global::Unity.VisualScripting.Antlr3.Runtime.Tree.TreeWizard.Visitor
		{
			private global::System.Collections.IList list;

			public RecordAllElementsVisitor(global::System.Collections.IList list)
			{
				this.list = list;
			}

			public override void Visit(object t)
			{
				list.Add(t);
			}
		}

		private sealed class PatternMatchingContextVisitor : global::Unity.VisualScripting.Antlr3.Runtime.Tree.TreeWizard.ContextVisitor
		{
			private global::Unity.VisualScripting.Antlr3.Runtime.Tree.TreeWizard owner;

			private global::Unity.VisualScripting.Antlr3.Runtime.Tree.TreeWizard.TreePattern pattern;

			private global::System.Collections.IList list;

			public PatternMatchingContextVisitor(global::Unity.VisualScripting.Antlr3.Runtime.Tree.TreeWizard owner, global::Unity.VisualScripting.Antlr3.Runtime.Tree.TreeWizard.TreePattern pattern, global::System.Collections.IList list)
			{
				this.owner = owner;
				this.pattern = pattern;
				this.list = list;
			}

			public void Visit(object t, object parent, int childIndex, global::System.Collections.IDictionary labels)
			{
				if (owner._Parse(t, pattern, null))
				{
					list.Add(t);
				}
			}
		}

		public class TreePattern : global::Unity.VisualScripting.Antlr3.Runtime.Tree.CommonTree
		{
			public string label;

			public bool hasTextArg;

			public TreePattern(global::Unity.VisualScripting.Antlr3.Runtime.IToken payload)
				: base(payload)
			{
			}

			public override string ToString()
			{
				if (label != null)
				{
					return "%" + label + ":" + base.ToString();
				}
				return base.ToString();
			}
		}

		private sealed class InvokeVisitorOnPatternMatchContextVisitor : global::Unity.VisualScripting.Antlr3.Runtime.Tree.TreeWizard.ContextVisitor
		{
			private global::Unity.VisualScripting.Antlr3.Runtime.Tree.TreeWizard owner;

			private global::Unity.VisualScripting.Antlr3.Runtime.Tree.TreeWizard.TreePattern pattern;

			private global::Unity.VisualScripting.Antlr3.Runtime.Tree.TreeWizard.ContextVisitor visitor;

			private global::System.Collections.Hashtable labels = new global::System.Collections.Hashtable();

			public InvokeVisitorOnPatternMatchContextVisitor(global::Unity.VisualScripting.Antlr3.Runtime.Tree.TreeWizard owner, global::Unity.VisualScripting.Antlr3.Runtime.Tree.TreeWizard.TreePattern pattern, global::Unity.VisualScripting.Antlr3.Runtime.Tree.TreeWizard.ContextVisitor visitor)
			{
				this.owner = owner;
				this.pattern = pattern;
				this.visitor = visitor;
			}

			public void Visit(object t, object parent, int childIndex, global::System.Collections.IDictionary unusedlabels)
			{
				labels.Clear();
				if (owner._Parse(t, pattern, labels))
				{
					visitor.Visit(t, parent, childIndex, labels);
				}
			}
		}

		public class WildcardTreePattern : global::Unity.VisualScripting.Antlr3.Runtime.Tree.TreeWizard.TreePattern
		{
			public WildcardTreePattern(global::Unity.VisualScripting.Antlr3.Runtime.IToken payload)
				: base(payload)
			{
			}
		}

		public class TreePatternTreeAdaptor : global::Unity.VisualScripting.Antlr3.Runtime.Tree.CommonTreeAdaptor
		{
			public override object Create(global::Unity.VisualScripting.Antlr3.Runtime.IToken payload)
			{
				return new global::Unity.VisualScripting.Antlr3.Runtime.Tree.TreeWizard.TreePattern(payload);
			}
		}

		protected global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITreeAdaptor adaptor;

		protected global::System.Collections.IDictionary tokenNameToTypeMap;

		public TreeWizard(global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITreeAdaptor adaptor)
		{
			this.adaptor = adaptor;
		}

		public TreeWizard(global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITreeAdaptor adaptor, global::System.Collections.IDictionary tokenNameToTypeMap)
		{
			this.adaptor = adaptor;
			this.tokenNameToTypeMap = tokenNameToTypeMap;
		}

		public TreeWizard(global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITreeAdaptor adaptor, string[] tokenNames)
		{
			this.adaptor = adaptor;
			tokenNameToTypeMap = ComputeTokenTypes(tokenNames);
		}

		public TreeWizard(string[] tokenNames)
			: this(null, tokenNames)
		{
		}

		public global::System.Collections.IDictionary ComputeTokenTypes(string[] tokenNames)
		{
			global::System.Collections.IDictionary dictionary = new global::System.Collections.Hashtable();
			if (tokenNames == null)
			{
				return dictionary;
			}
			for (int i = global::Unity.VisualScripting.Antlr3.Runtime.Token.MIN_TOKEN_TYPE; i < tokenNames.Length; i++)
			{
				string key = tokenNames[i];
				dictionary.Add(key, i);
			}
			return dictionary;
		}

		public int GetTokenType(string tokenName)
		{
			if (tokenNameToTypeMap == null)
			{
				return 0;
			}
			object obj = tokenNameToTypeMap[tokenName];
			if (obj != null)
			{
				return (int)obj;
			}
			return 0;
		}

		public global::System.Collections.IDictionary Index(object t)
		{
			global::System.Collections.IDictionary dictionary = new global::System.Collections.Hashtable();
			_Index(t, dictionary);
			return dictionary;
		}

		protected void _Index(object t, global::System.Collections.IDictionary m)
		{
			if (t != null)
			{
				int nodeType = adaptor.GetNodeType(t);
				global::System.Collections.IList list = m[nodeType] as global::System.Collections.IList;
				if (list == null)
				{
					list = (global::System.Collections.IList)(m[nodeType] = new global::System.Collections.Generic.List<object>());
				}
				list.Add(t);
				int childCount = adaptor.GetChildCount(t);
				for (int i = 0; i < childCount; i++)
				{
					object child = adaptor.GetChild(t, i);
					_Index(child, m);
				}
			}
		}

		public global::System.Collections.IList Find(object t, int ttype)
		{
			global::System.Collections.IList list = new global::System.Collections.Generic.List<object>();
			Visit(t, ttype, new global::Unity.VisualScripting.Antlr3.Runtime.Tree.TreeWizard.RecordAllElementsVisitor(list));
			return list;
		}

		public global::System.Collections.IList Find(object t, string pattern)
		{
			global::System.Collections.IList list = new global::System.Collections.Generic.List<object>();
			global::Unity.VisualScripting.Antlr3.Runtime.Tree.TreePatternLexer tokenizer = new global::Unity.VisualScripting.Antlr3.Runtime.Tree.TreePatternLexer(pattern);
			global::Unity.VisualScripting.Antlr3.Runtime.Tree.TreePatternParser treePatternParser = new global::Unity.VisualScripting.Antlr3.Runtime.Tree.TreePatternParser(tokenizer, this, new global::Unity.VisualScripting.Antlr3.Runtime.Tree.TreeWizard.TreePatternTreeAdaptor());
			global::Unity.VisualScripting.Antlr3.Runtime.Tree.TreeWizard.TreePattern treePattern = (global::Unity.VisualScripting.Antlr3.Runtime.Tree.TreeWizard.TreePattern)treePatternParser.Pattern();
			if (treePattern == null || treePattern.IsNil || (object)treePattern.GetType() == typeof(global::Unity.VisualScripting.Antlr3.Runtime.Tree.TreeWizard.WildcardTreePattern))
			{
				return null;
			}
			int type = treePattern.Type;
			Visit(t, type, new global::Unity.VisualScripting.Antlr3.Runtime.Tree.TreeWizard.PatternMatchingContextVisitor(this, treePattern, list));
			return list;
		}

		public object FindFirst(object t, int ttype)
		{
			return null;
		}

		public object FindFirst(object t, string pattern)
		{
			return null;
		}

		public void Visit(object t, int ttype, global::Unity.VisualScripting.Antlr3.Runtime.Tree.TreeWizard.ContextVisitor visitor)
		{
			_Visit(t, null, 0, ttype, visitor);
		}

		protected void _Visit(object t, object parent, int childIndex, int ttype, global::Unity.VisualScripting.Antlr3.Runtime.Tree.TreeWizard.ContextVisitor visitor)
		{
			if (t != null)
			{
				if (adaptor.GetNodeType(t) == ttype)
				{
					visitor.Visit(t, parent, childIndex, null);
				}
				int childCount = adaptor.GetChildCount(t);
				for (int i = 0; i < childCount; i++)
				{
					object child = adaptor.GetChild(t, i);
					_Visit(child, t, i, ttype, visitor);
				}
			}
		}

		public void Visit(object t, string pattern, global::Unity.VisualScripting.Antlr3.Runtime.Tree.TreeWizard.ContextVisitor visitor)
		{
			global::Unity.VisualScripting.Antlr3.Runtime.Tree.TreePatternLexer tokenizer = new global::Unity.VisualScripting.Antlr3.Runtime.Tree.TreePatternLexer(pattern);
			global::Unity.VisualScripting.Antlr3.Runtime.Tree.TreePatternParser treePatternParser = new global::Unity.VisualScripting.Antlr3.Runtime.Tree.TreePatternParser(tokenizer, this, new global::Unity.VisualScripting.Antlr3.Runtime.Tree.TreeWizard.TreePatternTreeAdaptor());
			global::Unity.VisualScripting.Antlr3.Runtime.Tree.TreeWizard.TreePattern treePattern = (global::Unity.VisualScripting.Antlr3.Runtime.Tree.TreeWizard.TreePattern)treePatternParser.Pattern();
			if (treePattern != null && !treePattern.IsNil && (object)treePattern.GetType() != typeof(global::Unity.VisualScripting.Antlr3.Runtime.Tree.TreeWizard.WildcardTreePattern))
			{
				int type = treePattern.Type;
				Visit(t, type, new global::Unity.VisualScripting.Antlr3.Runtime.Tree.TreeWizard.InvokeVisitorOnPatternMatchContextVisitor(this, treePattern, visitor));
			}
		}

		public bool Parse(object t, string pattern, global::System.Collections.IDictionary labels)
		{
			global::Unity.VisualScripting.Antlr3.Runtime.Tree.TreePatternLexer tokenizer = new global::Unity.VisualScripting.Antlr3.Runtime.Tree.TreePatternLexer(pattern);
			global::Unity.VisualScripting.Antlr3.Runtime.Tree.TreePatternParser treePatternParser = new global::Unity.VisualScripting.Antlr3.Runtime.Tree.TreePatternParser(tokenizer, this, new global::Unity.VisualScripting.Antlr3.Runtime.Tree.TreeWizard.TreePatternTreeAdaptor());
			global::Unity.VisualScripting.Antlr3.Runtime.Tree.TreeWizard.TreePattern t2 = (global::Unity.VisualScripting.Antlr3.Runtime.Tree.TreeWizard.TreePattern)treePatternParser.Pattern();
			return _Parse(t, t2, labels);
		}

		public bool Parse(object t, string pattern)
		{
			return Parse(t, pattern, null);
		}

		protected bool _Parse(object t1, global::Unity.VisualScripting.Antlr3.Runtime.Tree.TreeWizard.TreePattern t2, global::System.Collections.IDictionary labels)
		{
			if (t1 == null || t2 == null)
			{
				return false;
			}
			if ((object)t2.GetType() != typeof(global::Unity.VisualScripting.Antlr3.Runtime.Tree.TreeWizard.WildcardTreePattern))
			{
				if (adaptor.GetNodeType(t1) != t2.Type)
				{
					return false;
				}
				if (t2.hasTextArg && !adaptor.GetNodeText(t1).Equals(t2.Text))
				{
					return false;
				}
			}
			if (t2.label != null && labels != null)
			{
				labels[t2.label] = t1;
			}
			int childCount = adaptor.GetChildCount(t1);
			int childCount2 = t2.ChildCount;
			if (childCount != childCount2)
			{
				return false;
			}
			for (int i = 0; i < childCount; i++)
			{
				object child = adaptor.GetChild(t1, i);
				global::Unity.VisualScripting.Antlr3.Runtime.Tree.TreeWizard.TreePattern t3 = (global::Unity.VisualScripting.Antlr3.Runtime.Tree.TreeWizard.TreePattern)t2.GetChild(i);
				if (!_Parse(child, t3, labels))
				{
					return false;
				}
			}
			return true;
		}

		public object Create(string pattern)
		{
			global::Unity.VisualScripting.Antlr3.Runtime.Tree.TreePatternLexer tokenizer = new global::Unity.VisualScripting.Antlr3.Runtime.Tree.TreePatternLexer(pattern);
			global::Unity.VisualScripting.Antlr3.Runtime.Tree.TreePatternParser treePatternParser = new global::Unity.VisualScripting.Antlr3.Runtime.Tree.TreePatternParser(tokenizer, this, adaptor);
			return treePatternParser.Pattern();
		}

		public static bool Equals(object t1, object t2, global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITreeAdaptor adaptor)
		{
			return _Equals(t1, t2, adaptor);
		}

		public new bool Equals(object t1, object t2)
		{
			return _Equals(t1, t2, adaptor);
		}

		protected static bool _Equals(object t1, object t2, global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITreeAdaptor adaptor)
		{
			if (t1 == null || t2 == null)
			{
				return false;
			}
			if (adaptor.GetNodeType(t1) != adaptor.GetNodeType(t2))
			{
				return false;
			}
			if (!adaptor.GetNodeText(t1).Equals(adaptor.GetNodeText(t2)))
			{
				return false;
			}
			int childCount = adaptor.GetChildCount(t1);
			int childCount2 = adaptor.GetChildCount(t2);
			if (childCount != childCount2)
			{
				return false;
			}
			for (int i = 0; i < childCount; i++)
			{
				object child = adaptor.GetChild(t1, i);
				object child2 = adaptor.GetChild(t2, i);
				if (!_Equals(child, child2, adaptor))
				{
					return false;
				}
			}
			return true;
		}
	}
}
