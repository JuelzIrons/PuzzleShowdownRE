namespace UnityEngine.Rendering
{
	[global::Unity.Burst.BurstCompile(DisableSafetyChecks = true, OptimizeFor = global::Unity.Burst.OptimizeFor.Performance)]
	internal struct UpdateLODGroupDataJob : global::Unity.Jobs.IJobParallelFor
	{
		public const int k_BatchSize = 256;

		[global::Unity.Collections.ReadOnly]
		public global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.GPUInstanceIndex> lodGroupInstances;

		[global::Unity.Collections.ReadOnly]
		public global::UnityEngine.Rendering.GPUDrivenLODGroupData inputData;

		[global::Unity.Collections.ReadOnly]
		public bool supportDitheringCrossFade;

		public global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.LODGroupData> lodGroupsData;

		public global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.LODGroupCullingData> lodGroupsCullingData;

		[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
		public global::Unity.Collections.LowLevel.Unsafe.UnsafeAtomicCounter32 rendererCount;

		public unsafe void Execute(int index)
		{
			global::UnityEngine.Rendering.GPUInstanceIndex gPUInstanceIndex = lodGroupInstances[index];
			global::UnityEngine.LODFadeMode num = inputData.fadeMode[index];
			int num2 = inputData.lodOffset[index];
			int num3 = inputData.lodCount[index];
			short num4 = inputData.renderersCount[index];
			global::UnityEngine.Vector3 vector = inputData.worldSpaceReferencePoint[index];
			float num5 = inputData.worldSpaceSize[index];
			bool flag = inputData.lastLODIsBillboard[index];
			byte forceLODMask = inputData.forceLODMask[index];
			bool flag2 = num != global::UnityEngine.LODFadeMode.None && supportDitheringCrossFade;
			bool flag3 = num == global::UnityEngine.LODFadeMode.SpeedTree;
			global::UnityEngine.Rendering.LODGroupData* ptr = (global::UnityEngine.Rendering.LODGroupData*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(lodGroupsData) + gPUInstanceIndex.index;
			global::UnityEngine.Rendering.LODGroupCullingData* ptr2 = (global::UnityEngine.Rendering.LODGroupCullingData*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(lodGroupsCullingData) + gPUInstanceIndex.index;
			ptr->valid = true;
			ptr->lodCount = num3;
			ptr->rendererCount = (flag2 ? num4 : 0);
			ptr2->worldSpaceSize = num5;
			ptr2->worldSpaceReferencePoint = vector;
			ptr2->forceLODMask = forceLODMask;
			ptr2->lodCount = num3;
			rendererCount.Add(ptr->rendererCount);
			int num6 = 0;
			if (flag3)
			{
				int index2 = num2 + (num3 - 1);
				bool flag4 = num3 > 0 && inputData.lodRenderersCount[index2] == 1 && flag;
				num6 = ((num3 != 0) ? ((!flag4) ? (num3 - 1) : (global::System.Math.Max(num3, 2) - 2)) : 0);
			}
			for (int i = 0; i < num3; i++)
			{
				int num7 = num2 + i;
				float num8 = inputData.lodScreenRelativeTransitionHeight[num7];
				float num9 = global::UnityEngine.Rendering.LODRenderingUtils.CalculateLODDistance(num8, num5);
				ptr->screenRelativeTransitionHeights[i] = num8;
				ptr->fadeTransitionWidth[i] = 0f;
				ptr2->sqrDistances[i] = num9 * num9;
				ptr2->percentageFlags[i] = false;
				ptr2->transitionDistances[i] = 0f;
				if (flag3 && i < num6)
				{
					ptr2->percentageFlags[i] = true;
				}
				else if (flag2 && i >= num6)
				{
					float num10 = inputData.lodFadeTransitionWidth[num7];
					float num11 = ((i != 0) ? inputData.lodScreenRelativeTransitionHeight[num7 - 1] : 1f);
					float relativeScreenHeight = num8 + num10 * (num11 - num8);
					float b = num9 - global::UnityEngine.Rendering.LODRenderingUtils.CalculateLODDistance(relativeScreenHeight, num5);
					b = global::UnityEngine.Mathf.Max(0f, b);
					ptr->fadeTransitionWidth[i] = num10;
					ptr2->transitionDistances[i] = b;
				}
			}
		}
	}
}
