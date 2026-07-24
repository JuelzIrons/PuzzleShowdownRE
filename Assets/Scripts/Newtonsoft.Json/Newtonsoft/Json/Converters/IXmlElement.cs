namespace Newtonsoft.Json.Converters
{
	internal interface IXmlElement : global::Newtonsoft.Json.Converters.IXmlNode
	{
		bool IsEmpty { get; }

		void SetAttributeNode(global::Newtonsoft.Json.Converters.IXmlNode attribute);

		string? GetPrefixOfNamespace(string namespaceUri);
	}
}
