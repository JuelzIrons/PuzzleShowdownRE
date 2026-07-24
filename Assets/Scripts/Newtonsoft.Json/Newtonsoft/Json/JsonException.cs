namespace Newtonsoft.Json
{
	[global::System.Serializable]
	public class JsonException : global::System.Exception
	{
		public JsonException()
		{
		}

		public JsonException(string message)
			: base(message)
		{
		}

		public JsonException(string message, global::System.Exception? innerException)
			: base(message, innerException)
		{
		}

		public JsonException(global::System.Runtime.Serialization.SerializationInfo info, global::System.Runtime.Serialization.StreamingContext context)
			: base(info, context)
		{
		}

		internal static global::Newtonsoft.Json.JsonException Create(global::Newtonsoft.Json.IJsonLineInfo lineInfo, string path, string message)
		{
			message = global::Newtonsoft.Json.JsonPosition.FormatMessage(lineInfo, path, message);
			return new global::Newtonsoft.Json.JsonException(message);
		}
	}
}
