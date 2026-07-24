namespace UnityEngine.Rendering
{
	[global::Unity.Burst.BurstCompile(DisableSafetyChecks = true, OptimizeFor = global::Unity.Burst.OptimizeFor.Performance)]
	internal struct BuildDrawListsJob : global::Unity.Jobs.IJobParallelFor
	{
		public const int k_BatchSize = 128;

		public const int k_IntsPerCacheLine = 16;

		[global::Unity.Collections.ReadOnly]
		public global::Unity.Collections.NativeParallelHashMap<global::UnityEngine.Rendering.DrawKey, int> batchHash;

		[global::Unity.Collections.LowLevel.Unsafe.NativeDisableContainerSafetyRestriction]
		[global::Unity.Burst.NoAlias]
		[global::Unity.Collections.ReadOnly]
		public global::Unity.Collections.NativeList<global::UnityEngine.Rendering.DrawInstance> drawInstances;

		[global::Unity.Collections.LowLevel.Unsafe.NativeDisableContainerSafetyRestriction]
		[global::Unity.Burst.NoAlias]
		[global::Unity.Collections.ReadOnly]
		public global::Unity.Collections.NativeList<global::UnityEngine.Rendering.DrawBatch> drawBatches;

		[global::Unity.Collections.LowLevel.Unsafe.NativeDisableContainerSafetyRestriction]
		[global::Unity.Burst.NoAlias]
		[global::Unity.Collections.WriteOnly]
		public global::Unity.Collections.NativeArray<int> internalDrawIndex;

		[global::Unity.Collections.LowLevel.Unsafe.NativeDisableContainerSafetyRestriction]
		[global::Unity.Burst.NoAlias]
		[global::Unity.Collections.WriteOnly]
		public global::Unity.Collections.NativeArray<int> drawInstanceIndices;

		private unsafe static int IncrementCounter(int* counter)
		{
			return global::System.Threading.Interlocked.Increment(ref global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AsRef<int>(counter)) - 1;
		}

		public unsafe void Execute(int index)
		{
			ref global::UnityEngine.Rendering.DrawInstance reference = ref drawInstances.ElementAt(index);
			int num = batchHash[reference.key];
			ref global::UnityEngine.Rendering.DrawBatch reference2 = ref drawBatches.ElementAt(num);
			int num2 = IncrementCounter((int*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(internalDrawIndex) + num * 16);
			int index2 = reference2.instanceOffset + num2;
			drawInstanceIndices[index2] = reference.instanceIndex;
		}
	}
}
