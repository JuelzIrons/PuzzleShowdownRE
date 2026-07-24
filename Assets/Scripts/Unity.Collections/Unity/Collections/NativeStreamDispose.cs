namespace Unity.Collections
{
	[global::Unity.Collections.LowLevel.Unsafe.NativeContainer]
	[global::Unity.Collections.GenerateTestsForBurstCompatibility]
	internal struct NativeStreamDispose
	{
		public global::Unity.Collections.LowLevel.Unsafe.UnsafeStream m_StreamData;

		public void Dispose()
		{
			m_StreamData.Dispose();
		}
	}
}
