namespace System.Numerics
{
	internal static class BitOperations
	{
		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint RotateLeft(uint value, int offset)
		{
			return (value << offset) | (value >> 32 - offset);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static ulong RotateLeft(ulong value, int offset)
		{
			return (value << offset) | (value >> 64 - offset);
		}
	}
}
