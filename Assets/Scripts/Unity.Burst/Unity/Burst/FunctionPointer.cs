namespace Unity.Burst
{
	public readonly struct FunctionPointer<T> : global::Unity.Burst.IFunctionPointer
	{
		[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
		private readonly global::System.IntPtr _ptr;

		public global::System.IntPtr Value => _ptr;

		public T Invoke => global::System.Runtime.InteropServices.Marshal.GetDelegateForFunctionPointer<T>(_ptr);

		public bool IsCreated => _ptr != global::System.IntPtr.Zero;

		public FunctionPointer(global::System.IntPtr ptr)
		{
			_ptr = ptr;
		}

		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckIsCreated()
		{
			if (!IsCreated)
			{
				throw new global::System.NullReferenceException("Object reference not set to an instance of an object");
			}
		}

		global::Unity.Burst.IFunctionPointer global::Unity.Burst.IFunctionPointer.FromIntPtr(global::System.IntPtr ptr)
		{
			return new global::Unity.Burst.FunctionPointer<T>(ptr);
		}
	}
}
