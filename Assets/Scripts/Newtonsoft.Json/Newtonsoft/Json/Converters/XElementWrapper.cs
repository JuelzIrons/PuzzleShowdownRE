namespace Newtonsoft.Json.Converters
{
	internal class XElementWrapper : global::Newtonsoft.Json.Converters.XContainerWrapper, global::Newtonsoft.Json.Converters.IXmlElement, global::Newtonsoft.Json.Converters.IXmlNode
	{
		private global::System.Collections.Generic.List<global::Newtonsoft.Json.Converters.IXmlNode>? _attributes;

		private global::System.Xml.Linq.XElement Element => (global::System.Xml.Linq.XElement)base.WrappedNode;

		public override global::System.Collections.Generic.List<global::Newtonsoft.Json.Converters.IXmlNode> Attributes
		{
			get
			{
				if (_attributes == null)
				{
					if (!Element.HasAttributes && !HasImplicitNamespaceAttribute(NamespaceUri))
					{
						_attributes = global::Newtonsoft.Json.Converters.XmlNodeConverter.EmptyChildNodes;
					}
					else
					{
						_attributes = new global::System.Collections.Generic.List<global::Newtonsoft.Json.Converters.IXmlNode>();
						foreach (global::System.Xml.Linq.XAttribute item in Element.Attributes())
						{
							_attributes.Add(new global::Newtonsoft.Json.Converters.XAttributeWrapper(item));
						}
						string namespaceUri = NamespaceUri;
						if (HasImplicitNamespaceAttribute(namespaceUri))
						{
							_attributes.Insert(0, new global::Newtonsoft.Json.Converters.XAttributeWrapper(new global::System.Xml.Linq.XAttribute("xmlns", namespaceUri)));
						}
					}
				}
				return _attributes;
			}
		}

		public override string? Value
		{
			get
			{
				return Element.Value;
			}
			set
			{
				Element.Value = value ?? string.Empty;
			}
		}

		public override string? LocalName => Element.Name.LocalName;

		public override string? NamespaceUri => Element.Name.NamespaceName;

		public bool IsEmpty => Element.IsEmpty;

		public XElementWrapper(global::System.Xml.Linq.XElement element)
			: base(element)
		{
		}

		public void SetAttributeNode(global::Newtonsoft.Json.Converters.IXmlNode attribute)
		{
			global::Newtonsoft.Json.Converters.XObjectWrapper xObjectWrapper = (global::Newtonsoft.Json.Converters.XObjectWrapper)attribute;
			Element.Add(xObjectWrapper.WrappedNode);
			_attributes = null;
		}

		private bool HasImplicitNamespaceAttribute(string namespaceUri)
		{
			if (!global::Newtonsoft.Json.Utilities.StringUtils.IsNullOrEmpty(namespaceUri) && namespaceUri != ParentNode?.NamespaceUri && global::Newtonsoft.Json.Utilities.StringUtils.IsNullOrEmpty(GetPrefixOfNamespace(namespaceUri)))
			{
				bool flag = false;
				if (Element.HasAttributes)
				{
					foreach (global::System.Xml.Linq.XAttribute item in Element.Attributes())
					{
						if (item.Name.LocalName == "xmlns" && global::Newtonsoft.Json.Utilities.StringUtils.IsNullOrEmpty(item.Name.NamespaceName) && item.Value == namespaceUri)
						{
							flag = true;
						}
					}
				}
				if (!flag)
				{
					return true;
				}
			}
			return false;
		}

		public override global::Newtonsoft.Json.Converters.IXmlNode AppendChild(global::Newtonsoft.Json.Converters.IXmlNode newChild)
		{
			global::Newtonsoft.Json.Converters.IXmlNode result = base.AppendChild(newChild);
			_attributes = null;
			return result;
		}

		public string? GetPrefixOfNamespace(string namespaceUri)
		{
			return Element.GetPrefixOfNamespace(namespaceUri);
		}
	}
}
