namespace Newtonsoft.Json.Converters
{
	public class XmlNodeConverter : global::Newtonsoft.Json.JsonConverter
	{
		internal static readonly global::System.Collections.Generic.List<global::Newtonsoft.Json.Converters.IXmlNode> EmptyChildNodes = new global::System.Collections.Generic.List<global::Newtonsoft.Json.Converters.IXmlNode>();

		private const string TextName = "#text";

		private const string CommentName = "#comment";

		private const string CDataName = "#cdata-section";

		private const string WhitespaceName = "#whitespace";

		private const string SignificantWhitespaceName = "#significant-whitespace";

		private const string DeclarationName = "?xml";

		private const string JsonNamespaceUri = "http://james.newtonking.com/projects/json";

		public string? DeserializeRootElementName { get; set; }

		public bool WriteArrayAttribute { get; set; }

		public bool OmitRootObject { get; set; }

		public bool EncodeSpecialCharacters { get; set; }

		public override void WriteJson(global::Newtonsoft.Json.JsonWriter writer, object? value, global::Newtonsoft.Json.JsonSerializer serializer)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}
			global::Newtonsoft.Json.Converters.IXmlNode node = WrapXml(value);
			global::System.Xml.XmlNamespaceManager manager = new global::System.Xml.XmlNamespaceManager(new global::System.Xml.NameTable());
			PushParentNamespaces(node, manager);
			if (!OmitRootObject)
			{
				writer.WriteStartObject();
			}
			SerializeNode(writer, node, manager, !OmitRootObject);
			if (!OmitRootObject)
			{
				writer.WriteEndObject();
			}
		}

		private global::Newtonsoft.Json.Converters.IXmlNode WrapXml(object value)
		{
			if (value is global::System.Xml.Linq.XObject node)
			{
				return global::Newtonsoft.Json.Converters.XContainerWrapper.WrapNode(node);
			}
			if (value is global::System.Xml.XmlNode node2)
			{
				return global::Newtonsoft.Json.Converters.XmlNodeWrapper.WrapNode(node2);
			}
			throw new global::System.ArgumentException("Value must be an XML object.", "value");
		}

		private void PushParentNamespaces(global::Newtonsoft.Json.Converters.IXmlNode node, global::System.Xml.XmlNamespaceManager manager)
		{
			global::System.Collections.Generic.List<global::Newtonsoft.Json.Converters.IXmlNode> list = null;
			global::Newtonsoft.Json.Converters.IXmlNode xmlNode = node;
			while ((xmlNode = xmlNode.ParentNode) != null)
			{
				if (xmlNode.NodeType == global::System.Xml.XmlNodeType.Element)
				{
					if (list == null)
					{
						list = new global::System.Collections.Generic.List<global::Newtonsoft.Json.Converters.IXmlNode>();
					}
					list.Add(xmlNode);
				}
			}
			if (list == null)
			{
				return;
			}
			list.Reverse();
			foreach (global::Newtonsoft.Json.Converters.IXmlNode item in list)
			{
				manager.PushScope();
				foreach (global::Newtonsoft.Json.Converters.IXmlNode attribute in item.Attributes)
				{
					if (attribute.NamespaceUri == "http://www.w3.org/2000/xmlns/" && attribute.LocalName != "xmlns")
					{
						manager.AddNamespace(attribute.LocalName, attribute.Value);
					}
				}
			}
		}

		private string ResolveFullName(global::Newtonsoft.Json.Converters.IXmlNode node, global::System.Xml.XmlNamespaceManager manager)
		{
			string text = ((node.NamespaceUri == null || (node.LocalName == "xmlns" && node.NamespaceUri == "http://www.w3.org/2000/xmlns/")) ? null : manager.LookupPrefix(node.NamespaceUri));
			if (!global::Newtonsoft.Json.Utilities.StringUtils.IsNullOrEmpty(text))
			{
				return text + ":" + global::System.Xml.XmlConvert.DecodeName(node.LocalName);
			}
			return global::System.Xml.XmlConvert.DecodeName(node.LocalName);
		}

		private string GetPropertyName(global::Newtonsoft.Json.Converters.IXmlNode node, global::System.Xml.XmlNamespaceManager manager)
		{
			switch (node.NodeType)
			{
			case global::System.Xml.XmlNodeType.Attribute:
				if (node.NamespaceUri == "http://james.newtonking.com/projects/json")
				{
					return "$" + node.LocalName;
				}
				return "@" + ResolveFullName(node, manager);
			case global::System.Xml.XmlNodeType.CDATA:
				return "#cdata-section";
			case global::System.Xml.XmlNodeType.Comment:
				return "#comment";
			case global::System.Xml.XmlNodeType.Element:
				if (node.NamespaceUri == "http://james.newtonking.com/projects/json")
				{
					return "$" + node.LocalName;
				}
				return ResolveFullName(node, manager);
			case global::System.Xml.XmlNodeType.ProcessingInstruction:
				return "?" + ResolveFullName(node, manager);
			case global::System.Xml.XmlNodeType.DocumentType:
				return "!" + ResolveFullName(node, manager);
			case global::System.Xml.XmlNodeType.XmlDeclaration:
				return "?xml";
			case global::System.Xml.XmlNodeType.SignificantWhitespace:
				return "#significant-whitespace";
			case global::System.Xml.XmlNodeType.Text:
				return "#text";
			case global::System.Xml.XmlNodeType.Whitespace:
				return "#whitespace";
			default:
				throw new global::Newtonsoft.Json.JsonSerializationException("Unexpected XmlNodeType when getting node name: " + node.NodeType);
			}
		}

		private bool IsArray(global::Newtonsoft.Json.Converters.IXmlNode node)
		{
			foreach (global::Newtonsoft.Json.Converters.IXmlNode attribute in node.Attributes)
			{
				if (attribute.LocalName == "Array" && attribute.NamespaceUri == "http://james.newtonking.com/projects/json")
				{
					return global::System.Xml.XmlConvert.ToBoolean(attribute.Value);
				}
			}
			return false;
		}

		private void SerializeGroupedNodes(global::Newtonsoft.Json.JsonWriter writer, global::Newtonsoft.Json.Converters.IXmlNode node, global::System.Xml.XmlNamespaceManager manager, bool writePropertyName)
		{
			switch (node.ChildNodes.Count)
			{
			case 1:
			{
				string propertyName = GetPropertyName(node.ChildNodes[0], manager);
				WriteGroupedNodes(writer, manager, writePropertyName, node.ChildNodes, propertyName);
				return;
			}
			case 0:
				return;
			}
			global::System.Collections.Generic.Dictionary<string, object> dictionary = null;
			string text = null;
			for (int i = 0; i < node.ChildNodes.Count; i++)
			{
				global::Newtonsoft.Json.Converters.IXmlNode xmlNode = node.ChildNodes[i];
				string propertyName2 = GetPropertyName(xmlNode, manager);
				object value;
				if (dictionary == null)
				{
					if (text == null)
					{
						text = propertyName2;
						continue;
					}
					if (propertyName2 == text)
					{
						continue;
					}
					dictionary = new global::System.Collections.Generic.Dictionary<string, object>();
					if (i > 1)
					{
						global::System.Collections.Generic.List<global::Newtonsoft.Json.Converters.IXmlNode> list = new global::System.Collections.Generic.List<global::Newtonsoft.Json.Converters.IXmlNode>(i);
						for (int j = 0; j < i; j++)
						{
							list.Add(node.ChildNodes[j]);
						}
						dictionary.Add(text, list);
					}
					else
					{
						dictionary.Add(text, node.ChildNodes[0]);
					}
					dictionary.Add(propertyName2, xmlNode);
				}
				else if (!dictionary.TryGetValue(propertyName2, out value))
				{
					dictionary.Add(propertyName2, xmlNode);
				}
				else
				{
					global::System.Collections.Generic.List<global::Newtonsoft.Json.Converters.IXmlNode> list2 = value as global::System.Collections.Generic.List<global::Newtonsoft.Json.Converters.IXmlNode>;
					if (list2 == null)
					{
						list2 = (global::System.Collections.Generic.List<global::Newtonsoft.Json.Converters.IXmlNode>)(dictionary[propertyName2] = new global::System.Collections.Generic.List<global::Newtonsoft.Json.Converters.IXmlNode> { (global::Newtonsoft.Json.Converters.IXmlNode)value });
					}
					list2.Add(xmlNode);
				}
			}
			if (dictionary == null)
			{
				WriteGroupedNodes(writer, manager, writePropertyName, node.ChildNodes, text);
				return;
			}
			foreach (global::System.Collections.Generic.KeyValuePair<string, object> item in dictionary)
			{
				if (item.Value is global::System.Collections.Generic.List<global::Newtonsoft.Json.Converters.IXmlNode> groupedNodes)
				{
					WriteGroupedNodes(writer, manager, writePropertyName, groupedNodes, item.Key);
				}
				else
				{
					WriteGroupedNodes(writer, manager, writePropertyName, (global::Newtonsoft.Json.Converters.IXmlNode)item.Value, item.Key);
				}
			}
		}

		private void WriteGroupedNodes(global::Newtonsoft.Json.JsonWriter writer, global::System.Xml.XmlNamespaceManager manager, bool writePropertyName, global::System.Collections.Generic.List<global::Newtonsoft.Json.Converters.IXmlNode> groupedNodes, string elementNames)
		{
			if (groupedNodes.Count == 1 && !IsArray(groupedNodes[0]))
			{
				SerializeNode(writer, groupedNodes[0], manager, writePropertyName);
				return;
			}
			if (writePropertyName)
			{
				writer.WritePropertyName(elementNames);
			}
			writer.WriteStartArray();
			for (int i = 0; i < groupedNodes.Count; i++)
			{
				SerializeNode(writer, groupedNodes[i], manager, writePropertyName: false);
			}
			writer.WriteEndArray();
		}

		private void WriteGroupedNodes(global::Newtonsoft.Json.JsonWriter writer, global::System.Xml.XmlNamespaceManager manager, bool writePropertyName, global::Newtonsoft.Json.Converters.IXmlNode node, string elementNames)
		{
			if (!IsArray(node))
			{
				SerializeNode(writer, node, manager, writePropertyName);
				return;
			}
			if (writePropertyName)
			{
				writer.WritePropertyName(elementNames);
			}
			writer.WriteStartArray();
			SerializeNode(writer, node, manager, writePropertyName: false);
			writer.WriteEndArray();
		}

		private void SerializeNode(global::Newtonsoft.Json.JsonWriter writer, global::Newtonsoft.Json.Converters.IXmlNode node, global::System.Xml.XmlNamespaceManager manager, bool writePropertyName)
		{
			switch (node.NodeType)
			{
			case global::System.Xml.XmlNodeType.Document:
			case global::System.Xml.XmlNodeType.DocumentFragment:
				SerializeGroupedNodes(writer, node, manager, writePropertyName);
				break;
			case global::System.Xml.XmlNodeType.Element:
				if (IsArray(node) && AllSameName(node) && node.ChildNodes.Count > 0)
				{
					SerializeGroupedNodes(writer, node, manager, writePropertyName: false);
					break;
				}
				manager.PushScope();
				foreach (global::Newtonsoft.Json.Converters.IXmlNode attribute in node.Attributes)
				{
					if (attribute.NamespaceUri == "http://www.w3.org/2000/xmlns/")
					{
						string prefix = ((attribute.LocalName != "xmlns") ? global::System.Xml.XmlConvert.DecodeName(attribute.LocalName) : string.Empty);
						string value = attribute.Value;
						if (value == null)
						{
							throw new global::Newtonsoft.Json.JsonSerializationException("Namespace attribute must have a value.");
						}
						manager.AddNamespace(prefix, value);
					}
				}
				if (writePropertyName)
				{
					writer.WritePropertyName(GetPropertyName(node, manager));
				}
				if (!ValueAttributes(node.Attributes) && node.ChildNodes.Count == 1 && node.ChildNodes[0].NodeType == global::System.Xml.XmlNodeType.Text)
				{
					writer.WriteValue(node.ChildNodes[0].Value);
				}
				else if (node.ChildNodes.Count == 0 && node.Attributes.Count == 0)
				{
					if (((global::Newtonsoft.Json.Converters.IXmlElement)node).IsEmpty)
					{
						writer.WriteNull();
					}
					else
					{
						writer.WriteValue(string.Empty);
					}
				}
				else
				{
					writer.WriteStartObject();
					for (int i = 0; i < node.Attributes.Count; i++)
					{
						SerializeNode(writer, node.Attributes[i], manager, writePropertyName: true);
					}
					SerializeGroupedNodes(writer, node, manager, writePropertyName: true);
					writer.WriteEndObject();
				}
				manager.PopScope();
				break;
			case global::System.Xml.XmlNodeType.Comment:
				if (writePropertyName)
				{
					writer.WriteComment(node.Value);
				}
				break;
			case global::System.Xml.XmlNodeType.Attribute:
			case global::System.Xml.XmlNodeType.Text:
			case global::System.Xml.XmlNodeType.CDATA:
			case global::System.Xml.XmlNodeType.ProcessingInstruction:
			case global::System.Xml.XmlNodeType.Whitespace:
			case global::System.Xml.XmlNodeType.SignificantWhitespace:
				if ((!(node.NamespaceUri == "http://www.w3.org/2000/xmlns/") || !(node.Value == "http://james.newtonking.com/projects/json")) && (!(node.NamespaceUri == "http://james.newtonking.com/projects/json") || !(node.LocalName == "Array")))
				{
					if (writePropertyName)
					{
						writer.WritePropertyName(GetPropertyName(node, manager));
					}
					writer.WriteValue(node.Value);
				}
				break;
			case global::System.Xml.XmlNodeType.XmlDeclaration:
			{
				global::Newtonsoft.Json.Converters.IXmlDeclaration xmlDeclaration = (global::Newtonsoft.Json.Converters.IXmlDeclaration)node;
				writer.WritePropertyName(GetPropertyName(node, manager));
				writer.WriteStartObject();
				if (!global::Newtonsoft.Json.Utilities.StringUtils.IsNullOrEmpty(xmlDeclaration.Version))
				{
					writer.WritePropertyName("@version");
					writer.WriteValue(xmlDeclaration.Version);
				}
				if (!global::Newtonsoft.Json.Utilities.StringUtils.IsNullOrEmpty(xmlDeclaration.Encoding))
				{
					writer.WritePropertyName("@encoding");
					writer.WriteValue(xmlDeclaration.Encoding);
				}
				if (!global::Newtonsoft.Json.Utilities.StringUtils.IsNullOrEmpty(xmlDeclaration.Standalone))
				{
					writer.WritePropertyName("@standalone");
					writer.WriteValue(xmlDeclaration.Standalone);
				}
				writer.WriteEndObject();
				break;
			}
			case global::System.Xml.XmlNodeType.DocumentType:
			{
				global::Newtonsoft.Json.Converters.IXmlDocumentType xmlDocumentType = (global::Newtonsoft.Json.Converters.IXmlDocumentType)node;
				writer.WritePropertyName(GetPropertyName(node, manager));
				writer.WriteStartObject();
				if (!global::Newtonsoft.Json.Utilities.StringUtils.IsNullOrEmpty(xmlDocumentType.Name))
				{
					writer.WritePropertyName("@name");
					writer.WriteValue(xmlDocumentType.Name);
				}
				if (!global::Newtonsoft.Json.Utilities.StringUtils.IsNullOrEmpty(xmlDocumentType.Public))
				{
					writer.WritePropertyName("@public");
					writer.WriteValue(xmlDocumentType.Public);
				}
				if (!global::Newtonsoft.Json.Utilities.StringUtils.IsNullOrEmpty(xmlDocumentType.System))
				{
					writer.WritePropertyName("@system");
					writer.WriteValue(xmlDocumentType.System);
				}
				if (!global::Newtonsoft.Json.Utilities.StringUtils.IsNullOrEmpty(xmlDocumentType.InternalSubset))
				{
					writer.WritePropertyName("@internalSubset");
					writer.WriteValue(xmlDocumentType.InternalSubset);
				}
				writer.WriteEndObject();
				break;
			}
			default:
				throw new global::Newtonsoft.Json.JsonSerializationException("Unexpected XmlNodeType when serializing nodes: " + node.NodeType);
			}
		}

		private static bool AllSameName(global::Newtonsoft.Json.Converters.IXmlNode node)
		{
			foreach (global::Newtonsoft.Json.Converters.IXmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName != node.LocalName)
				{
					return false;
				}
			}
			return true;
		}

		public override object? ReadJson(global::Newtonsoft.Json.JsonReader reader, global::System.Type objectType, object? existingValue, global::Newtonsoft.Json.JsonSerializer serializer)
		{
			switch (reader.TokenType)
			{
			case global::Newtonsoft.Json.JsonToken.Null:
				return null;
			default:
				throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, "XmlNodeConverter can only convert JSON that begins with an object.");
			case global::Newtonsoft.Json.JsonToken.StartObject:
			{
				global::System.Xml.XmlNamespaceManager manager = new global::System.Xml.XmlNamespaceManager(new global::System.Xml.NameTable());
				global::Newtonsoft.Json.Converters.IXmlDocument xmlDocument = null;
				global::Newtonsoft.Json.Converters.IXmlNode xmlNode = null;
				if (typeof(global::System.Xml.Linq.XObject).IsAssignableFrom(objectType))
				{
					if (objectType != typeof(global::System.Xml.Linq.XContainer) && objectType != typeof(global::System.Xml.Linq.XDocument) && objectType != typeof(global::System.Xml.Linq.XElement) && objectType != typeof(global::System.Xml.Linq.XNode) && objectType != typeof(global::System.Xml.Linq.XObject))
					{
						throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, "XmlNodeConverter only supports deserializing XDocument, XElement, XContainer, XNode or XObject.");
					}
					xmlDocument = new global::Newtonsoft.Json.Converters.XDocumentWrapper(new global::System.Xml.Linq.XDocument());
					xmlNode = xmlDocument;
				}
				if (typeof(global::System.Xml.XmlNode).IsAssignableFrom(objectType))
				{
					if (objectType != typeof(global::System.Xml.XmlDocument) && objectType != typeof(global::System.Xml.XmlElement) && objectType != typeof(global::System.Xml.XmlNode))
					{
						throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, "XmlNodeConverter only supports deserializing XmlDocument, XmlElement or XmlNode.");
					}
					xmlDocument = new global::Newtonsoft.Json.Converters.XmlDocumentWrapper(new global::System.Xml.XmlDocument
					{
						XmlResolver = null
					});
					xmlNode = xmlDocument;
				}
				if (xmlDocument == null || xmlNode == null)
				{
					throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, "Unexpected type when converting XML: " + objectType);
				}
				if (!global::Newtonsoft.Json.Utilities.StringUtils.IsNullOrEmpty(DeserializeRootElementName))
				{
					ReadElement(reader, xmlDocument, xmlNode, DeserializeRootElementName, manager);
				}
				else
				{
					reader.ReadAndAssert();
					DeserializeNode(reader, xmlDocument, manager, xmlNode);
				}
				if (objectType == typeof(global::System.Xml.Linq.XElement))
				{
					global::System.Xml.Linq.XElement obj = (global::System.Xml.Linq.XElement)xmlDocument.DocumentElement.WrappedNode;
					obj.Remove();
					return obj;
				}
				if (objectType == typeof(global::System.Xml.XmlElement))
				{
					return xmlDocument.DocumentElement.WrappedNode;
				}
				return xmlDocument.WrappedNode;
			}
			}
		}

		private void DeserializeValue(global::Newtonsoft.Json.JsonReader reader, global::Newtonsoft.Json.Converters.IXmlDocument document, global::System.Xml.XmlNamespaceManager manager, string propertyName, global::Newtonsoft.Json.Converters.IXmlNode currentNode)
		{
			if (!EncodeSpecialCharacters)
			{
				switch (propertyName)
				{
				case "#text":
					currentNode.AppendChild(document.CreateTextNode(ConvertTokenToXmlValue(reader)));
					return;
				case "#cdata-section":
					currentNode.AppendChild(document.CreateCDataSection(ConvertTokenToXmlValue(reader)));
					return;
				case "#whitespace":
					currentNode.AppendChild(document.CreateWhitespace(ConvertTokenToXmlValue(reader)));
					return;
				case "#significant-whitespace":
					currentNode.AppendChild(document.CreateSignificantWhitespace(ConvertTokenToXmlValue(reader)));
					return;
				}
				if (!global::Newtonsoft.Json.Utilities.StringUtils.IsNullOrEmpty(propertyName) && propertyName[0] == '?')
				{
					CreateInstruction(reader, document, currentNode, propertyName);
					return;
				}
				if (string.Equals(propertyName, "!DOCTYPE", global::System.StringComparison.OrdinalIgnoreCase))
				{
					CreateDocumentType(reader, document, currentNode);
					return;
				}
			}
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.StartArray)
			{
				ReadArrayElements(reader, document, propertyName, currentNode, manager);
			}
			else
			{
				ReadElement(reader, document, currentNode, propertyName, manager);
			}
		}

		private void ReadElement(global::Newtonsoft.Json.JsonReader reader, global::Newtonsoft.Json.Converters.IXmlDocument document, global::Newtonsoft.Json.Converters.IXmlNode currentNode, string propertyName, global::System.Xml.XmlNamespaceManager manager)
		{
			if (global::Newtonsoft.Json.Utilities.StringUtils.IsNullOrEmpty(propertyName))
			{
				throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, "XmlNodeConverter cannot convert JSON with an empty property name to XML.");
			}
			global::System.Collections.Generic.Dictionary<string, string> attributeNameValues = null;
			string elementPrefix = null;
			if (!EncodeSpecialCharacters)
			{
				attributeNameValues = (ShouldReadInto(reader) ? ReadAttributeElements(reader, manager) : null);
				elementPrefix = global::Newtonsoft.Json.Utilities.MiscellaneousUtils.GetPrefix(propertyName);
				if (global::Newtonsoft.Json.Utilities.StringUtils.StartsWith(propertyName, '@'))
				{
					string text = propertyName.Substring(1);
					string prefix = global::Newtonsoft.Json.Utilities.MiscellaneousUtils.GetPrefix(text);
					AddAttribute(reader, document, currentNode, propertyName, text, manager, prefix);
					return;
				}
				if (global::Newtonsoft.Json.Utilities.StringUtils.StartsWith(propertyName, '$'))
				{
					switch (propertyName)
					{
					case "$values":
						propertyName = propertyName.Substring(1);
						elementPrefix = manager.LookupPrefix("http://james.newtonking.com/projects/json");
						CreateElement(reader, document, currentNode, propertyName, manager, elementPrefix, attributeNameValues);
						return;
					case "$id":
					case "$ref":
					case "$type":
					case "$value":
					{
						string attributeName = propertyName.Substring(1);
						string attributePrefix = manager.LookupPrefix("http://james.newtonking.com/projects/json");
						AddAttribute(reader, document, currentNode, propertyName, attributeName, manager, attributePrefix);
						return;
					}
					}
				}
			}
			else if (ShouldReadInto(reader))
			{
				reader.ReadAndAssert();
			}
			CreateElement(reader, document, currentNode, propertyName, manager, elementPrefix, attributeNameValues);
		}

		private void CreateElement(global::Newtonsoft.Json.JsonReader reader, global::Newtonsoft.Json.Converters.IXmlDocument document, global::Newtonsoft.Json.Converters.IXmlNode currentNode, string elementName, global::System.Xml.XmlNamespaceManager manager, string? elementPrefix, global::System.Collections.Generic.Dictionary<string, string?>? attributeNameValues)
		{
			global::Newtonsoft.Json.Converters.IXmlElement xmlElement = CreateElement(elementName, document, elementPrefix, manager);
			currentNode.AppendChild(xmlElement);
			if (attributeNameValues != null)
			{
				foreach (global::System.Collections.Generic.KeyValuePair<string, string> attributeNameValue in attributeNameValues)
				{
					string text = global::System.Xml.XmlConvert.EncodeName(attributeNameValue.Key);
					string prefix = global::Newtonsoft.Json.Utilities.MiscellaneousUtils.GetPrefix(attributeNameValue.Key);
					global::Newtonsoft.Json.Converters.IXmlNode attributeNode = ((!global::Newtonsoft.Json.Utilities.StringUtils.IsNullOrEmpty(prefix)) ? document.CreateAttribute(text, manager.LookupNamespace(prefix) ?? string.Empty, attributeNameValue.Value) : document.CreateAttribute(text, attributeNameValue.Value));
					xmlElement.SetAttributeNode(attributeNode);
				}
			}
			switch (reader.TokenType)
			{
			case global::Newtonsoft.Json.JsonToken.Integer:
			case global::Newtonsoft.Json.JsonToken.Float:
			case global::Newtonsoft.Json.JsonToken.String:
			case global::Newtonsoft.Json.JsonToken.Boolean:
			case global::Newtonsoft.Json.JsonToken.Date:
			case global::Newtonsoft.Json.JsonToken.Bytes:
			{
				string text2 = ConvertTokenToXmlValue(reader);
				if (text2 != null)
				{
					xmlElement.AppendChild(document.CreateTextNode(text2));
				}
				break;
			}
			case global::Newtonsoft.Json.JsonToken.EndObject:
				manager.RemoveNamespace(string.Empty, manager.DefaultNamespace);
				break;
			default:
				manager.PushScope();
				DeserializeNode(reader, document, manager, xmlElement);
				manager.PopScope();
				manager.RemoveNamespace(string.Empty, manager.DefaultNamespace);
				break;
			case global::Newtonsoft.Json.JsonToken.Null:
				break;
			}
		}

		private static void AddAttribute(global::Newtonsoft.Json.JsonReader reader, global::Newtonsoft.Json.Converters.IXmlDocument document, global::Newtonsoft.Json.Converters.IXmlNode currentNode, string propertyName, string attributeName, global::System.Xml.XmlNamespaceManager manager, string? attributePrefix)
		{
			if (currentNode.NodeType == global::System.Xml.XmlNodeType.Document)
			{
				throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("JSON root object has property '{0}' that will be converted to an attribute. A root object cannot have any attribute properties. Consider specifying a DeserializeRootElementName.", global::System.Globalization.CultureInfo.InvariantCulture, propertyName));
			}
			string text = global::System.Xml.XmlConvert.EncodeName(attributeName);
			string value = ConvertTokenToXmlValue(reader);
			global::Newtonsoft.Json.Converters.IXmlNode attributeNode = ((!global::Newtonsoft.Json.Utilities.StringUtils.IsNullOrEmpty(attributePrefix)) ? document.CreateAttribute(text, manager.LookupNamespace(attributePrefix), value) : document.CreateAttribute(text, value));
			((global::Newtonsoft.Json.Converters.IXmlElement)currentNode).SetAttributeNode(attributeNode);
		}

		private static string? ConvertTokenToXmlValue(global::Newtonsoft.Json.JsonReader reader)
		{
			switch (reader.TokenType)
			{
			case global::Newtonsoft.Json.JsonToken.String:
				return reader.Value?.ToString();
			case global::Newtonsoft.Json.JsonToken.Integer:
				if (reader.Value is global::System.Numerics.BigInteger bigInteger)
				{
					return bigInteger.ToString(global::System.Globalization.CultureInfo.InvariantCulture);
				}
				return global::System.Xml.XmlConvert.ToString(global::System.Convert.ToInt64(reader.Value, global::System.Globalization.CultureInfo.InvariantCulture));
			case global::Newtonsoft.Json.JsonToken.Float:
				if (reader.Value is decimal num)
				{
					return global::System.Xml.XmlConvert.ToString(num);
				}
				if (reader.Value is float num2)
				{
					return global::System.Xml.XmlConvert.ToString(num2);
				}
				return global::System.Xml.XmlConvert.ToString(global::System.Convert.ToDouble(reader.Value, global::System.Globalization.CultureInfo.InvariantCulture));
			case global::Newtonsoft.Json.JsonToken.Boolean:
				return global::System.Xml.XmlConvert.ToString(global::System.Convert.ToBoolean(reader.Value, global::System.Globalization.CultureInfo.InvariantCulture));
			case global::Newtonsoft.Json.JsonToken.Date:
			{
				if (reader.Value is global::System.DateTimeOffset dateTimeOffset)
				{
					return global::System.Xml.XmlConvert.ToString(dateTimeOffset);
				}
				global::System.DateTime dateTime = global::System.Convert.ToDateTime(reader.Value, global::System.Globalization.CultureInfo.InvariantCulture);
				return global::System.Xml.XmlConvert.ToString(dateTime, global::Newtonsoft.Json.Utilities.DateTimeUtils.ToSerializationMode(dateTime.Kind));
			}
			case global::Newtonsoft.Json.JsonToken.Bytes:
				return global::System.Convert.ToBase64String((byte[])reader.Value);
			case global::Newtonsoft.Json.JsonToken.Null:
				return null;
			default:
				throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Cannot get an XML string value from token type '{0}'.", global::System.Globalization.CultureInfo.InvariantCulture, reader.TokenType));
			}
		}

		private void ReadArrayElements(global::Newtonsoft.Json.JsonReader reader, global::Newtonsoft.Json.Converters.IXmlDocument document, string propertyName, global::Newtonsoft.Json.Converters.IXmlNode currentNode, global::System.Xml.XmlNamespaceManager manager)
		{
			string prefix = global::Newtonsoft.Json.Utilities.MiscellaneousUtils.GetPrefix(propertyName);
			global::Newtonsoft.Json.Converters.IXmlElement xmlElement = CreateElement(propertyName, document, prefix, manager);
			currentNode.AppendChild(xmlElement);
			int num = 0;
			while (reader.Read() && reader.TokenType != global::Newtonsoft.Json.JsonToken.EndArray)
			{
				DeserializeValue(reader, document, manager, propertyName, xmlElement);
				num++;
			}
			if (WriteArrayAttribute)
			{
				AddJsonArrayAttribute(xmlElement, document);
			}
			if (num != 1 || !WriteArrayAttribute)
			{
				return;
			}
			foreach (global::Newtonsoft.Json.Converters.IXmlNode childNode in xmlElement.ChildNodes)
			{
				if (childNode is global::Newtonsoft.Json.Converters.IXmlElement xmlElement2 && xmlElement2.LocalName == propertyName)
				{
					AddJsonArrayAttribute(xmlElement2, document);
					break;
				}
			}
		}

		private void AddJsonArrayAttribute(global::Newtonsoft.Json.Converters.IXmlElement element, global::Newtonsoft.Json.Converters.IXmlDocument document)
		{
			element.SetAttributeNode(document.CreateAttribute("json:Array", "http://james.newtonking.com/projects/json", "true"));
			if (element is global::Newtonsoft.Json.Converters.XElementWrapper && element.GetPrefixOfNamespace("http://james.newtonking.com/projects/json") == null)
			{
				element.SetAttributeNode(document.CreateAttribute("xmlns:json", "http://www.w3.org/2000/xmlns/", "http://james.newtonking.com/projects/json"));
			}
		}

		private bool ShouldReadInto(global::Newtonsoft.Json.JsonReader reader)
		{
			switch (reader.TokenType)
			{
			case global::Newtonsoft.Json.JsonToken.StartConstructor:
			case global::Newtonsoft.Json.JsonToken.Integer:
			case global::Newtonsoft.Json.JsonToken.Float:
			case global::Newtonsoft.Json.JsonToken.String:
			case global::Newtonsoft.Json.JsonToken.Boolean:
			case global::Newtonsoft.Json.JsonToken.Null:
			case global::Newtonsoft.Json.JsonToken.Date:
			case global::Newtonsoft.Json.JsonToken.Bytes:
				return false;
			default:
				return true;
			}
		}

		private global::System.Collections.Generic.Dictionary<string, string?>? ReadAttributeElements(global::Newtonsoft.Json.JsonReader reader, global::System.Xml.XmlNamespaceManager manager)
		{
			global::System.Collections.Generic.Dictionary<string, string> dictionary = null;
			bool flag = false;
			while (!flag && reader.Read())
			{
				switch (reader.TokenType)
				{
				case global::Newtonsoft.Json.JsonToken.PropertyName:
				{
					string text = reader.Value.ToString();
					if (!global::Newtonsoft.Json.Utilities.StringUtils.IsNullOrEmpty(text))
					{
						switch (text[0])
						{
						case '@':
						{
							if (dictionary == null)
							{
								dictionary = new global::System.Collections.Generic.Dictionary<string, string>();
							}
							text = text.Substring(1);
							reader.ReadAndAssert();
							string value = ConvertTokenToXmlValue(reader);
							dictionary.Add(text, value);
							if (IsNamespaceAttribute(text, out string prefix))
							{
								manager.AddNamespace(prefix, value);
							}
							break;
						}
						case '$':
							switch (text)
							{
							case "$values":
							case "$id":
							case "$ref":
							case "$type":
							case "$value":
							{
								string text2 = manager.LookupPrefix("http://james.newtonking.com/projects/json");
								if (text2 == null)
								{
									if (dictionary == null)
									{
										dictionary = new global::System.Collections.Generic.Dictionary<string, string>();
									}
									int? num = null;
									int? num2;
									while (true)
									{
										num2 = num;
										if (manager.LookupNamespace("json" + num2) == null)
										{
											break;
										}
										num = num.GetValueOrDefault() + 1;
									}
									num2 = num;
									text2 = "json" + num2;
									dictionary.Add("xmlns:" + text2, "http://james.newtonking.com/projects/json");
									manager.AddNamespace(text2, "http://james.newtonking.com/projects/json");
								}
								if (text == "$values")
								{
									flag = true;
									break;
								}
								text = text.Substring(1);
								reader.ReadAndAssert();
								if (!global::Newtonsoft.Json.Utilities.JsonTokenUtils.IsPrimitiveToken(reader.TokenType))
								{
									throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, "Unexpected JsonToken: " + reader.TokenType);
								}
								if (dictionary == null)
								{
									dictionary = new global::System.Collections.Generic.Dictionary<string, string>();
								}
								string value = reader.Value?.ToString();
								dictionary.Add(text2 + ":" + text, value);
								break;
							}
							default:
								flag = true;
								break;
							}
							break;
						default:
							flag = true;
							break;
						}
					}
					else
					{
						flag = true;
					}
					break;
				}
				case global::Newtonsoft.Json.JsonToken.Comment:
				case global::Newtonsoft.Json.JsonToken.EndObject:
					flag = true;
					break;
				default:
					throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, "Unexpected JsonToken: " + reader.TokenType);
				}
			}
			return dictionary;
		}

		private void CreateInstruction(global::Newtonsoft.Json.JsonReader reader, global::Newtonsoft.Json.Converters.IXmlDocument document, global::Newtonsoft.Json.Converters.IXmlNode currentNode, string propertyName)
		{
			if (propertyName == "?xml")
			{
				string text = null;
				string encoding = null;
				string standalone = null;
				while (reader.Read() && reader.TokenType != global::Newtonsoft.Json.JsonToken.EndObject)
				{
					switch (reader.Value?.ToString())
					{
					case "@version":
						reader.ReadAndAssert();
						text = ConvertTokenToXmlValue(reader);
						break;
					case "@encoding":
						reader.ReadAndAssert();
						encoding = ConvertTokenToXmlValue(reader);
						break;
					case "@standalone":
						reader.ReadAndAssert();
						standalone = ConvertTokenToXmlValue(reader);
						break;
					default:
						throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, "Unexpected property name encountered while deserializing XmlDeclaration: " + reader.Value);
					}
				}
				if (text == null)
				{
					throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, "Version not specified for XML declaration.");
				}
				global::Newtonsoft.Json.Converters.IXmlNode newChild = document.CreateXmlDeclaration(text, encoding, standalone);
				currentNode.AppendChild(newChild);
			}
			else
			{
				global::Newtonsoft.Json.Converters.IXmlNode newChild2 = document.CreateProcessingInstruction(propertyName.Substring(1), ConvertTokenToXmlValue(reader));
				currentNode.AppendChild(newChild2);
			}
		}

		private void CreateDocumentType(global::Newtonsoft.Json.JsonReader reader, global::Newtonsoft.Json.Converters.IXmlDocument document, global::Newtonsoft.Json.Converters.IXmlNode currentNode)
		{
			string text = null;
			string publicId = null;
			string systemId = null;
			string internalSubset = null;
			while (reader.Read() && reader.TokenType != global::Newtonsoft.Json.JsonToken.EndObject)
			{
				switch (reader.Value?.ToString())
				{
				case "@name":
					reader.ReadAndAssert();
					text = ConvertTokenToXmlValue(reader);
					break;
				case "@public":
					reader.ReadAndAssert();
					publicId = ConvertTokenToXmlValue(reader);
					break;
				case "@system":
					reader.ReadAndAssert();
					systemId = ConvertTokenToXmlValue(reader);
					break;
				case "@internalSubset":
					reader.ReadAndAssert();
					internalSubset = ConvertTokenToXmlValue(reader);
					break;
				default:
					throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, "Unexpected property name encountered while deserializing XmlDeclaration: " + reader.Value);
				}
			}
			if (text == null)
			{
				throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, "Name not specified for XML document type.");
			}
			global::Newtonsoft.Json.Converters.IXmlNode newChild = document.CreateXmlDocumentType(text, publicId, systemId, internalSubset);
			currentNode.AppendChild(newChild);
		}

		private global::Newtonsoft.Json.Converters.IXmlElement CreateElement(string elementName, global::Newtonsoft.Json.Converters.IXmlDocument document, string? elementPrefix, global::System.Xml.XmlNamespaceManager manager)
		{
			string text = (EncodeSpecialCharacters ? global::System.Xml.XmlConvert.EncodeLocalName(elementName) : global::System.Xml.XmlConvert.EncodeName(elementName));
			string text2 = (global::Newtonsoft.Json.Utilities.StringUtils.IsNullOrEmpty(elementPrefix) ? manager.DefaultNamespace : manager.LookupNamespace(elementPrefix));
			if (global::Newtonsoft.Json.Utilities.StringUtils.IsNullOrEmpty(text2))
			{
				return document.CreateElement(text);
			}
			return document.CreateElement(text, text2);
		}

		private void DeserializeNode(global::Newtonsoft.Json.JsonReader reader, global::Newtonsoft.Json.Converters.IXmlDocument document, global::System.Xml.XmlNamespaceManager manager, global::Newtonsoft.Json.Converters.IXmlNode currentNode)
		{
			do
			{
				switch (reader.TokenType)
				{
				case global::Newtonsoft.Json.JsonToken.PropertyName:
				{
					if (currentNode.NodeType == global::System.Xml.XmlNodeType.Document && document.DocumentElement != null)
					{
						throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, "JSON root object has multiple properties. The root object must have a single property in order to create a valid XML document. Consider specifying a DeserializeRootElementName.");
					}
					string text = reader.Value.ToString();
					reader.ReadAndAssert();
					if (reader.TokenType == global::Newtonsoft.Json.JsonToken.StartArray)
					{
						int num = 0;
						while (reader.Read() && reader.TokenType != global::Newtonsoft.Json.JsonToken.EndArray)
						{
							DeserializeValue(reader, document, manager, text, currentNode);
							num++;
						}
						if (num != 1 || !WriteArrayAttribute)
						{
							break;
						}
						global::Newtonsoft.Json.Utilities.MiscellaneousUtils.GetQualifiedNameParts(text, out string prefix, out string localName);
						string text2 = (global::Newtonsoft.Json.Utilities.StringUtils.IsNullOrEmpty(prefix) ? manager.DefaultNamespace : manager.LookupNamespace(prefix));
						foreach (global::Newtonsoft.Json.Converters.IXmlNode childNode in currentNode.ChildNodes)
						{
							if (childNode is global::Newtonsoft.Json.Converters.IXmlElement xmlElement && xmlElement.LocalName == localName && xmlElement.NamespaceUri == text2)
							{
								AddJsonArrayAttribute(xmlElement, document);
								break;
							}
						}
					}
					else
					{
						DeserializeValue(reader, document, manager, text, currentNode);
					}
					break;
				}
				case global::Newtonsoft.Json.JsonToken.StartConstructor:
				{
					string propertyName = reader.Value.ToString();
					while (reader.Read() && reader.TokenType != global::Newtonsoft.Json.JsonToken.EndConstructor)
					{
						DeserializeValue(reader, document, manager, propertyName, currentNode);
					}
					break;
				}
				case global::Newtonsoft.Json.JsonToken.Comment:
					currentNode.AppendChild(document.CreateComment((string)reader.Value));
					break;
				case global::Newtonsoft.Json.JsonToken.EndObject:
				case global::Newtonsoft.Json.JsonToken.EndArray:
					return;
				default:
					throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, "Unexpected JsonToken when deserializing node: " + reader.TokenType);
				}
			}
			while (reader.Read());
		}

		private bool IsNamespaceAttribute(string attributeName, [global::System.Diagnostics.CodeAnalysis.NotNullWhen(true)] out string? prefix)
		{
			if (attributeName.StartsWith("xmlns", global::System.StringComparison.Ordinal))
			{
				if (attributeName.Length == 5)
				{
					prefix = string.Empty;
					return true;
				}
				if (attributeName[5] == ':')
				{
					prefix = attributeName.Substring(6, attributeName.Length - 6);
					return true;
				}
			}
			prefix = null;
			return false;
		}

		private bool ValueAttributes(global::System.Collections.Generic.List<global::Newtonsoft.Json.Converters.IXmlNode> c)
		{
			foreach (global::Newtonsoft.Json.Converters.IXmlNode item in c)
			{
				if (!(item.NamespaceUri == "http://james.newtonking.com/projects/json") && (!(item.NamespaceUri == "http://www.w3.org/2000/xmlns/") || !(item.Value == "http://james.newtonking.com/projects/json")))
				{
					return true;
				}
			}
			return false;
		}

		public override bool CanConvert(global::System.Type valueType)
		{
			if (global::Newtonsoft.Json.Utilities.TypeExtensions.AssignableToTypeName(valueType, "System.Xml.Linq.XObject", searchInterfaces: false))
			{
				return IsXObject(valueType);
			}
			if (global::Newtonsoft.Json.Utilities.TypeExtensions.AssignableToTypeName(valueType, "System.Xml.XmlNode", searchInterfaces: false))
			{
				return IsXmlNode(valueType);
			}
			return false;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
		private bool IsXObject(global::System.Type valueType)
		{
			return typeof(global::System.Xml.Linq.XObject).IsAssignableFrom(valueType);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
		private bool IsXmlNode(global::System.Type valueType)
		{
			return typeof(global::System.Xml.XmlNode).IsAssignableFrom(valueType);
		}
	}
}
