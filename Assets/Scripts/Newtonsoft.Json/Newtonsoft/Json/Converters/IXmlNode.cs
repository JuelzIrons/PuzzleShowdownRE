namespace Newtonsoft.Json.Converters
{
	internal interface IXmlNode
	{
		global::System.Xml.XmlNodeType NodeType { get; }

		string? LocalName { get; }

		global::System.Collections.Generic.List<global::Newtonsoft.Json.Converters.IXmlNode> ChildNodes { get; }

		global::System.Collections.Generic.List<global::Newtonsoft.Json.Converters.IXmlNode> Attributes { get; }

		global::Newtonsoft.Json.Converters.IXmlNode? ParentNode { get; }

		string? Value { get; set; }

		string? NamespaceUri { get; }

		object? WrappedNode { get; }

		global::Newtonsoft.Json.Converters.IXmlNode AppendChild(global::Newtonsoft.Json.Converters.IXmlNode newChild);
	}
}
