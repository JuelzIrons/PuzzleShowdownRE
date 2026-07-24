namespace Newtonsoft.Json.Converters
{
	internal class XContainerWrapper : global::Newtonsoft.Json.Converters.XObjectWrapper
	{
		private global::System.Collections.Generic.List<global::Newtonsoft.Json.Converters.IXmlNode>? _childNodes;

		private global::System.Xml.Linq.XContainer Container => (global::System.Xml.Linq.XContainer)base.WrappedNode;

		public override global::System.Collections.Generic.List<global::Newtonsoft.Json.Converters.IXmlNode> ChildNodes
		{
			get
			{
				if (_childNodes == null)
				{
					if (!HasChildNodes)
					{
						_childNodes = global::Newtonsoft.Json.Converters.XmlNodeConverter.EmptyChildNodes;
					}
					else
					{
						_childNodes = new global::System.Collections.Generic.List<global::Newtonsoft.Json.Converters.IXmlNode>();
						foreach (global::System.Xml.Linq.XNode item in Container.Nodes())
						{
							_childNodes.Add(WrapNode(item));
						}
					}
				}
				return _childNodes;
			}
		}

		protected virtual bool HasChildNodes => Container.LastNode != null;

		public override global::Newtonsoft.Json.Converters.IXmlNode? ParentNode
		{
			get
			{
				if (Container.Parent == null)
				{
					return null;
				}
				return WrapNode(Container.Parent);
			}
		}

		public XContainerWrapper(global::System.Xml.Linq.XContainer container)
			: base(container)
		{
		}

		internal static global::Newtonsoft.Json.Converters.IXmlNode WrapNode(global::System.Xml.Linq.XObject node)
		{
			if (node is global::System.Xml.Linq.XDocument document)
			{
				return new global::Newtonsoft.Json.Converters.XDocumentWrapper(document);
			}
			if (node is global::System.Xml.Linq.XElement element)
			{
				return new global::Newtonsoft.Json.Converters.XElementWrapper(element);
			}
			if (node is global::System.Xml.Linq.XContainer container)
			{
				return new global::Newtonsoft.Json.Converters.XContainerWrapper(container);
			}
			if (node is global::System.Xml.Linq.XProcessingInstruction processingInstruction)
			{
				return new global::Newtonsoft.Json.Converters.XProcessingInstructionWrapper(processingInstruction);
			}
			if (node is global::System.Xml.Linq.XText text)
			{
				return new global::Newtonsoft.Json.Converters.XTextWrapper(text);
			}
			if (node is global::System.Xml.Linq.XComment text2)
			{
				return new global::Newtonsoft.Json.Converters.XCommentWrapper(text2);
			}
			if (node is global::System.Xml.Linq.XAttribute attribute)
			{
				return new global::Newtonsoft.Json.Converters.XAttributeWrapper(attribute);
			}
			if (node is global::System.Xml.Linq.XDocumentType documentType)
			{
				return new global::Newtonsoft.Json.Converters.XDocumentTypeWrapper(documentType);
			}
			return new global::Newtonsoft.Json.Converters.XObjectWrapper(node);
		}

		public override global::Newtonsoft.Json.Converters.IXmlNode AppendChild(global::Newtonsoft.Json.Converters.IXmlNode newChild)
		{
			Container.Add(newChild.WrappedNode);
			_childNodes = null;
			return newChild;
		}
	}
}
