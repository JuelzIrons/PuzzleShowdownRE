namespace UnityEngine.Rendering
{
	internal static class MemoryUtilities
	{
		public unsafe static T* Malloc<T>(int count, global::Unity.Collections.Allocator allocator) where T : unmanaged
		{
			return (T*)global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.Malloc(global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<T>() * count, global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AlignOf<T>(), allocator);
		}

		public unsafe static void Free<T>(T* p, global::Unity.Collections.Allocator allocator) where T : unmanaged
		{
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.Free(p, allocator);
		}
	}
}
