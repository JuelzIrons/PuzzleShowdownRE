namespace Unity.Multiplayer.Tools.Common
{
	internal class UnhandledEnumBackingTypeException<TEnum, TValue> : global::System.Exception where TEnum : unmanaged, global::System.Enum
	{
		public UnhandledEnumBackingTypeException()
			: base("The enum TEnum cannot be used as a key in an EnumMap " + string.Format("because its backing type {0} is not {1}. ", global::System.Enum.GetUnderlyingType(typeof(TEnum)), "Int32") + "This constraint is required by EnumMap.CastEnumToInt.")
		{
		}
	}
}
