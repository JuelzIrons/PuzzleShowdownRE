namespace Unity.Networking.Transport
{
	internal struct ManagedCallWrapper
	{
		[global::System.Runtime.InteropServices.UnmanagedFunctionPointer(global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		private unsafe delegate void MethodDelegate(void* functionPtr, void* arguments, int argumentsSize);

		private static global::System.IntPtr s_CachedWrapperPtr;

		[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
		private global::System.IntPtr m_ManagedFunctionPtr;

		[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
		private global::System.IntPtr m_WrapperPtr;

		public bool IsCreated => m_ManagedFunctionPtr != (global::System.IntPtr)0;

		[global::AOT.MonoPInvokeCallback(typeof(global::Unity.Networking.Transport.ManagedCallWrapper.MethodDelegate))]
		private unsafe static void Method(void* functionPtr, void* arguments, int argumentsSize)
		{
			((delegate*<void*, int, void>)functionPtr)(arguments, argumentsSize);
		}

		private unsafe static void Initialize()
		{
			if (!(s_CachedWrapperPtr != (global::System.IntPtr)0))
			{
				global::Unity.Networking.Transport.ManagedCallWrapper.MethodDelegate methodDelegate = Method;
				global::System.Runtime.InteropServices.GCHandle.Alloc(methodDelegate);
				s_CachedWrapperPtr = global::System.Runtime.InteropServices.Marshal.GetFunctionPointerForDelegate(methodDelegate);
			}
		}

		public unsafe ManagedCallWrapper(delegate*<void*, int, void> managedFunctionPtr)
		{
			Initialize();
			m_WrapperPtr = s_CachedWrapperPtr;
			m_ManagedFunctionPtr = new global::System.IntPtr(managedFunctionPtr);
		}

		public unsafe void Invoke(void* arguments, int argumentsSize)
		{
			if (m_ManagedFunctionPtr == (global::System.IntPtr)0)
			{
				throw new global::System.NullReferenceException("Trying to invoke a null function pointer");
			}
			((delegate* unmanaged[Cdecl]<void*, void*, int, void>)(void*)m_WrapperPtr)((void*)m_ManagedFunctionPtr, arguments, argumentsSize);
		}

		public unsafe void Invoke<T>(ref T arguments) where T : unmanaged
		{
			fixed (T* ptr = &arguments)
			{
				void* arguments2 = ptr;
				Invoke(arguments2, global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<T>());
			}
		}

		public unsafe static ref A ArgumentsFromPtr<A>(void* argumentsPtr, int size) where A : unmanaged
		{
			return ref *(A*)argumentsPtr;
		}
	}
}
