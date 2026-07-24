namespace Newtonsoft.Json.Converters
{
	[global::System.Obsolete("BSON reading and writing has been moved to its own package. See https://www.nuget.org/packages/Newtonsoft.Json.Bson for more details.")]
	public class BsonObjectIdConverter : global::Newtonsoft.Json.JsonConverter
	{
		public override void WriteJson(global::Newtonsoft.Json.JsonWriter writer, object value, global::Newtonsoft.Json.JsonSerializer serializer)
		{
			global::Newtonsoft.Json.Bson.BsonObjectId bsonObjectId = (global::Newtonsoft.Json.Bson.BsonObjectId)value;
			if (writer is global::Newtonsoft.Json.Bson.BsonWriter bsonWriter)
			{
				bsonWriter.WriteObjectId(bsonObjectId.Value);
			}
			else
			{
				writer.WriteValue(bsonObjectId.Value);
			}
		}

		public override object ReadJson(global::Newtonsoft.Json.JsonReader reader, global::System.Type objectType, object existingValue, global::Newtonsoft.Json.JsonSerializer serializer)
		{
			if (reader.TokenType != global::Newtonsoft.Json.JsonToken.Bytes)
			{
				throw new global::Newtonsoft.Json.JsonSerializationException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Expected Bytes but got {0}.", global::System.Globalization.CultureInfo.InvariantCulture, reader.TokenType));
			}
			return new global::Newtonsoft.Json.Bson.BsonObjectId((byte[])reader.Value);
		}

		public override bool CanConvert(global::System.Type objectType)
		{
			return objectType == typeof(global::Newtonsoft.Json.Bson.BsonObjectId);
		}
	}
}
