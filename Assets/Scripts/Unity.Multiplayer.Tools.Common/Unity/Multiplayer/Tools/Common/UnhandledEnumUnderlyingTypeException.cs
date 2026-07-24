namespace Unity.Multiplayer.Tools.Common
{
	internal class UnhandledEnumUnderlyingTypeException<TEnum, TRequiredUnderlyingType> : global::System.Exception where TEnum : unmanaged, global::System.Enum
	{
		public UnhandledEnumUnderlyingTypeException()
			: base("Cannot use ContainsAny, SetFlags, or SetFlagsInPlace " + string.Format("on enum {0}, because its underlying type {1} is not the required", "TEnum", typeof(TEnum).UnderlyingSystemType) + $" underlying type {typeof(TRequiredUnderlyingType)}")
		{
		}
	}
}
