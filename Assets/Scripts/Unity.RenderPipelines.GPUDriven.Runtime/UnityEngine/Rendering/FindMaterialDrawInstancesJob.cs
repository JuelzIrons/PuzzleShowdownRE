namespace UnityEngine.Rendering
{
	[global::Unity.Burst.BurstCompile(DisableSafetyChecks = true, OptimizeFor = global::Unity.Burst.OptimizeFor.Performance)]
	internal struct FindMaterialDrawInstancesJob : global::Unity.Jobs.IJobParallelForBatch
	{
		public const int k_BatchSize = 128;

		[global::Unity.Collections.ReadOnly]
		public global::Unity.Collections.NativeArray<uint> materialsSorted;

		[global::Unity.Collections.LowLevel.Unsafe.NativeDisableContainerSafetyRestriction]
		[global::Unity.Burst.NoAlias]
		[global::Unity.Collections.ReadOnly]
		public global::Unity.Collections.NativeList<global::UnityEngine.Rendering.DrawInstance> drawInstances;

		[global::Unity.Collections.WriteOnly]
		public global::Unity.Collections.NativeList<int>.ParallelWriter outDrawInstanceIndicesWriter;

		public unsafe void Execute(int startIndex, int count)
		{
			int* ptr = stackalloc int[128];
			int count2 = 0;
			for (int i = startIndex; i < startIndex + count; i++)
			{
				ref global::UnityEngine.Rendering.DrawInstance reference = ref drawInstances.ElementAt(i);
				if (global::Unity.Collections.NativeSortExtension.BinarySearch(materialsSorted, reference.key.materialID.value) >= 0)
				{
					ptr[count2++] = i;
				}
			}
			outDrawInstanceIndicesWriter.AddRangeNoResize(ptr, count2);
		}
	}
}
