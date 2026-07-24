namespace UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler
{
	internal static class NativeListExtensions
	{
		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		internal unsafe static global::System.ReadOnlySpan<T> MakeReadOnlySpan<T>(this ref global::Unity.Collections.NativeList<T> list, int first, int numElements) where T : unmanaged
		{
			return new global::System.ReadOnlySpan<T>(global::Unity.Collections.LowLevel.Unsafe.NativeListUnsafeUtility.GetUnsafeReadOnlyPtr(list) + first, numElements);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		internal static int LastIndex<T>(this ref global::Unity.Collections.NativeList<T> list) where T : unmanaged
		{
			return list.Length - 1;
		}
	}
}
