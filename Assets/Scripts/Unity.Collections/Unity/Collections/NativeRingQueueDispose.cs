namespace Unity.Collections
{
	[global::Unity.Collections.LowLevel.Unsafe.NativeContainer]
	[global::Unity.Collections.GenerateTestsForBurstCompatibility]
	internal struct NativeRingQueueDispose
	{
		[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
		public unsafe global::Unity.Collections.LowLevel.Unsafe.UnsafeRingQueue<int>* m_QueueData;

		public unsafe void Dispose()
		{
			global::Unity.Collections.LowLevel.Unsafe.UnsafeRingQueue<int>.Free(m_QueueData);
		}
	}
}
