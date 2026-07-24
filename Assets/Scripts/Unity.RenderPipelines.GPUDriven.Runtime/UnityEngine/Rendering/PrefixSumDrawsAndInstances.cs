namespace UnityEngine.Rendering
{
	[global::Unity.Burst.BurstCompile(DisableSafetyChecks = true, OptimizeFor = global::Unity.Burst.OptimizeFor.Performance)]
	internal struct PrefixSumDrawsAndInstances : global::Unity.Jobs.IJob
	{
		[global::Unity.Collections.ReadOnly]
		public global::Unity.Collections.NativeList<global::UnityEngine.Rendering.DrawRange> drawRanges;

		[global::Unity.Collections.ReadOnly]
		public global::Unity.Collections.NativeArray<int> drawBatchIndices;

		[global::Unity.Collections.ReadOnly]
		public global::Unity.Collections.NativeArray<int> batchBinAllocOffsets;

		[global::Unity.Collections.ReadOnly]
		public global::Unity.Collections.NativeArray<int> batchBinCounts;

		[global::Unity.Collections.ReadOnly]
		public global::Unity.Collections.NativeArray<int> binVisibleInstanceCounts;

		[global::Unity.Collections.LowLevel.Unsafe.NativeDisableContainerSafetyRestriction]
		[global::Unity.Burst.NoAlias]
		[global::Unity.Collections.WriteOnly]
		public global::Unity.Collections.NativeArray<int> batchDrawCommandOffsets;

		[global::Unity.Collections.LowLevel.Unsafe.NativeDisableContainerSafetyRestriction]
		[global::Unity.Burst.NoAlias]
		[global::Unity.Collections.WriteOnly]
		public global::Unity.Collections.NativeArray<int> binVisibleInstanceOffsets;

		[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
		public global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.BatchCullingOutputDrawCommands> cullingOutput;

		[global::Unity.Collections.ReadOnly]
		public global::UnityEngine.Rendering.IndirectBufferLimits indirectBufferLimits;

		[global::Unity.Collections.LowLevel.Unsafe.NativeDisableContainerSafetyRestriction]
		[global::Unity.Burst.NoAlias]
		public global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.IndirectBufferAllocInfo> indirectBufferAllocInfo;

		[global::Unity.Collections.LowLevel.Unsafe.NativeDisableContainerSafetyRestriction]
		[global::Unity.Burst.NoAlias]
		public global::Unity.Collections.NativeArray<int> indirectAllocationCounters;

		public unsafe void Execute()
		{
			global::UnityEngine.Rendering.BatchCullingOutputDrawCommands value = cullingOutput[0];
			bool flag = indirectBufferLimits.maxInstanceCount > 0;
			int num2;
			int num3;
			int num4;
			while (true)
			{
				int num = 0;
				num2 = 0;
				num3 = 0;
				num4 = 0;
				int num5 = 0;
				for (int i = 0; i < drawRanges.Length; i++)
				{
					global::UnityEngine.Rendering.DrawRange drawRange = drawRanges[i];
					bool flag2 = flag && drawRange.key.supportsIndirect;
					int num6 = 0;
					int drawCommandsBegin = (flag2 ? num4 : num2);
					for (int j = 0; j < drawRange.drawCount; j++)
					{
						int index = drawBatchIndices[drawRange.drawOffset + j];
						int num7 = batchBinAllocOffsets[index];
						int num8 = batchBinCounts[index];
						if (flag2)
						{
							batchDrawCommandOffsets[index] = num4;
							num4 += num8;
						}
						else
						{
							batchDrawCommandOffsets[index] = num2;
							num2 += num8;
						}
						num6 += num8;
						for (int k = 0; k < num8; k++)
						{
							int index2 = num7 + k;
							if (flag2)
							{
								binVisibleInstanceOffsets[index2] = num5;
								num5 += binVisibleInstanceCounts[index2];
							}
							else
							{
								binVisibleInstanceOffsets[index2] = num3;
								num3 += binVisibleInstanceCounts[index2];
							}
						}
					}
					if (num6 != 0)
					{
						global::UnityEngine.Rendering.RangeKey key = drawRange.key;
						value.drawRanges[num] = new global::UnityEngine.Rendering.BatchDrawRange
						{
							drawCommandsBegin = (uint)drawCommandsBegin,
							drawCommandsCount = (uint)num6,
							drawCommandsType = (flag2 ? global::UnityEngine.Rendering.BatchDrawCommandType.Indirect : global::UnityEngine.Rendering.BatchDrawCommandType.Direct),
							filterSettings = new global::UnityEngine.Rendering.BatchFilterSettings
							{
								renderingLayerMask = key.renderingLayerMask,
								rendererPriority = key.rendererPriority,
								layer = key.layer,
								batchLayer = (byte)(flag2 ? 28 : 29),
								motionMode = key.motionMode,
								shadowCastingMode = key.shadowCastingMode,
								receiveShadows = true,
								staticShadowCaster = key.staticShadowCaster,
								allDepthSorted = false
							}
						};
						num++;
					}
				}
				value.drawRangeCount = num;
				bool flag3 = true;
				if (flag)
				{
					int* unsafePtr = (int*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(indirectAllocationCounters);
					global::UnityEngine.Rendering.IndirectBufferAllocInfo value2 = new global::UnityEngine.Rendering.IndirectBufferAllocInfo
					{
						drawCount = num4,
						instanceCount = num5
					};
					int drawCount = value2.drawCount;
					int num9 = global::System.Threading.Interlocked.Add(ref global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AsRef<int>(unsafePtr + 1), drawCount);
					value2.drawAllocIndex = num9 - drawCount;
					int num10 = global::System.Threading.Interlocked.Add(ref global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AsRef<int>(unsafePtr), value2.instanceCount);
					value2.instanceAllocIndex = num10 - value2.instanceCount;
					if (!value2.IsWithinLimits(in indirectBufferLimits))
					{
						value2 = default(global::UnityEngine.Rendering.IndirectBufferAllocInfo);
						flag3 = false;
					}
					indirectBufferAllocInfo[0] = value2;
				}
				if (flag3)
				{
					break;
				}
				flag = false;
			}
			if (num2 != 0)
			{
				value.drawCommandCount = num2;
				value.drawCommands = global::UnityEngine.Rendering.MemoryUtilities.Malloc<global::UnityEngine.Rendering.BatchDrawCommand>(num2, global::Unity.Collections.Allocator.TempJob);
				value.visibleInstanceCount = num3;
				value.visibleInstances = global::UnityEngine.Rendering.MemoryUtilities.Malloc<int>(num3, global::Unity.Collections.Allocator.TempJob);
			}
			if (num4 != 0)
			{
				value.indirectDrawCommandCount = num4;
				value.indirectDrawCommands = global::UnityEngine.Rendering.MemoryUtilities.Malloc<global::UnityEngine.Rendering.BatchDrawCommandIndirect>(num4, global::Unity.Collections.Allocator.TempJob);
			}
			int num11 = num2 + num4;
			value.instanceSortingPositions = global::UnityEngine.Rendering.MemoryUtilities.Malloc<float>(3 * num11, global::Unity.Collections.Allocator.TempJob);
			cullingOutput[0] = value;
		}
	}
}
