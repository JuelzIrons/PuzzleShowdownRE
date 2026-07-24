namespace Unity.Services.Relay.Http
{
	[global::UnityEngine.Scripting.Preserve]
	internal class JsonObjectConverter : global::Newtonsoft.Json.JsonConverter
	{
		public override void WriteJson(global::Newtonsoft.Json.JsonWriter writer, object value, global::Newtonsoft.Json.JsonSerializer serializer)
		{
			global::Unity.Services.Relay.Http.JsonObject jsonObject = (global::Unity.Services.Relay.Http.JsonObject)value;
			if (jsonObject.obj == null)
			{
				writer.WriteNull();
			}
			else
			{
				global::Newtonsoft.Json.Linq.JToken.FromObject(jsonObject.obj).WriteTo(writer);
			}
		}

		public override object ReadJson(global::Newtonsoft.Json.JsonReader reader, global::System.Type objectType, object existingValue, global::Newtonsoft.Json.JsonSerializer serializer)
		{
			if (reader.TokenType != global::Newtonsoft.Json.JsonToken.Null)
			{
				if (reader.Value != null)
				{
					return new global::Unity.Services.Relay.Http.JsonObject(reader.Value);
				}
				try
				{
					return new global::Unity.Services.Relay.Http.JsonObject(global::Newtonsoft.Json.Linq.JObject.Load(reader));
				}
				catch (global::Newtonsoft.Json.JsonReaderException)
				{
					return new global::Unity.Services.Relay.Http.JsonObject(global::Newtonsoft.Json.Linq.JArray.Load(reader));
				}
			}
			return new global::Unity.Services.Relay.Http.JsonObject(null);
		}

		public override bool CanConvert(global::System.Type objectType)
		{
			throw new global::System.NotImplementedException();
		}
	}
}
