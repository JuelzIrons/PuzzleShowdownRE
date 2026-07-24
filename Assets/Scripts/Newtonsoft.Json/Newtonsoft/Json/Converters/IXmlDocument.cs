namespace Newtonsoft.Json.Converters
{
	internal interface IXmlDocument : global::Newtonsoft.Json.Converters.IXmlNode
	{
		global::Newtonsoft.Json.Converters.IXmlElement? DocumentElement { get; }

		global::Newtonsoft.Json.Converters.IXmlNode CreateComment(string? text);

		global::Newtonsoft.Json.Converters.IXmlNode CreateTextNode(string? text);

		global::Newtonsoft.Json.Converters.IXmlNode CreateCDataSection(string? data);

		global::Newtonsoft.Json.Converters.IXmlNode CreateWhitespace(string? text);

		global::Newtonsoft.Json.Converters.IXmlNode CreateSignificantWhitespace(string? text);

		global::Newtonsoft.Json.Converters.IXmlNode CreateXmlDeclaration(string version, string? encoding, string? standalone);

		global::Newtonsoft.Json.Converters.IXmlNode CreateXmlDocumentType(string name, string? publicId, string? systemId, string? internalSubset);

		global::Newtonsoft.Json.Converters.IXmlNode CreateProcessingInstruction(string target, string data);

		global::Newtonsoft.Json.Converters.IXmlElement CreateElement(string elementName);

		global::Newtonsoft.Json.Converters.IXmlElement CreateElement(string qualifiedName, string namespaceUri);

		global::Newtonsoft.Json.Converters.IXmlNode CreateAttribute(string name, string value);

		global::Newtonsoft.Json.Converters.IXmlNode CreateAttribute(string qualifiedName, string namespaceUri, string value);
	}
}
