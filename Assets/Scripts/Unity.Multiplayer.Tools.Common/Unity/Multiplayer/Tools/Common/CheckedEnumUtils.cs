namespace Unity.Multiplayer.Tools.Common
{
	internal static class CheckedEnumUtils<TEnum, TUnderlying> where TEnum : unmanaged, global::System.Enum where TUnderlying : unmanaged
	{
		static CheckedEnumUtils()
		{
			if (global::System.Enum.GetUnderlyingType(typeof(TEnum)) != typeof(TUnderlying))
			{
				throw new global::Unity.Multiplayer.Tools.Common.UnhandledEnumUnderlyingTypeException<TEnum, int>();
			}
		}

		public static TUnderlying CheckedCastToUnderlying(TEnum value)
		{
			return value.UnsafeCastToUnderlying<TEnum, TUnderlying>();
		}
	}
}
