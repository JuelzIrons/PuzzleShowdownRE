namespace Unity.VisualScripting.Antlr3.Runtime.Tree
{
	public class RewriteRuleNodeStream : global::Unity.VisualScripting.Antlr3.Runtime.Tree.RewriteRuleElementStream<object>
	{
		public RewriteRuleNodeStream(global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITreeAdaptor adaptor, string elementDescription)
			: base(adaptor, elementDescription)
		{
		}

		public RewriteRuleNodeStream(global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITreeAdaptor adaptor, string elementDescription, object oneElement)
			: base(adaptor, elementDescription, oneElement)
		{
		}

		public RewriteRuleNodeStream(global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITreeAdaptor adaptor, string elementDescription, global::System.Collections.Generic.IList<object> elements)
			: base(adaptor, elementDescription, elements)
		{
		}

		[global::System.Obsolete("This constructor is for internal use only and might be phased out soon. Use instead the one with IList<T>.")]
		public RewriteRuleNodeStream(global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITreeAdaptor adaptor, string elementDescription, global::System.Collections.IList elements)
			: base(adaptor, elementDescription, elements)
		{
		}

		public object NextNode()
		{
			return _Next();
		}

		protected override object ToTree(object el)
		{
			return adaptor.DupNode(el);
		}
	}
}
