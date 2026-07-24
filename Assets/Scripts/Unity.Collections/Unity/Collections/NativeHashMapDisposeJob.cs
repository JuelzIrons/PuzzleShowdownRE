namespace Unity.Collections
{
	[global::Unity.Burst.BurstCompile]
	internal struct NativeHashMapDisposeJob : global::Unity.Jobs.IJob
	{
		internal global::Unity.Collections.NativeHashMapDispose Data;

		public void Execute()
		{
			Data.Dispose();
		}
	}
}
