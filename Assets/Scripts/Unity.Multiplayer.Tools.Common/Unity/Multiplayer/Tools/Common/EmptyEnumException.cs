namespace Unity.Multiplayer.Tools.Common
{
	internal class EmptyEnumException<TEnum, TValue> : global::System.Exception where TEnum : unmanaged, global::System.Enum
	{
		public EmptyEnumException()
			: base("The enum TEnum cannot be used as a key in an EnumMap because it is empty and has no values.")
		{
		}
	}
}
