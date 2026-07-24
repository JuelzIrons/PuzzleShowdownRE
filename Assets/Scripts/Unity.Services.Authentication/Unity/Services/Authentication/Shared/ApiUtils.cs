namespace Unity.Services.Authentication.Shared
{
	internal class ApiUtils
	{
		public static readonly global::System.Text.RegularExpressions.Regex JsonRegex = new global::System.Text.RegularExpressions.Regex("(?i)^(application/json|[^;/ \t]+/[^;/ \t]+[+]json)[ \t]*(;.*)?$");

		public static global::Unity.Services.Authentication.Shared.Multimap<string, string> ParameterToMultiMap(global::Unity.Services.Authentication.Shared.IApiConfiguration configuration, string collectionFormat, string name, object value)
		{
			global::Unity.Services.Authentication.Shared.Multimap<string, string> multimap = new global::Unity.Services.Authentication.Shared.Multimap<string, string>();
			if (value is global::System.Collections.ICollection collection && collectionFormat == "multi")
			{
				foreach (object item in collection)
				{
					multimap.Add(name, ParameterToString(configuration, item));
				}
			}
			else if (value is global::System.Collections.IDictionary dictionary)
			{
				if (collectionFormat == "deepObject")
				{
					foreach (global::System.Collections.DictionaryEntry item2 in dictionary)
					{
						multimap.Add(name + "[" + item2.Key?.ToString() + "]", ParameterToString(configuration, item2.Value));
					}
				}
				else
				{
					foreach (global::System.Collections.DictionaryEntry item3 in dictionary)
					{
						multimap.Add(item3.Key.ToString(), ParameterToString(configuration, item3.Value));
					}
				}
			}
			else
			{
				multimap.Add(name, ParameterToString(configuration, value));
			}
			return multimap;
		}

		public static string ParameterToString(global::Unity.Services.Authentication.Shared.IApiConfiguration configuration, object obj)
		{
			if (obj is global::System.DateTime dateTime)
			{
				return dateTime.ToString(configuration.DateTimeFormat);
			}
			if (obj is global::System.DateTimeOffset dateTimeOffset)
			{
				return dateTimeOffset.ToString(configuration.DateTimeFormat);
			}
			if (obj is bool)
			{
				if (!(bool)obj)
				{
					return "false";
				}
				return "true";
			}
			if (obj is global::System.Collections.ICollection source)
			{
				return string.Join(",", global::System.Linq.Enumerable.Cast<object>(source));
			}
			if (obj is global::System.Enum && HasEnumMemberAttrValue(obj))
			{
				return GetEnumMemberAttrValue(obj);
			}
			return global::System.Convert.ToString(obj, global::System.Globalization.CultureInfo.InvariantCulture);
		}

		public static string Base64Encode(string text)
		{
			return global::System.Convert.ToBase64String(global::System.Text.Encoding.UTF8.GetBytes(text));
		}

		public static string SelectHeaderContentType(string[] contentTypes)
		{
			if (contentTypes.Length == 0)
			{
				return null;
			}
			foreach (string text in contentTypes)
			{
				if (IsJsonMime(text))
				{
					return text;
				}
			}
			return contentTypes[0];
		}

		public static string SelectHeaderAccept(string[] accepts)
		{
			if (accepts.Length == 0)
			{
				return null;
			}
			if (global::System.Linq.Enumerable.Contains(accepts, "application/json", global::System.StringComparer.OrdinalIgnoreCase))
			{
				return "application/json";
			}
			return string.Join(",", accepts);
		}

		public static bool IsJsonMime(string mime)
		{
			if (string.IsNullOrWhiteSpace(mime))
			{
				return false;
			}
			if (!JsonRegex.IsMatch(mime))
			{
				return mime.Equals("application/json-patch+json");
			}
			return true;
		}

		private static bool HasEnumMemberAttrValue(object enumVal)
		{
			if (enumVal == null)
			{
				throw new global::System.ArgumentNullException("enumVal");
			}
			if (global::System.Linq.Enumerable.FirstOrDefault(global::System.Linq.Enumerable.OfType<global::System.Runtime.Serialization.EnumMemberAttribute>(global::System.Linq.Enumerable.FirstOrDefault(enumVal.GetType().GetMember(enumVal.ToString() ?? throw new global::System.InvalidOperationException()))?.GetCustomAttributes(inherit: false))) != null)
			{
				return true;
			}
			return false;
		}

		private static string GetEnumMemberAttrValue(object enumVal)
		{
			if (enumVal == null)
			{
				throw new global::System.ArgumentNullException("enumVal");
			}
			return (global::System.Linq.Enumerable.FirstOrDefault(global::System.Linq.Enumerable.OfType<global::System.Runtime.Serialization.EnumMemberAttribute>(global::System.Linq.Enumerable.FirstOrDefault(enumVal.GetType().GetMember(enumVal.ToString() ?? throw new global::System.InvalidOperationException()))?.GetCustomAttributes(inherit: false))))?.Value;
		}
	}
}
