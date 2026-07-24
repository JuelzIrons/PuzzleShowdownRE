namespace Newtonsoft.Json.Converters
{
	internal class XmlNodeWrapper : global::Newtonsoft.Json.Converters.IXmlNode
	{
		private readonly global::System.Xml.XmlNode _node;

		private global::System.Collections.Generic.List<global::Newtonsoft.Json.Converters.IXmlNode>? _childNodes;

		private global::System.Collections.Generic.List<global::Newtonsoft.Json.Converters.IXmlNode>? _attributes;

		public object? WrappedNode => _node;

		public global::System.Xml.XmlNodeType NodeType => _node.NodeType;

		public virtual string? LocalName => _node.LocalName;

		public global::System.Collections.Generic.List<global::Newtonsoft.Json.Converters.IXmlNode> ChildNodes
		{
			get
			{
				if (_childNodes == null)
				{
					if (!_node.HasChildNodes)
					{
						_childNodes = global::Newtonsoft.Json.Converters.XmlNodeConverter.EmptyChildNodes;
					}
					else
					{
						_childNodes = new global::System.Collections.Generic.List<global::Newtonsoft.Json.Converters.IXmlNode>(_node.ChildNodes.Count);
						foreach (global::System.Xml.XmlNode childNode in _node.ChildNodes)
						{
							_childNodes.Add(WrapNode(childNode));
						}
					}
				}
				return _childNodes;
			}
		}

		protected virtual bool HasChildNodes => _node.HasChildNodes;

		public global::System.Collections.Generic.List<global::Newtonsoft.Json.Converters.IXmlNode> Attributes
		{
			get
			{
				if (_attributes == null)
				{
					if (!HasAttributes)
					{
						_attributes = global::Newtonsoft.Json.Converters.XmlNodeConverter.EmptyChildNodes;
					}
					else
					{
						_attributes = new global::System.Collections.Generic.List<global::Newtonsoft.Json.Converters.IXmlNode>(_node.Attributes.Count);
						foreach (global::System.Xml.XmlAttribute attribute in _node.Attributes)
						{
							_attributes.Add(WrapNode(attribute));
						}
					}
				}
				return _attributes;
			}
		}

		private bool HasAttributes
		{
			get
			{
				if (_node is global::System.Xml.XmlElement xmlElement)
				{
					return xmlElement.HasAttributes;
				}
				global::System.Xml.XmlAttributeCollection attributes = _node.Attributes;
				if (attributes == null)
				{
					return false;
				}
				return attributes.Count > 0;
			}
		}

		public global::Newtonsoft.Json.Converters.IXmlNode? ParentNode
		{
			get
			{
				global::System.Xml.XmlNode xmlNode = ((_node is global::System.Xml.XmlAttribute xmlAttribute) ? xmlAttribute.OwnerElement : _node.ParentNode);
				if (xmlNode == null)
				{
					return null;
				}
				return WrapNode(xmlNode);
			}
		}

		public string? Value
		{
			get
			{
				return _node.Value;
			}
			set
			{
				_node.Value = value;
			}
		}

		public string? NamespaceUri => _node.NamespaceURI;

		public XmlNodeWrapper(global::System.Xml.XmlNode node)
		{
			_node = node;
		}

		internal static global::Newtonsoft.Json.Converters.IXmlNode WrapNode(global::System.Xml.XmlNode node)
		{
			return node.NodeType switch
			{
				global::System.Xml.XmlNodeType.Element => new global::Newtonsoft.Json.Converters.XmlElementWrapper((global::System.Xml.XmlElement)node), 
				global::System.Xml.XmlNodeType.XmlDeclaration => new global::Newtonsoft.Json.Converters.XmlDeclarationWrapper((global::System.Xml.XmlDeclaration)node), 
				global::System.Xml.XmlNodeType.DocumentType => new global::Newtonsoft.Json.Converters.XmlDocumentTypeWrapper((global::System.Xml.XmlDocumentType)node), 
				_ => new global::Newtonsoft.Json.Converters.XmlNodeWrapper(node), 
			};
		}

		public global::Newtonsoft.Json.Converters.IXmlNode AppendChild(global::Newtonsoft.Json.Converters.IXmlNode newChild)
		{
			global::Newtonsoft.Json.Converters.XmlNodeWrapper xmlNodeWrapper = (global::Newtonsoft.Json.Converters.XmlNodeWrapper)newChild;
			_node.AppendChild(xmlNodeWrapper._node);
			_childNodes = null;
			_attributes = null;
			return newChild;
		}
	}
}
