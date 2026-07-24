namespace Newtonsoft.Json
{
	[global::System.Serializable]
	public class JsonWriterException : global::Newtonsoft.Json.JsonException
	{
		public string? Path { get; }

		public JsonWriterException()
		{
		}

		public JsonWriterException(string message)
			: base(message)
		{
		}

		public JsonWriterException(string message, global::System.Exception innerException)
			: base(message, innerException)
		{
		}

		public JsonWriterException(global::System.Runtime.Serialization.SerializationInfo info, global::System.Runtime.Serialization.StreamingContext context)
			: base(info, context)
		{
		}

		public JsonWriterException(string message, string path, global::System.Exception? innerException)
			: base(message, innerException)
		{
			Path = path;
		}

		internal static global::Newtonsoft.Json.JsonWriterException Create(global::Newtonsoft.Json.JsonWriter writer, string message, global::System.Exception? ex)
		{
			return Create(writer.ContainerPath, message, ex);
		}

		internal static global::Newtonsoft.Json.JsonWriterException Create(string path, string message, global::System.Exception? ex)
		{
			message = global::Newtonsoft.Json.JsonPosition.FormatMessage(null, path, message);
			return new global::Newtonsoft.Json.JsonWriterException(message, path, ex);
		}
	}
}
