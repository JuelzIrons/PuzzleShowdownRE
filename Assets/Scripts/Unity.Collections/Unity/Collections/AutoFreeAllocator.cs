namespace Unity.Collections
{
	[global::Unity.Burst.BurstCompile]
	internal struct AutoFreeAllocator : global::Unity.Collections.AllocatorManager.IAllocator, global::System.IDisposable
	{
		[global::System.Runtime.InteropServices.UnmanagedFunctionPointer(global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		internal delegate int Try_000000E3_0024PostfixBurstDelegate(global::System.IntPtr state, ref global::Unity.Collections.AllocatorManager.Block block);

		internal static class Try_000000E3_0024BurstDirectCall
		{
			private static global::System.IntPtr Pointer;

			[global::Unity.Burst.BurstDiscard]
			private static void GetFunctionPointerDiscard(ref global::System.IntPtr P_0)
			{
				if (Pointer == (global::System.IntPtr)0)
				{
					Pointer = global::Unity.Burst.BurstCompiler.CompileFunctionPointer<global::Unity.Collections.AutoFreeAllocator.Try_000000E3_0024PostfixBurstDelegate>(Try).Value;
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

		private global::Unity.Collections.ArrayOfArrays<global::System.IntPtr> m_allocated;

		private global::Unity.Collections.ArrayOfArrays<global::System.IntPtr> m_tofree;

		private global::Unity.Collections.AllocatorManager.AllocatorHandle m_handle;

		private global::Unity.Collections.AllocatorManager.AllocatorHandle m_backingAllocatorHandle;

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

		public unsafe void Update()
		{
			int length = m_tofree.Length;
			while (length-- > 0)
			{
				int length2 = m_allocated.Length;
				while (length2-- > 0)
				{
					if (m_allocated[length2] == m_tofree[length])
					{
						global::Unity.Collections.Memory.Unmanaged.Free((void*)m_tofree[length], m_backingAllocatorHandle);
						m_allocated.RemoveAtSwapBack(length2);
						break;
					}
				}
			}
			m_tofree.Rewind();
			m_allocated.TrimExcess();
		}

		public void Initialize(global::Unity.Collections.AllocatorManager.AllocatorHandle backingAllocatorHandle)
		{
			m_allocated = new global::Unity.Collections.ArrayOfArrays<global::System.IntPtr>(1048576, backingAllocatorHandle);
			m_tofree = new global::Unity.Collections.ArrayOfArrays<global::System.IntPtr>(131072, backingAllocatorHandle);
			m_backingAllocatorHandle = backingAllocatorHandle;
		}

		public unsafe void FreeAll()
		{
			Update();
			m_handle.Rewind();
			for (int i = 0; i < m_allocated.Length; i++)
			{
				global::Unity.Collections.Memory.Unmanaged.Free((void*)m_allocated[i], m_backingAllocatorHandle);
			}
			m_allocated.Rewind();
		}

		public void Dispose()
		{
			FreeAll();
			m_tofree.Dispose();
			m_allocated.Dispose();
		}

		public unsafe int Try(ref global::Unity.Collections.AllocatorManager.Block block)
		{
			if (block.Range.Pointer == global::System.IntPtr.Zero)
			{
				if (block.Bytes == 0L)
				{
					return 0;
				}
				byte* ptr = (byte*)global::Unity.Collections.Memory.Unmanaged.Allocate(block.Bytes, block.Alignment, m_backingAllocatorHandle);
				block.Range.Pointer = (global::System.IntPtr)ptr;
				block.AllocatedItems = block.Range.Items;
				m_allocated.LockfreeAdd(block.Range.Pointer);
				return 0;
			}
			if (block.Range.Items == 0)
			{
				m_tofree.LockfreeAdd(block.Range.Pointer);
				block.Range.Pointer = global::System.IntPtr.Zero;
				block.AllocatedItems = 0;
				return 0;
			}
			return -1;
		}

		[global::Unity.Burst.BurstCompile]
		[global::AOT.MonoPInvokeCallback(typeof(global::Unity.Collections.AllocatorManager.TryFunction))]
		internal static int Try(global::System.IntPtr state, ref global::Unity.Collections.AllocatorManager.Block block)
		{
			return global::Unity.Collections.AutoFreeAllocator.Try_000000E3_0024BurstDirectCall.Invoke(state, ref block);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::Unity.Burst.BurstCompile]
		[global::AOT.MonoPInvokeCallback(typeof(global::Unity.Collections.AllocatorManager.TryFunction))]
		internal unsafe static int Try_0024BurstManaged(global::System.IntPtr state, ref global::Unity.Collections.AllocatorManager.Block block)
		{
			return ((global::Unity.Collections.AutoFreeAllocator*)(void*)state)->Try(ref block);
		}
	}
}
