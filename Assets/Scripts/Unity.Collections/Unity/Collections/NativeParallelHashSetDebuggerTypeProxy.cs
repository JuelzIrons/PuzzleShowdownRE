namespace Unity.Collections
{
	internal sealed class NativeParallelHashSetDebuggerTypeProxy<T> where T : unmanaged, global::System.IEquatable<T>
	{
		private global::Unity.Collections.NativeParallelHashSet<T> Data;

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

		public NativeParallelHashSetDebuggerTypeProxy(global::Unity.Collections.NativeParallelHashSet<T> data)
		{
			Data = data;
		}
	}
}
