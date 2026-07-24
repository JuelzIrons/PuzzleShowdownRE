namespace Newtonsoft.Json.Converters
{
	public class JavaScriptDateTimeConverter : global::Newtonsoft.Json.Converters.DateTimeConverterBase
	{
		public override void WriteJson(global::Newtonsoft.Json.JsonWriter writer, object? value, global::Newtonsoft.Json.JsonSerializer serializer)
		{
			long value2;
			if (value is global::System.DateTime dateTime)
			{
				value2 = global::Newtonsoft.Json.Utilities.DateTimeUtils.ConvertDateTimeToJavaScriptTicks(dateTime.ToUniversalTime());
			}
			else
			{
				if (!(value is global::System.DateTimeOffset dateTimeOffset))
				{
					throw new global::Newtonsoft.Json.JsonSerializationException("Expected date object value.");
				}
				value2 = global::Newtonsoft.Json.Utilities.DateTimeUtils.ConvertDateTimeToJavaScriptTicks(dateTimeOffset.ToUniversalTime().UtcDateTime);
			}
			writer.WriteStartConstructor("Date");
			writer.WriteValue(value2);
			writer.WriteEndConstructor();
		}

		public override object? ReadJson(global::Newtonsoft.Json.JsonReader reader, global::System.Type objectType, object? existingValue, global::Newtonsoft.Json.JsonSerializer serializer)
		{
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.Null)
			{
				if (!global::Newtonsoft.Json.Utilities.ReflectionUtils.IsNullable(objectType))
				{
					throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Cannot convert null value to {0}.", global::System.Globalization.CultureInfo.InvariantCulture, objectType));
				}
				return null;
			}
			if (reader.TokenType != global::Newtonsoft.Json.JsonToken.StartConstructor || !string.Equals(reader.Value?.ToString(), "Date", global::System.StringComparison.Ordinal))
			{
				throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Unexpected token or value when parsing date. Token: {0}, Value: {1}", global::System.Globalization.CultureInfo.InvariantCulture, reader.TokenType, reader.Value));
			}
			if (!global::Newtonsoft.Json.Utilities.JavaScriptUtils.TryGetDateFromConstructorJson(reader, out global::System.DateTime dateTime, out string errorMessage))
			{
				throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, errorMessage);
			}
			if ((global::Newtonsoft.Json.Utilities.ReflectionUtils.IsNullableType(objectType) ? global::System.Nullable.GetUnderlyingType(objectType) : objectType) == typeof(global::System.DateTimeOffset))
			{
				return new global::System.DateTimeOffset(dateTime);
			}
			return dateTime;
		}
	}
}
