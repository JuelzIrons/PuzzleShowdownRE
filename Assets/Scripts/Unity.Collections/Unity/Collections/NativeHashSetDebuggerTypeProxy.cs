namespace Unity.Collections
{
	internal sealed class NativeHashSetDebuggerTypeProxy<T> where T : unmanaged, global::System.IEquatable<T>
	{
		private unsafe global::Unity.Collections.LowLevel.Unsafe.HashMapHelper<T>* Data;

		public unsafe global::System.Collections.Generic.List<T> Items
		{
			get
			{
				if (Data == null)
				{
					return null;
				}
				global::System.Collections.Generic.List<T> list = new global::System.Collections.Generic.List<T>();
				using global::Unity.Collections.NativeArray<T> nativeArray = Data->GetKeyArray(global::Unity.Collections.Allocator.Temp);
				for (int i = 0; i < nativeArray.Length; i++)
				{
					list.Add(nativeArray[i]);
				}
				return list;
			}
		}

		public unsafe NativeHashSetDebuggerTypeProxy(global::Unity.Collections.NativeHashSet<T> data)
		{
			Data = data.m_Data;
		}
	}
}
