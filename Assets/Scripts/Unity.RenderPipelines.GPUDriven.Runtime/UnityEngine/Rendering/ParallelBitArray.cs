namespace UnityEngine.Rendering
{
	internal struct ParallelBitArray
	{
		private global::Unity.Collections.Allocator m_Allocator;

		private global::Unity.Collections.NativeArray<long> m_Bits;

		private int m_Length;

		public int Length => m_Length;

		public bool IsCreated => m_Bits.IsCreated;

		public ParallelBitArray(int length, global::Unity.Collections.Allocator allocator, global::Unity.Collections.NativeArrayOptions options = global::Unity.Collections.NativeArrayOptions.ClearMemory)
		{
			m_Allocator = allocator;
			m_Bits = new global::Unity.Collections.NativeArray<long>((length + 63) / 64, allocator, options);
			m_Length = length;
		}

		public void Dispose()
		{
			m_Bits.Dispose();
			m_Length = 0;
		}

		public void Dispose(global::Unity.Jobs.JobHandle inputDeps)
		{
			m_Bits.Dispose(inputDeps);
			m_Length = 0;
		}

		public void Resize(int newLength)
		{
			int length = m_Length;
			if (newLength == length)
			{
				return;
			}
			int length2 = m_Bits.Length;
			int num = (newLength + 63) / 64;
			if (num != length2)
			{
				global::Unity.Collections.NativeArray<long> nativeArray = new global::Unity.Collections.NativeArray<long>(num, m_Allocator, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
				if (m_Bits.IsCreated)
				{
					global::Unity.Collections.NativeArray<long>.Copy(m_Bits, nativeArray, m_Bits.Length);
					m_Bits.Dispose();
				}
				m_Bits = nativeArray;
			}
			int num2 = global::System.Math.Min(length, newLength);
			for (int i = global::System.Math.Min(length2, num); i < m_Bits.Length; i++)
			{
				int num3 = global::System.Math.Max(num2 - 64 * i, 0);
				if (num3 < 64)
				{
					ulong num4 = (ulong)((1L << num3) - 1);
					m_Bits[i] &= (long)num4;
				}
			}
			m_Length = newLength;
		}

		public unsafe void Set(int index, bool value)
		{
			int num = index >> 6;
			long* unsafePtr = (long*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(m_Bits);
			ulong num2 = (ulong)(1L << (index & 0x3F));
			long num3 = (long)(~num2);
			long num4 = (long)(value ? num2 : 0);
			long num5;
			long value2;
			do
			{
				num5 = global::System.Threading.Interlocked.Read(ref unsafePtr[num]);
				value2 = (num5 & num3) | num4;
			}
			while (global::System.Threading.Interlocked.CompareExchange(ref unsafePtr[num], value2, num5) != num5);
		}

		public unsafe bool Get(int index)
		{
			int num = index >> 6;
			long* unsafeReadOnlyPtr = (long*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafeReadOnlyPtr(m_Bits);
			long num2 = 1L << (index & 0x3F);
			return (unsafeReadOnlyPtr[num] & num2) != 0;
		}

		public ulong GetChunk(int chunk_index)
		{
			return (ulong)m_Bits[chunk_index];
		}

		public void SetChunk(int chunk_index, ulong chunk_bits)
		{
			m_Bits[chunk_index] = (long)chunk_bits;
		}

		public unsafe ulong InterlockedReadChunk(int chunk_index)
		{
			long* unsafeReadOnlyPtr = (long*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafeReadOnlyPtr(m_Bits);
			return (ulong)global::System.Threading.Interlocked.Read(ref unsafeReadOnlyPtr[chunk_index]);
		}

		public unsafe void InterlockedOrChunk(int chunk_index, ulong chunk_bits)
		{
			long* unsafePtr = (long*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(m_Bits);
			long num;
			long value;
			do
			{
				num = global::System.Threading.Interlocked.Read(ref unsafePtr[chunk_index]);
				value = num | (long)chunk_bits;
			}
			while (global::System.Threading.Interlocked.CompareExchange(ref unsafePtr[chunk_index], value, num) != num);
		}

		public int ChunkCount()
		{
			return m_Bits.Length;
		}

		public global::UnityEngine.Rendering.ParallelBitArray GetSubArray(int length)
		{
			return new global::UnityEngine.Rendering.ParallelBitArray
			{
				m_Bits = m_Bits.GetSubArray(0, (length + 63) / 64),
				m_Length = length
			};
		}

		public global::Unity.Collections.NativeArray<long> GetBitsArray()
		{
			return m_Bits;
		}

		public void FillZeroes(int length)
		{
			length = global::System.Math.Min(length, m_Length);
			int num = length / 64;
			int num2 = length & 0x3F;
			global::UnityEngine.Rendering.ArrayExtensions.FillArray(ref m_Bits, 0L, 0, num);
			if (num2 > 0)
			{
				long num3 = (1L << num2) - 1;
				m_Bits[num] &= ~num3;
			}
		}
	}
}
