namespace Newtonsoft.Json.Converters
{
	public class ExpandoObjectConverter : global::Newtonsoft.Json.JsonConverter
	{
		public override bool CanWrite => false;

		public override void WriteJson(global::Newtonsoft.Json.JsonWriter writer, object? value, global::Newtonsoft.Json.JsonSerializer serializer)
		{
		}

		public override object? ReadJson(global::Newtonsoft.Json.JsonReader reader, global::System.Type objectType, object? existingValue, global::Newtonsoft.Json.JsonSerializer serializer)
		{
			return ReadValue(reader);
		}

		private object? ReadValue(global::Newtonsoft.Json.JsonReader reader)
		{
			if (!reader.MoveToContent())
			{
				throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, "Unexpected end when reading ExpandoObject.");
			}
			switch (reader.TokenType)
			{
			case global::Newtonsoft.Json.JsonToken.StartObject:
				return ReadObject(reader);
			case global::Newtonsoft.Json.JsonToken.StartArray:
				return ReadList(reader);
			default:
				if (global::Newtonsoft.Json.Utilities.JsonTokenUtils.IsPrimitiveToken(reader.TokenType))
				{
					return reader.Value;
				}
				throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Unexpected token when converting ExpandoObject: {0}", global::System.Globalization.CultureInfo.InvariantCulture, reader.TokenType));
			}
		}

		private object ReadList(global::Newtonsoft.Json.JsonReader reader)
		{
			global::System.Collections.Generic.IList<object> list = new global::System.Collections.Generic.List<object>();
			while (reader.Read())
			{
				switch (reader.TokenType)
				{
				case global::Newtonsoft.Json.JsonToken.EndArray:
					return list;
				case global::Newtonsoft.Json.JsonToken.Comment:
					continue;
				}
				object item = ReadValue(reader);
				list.Add(item);
			}
			throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, "Unexpected end when reading ExpandoObject.");
		}

		private object ReadObject(global::Newtonsoft.Json.JsonReader reader)
		{
			global::System.Collections.Generic.IDictionary<string, object> dictionary = new global::System.Dynamic.ExpandoObject();
			while (reader.Read())
			{
				switch (reader.TokenType)
				{
				case global::Newtonsoft.Json.JsonToken.PropertyName:
				{
					string key = reader.Value.ToString();
					if (!reader.Read())
					{
						throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, "Unexpected end when reading ExpandoObject.");
					}
					object value = ReadValue(reader);
					dictionary[key] = value;
					break;
				}
				case global::Newtonsoft.Json.JsonToken.EndObject:
					return dictionary;
				}
			}
			throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, "Unexpected end when reading ExpandoObject.");
		}

		public override bool CanConvert(global::System.Type objectType)
		{
			return objectType == typeof(global::System.Dynamic.ExpandoObject);
		}
	}
}
