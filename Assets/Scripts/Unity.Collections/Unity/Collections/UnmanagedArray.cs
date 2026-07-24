namespace Unity.Collections
{
	internal struct UnmanagedArray<T> : global::System.IDisposable where T : unmanaged
	{
		private global::System.IntPtr m_pointer;

		private int m_length;

		private global::Unity.Collections.AllocatorManager.AllocatorHandle m_allocator;

		public int Length => m_length;

		public unsafe ref T this[int index]
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return ref *(T*)((byte*)(void*)m_pointer + (nint)index * (nint)sizeof(T));
			}
		}

		public unsafe UnmanagedArray(int length, global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
		{
			m_pointer = (global::System.IntPtr)global::Unity.Collections.Memory.Unmanaged.Array.Allocate<T>(length, allocator);
			m_length = length;
			m_allocator = allocator;
		}

		public unsafe void Dispose()
		{
			global::Unity.Collections.Memory.Unmanaged.Free((T*)(void*)m_pointer, global::Unity.Collections.Allocator.Persistent);
		}

		public unsafe T* GetUnsafePointer()
		{
			return (T*)(void*)m_pointer;
		}
	}
}
