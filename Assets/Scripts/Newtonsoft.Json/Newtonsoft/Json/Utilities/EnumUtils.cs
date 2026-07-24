namespace Newtonsoft.Json.Utilities
{
	internal static class EnumUtils
	{
		private const char EnumSeparatorChar = ',';

		private const string EnumSeparatorString = ", ";

		private static readonly global::Newtonsoft.Json.Utilities.ThreadSafeStore<global::Newtonsoft.Json.Utilities.StructMultiKey<global::System.Type, global::Newtonsoft.Json.Serialization.NamingStrategy?>, global::Newtonsoft.Json.Utilities.EnumInfo> ValuesAndNamesPerEnum = new global::Newtonsoft.Json.Utilities.ThreadSafeStore<global::Newtonsoft.Json.Utilities.StructMultiKey<global::System.Type, global::Newtonsoft.Json.Serialization.NamingStrategy>, global::Newtonsoft.Json.Utilities.EnumInfo>(InitializeValuesAndNames);

		private static global::Newtonsoft.Json.Serialization.CamelCaseNamingStrategy _camelCaseNamingStrategy = new global::Newtonsoft.Json.Serialization.CamelCaseNamingStrategy();

		private static global::Newtonsoft.Json.Utilities.EnumInfo InitializeValuesAndNames(global::Newtonsoft.Json.Utilities.StructMultiKey<global::System.Type, global::Newtonsoft.Json.Serialization.NamingStrategy?> key)
		{
			global::System.Type value = key.Value1;
			string[] names = global::System.Enum.GetNames(value);
			string[] array = new string[names.Length];
			ulong[] array2 = new ulong[names.Length];
			for (int i = 0; i < names.Length; i++)
			{
				string text = names[i];
				global::System.Reflection.FieldInfo field = value.GetField(text, global::System.Reflection.BindingFlags.Static | global::System.Reflection.BindingFlags.Public | global::System.Reflection.BindingFlags.NonPublic);
				array2[i] = ToUInt64(field.GetValue(null));
				string text2 = global::System.Linq.Enumerable.SingleOrDefault(global::System.Linq.Enumerable.Select(global::System.Linq.Enumerable.Cast<global::System.Runtime.Serialization.EnumMemberAttribute>(field.GetCustomAttributes(typeof(global::System.Runtime.Serialization.EnumMemberAttribute), inherit: true)), (global::System.Runtime.Serialization.EnumMemberAttribute a) => a.Value));
				bool hasSpecifiedName = text2 != null;
				if (text2 == null)
				{
					text2 = text;
				}
				string text3 = text2;
				if (global::System.Array.IndexOf(array, text3, 0, i) != -1)
				{
					throw new global::System.InvalidOperationException("Enum name '{0}' already exists on enum '{1}'.".FormatWith(global::System.Globalization.CultureInfo.InvariantCulture, text3, value.Name));
				}
				array[i] = ((key.Value2 != null) ? key.Value2.GetPropertyName(text3, hasSpecifiedName) : text3);
			}
			return new global::Newtonsoft.Json.Utilities.EnumInfo(value.IsDefined(typeof(global::System.FlagsAttribute), inherit: false), array2, names, array);
		}

		public static global::System.Collections.Generic.IList<T> GetFlagsValues<T>(T value) where T : struct
		{
			global::System.Type typeFromHandle = typeof(T);
			if (!typeFromHandle.IsDefined(typeof(global::System.FlagsAttribute), inherit: false))
			{
				throw new global::System.ArgumentException("Enum type {0} is not a set of flags.".FormatWith(global::System.Globalization.CultureInfo.InvariantCulture, typeFromHandle));
			}
			global::System.Type underlyingType = global::System.Enum.GetUnderlyingType(value.GetType());
			ulong num = ToUInt64(value);
			global::Newtonsoft.Json.Utilities.EnumInfo enumValuesAndNames = GetEnumValuesAndNames(typeFromHandle);
			global::System.Collections.Generic.IList<T> list = new global::System.Collections.Generic.List<T>();
			for (int i = 0; i < enumValuesAndNames.Values.Length; i++)
			{
				ulong num2 = enumValuesAndNames.Values[i];
				if ((num & num2) == num2 && num2 != 0L)
				{
					list.Add((T)global::System.Convert.ChangeType(num2, underlyingType, global::System.Globalization.CultureInfo.CurrentCulture));
				}
			}
			if (list.Count == 0 && global::System.Linq.Enumerable.Any(enumValuesAndNames.Values, (ulong v) => v == 0))
			{
				list.Add(default(T));
			}
			return list;
		}

		public static bool TryToString(global::System.Type enumType, object value, bool camelCase, [global::System.Diagnostics.CodeAnalysis.NotNullWhen(true)] out string? name)
		{
			return TryToString(enumType, value, camelCase ? _camelCaseNamingStrategy : null, out name);
		}

		public static bool TryToString(global::System.Type enumType, object value, global::Newtonsoft.Json.Serialization.NamingStrategy? namingStrategy, [global::System.Diagnostics.CodeAnalysis.NotNullWhen(true)] out string? name)
		{
			global::Newtonsoft.Json.Utilities.EnumInfo enumInfo = ValuesAndNamesPerEnum.Get(new global::Newtonsoft.Json.Utilities.StructMultiKey<global::System.Type, global::Newtonsoft.Json.Serialization.NamingStrategy>(enumType, namingStrategy));
			ulong num = ToUInt64(value);
			if (!enumInfo.IsFlags)
			{
				int num2 = global::System.Array.BinarySearch(enumInfo.Values, num);
				if (num2 >= 0)
				{
					name = enumInfo.ResolvedNames[num2];
					return true;
				}
				name = null;
				return false;
			}
			name = InternalFlagsFormat(enumInfo, num);
			return name != null;
		}

		private static string? InternalFlagsFormat(global::Newtonsoft.Json.Utilities.EnumInfo entry, ulong result)
		{
			string[] resolvedNames = entry.ResolvedNames;
			ulong[] values = entry.Values;
			int num = values.Length - 1;
			global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder();
			bool flag = true;
			ulong num2 = result;
			while (num >= 0 && (num != 0 || values[num] != 0L))
			{
				if ((result & values[num]) == values[num])
				{
					result -= values[num];
					if (!flag)
					{
						stringBuilder.Insert(0, ", ");
					}
					string value = resolvedNames[num];
					stringBuilder.Insert(0, value);
					flag = false;
				}
				num--;
			}
			if (result != 0L)
			{
				return null;
			}
			if (num2 == 0L)
			{
				if (values.Length != 0 && values[0] == 0L)
				{
					return resolvedNames[0];
				}
				return null;
			}
			return stringBuilder.ToString();
		}

		public static global::Newtonsoft.Json.Utilities.EnumInfo GetEnumValuesAndNames(global::System.Type enumType)
		{
			return ValuesAndNamesPerEnum.Get(new global::Newtonsoft.Json.Utilities.StructMultiKey<global::System.Type, global::Newtonsoft.Json.Serialization.NamingStrategy>(enumType, null));
		}

		private static ulong ToUInt64(object value)
		{
			bool isEnum;
			return global::Newtonsoft.Json.Utilities.ConvertUtils.GetTypeCode(value.GetType(), out isEnum) switch
			{
				global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.SByte => (ulong)(sbyte)value, 
				global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Byte => (byte)value, 
				global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Boolean => global::System.Convert.ToByte((bool)value), 
				global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Int16 => (ulong)(short)value, 
				global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.UInt16 => (ushort)value, 
				global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Char => (char)value, 
				global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.UInt32 => (uint)value, 
				global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Int32 => (ulong)(int)value, 
				global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.UInt64 => (ulong)value, 
				global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Int64 => (ulong)(long)value, 
				_ => throw new global::System.InvalidOperationException("Unknown enum type."), 
			};
		}

		public static object ParseEnum(global::System.Type enumType, global::Newtonsoft.Json.Serialization.NamingStrategy? namingStrategy, string value, bool disallowNumber)
		{
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(enumType, "enumType");
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(value, "value");
			if (!enumType.IsEnum())
			{
				throw new global::System.ArgumentException("Type provided must be an Enum.", "enumType");
			}
			global::Newtonsoft.Json.Utilities.EnumInfo enumInfo = ValuesAndNamesPerEnum.Get(new global::Newtonsoft.Json.Utilities.StructMultiKey<global::System.Type, global::Newtonsoft.Json.Serialization.NamingStrategy>(enumType, namingStrategy));
			string[] names = enumInfo.Names;
			string[] resolvedNames = enumInfo.ResolvedNames;
			ulong[] values = enumInfo.Values;
			int? num = FindIndexByName(resolvedNames, value, 0, value.Length, global::System.StringComparison.Ordinal);
			if (num.HasValue)
			{
				return global::System.Enum.ToObject(enumType, values[num.Value]);
			}
			int num2 = -1;
			for (int i = 0; i < value.Length; i++)
			{
				if (!char.IsWhiteSpace(value[i]))
				{
					num2 = i;
					break;
				}
			}
			if (num2 == -1)
			{
				throw new global::System.ArgumentException("Must specify valid information for parsing in the string.");
			}
			char c = value[num2];
			if (char.IsDigit(c) || c == '-' || c == '+')
			{
				global::System.Type underlyingType = global::System.Enum.GetUnderlyingType(enumType);
				value = value.Trim();
				object obj = null;
				try
				{
					obj = global::System.Convert.ChangeType(value, underlyingType, global::System.Globalization.CultureInfo.InvariantCulture);
				}
				catch (global::System.FormatException)
				{
				}
				if (obj != null)
				{
					if (disallowNumber)
					{
						throw new global::System.FormatException("Integer string '{0}' is not allowed.".FormatWith(global::System.Globalization.CultureInfo.InvariantCulture, value));
					}
					return global::System.Enum.ToObject(enumType, obj);
				}
			}
			ulong num3 = 0uL;
			int j = num2;
			while (j <= value.Length)
			{
				int num4 = value.IndexOf(',', j);
				if (num4 == -1)
				{
					num4 = value.Length;
				}
				int num5 = num4;
				for (; j < num4 && char.IsWhiteSpace(value[j]); j++)
				{
				}
				while (num5 > j && char.IsWhiteSpace(value[num5 - 1]))
				{
					num5--;
				}
				int valueSubstringLength = num5 - j;
				num = MatchName(value, names, resolvedNames, j, valueSubstringLength, global::System.StringComparison.Ordinal);
				if (!num.HasValue)
				{
					num = MatchName(value, names, resolvedNames, j, valueSubstringLength, global::System.StringComparison.OrdinalIgnoreCase);
				}
				if (!num.HasValue)
				{
					num = FindIndexByName(resolvedNames, value, 0, value.Length, global::System.StringComparison.OrdinalIgnoreCase);
					if (num.HasValue)
					{
						return global::System.Enum.ToObject(enumType, values[num.Value]);
					}
					throw new global::System.ArgumentException("Requested value '{0}' was not found.".FormatWith(global::System.Globalization.CultureInfo.InvariantCulture, value));
				}
				num3 |= values[num.Value];
				j = num4 + 1;
			}
			return global::System.Enum.ToObject(enumType, num3);
		}

		private static int? MatchName(string value, string[] enumNames, string[] resolvedNames, int valueIndex, int valueSubstringLength, global::System.StringComparison comparison)
		{
			int? result = FindIndexByName(resolvedNames, value, valueIndex, valueSubstringLength, comparison);
			if (!result.HasValue)
			{
				result = FindIndexByName(enumNames, value, valueIndex, valueSubstringLength, comparison);
			}
			return result;
		}

		private static int? FindIndexByName(string[] enumNames, string value, int valueIndex, int valueSubstringLength, global::System.StringComparison comparison)
		{
			for (int i = 0; i < enumNames.Length; i++)
			{
				if (enumNames[i].Length == valueSubstringLength && string.Compare(enumNames[i], 0, value, valueIndex, valueSubstringLength, comparison) == 0)
				{
					return i;
				}
			}
			return null;
		}
	}
}
