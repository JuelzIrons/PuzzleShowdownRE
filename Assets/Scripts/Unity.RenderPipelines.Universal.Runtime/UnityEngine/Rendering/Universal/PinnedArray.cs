namespace UnityEngine.Rendering.Universal
{
	internal struct PinnedArray<T> : global::System.IDisposable where T : struct
	{
		public T[] managedArray;

		public global::System.Runtime.InteropServices.GCHandle handle;

		public global::Unity.Collections.NativeArray<T> nativeArray;

		public int length
		{
			get
			{
				if (managedArray == null)
				{
					return 0;
				}
				return managedArray.Length;
			}
		}

		public unsafe PinnedArray(int length)
		{
			managedArray = new T[length];
			handle = global::System.Runtime.InteropServices.GCHandle.Alloc(managedArray, global::System.Runtime.InteropServices.GCHandleType.Pinned);
			nativeArray = global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>((void*)handle.AddrOfPinnedObject(), length, global::Unity.Collections.Allocator.None);
		}

		public void Dispose()
		{
			if (managedArray != null)
			{
				handle.Free();
				this = default(global::UnityEngine.Rendering.Universal.PinnedArray<T>);
			}
		}
	}
}
