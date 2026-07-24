namespace Unity.Multiplayer.Tools.Common
{
	internal static class IntFlagEnumUtils<TEnum> where TEnum : unmanaged, global::System.Enum
	{
		static IntFlagEnumUtils()
		{
			global::System.Type typeFromHandle = typeof(TEnum);
			if (typeFromHandle.GetCustomAttributes(typeof(global::System.FlagsAttribute), inherit: true).Length == 0)
			{
				throw new global::Unity.Multiplayer.Tools.Common.EnumWithoutFlagsAttributeException<TEnum>();
			}
			if (global::System.Enum.GetUnderlyingType(typeFromHandle) != typeof(int))
			{
				throw new global::Unity.Multiplayer.Tools.Common.UnhandledEnumUnderlyingTypeException<TEnum, int>();
			}
		}

		public static bool ContainsAny(TEnum a, TEnum b)
		{
			return (a.UnsafeCastToInt() & b.UnsafeCastToInt()) != 0;
		}

		public static bool ContainsAll(TEnum a, TEnum b)
		{
			return (a.UnsafeCastToInt() & b.UnsafeCastToInt()) == b.UnsafeCastToInt();
		}

		public static TEnum SetFlags(TEnum a, TEnum b, bool value)
		{
			return (value ? (a.UnsafeCastToInt() | b.UnsafeCastToInt()) : (a.UnsafeCastToInt() & ~b.UnsafeCastToInt())).UnsafeCastToEnum<TEnum>();
		}
	}
}
