namespace Newtonsoft.Json.Schema
{
	[global::System.Serializable]
	[global::System.Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	public class JsonSchemaException : global::Newtonsoft.Json.JsonException
	{
		public int LineNumber { get; }

		public int LinePosition { get; }

		public string Path { get; }

		public JsonSchemaException()
		{
		}

		public JsonSchemaException(string message)
			: base(message)
		{
		}

		public JsonSchemaException(string message, global::System.Exception innerException)
			: base(message, innerException)
		{
		}

		public JsonSchemaException(global::System.Runtime.Serialization.SerializationInfo info, global::System.Runtime.Serialization.StreamingContext context)
			: base(info, context)
		{
		}

		internal JsonSchemaException(string message, global::System.Exception innerException, string path, int lineNumber, int linePosition)
			: base(message, innerException)
		{
			Path = path;
			LineNumber = lineNumber;
			LinePosition = linePosition;
		}
	}
}
