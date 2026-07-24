namespace Unity.VisualScripting.Antlr3.Runtime.Tree
{
	public class RewriteRuleTokenStream : global::Unity.VisualScripting.Antlr3.Runtime.Tree.RewriteRuleElementStream<global::Unity.VisualScripting.Antlr3.Runtime.IToken>
	{
		public RewriteRuleTokenStream(global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITreeAdaptor adaptor, string elementDescription)
			: base(adaptor, elementDescription)
		{
		}

		public RewriteRuleTokenStream(global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITreeAdaptor adaptor, string elementDescription, global::Unity.VisualScripting.Antlr3.Runtime.IToken oneElement)
			: base(adaptor, elementDescription, oneElement)
		{
		}

		public RewriteRuleTokenStream(global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITreeAdaptor adaptor, string elementDescription, global::System.Collections.Generic.IList<global::Unity.VisualScripting.Antlr3.Runtime.IToken> elements)
			: base(adaptor, elementDescription, elements)
		{
		}

		[global::System.Obsolete("This constructor is for internal use only and might be phased out soon. Use instead the one with IList<T>.")]
		public RewriteRuleTokenStream(global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITreeAdaptor adaptor, string elementDescription, global::System.Collections.IList elements)
			: base(adaptor, elementDescription, elements)
		{
		}

		public object NextNode()
		{
			return adaptor.Create((global::Unity.VisualScripting.Antlr3.Runtime.IToken)_Next());
		}

		public global::Unity.VisualScripting.Antlr3.Runtime.IToken NextToken()
		{
			return (global::Unity.VisualScripting.Antlr3.Runtime.IToken)_Next();
		}

		protected override object ToTree(global::Unity.VisualScripting.Antlr3.Runtime.IToken el)
		{
			return el;
		}
	}
}
