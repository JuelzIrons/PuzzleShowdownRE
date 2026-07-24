namespace Newtonsoft.Json.Converters
{
	internal class XDocumentTypeWrapper : global::Newtonsoft.Json.Converters.XObjectWrapper, global::Newtonsoft.Json.Converters.IXmlDocumentType, global::Newtonsoft.Json.Converters.IXmlNode
	{
		private readonly global::System.Xml.Linq.XDocumentType _documentType;

		public string Name => _documentType.Name;

		public string? System => _documentType.SystemId;

		public string? Public => _documentType.PublicId;

		public string? InternalSubset => _documentType.InternalSubset;

		public override string? LocalName => "DOCTYPE";

		public XDocumentTypeWrapper(global::System.Xml.Linq.XDocumentType documentType)
			: base(documentType)
		{
			_documentType = documentType;
		}
	}
}
