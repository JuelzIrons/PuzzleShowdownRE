namespace Unity.Networking.Transport.Utilities.LowLevel.Unsafe
{
	internal struct UnsafeAtomicFreeList : global::System.IDisposable
	{
		[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
		private unsafe int* m_Buffer;

		private int m_BufferSize;

		private int m_Length;

		private global::Unity.Collections.Allocator m_Allocator;

		public int Capacity => m_Length;

		public unsafe int InUse => *m_Buffer - m_Buffer[1];

		public unsafe bool IsCreated => m_Buffer != null;

		public unsafe UnsafeAtomicFreeList(int capacity, global::Unity.Collections.Allocator allocator)
		{
			m_Allocator = allocator;
			m_Length = capacity;
			m_BufferSize = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<int>() * (capacity + 2);
			m_Buffer = (int*)global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.Malloc(m_BufferSize, global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AlignOf<int>(), allocator);
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemClear(m_Buffer, m_BufferSize);
		}

		public unsafe void Dispose()
		{
			if (IsCreated)
			{
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.Free(m_Buffer, m_Allocator);
			}
		}

		public unsafe void Reset()
		{
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemClear(m_Buffer, m_BufferSize);
		}

		public unsafe void Push(int item)
		{
			int* buffer = m_Buffer;
			int num = global::System.Threading.Interlocked.Increment(ref buffer[1]) - 1;
			while (global::System.Threading.Interlocked.CompareExchange(ref buffer[num + 2], item + 1, 0) != 0)
			{
			}
		}

		public unsafe int Pop()
		{
			int* buffer = m_Buffer;
			int num = buffer[1] - 1;
			while (num >= 0 && global::System.Threading.Interlocked.CompareExchange(ref buffer[1], num, num + 1) != num + 1)
			{
				num = buffer[1] - 1;
			}
			if (num >= 0)
			{
				int num2;
				for (num2 = 0; num2 == 0; num2 = global::System.Threading.Interlocked.Exchange(ref buffer[2 + num], 0))
				{
				}
				return num2 - 1;
			}
			num = global::System.Threading.Interlocked.Increment(ref *buffer) - 1;
			if (num >= Capacity)
			{
				global::System.Threading.Interlocked.Decrement(ref *buffer);
				return -1;
			}
			return num;
		}
	}
}
