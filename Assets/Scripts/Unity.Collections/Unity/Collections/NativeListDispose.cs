namespace Unity.Collections
{
	[global::Unity.Collections.LowLevel.Unsafe.NativeContainer]
	[global::Unity.Collections.GenerateTestsForBurstCompatibility]
	internal struct NativeListDispose
	{
		[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
		public unsafe global::Unity.Collections.LowLevel.Unsafe.UntypedUnsafeList* m_ListData;

		public unsafe void Dispose()
		{
			global::Unity.Collections.LowLevel.Unsafe.UnsafeList<int>* listData = (global::Unity.Collections.LowLevel.Unsafe.UnsafeList<int>*)m_ListData;
			global::Unity.Collections.LowLevel.Unsafe.UnsafeList<int>.Destroy(listData);
		}
	}
}
