namespace UnityEngine.Rendering
{
	[global::Unity.Burst.BurstCompile(DisableSafetyChecks = true, OptimizeFor = global::Unity.Burst.OptimizeFor.Performance)]
	internal struct UpdateLODGroupTransformJob : global::Unity.Jobs.IJobParallelFor
	{
		public const int k_BatchSize = 256;

		[global::Unity.Collections.ReadOnly]
		public global::Unity.Collections.NativeParallelHashMap<int, global::UnityEngine.Rendering.GPUInstanceIndex> lodGroupDataHash;

		[global::Unity.Collections.ReadOnly]
		public global::Unity.Collections.NativeArray<global::UnityEngine.EntityId> lodGroupIDs;

		[global::Unity.Collections.ReadOnly]
		public global::Unity.Collections.NativeArray<global::UnityEngine.Vector3> worldSpaceReferencePoints;

		[global::Unity.Collections.ReadOnly]
		public global::Unity.Collections.NativeArray<float> worldSpaceSizes;

		[global::Unity.Collections.ReadOnly]
		public bool requiresGPUUpload;

		[global::Unity.Collections.ReadOnly]
		public bool supportDitheringCrossFade;

		[global::Unity.Collections.LowLevel.Unsafe.NativeDisableContainerSafetyRestriction]
		[global::Unity.Burst.NoAlias]
		[global::Unity.Collections.ReadOnly]
		public global::Unity.Collections.NativeList<global::UnityEngine.Rendering.LODGroupData> lodGroupData;

		[global::Unity.Collections.LowLevel.Unsafe.NativeDisableContainerSafetyRestriction]
		[global::Unity.Burst.NoAlias]
		[global::Unity.Collections.WriteOnly]
		public global::Unity.Collections.NativeList<global::UnityEngine.Rendering.LODGroupCullingData> lodGroupCullingData;

		[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
		public global::Unity.Collections.LowLevel.Unsafe.UnsafeAtomicCounter32 atomicUpdateCount;

		public unsafe void Execute(int index)
		{
			int key = lodGroupIDs[index];
			if (!lodGroupDataHash.TryGetValue(key, out var item))
			{
				return;
			}
			float num = worldSpaceSizes[index];
			global::UnityEngine.Rendering.LODGroupData* ptr = global::Unity.Collections.LowLevel.Unsafe.NativeListUnsafeUtility.GetUnsafePtr(lodGroupData) + item.index;
			global::UnityEngine.Rendering.LODGroupCullingData* ptr2 = global::Unity.Collections.LowLevel.Unsafe.NativeListUnsafeUtility.GetUnsafePtr(lodGroupCullingData) + item.index;
			ptr2->worldSpaceSize = num;
			ptr2->worldSpaceReferencePoint = worldSpaceReferencePoints[index];
			for (int i = 0; i < ptr->lodCount; i++)
			{
				float num2 = ptr->screenRelativeTransitionHeights[i];
				float num3 = global::UnityEngine.Rendering.LODRenderingUtils.CalculateLODDistance(num2, num);
				ptr2->sqrDistances[i] = num3 * num3;
				if (supportDitheringCrossFade && !ptr2->percentageFlags[i])
				{
					float num4 = ((i != 0) ? ptr->screenRelativeTransitionHeights[i - 1] : 1f);
					float relativeScreenHeight = num2 + ptr->fadeTransitionWidth[i] * (num4 - num2);
					float b = num3 - global::UnityEngine.Rendering.LODRenderingUtils.CalculateLODDistance(relativeScreenHeight, num);
					b = global::UnityEngine.Mathf.Max(0f, b);
					ptr2->transitionDistances[i] = b;
				}
				else
				{
					ptr2->transitionDistances[i] = 0f;
				}
			}
		}
	}
}
