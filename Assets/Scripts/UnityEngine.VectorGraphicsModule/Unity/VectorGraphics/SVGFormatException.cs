namespace Unity.VectorGraphics
{
	internal class SVGFormatException : global::System.Exception
	{
		public static global::Unity.VectorGraphics.SVGFormatException StackError => new global::Unity.VectorGraphics.SVGFormatException("Vector scene construction mismatch");

		public SVGFormatException()
		{
		}

		public SVGFormatException(string message)
			: base(ComposeMessage(null, message))
		{
		}

		public SVGFormatException(global::System.Xml.XmlReader reader, string message)
			: base(ComposeMessage(reader, message))
		{
		}

		private static string ComposeMessage(global::System.Xml.XmlReader reader, string message)
		{
			if (reader is global::System.Xml.IXmlLineInfo xmlLineInfo)
			{
				return "SVG Error (line " + xmlLineInfo.LineNumber + ", character " + xmlLineInfo.LinePosition + "): " + message;
			}
			return "SVG Error: " + message;
		}
	}
}
