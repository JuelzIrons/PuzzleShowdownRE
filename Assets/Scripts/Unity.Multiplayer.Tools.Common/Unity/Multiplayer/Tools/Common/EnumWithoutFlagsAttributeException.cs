namespace Unity.Multiplayer.Tools.Common
{
	internal class EnumWithoutFlagsAttributeException<TEnum> : global::System.Exception where TEnum : unmanaged, global::System.Enum
	{
		public EnumWithoutFlagsAttributeException()
			: base("Cannot use ContainsAny, SetFlags, or SetFlagsInPlace on enum TEnum, as it does not have the FlagsAttribute attribute")
		{
		}
	}
}
