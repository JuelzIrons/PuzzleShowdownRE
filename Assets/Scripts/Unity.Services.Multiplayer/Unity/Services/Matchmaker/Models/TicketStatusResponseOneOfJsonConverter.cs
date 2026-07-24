namespace Unity.Services.Matchmaker.Models
{
	[global::UnityEngine.Scripting.Preserve]
	internal class TicketStatusResponseOneOfJsonConverter : global::Newtonsoft.Json.JsonConverter
	{
		public override void WriteJson(global::Newtonsoft.Json.JsonWriter writer, object value, global::Newtonsoft.Json.JsonSerializer serializer)
		{
			throw new global::System.NotImplementedException();
		}

		public override object ReadJson(global::Newtonsoft.Json.JsonReader reader, global::System.Type objectType, object existingValue, global::Newtonsoft.Json.JsonSerializer serializer)
		{
			if (reader.TokenType != global::Newtonsoft.Json.JsonToken.Null)
			{
				return global::Unity.Services.Matchmaker.Models.TicketStatusResponseOneOf.FromJson(global::Newtonsoft.Json.Linq.JObject.Load(reader).ToString(global::Newtonsoft.Json.Formatting.None));
			}
			return null;
		}

		public override bool CanConvert(global::System.Type objectType)
		{
			return objectType == typeof(global::Unity.Services.Matchmaker.Models.TicketStatusResponseOneOf);
		}
	}
}
