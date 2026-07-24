namespace Newtonsoft.Json
{
	[global::System.Serializable]
	public class JsonSerializationException : global::Newtonsoft.Json.JsonException
	{
		public int LineNumber { get; }

		public int LinePosition { get; }

		public string? Path { get; }

		public JsonSerializationException()
		{
		}

		public JsonSerializationException(string message)
			: base(message)
		{
		}

		public JsonSerializationException(string message, global::System.Exception innerException)
			: base(message, innerException)
		{
		}

		public JsonSerializationException(global::System.Runtime.Serialization.SerializationInfo info, global::System.Runtime.Serialization.StreamingContext context)
			: base(info, context)
		{
		}

		public JsonSerializationException(string message, string path, int lineNumber, int linePosition, global::System.Exception? innerException)
			: base(message, innerException)
		{
			Path = path;
			LineNumber = lineNumber;
			LinePosition = linePosition;
		}

		internal static global::Newtonsoft.Json.JsonSerializationException Create(global::Newtonsoft.Json.JsonReader reader, string message)
		{
			return Create(reader, message, null);
		}

		internal static global::Newtonsoft.Json.JsonSerializationException Create(global::Newtonsoft.Json.JsonReader reader, string message, global::System.Exception? ex)
		{
			return Create(reader as global::Newtonsoft.Json.IJsonLineInfo, reader.Path, message, ex);
		}

		internal static global::Newtonsoft.Json.JsonSerializationException Create(global::Newtonsoft.Json.IJsonLineInfo? lineInfo, string path, string message, global::System.Exception? ex)
		{
			message = global::Newtonsoft.Json.JsonPosition.FormatMessage(lineInfo, path, message);
			int lineNumber;
			int linePosition;
			if (lineInfo != null && lineInfo.HasLineInfo())
			{
				lineNumber = lineInfo.LineNumber;
				linePosition = lineInfo.LinePosition;
			}
			else
			{
				lineNumber = 0;
				linePosition = 0;
			}
			return new global::Newtonsoft.Json.JsonSerializationException(message, path, lineNumber, linePosition, ex);
		}
	}
}
