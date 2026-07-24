namespace Newtonsoft.Json
{
	public static class JsonConvert
	{
		public static readonly string True = "true";

		public static readonly string False = "false";

		public static readonly string Null = "null";

		public static readonly string Undefined = "undefined";

		public static readonly string PositiveInfinity = "Infinity";

		public static readonly string NegativeInfinity = "-Infinity";

		public static readonly string NaN = "NaN";

		public static global::System.Func<global::Newtonsoft.Json.JsonSerializerSettings>? DefaultSettings { get; set; }

		public static string ToString(global::System.DateTime value)
		{
			return ToString(value, global::Newtonsoft.Json.DateFormatHandling.IsoDateFormat, global::Newtonsoft.Json.DateTimeZoneHandling.RoundtripKind);
		}

		public static string ToString(global::System.DateTime value, global::Newtonsoft.Json.DateFormatHandling format, global::Newtonsoft.Json.DateTimeZoneHandling timeZoneHandling)
		{
			global::System.DateTime value2 = global::Newtonsoft.Json.Utilities.DateTimeUtils.EnsureDateTime(value, timeZoneHandling);
			using global::System.IO.StringWriter stringWriter = global::Newtonsoft.Json.Utilities.StringUtils.CreateStringWriter(64);
			stringWriter.Write('"');
			global::Newtonsoft.Json.Utilities.DateTimeUtils.WriteDateTimeString(stringWriter, value2, format, null, global::System.Globalization.CultureInfo.InvariantCulture);
			stringWriter.Write('"');
			return stringWriter.ToString();
		}

		public static string ToString(global::System.DateTimeOffset value)
		{
			return ToString(value, global::Newtonsoft.Json.DateFormatHandling.IsoDateFormat);
		}

		public static string ToString(global::System.DateTimeOffset value, global::Newtonsoft.Json.DateFormatHandling format)
		{
			using global::System.IO.StringWriter stringWriter = global::Newtonsoft.Json.Utilities.StringUtils.CreateStringWriter(64);
			stringWriter.Write('"');
			global::Newtonsoft.Json.Utilities.DateTimeUtils.WriteDateTimeOffsetString(stringWriter, value, format, null, global::System.Globalization.CultureInfo.InvariantCulture);
			stringWriter.Write('"');
			return stringWriter.ToString();
		}

		public static string ToString(bool value)
		{
			if (!value)
			{
				return False;
			}
			return True;
		}

		public static string ToString(char value)
		{
			return ToString(char.ToString(value));
		}

		public static string ToString(global::System.Enum value)
		{
			return value.ToString("D");
		}

		public static string ToString(int value)
		{
			return value.ToString(null, global::System.Globalization.CultureInfo.InvariantCulture);
		}

		public static string ToString(short value)
		{
			return value.ToString(null, global::System.Globalization.CultureInfo.InvariantCulture);
		}

		[global::System.CLSCompliant(false)]
		public static string ToString(ushort value)
		{
			return value.ToString(null, global::System.Globalization.CultureInfo.InvariantCulture);
		}

		[global::System.CLSCompliant(false)]
		public static string ToString(uint value)
		{
			return value.ToString(null, global::System.Globalization.CultureInfo.InvariantCulture);
		}

		public static string ToString(long value)
		{
			return value.ToString(null, global::System.Globalization.CultureInfo.InvariantCulture);
		}

		private static string ToStringInternal(global::System.Numerics.BigInteger value)
		{
			return value.ToString(null, global::System.Globalization.CultureInfo.InvariantCulture);
		}

		[global::System.CLSCompliant(false)]
		public static string ToString(ulong value)
		{
			return value.ToString(null, global::System.Globalization.CultureInfo.InvariantCulture);
		}

		public static string ToString(float value)
		{
			return EnsureDecimalPlace(value, value.ToString("R", global::System.Globalization.CultureInfo.InvariantCulture));
		}

		internal static string ToString(float value, global::Newtonsoft.Json.FloatFormatHandling floatFormatHandling, char quoteChar, bool nullable)
		{
			return EnsureFloatFormat(value, EnsureDecimalPlace(value, value.ToString("R", global::System.Globalization.CultureInfo.InvariantCulture)), floatFormatHandling, quoteChar, nullable);
		}

		private static string EnsureFloatFormat(double value, string text, global::Newtonsoft.Json.FloatFormatHandling floatFormatHandling, char quoteChar, bool nullable)
		{
			if (floatFormatHandling == global::Newtonsoft.Json.FloatFormatHandling.Symbol || (!double.IsInfinity(value) && !double.IsNaN(value)))
			{
				return text;
			}
			if (floatFormatHandling == global::Newtonsoft.Json.FloatFormatHandling.DefaultValue)
			{
				if (nullable)
				{
					return Null;
				}
				return "0.0";
			}
			return quoteChar + text + quoteChar;
		}

		public static string ToString(double value)
		{
			return EnsureDecimalPlace(value, value.ToString("R", global::System.Globalization.CultureInfo.InvariantCulture));
		}

		internal static string ToString(double value, global::Newtonsoft.Json.FloatFormatHandling floatFormatHandling, char quoteChar, bool nullable)
		{
			return EnsureFloatFormat(value, EnsureDecimalPlace(value, value.ToString("R", global::System.Globalization.CultureInfo.InvariantCulture)), floatFormatHandling, quoteChar, nullable);
		}

		private static string EnsureDecimalPlace(double value, string text)
		{
			if (double.IsNaN(value) || double.IsInfinity(value) || global::Newtonsoft.Json.Utilities.StringUtils.IndexOf(text, '.') != -1 || global::Newtonsoft.Json.Utilities.StringUtils.IndexOf(text, 'E') != -1 || global::Newtonsoft.Json.Utilities.StringUtils.IndexOf(text, 'e') != -1)
			{
				return text;
			}
			return text + ".0";
		}

		private static string EnsureDecimalPlace(string text)
		{
			if (global::Newtonsoft.Json.Utilities.StringUtils.IndexOf(text, '.') != -1)
			{
				return text;
			}
			return text + ".0";
		}

		public static string ToString(byte value)
		{
			return value.ToString(null, global::System.Globalization.CultureInfo.InvariantCulture);
		}

		[global::System.CLSCompliant(false)]
		public static string ToString(sbyte value)
		{
			return value.ToString(null, global::System.Globalization.CultureInfo.InvariantCulture);
		}

		public static string ToString(decimal value)
		{
			return EnsureDecimalPlace(value.ToString(null, global::System.Globalization.CultureInfo.InvariantCulture));
		}

		public static string ToString(global::System.Guid value)
		{
			return ToString(value, '"');
		}

		internal static string ToString(global::System.Guid value, char quoteChar)
		{
			string text = value.ToString("D", global::System.Globalization.CultureInfo.InvariantCulture);
			string text2 = quoteChar.ToString(global::System.Globalization.CultureInfo.InvariantCulture);
			return text2 + text + text2;
		}

		public static string ToString(global::System.TimeSpan value)
		{
			return ToString(value, '"');
		}

		internal static string ToString(global::System.TimeSpan value, char quoteChar)
		{
			return ToString(value.ToString(), quoteChar);
		}

		public static string ToString(global::System.Uri? value)
		{
			if (value == null)
			{
				return Null;
			}
			return ToString(value, '"');
		}

		internal static string ToString(global::System.Uri value, char quoteChar)
		{
			return ToString(value.OriginalString, quoteChar);
		}

		public static string ToString(string? value)
		{
			return ToString(value, '"');
		}

		public static string ToString(string? value, char delimiter)
		{
			return ToString(value, delimiter, global::Newtonsoft.Json.StringEscapeHandling.Default);
		}

		public static string ToString(string? value, char delimiter, global::Newtonsoft.Json.StringEscapeHandling stringEscapeHandling)
		{
			if (delimiter != '"' && delimiter != '\'')
			{
				throw new global::System.ArgumentException("Delimiter must be a single or double quote.", "delimiter");
			}
			return global::Newtonsoft.Json.Utilities.JavaScriptUtils.ToEscapedJavaScriptString(value, delimiter, appendDelimiters: true, stringEscapeHandling);
		}

		public static string ToString(object? value)
		{
			if (value == null)
			{
				return Null;
			}
			return global::Newtonsoft.Json.Utilities.ConvertUtils.GetTypeCode(value.GetType()) switch
			{
				global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.String => ToString((string)value), 
				global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Char => ToString((char)value), 
				global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Boolean => ToString((bool)value), 
				global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.SByte => ToString((sbyte)value), 
				global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Int16 => ToString((short)value), 
				global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.UInt16 => ToString((ushort)value), 
				global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Int32 => ToString((int)value), 
				global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Byte => ToString((byte)value), 
				global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.UInt32 => ToString((uint)value), 
				global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Int64 => ToString((long)value), 
				global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.UInt64 => ToString((ulong)value), 
				global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Single => ToString((float)value), 
				global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Double => ToString((double)value), 
				global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.DateTime => ToString((global::System.DateTime)value), 
				global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Decimal => ToString((decimal)value), 
				global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.DBNull => Null, 
				global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.DateTimeOffset => ToString((global::System.DateTimeOffset)value), 
				global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Guid => ToString((global::System.Guid)value), 
				global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Uri => ToString((global::System.Uri)value), 
				global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.TimeSpan => ToString((global::System.TimeSpan)value), 
				global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.BigInteger => ToStringInternal((global::System.Numerics.BigInteger)value), 
				_ => throw new global::System.ArgumentException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Unsupported type: {0}. Use the JsonSerializer class to get the object's JSON representation.", global::System.Globalization.CultureInfo.InvariantCulture, value.GetType())), 
			};
		}

		[global::System.Diagnostics.DebuggerStepThrough]
		public static string SerializeObject(object? value)
		{
			return SerializeObject(value, (global::System.Type?)null, (global::Newtonsoft.Json.JsonSerializerSettings?)null);
		}

		[global::System.Diagnostics.DebuggerStepThrough]
		public static string SerializeObject(object? value, global::Newtonsoft.Json.Formatting formatting)
		{
			return SerializeObject(value, formatting, (global::Newtonsoft.Json.JsonSerializerSettings?)null);
		}

		[global::System.Diagnostics.DebuggerStepThrough]
		public static string SerializeObject(object? value, params global::Newtonsoft.Json.JsonConverter[] converters)
		{
			global::Newtonsoft.Json.JsonSerializerSettings settings = ((converters != null && converters.Length != 0) ? new global::Newtonsoft.Json.JsonSerializerSettings
			{
				Converters = converters
			} : null);
			return SerializeObject(value, null, settings);
		}

		[global::System.Diagnostics.DebuggerStepThrough]
		public static string SerializeObject(object? value, global::Newtonsoft.Json.Formatting formatting, params global::Newtonsoft.Json.JsonConverter[] converters)
		{
			global::Newtonsoft.Json.JsonSerializerSettings settings = ((converters != null && converters.Length != 0) ? new global::Newtonsoft.Json.JsonSerializerSettings
			{
				Converters = converters
			} : null);
			return SerializeObject(value, null, formatting, settings);
		}

		[global::System.Diagnostics.DebuggerStepThrough]
		public static string SerializeObject(object? value, global::Newtonsoft.Json.JsonSerializerSettings? settings)
		{
			return SerializeObject(value, null, settings);
		}

		[global::System.Diagnostics.DebuggerStepThrough]
		public static string SerializeObject(object? value, global::System.Type? type, global::Newtonsoft.Json.JsonSerializerSettings? settings)
		{
			global::Newtonsoft.Json.JsonSerializer jsonSerializer = global::Newtonsoft.Json.JsonSerializer.CreateDefault(settings);
			return SerializeObjectInternal(value, type, jsonSerializer);
		}

		[global::System.Diagnostics.DebuggerStepThrough]
		public static string SerializeObject(object? value, global::Newtonsoft.Json.Formatting formatting, global::Newtonsoft.Json.JsonSerializerSettings? settings)
		{
			return SerializeObject(value, null, formatting, settings);
		}

		[global::System.Diagnostics.DebuggerStepThrough]
		public static string SerializeObject(object? value, global::System.Type? type, global::Newtonsoft.Json.Formatting formatting, global::Newtonsoft.Json.JsonSerializerSettings? settings)
		{
			global::Newtonsoft.Json.JsonSerializer jsonSerializer = global::Newtonsoft.Json.JsonSerializer.CreateDefault(settings);
			jsonSerializer.Formatting = formatting;
			return SerializeObjectInternal(value, type, jsonSerializer);
		}

		private static string SerializeObjectInternal(object? value, global::System.Type? type, global::Newtonsoft.Json.JsonSerializer jsonSerializer)
		{
			global::System.IO.StringWriter stringWriter = new global::System.IO.StringWriter(new global::System.Text.StringBuilder(256), global::System.Globalization.CultureInfo.InvariantCulture);
			using (global::Newtonsoft.Json.JsonTextWriter jsonTextWriter = new global::Newtonsoft.Json.JsonTextWriter(stringWriter))
			{
				jsonTextWriter.Formatting = jsonSerializer.Formatting;
				jsonSerializer.Serialize(jsonTextWriter, value, type);
			}
			return stringWriter.ToString();
		}

		[global::System.Diagnostics.DebuggerStepThrough]
		public static object? DeserializeObject(string value)
		{
			return DeserializeObject(value, (global::System.Type?)null, (global::Newtonsoft.Json.JsonSerializerSettings?)null);
		}

		[global::System.Diagnostics.DebuggerStepThrough]
		public static object? DeserializeObject(string value, global::Newtonsoft.Json.JsonSerializerSettings settings)
		{
			return DeserializeObject(value, null, settings);
		}

		[global::System.Diagnostics.DebuggerStepThrough]
		public static object? DeserializeObject(string value, global::System.Type type)
		{
			return DeserializeObject(value, type, (global::Newtonsoft.Json.JsonSerializerSettings?)null);
		}

		[global::System.Diagnostics.DebuggerStepThrough]
		public static T? DeserializeObject<T>(string value)
		{
			return global::Newtonsoft.Json.JsonConvert.DeserializeObject<T>(value, (global::Newtonsoft.Json.JsonSerializerSettings?)null);
		}

		[global::System.Diagnostics.DebuggerStepThrough]
		public static T? DeserializeAnonymousType<T>(string value, T anonymousTypeObject)
		{
			return DeserializeObject<T>(value);
		}

		[global::System.Diagnostics.DebuggerStepThrough]
		public static T? DeserializeAnonymousType<T>(string value, T anonymousTypeObject, global::Newtonsoft.Json.JsonSerializerSettings settings)
		{
			return DeserializeObject<T>(value, settings);
		}

		[global::System.Diagnostics.DebuggerStepThrough]
		public static T? DeserializeObject<T>(string value, params global::Newtonsoft.Json.JsonConverter[] converters)
		{
			return (T)DeserializeObject(value, typeof(T), converters);
		}

		[global::System.Diagnostics.DebuggerStepThrough]
		public static T? DeserializeObject<T>(string value, global::Newtonsoft.Json.JsonSerializerSettings? settings)
		{
			return (T)DeserializeObject(value, typeof(T), settings);
		}

		[global::System.Diagnostics.DebuggerStepThrough]
		public static object? DeserializeObject(string value, global::System.Type type, params global::Newtonsoft.Json.JsonConverter[] converters)
		{
			global::Newtonsoft.Json.JsonSerializerSettings settings = ((converters != null && converters.Length != 0) ? new global::Newtonsoft.Json.JsonSerializerSettings
			{
				Converters = converters
			} : null);
			return DeserializeObject(value, type, settings);
		}

		public static object? DeserializeObject(string value, global::System.Type? type, global::Newtonsoft.Json.JsonSerializerSettings? settings)
		{
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(value, "value");
			global::Newtonsoft.Json.JsonSerializer jsonSerializer = global::Newtonsoft.Json.JsonSerializer.CreateDefault(settings);
			if (!jsonSerializer.IsCheckAdditionalContentSet())
			{
				jsonSerializer.CheckAdditionalContent = true;
			}
			using global::Newtonsoft.Json.JsonTextReader reader = new global::Newtonsoft.Json.JsonTextReader(new global::System.IO.StringReader(value));
			return jsonSerializer.Deserialize(reader, type);
		}

		[global::System.Diagnostics.DebuggerStepThrough]
		public static void PopulateObject(string value, object target)
		{
			PopulateObject(value, target, null);
		}

		public static void PopulateObject(string value, object target, global::Newtonsoft.Json.JsonSerializerSettings? settings)
		{
			global::Newtonsoft.Json.JsonSerializer jsonSerializer = global::Newtonsoft.Json.JsonSerializer.CreateDefault(settings);
			using global::Newtonsoft.Json.JsonReader jsonReader = new global::Newtonsoft.Json.JsonTextReader(new global::System.IO.StringReader(value));
			jsonSerializer.Populate(jsonReader, target);
			if (settings == null || !settings.CheckAdditionalContent)
			{
				return;
			}
			while (jsonReader.Read())
			{
				if (jsonReader.TokenType != global::Newtonsoft.Json.JsonToken.Comment)
				{
					throw global::Newtonsoft.Json.JsonSerializationException.Create(jsonReader, "Additional text found in JSON string after finishing deserializing object.");
				}
			}
		}

		public static string SerializeXmlNode(global::System.Xml.XmlNode? node)
		{
			return SerializeXmlNode(node, global::Newtonsoft.Json.Formatting.None);
		}

		public static string SerializeXmlNode(global::System.Xml.XmlNode? node, global::Newtonsoft.Json.Formatting formatting)
		{
			global::Newtonsoft.Json.Converters.XmlNodeConverter xmlNodeConverter = new global::Newtonsoft.Json.Converters.XmlNodeConverter();
			return SerializeObject(node, formatting, xmlNodeConverter);
		}

		public static string SerializeXmlNode(global::System.Xml.XmlNode? node, global::Newtonsoft.Json.Formatting formatting, bool omitRootObject)
		{
			global::Newtonsoft.Json.Converters.XmlNodeConverter xmlNodeConverter = new global::Newtonsoft.Json.Converters.XmlNodeConverter
			{
				OmitRootObject = omitRootObject
			};
			return SerializeObject(node, formatting, xmlNodeConverter);
		}

		public static global::System.Xml.XmlDocument? DeserializeXmlNode(string value)
		{
			return DeserializeXmlNode(value, null);
		}

		public static global::System.Xml.XmlDocument? DeserializeXmlNode(string value, string? deserializeRootElementName)
		{
			return DeserializeXmlNode(value, deserializeRootElementName, writeArrayAttribute: false);
		}

		public static global::System.Xml.XmlDocument? DeserializeXmlNode(string value, string? deserializeRootElementName, bool writeArrayAttribute)
		{
			return DeserializeXmlNode(value, deserializeRootElementName, writeArrayAttribute, encodeSpecialCharacters: false);
		}

		public static global::System.Xml.XmlDocument? DeserializeXmlNode(string value, string? deserializeRootElementName, bool writeArrayAttribute, bool encodeSpecialCharacters)
		{
			global::Newtonsoft.Json.Converters.XmlNodeConverter xmlNodeConverter = new global::Newtonsoft.Json.Converters.XmlNodeConverter();
			xmlNodeConverter.DeserializeRootElementName = deserializeRootElementName;
			xmlNodeConverter.WriteArrayAttribute = writeArrayAttribute;
			xmlNodeConverter.EncodeSpecialCharacters = encodeSpecialCharacters;
			return (global::System.Xml.XmlDocument)DeserializeObject(value, typeof(global::System.Xml.XmlDocument), xmlNodeConverter);
		}

		public static string SerializeXNode(global::System.Xml.Linq.XObject? node)
		{
			return SerializeXNode(node, global::Newtonsoft.Json.Formatting.None);
		}

		public static string SerializeXNode(global::System.Xml.Linq.XObject? node, global::Newtonsoft.Json.Formatting formatting)
		{
			return SerializeXNode(node, formatting, omitRootObject: false);
		}

		public static string SerializeXNode(global::System.Xml.Linq.XObject? node, global::Newtonsoft.Json.Formatting formatting, bool omitRootObject)
		{
			global::Newtonsoft.Json.Converters.XmlNodeConverter xmlNodeConverter = new global::Newtonsoft.Json.Converters.XmlNodeConverter
			{
				OmitRootObject = omitRootObject
			};
			return SerializeObject(node, formatting, xmlNodeConverter);
		}

		public static global::System.Xml.Linq.XDocument? DeserializeXNode(string value)
		{
			return DeserializeXNode(value, null);
		}

		public static global::System.Xml.Linq.XDocument? DeserializeXNode(string value, string? deserializeRootElementName)
		{
			return DeserializeXNode(value, deserializeRootElementName, writeArrayAttribute: false);
		}

		public static global::System.Xml.Linq.XDocument? DeserializeXNode(string value, string? deserializeRootElementName, bool writeArrayAttribute)
		{
			return DeserializeXNode(value, deserializeRootElementName, writeArrayAttribute, encodeSpecialCharacters: false);
		}

		public static global::System.Xml.Linq.XDocument? DeserializeXNode(string value, string? deserializeRootElementName, bool writeArrayAttribute, bool encodeSpecialCharacters)
		{
			global::Newtonsoft.Json.Converters.XmlNodeConverter xmlNodeConverter = new global::Newtonsoft.Json.Converters.XmlNodeConverter();
			xmlNodeConverter.DeserializeRootElementName = deserializeRootElementName;
			xmlNodeConverter.WriteArrayAttribute = writeArrayAttribute;
			xmlNodeConverter.EncodeSpecialCharacters = encodeSpecialCharacters;
			return (global::System.Xml.Linq.XDocument)DeserializeObject(value, typeof(global::System.Xml.Linq.XDocument), xmlNodeConverter);
		}
	}
}
