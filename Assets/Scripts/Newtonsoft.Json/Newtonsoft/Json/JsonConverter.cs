namespace Newtonsoft.Json
{
	public abstract class JsonConverter
	{
		public virtual bool CanRead => true;

		public virtual bool CanWrite => true;

		public abstract void WriteJson(global::Newtonsoft.Json.JsonWriter writer, object? value, global::Newtonsoft.Json.JsonSerializer serializer);

		public abstract object? ReadJson(global::Newtonsoft.Json.JsonReader reader, global::System.Type objectType, object? existingValue, global::Newtonsoft.Json.JsonSerializer serializer);

		public abstract bool CanConvert(global::System.Type objectType);
	}
	public abstract class JsonConverter<T> : global::Newtonsoft.Json.JsonConverter
	{
		public sealed override void WriteJson(global::Newtonsoft.Json.JsonWriter writer, object? value, global::Newtonsoft.Json.JsonSerializer serializer)
		{
			if (!((value != null) ? (value is T) : global::Newtonsoft.Json.Utilities.ReflectionUtils.IsNullable(typeof(T))))
			{
				throw new global::Newtonsoft.Json.JsonSerializationException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Converter cannot write specified value to JSON. {0} is required.", global::System.Globalization.CultureInfo.InvariantCulture, typeof(T)));
			}
			WriteJson(writer, (T)value, serializer);
		}

		public abstract void WriteJson(global::Newtonsoft.Json.JsonWriter writer, T? value, global::Newtonsoft.Json.JsonSerializer serializer);

		public sealed override object? ReadJson(global::Newtonsoft.Json.JsonReader reader, global::System.Type objectType, object? existingValue, global::Newtonsoft.Json.JsonSerializer serializer)
		{
			bool flag = existingValue == null;
			if (!flag && !(existingValue is T))
			{
				throw new global::Newtonsoft.Json.JsonSerializationException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Converter cannot read JSON with the specified existing value. {0} is required.", global::System.Globalization.CultureInfo.InvariantCulture, typeof(T)));
			}
			return ReadJson(reader, objectType, flag ? default(T) : ((T)existingValue), !flag, serializer);
		}

		public abstract T? ReadJson(global::Newtonsoft.Json.JsonReader reader, global::System.Type objectType, T? existingValue, bool hasExistingValue, global::Newtonsoft.Json.JsonSerializer serializer);

		public sealed override bool CanConvert(global::System.Type objectType)
		{
			return typeof(T).IsAssignableFrom(objectType);
		}
	}
}
