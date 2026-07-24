namespace Newtonsoft.Json.Converters
{
	internal class XObjectWrapper : global::Newtonsoft.Json.Converters.IXmlNode
	{
		private readonly global::System.Xml.Linq.XObject? _xmlObject;

		public object? WrappedNode => _xmlObject;

		public virtual global::System.Xml.XmlNodeType NodeType => _xmlObject?.NodeType ?? global::System.Xml.XmlNodeType.None;

		public virtual string? LocalName => null;

		public virtual global::System.Collections.Generic.List<global::Newtonsoft.Json.Converters.IXmlNode> ChildNodes => global::Newtonsoft.Json.Converters.XmlNodeConverter.EmptyChildNodes;

		public virtual global::System.Collections.Generic.List<global::Newtonsoft.Json.Converters.IXmlNode> Attributes => global::Newtonsoft.Json.Converters.XmlNodeConverter.EmptyChildNodes;

		public virtual global::Newtonsoft.Json.Converters.IXmlNode? ParentNode => null;

		public virtual string? Value
		{
			get
			{
				return null;
			}
			set
			{
				throw new global::System.InvalidOperationException();
			}
		}

		public virtual string? NamespaceUri => null;

		public XObjectWrapper(global::System.Xml.Linq.XObject? xmlObject)
		{
			_xmlObject = xmlObject;
		}

		public virtual global::Newtonsoft.Json.Converters.IXmlNode AppendChild(global::Newtonsoft.Json.Converters.IXmlNode newChild)
		{
			throw new global::System.InvalidOperationException();
		}
	}
}
