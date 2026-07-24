namespace Newtonsoft.Json.Converters
{
	internal class XmlDeclarationWrapper : global::Newtonsoft.Json.Converters.XmlNodeWrapper, global::Newtonsoft.Json.Converters.IXmlDeclaration, global::Newtonsoft.Json.Converters.IXmlNode
	{
		private readonly global::System.Xml.XmlDeclaration _declaration;

		public string? Version => _declaration.Version;

		public string? Encoding
		{
			get
			{
				return _declaration.Encoding;
			}
			set
			{
				_declaration.Encoding = value;
			}
		}

		public string? Standalone
		{
			get
			{
				return _declaration.Standalone;
			}
			set
			{
				_declaration.Standalone = value;
			}
		}

		public XmlDeclarationWrapper(global::System.Xml.XmlDeclaration declaration)
			: base(declaration)
		{
			_declaration = declaration;
		}
	}
}
