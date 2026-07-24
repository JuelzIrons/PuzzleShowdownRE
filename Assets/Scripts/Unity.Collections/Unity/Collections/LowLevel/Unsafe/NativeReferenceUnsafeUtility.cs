namespace Unity.Collections.LowLevel.Unsafe
{
	[global::Unity.Collections.GenerateTestsForBurstCompatibility]
	public static class NativeReferenceUnsafeUtility
	{
		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
		public unsafe static T* GetUnsafePtr<T>(this global::Unity.Collections.NativeReference<T> reference) where T : unmanaged
		{
			return (T*)reference.m_Data;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
		public unsafe static T* GetUnsafeReadOnlyPtr<T>(this global::Unity.Collections.NativeReference<T> reference) where T : unmanaged
		{
			return (T*)reference.m_Data;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
		public unsafe static T* GetUnsafePtrWithoutChecks<T>(this global::Unity.Collections.NativeReference<T> reference) where T : unmanaged
		{
			return (T*)reference.m_Data;
		}
	}
}
