namespace UnityEngine.Rendering
{
	[global::Unity.Burst.BurstCompile(DisableSafetyChecks = true, OptimizeFor = global::Unity.Burst.OptimizeFor.Performance)]
	internal struct CompactVisibilityMasksJob : global::Unity.Jobs.IJobParallelForBatch
	{
		public const int k_BatchSize = 64;

		[global::Unity.Collections.ReadOnly]
		public global::Unity.Collections.NativeArray<byte> rendererVisibilityMasks;

		[global::Unity.Collections.LowLevel.Unsafe.NativeDisableContainerSafetyRestriction]
		[global::Unity.Burst.NoAlias]
		public global::UnityEngine.Rendering.ParallelBitArray compactedVisibilityMasks;

		public void Execute(int startIndex, int count)
		{
			ulong num = 0uL;
			for (int i = 0; i < count; i++)
			{
				if (rendererVisibilityMasks[startIndex + i] != 0)
				{
					num |= (ulong)(1L << i);
				}
			}
			int chunk_index = startIndex / 64;
			compactedVisibilityMasks.InterlockedOrChunk(chunk_index, num);
		}
	}
}
