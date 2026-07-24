namespace UnityEngine.Rendering
{
	[global::Unity.Burst.BurstCompile(DisableSafetyChecks = true, OptimizeFor = global::Unity.Burst.OptimizeFor.Performance)]
	internal struct RegisterNewMaterialsJob : global::Unity.Jobs.IJobParallelFor
	{
		public const int k_BatchSize = 128;

		[global::Unity.Collections.ReadOnly]
		public global::Unity.Collections.NativeArray<global::UnityEngine.EntityId> instanceIDs;

		[global::Unity.Collections.ReadOnly]
		public global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.GPUDrivenPackedMaterialData> packedMaterialDatas;

		[global::Unity.Collections.ReadOnly]
		public global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.BatchMaterialID> batchIDs;

		[global::Unity.Collections.WriteOnly]
		public global::Unity.Collections.NativeParallelHashMap<global::UnityEngine.EntityId, global::UnityEngine.Rendering.BatchMaterialID>.ParallelWriter batchMaterialHashMap;

		[global::Unity.Collections.WriteOnly]
		public global::Unity.Collections.NativeParallelHashMap<global::UnityEngine.EntityId, global::UnityEngine.Rendering.GPUDrivenPackedMaterialData>.ParallelWriter packedMaterialHashMap;

		public void Execute(int index)
		{
			global::UnityEngine.EntityId key = instanceIDs[index];
			batchMaterialHashMap.TryAdd(key, batchIDs[index]);
			packedMaterialHashMap.TryAdd(key, packedMaterialDatas[index]);
		}
	}
}
