namespace Unity.Collections.LowLevel.Unsafe
{
	internal sealed class UnsafeParallelHashSetDebuggerTypeProxy<T> where T : unmanaged, global::System.IEquatable<T>
	{
		private global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashSet<T> Data;

		public global::System.Collections.Generic.List<T> Items
		{
			get
			{
				global::System.Collections.Generic.List<T> list = new global::System.Collections.Generic.List<T>();
				using global::Unity.Collections.NativeArray<T> nativeArray = Data.ToNativeArray(global::Unity.Collections.Allocator.Temp);
				for (int i = 0; i < nativeArray.Length; i++)
				{
					list.Add(nativeArray[i]);
				}
				return list;
			}
		}

		public UnsafeParallelHashSetDebuggerTypeProxy(global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashSet<T> data)
		{
			Data = data;
		}
	}
}
