namespace Unity.Services.Core.Internal.Serialization
{
	internal class NewtonsoftSerializer : global::Unity.Services.Core.Internal.Serialization.IJsonSerializer
	{
		private readonly global::Newtonsoft.Json.JsonSerializer m_Serializer;

		public NewtonsoftSerializer(global::Newtonsoft.Json.JsonSerializerSettings settings = null)
			: this(global::Newtonsoft.Json.JsonSerializer.Create(settings))
		{
		}

		internal NewtonsoftSerializer(global::Newtonsoft.Json.JsonSerializer serializer)
		{
			m_Serializer = serializer;
		}

		public string SerializeObject<T>(T value)
		{
			using global::System.IO.StringWriter stringWriter = new global::System.IO.StringWriter(new global::System.Text.StringBuilder(256), global::System.Globalization.CultureInfo.InvariantCulture);
			using global::Newtonsoft.Json.JsonTextWriter jsonTextWriter = new global::Newtonsoft.Json.JsonTextWriter(stringWriter);
			jsonTextWriter.Formatting = m_Serializer.Formatting;
			m_Serializer.Serialize(jsonTextWriter, value, typeof(T));
			return stringWriter.ToString();
		}

		public T DeserializeObject<T>(string value)
		{
			using global::Newtonsoft.Json.JsonTextReader reader = new global::Newtonsoft.Json.JsonTextReader(new global::System.IO.StringReader(value));
			return (T)m_Serializer.Deserialize(reader, typeof(T));
		}
	}
}
