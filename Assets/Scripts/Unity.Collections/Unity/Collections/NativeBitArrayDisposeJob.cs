namespace Unity.Collections
{
	[global::Unity.Burst.BurstCompile]
	internal struct NativeBitArrayDisposeJob : global::Unity.Jobs.IJob
	{
		public global::Unity.Collections.NativeBitArrayDispose Data;

		public void Execute()
		{
			Data.Dispose();
		}
	}
}
