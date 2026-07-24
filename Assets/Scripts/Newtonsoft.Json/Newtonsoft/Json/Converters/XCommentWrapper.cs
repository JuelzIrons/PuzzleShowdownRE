namespace Newtonsoft.Json.Converters
{
	internal class XCommentWrapper : global::Newtonsoft.Json.Converters.XObjectWrapper
	{
		private global::System.Xml.Linq.XComment Text => (global::System.Xml.Linq.XComment)base.WrappedNode;

		public override string? Value
		{
			get
			{
				return Text.Value;
			}
			set
			{
				Text.Value = value ?? string.Empty;
			}
		}

		public override global::Newtonsoft.Json.Converters.IXmlNode? ParentNode
		{
			get
			{
				if (Text.Parent == null)
				{
					return null;
				}
				return global::Newtonsoft.Json.Converters.XContainerWrapper.WrapNode(Text.Parent);
			}
		}

		public XCommentWrapper(global::System.Xml.Linq.XComment text)
			: base(text)
		{
		}
	}
}
