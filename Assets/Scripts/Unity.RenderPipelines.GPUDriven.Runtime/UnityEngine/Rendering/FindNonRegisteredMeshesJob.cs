namespace UnityEngine.Rendering
{
	[global::Unity.Burst.BurstCompile(DisableSafetyChecks = true, OptimizeFor = global::Unity.Burst.OptimizeFor.Performance)]
	internal struct FindNonRegisteredMeshesJob : global::Unity.Jobs.IJobParallelForBatch
	{
		public const int k_BatchSize = 128;

		[global::Unity.Collections.ReadOnly]
		public global::Unity.Collections.NativeArray<global::UnityEngine.EntityId> instanceIDs;

		[global::Unity.Collections.ReadOnly]
		public global::Unity.Collections.NativeParallelHashMap<global::UnityEngine.EntityId, global::UnityEngine.Rendering.BatchMeshID> hashMap;

		[global::Unity.Collections.WriteOnly]
		public global::Unity.Collections.NativeList<global::UnityEngine.EntityId>.ParallelWriter outInstancesWriter;

		public unsafe void Execute(int startIndex, int count)
		{
			global::UnityEngine.EntityId* ptr = stackalloc global::UnityEngine.EntityId[128];
			global::Unity.Collections.LowLevel.Unsafe.UnsafeList<global::UnityEngine.EntityId> unsafeList = new global::Unity.Collections.LowLevel.Unsafe.UnsafeList<global::UnityEngine.EntityId>(ptr, 128);
			unsafeList.Length = 0;
			for (int i = startIndex; i < startIndex + count; i++)
			{
				global::UnityEngine.EntityId entityId = instanceIDs[i];
				if (!hashMap.ContainsKey(entityId))
				{
					unsafeList.AddNoResize(entityId);
				}
			}
			outInstancesWriter.AddRangeNoResize(ptr, unsafeList.Length);
		}
	}
}
