namespace Unity.Collections
{
	[global::Unity.Burst.BurstCompile]
	[global::Unity.Collections.GenerateTestsForBurstCompatibility]
	internal struct NativeListDisposeJob : global::Unity.Jobs.IJob
	{
		internal global::Unity.Collections.NativeListDispose Data;

		public void Execute()
		{
			Data.Dispose();
		}
	}
}
