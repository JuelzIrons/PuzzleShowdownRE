namespace Unity.Collections
{
	[global::Unity.Collections.LowLevel.Unsafe.NativeContainer]
	[global::Unity.Collections.GenerateTestsForBurstCompatibility]
	internal struct NativeQueueDispose
	{
		[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
		public unsafe global::Unity.Collections.UnsafeQueue<int>* m_QueueData;

		public unsafe void Dispose()
		{
			global::Unity.Collections.UnsafeQueue<int>.Free(m_QueueData);
		}
	}
}
