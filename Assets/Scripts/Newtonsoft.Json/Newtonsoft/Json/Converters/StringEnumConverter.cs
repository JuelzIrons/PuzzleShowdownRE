namespace Newtonsoft.Json.Converters
{
	public class StringEnumConverter : global::Newtonsoft.Json.JsonConverter
	{
		[global::System.Obsolete("StringEnumConverter.CamelCaseText is obsolete. Set StringEnumConverter.NamingStrategy with CamelCaseNamingStrategy instead.")]
		public bool CamelCaseText
		{
			get
			{
				if (!(NamingStrategy is global::Newtonsoft.Json.Serialization.CamelCaseNamingStrategy))
				{
					return false;
				}
				return true;
			}
			set
			{
				if (value)
				{
					if (!(NamingStrategy is global::Newtonsoft.Json.Serialization.CamelCaseNamingStrategy))
					{
						NamingStrategy = new global::Newtonsoft.Json.Serialization.CamelCaseNamingStrategy();
					}
				}
				else if (NamingStrategy is global::Newtonsoft.Json.Serialization.CamelCaseNamingStrategy)
				{
					NamingStrategy = null;
				}
			}
		}

		public global::Newtonsoft.Json.Serialization.NamingStrategy? NamingStrategy { get; set; }

		public bool AllowIntegerValues { get; set; } = true;

		public StringEnumConverter()
		{
		}

		[global::System.Obsolete("StringEnumConverter(bool) is obsolete. Create a converter with StringEnumConverter(NamingStrategy, bool) instead.")]
		public StringEnumConverter(bool camelCaseText)
		{
			if (camelCaseText)
			{
				NamingStrategy = new global::Newtonsoft.Json.Serialization.CamelCaseNamingStrategy();
			}
		}

		public StringEnumConverter(global::Newtonsoft.Json.Serialization.NamingStrategy namingStrategy, bool allowIntegerValues = true)
		{
			NamingStrategy = namingStrategy;
			AllowIntegerValues = allowIntegerValues;
		}

		public StringEnumConverter(global::System.Type namingStrategyType)
		{
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(namingStrategyType, "namingStrategyType");
			NamingStrategy = global::Newtonsoft.Json.Serialization.JsonTypeReflector.CreateNamingStrategyInstance(namingStrategyType, null);
		}

		public StringEnumConverter(global::System.Type namingStrategyType, object[] namingStrategyParameters)
		{
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(namingStrategyType, "namingStrategyType");
			NamingStrategy = global::Newtonsoft.Json.Serialization.JsonTypeReflector.CreateNamingStrategyInstance(namingStrategyType, namingStrategyParameters);
		}

		public StringEnumConverter(global::System.Type namingStrategyType, object[] namingStrategyParameters, bool allowIntegerValues)
		{
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(namingStrategyType, "namingStrategyType");
			NamingStrategy = global::Newtonsoft.Json.Serialization.JsonTypeReflector.CreateNamingStrategyInstance(namingStrategyType, namingStrategyParameters);
			AllowIntegerValues = allowIntegerValues;
		}

		public override void WriteJson(global::Newtonsoft.Json.JsonWriter writer, object? value, global::Newtonsoft.Json.JsonSerializer serializer)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}
			global::System.Enum obj = (global::System.Enum)value;
			if (!global::Newtonsoft.Json.Utilities.EnumUtils.TryToString(obj.GetType(), value, NamingStrategy, out string name))
			{
				if (!AllowIntegerValues)
				{
					throw global::Newtonsoft.Json.JsonSerializationException.Create(null, writer.ContainerPath, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Integer value {0} is not allowed.", global::System.Globalization.CultureInfo.InvariantCulture, obj.ToString("D")), null);
				}
				writer.WriteValue(value);
			}
			else
			{
				writer.WriteValue(name);
			}
		}

		public override object? ReadJson(global::Newtonsoft.Json.JsonReader reader, global::System.Type objectType, object? existingValue, global::Newtonsoft.Json.JsonSerializer serializer)
		{
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.Null)
			{
				if (!global::Newtonsoft.Json.Utilities.ReflectionUtils.IsNullableType(objectType))
				{
					throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Cannot convert null value to {0}.", global::System.Globalization.CultureInfo.InvariantCulture, objectType));
				}
				return null;
			}
			bool flag = global::Newtonsoft.Json.Utilities.ReflectionUtils.IsNullableType(objectType);
			global::System.Type type = (flag ? global::System.Nullable.GetUnderlyingType(objectType) : objectType);
			try
			{
				if (reader.TokenType == global::Newtonsoft.Json.JsonToken.String)
				{
					string value = reader.Value?.ToString();
					if (global::Newtonsoft.Json.Utilities.StringUtils.IsNullOrEmpty(value) && flag)
					{
						return null;
					}
					return global::Newtonsoft.Json.Utilities.EnumUtils.ParseEnum(type, NamingStrategy, value, !AllowIntegerValues);
				}
				if (reader.TokenType == global::Newtonsoft.Json.JsonToken.Integer)
				{
					if (!AllowIntegerValues)
					{
						throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Integer value {0} is not allowed.", global::System.Globalization.CultureInfo.InvariantCulture, reader.Value));
					}
					return global::Newtonsoft.Json.Utilities.ConvertUtils.ConvertOrCast(reader.Value, global::System.Globalization.CultureInfo.InvariantCulture, type);
				}
			}
			catch (global::System.Exception ex)
			{
				throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Error converting value {0} to type '{1}'.", global::System.Globalization.CultureInfo.InvariantCulture, global::Newtonsoft.Json.Utilities.MiscellaneousUtils.ToString(reader.Value), objectType), ex);
			}
			throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Unexpected token {0} when parsing enum.", global::System.Globalization.CultureInfo.InvariantCulture, reader.TokenType));
		}

		public override bool CanConvert(global::System.Type objectType)
		{
			return global::Newtonsoft.Json.Utilities.TypeExtensions.IsEnum(global::Newtonsoft.Json.Utilities.ReflectionUtils.IsNullableType(objectType) ? global::System.Nullable.GetUnderlyingType(objectType) : objectType);
		}
	}
}
