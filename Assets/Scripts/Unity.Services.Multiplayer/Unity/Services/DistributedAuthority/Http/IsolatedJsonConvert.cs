namespace Unity.Services.DistributedAuthority.Http
{
	internal static class IsolatedJsonConvert
	{
		[global::System.Diagnostics.DebuggerStepThrough]
		public static string SerializeObject(object value)
		{
			return SerializeObject(value, null, null);
		}

		[global::System.Diagnostics.DebuggerStepThrough]
		public static string SerializeObject(object value, global::Newtonsoft.Json.JsonSerializerSettings settings)
		{
			return SerializeObject(value, null, settings);
		}

		[global::System.Diagnostics.DebuggerStepThrough]
		public static string SerializeObject(object value, global::System.Type type, global::Newtonsoft.Json.JsonSerializerSettings settings)
		{
			global::Newtonsoft.Json.JsonSerializer jsonSerializer = global::Newtonsoft.Json.JsonSerializer.Create(settings);
			return SerializeObjectInternal(value, type, jsonSerializer);
		}

		private static string SerializeObjectInternal(object value, global::System.Type type, global::Newtonsoft.Json.JsonSerializer jsonSerializer)
		{
			global::System.IO.StringWriter stringWriter = new global::System.IO.StringWriter(new global::System.Text.StringBuilder(256), global::System.Globalization.CultureInfo.InvariantCulture);
			using (global::Newtonsoft.Json.JsonTextWriter jsonTextWriter = new global::Newtonsoft.Json.JsonTextWriter(stringWriter))
			{
				jsonTextWriter.Formatting = jsonSerializer.Formatting;
				jsonSerializer.Serialize(jsonTextWriter, value, type);
			}
			return stringWriter.ToString();
		}

		[global::System.Diagnostics.DebuggerStepThrough]
		public static object DeserializeObject(string value, global::System.Type type)
		{
			return DeserializeObject(value, type, null);
		}

		[global::System.Diagnostics.DebuggerStepThrough]
		public static T DeserializeObject<T>(string value, global::Newtonsoft.Json.JsonSerializerSettings settings)
		{
			return (T)DeserializeObject(value, typeof(T), settings);
		}

		public static object DeserializeObject(string value, global::System.Type type, global::Newtonsoft.Json.JsonSerializerSettings settings)
		{
			global::Newtonsoft.Json.JsonSerializer jsonSerializer = global::Newtonsoft.Json.JsonSerializer.Create(settings);
			using global::Newtonsoft.Json.JsonTextReader reader = new global::Newtonsoft.Json.JsonTextReader(new global::System.IO.StringReader(value));
			return jsonSerializer.Deserialize(reader, type);
		}
	}
}
