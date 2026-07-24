namespace Unity.Collections
{
	[global::Unity.Collections.GenerateTestsForBurstCompatibility]
	internal struct UnsafeQueueDispose
	{
		[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
		internal unsafe global::Unity.Collections.UnsafeQueueData* m_Buffer;

		internal global::Unity.Collections.AllocatorManager.AllocatorHandle m_AllocatorLabel;

		public unsafe void Dispose()
		{
			global::Unity.Collections.UnsafeQueueData.DeallocateQueue(m_Buffer, m_AllocatorLabel);
		}
	}
}
