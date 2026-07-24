namespace UnityEngine.Rendering.Universal
{
	internal static class NativeArrayExtensions
	{
		public unsafe static ref T UnsafeElementAt<T>(this global::Unity.Collections.NativeArray<T> array, int index) where T : struct
		{
			return ref global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.ArrayElementAsRef<T>(global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafeReadOnlyPtr(array), index);
		}

		public unsafe static ref T UnsafeElementAtMutable<T>(this global::Unity.Collections.NativeArray<T> array, int index) where T : struct
		{
			return ref global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.ArrayElementAsRef<T>(global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(array), index);
		}
	}
}
