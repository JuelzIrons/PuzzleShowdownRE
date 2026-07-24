namespace Newtonsoft.Json.Converters
{
	internal class XmlDocumentWrapper : global::Newtonsoft.Json.Converters.XmlNodeWrapper, global::Newtonsoft.Json.Converters.IXmlDocument, global::Newtonsoft.Json.Converters.IXmlNode
	{
		private readonly global::System.Xml.XmlDocument _document;

		public global::Newtonsoft.Json.Converters.IXmlElement? DocumentElement
		{
			get
			{
				if (_document.DocumentElement == null)
				{
					return null;
				}
				return new global::Newtonsoft.Json.Converters.XmlElementWrapper(_document.DocumentElement);
			}
		}

		public XmlDocumentWrapper(global::System.Xml.XmlDocument document)
			: base(document)
		{
			_document = document;
		}

		public global::Newtonsoft.Json.Converters.IXmlNode CreateComment(string? data)
		{
			return new global::Newtonsoft.Json.Converters.XmlNodeWrapper(_document.CreateComment(data));
		}

		public global::Newtonsoft.Json.Converters.IXmlNode CreateTextNode(string? text)
		{
			return new global::Newtonsoft.Json.Converters.XmlNodeWrapper(_document.CreateTextNode(text));
		}

		public global::Newtonsoft.Json.Converters.IXmlNode CreateCDataSection(string? data)
		{
			return new global::Newtonsoft.Json.Converters.XmlNodeWrapper(_document.CreateCDataSection(data));
		}

		public global::Newtonsoft.Json.Converters.IXmlNode CreateWhitespace(string? text)
		{
			return new global::Newtonsoft.Json.Converters.XmlNodeWrapper(_document.CreateWhitespace(text));
		}

		public global::Newtonsoft.Json.Converters.IXmlNode CreateSignificantWhitespace(string? text)
		{
			return new global::Newtonsoft.Json.Converters.XmlNodeWrapper(_document.CreateSignificantWhitespace(text));
		}

		public global::Newtonsoft.Json.Converters.IXmlNode CreateXmlDeclaration(string version, string? encoding, string? standalone)
		{
			return new global::Newtonsoft.Json.Converters.XmlDeclarationWrapper(_document.CreateXmlDeclaration(version, encoding, standalone));
		}

		public global::Newtonsoft.Json.Converters.IXmlNode CreateXmlDocumentType(string name, string? publicId, string? systemId, string? internalSubset)
		{
			return new global::Newtonsoft.Json.Converters.XmlDocumentTypeWrapper(_document.CreateDocumentType(name, publicId, systemId, null));
		}

		public global::Newtonsoft.Json.Converters.IXmlNode CreateProcessingInstruction(string target, string data)
		{
			return new global::Newtonsoft.Json.Converters.XmlNodeWrapper(_document.CreateProcessingInstruction(target, data));
		}

		public global::Newtonsoft.Json.Converters.IXmlElement CreateElement(string elementName)
		{
			return new global::Newtonsoft.Json.Converters.XmlElementWrapper(_document.CreateElement(elementName));
		}

		public global::Newtonsoft.Json.Converters.IXmlElement CreateElement(string qualifiedName, string namespaceUri)
		{
			return new global::Newtonsoft.Json.Converters.XmlElementWrapper(_document.CreateElement(qualifiedName, namespaceUri));
		}

		public global::Newtonsoft.Json.Converters.IXmlNode CreateAttribute(string name, string? value)
		{
			return new global::Newtonsoft.Json.Converters.XmlNodeWrapper(_document.CreateAttribute(name))
			{
				Value = value
			};
		}

		public global::Newtonsoft.Json.Converters.IXmlNode CreateAttribute(string qualifiedName, string? namespaceUri, string? value)
		{
			return new global::Newtonsoft.Json.Converters.XmlNodeWrapper(_document.CreateAttribute(qualifiedName, namespaceUri))
			{
				Value = value
			};
		}
	}
}
