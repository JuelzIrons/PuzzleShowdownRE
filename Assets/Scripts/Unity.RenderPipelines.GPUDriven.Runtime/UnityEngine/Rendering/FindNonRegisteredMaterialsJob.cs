namespace UnityEngine.Rendering
{
	[global::Unity.Burst.BurstCompile(DisableSafetyChecks = true, OptimizeFor = global::Unity.Burst.OptimizeFor.Performance)]
	internal struct FindNonRegisteredMaterialsJob : global::Unity.Jobs.IJobParallelForBatch
	{
		public const int k_BatchSize = 128;

		[global::Unity.Collections.ReadOnly]
		public global::Unity.Collections.NativeArray<global::UnityEngine.EntityId> instanceIDs;

		[global::Unity.Collections.ReadOnly]
		public global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.GPUDrivenPackedMaterialData> packedMaterialDatas;

		[global::Unity.Collections.ReadOnly]
		public global::Unity.Collections.NativeParallelHashMap<global::UnityEngine.EntityId, global::UnityEngine.Rendering.BatchMaterialID> hashMap;

		[global::Unity.Collections.WriteOnly]
		public global::Unity.Collections.NativeList<global::UnityEngine.EntityId>.ParallelWriter outInstancesWriter;

		[global::Unity.Collections.WriteOnly]
		public global::Unity.Collections.NativeList<global::UnityEngine.Rendering.GPUDrivenPackedMaterialData>.ParallelWriter outPackedMaterialDatasWriter;

		public unsafe void Execute(int startIndex, int count)
		{
			int* ptr = stackalloc int[128];
			global::Unity.Collections.LowLevel.Unsafe.UnsafeList<int> unsafeList = new global::Unity.Collections.LowLevel.Unsafe.UnsafeList<int>(ptr, 128);
			global::UnityEngine.Rendering.GPUDrivenPackedMaterialData* ptr2 = stackalloc global::UnityEngine.Rendering.GPUDrivenPackedMaterialData[128];
			global::Unity.Collections.LowLevel.Unsafe.UnsafeList<global::UnityEngine.Rendering.GPUDrivenPackedMaterialData> unsafeList2 = new global::Unity.Collections.LowLevel.Unsafe.UnsafeList<global::UnityEngine.Rendering.GPUDrivenPackedMaterialData>(ptr2, 128);
			unsafeList.Length = 0;
			unsafeList2.Length = 0;
			for (int i = startIndex; i < startIndex + count; i++)
			{
				int num = instanceIDs[i];
				if (!hashMap.ContainsKey(num))
				{
					unsafeList.AddNoResize(num);
					unsafeList2.AddNoResize(packedMaterialDatas[i]);
				}
			}
			outInstancesWriter.AddRangeNoResize(ptr, unsafeList.Length);
			outPackedMaterialDatasWriter.AddRangeNoResize(ptr2, unsafeList2.Length);
		}
	}
}
