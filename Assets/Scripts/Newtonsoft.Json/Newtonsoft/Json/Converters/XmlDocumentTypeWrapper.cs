namespace Newtonsoft.Json.Converters
{
	internal class XmlDocumentTypeWrapper : global::Newtonsoft.Json.Converters.XmlNodeWrapper, global::Newtonsoft.Json.Converters.IXmlDocumentType, global::Newtonsoft.Json.Converters.IXmlNode
	{
		private readonly global::System.Xml.XmlDocumentType _documentType;

		public string Name => _documentType.Name;

		public string? System => _documentType.SystemId;

		public string? Public => _documentType.PublicId;

		public string? InternalSubset => _documentType.InternalSubset;

		public override string? LocalName => "DOCTYPE";

		public XmlDocumentTypeWrapper(global::System.Xml.XmlDocumentType documentType)
			: base(documentType)
		{
			_documentType = documentType;
		}
	}
}
