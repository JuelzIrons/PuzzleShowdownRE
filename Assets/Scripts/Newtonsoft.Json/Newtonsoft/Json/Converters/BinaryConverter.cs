namespace Newtonsoft.Json.Converters
{
	public class BinaryConverter : global::Newtonsoft.Json.JsonConverter
	{
		private const string BinaryTypeName = "System.Data.Linq.Binary";

		private const string BinaryToArrayName = "ToArray";

		private static global::Newtonsoft.Json.Utilities.ReflectionObject? _reflectionObject;

		public override void WriteJson(global::Newtonsoft.Json.JsonWriter writer, object? value, global::Newtonsoft.Json.JsonSerializer serializer)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}
			byte[] byteArray = GetByteArray(value);
			writer.WriteValue(byteArray);
		}

		private byte[] GetByteArray(object value)
		{
			if (value.GetType().FullName == "System.Data.Linq.Binary")
			{
				EnsureReflectionObject(value.GetType());
				return (byte[])_reflectionObject.GetValue(value, "ToArray");
			}
			if (value is global::System.Data.SqlTypes.SqlBinary sqlBinary)
			{
				return sqlBinary.Value;
			}
			throw new global::Newtonsoft.Json.JsonSerializationException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Unexpected value type when writing binary: {0}", global::System.Globalization.CultureInfo.InvariantCulture, value.GetType()));
		}

		private static void EnsureReflectionObject(global::System.Type t)
		{
			if (_reflectionObject == null)
			{
				_reflectionObject = global::Newtonsoft.Json.Utilities.ReflectionObject.Create(t, t.GetConstructor(new global::System.Type[1] { typeof(byte[]) }), "ToArray");
			}
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
			byte[] array;
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.StartArray)
			{
				array = ReadByteArray(reader);
			}
			else
			{
				if (reader.TokenType != global::Newtonsoft.Json.JsonToken.String)
				{
					throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Unexpected token parsing binary. Expected String or StartArray, got {0}.", global::System.Globalization.CultureInfo.InvariantCulture, reader.TokenType));
				}
				array = global::System.Convert.FromBase64String(reader.Value.ToString());
			}
			global::System.Type type = (global::Newtonsoft.Json.Utilities.ReflectionUtils.IsNullableType(objectType) ? global::System.Nullable.GetUnderlyingType(objectType) : objectType);
			if (type.FullName == "System.Data.Linq.Binary")
			{
				EnsureReflectionObject(type);
				return _reflectionObject.Creator(array);
			}
			if (type == typeof(global::System.Data.SqlTypes.SqlBinary))
			{
				return new global::System.Data.SqlTypes.SqlBinary(array);
			}
			throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Unexpected object type when writing binary: {0}", global::System.Globalization.CultureInfo.InvariantCulture, objectType));
		}

		private byte[] ReadByteArray(global::Newtonsoft.Json.JsonReader reader)
		{
			global::System.Collections.Generic.List<byte> list = new global::System.Collections.Generic.List<byte>();
			while (reader.Read())
			{
				switch (reader.TokenType)
				{
				case global::Newtonsoft.Json.JsonToken.Integer:
					list.Add(global::System.Convert.ToByte(reader.Value, global::System.Globalization.CultureInfo.InvariantCulture));
					break;
				case global::Newtonsoft.Json.JsonToken.EndArray:
					return list.ToArray();
				default:
					throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Unexpected token when reading bytes: {0}", global::System.Globalization.CultureInfo.InvariantCulture, reader.TokenType));
				case global::Newtonsoft.Json.JsonToken.Comment:
					break;
				}
			}
			throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, "Unexpected end when reading bytes.");
		}

		public override bool CanConvert(global::System.Type objectType)
		{
			if (objectType.FullName == "System.Data.Linq.Binary")
			{
				return true;
			}
			if (objectType == typeof(global::System.Data.SqlTypes.SqlBinary) || objectType == typeof(global::System.Data.SqlTypes.SqlBinary?))
			{
				return true;
			}
			return false;
		}
	}
}
