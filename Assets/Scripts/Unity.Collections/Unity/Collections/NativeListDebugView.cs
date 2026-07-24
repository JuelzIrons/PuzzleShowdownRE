namespace Unity.Collections
{
	internal sealed class NativeListDebugView<T> where T : unmanaged
	{
		private unsafe global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T>* Data;

		public unsafe T[] Items
		{
			get
			{
				if (Data == null)
				{
					return null;
				}
				int length = Data->Length;
				T[] array = new T[length];
				fixed (T* destination = &array[0])
				{
					global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(destination, Data->Ptr, length * global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<T>());
				}
				return array;
			}
		}

		public unsafe NativeListDebugView(global::Unity.Collections.NativeList<T> array)
		{
			Data = array.m_ListData;
		}
	}
}
