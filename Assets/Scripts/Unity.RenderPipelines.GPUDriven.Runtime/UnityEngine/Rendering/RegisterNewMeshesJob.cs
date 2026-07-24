namespace UnityEngine.Rendering
{
	[global::Unity.Burst.BurstCompile(DisableSafetyChecks = true, OptimizeFor = global::Unity.Burst.OptimizeFor.Performance)]
	internal struct RegisterNewMeshesJob : global::Unity.Jobs.IJobParallelFor
	{
		public const int k_BatchSize = 128;

		[global::Unity.Collections.ReadOnly]
		public global::Unity.Collections.NativeArray<global::UnityEngine.EntityId> instanceIDs;

		[global::Unity.Collections.ReadOnly]
		public global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.BatchMeshID> batchIDs;

		[global::Unity.Collections.WriteOnly]
		public global::Unity.Collections.NativeParallelHashMap<global::UnityEngine.EntityId, global::UnityEngine.Rendering.BatchMeshID>.ParallelWriter hashMap;

		public void Execute(int index)
		{
			hashMap.TryAdd(instanceIDs[index], batchIDs[index]);
		}
	}
}
