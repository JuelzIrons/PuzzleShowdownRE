namespace Unity.Multiplayer.Tools.Common
{
	internal static class EnumUtil
	{
		public static T[] GetValues<T>() where T : unmanaged, global::System.Enum
		{
			return (T[])global::System.Enum.GetValues(typeof(T));
		}

		public static string[] GetNames<T>() where T : unmanaged, global::System.Enum
		{
			return global::System.Enum.GetNames(typeof(T));
		}

		public static global::System.Collections.Generic.IEnumerable<(string name, T value)> GetValuesAndNames<T>(params T[] skip) where T : unmanaged, global::System.Enum
		{
			string[] names = GetNames<T>();
			foreach (string text in names)
			{
				T val = global::System.Enum.Parse<T>(text);
				if (skip == null || !global::System.Linq.Enumerable.Contains(skip, val))
				{
					yield return (name: text, value: val);
				}
			}
		}

		public unsafe static TUnderlying UnsafeCastToUnderlying<TEnum, TUnderlying>(this TEnum enumValue) where TEnum : unmanaged, global::System.Enum where TUnderlying : unmanaged
		{
			return *(TUnderlying*)(&enumValue);
		}

		public unsafe static int UnsafeCastToInt<TEnum>(this TEnum enumValue) where TEnum : unmanaged, global::System.Enum
		{
			return *(int*)(&enumValue);
		}

		public unsafe static TEnum UnsafeCastToEnum<TEnum>(this int value) where TEnum : unmanaged, global::System.Enum
		{
			return *(TEnum*)(&value);
		}

		public unsafe static TEnum UnsafeCastToEnum<TUnderlying, TEnum>(this TUnderlying value) where TUnderlying : unmanaged where TEnum : unmanaged, global::System.Enum
		{
			return *(TEnum*)(&value);
		}

		public static bool ContainsAny<TEnum>(this TEnum a, TEnum b) where TEnum : unmanaged, global::System.Enum
		{
			return global::Unity.Multiplayer.Tools.Common.IntFlagEnumUtils<TEnum>.ContainsAny(a, b);
		}

		public static bool ContainsAll<TEnum>(this TEnum a, TEnum b) where TEnum : unmanaged, global::System.Enum
		{
			return global::Unity.Multiplayer.Tools.Common.IntFlagEnumUtils<TEnum>.ContainsAll(a, b);
		}

		public static TEnum SetFlags<TEnum>(this TEnum a, TEnum b, bool value) where TEnum : unmanaged, global::System.Enum
		{
			return global::Unity.Multiplayer.Tools.Common.IntFlagEnumUtils<TEnum>.SetFlags(a, b, value);
		}

		public static void SetFlagsInPlace<TEnum>(this ref TEnum a, TEnum b, bool value) where TEnum : unmanaged, global::System.Enum
		{
			a = global::Unity.Multiplayer.Tools.Common.IntFlagEnumUtils<TEnum>.SetFlags(a, b, value);
		}
	}
}
