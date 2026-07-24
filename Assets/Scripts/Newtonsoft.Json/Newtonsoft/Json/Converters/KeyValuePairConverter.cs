namespace Newtonsoft.Json.Converters
{
	public class KeyValuePairConverter : global::Newtonsoft.Json.JsonConverter
	{
		private const string KeyName = "Key";

		private const string ValueName = "Value";

		private static readonly global::Newtonsoft.Json.Utilities.ThreadSafeStore<global::System.Type, global::Newtonsoft.Json.Utilities.ReflectionObject> ReflectionObjectPerType = new global::Newtonsoft.Json.Utilities.ThreadSafeStore<global::System.Type, global::Newtonsoft.Json.Utilities.ReflectionObject>(InitializeReflectionObject);

		private static global::Newtonsoft.Json.Utilities.ReflectionObject InitializeReflectionObject(global::System.Type t)
		{
			global::System.Type[] genericArguments = t.GetGenericArguments();
			global::System.Type type = ((global::System.Collections.Generic.IList<global::System.Type>)genericArguments)[0];
			global::System.Type type2 = ((global::System.Collections.Generic.IList<global::System.Type>)genericArguments)[1];
			return global::Newtonsoft.Json.Utilities.ReflectionObject.Create(t, t.GetConstructor(new global::System.Type[2] { type, type2 }), "Key", "Value");
		}

		public override void WriteJson(global::Newtonsoft.Json.JsonWriter writer, object? value, global::Newtonsoft.Json.JsonSerializer serializer)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}
			global::Newtonsoft.Json.Utilities.ReflectionObject reflectionObject = ReflectionObjectPerType.Get(value.GetType());
			global::Newtonsoft.Json.Serialization.DefaultContractResolver defaultContractResolver = serializer.ContractResolver as global::Newtonsoft.Json.Serialization.DefaultContractResolver;
			writer.WriteStartObject();
			writer.WritePropertyName((defaultContractResolver != null) ? defaultContractResolver.GetResolvedPropertyName("Key") : "Key");
			serializer.Serialize(writer, reflectionObject.GetValue(value, "Key"), reflectionObject.GetType("Key"));
			writer.WritePropertyName((defaultContractResolver != null) ? defaultContractResolver.GetResolvedPropertyName("Value") : "Value");
			serializer.Serialize(writer, reflectionObject.GetValue(value, "Value"), reflectionObject.GetType("Value"));
			writer.WriteEndObject();
		}

		public override object? ReadJson(global::Newtonsoft.Json.JsonReader reader, global::System.Type objectType, object? existingValue, global::Newtonsoft.Json.JsonSerializer serializer)
		{
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.Null)
			{
				if (!global::Newtonsoft.Json.Utilities.ReflectionUtils.IsNullableType(objectType))
				{
					throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, "Cannot convert null value to KeyValuePair.");
				}
				return null;
			}
			object obj = null;
			object obj2 = null;
			reader.ReadAndAssert();
			global::System.Type key = (global::Newtonsoft.Json.Utilities.ReflectionUtils.IsNullableType(objectType) ? global::System.Nullable.GetUnderlyingType(objectType) : objectType);
			global::Newtonsoft.Json.Utilities.ReflectionObject reflectionObject = ReflectionObjectPerType.Get(key);
			global::Newtonsoft.Json.Serialization.JsonContract jsonContract = serializer.ContractResolver.ResolveContract(reflectionObject.GetType("Key"));
			global::Newtonsoft.Json.Serialization.JsonContract jsonContract2 = serializer.ContractResolver.ResolveContract(reflectionObject.GetType("Value"));
			while (reader.TokenType == global::Newtonsoft.Json.JsonToken.PropertyName)
			{
				string a = reader.Value.ToString();
				if (string.Equals(a, "Key", global::System.StringComparison.OrdinalIgnoreCase))
				{
					reader.ReadForTypeAndAssert(jsonContract, hasConverter: false);
					obj = serializer.Deserialize(reader, jsonContract.UnderlyingType);
				}
				else if (string.Equals(a, "Value", global::System.StringComparison.OrdinalIgnoreCase))
				{
					reader.ReadForTypeAndAssert(jsonContract2, hasConverter: false);
					obj2 = serializer.Deserialize(reader, jsonContract2.UnderlyingType);
				}
				else
				{
					reader.Skip();
				}
				reader.ReadAndAssert();
			}
			return reflectionObject.Creator(obj, obj2);
		}

		public override bool CanConvert(global::System.Type objectType)
		{
			global::System.Type type = (global::Newtonsoft.Json.Utilities.ReflectionUtils.IsNullableType(objectType) ? global::System.Nullable.GetUnderlyingType(objectType) : objectType);
			if (global::Newtonsoft.Json.Utilities.TypeExtensions.IsValueType(type) && global::Newtonsoft.Json.Utilities.TypeExtensions.IsGenericType(type))
			{
				return type.GetGenericTypeDefinition() == typeof(global::System.Collections.Generic.KeyValuePair<, >);
			}
			return false;
		}
	}
}
