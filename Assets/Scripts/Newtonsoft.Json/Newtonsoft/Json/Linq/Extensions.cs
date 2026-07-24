namespace Newtonsoft.Json.Linq
{
	public static class Extensions
	{
		public static global::Newtonsoft.Json.Linq.IJEnumerable<global::Newtonsoft.Json.Linq.JToken> Ancestors<T>(this global::System.Collections.Generic.IEnumerable<T> source) where T : global::Newtonsoft.Json.Linq.JToken
		{
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(source, "source");
			return global::System.Linq.Enumerable.SelectMany(source, (T j) => j.Ancestors()).AsJEnumerable();
		}

		public static global::Newtonsoft.Json.Linq.IJEnumerable<global::Newtonsoft.Json.Linq.JToken> AncestorsAndSelf<T>(this global::System.Collections.Generic.IEnumerable<T> source) where T : global::Newtonsoft.Json.Linq.JToken
		{
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(source, "source");
			return global::System.Linq.Enumerable.SelectMany(source, (T j) => j.AncestorsAndSelf()).AsJEnumerable();
		}

		public static global::Newtonsoft.Json.Linq.IJEnumerable<global::Newtonsoft.Json.Linq.JToken> Descendants<T>(this global::System.Collections.Generic.IEnumerable<T> source) where T : global::Newtonsoft.Json.Linq.JContainer
		{
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(source, "source");
			return global::System.Linq.Enumerable.SelectMany(source, (T j) => j.Descendants()).AsJEnumerable();
		}

		public static global::Newtonsoft.Json.Linq.IJEnumerable<global::Newtonsoft.Json.Linq.JToken> DescendantsAndSelf<T>(this global::System.Collections.Generic.IEnumerable<T> source) where T : global::Newtonsoft.Json.Linq.JContainer
		{
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(source, "source");
			return global::System.Linq.Enumerable.SelectMany(source, (T j) => j.DescendantsAndSelf()).AsJEnumerable();
		}

		public static global::Newtonsoft.Json.Linq.IJEnumerable<global::Newtonsoft.Json.Linq.JProperty> Properties(this global::System.Collections.Generic.IEnumerable<global::Newtonsoft.Json.Linq.JObject> source)
		{
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(source, "source");
			return global::System.Linq.Enumerable.SelectMany(source, (global::Newtonsoft.Json.Linq.JObject d) => d.Properties()).AsJEnumerable();
		}

		public static global::Newtonsoft.Json.Linq.IJEnumerable<global::Newtonsoft.Json.Linq.JToken> Values(this global::System.Collections.Generic.IEnumerable<global::Newtonsoft.Json.Linq.JToken> source, object? key)
		{
			return source.Values<global::Newtonsoft.Json.Linq.JToken, global::Newtonsoft.Json.Linq.JToken>(key).AsJEnumerable();
		}

		public static global::Newtonsoft.Json.Linq.IJEnumerable<global::Newtonsoft.Json.Linq.JToken> Values(this global::System.Collections.Generic.IEnumerable<global::Newtonsoft.Json.Linq.JToken> source)
		{
			return source.Values(null);
		}

		public static global::System.Collections.Generic.IEnumerable<U?> Values<U>(this global::System.Collections.Generic.IEnumerable<global::Newtonsoft.Json.Linq.JToken> source, object key)
		{
			return source.Values<global::Newtonsoft.Json.Linq.JToken, U>(key);
		}

		public static global::System.Collections.Generic.IEnumerable<U?> Values<U>(this global::System.Collections.Generic.IEnumerable<global::Newtonsoft.Json.Linq.JToken> source)
		{
			return source.Values<global::Newtonsoft.Json.Linq.JToken, U>(null);
		}

		public static U? Value<U>(this global::System.Collections.Generic.IEnumerable<global::Newtonsoft.Json.Linq.JToken> value)
		{
			return value.Value<global::Newtonsoft.Json.Linq.JToken, U>();
		}

		public static U? Value<T, U>(this global::System.Collections.Generic.IEnumerable<T> value) where T : global::Newtonsoft.Json.Linq.JToken
		{
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(value, "value");
			return ((value as global::Newtonsoft.Json.Linq.JToken) ?? throw new global::System.ArgumentException("Source value must be a JToken.")).Convert<global::Newtonsoft.Json.Linq.JToken, U>();
		}

		internal static global::System.Collections.Generic.IEnumerable<U?> Values<T, U>(this global::System.Collections.Generic.IEnumerable<T> source, object? key) where T : global::Newtonsoft.Json.Linq.JToken
		{
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(source, "source");
			if (key == null)
			{
				foreach (T item in source)
				{
					if (item is global::Newtonsoft.Json.Linq.JValue token)
					{
						yield return token.Convert<global::Newtonsoft.Json.Linq.JValue, U>();
						continue;
					}
					foreach (global::Newtonsoft.Json.Linq.JToken item2 in item.Children())
					{
						yield return item2.Convert<global::Newtonsoft.Json.Linq.JToken, U>();
					}
				}
				yield break;
			}
			foreach (T item3 in source)
			{
				global::Newtonsoft.Json.Linq.JToken jToken = item3[key];
				if (jToken != null)
				{
					yield return jToken.Convert<global::Newtonsoft.Json.Linq.JToken, U>();
				}
			}
		}

		public static global::Newtonsoft.Json.Linq.IJEnumerable<global::Newtonsoft.Json.Linq.JToken> Children<T>(this global::System.Collections.Generic.IEnumerable<T> source) where T : global::Newtonsoft.Json.Linq.JToken
		{
			return source.Children<T, global::Newtonsoft.Json.Linq.JToken>().AsJEnumerable();
		}

		public static global::System.Collections.Generic.IEnumerable<U?> Children<T, U>(this global::System.Collections.Generic.IEnumerable<T> source) where T : global::Newtonsoft.Json.Linq.JToken
		{
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(source, "source");
			return global::System.Linq.Enumerable.SelectMany(source, (T c) => c.Children()).Convert<global::Newtonsoft.Json.Linq.JToken, U>();
		}

		internal static global::System.Collections.Generic.IEnumerable<U?> Convert<T, U>(this global::System.Collections.Generic.IEnumerable<T> source) where T : global::Newtonsoft.Json.Linq.JToken
		{
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(source, "source");
			foreach (T item in source)
			{
				yield return item.Convert<global::Newtonsoft.Json.Linq.JToken, U>();
			}
		}

		internal static U? Convert<T, U>(this T token) where T : global::Newtonsoft.Json.Linq.JToken?
		{
			if (token == null)
			{
				return default(U);
			}
			if (token is U)
			{
				U result = (U)(object)((token is U) ? token : null);
				if (typeof(U) != typeof(global::System.IComparable) && typeof(U) != typeof(global::System.IFormattable))
				{
					return result;
				}
			}
			if (!(token is global::Newtonsoft.Json.Linq.JValue jValue))
			{
				throw new global::System.InvalidCastException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Cannot cast {0} to {1}.", global::System.Globalization.CultureInfo.InvariantCulture, token.GetType(), typeof(T)));
			}
			object value = jValue.Value;
			if (value is U)
			{
				return (U)value;
			}
			global::System.Type type = typeof(U);
			if (global::Newtonsoft.Json.Utilities.ReflectionUtils.IsNullableType(type))
			{
				if (jValue.Value == null)
				{
					return default(U);
				}
				type = global::System.Nullable.GetUnderlyingType(type);
			}
			return (U)global::System.Convert.ChangeType(jValue.Value, type, global::System.Globalization.CultureInfo.InvariantCulture);
		}

		public static global::Newtonsoft.Json.Linq.IJEnumerable<global::Newtonsoft.Json.Linq.JToken> AsJEnumerable(this global::System.Collections.Generic.IEnumerable<global::Newtonsoft.Json.Linq.JToken> source)
		{
			return source.AsJEnumerable<global::Newtonsoft.Json.Linq.JToken>();
		}

		public static global::Newtonsoft.Json.Linq.IJEnumerable<T> AsJEnumerable<T>(this global::System.Collections.Generic.IEnumerable<T> source) where T : global::Newtonsoft.Json.Linq.JToken
		{
			if (source == null)
			{
				return null;
			}
			if (source is global::Newtonsoft.Json.Linq.IJEnumerable<T> result)
			{
				return result;
			}
			return new global::Newtonsoft.Json.Linq.JEnumerable<T>(source);
		}
	}
}
