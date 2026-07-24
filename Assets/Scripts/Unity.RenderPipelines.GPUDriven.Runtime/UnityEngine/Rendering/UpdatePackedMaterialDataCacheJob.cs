namespace UnityEngine.Rendering
{
	[global::Unity.Burst.BurstCompile(DisableSafetyChecks = true, OptimizeFor = global::Unity.Burst.OptimizeFor.Performance)]
	internal struct UpdatePackedMaterialDataCacheJob : global::Unity.Jobs.IJob
	{
		[global::Unity.Collections.ReadOnly]
		public global::Unity.Collections.NativeArray<global::UnityEngine.EntityId>.ReadOnly materialIDs;

		[global::Unity.Collections.ReadOnly]
		public global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.GPUDrivenPackedMaterialData>.ReadOnly packedMaterialDatas;

		public global::Unity.Collections.NativeParallelHashMap<global::UnityEngine.EntityId, global::UnityEngine.Rendering.GPUDrivenPackedMaterialData> packedMaterialHash;

		private void ProcessMaterial(int i)
		{
			global::UnityEngine.EntityId entityId = materialIDs[i];
			global::UnityEngine.Rendering.GPUDrivenPackedMaterialData value = packedMaterialDatas[i];
			if (!(entityId == 0))
			{
				packedMaterialHash[entityId] = value;
			}
		}

		public void Execute()
		{
			for (int i = 0; i < materialIDs.Length; i++)
			{
				ProcessMaterial(i);
			}
		}
	}
}
