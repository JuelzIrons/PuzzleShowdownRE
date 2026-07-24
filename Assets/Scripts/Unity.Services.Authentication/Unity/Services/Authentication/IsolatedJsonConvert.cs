namespace Unity.Services.Authentication
{
	internal static class IsolatedJsonConvert
	{
		[global::System.Diagnostics.DebuggerStepThrough]
		public static string SerializeObject(object value)
		{
			return SerializeObject(value, (global::System.Type)null, (global::Newtonsoft.Json.JsonSerializerSettings)null);
		}

		[global::System.Diagnostics.DebuggerStepThrough]
		public static string SerializeObject(object value, global::Newtonsoft.Json.Formatting formatting)
		{
			return SerializeObject(value, formatting, (global::Newtonsoft.Json.JsonSerializerSettings)null);
		}

		[global::System.Diagnostics.DebuggerStepThrough]
		public static string SerializeObject(object value, params global::Newtonsoft.Json.JsonConverter[] converters)
		{
			global::Newtonsoft.Json.JsonSerializerSettings jsonSerializerSettings = ((converters != null && converters.Length != 0) ? new global::Newtonsoft.Json.JsonSerializerSettings
			{
				Converters = converters
			} : null);
			global::Newtonsoft.Json.JsonSerializerSettings settings = jsonSerializerSettings;
			return SerializeObject(value, null, settings);
		}

		[global::System.Diagnostics.DebuggerStepThrough]
		public static string SerializeObject(object value, global::Newtonsoft.Json.Formatting formatting, params global::Newtonsoft.Json.JsonConverter[] converters)
		{
			global::Newtonsoft.Json.JsonSerializerSettings jsonSerializerSettings = ((converters != null && converters.Length != 0) ? new global::Newtonsoft.Json.JsonSerializerSettings
			{
				Converters = converters
			} : null);
			global::Newtonsoft.Json.JsonSerializerSettings settings = jsonSerializerSettings;
			return SerializeObject(value, null, formatting, settings);
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

		[global::System.Diagnostics.DebuggerStepThrough]
		public static string SerializeObject(object value, global::Newtonsoft.Json.Formatting formatting, global::Newtonsoft.Json.JsonSerializerSettings settings)
		{
			return SerializeObject(value, null, formatting, settings);
		}

		[global::System.Diagnostics.DebuggerStepThrough]
		public static string SerializeObject(object value, global::System.Type type, global::Newtonsoft.Json.Formatting formatting, global::Newtonsoft.Json.JsonSerializerSettings settings)
		{
			global::Newtonsoft.Json.JsonSerializer jsonSerializer = global::Newtonsoft.Json.JsonSerializer.Create(settings);
			jsonSerializer.Formatting = formatting;
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
		public static object DeserializeObject(string value)
		{
			return DeserializeObject(value, (global::System.Type)null, (global::Newtonsoft.Json.JsonSerializerSettings)null);
		}

		[global::System.Diagnostics.DebuggerStepThrough]
		public static object DeserializeObject(string value, global::Newtonsoft.Json.JsonSerializerSettings settings)
		{
			return DeserializeObject(value, null, settings);
		}

		[global::System.Diagnostics.DebuggerStepThrough]
		public static object DeserializeObject(string value, global::System.Type type)
		{
			return DeserializeObject(value, type, (global::Newtonsoft.Json.JsonSerializerSettings)null);
		}

		[global::System.Diagnostics.DebuggerStepThrough]
		public static T DeserializeObject<T>(string value)
		{
			return global::Unity.Services.Authentication.IsolatedJsonConvert.DeserializeObject<T>(value, (global::Newtonsoft.Json.JsonSerializerSettings)null);
		}

		[global::System.Diagnostics.DebuggerStepThrough]
		public static T DeserializeAnonymousType<T>(string value, T anonymousTypeObject)
		{
			return DeserializeObject<T>(value);
		}

		[global::System.Diagnostics.DebuggerStepThrough]
		public static T DeserializeAnonymousType<T>(string value, T anonymousTypeObject, global::Newtonsoft.Json.JsonSerializerSettings settings)
		{
			return DeserializeObject<T>(value, settings);
		}

		[global::System.Diagnostics.DebuggerStepThrough]
		public static T DeserializeObject<T>(string value, params global::Newtonsoft.Json.JsonConverter[] converters)
		{
			return (T)DeserializeObject(value, typeof(T), converters);
		}

		[global::System.Diagnostics.DebuggerStepThrough]
		public static T DeserializeObject<T>(string value, global::Newtonsoft.Json.JsonSerializerSettings settings)
		{
			return (T)DeserializeObject(value, typeof(T), settings);
		}

		[global::System.Diagnostics.DebuggerStepThrough]
		public static object DeserializeObject(string value, global::System.Type type, params global::Newtonsoft.Json.JsonConverter[] converters)
		{
			global::Newtonsoft.Json.JsonSerializerSettings jsonSerializerSettings = ((converters != null && converters.Length != 0) ? new global::Newtonsoft.Json.JsonSerializerSettings
			{
				Converters = converters
			} : null);
			global::Newtonsoft.Json.JsonSerializerSettings settings = jsonSerializerSettings;
			return DeserializeObject(value, type, settings);
		}

		public static object DeserializeObject(string value, global::System.Type type, global::Newtonsoft.Json.JsonSerializerSettings settings)
		{
			global::Newtonsoft.Json.JsonSerializer jsonSerializer = global::Newtonsoft.Json.JsonSerializer.Create(settings);
			using global::Newtonsoft.Json.JsonTextReader reader = new global::Newtonsoft.Json.JsonTextReader(new global::System.IO.StringReader(value));
			return jsonSerializer.Deserialize(reader, type);
		}

		[global::System.Diagnostics.DebuggerStepThrough]
		public static void PopulateObject(string value, object target)
		{
			PopulateObject(value, target, null);
		}

		public static void PopulateObject(string value, object target, global::Newtonsoft.Json.JsonSerializerSettings settings)
		{
			using global::Newtonsoft.Json.JsonReader jsonReader = new global::Newtonsoft.Json.JsonTextReader(new global::System.IO.StringReader(value));
			global::Newtonsoft.Json.JsonSerializer.Create(settings).Populate(jsonReader, target);
			if (settings == null || !settings.CheckAdditionalContent)
			{
				return;
			}
			while (jsonReader.Read())
			{
				if (jsonReader.TokenType != global::Newtonsoft.Json.JsonToken.Comment)
				{
					throw new global::Newtonsoft.Json.JsonSerializationException("Additional text found in JSON string after finishing deserializing object.");
				}
			}
		}
	}
}
