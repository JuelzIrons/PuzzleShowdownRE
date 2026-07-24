namespace Unity.Collections.LowLevel.Unsafe
{
	[global::Unity.Collections.GenerateTestsForBurstCompatibility]
	internal static class UnsafePtrListExtensions
	{
		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
		public static ref global::Unity.Collections.LowLevel.Unsafe.UnsafeList<global::System.IntPtr> ListData<T>(this ref global::Unity.Collections.LowLevel.Unsafe.UnsafePtrList<T> from) where T : unmanaged
		{
			return ref global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.As<global::Unity.Collections.LowLevel.Unsafe.UnsafePtrList<T>, global::Unity.Collections.LowLevel.Unsafe.UnsafeList<global::System.IntPtr>>(ref from);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
		public static global::Unity.Collections.LowLevel.Unsafe.UnsafeList<global::System.IntPtr> ListDataRO<T>(this global::Unity.Collections.LowLevel.Unsafe.UnsafePtrList<T> from) where T : unmanaged
		{
			return global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.As<global::Unity.Collections.LowLevel.Unsafe.UnsafePtrList<T>, global::Unity.Collections.LowLevel.Unsafe.UnsafeList<global::System.IntPtr>>(ref from);
		}
	}
}
