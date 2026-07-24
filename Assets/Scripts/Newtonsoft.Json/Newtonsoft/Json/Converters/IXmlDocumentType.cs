namespace Newtonsoft.Json.Converters
{
	internal interface IXmlDocumentType : global::Newtonsoft.Json.Converters.IXmlNode
	{
		string Name { get; }

		string? System { get; }

		string? Public { get; }

		string? InternalSubset { get; }
	}
}
