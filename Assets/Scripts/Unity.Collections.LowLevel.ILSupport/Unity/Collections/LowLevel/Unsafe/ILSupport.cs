namespace Unity.Collections.LowLevel.Unsafe
{
	internal static class ILSupport
	{
		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public unsafe static void* AddressOf<T>(in T thing) where T : struct
		{
			return global::System.Runtime.CompilerServices.Unsafe.AsPointer(ref thing);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static ref T AsRef<T>(in T thing) where T : struct
		{
			return ref thing;
		}
	}
}
