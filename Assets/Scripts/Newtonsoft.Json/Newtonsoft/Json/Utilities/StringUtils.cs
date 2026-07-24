namespace Newtonsoft.Json.Utilities
{
	internal static class StringUtils
	{
		private enum SeparatedCaseState
		{
			Start = 0,
			Lower = 1,
			Upper = 2,
			NewWord = 3
		}

		public const string CarriageReturnLineFeed = "\r\n";

		public const string Empty = "";

		public const char CarriageReturn = '\r';

		public const char LineFeed = '\n';

		public const char Tab = '\t';

		public static bool IsNullOrEmpty([global::System.Diagnostics.CodeAnalysis.NotNullWhen(false)] string? value)
		{
			return string.IsNullOrEmpty(value);
		}

		public static string FormatWith(this string format, global::System.IFormatProvider provider, object? arg0)
		{
			return format.FormatWith(provider, new object[1] { arg0 });
		}

		public static string FormatWith(this string format, global::System.IFormatProvider provider, object? arg0, object? arg1)
		{
			return format.FormatWith(provider, new object[2] { arg0, arg1 });
		}

		public static string FormatWith(this string format, global::System.IFormatProvider provider, object? arg0, object? arg1, object? arg2)
		{
			return format.FormatWith(provider, new object[3] { arg0, arg1, arg2 });
		}

		public static string FormatWith(this string format, global::System.IFormatProvider provider, object? arg0, object? arg1, object? arg2, object? arg3)
		{
			return format.FormatWith(provider, new object[4] { arg0, arg1, arg2, arg3 });
		}

		private static string FormatWith(this string format, global::System.IFormatProvider provider, params object?[] args)
		{
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(format, "format");
			return string.Format(provider, format, args);
		}

		public static bool IsWhiteSpace(string s)
		{
			if (s == null)
			{
				throw new global::System.ArgumentNullException("s");
			}
			if (s.Length == 0)
			{
				return false;
			}
			for (int i = 0; i < s.Length; i++)
			{
				if (!char.IsWhiteSpace(s[i]))
				{
					return false;
				}
			}
			return true;
		}

		public static global::System.IO.StringWriter CreateStringWriter(int capacity)
		{
			return new global::System.IO.StringWriter(new global::System.Text.StringBuilder(capacity), global::System.Globalization.CultureInfo.InvariantCulture);
		}

		public static void ToCharAsUnicode(char c, char[] buffer)
		{
			buffer[0] = '\\';
			buffer[1] = 'u';
			buffer[2] = global::Newtonsoft.Json.Utilities.MathUtils.IntToHex(((int)c >> 12) & 0xF);
			buffer[3] = global::Newtonsoft.Json.Utilities.MathUtils.IntToHex(((int)c >> 8) & 0xF);
			buffer[4] = global::Newtonsoft.Json.Utilities.MathUtils.IntToHex(((int)c >> 4) & 0xF);
			buffer[5] = global::Newtonsoft.Json.Utilities.MathUtils.IntToHex(c & 0xF);
		}

		public static TSource? ForgivingCaseSensitiveFind<TSource>(this global::System.Collections.Generic.IEnumerable<TSource> source, global::System.Func<TSource, string> valueSelector, string testValue)
		{
			if (source == null)
			{
				throw new global::System.ArgumentNullException("source");
			}
			if (valueSelector == null)
			{
				throw new global::System.ArgumentNullException("valueSelector");
			}
			global::System.Collections.Generic.IEnumerable<TSource> source2 = global::System.Linq.Enumerable.Where<TSource>(source, (TSource s) => string.Equals(valueSelector(s), testValue, global::System.StringComparison.OrdinalIgnoreCase));
			if (global::System.Linq.Enumerable.Count(source2) <= 1)
			{
				return global::System.Linq.Enumerable.SingleOrDefault(source2);
			}
			return global::System.Linq.Enumerable.SingleOrDefault(global::System.Linq.Enumerable.Where<TSource>(source, (TSource s) => string.Equals(valueSelector(s), testValue, global::System.StringComparison.Ordinal)));
		}

		public static string ToCamelCase(string s)
		{
			if (IsNullOrEmpty(s) || !char.IsUpper(s[0]))
			{
				return s;
			}
			char[] array = s.ToCharArray();
			for (int i = 0; i < array.Length && (i != 1 || char.IsUpper(array[i])); i++)
			{
				bool flag = i + 1 < array.Length;
				if (i > 0 && flag && !char.IsUpper(array[i + 1]))
				{
					if (char.IsSeparator(array[i + 1]))
					{
						array[i] = ToLower(array[i]);
					}
					break;
				}
				array[i] = ToLower(array[i]);
			}
			return new string(array);
		}

		private static char ToLower(char c)
		{
			c = char.ToLower(c, global::System.Globalization.CultureInfo.InvariantCulture);
			return c;
		}

		public static string ToSnakeCase(string s)
		{
			return ToSeparatedCase(s, '_');
		}

		public static string ToKebabCase(string s)
		{
			return ToSeparatedCase(s, '-');
		}

		private static string ToSeparatedCase(string s, char separator)
		{
			if (IsNullOrEmpty(s))
			{
				return s;
			}
			global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder();
			global::Newtonsoft.Json.Utilities.StringUtils.SeparatedCaseState separatedCaseState = global::Newtonsoft.Json.Utilities.StringUtils.SeparatedCaseState.Start;
			for (int i = 0; i < s.Length; i++)
			{
				if (s[i] == ' ')
				{
					if (separatedCaseState != global::Newtonsoft.Json.Utilities.StringUtils.SeparatedCaseState.Start)
					{
						separatedCaseState = global::Newtonsoft.Json.Utilities.StringUtils.SeparatedCaseState.NewWord;
					}
				}
				else if (char.IsUpper(s[i]))
				{
					switch (separatedCaseState)
					{
					case global::Newtonsoft.Json.Utilities.StringUtils.SeparatedCaseState.Upper:
					{
						bool flag = i + 1 < s.Length;
						if (i > 0 && flag)
						{
							char c = s[i + 1];
							if (!char.IsUpper(c) && c != separator)
							{
								stringBuilder.Append(separator);
							}
						}
						break;
					}
					case global::Newtonsoft.Json.Utilities.StringUtils.SeparatedCaseState.Lower:
					case global::Newtonsoft.Json.Utilities.StringUtils.SeparatedCaseState.NewWord:
						stringBuilder.Append(separator);
						break;
					}
					char value = char.ToLower(s[i], global::System.Globalization.CultureInfo.InvariantCulture);
					stringBuilder.Append(value);
					separatedCaseState = global::Newtonsoft.Json.Utilities.StringUtils.SeparatedCaseState.Upper;
				}
				else if (s[i] == separator)
				{
					stringBuilder.Append(separator);
					separatedCaseState = global::Newtonsoft.Json.Utilities.StringUtils.SeparatedCaseState.Start;
				}
				else
				{
					if (separatedCaseState == global::Newtonsoft.Json.Utilities.StringUtils.SeparatedCaseState.NewWord)
					{
						stringBuilder.Append(separator);
					}
					stringBuilder.Append(s[i]);
					separatedCaseState = global::Newtonsoft.Json.Utilities.StringUtils.SeparatedCaseState.Lower;
				}
			}
			return stringBuilder.ToString();
		}

		public static bool IsHighSurrogate(char c)
		{
			return char.IsHighSurrogate(c);
		}

		public static bool IsLowSurrogate(char c)
		{
			return char.IsLowSurrogate(c);
		}

		public static int IndexOf(string s, char c)
		{
			return s.IndexOf(c);
		}

		public static string Replace(string s, string oldValue, string newValue)
		{
			return s.Replace(oldValue, newValue);
		}

		public static bool StartsWith(this string source, char value)
		{
			if (source.Length > 0)
			{
				return source[0] == value;
			}
			return false;
		}

		public static bool EndsWith(this string source, char value)
		{
			if (source.Length > 0)
			{
				return source[source.Length - 1] == value;
			}
			return false;
		}

		public static string Trim(this string s, int start, int length)
		{
			if (s == null)
			{
				throw new global::System.ArgumentNullException();
			}
			if (start < 0)
			{
				throw new global::System.ArgumentOutOfRangeException("start");
			}
			if (length < 0)
			{
				throw new global::System.ArgumentOutOfRangeException("length");
			}
			int num = start + length - 1;
			if (num >= s.Length)
			{
				throw new global::System.ArgumentOutOfRangeException("length");
			}
			while (start < num && char.IsWhiteSpace(s[start]))
			{
				start++;
			}
			while (num >= start && char.IsWhiteSpace(s[num]))
			{
				num--;
			}
			return s.Substring(start, num - start + 1);
		}
	}
}
