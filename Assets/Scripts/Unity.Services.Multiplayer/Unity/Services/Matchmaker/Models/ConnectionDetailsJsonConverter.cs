namespace Unity.Services.Matchmaker.Models
{
	[global::UnityEngine.Scripting.Preserve]
	internal class ConnectionDetailsJsonConverter : global::Newtonsoft.Json.JsonConverter
	{
		public override void WriteJson(global::Newtonsoft.Json.JsonWriter writer, object value, global::Newtonsoft.Json.JsonSerializer serializer)
		{
			global::Unity.Services.Matchmaker.Models.ConnectionDetails connectionDetails = (global::Unity.Services.Matchmaker.Models.ConnectionDetails)value;
			if (connectionDetails.Type == typeof(global::Unity.Services.Matchmaker.Models.IpPortConnectionDetails))
			{
				serializer.Serialize(writer, (global::Unity.Services.Matchmaker.Models.IpPortConnectionDetails)connectionDetails.Value);
				return;
			}
			if (connectionDetails.Type == typeof(global::Unity.Services.Matchmaker.Models.CustomConnectionDetails))
			{
				serializer.Serialize(writer, (global::Unity.Services.Matchmaker.Models.CustomConnectionDetails)connectionDetails.Value);
				return;
			}
			throw new global::System.NotImplementedException();
		}

		public override object ReadJson(global::Newtonsoft.Json.JsonReader reader, global::System.Type objectType, object existingValue, global::Newtonsoft.Json.JsonSerializer serializer)
		{
			if (reader.TokenType != global::Newtonsoft.Json.JsonToken.Null)
			{
				return global::Unity.Services.Matchmaker.Models.ConnectionDetails.FromJson(global::Newtonsoft.Json.Linq.JObject.Load(reader).ToString(global::Newtonsoft.Json.Formatting.None));
			}
			return null;
		}

		public override bool CanConvert(global::System.Type objectType)
		{
			return objectType == typeof(global::Unity.Services.Matchmaker.Models.ConnectionDetails);
		}
	}
}
