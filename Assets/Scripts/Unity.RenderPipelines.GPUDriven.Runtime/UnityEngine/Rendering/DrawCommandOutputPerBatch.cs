namespace UnityEngine.Rendering
{
	[global::Unity.Burst.BurstCompile(DisableSafetyChecks = true, OptimizeFor = global::Unity.Burst.OptimizeFor.Performance)]
	internal struct DrawCommandOutputPerBatch : global::Unity.Jobs.IJobParallelFor
	{
		[global::Unity.Collections.ReadOnly]
		public global::UnityEngine.Rendering.BinningConfig binningConfig;

		[global::Unity.Collections.ReadOnly]
		public global::Unity.Collections.NativeParallelHashMap<uint, global::UnityEngine.Rendering.BatchID> batchIDs;

		[global::Unity.Collections.ReadOnly]
		public global::UnityEngine.Rendering.GPUInstanceDataBuffer.ReadOnly instanceDataBuffer;

		[global::Unity.Collections.ReadOnly]
		public global::Unity.Collections.NativeList<global::UnityEngine.Rendering.DrawBatch> drawBatches;

		[global::Unity.Collections.ReadOnly]
		public global::Unity.Collections.NativeArray<int> drawInstanceIndices;

		[global::Unity.Collections.ReadOnly]
		public global::UnityEngine.Rendering.CPUInstanceData.ReadOnly instanceData;

		[global::Unity.Collections.ReadOnly]
		public global::Unity.Collections.NativeArray<byte> rendererVisibilityMasks;

		[global::Unity.Collections.ReadOnly]
		public global::Unity.Collections.NativeArray<byte> rendererMeshLodSettings;

		[global::Unity.Collections.ReadOnly]
		public global::Unity.Collections.NativeArray<byte> rendererCrossFadeValues;

		[global::Unity.Collections.ReadOnly]
		[global::Unity.Collections.DeallocateOnJobCompletion]
		public global::Unity.Collections.NativeArray<int> batchBinAllocOffsets;

		[global::Unity.Collections.ReadOnly]
		[global::Unity.Collections.DeallocateOnJobCompletion]
		public global::Unity.Collections.NativeArray<int> batchBinCounts;

		[global::Unity.Collections.ReadOnly]
		[global::Unity.Collections.DeallocateOnJobCompletion]
		public global::Unity.Collections.NativeArray<int> batchDrawCommandOffsets;

		[global::Unity.Collections.ReadOnly]
		[global::Unity.Collections.DeallocateOnJobCompletion]
		public global::Unity.Collections.NativeArray<short> binConfigIndices;

		[global::Unity.Collections.ReadOnly]
		[global::Unity.Collections.DeallocateOnJobCompletion]
		public global::Unity.Collections.NativeArray<int> binVisibleInstanceOffsets;

		[global::Unity.Collections.ReadOnly]
		[global::Unity.Collections.DeallocateOnJobCompletion]
		public global::Unity.Collections.NativeArray<int> binVisibleInstanceCounts;

		[global::Unity.Collections.ReadOnly]
		public global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.BatchCullingOutputDrawCommands> cullingOutput;

		[global::Unity.Collections.ReadOnly]
		public global::UnityEngine.Rendering.IndirectBufferLimits indirectBufferLimits;

		[global::Unity.Collections.ReadOnly]
		public global::UnityEngine.GraphicsBufferHandle visibleInstancesBufferHandle;

		[global::Unity.Collections.ReadOnly]
		public global::UnityEngine.GraphicsBufferHandle indirectArgsBufferHandle;

		[global::Unity.Collections.LowLevel.Unsafe.NativeDisableContainerSafetyRestriction]
		[global::Unity.Burst.NoAlias]
		public global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.IndirectBufferAllocInfo> indirectBufferAllocInfo;

		[global::Unity.Collections.LowLevel.Unsafe.NativeDisableContainerSafetyRestriction]
		[global::Unity.Burst.NoAlias]
		public global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.IndirectDrawInfo> indirectDrawInfoGlobalArray;

		[global::Unity.Collections.LowLevel.Unsafe.NativeDisableContainerSafetyRestriction]
		[global::Unity.Burst.NoAlias]
		public global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.IndirectInstanceInfo> indirectInstanceInfoGlobalArray;

		private int EncodeGPUInstanceIndexAndCrossFade(int rendererIndex, bool negateCrossFade)
		{
			global::UnityEngine.Rendering.GPUInstanceIndex gPUInstanceIndex = instanceDataBuffer.CPUInstanceToGPUInstance(global::UnityEngine.Rendering.InstanceHandle.FromInt(rendererIndex));
			int num = rendererCrossFadeValues[rendererIndex];
			if ((long)num == 255)
			{
				return gPUInstanceIndex.index;
			}
			num -= 127;
			if (negateCrossFade)
			{
				num = -num;
			}
			gPUInstanceIndex.index |= num << 24;
			return gPUInstanceIndex.index;
		}

		private bool IsInstanceFlipped(int rendererIndex)
		{
			global::UnityEngine.Rendering.InstanceHandle instance = global::UnityEngine.Rendering.InstanceHandle.FromInt(rendererIndex);
			int index = instanceData.InstanceToIndex(instance);
			return instanceData.localToWorldIsFlippedBits.Get(index);
		}

		private bool IsMeshLodVisible(int batchLodLevel, int rendererIndex, bool supportsCrossFade, ref bool negateCrossfade)
		{
			if (batchLodLevel < 0)
			{
				return true;
			}
			byte b = rendererMeshLodSettings[rendererIndex];
			uint num = (uint)(b & -193);
			if (batchLodLevel == num)
			{
				return true;
			}
			if (!supportsCrossFade)
			{
				return false;
			}
			uint num2 = (uint)(b & 0xC0);
			if (num2 == 0)
			{
				return false;
			}
			int num3 = (int)(num2 - 128) >> 6;
			negateCrossfade = true;
			return batchLodLevel == num + num3;
		}

		public unsafe void Execute(int batchIndex)
		{
			global::UnityEngine.Rendering.DrawBatch drawBatch = drawBatches[batchIndex];
			int num = batchBinCounts[batchIndex];
			if (num == 0)
			{
				return;
			}
			global::UnityEngine.Rendering.BatchCullingOutputDrawCommands batchCullingOutputDrawCommands = cullingOutput[0];
			global::UnityEngine.Rendering.IndirectBufferAllocInfo indirectBufferAllocInfo = default(global::UnityEngine.Rendering.IndirectBufferAllocInfo);
			if (indirectBufferLimits.maxDrawCount > 0)
			{
				indirectBufferAllocInfo = this.indirectBufferAllocInfo[0];
			}
			bool flag = !indirectBufferAllocInfo.IsEmpty() && drawBatch.key.range.supportsIndirect;
			int visibilityConfigCount = binningConfig.visibilityConfigCount;
			int* ptr = stackalloc int[visibilityConfigCount];
			for (int i = 0; i < visibilityConfigCount; i++)
			{
				ptr[i] = 0;
			}
			int* ptr2 = stackalloc int[visibilityConfigCount];
			int num2 = batchBinAllocOffsets[batchIndex];
			int num3 = batchDrawCommandOffsets[batchIndex];
			int num4 = 0;
			bool flag2 = drawBatch.key.range.motionMode == global::UnityEngine.MotionVectorGenerationMode.Object || drawBatch.key.range.motionMode == global::UnityEngine.MotionVectorGenerationMode.ForceNoMotion;
			for (int j = 0; j < num; j++)
			{
				int index = num2 + j;
				int num5 = binVisibleInstanceOffsets[index];
				int num6 = binVisibleInstanceCounts[index];
				num4 = num5;
				short num7 = binConfigIndices[index];
				ptr[num7] = num5;
				int num8 = (ptr2[num7] = num3 + j);
				global::UnityEngine.Rendering.BatchDrawCommandFlags batchDrawCommandFlags = drawBatch.key.flags;
				if ((num7 & 1) != 0)
				{
					batchDrawCommandFlags |= global::UnityEngine.Rendering.BatchDrawCommandFlags.FlipWinding;
				}
				int num9 = num7 >> 1;
				if (binningConfig.supportsCrossFade)
				{
					batchDrawCommandFlags = (((num9 & 1) != 0) ? (batchDrawCommandFlags | global::UnityEngine.Rendering.BatchDrawCommandFlags.LODCrossFadeKeyword) : (batchDrawCommandFlags & ~global::UnityEngine.Rendering.BatchDrawCommandFlags.LODCrossFadeKeyword));
					num9 >>= 1;
				}
				else
				{
					batchDrawCommandFlags &= ~global::UnityEngine.Rendering.BatchDrawCommandFlags.LODCrossFadeKeyword;
				}
				if (binningConfig.supportsMotionCheck)
				{
					if ((num9 & 1) != 0 && flag2)
					{
						batchDrawCommandFlags |= global::UnityEngine.Rendering.BatchDrawCommandFlags.HasMotion;
					}
					num9 >>= 1;
				}
				int sortingPosition = 0;
				if ((batchDrawCommandFlags & global::UnityEngine.Rendering.BatchDrawCommandFlags.HasSortingPosition) != global::UnityEngine.Rendering.BatchDrawCommandFlags.None)
				{
					int num10 = num8;
					if (flag)
					{
						num10 += batchCullingOutputDrawCommands.drawCommandCount;
					}
					sortingPosition = 3 * num10;
				}
				if (flag)
				{
					int num11 = indirectBufferAllocInfo.instanceAllocIndex + num5;
					int num12 = indirectBufferAllocInfo.drawAllocIndex + num8;
					indirectDrawInfoGlobalArray[num12] = new global::UnityEngine.Rendering.IndirectDrawInfo
					{
						indexCount = drawBatch.procInfo.indexCount,
						firstIndex = drawBatch.procInfo.firstIndex,
						baseVertex = drawBatch.procInfo.baseVertex,
						firstInstanceGlobalIndex = (uint)num11,
						maxInstanceCountAndTopology = ((uint)(num6 << 3) | (uint)drawBatch.procInfo.topology)
					};
					batchCullingOutputDrawCommands.indirectDrawCommands[num8] = new global::UnityEngine.Rendering.BatchDrawCommandIndirect
					{
						flags = batchDrawCommandFlags,
						visibleOffset = (uint)num11,
						batchID = batchIDs[drawBatch.key.overridenComponents],
						materialID = drawBatch.key.materialID,
						splitVisibilityMask = (ushort)num9,
						lightmapIndex = (ushort)drawBatch.key.lightmapIndex,
						sortingPosition = sortingPosition,
						meshID = drawBatch.key.meshID,
						topology = drawBatch.procInfo.topology,
						visibleInstancesBufferHandle = visibleInstancesBufferHandle,
						indirectArgsBufferHandle = indirectArgsBufferHandle,
						indirectArgsBufferOffset = (uint)(num12 * 20)
					};
				}
				else
				{
					batchCullingOutputDrawCommands.drawCommands[num8] = new global::UnityEngine.Rendering.BatchDrawCommand
					{
						flags = batchDrawCommandFlags,
						visibleOffset = (uint)num5,
						visibleCount = (uint)num6,
						batchID = batchIDs[drawBatch.key.overridenComponents],
						materialID = drawBatch.key.materialID,
						splitVisibilityMask = (ushort)num9,
						lightmapIndex = (ushort)drawBatch.key.lightmapIndex,
						sortingPosition = sortingPosition,
						meshID = drawBatch.key.meshID,
						submeshIndex = (ushort)drawBatch.key.submeshIndex,
						activeMeshLod = (ushort)drawBatch.key.activeMeshLod
					};
				}
			}
			int instanceOffset = drawBatch.instanceOffset;
			int instanceCount = drawBatch.instanceCount;
			bool supportsCrossFade = (drawBatch.key.flags & global::UnityEngine.Rendering.BatchDrawCommandFlags.LODCrossFadeKeyword) != 0;
			int num13 = 0;
			if (num > 1)
			{
				for (int k = 0; k < instanceCount; k++)
				{
					int num14 = drawInstanceIndices[instanceOffset + k];
					bool flag3 = IsInstanceFlipped(num14);
					int num15 = rendererVisibilityMasks[num14];
					if (num15 == 0)
					{
						continue;
					}
					bool negateCrossfade = false;
					if (!IsMeshLodVisible(drawBatch.key.activeMeshLod, num14, supportsCrossFade, ref negateCrossfade))
					{
						continue;
					}
					num13 = num14;
					int num16 = (num15 << 1) | (flag3 ? 1 : 0);
					int num17 = ptr[num16];
					ptr[num16]++;
					int num18 = EncodeGPUInstanceIndexAndCrossFade(num14, negateCrossfade);
					if (flag)
					{
						if (binningConfig.supportsCrossFade)
						{
							num15 >>= 1;
						}
						if (binningConfig.supportsMotionCheck)
						{
							num15 >>= 1;
						}
						indirectInstanceInfoGlobalArray[indirectBufferAllocInfo.instanceAllocIndex + num17] = new global::UnityEngine.Rendering.IndirectInstanceInfo
						{
							drawOffsetAndSplitMask = ((ptr2[num16] << 8) | num15),
							instanceIndexAndCrossFade = num18
						};
					}
					else
					{
						batchCullingOutputDrawCommands.visibleInstances[num17] = num18;
					}
				}
			}
			else
			{
				int num19 = num4;
				for (int l = 0; l < instanceCount; l++)
				{
					int num20 = drawInstanceIndices[instanceOffset + l];
					int num21 = rendererVisibilityMasks[num20];
					if (num21 == 0)
					{
						continue;
					}
					bool negateCrossfade2 = false;
					if (!IsMeshLodVisible(drawBatch.key.activeMeshLod, num20, supportsCrossFade, ref negateCrossfade2))
					{
						continue;
					}
					num13 = num20;
					int num22 = EncodeGPUInstanceIndexAndCrossFade(num20, negateCrossfade2);
					if (flag)
					{
						if (binningConfig.supportsCrossFade)
						{
							num21 >>= 1;
						}
						if (binningConfig.supportsMotionCheck)
						{
							num21 >>= 1;
						}
						indirectInstanceInfoGlobalArray[indirectBufferAllocInfo.instanceAllocIndex + num19] = new global::UnityEngine.Rendering.IndirectInstanceInfo
						{
							drawOffsetAndSplitMask = ((num3 << 8) | num21),
							instanceIndexAndCrossFade = num22
						};
					}
					else
					{
						batchCullingOutputDrawCommands.visibleInstances[num19] = num22;
					}
					num19++;
				}
			}
			if ((drawBatch.key.flags & global::UnityEngine.Rendering.BatchDrawCommandFlags.HasSortingPosition) != global::UnityEngine.Rendering.BatchDrawCommandFlags.None)
			{
				global::UnityEngine.Rendering.InstanceHandle instance = global::UnityEngine.Rendering.InstanceHandle.FromInt(num13 & 0xFFFFFF);
				int index2 = instanceData.InstanceToIndex(instance);
				global::Unity.Mathematics.float3 center = instanceData.worldAABBs.UnsafeElementAt(index2).center;
				int num23 = num3;
				if (flag)
				{
					num23 += batchCullingOutputDrawCommands.drawCommandCount;
				}
				int num24 = 3 * num23;
				batchCullingOutputDrawCommands.instanceSortingPositions[num24] = center.x;
				batchCullingOutputDrawCommands.instanceSortingPositions[num24 + 1] = center.y;
				batchCullingOutputDrawCommands.instanceSortingPositions[num24 + 2] = center.z;
			}
		}
	}
}
