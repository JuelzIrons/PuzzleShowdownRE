namespace Newtonsoft.Json.Converters
{
	public class EntityKeyMemberConverter : global::Newtonsoft.Json.JsonConverter
	{
		private const string EntityKeyMemberFullTypeName = "System.Data.EntityKeyMember";

		private const string KeyPropertyName = "Key";

		private const string TypePropertyName = "Type";

		private const string ValuePropertyName = "Value";

		private static global::Newtonsoft.Json.Utilities.ReflectionObject? _reflectionObject;

		public override void WriteJson(global::Newtonsoft.Json.JsonWriter writer, object? value, global::Newtonsoft.Json.JsonSerializer serializer)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}
			EnsureReflectionObject(value.GetType());
			global::Newtonsoft.Json.Serialization.DefaultContractResolver defaultContractResolver = serializer.ContractResolver as global::Newtonsoft.Json.Serialization.DefaultContractResolver;
			string value2 = (string)_reflectionObject.GetValue(value, "Key");
			object value3 = _reflectionObject.GetValue(value, "Value");
			global::System.Type type = value3?.GetType();
			writer.WriteStartObject();
			writer.WritePropertyName((defaultContractResolver != null) ? defaultContractResolver.GetResolvedPropertyName("Key") : "Key");
			writer.WriteValue(value2);
			writer.WritePropertyName((defaultContractResolver != null) ? defaultContractResolver.GetResolvedPropertyName("Type") : "Type");
			writer.WriteValue(type?.FullName);
			writer.WritePropertyName((defaultContractResolver != null) ? defaultContractResolver.GetResolvedPropertyName("Value") : "Value");
			if (type != null)
			{
				if (global::Newtonsoft.Json.Serialization.JsonSerializerInternalWriter.TryConvertToString(value3, type, out string s))
				{
					writer.WriteValue(s);
				}
				else
				{
					writer.WriteValue(value3);
				}
			}
			else
			{
				writer.WriteNull();
			}
			writer.WriteEndObject();
		}

		private static void ReadAndAssertProperty(global::Newtonsoft.Json.JsonReader reader, string propertyName)
		{
			reader.ReadAndAssert();
			if (reader.TokenType != global::Newtonsoft.Json.JsonToken.PropertyName || !string.Equals(reader.Value?.ToString(), propertyName, global::System.StringComparison.OrdinalIgnoreCase))
			{
				throw new global::Newtonsoft.Json.JsonSerializationException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Expected JSON property '{0}'.", global::System.Globalization.CultureInfo.InvariantCulture, propertyName));
			}
		}

		public override object? ReadJson(global::Newtonsoft.Json.JsonReader reader, global::System.Type objectType, object? existingValue, global::Newtonsoft.Json.JsonSerializer serializer)
		{
			EnsureReflectionObject(objectType);
			object obj = _reflectionObject.Creator();
			ReadAndAssertProperty(reader, "Key");
			reader.ReadAndAssert();
			_reflectionObject.SetValue(obj, "Key", reader.Value?.ToString());
			ReadAndAssertProperty(reader, "Type");
			reader.ReadAndAssert();
			global::System.Type type = global::System.Type.GetType(reader.Value?.ToString());
			ReadAndAssertProperty(reader, "Value");
			reader.ReadAndAssert();
			_reflectionObject.SetValue(obj, "Value", serializer.Deserialize(reader, type));
			reader.ReadAndAssert();
			return obj;
		}

		private static void EnsureReflectionObject(global::System.Type objectType)
		{
			if (_reflectionObject == null)
			{
				_reflectionObject = global::Newtonsoft.Json.Utilities.ReflectionObject.Create(objectType, "Key", "Value");
			}
		}

		public override bool CanConvert(global::System.Type objectType)
		{
			return global::Newtonsoft.Json.Utilities.TypeExtensions.AssignableToTypeName(objectType, "System.Data.EntityKeyMember", searchInterfaces: false);
		}
	}
}
