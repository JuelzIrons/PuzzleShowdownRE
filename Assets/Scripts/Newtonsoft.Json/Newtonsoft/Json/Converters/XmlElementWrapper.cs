namespace Newtonsoft.Json.Converters
{
	internal class XmlElementWrapper : global::Newtonsoft.Json.Converters.XmlNodeWrapper, global::Newtonsoft.Json.Converters.IXmlElement, global::Newtonsoft.Json.Converters.IXmlNode
	{
		private readonly global::System.Xml.XmlElement _element;

		public bool IsEmpty => _element.IsEmpty;

		public XmlElementWrapper(global::System.Xml.XmlElement element)
			: base(element)
		{
			_element = element;
		}

		public void SetAttributeNode(global::Newtonsoft.Json.Converters.IXmlNode attribute)
		{
			global::Newtonsoft.Json.Converters.XmlNodeWrapper xmlNodeWrapper = (global::Newtonsoft.Json.Converters.XmlNodeWrapper)attribute;
			_element.SetAttributeNode((global::System.Xml.XmlAttribute)xmlNodeWrapper.WrappedNode);
		}

		public string? GetPrefixOfNamespace(string namespaceUri)
		{
			return _element.GetPrefixOfNamespace(namespaceUri);
		}
	}
}
