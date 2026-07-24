namespace Unity.Collections
{
	[global::Unity.Burst.BurstCompile]
	internal struct NativeStreamDisposeJob : global::Unity.Jobs.IJob
	{
		public global::Unity.Collections.NativeStreamDispose Data;

		public void Execute()
		{
			Data.Dispose();
		}
	}
}
