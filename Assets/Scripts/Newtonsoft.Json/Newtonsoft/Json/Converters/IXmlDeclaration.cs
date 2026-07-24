namespace Newtonsoft.Json.Converters
{
	internal interface IXmlDeclaration : global::Newtonsoft.Json.Converters.IXmlNode
	{
		string? Version { get; }

		string? Encoding { get; set; }

		string? Standalone { get; set; }
	}
}
