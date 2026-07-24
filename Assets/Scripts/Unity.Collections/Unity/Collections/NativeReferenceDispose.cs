namespace Unity.Collections
{
	[global::Unity.Collections.LowLevel.Unsafe.NativeContainer]
	internal struct NativeReferenceDispose
	{
		[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
		internal unsafe void* m_Data;

		internal global::Unity.Collections.AllocatorManager.AllocatorHandle m_AllocatorLabel;

		public unsafe void Dispose()
		{
			global::Unity.Collections.Memory.Unmanaged.Free(m_Data, m_AllocatorLabel);
		}
	}
}
