namespace Unity.Multiplayer.Tools.Common
{
	internal class DiscontinuousEnumException<TEnum, TValue> : global::System.Exception where TEnum : unmanaged, global::System.Enum
	{
		public DiscontinuousEnumException()
			: base("The enum TEnum cannot be used as a key in an EnumMap because it is discontinuous, and EnumMap requires continuous keys for storage in a fixed array. Consider using a dictionary instead.")
		{
		}
	}
}
