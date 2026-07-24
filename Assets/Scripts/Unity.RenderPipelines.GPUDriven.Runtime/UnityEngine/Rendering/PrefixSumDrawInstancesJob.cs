namespace UnityEngine.Rendering
{
	[global::Unity.Burst.BurstCompile(DisableSafetyChecks = true, OptimizeFor = global::Unity.Burst.OptimizeFor.Performance)]
	internal struct PrefixSumDrawInstancesJob : global::Unity.Jobs.IJob
	{
		[global::Unity.Collections.ReadOnly]
		public global::Unity.Collections.NativeParallelHashMap<global::UnityEngine.Rendering.RangeKey, int> rangeHash;

		public global::Unity.Collections.NativeList<global::UnityEngine.Rendering.DrawRange> drawRanges;

		public global::Unity.Collections.NativeList<global::UnityEngine.Rendering.DrawBatch> drawBatches;

		public global::Unity.Collections.NativeArray<int> drawBatchIndices;

		public void Execute()
		{
			int num = 0;
			for (int i = 0; i < drawRanges.Length; i++)
			{
				ref global::UnityEngine.Rendering.DrawRange reference = ref drawRanges.ElementAt(i);
				reference.drawOffset = num;
				num += reference.drawCount;
			}
			global::Unity.Collections.NativeArray<int> nativeArray = new global::Unity.Collections.NativeArray<int>(drawRanges.Length, global::Unity.Collections.Allocator.Temp);
			for (int j = 0; j < drawBatches.Length; j++)
			{
				ref global::UnityEngine.Rendering.DrawBatch reference2 = ref drawBatches.ElementAt(j);
				if (rangeHash.TryGetValue(reference2.key.range, out var item))
				{
					ref global::UnityEngine.Rendering.DrawRange reference3 = ref drawRanges.ElementAt(item);
					drawBatchIndices[reference3.drawOffset + nativeArray[item]] = j;
					nativeArray[item]++;
				}
			}
			int num2 = 0;
			for (int k = 0; k < drawBatchIndices.Length; k++)
			{
				int index = drawBatchIndices[k];
				ref global::UnityEngine.Rendering.DrawBatch reference4 = ref drawBatches.ElementAt(index);
				reference4.instanceOffset = num2;
				num2 += reference4.instanceCount;
			}
			nativeArray.Dispose();
		}
	}
}
