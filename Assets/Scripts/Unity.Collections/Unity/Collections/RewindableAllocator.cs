namespace Unity.Collections
{
	[global::Unity.Burst.BurstCompile]
	public struct RewindableAllocator : global::Unity.Collections.AllocatorManager.IAllocator, global::System.IDisposable
	{
		internal struct Union
		{
			internal long m_long;

			private const int currentBits = 40;

			private const int currentOffset = 0;

			private const long currentMask = 1099511627775L;

			private const int allocCountBits = 24;

			private const int allocCountOffset = 40;

			private const long allocCountMask = 16777215L;

			internal long m_current
			{
				[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
				get
				{
					return m_long & 0xFFFFFFFFFFL;
				}
				[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
				set
				{
					m_long &= -1099511627776L;
					m_long |= value & 0xFFFFFFFFFFL;
				}
			}

			internal long m_allocCount
			{
				[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
				get
				{
					return (m_long >> 40) & 0xFFFFFF;
				}
				[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
				set
				{
					m_long &= 1099511627775L;
					m_long |= (value & 0xFFFFFF) << 40;
				}
			}
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility]
		internal struct MemoryBlock : global::System.IDisposable
		{
			public const int kMaximumAlignment = 16384;

			public unsafe byte* m_pointer;

			public long m_bytes;

			public global::Unity.Collections.RewindableAllocator.Union m_union;

			public unsafe MemoryBlock(long bytes)
			{
				m_pointer = (byte*)global::Unity.Collections.Memory.Unmanaged.Allocate(bytes, 16384, global::Unity.Collections.Allocator.Persistent);
				m_bytes = bytes;
				m_union = default(global::Unity.Collections.RewindableAllocator.Union);
			}

			public void Rewind()
			{
				m_union = default(global::Unity.Collections.RewindableAllocator.Union);
			}

			public unsafe void Dispose()
			{
				global::Unity.Collections.Memory.Unmanaged.Free(m_pointer, global::Unity.Collections.Allocator.Persistent);
				m_pointer = null;
				m_bytes = 0L;
				m_union = default(global::Unity.Collections.RewindableAllocator.Union);
			}

			public unsafe bool Contains(global::System.IntPtr ptr)
			{
				void* ptr2 = (void*)ptr;
				if (ptr2 >= m_pointer)
				{
					return ptr2 < m_pointer + m_union.m_current;
				}
				return false;
			}
		}

		[global::System.Runtime.InteropServices.UnmanagedFunctionPointer(global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		internal delegate int Try_000009E0_0024PostfixBurstDelegate(global::System.IntPtr state, ref global::Unity.Collections.AllocatorManager.Block block);

		internal static class Try_000009E0_0024BurstDirectCall
		{
			private static global::System.IntPtr Pointer;

			[global::Unity.Burst.BurstDiscard]
			private static void GetFunctionPointerDiscard(ref global::System.IntPtr P_0)
			{
				if (Pointer == (global::System.IntPtr)0)
				{
					Pointer = global::Unity.Burst.BurstCompiler.CompileFunctionPointer<global::Unity.Collections.RewindableAllocator.Try_000009E0_0024PostfixBurstDelegate>(Try).Value;
				}
				P_0 = Pointer;
			}

			private static global::System.IntPtr GetFunctionPointer()
			{
				nint result = 0;
				GetFunctionPointerDiscard(ref result);
				return result;
			}

			public unsafe static int Invoke(global::System.IntPtr state, ref global::Unity.Collections.AllocatorManager.Block block)
			{
				if (global::Unity.Burst.BurstCompiler.IsEnabled)
				{
					global::System.IntPtr functionPointer = GetFunctionPointer();
					if (functionPointer != (global::System.IntPtr)0)
					{
						return ((delegate* unmanaged[Cdecl]<global::System.IntPtr, ref global::Unity.Collections.AllocatorManager.Block, int>)functionPointer)(state, ref block);
					}
				}
				return Try_0024BurstManaged(state, ref block);
			}
		}

		private const int kLog2MaxMemoryBlockSize = 26;

		private const long kMaxMemoryBlockSize = 67108864L;

		private const long kMinMemoryBlockSize = 131072L;

		private const int kMaxNumBlocks = 64;

		private const int kBlockBusyRewindMask = int.MinValue;

		private const int kBlockBusyAllocateMask = int.MaxValue;

		private global::Unity.Collections.Spinner m_spinner;

		private global::Unity.Collections.AllocatorManager.AllocatorHandle m_handle;

		private global::Unity.Collections.UnmanagedArray<global::Unity.Collections.RewindableAllocator.MemoryBlock> m_block;

		private int m_last;

		private int m_used;

		private byte m_enableBlockFree;

		private byte m_reachMaxBlockSize;

		public bool EnableBlockFree
		{
			get
			{
				return m_enableBlockFree != 0;
			}
			set
			{
				m_enableBlockFree = (byte)(value ? 1 : 0);
			}
		}

		public int BlocksAllocated => m_last + 1;

		public int InitialSizeInBytes => (int)m_block[0].m_bytes;

		internal long MaxMemoryBlockSize => 67108864L;

		internal long BytesAllocated
		{
			get
			{
				long num = 0L;
				for (int i = 0; i <= m_last; i++)
				{
					num += m_block[i].m_bytes;
				}
				return num;
			}
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Uses managed delegate")]
		public global::Unity.Collections.AllocatorManager.TryFunction Function => Try;

		public global::Unity.Collections.AllocatorManager.AllocatorHandle Handle
		{
			get
			{
				return m_handle;
			}
			set
			{
				m_handle = value;
			}
		}

		public global::Unity.Collections.Allocator ToAllocator => m_handle.ToAllocator;

		public bool IsCustomAllocator => m_handle.IsCustomAllocator;

		public bool IsAutoDispose => true;

		public void Initialize(int initialSizeInBytes, bool enableBlockFree = false)
		{
			m_spinner = default(global::Unity.Collections.Spinner);
			m_block = new global::Unity.Collections.UnmanagedArray<global::Unity.Collections.RewindableAllocator.MemoryBlock>(64, global::Unity.Collections.Allocator.Persistent);
			long bytes = (((long)initialSizeInBytes > 131072L) ? initialSizeInBytes : 131072);
			m_block[0] = new global::Unity.Collections.RewindableAllocator.MemoryBlock(bytes);
			m_last = (m_used = 0);
			m_enableBlockFree = (byte)(enableBlockFree ? 1 : 0);
			m_reachMaxBlockSize = (byte)(((long)initialSizeInBytes >= 67108864L) ? 1 : 0);
		}

		public void Rewind()
		{
			if (global::Unity.Jobs.LowLevel.Unsafe.JobsUtility.IsExecutingJob)
			{
				throw new global::System.InvalidOperationException("You cannot Rewind a RewindableAllocator from a Job.");
			}
			m_handle.Rewind();
			while (m_last > m_used)
			{
				m_block[m_last--].Dispose();
			}
			while (m_used > 0)
			{
				m_block[m_used--].Rewind();
			}
			m_block[0].Rewind();
		}

		public void Dispose()
		{
			if (global::Unity.Jobs.LowLevel.Unsafe.JobsUtility.IsExecutingJob)
			{
				throw new global::System.InvalidOperationException("You cannot Dispose a RewindableAllocator from a Job.");
			}
			m_used = 0;
			Rewind();
			m_block[0].Dispose();
			m_block.Dispose();
			m_last = (m_used = 0);
		}

		private unsafe int TryAllocate(ref global::Unity.Collections.AllocatorManager.Block block, int startIndex, int lastIndex, long alignedSize, long alignmentMask)
		{
			for (int i = startIndex; i <= lastIndex; i++)
			{
				global::Unity.Collections.RewindableAllocator.Union union = default(global::Unity.Collections.RewindableAllocator.Union);
				long num = 0L;
				bool flag = false;
				union.m_long = global::System.Threading.Interlocked.Read(ref m_block[i].m_union.m_long);
				global::Unity.Collections.RewindableAllocator.Union union2;
				do
				{
					num = (union.m_current + alignmentMask) & ~alignmentMask;
					if (num + block.Bytes > m_block[i].m_bytes)
					{
						flag = true;
						break;
					}
					union2 = union;
					global::Unity.Collections.RewindableAllocator.Union union3 = new global::Unity.Collections.RewindableAllocator.Union
					{
						m_current = ((num + alignedSize > m_block[i].m_bytes) ? m_block[i].m_bytes : (num + alignedSize)),
						m_allocCount = union.m_allocCount + 1
					};
					union.m_long = global::System.Threading.Interlocked.CompareExchange(ref m_block[i].m_union.m_long, union3.m_long, union2.m_long);
				}
				while (union.m_long != union2.m_long);
				if (!flag)
				{
					block.Range.Pointer = (global::System.IntPtr)(m_block[i].m_pointer + num);
					block.AllocatedItems = block.Range.Items;
					global::System.Threading.Interlocked.MemoryBarrier();
					int num2 = m_used;
					int num3;
					int num4;
					do
					{
						num3 = num2;
						num4 = ((i > num3) ? i : num3);
						num2 = global::System.Threading.Interlocked.CompareExchange(ref m_used, num4, num3);
					}
					while (num4 != num3);
					return 0;
				}
			}
			return -1;
		}

		public int Try(ref global::Unity.Collections.AllocatorManager.Block block)
		{
			if (block.Range.Pointer == global::System.IntPtr.Zero)
			{
				int num = global::Unity.Mathematics.math.max(64, block.Alignment);
				int num2 = ((num != 64) ? 1 : 0);
				int num3 = 63;
				if (num2 == 1)
				{
					num = (num + num3) & ~num3;
				}
				long num4 = (long)num - 1L;
				long num5 = (block.Bytes + num2 * num + num4) & ~num4;
				int last = m_last;
				int num6 = TryAllocate(ref block, 0, m_last, num5, num4);
				if (num6 == 0)
				{
					return num6;
				}
				m_spinner.Acquire();
				num6 = TryAllocate(ref block, last, m_last, num5, num4);
				if (num6 == 0)
				{
					m_spinner.Release();
					return num6;
				}
				long x = ((m_reachMaxBlockSize != 0) ? (m_block[m_last].m_bytes + 67108864) : (m_block[m_last].m_bytes << 1));
				x = global::Unity.Mathematics.math.max(x, num5);
				m_reachMaxBlockSize = (byte)((x >= 67108864) ? 1 : 0);
				m_block[m_last + 1] = new global::Unity.Collections.RewindableAllocator.MemoryBlock(x);
				global::System.Threading.Interlocked.Increment(ref m_last);
				num6 = TryAllocate(ref block, m_last, m_last, num5, num4);
				m_spinner.Release();
				return num6;
			}
			if (block.Range.Items == 0)
			{
				if (m_enableBlockFree != 0)
				{
					for (int i = 0; i <= m_last; i++)
					{
						if (!m_block[i].Contains(block.Range.Pointer))
						{
							continue;
						}
						global::Unity.Collections.RewindableAllocator.Union union = new global::Unity.Collections.RewindableAllocator.Union
						{
							m_long = global::System.Threading.Interlocked.Read(ref m_block[i].m_union.m_long)
						};
						global::Unity.Collections.RewindableAllocator.Union union2;
						do
						{
							union2 = union;
							global::Unity.Collections.RewindableAllocator.Union union3 = union;
							union3.m_allocCount--;
							if (union3.m_allocCount == 0L)
							{
								union3.m_current = 0L;
							}
							union.m_long = global::System.Threading.Interlocked.CompareExchange(ref m_block[i].m_union.m_long, union3.m_long, union2.m_long);
						}
						while (union.m_long != union2.m_long);
					}
				}
				return 0;
			}
			return -1;
		}

		[global::Unity.Burst.BurstCompile]
		[global::AOT.MonoPInvokeCallback(typeof(global::Unity.Collections.AllocatorManager.TryFunction))]
		internal static int Try(global::System.IntPtr state, ref global::Unity.Collections.AllocatorManager.Block block)
		{
			return global::Unity.Collections.RewindableAllocator.Try_000009E0_0024BurstDirectCall.Invoke(state, ref block);
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
		public unsafe global::Unity.Collections.NativeArray<T> AllocateNativeArray<T>(int length) where T : unmanaged
		{
			return new global::Unity.Collections.NativeArray<T>
			{
				m_Buffer = global::Unity.Collections.AllocatorManager.AllocateStruct(ref this, default(T), length),
				m_Length = length,
				m_AllocatorLabel = global::Unity.Collections.Allocator.None
			};
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
		public unsafe global::Unity.Collections.NativeList<T> AllocateNativeList<T>(int capacity) where T : unmanaged
		{
			global::Unity.Collections.NativeList<T> result = default(global::Unity.Collections.NativeList<T>);
			result.m_ListData = global::Unity.Collections.AllocatorManager.Allocate(ref this, default(global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T>), 1);
			result.m_ListData->Ptr = global::Unity.Collections.AllocatorManager.Allocate(ref this, default(T), capacity);
			result.m_ListData->m_length = 0;
			result.m_ListData->m_capacity = capacity;
			result.m_ListData->Allocator = global::Unity.Collections.Allocator.None;
			return result;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::Unity.Burst.BurstCompile]
		[global::AOT.MonoPInvokeCallback(typeof(global::Unity.Collections.AllocatorManager.TryFunction))]
		internal unsafe static int Try_0024BurstManaged(global::System.IntPtr state, ref global::Unity.Collections.AllocatorManager.Block block)
		{
			return ((global::Unity.Collections.RewindableAllocator*)(void*)state)->Try(ref block);
		}
	}
}
