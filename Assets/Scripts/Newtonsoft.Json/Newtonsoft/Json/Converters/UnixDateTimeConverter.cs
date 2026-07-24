namespace Newtonsoft.Json.Converters
{
	public class UnixDateTimeConverter : global::Newtonsoft.Json.Converters.DateTimeConverterBase
	{
		internal static readonly global::System.DateTime UnixEpoch = new global::System.DateTime(1970, 1, 1, 0, 0, 0, global::System.DateTimeKind.Utc);

		public bool AllowPreEpoch { get; set; }

		public UnixDateTimeConverter()
			: this(allowPreEpoch: false)
		{
		}

		public UnixDateTimeConverter(bool allowPreEpoch)
		{
			AllowPreEpoch = allowPreEpoch;
		}

		public override void WriteJson(global::Newtonsoft.Json.JsonWriter writer, object? value, global::Newtonsoft.Json.JsonSerializer serializer)
		{
			long num;
			if (value is global::System.DateTime dateTime)
			{
				num = (long)(dateTime.ToUniversalTime() - UnixEpoch).TotalSeconds;
			}
			else
			{
				if (!(value is global::System.DateTimeOffset dateTimeOffset))
				{
					throw new global::Newtonsoft.Json.JsonSerializationException("Expected date object value.");
				}
				num = (long)(dateTimeOffset.ToUniversalTime() - UnixEpoch).TotalSeconds;
			}
			if (!AllowPreEpoch && num < 0)
			{
				throw new global::Newtonsoft.Json.JsonSerializationException("Cannot convert date value that is before Unix epoch of 00:00:00 UTC on 1 January 1970.");
			}
			writer.WriteValue(num);
		}

		public override object? ReadJson(global::Newtonsoft.Json.JsonReader reader, global::System.Type objectType, object? existingValue, global::Newtonsoft.Json.JsonSerializer serializer)
		{
			bool flag = global::Newtonsoft.Json.Utilities.ReflectionUtils.IsNullable(objectType);
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.Null)
			{
				if (!flag)
				{
					throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Cannot convert null value to {0}.", global::System.Globalization.CultureInfo.InvariantCulture, objectType));
				}
				return null;
			}
			long result;
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.Integer)
			{
				result = (long)reader.Value;
			}
			else
			{
				if (reader.TokenType != global::Newtonsoft.Json.JsonToken.String)
				{
					throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Unexpected token parsing date. Expected Integer or String, got {0}.", global::System.Globalization.CultureInfo.InvariantCulture, reader.TokenType));
				}
				if (!long.TryParse((string)reader.Value, out result))
				{
					throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Cannot convert invalid value to {0}.", global::System.Globalization.CultureInfo.InvariantCulture, objectType));
				}
			}
			if (AllowPreEpoch || result >= 0)
			{
				global::System.DateTime unixEpoch = UnixEpoch;
				global::System.DateTime dateTime = unixEpoch.AddSeconds(result);
				if ((flag ? global::System.Nullable.GetUnderlyingType(objectType) : objectType) == typeof(global::System.DateTimeOffset))
				{
					return new global::System.DateTimeOffset(dateTime, global::System.TimeSpan.Zero);
				}
				return dateTime;
			}
			throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Cannot convert value that is before Unix epoch of 00:00:00 UTC on 1 January 1970 to {0}.", global::System.Globalization.CultureInfo.InvariantCulture, objectType));
		}
	}
}
