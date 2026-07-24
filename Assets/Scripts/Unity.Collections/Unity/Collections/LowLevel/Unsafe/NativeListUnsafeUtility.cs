namespace Unity.Collections.LowLevel.Unsafe
{
	[global::Unity.Collections.GenerateTestsForBurstCompatibility]
	public static class NativeListUnsafeUtility
	{
		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
		public unsafe static T* GetUnsafePtr<T>(this global::Unity.Collections.NativeList<T> list) where T : unmanaged
		{
			return list.m_ListData->Ptr;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
		public unsafe static T* GetUnsafeReadOnlyPtr<T>(this global::Unity.Collections.NativeList<T> list) where T : unmanaged
		{
			return list.m_ListData->Ptr;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
		public unsafe static void* GetInternalListDataPtrUnchecked<T>(ref global::Unity.Collections.NativeList<T> list) where T : unmanaged
		{
			return list.m_ListData;
		}
	}
}
