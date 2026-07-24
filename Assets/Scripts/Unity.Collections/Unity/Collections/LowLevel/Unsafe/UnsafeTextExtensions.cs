namespace Unity.Collections.LowLevel.Unsafe
{
	internal static class UnsafeTextExtensions
	{
		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static ref global::Unity.Collections.LowLevel.Unsafe.UnsafeList<byte> AsUnsafeListOfBytes(this ref global::Unity.Collections.LowLevel.Unsafe.UnsafeText text)
		{
			return ref global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.As<global::Unity.Collections.LowLevel.Unsafe.UntypedUnsafeList, global::Unity.Collections.LowLevel.Unsafe.UnsafeList<byte>>(ref text.m_UntypedListData);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Collections.LowLevel.Unsafe.UnsafeList<byte> AsUnsafeListOfBytesRO(this global::Unity.Collections.LowLevel.Unsafe.UnsafeText text)
		{
			return global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.As<global::Unity.Collections.LowLevel.Unsafe.UntypedUnsafeList, global::Unity.Collections.LowLevel.Unsafe.UnsafeList<byte>>(ref text.m_UntypedListData);
		}
	}
}
