namespace Newtonsoft.Json.Converters
{
	internal class XAttributeWrapper : global::Newtonsoft.Json.Converters.XObjectWrapper
	{
		private global::System.Xml.Linq.XAttribute Attribute => (global::System.Xml.Linq.XAttribute)base.WrappedNode;

		public override string? Value
		{
			get
			{
				return Attribute.Value;
			}
			set
			{
				Attribute.Value = value ?? string.Empty;
			}
		}

		public override string? LocalName => Attribute.Name.LocalName;

		public override string? NamespaceUri => Attribute.Name.NamespaceName;

		public override global::Newtonsoft.Json.Converters.IXmlNode? ParentNode
		{
			get
			{
				if (Attribute.Parent == null)
				{
					return null;
				}
				return global::Newtonsoft.Json.Converters.XContainerWrapper.WrapNode(Attribute.Parent);
			}
		}

		public XAttributeWrapper(global::System.Xml.Linq.XAttribute attribute)
			: base(attribute)
		{
		}
	}
}
