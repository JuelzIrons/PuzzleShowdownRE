namespace Newtonsoft.Json.Converters
{
	internal class XDocumentWrapper : global::Newtonsoft.Json.Converters.XContainerWrapper, global::Newtonsoft.Json.Converters.IXmlDocument, global::Newtonsoft.Json.Converters.IXmlNode
	{
		private global::System.Xml.Linq.XDocument Document => (global::System.Xml.Linq.XDocument)base.WrappedNode;

		public override global::System.Collections.Generic.List<global::Newtonsoft.Json.Converters.IXmlNode> ChildNodes
		{
			get
			{
				global::System.Collections.Generic.List<global::Newtonsoft.Json.Converters.IXmlNode> childNodes = base.ChildNodes;
				if (Document.Declaration != null && (childNodes.Count == 0 || childNodes[0].NodeType != global::System.Xml.XmlNodeType.XmlDeclaration))
				{
					childNodes.Insert(0, new global::Newtonsoft.Json.Converters.XDeclarationWrapper(Document.Declaration));
				}
				return childNodes;
			}
		}

		protected override bool HasChildNodes
		{
			get
			{
				if (base.HasChildNodes)
				{
					return true;
				}
				return Document.Declaration != null;
			}
		}

		public global::Newtonsoft.Json.Converters.IXmlElement? DocumentElement
		{
			get
			{
				if (Document.Root == null)
				{
					return null;
				}
				return new global::Newtonsoft.Json.Converters.XElementWrapper(Document.Root);
			}
		}

		public XDocumentWrapper(global::System.Xml.Linq.XDocument document)
			: base(document)
		{
		}

		public global::Newtonsoft.Json.Converters.IXmlNode CreateComment(string? text)
		{
			return new global::Newtonsoft.Json.Converters.XObjectWrapper(new global::System.Xml.Linq.XComment(text));
		}

		public global::Newtonsoft.Json.Converters.IXmlNode CreateTextNode(string? text)
		{
			return new global::Newtonsoft.Json.Converters.XObjectWrapper(new global::System.Xml.Linq.XText(text));
		}

		public global::Newtonsoft.Json.Converters.IXmlNode CreateCDataSection(string? data)
		{
			return new global::Newtonsoft.Json.Converters.XObjectWrapper(new global::System.Xml.Linq.XCData(data));
		}

		public global::Newtonsoft.Json.Converters.IXmlNode CreateWhitespace(string? text)
		{
			return new global::Newtonsoft.Json.Converters.XObjectWrapper(new global::System.Xml.Linq.XText(text));
		}

		public global::Newtonsoft.Json.Converters.IXmlNode CreateSignificantWhitespace(string? text)
		{
			return new global::Newtonsoft.Json.Converters.XObjectWrapper(new global::System.Xml.Linq.XText(text));
		}

		public global::Newtonsoft.Json.Converters.IXmlNode CreateXmlDeclaration(string version, string? encoding, string? standalone)
		{
			return new global::Newtonsoft.Json.Converters.XDeclarationWrapper(new global::System.Xml.Linq.XDeclaration(version, encoding, standalone));
		}

		public global::Newtonsoft.Json.Converters.IXmlNode CreateXmlDocumentType(string name, string? publicId, string? systemId, string? internalSubset)
		{
			return new global::Newtonsoft.Json.Converters.XDocumentTypeWrapper(new global::System.Xml.Linq.XDocumentType(name, publicId, systemId, internalSubset));
		}

		public global::Newtonsoft.Json.Converters.IXmlNode CreateProcessingInstruction(string target, string data)
		{
			return new global::Newtonsoft.Json.Converters.XProcessingInstructionWrapper(new global::System.Xml.Linq.XProcessingInstruction(target, data));
		}

		public global::Newtonsoft.Json.Converters.IXmlElement CreateElement(string elementName)
		{
			return new global::Newtonsoft.Json.Converters.XElementWrapper(new global::System.Xml.Linq.XElement(elementName));
		}

		public global::Newtonsoft.Json.Converters.IXmlElement CreateElement(string qualifiedName, string namespaceUri)
		{
			return new global::Newtonsoft.Json.Converters.XElementWrapper(new global::System.Xml.Linq.XElement(global::System.Xml.Linq.XName.Get(global::Newtonsoft.Json.Utilities.MiscellaneousUtils.GetLocalName(qualifiedName), namespaceUri)));
		}

		public global::Newtonsoft.Json.Converters.IXmlNode CreateAttribute(string name, string value)
		{
			return new global::Newtonsoft.Json.Converters.XAttributeWrapper(new global::System.Xml.Linq.XAttribute(name, value));
		}

		public global::Newtonsoft.Json.Converters.IXmlNode CreateAttribute(string qualifiedName, string namespaceUri, string value)
		{
			return new global::Newtonsoft.Json.Converters.XAttributeWrapper(new global::System.Xml.Linq.XAttribute(global::System.Xml.Linq.XName.Get(global::Newtonsoft.Json.Utilities.MiscellaneousUtils.GetLocalName(qualifiedName), namespaceUri), value));
		}

		public override global::Newtonsoft.Json.Converters.IXmlNode AppendChild(global::Newtonsoft.Json.Converters.IXmlNode newChild)
		{
			if (newChild is global::Newtonsoft.Json.Converters.XDeclarationWrapper xDeclarationWrapper)
			{
				Document.Declaration = xDeclarationWrapper.Declaration;
				return xDeclarationWrapper;
			}
			return base.AppendChild(newChild);
		}
	}
}
