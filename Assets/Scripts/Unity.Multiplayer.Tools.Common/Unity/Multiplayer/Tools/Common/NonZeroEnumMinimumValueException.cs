namespace Unity.Multiplayer.Tools.Common
{
	internal class NonZeroEnumMinimumValueException<TEnum, TValue> : global::System.Exception where TEnum : unmanaged, global::System.Enum
	{
		public NonZeroEnumMinimumValueException()
			: base("The enum TEnum cannot be used as a key in an EnumMap because its minimum value is non-zero. Consider using a dictionary instead.")
		{
		}
	}
}
