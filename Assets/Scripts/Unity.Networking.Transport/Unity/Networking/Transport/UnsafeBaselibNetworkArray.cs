namespace Unity.Networking.Transport
{
	internal struct UnsafeBaselibNetworkArray : global::System.IDisposable
	{
		[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
		private global::Unity.Collections.LowLevel.Unsafe.UnsafePtrList<global::Unity.Baselib.LowLevel.Binding.Baselib_RegisteredNetwork_Buffer> m_BufferPool;

		private uint m_ElementSize;

		public uint ElementSize => m_ElementSize;

		public unsafe UnsafeBaselibNetworkArray(int capacity, int typeSize)
		{
			m_ElementSize = (uint)typeSize;
			long num = typeSize;
			m_BufferPool = new global::Unity.Collections.LowLevel.Unsafe.UnsafePtrList<global::Unity.Baselib.LowLevel.Binding.Baselib_RegisteredNetwork_Buffer>(capacity, global::Unity.Collections.Allocator.Persistent);
			global::Unity.Baselib.LowLevel.Binding.Baselib_Memory_PageSizeInfo* ptr = stackalloc global::Unity.Baselib.LowLevel.Binding.Baselib_Memory_PageSizeInfo[1];
			global::Unity.Baselib.LowLevel.Binding.Baselib_Memory_GetPageSizeInfo(ptr);
			ulong defaultPageSize = ptr->defaultPageSize;
			for (int i = 0; i < capacity; i++)
			{
				ulong pageCount = 1uL;
				if ((ulong)num > defaultPageSize)
				{
					pageCount = (ulong)global::Unity.Mathematics.math.ceil((double)num / (double)defaultPageSize);
				}
				global::Unity.Baselib.LowLevel.Binding.Baselib_RegisteredNetwork_Buffer* ptr2 = (global::Unity.Baselib.LowLevel.Binding.Baselib_RegisteredNetwork_Buffer*)global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.Malloc(global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<global::Unity.Baselib.LowLevel.Binding.Baselib_RegisteredNetwork_Buffer>(), global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AlignOf<global::Unity.Baselib.LowLevel.Binding.Baselib_RegisteredNetwork_Buffer>(), global::Unity.Collections.Allocator.Persistent);
				global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorState baselib_ErrorState = default(global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorState);
				global::Unity.Baselib.LowLevel.Binding.Baselib_Memory_PageAllocation pageAllocation = global::Unity.Baselib.LowLevel.Binding.Baselib_Memory_AllocatePages(ptr->defaultPageSize, pageCount, 1uL, global::Unity.Baselib.LowLevel.Binding.Baselib_Memory_PageState.ReadWrite, &baselib_ErrorState);
				if (baselib_ErrorState.code != global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorCode.Success)
				{
					break;
				}
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemSet((void*)pageAllocation.ptr, 0, (long)(pageAllocation.pageCount * pageAllocation.pageSize));
				*ptr2 = global::Unity.Baselib.LowLevel.Binding.Baselib_RegisteredNetwork_Buffer_Register(pageAllocation, &baselib_ErrorState);
				if (baselib_ErrorState.code != global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorCode.Success)
				{
					global::Unity.Baselib.LowLevel.Binding.Baselib_Memory_ReleasePages(pageAllocation, &baselib_ErrorState);
					*ptr2 = default(global::Unity.Baselib.LowLevel.Binding.Baselib_RegisteredNetwork_Buffer);
				}
				m_BufferPool.Add(ptr2);
			}
		}

		public unsafe void Dispose()
		{
			global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorState baselib_ErrorState = default(global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorState);
			for (int i = 0; i < m_BufferPool.Length; i++)
			{
				global::Unity.Baselib.LowLevel.Binding.Baselib_RegisteredNetwork_Buffer* intPtr = m_BufferPool[i];
				global::Unity.Baselib.LowLevel.Binding.Baselib_Memory_PageAllocation allocation = intPtr->allocation;
				global::Unity.Baselib.LowLevel.Binding.Baselib_RegisteredNetwork_Buffer_Deregister(*intPtr);
				global::Unity.Baselib.LowLevel.Binding.Baselib_Memory_ReleasePages(allocation, &baselib_ErrorState);
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.Free(intPtr, global::Unity.Collections.Allocator.Persistent);
			}
			m_BufferPool.Dispose();
		}

		public unsafe global::Unity.Baselib.LowLevel.Binding.Baselib_RegisteredNetwork_BufferSlice AtIndexAsSlice(int index)
		{
			global::Unity.Baselib.LowLevel.Binding.Baselib_RegisteredNetwork_Buffer* ptr = null;
			ptr = m_BufferPool[index];
			global::System.IntPtr data = (global::System.IntPtr)(void*)ptr->allocation.ptr;
			global::Unity.Baselib.LowLevel.Binding.Baselib_RegisteredNetwork_BufferSlice result = default(global::Unity.Baselib.LowLevel.Binding.Baselib_RegisteredNetwork_BufferSlice);
			result.id = ptr->id;
			result.data = data;
			result.offset = 0u;
			result.size = m_ElementSize;
			return result;
		}

		public unsafe global::System.IntPtr GetBufferPtr(int index)
		{
			return m_BufferPool[index]->allocation.ptr;
		}
	}
}
