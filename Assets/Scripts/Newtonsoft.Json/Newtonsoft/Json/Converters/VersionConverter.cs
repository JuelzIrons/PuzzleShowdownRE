namespace Newtonsoft.Json.Converters
{
	public class VersionConverter : global::Newtonsoft.Json.JsonConverter
	{
		public override void WriteJson(global::Newtonsoft.Json.JsonWriter writer, object? value, global::Newtonsoft.Json.JsonSerializer serializer)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}
			if (value is global::System.Version)
			{
				writer.WriteValue(value.ToString());
				return;
			}
			throw new global::Newtonsoft.Json.JsonSerializationException("Expected Version object value");
		}

		public override object? ReadJson(global::Newtonsoft.Json.JsonReader reader, global::System.Type objectType, object? existingValue, global::Newtonsoft.Json.JsonSerializer serializer)
		{
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.Null)
			{
				return null;
			}
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.String)
			{
				try
				{
					return new global::System.Version((string)reader.Value);
				}
				catch (global::System.Exception ex)
				{
					throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Error parsing version string: {0}", global::System.Globalization.CultureInfo.InvariantCulture, reader.Value), ex);
				}
			}
			throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Unexpected token or value when parsing version. Token: {0}, Value: {1}", global::System.Globalization.CultureInfo.InvariantCulture, reader.TokenType, reader.Value));
		}

		public override bool CanConvert(global::System.Type objectType)
		{
			return objectType == typeof(global::System.Version);
		}
	}
}
