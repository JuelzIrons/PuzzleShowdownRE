namespace Newtonsoft.Json.Converters
{
	public class RegexConverter : global::Newtonsoft.Json.JsonConverter
	{
		private const string PatternName = "Pattern";

		private const string OptionsName = "Options";

		public override void WriteJson(global::Newtonsoft.Json.JsonWriter writer, object? value, global::Newtonsoft.Json.JsonSerializer serializer)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}
			global::System.Text.RegularExpressions.Regex regex = (global::System.Text.RegularExpressions.Regex)value;
			if (writer is global::Newtonsoft.Json.Bson.BsonWriter writer2)
			{
				WriteBson(writer2, regex);
			}
			else
			{
				WriteJson(writer, regex, serializer);
			}
		}

		private bool HasFlag(global::System.Text.RegularExpressions.RegexOptions options, global::System.Text.RegularExpressions.RegexOptions flag)
		{
			return (options & flag) == flag;
		}

		private void WriteBson(global::Newtonsoft.Json.Bson.BsonWriter writer, global::System.Text.RegularExpressions.Regex regex)
		{
			string text = null;
			if (HasFlag(regex.Options, global::System.Text.RegularExpressions.RegexOptions.IgnoreCase))
			{
				text += "i";
			}
			if (HasFlag(regex.Options, global::System.Text.RegularExpressions.RegexOptions.Multiline))
			{
				text += "m";
			}
			if (HasFlag(regex.Options, global::System.Text.RegularExpressions.RegexOptions.Singleline))
			{
				text += "s";
			}
			text += "u";
			if (HasFlag(regex.Options, global::System.Text.RegularExpressions.RegexOptions.ExplicitCapture))
			{
				text += "x";
			}
			writer.WriteRegex(regex.ToString(), text);
		}

		private void WriteJson(global::Newtonsoft.Json.JsonWriter writer, global::System.Text.RegularExpressions.Regex regex, global::Newtonsoft.Json.JsonSerializer serializer)
		{
			global::Newtonsoft.Json.Serialization.DefaultContractResolver defaultContractResolver = serializer.ContractResolver as global::Newtonsoft.Json.Serialization.DefaultContractResolver;
			writer.WriteStartObject();
			writer.WritePropertyName((defaultContractResolver != null) ? defaultContractResolver.GetResolvedPropertyName("Pattern") : "Pattern");
			writer.WriteValue(regex.ToString());
			writer.WritePropertyName((defaultContractResolver != null) ? defaultContractResolver.GetResolvedPropertyName("Options") : "Options");
			serializer.Serialize(writer, regex.Options);
			writer.WriteEndObject();
		}

		public override object? ReadJson(global::Newtonsoft.Json.JsonReader reader, global::System.Type objectType, object? existingValue, global::Newtonsoft.Json.JsonSerializer serializer)
		{
			return reader.TokenType switch
			{
				global::Newtonsoft.Json.JsonToken.StartObject => ReadRegexObject(reader, serializer), 
				global::Newtonsoft.Json.JsonToken.String => ReadRegexString(reader), 
				global::Newtonsoft.Json.JsonToken.Null => null, 
				_ => throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, "Unexpected token when reading Regex."), 
			};
		}

		private object ReadRegexString(global::Newtonsoft.Json.JsonReader reader)
		{
			string text = (string)reader.Value;
			if (text.Length > 0 && text[0] == '/')
			{
				int num = text.LastIndexOf('/');
				if (num > 0)
				{
					string pattern = text.Substring(1, num - 1);
					global::System.Text.RegularExpressions.RegexOptions regexOptions = global::Newtonsoft.Json.Utilities.MiscellaneousUtils.GetRegexOptions(text.Substring(num + 1));
					return new global::System.Text.RegularExpressions.Regex(pattern, regexOptions);
				}
			}
			throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, "Regex pattern must be enclosed by slashes.");
		}

		private global::System.Text.RegularExpressions.Regex ReadRegexObject(global::Newtonsoft.Json.JsonReader reader, global::Newtonsoft.Json.JsonSerializer serializer)
		{
			string text = null;
			global::System.Text.RegularExpressions.RegexOptions? regexOptions = null;
			while (reader.Read())
			{
				switch (reader.TokenType)
				{
				case global::Newtonsoft.Json.JsonToken.PropertyName:
				{
					string a = reader.Value.ToString();
					if (!reader.Read())
					{
						throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, "Unexpected end when reading Regex.");
					}
					if (string.Equals(a, "Pattern", global::System.StringComparison.OrdinalIgnoreCase))
					{
						text = (string)reader.Value;
					}
					else if (string.Equals(a, "Options", global::System.StringComparison.OrdinalIgnoreCase))
					{
						regexOptions = serializer.Deserialize<global::System.Text.RegularExpressions.RegexOptions>(reader);
					}
					else
					{
						reader.Skip();
					}
					break;
				}
				case global::Newtonsoft.Json.JsonToken.EndObject:
					if (text == null)
					{
						throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, "Error deserializing Regex. No pattern found.");
					}
					return new global::System.Text.RegularExpressions.Regex(text, regexOptions.GetValueOrDefault());
				}
			}
			throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, "Unexpected end when reading Regex.");
		}

		public override bool CanConvert(global::System.Type objectType)
		{
			if (objectType.Name == "Regex")
			{
				return IsRegex(objectType);
			}
			return false;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
		private bool IsRegex(global::System.Type objectType)
		{
			return objectType == typeof(global::System.Text.RegularExpressions.Regex);
		}
	}
}
