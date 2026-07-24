namespace Newtonsoft.Json.Converters
{
	internal class XDeclarationWrapper : global::Newtonsoft.Json.Converters.XObjectWrapper, global::Newtonsoft.Json.Converters.IXmlDeclaration, global::Newtonsoft.Json.Converters.IXmlNode
	{
		internal global::System.Xml.Linq.XDeclaration Declaration { get; }

		public override global::System.Xml.XmlNodeType NodeType => global::System.Xml.XmlNodeType.XmlDeclaration;

		public string? Version => Declaration.Version;

		public string? Encoding
		{
			get
			{
				return Declaration.Encoding;
			}
			set
			{
				Declaration.Encoding = value;
			}
		}

		public string? Standalone
		{
			get
			{
				return Declaration.Standalone;
			}
			set
			{
				Declaration.Standalone = value;
			}
		}

		public XDeclarationWrapper(global::System.Xml.Linq.XDeclaration declaration)
			: base(null)
		{
			Declaration = declaration;
		}
	}
}
