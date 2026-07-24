namespace Unity.Collections.LowLevel.Unsafe
{
	internal sealed class UnsafeHashSetDebuggerTypeProxy<T> where T : unmanaged, global::System.IEquatable<T>
	{
		private global::Unity.Collections.LowLevel.Unsafe.HashMapHelper<T> Data;

		public global::System.Collections.Generic.List<T> Items
		{
			get
			{
				global::System.Collections.Generic.List<T> list = new global::System.Collections.Generic.List<T>();
				using global::Unity.Collections.NativeArray<T> nativeArray = Data.GetKeyArray(global::Unity.Collections.Allocator.Temp);
				for (int i = 0; i < nativeArray.Length; i++)
				{
					list.Add(nativeArray[i]);
				}
				return list;
			}
		}

		public UnsafeHashSetDebuggerTypeProxy(global::Unity.Collections.LowLevel.Unsafe.UnsafeHashSet<T> data)
		{
			Data = data.m_Data;
		}
	}
}
