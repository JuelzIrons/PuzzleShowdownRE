namespace Unity.Collections
{
	[global::Unity.Burst.BurstCompile]
	internal struct NativeReferenceDisposeJob : global::Unity.Jobs.IJob
	{
		internal global::Unity.Collections.NativeReferenceDispose Data;

		public void Execute()
		{
			Data.Dispose();
		}
	}
}
