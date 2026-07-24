namespace Unity.Services.Matchmaker.Http
{
	[global::UnityEngine.Scripting.Preserve]
	internal class JsonObjectCollectionConverter : global::Newtonsoft.Json.JsonConverter
	{
		public override void WriteJson(global::Newtonsoft.Json.JsonWriter writer, object value, global::Newtonsoft.Json.JsonSerializer serializer)
		{
			object obj = value;
			global::System.Type type = value.GetType();
			if (type == typeof(global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Matchmaker.Http.IDeserializable>))
			{
				obj = (global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Matchmaker.Http.IDeserializable>)value;
			}
			else if (type == typeof(global::System.Collections.Generic.List<global::Unity.Services.Matchmaker.Http.IDeserializable>))
			{
				obj = (global::System.Collections.Generic.List<global::Unity.Services.Matchmaker.Http.IDeserializable>)value;
			}
			else if (type == typeof(global::System.Collections.Generic.List<global::System.Collections.Generic.List<global::Unity.Services.Matchmaker.Http.IDeserializable>>))
			{
				obj = (global::System.Collections.Generic.List<global::System.Collections.Generic.List<global::Unity.Services.Matchmaker.Http.IDeserializable>>)value;
			}
			if (obj == null)
			{
				writer.WriteNull();
			}
			else
			{
				global::Newtonsoft.Json.Linq.JToken.FromObject(obj).WriteTo(writer);
			}
		}

		public override object ReadJson(global::Newtonsoft.Json.JsonReader reader, global::System.Type objectType, object existingValue, global::Newtonsoft.Json.JsonSerializer serializer)
		{
			if (reader.TokenType != global::Newtonsoft.Json.JsonToken.Null)
			{
				global::System.Collections.Generic.List<object> obj = (global::System.Collections.Generic.List<object>)reader.Value;
				global::System.Collections.Generic.List<global::Unity.Services.Matchmaker.Http.JsonObject> list = new global::System.Collections.Generic.List<global::Unity.Services.Matchmaker.Http.JsonObject>();
				{
					foreach (object item in obj)
					{
						list.Add(new global::Unity.Services.Matchmaker.Http.JsonObject(item));
					}
					return list;
				}
			}
			return null;
		}

		public override bool CanConvert(global::System.Type objectType)
		{
			if (!(objectType == typeof(global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Matchmaker.Http.IDeserializable>)) && !(objectType == typeof(global::System.Collections.Generic.List<global::Unity.Services.Matchmaker.Http.IDeserializable>)))
			{
				return objectType == typeof(global::System.Collections.Generic.List<global::System.Collections.Generic.List<global::Unity.Services.Matchmaker.Http.IDeserializable>>);
			}
			return true;
		}
	}
}
