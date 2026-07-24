namespace Unity.Collections
{
	[global::Unity.Burst.BurstCompile]
	internal struct NativeTextDisposeJob : global::Unity.Jobs.IJob
	{
		public global::Unity.Collections.NativeTextDispose Data;

		public void Execute()
		{
			Data.Dispose();
		}
	}
}
