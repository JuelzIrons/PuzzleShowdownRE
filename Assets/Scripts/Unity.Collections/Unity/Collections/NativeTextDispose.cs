namespace Unity.Collections
{
	[global::Unity.Collections.LowLevel.Unsafe.NativeContainer]
	[global::Unity.Collections.GenerateTestsForBurstCompatibility]
	internal struct NativeTextDispose
	{
		[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
		public unsafe global::Unity.Collections.LowLevel.Unsafe.UnsafeText* m_TextData;

		public unsafe void Dispose()
		{
			global::Unity.Collections.LowLevel.Unsafe.UnsafeText.Free(m_TextData);
		}
	}
}
