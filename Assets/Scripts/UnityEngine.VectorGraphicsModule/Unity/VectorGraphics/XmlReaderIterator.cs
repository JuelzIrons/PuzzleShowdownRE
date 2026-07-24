namespace Unity.VectorGraphics
{
	internal class XmlReaderIterator
	{
		internal class Node
		{
			private global::System.Xml.XmlReader reader;

			private int depth;

			private string name;

			public string Name => name;

			public string this[string attrib] => reader.GetAttribute(attrib);

			public int Depth => depth;

			public Node(global::System.Xml.XmlReader reader)
			{
				this.reader = reader;
				name = reader.Name;
				depth = reader.Depth;
			}

			public global::Unity.VectorGraphics.SVGPropertySheet GetAttributes()
			{
				global::Unity.VectorGraphics.SVGPropertySheet sVGPropertySheet = new global::Unity.VectorGraphics.SVGPropertySheet();
				for (int i = 0; i < reader.AttributeCount; i++)
				{
					reader.MoveToAttribute(i);
					sVGPropertySheet[reader.Name] = reader.Value;
				}
				reader.MoveToElement();
				return sVGPropertySheet;
			}

			public global::Unity.VectorGraphics.SVGFormatException GetException(string message)
			{
				return new global::Unity.VectorGraphics.SVGFormatException(reader, message);
			}

			public global::Unity.VectorGraphics.SVGFormatException GetUnsupportedAttribValException(string attrib)
			{
				return new global::Unity.VectorGraphics.SVGFormatException(reader, "Value '" + this[attrib] + "' is invalid for attribute '" + attrib + "'");
			}
		}

		private global::System.Xml.XmlReader reader;

		private bool currentElementVisited;

		public XmlReaderIterator(global::System.Xml.XmlReader reader)
		{
			this.reader = reader;
		}

		public bool GoToRoot(string tagName)
		{
			return reader.ReadToFollowing(tagName) && reader.Depth == 0;
		}

		public global::Unity.VectorGraphics.XmlReaderIterator.Node VisitCurrent()
		{
			currentElementVisited = true;
			return new global::Unity.VectorGraphics.XmlReaderIterator.Node(reader);
		}

		public bool IsEmptyElement()
		{
			return reader.IsEmptyElement;
		}

		public bool GoToNextChild(global::Unity.VectorGraphics.XmlReaderIterator.Node node)
		{
			if (!currentElementVisited)
			{
				return reader.Depth == node.Depth + 1;
			}
			reader.Read();
			while (reader.NodeType != global::System.Xml.XmlNodeType.None && reader.NodeType != global::System.Xml.XmlNodeType.Element)
			{
				reader.Read();
			}
			if (reader.NodeType != global::System.Xml.XmlNodeType.Element)
			{
				return false;
			}
			currentElementVisited = false;
			return reader.Depth == node.Depth + 1;
		}

		public void SkipCurrentChildTree(global::Unity.VectorGraphics.XmlReaderIterator.Node node)
		{
			while (GoToNextChild(node))
			{
				SkipCurrentChildTree(VisitCurrent());
			}
		}

		public string ReadTextWithinElement()
		{
			if (reader.IsEmptyElement)
			{
				return "";
			}
			string text = "";
			while (reader.Read() && reader.NodeType != global::System.Xml.XmlNodeType.EndElement)
			{
				text += reader.Value;
			}
			return text;
		}
	}
}
