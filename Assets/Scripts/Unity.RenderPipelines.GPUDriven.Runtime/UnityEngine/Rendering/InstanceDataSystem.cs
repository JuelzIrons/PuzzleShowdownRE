namespace UnityEngine.Rendering
{
	internal class InstanceDataSystem : global::System.IDisposable
	{
		private static class InstanceTransformUpdateIDs
		{
			public static readonly int _TransformUpdateQueueCount = global::UnityEngine.Shader.PropertyToID("_TransformUpdateQueueCount");

			public static readonly int _TransformUpdateOutputL2WVec4Offset = global::UnityEngine.Shader.PropertyToID("_TransformUpdateOutputL2WVec4Offset");

			public static readonly int _TransformUpdateOutputW2LVec4Offset = global::UnityEngine.Shader.PropertyToID("_TransformUpdateOutputW2LVec4Offset");

			public static readonly int _TransformUpdateOutputPrevL2WVec4Offset = global::UnityEngine.Shader.PropertyToID("_TransformUpdateOutputPrevL2WVec4Offset");

			public static readonly int _TransformUpdateOutputPrevW2LVec4Offset = global::UnityEngine.Shader.PropertyToID("_TransformUpdateOutputPrevW2LVec4Offset");

			public static readonly int _BoundingSphereOutputVec4Offset = global::UnityEngine.Shader.PropertyToID("_BoundingSphereOutputVec4Offset");

			public static readonly int _TransformUpdateDataQueue = global::UnityEngine.Shader.PropertyToID("_TransformUpdateDataQueue");

			public static readonly int _TransformUpdateIndexQueue = global::UnityEngine.Shader.PropertyToID("_TransformUpdateIndexQueue");

			public static readonly int _BoundingSphereDataQueue = global::UnityEngine.Shader.PropertyToID("_BoundingSphereDataQueue");

			public static readonly int _OutputTransformBuffer = global::UnityEngine.Shader.PropertyToID("_OutputTransformBuffer");

			public static readonly int _ProbeUpdateQueueCount = global::UnityEngine.Shader.PropertyToID("_ProbeUpdateQueueCount");

			public static readonly int _SHUpdateVec4Offset = global::UnityEngine.Shader.PropertyToID("_SHUpdateVec4Offset");

			public static readonly int _ProbeUpdateDataQueue = global::UnityEngine.Shader.PropertyToID("_ProbeUpdateDataQueue");

			public static readonly int _ProbeOcclusionUpdateDataQueue = global::UnityEngine.Shader.PropertyToID("_ProbeOcclusionUpdateDataQueue");

			public static readonly int _ProbeUpdateIndexQueue = global::UnityEngine.Shader.PropertyToID("_ProbeUpdateIndexQueue");

			public static readonly int _OutputProbeBuffer = global::UnityEngine.Shader.PropertyToID("_OutputProbeBuffer");
		}

		private static class InstanceWindDataUpdateIDs
		{
			public static readonly int _WindDataQueueCount = global::UnityEngine.Shader.PropertyToID("_WindDataQueueCount");

			public static readonly int _WindDataUpdateIndexQueue = global::UnityEngine.Shader.PropertyToID("_WindDataUpdateIndexQueue");

			public static readonly int _WindDataBuffer = global::UnityEngine.Shader.PropertyToID("_WindDataBuffer");

			public static readonly int _WindParamAddressArray = global::UnityEngine.Shader.PropertyToID("_WindParamAddressArray");

			public static readonly int _WindHistoryParamAddressArray = global::UnityEngine.Shader.PropertyToID("_WindHistoryParamAddressArray");
		}

		[global::Unity.Burst.BurstCompile(DisableSafetyChecks = true, OptimizeFor = global::Unity.Burst.OptimizeFor.Performance)]
		private struct QueryRendererGroupInstancesCountJob : global::Unity.Jobs.IJobParallelForBatch
		{
			public const int k_BatchSize = 128;

			[global::Unity.Collections.ReadOnly]
			public global::UnityEngine.Rendering.CPUInstanceData instanceData;

			[global::Unity.Collections.ReadOnly]
			public global::UnityEngine.Rendering.CPUSharedInstanceData sharedInstanceData;

			[global::Unity.Collections.ReadOnly]
			public global::Unity.Collections.NativeParallelMultiHashMap<int, global::UnityEngine.Rendering.InstanceHandle> rendererGroupInstanceMultiHash;

			[global::Unity.Collections.LowLevel.Unsafe.NativeDisableContainerSafetyRestriction]
			[global::Unity.Burst.NoAlias]
			[global::Unity.Collections.ReadOnly]
			public global::Unity.Collections.NativeArray<global::UnityEngine.EntityId> rendererGroupIDs;

			[global::Unity.Collections.LowLevel.Unsafe.NativeDisableContainerSafetyRestriction]
			[global::Unity.Burst.NoAlias]
			[global::Unity.Collections.WriteOnly]
			public global::Unity.Collections.NativeArray<int> instancesCount;

			public void Execute(int startIndex, int count)
			{
				for (int i = startIndex; i < startIndex + count; i++)
				{
					global::UnityEngine.EntityId entityId = rendererGroupIDs[i];
					if (rendererGroupInstanceMultiHash.TryGetFirstValue(entityId, out var item, out var _))
					{
						global::UnityEngine.Rendering.SharedInstanceHandle instance = instanceData.Get_SharedInstance(item);
						int value = sharedInstanceData.Get_RefCount(instance);
						instancesCount[i] = value;
					}
					else
					{
						instancesCount[i] = 0;
					}
				}
			}
		}

		[global::Unity.Burst.BurstCompile(DisableSafetyChecks = true, OptimizeFor = global::Unity.Burst.OptimizeFor.Performance)]
		private struct ComputeInstancesOffsetAndResizeInstancesArrayJob : global::Unity.Jobs.IJob
		{
			[global::Unity.Collections.ReadOnly]
			public global::Unity.Collections.NativeArray<int> instancesCount;

			[global::Unity.Collections.WriteOnly]
			public global::Unity.Collections.NativeArray<int> instancesOffset;

			public global::Unity.Collections.NativeList<global::UnityEngine.Rendering.InstanceHandle> instances;

			public void Execute()
			{
				int num = 0;
				for (int i = 0; i < instancesCount.Length; i++)
				{
					instancesOffset[i] = num;
					num += instancesCount[i];
				}
				instances.ResizeUninitialized(num);
			}
		}

		[global::Unity.Burst.BurstCompile(DisableSafetyChecks = true, OptimizeFor = global::Unity.Burst.OptimizeFor.Performance)]
		private struct QueryRendererGroupInstancesJob : global::Unity.Jobs.IJobParallelForBatch
		{
			public const int k_BatchSize = 128;

			[global::Unity.Collections.ReadOnly]
			public global::Unity.Collections.NativeParallelMultiHashMap<int, global::UnityEngine.Rendering.InstanceHandle> rendererGroupInstanceMultiHash;

			[global::Unity.Collections.LowLevel.Unsafe.NativeDisableContainerSafetyRestriction]
			[global::Unity.Burst.NoAlias]
			[global::Unity.Collections.ReadOnly]
			public global::Unity.Collections.NativeArray<global::UnityEngine.EntityId> rendererGroupIDs;

			[global::Unity.Collections.LowLevel.Unsafe.NativeDisableContainerSafetyRestriction]
			[global::Unity.Burst.NoAlias]
			[global::Unity.Collections.WriteOnly]
			public global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.InstanceHandle> instances;

			[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
			public global::Unity.Collections.LowLevel.Unsafe.UnsafeAtomicCounter32 atomicNonFoundInstancesCount;

			public unsafe void Execute(int startIndex, int count)
			{
				int num = 0;
				for (int i = startIndex; i < startIndex + count; i++)
				{
					if (rendererGroupInstanceMultiHash.TryGetFirstValue(rendererGroupIDs[i], out var item, out var _))
					{
						instances[i] = item;
						continue;
					}
					num++;
					instances[i] = global::UnityEngine.Rendering.InstanceHandle.Invalid;
				}
				if (atomicNonFoundInstancesCount.Counter != null && num > 0)
				{
					atomicNonFoundInstancesCount.Add(num);
				}
			}
		}

		[global::Unity.Burst.BurstCompile(DisableSafetyChecks = true, OptimizeFor = global::Unity.Burst.OptimizeFor.Performance)]
		private struct QueryRendererGroupInstancesMultiJob : global::Unity.Jobs.IJobParallelForBatch
		{
			public const int k_BatchSize = 128;

			[global::Unity.Collections.ReadOnly]
			public global::Unity.Collections.NativeParallelMultiHashMap<int, global::UnityEngine.Rendering.InstanceHandle> rendererGroupInstanceMultiHash;

			[global::Unity.Collections.LowLevel.Unsafe.NativeDisableContainerSafetyRestriction]
			[global::Unity.Burst.NoAlias]
			[global::Unity.Collections.ReadOnly]
			public global::Unity.Collections.NativeArray<global::UnityEngine.EntityId> rendererGroupIDs;

			[global::Unity.Collections.LowLevel.Unsafe.NativeDisableContainerSafetyRestriction]
			[global::Unity.Burst.NoAlias]
			[global::Unity.Collections.ReadOnly]
			public global::Unity.Collections.NativeArray<int> instancesOffsets;

			[global::Unity.Collections.LowLevel.Unsafe.NativeDisableContainerSafetyRestriction]
			[global::Unity.Burst.NoAlias]
			[global::Unity.Collections.ReadOnly]
			public global::Unity.Collections.NativeArray<int> instancesCounts;

			[global::Unity.Collections.LowLevel.Unsafe.NativeDisableContainerSafetyRestriction]
			[global::Unity.Burst.NoAlias]
			[global::Unity.Collections.WriteOnly]
			public global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.InstanceHandle> instances;

			[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
			public global::Unity.Collections.LowLevel.Unsafe.UnsafeAtomicCounter32 atomicNonFoundSharedInstancesCount;

			[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
			public global::Unity.Collections.LowLevel.Unsafe.UnsafeAtomicCounter32 atomicNonFoundInstancesCount;

			public unsafe void Execute(int startIndex, int count)
			{
				int num = 0;
				int num2 = 0;
				for (int i = startIndex; i < startIndex + count; i++)
				{
					global::UnityEngine.EntityId entityId = rendererGroupIDs[i];
					int num3 = instancesOffsets[i];
					int num4 = instancesCounts[i];
					global::UnityEngine.Rendering.InstanceHandle item;
					global::Unity.Collections.NativeParallelMultiHashMapIterator<int> it;
					bool flag = rendererGroupInstanceMultiHash.TryGetFirstValue(entityId, out item, out it);
					if (!flag)
					{
						num++;
					}
					for (int j = 0; j < num4; j++)
					{
						int index = num3 + j;
						if (flag)
						{
							instances[index] = item;
							flag = rendererGroupInstanceMultiHash.TryGetNextValue(out item, ref it);
						}
						else
						{
							num2++;
							instances[index] = global::UnityEngine.Rendering.InstanceHandle.Invalid;
						}
					}
				}
				if (atomicNonFoundSharedInstancesCount.Counter != null && num > 0)
				{
					atomicNonFoundSharedInstancesCount.Add(num);
				}
				if (atomicNonFoundInstancesCount.Counter != null && num2 > 0)
				{
					atomicNonFoundInstancesCount.Add(num2);
				}
			}
		}

		[global::Unity.Burst.BurstCompile(DisableSafetyChecks = true, OptimizeFor = global::Unity.Burst.OptimizeFor.Performance)]
		private struct QuerySortedMeshInstancesJob : global::Unity.Jobs.IJobParallelForBatch
		{
			public const int k_BatchSize = 64;

			[global::Unity.Collections.ReadOnly]
			public global::UnityEngine.Rendering.CPUInstanceData instanceData;

			[global::Unity.Collections.ReadOnly]
			public global::UnityEngine.Rendering.CPUSharedInstanceData sharedInstanceData;

			[global::Unity.Collections.ReadOnly]
			public global::Unity.Collections.NativeArray<global::UnityEngine.EntityId> sortedMeshID;

			[global::Unity.Collections.NativeDisableParallelForRestriction]
			[global::Unity.Collections.WriteOnly]
			public global::Unity.Collections.NativeList<global::UnityEngine.Rendering.InstanceHandle> instances;

			public void Execute(int startIndex, int count)
			{
				ulong num = 0uL;
				for (int i = 0; i < count; i++)
				{
					int index = startIndex + i;
					_ = instanceData.instances[index];
					global::UnityEngine.Rendering.SharedInstanceHandle instance = instanceData.sharedInstances[index];
					int num2 = sharedInstanceData.Get_MeshID(instance);
					if (global::Unity.Collections.NativeSortExtension.BinarySearch(sortedMeshID, num2) >= 0)
					{
						num |= (ulong)(1L << i);
					}
				}
				int num3 = global::Unity.Mathematics.math.countbits(num);
				if (num3 > 0)
				{
					int num4 = AtomicAddLengthNoResize(in instances, num3);
					int num5 = global::Unity.Mathematics.math.tzcnt(num);
					while (num != 0L)
					{
						int index2 = startIndex + num5;
						instances[num4] = instanceData.instances[index2];
						num4++;
						num &= (ulong)(~(1L << num5));
						num5 = global::Unity.Mathematics.math.tzcnt(num);
					}
				}
			}
		}

		[global::Unity.Burst.BurstCompile(DisableSafetyChecks = true, OptimizeFor = global::Unity.Burst.OptimizeFor.Performance)]
		private struct CalculateInterpolatedLightAndOcclusionProbesBatchJob : global::Unity.Jobs.IJobParallelFor
		{
			public const int k_BatchSize = 1;

			public const int k_CalculatedProbesPerBatch = 8;

			[global::Unity.Collections.ReadOnly]
			public int probesCount;

			[global::Unity.Collections.ReadOnly]
			public global::UnityEngine.LightProbesQuery lightProbesQuery;

			[global::Unity.Collections.NativeDisableParallelForRestriction]
			[global::Unity.Collections.ReadOnly]
			public global::Unity.Collections.NativeArray<global::UnityEngine.Vector3> queryPostitions;

			[global::Unity.Collections.NativeDisableParallelForRestriction]
			public global::Unity.Collections.NativeArray<int> compactTetrahedronCache;

			[global::Unity.Collections.NativeDisableParallelForRestriction]
			[global::Unity.Collections.WriteOnly]
			public global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.SphericalHarmonicsL2> probesSphericalHarmonics;

			[global::Unity.Collections.NativeDisableParallelForRestriction]
			[global::Unity.Collections.WriteOnly]
			public global::Unity.Collections.NativeArray<global::UnityEngine.Vector4> probesOcclusion;

			public void Execute(int index)
			{
				int num = index * 8;
				int length = global::Unity.Mathematics.math.min(probesCount, num + 8) - num;
				global::Unity.Collections.NativeArray<int> subArray = compactTetrahedronCache.GetSubArray(num, length);
				global::Unity.Collections.NativeArray<global::UnityEngine.Vector3> subArray2 = queryPostitions.GetSubArray(num, length);
				global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.SphericalHarmonicsL2> subArray3 = probesSphericalHarmonics.GetSubArray(num, length);
				global::Unity.Collections.NativeArray<global::UnityEngine.Vector4> subArray4 = probesOcclusion.GetSubArray(num, length);
				lightProbesQuery.CalculateInterpolatedLightAndOcclusionProbes(subArray2, subArray, subArray3, subArray4);
			}
		}

		[global::Unity.Burst.BurstCompile(DisableSafetyChecks = true, OptimizeFor = global::Unity.Burst.OptimizeFor.Performance)]
		private struct ScatterTetrahedronCacheIndicesJob : global::Unity.Jobs.IJobParallelFor
		{
			public const int k_BatchSize = 128;

			[global::Unity.Collections.ReadOnly]
			public global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.InstanceHandle> probeInstances;

			[global::Unity.Collections.ReadOnly]
			public global::Unity.Collections.NativeArray<int> compactTetrahedronCache;

			[global::Unity.Collections.LowLevel.Unsafe.NativeDisableContainerSafetyRestriction]
			[global::Unity.Burst.NoAlias]
			[global::Unity.Collections.NativeDisableParallelForRestriction]
			public global::UnityEngine.Rendering.CPUInstanceData instanceData;

			public void Execute(int index)
			{
				global::UnityEngine.Rendering.InstanceHandle instance = probeInstances[index];
				instanceData.Set_TetrahedronCacheIndex(instance, compactTetrahedronCache[index]);
			}
		}

		[global::Unity.Burst.BurstCompile(DisableSafetyChecks = true, OptimizeFor = global::Unity.Burst.OptimizeFor.Performance)]
		private struct TransformUpdateJob : global::Unity.Jobs.IJobParallelForBatch
		{
			public const int k_BatchSize = 64;

			[global::Unity.Collections.ReadOnly]
			public bool initialize;

			[global::Unity.Collections.ReadOnly]
			public bool enableBoundingSpheres;

			[global::Unity.Collections.ReadOnly]
			public global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.InstanceHandle> instances;

			[global::Unity.Collections.ReadOnly]
			public global::Unity.Collections.NativeArray<global::UnityEngine.Matrix4x4> localToWorldMatrices;

			[global::Unity.Collections.ReadOnly]
			public global::Unity.Collections.NativeArray<global::UnityEngine.Matrix4x4> prevLocalToWorldMatrices;

			[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
			public global::Unity.Collections.LowLevel.Unsafe.UnsafeAtomicCounter32 atomicTransformQueueCount;

			[global::Unity.Collections.NativeDisableParallelForRestriction]
			public global::UnityEngine.Rendering.CPUSharedInstanceData sharedInstanceData;

			[global::Unity.Collections.NativeDisableParallelForRestriction]
			public global::UnityEngine.Rendering.CPUInstanceData instanceData;

			[global::Unity.Collections.NativeDisableParallelForRestriction]
			public global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.InstanceHandle> transformUpdateInstanceQueue;

			[global::Unity.Collections.NativeDisableParallelForRestriction]
			public global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.TransformUpdatePacket> transformUpdateDataQueue;

			[global::Unity.Collections.NativeDisableParallelForRestriction]
			public global::Unity.Collections.NativeArray<global::Unity.Mathematics.float4> boundingSpheresDataQueue;

			public unsafe void Execute(int startIndex, int count)
			{
				ulong num = 0uL;
				for (int i = 0; i < count; i++)
				{
					global::UnityEngine.Rendering.InstanceHandle instance = instances[startIndex + i];
					if (!instance.valid)
					{
						continue;
					}
					if (!initialize)
					{
						int index = instanceData.InstanceToIndex(instance);
						int index2 = sharedInstanceData.InstanceToIndex(in instanceData, instance);
						global::UnityEngine.Rendering.TransformUpdateFlags transformUpdateFlags = sharedInstanceData.flags[index2].transformUpdateFlags;
						bool flag = instanceData.movedInCurrentFrameBits.Get(index);
						if ((transformUpdateFlags & global::UnityEngine.Rendering.TransformUpdateFlags.IsPartOfStaticBatch) != 0 || flag)
						{
							continue;
						}
					}
					num |= (ulong)(1L << i);
				}
				int num2 = global::Unity.Mathematics.math.countbits(num);
				if (num2 <= 0)
				{
					return;
				}
				int num3 = atomicTransformQueueCount.Add(num2);
				int num4 = global::Unity.Mathematics.math.tzcnt(num);
				while (num != 0L)
				{
					int index3 = startIndex + num4;
					global::UnityEngine.Rendering.InstanceHandle instanceHandle = instances[index3];
					int index4 = instanceData.InstanceToIndex(instanceHandle);
					int index5 = sharedInstanceData.InstanceToIndex(in instanceData, instanceHandle);
					bool flag2 = (sharedInstanceData.flags[index5].transformUpdateFlags & global::UnityEngine.Rendering.TransformUpdateFlags.IsPartOfStaticBatch) != 0;
					instanceData.movedInCurrentFrameBits.Set(index4, !flag2);
					transformUpdateInstanceQueue[num3] = instanceHandle;
					ref global::Unity.Mathematics.float4x4 reference = ref global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.ArrayElementAsRef<global::Unity.Mathematics.float4x4>(global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafeReadOnlyPtr(localToWorldMatrices), index3);
					ref global::UnityEngine.Rendering.AABB reference2 = ref global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.ArrayElementAsRef<global::UnityEngine.Rendering.AABB>(global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(sharedInstanceData.localAABBs), index5);
					global::UnityEngine.Rendering.AABB value = global::UnityEngine.Rendering.AABB.Transform(reference, reference2);
					instanceData.worldAABBs[index4] = value;
					if (initialize)
					{
						global::UnityEngine.Rendering.PackedMatrix packedMatrix = global::UnityEngine.Rendering.PackedMatrix.FromFloat4x4(in reference);
						global::UnityEngine.Rendering.PackedMatrix packedMatrix2 = global::UnityEngine.Rendering.PackedMatrix.FromMatrix4x4(prevLocalToWorldMatrices[index3]);
						transformUpdateDataQueue[num3 * 2] = new global::UnityEngine.Rendering.TransformUpdatePacket
						{
							localToWorld0 = packedMatrix.packed0,
							localToWorld1 = packedMatrix.packed1,
							localToWorld2 = packedMatrix.packed2
						};
						transformUpdateDataQueue[num3 * 2 + 1] = new global::UnityEngine.Rendering.TransformUpdatePacket
						{
							localToWorld0 = packedMatrix2.packed0,
							localToWorld1 = packedMatrix2.packed1,
							localToWorld2 = packedMatrix2.packed2
						};
					}
					else
					{
						global::UnityEngine.Rendering.PackedMatrix packedMatrix3 = global::UnityEngine.Rendering.PackedMatrix.FromMatrix4x4((global::UnityEngine.Matrix4x4)reference);
						transformUpdateDataQueue[num3] = new global::UnityEngine.Rendering.TransformUpdatePacket
						{
							localToWorld0 = packedMatrix3.packed0,
							localToWorld1 = packedMatrix3.packed1,
							localToWorld2 = packedMatrix3.packed2
						};
						float num5 = global::Unity.Mathematics.math.determinant((global::Unity.Mathematics.float3x3)reference);
						instanceData.localToWorldIsFlippedBits.Set(index4, num5 < 0f);
					}
					if (enableBoundingSpheres)
					{
						boundingSpheresDataQueue[num3] = new global::Unity.Mathematics.float4(value.center.x, value.center.y, value.center.z, global::Unity.Mathematics.math.distance(value.max, value.min) * 0.5f);
					}
					num3++;
					num &= (ulong)(~(1L << num4));
					num4 = global::Unity.Mathematics.math.tzcnt(num);
				}
			}
		}

		[global::Unity.Burst.BurstCompile(DisableSafetyChecks = true, OptimizeFor = global::Unity.Burst.OptimizeFor.Performance)]
		private struct ProbesUpdateJob : global::Unity.Jobs.IJobParallelForBatch
		{
			public const int k_BatchSize = 64;

			[global::Unity.Collections.LowLevel.Unsafe.NativeDisableContainerSafetyRestriction]
			[global::Unity.Burst.NoAlias]
			[global::Unity.Collections.ReadOnly]
			public global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.InstanceHandle> instances;

			[global::Unity.Collections.NativeDisableParallelForRestriction]
			[global::Unity.Collections.LowLevel.Unsafe.NativeDisableContainerSafetyRestriction]
			[global::Unity.Burst.NoAlias]
			public global::UnityEngine.Rendering.CPUInstanceData instanceData;

			[global::Unity.Collections.ReadOnly]
			public global::UnityEngine.Rendering.CPUSharedInstanceData sharedInstanceData;

			[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
			public global::Unity.Collections.LowLevel.Unsafe.UnsafeAtomicCounter32 atomicProbesQueueCount;

			[global::Unity.Collections.NativeDisableParallelForRestriction]
			public global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.InstanceHandle> probeInstanceQueue;

			[global::Unity.Collections.NativeDisableParallelForRestriction]
			public global::Unity.Collections.NativeArray<int> compactTetrahedronCache;

			[global::Unity.Collections.NativeDisableParallelForRestriction]
			public global::Unity.Collections.NativeArray<global::UnityEngine.Vector3> probeQueryPosition;

			public unsafe void Execute(int startIndex, int count)
			{
				ulong num = 0uL;
				for (int i = 0; i < count; i++)
				{
					global::UnityEngine.Rendering.InstanceHandle instance = instances[startIndex + i];
					if (instance.valid)
					{
						int index = sharedInstanceData.InstanceToIndex(in instanceData, instance);
						if ((sharedInstanceData.flags[index].transformUpdateFlags & global::UnityEngine.Rendering.TransformUpdateFlags.HasLightProbeCombined) != global::UnityEngine.Rendering.TransformUpdateFlags.None)
						{
							num |= (ulong)(1L << i);
						}
					}
				}
				int num2 = global::Unity.Mathematics.math.countbits(num);
				if (num2 > 0)
				{
					int num3 = atomicProbesQueueCount.Add(num2);
					int num4 = global::Unity.Mathematics.math.tzcnt(num);
					while (num != 0L)
					{
						global::UnityEngine.Rendering.InstanceHandle instanceHandle = instances[startIndex + num4];
						int index2 = instanceData.InstanceToIndex(instanceHandle);
						ref global::UnityEngine.Rendering.AABB reference = ref global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.ArrayElementAsRef<global::UnityEngine.Rendering.AABB>(global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(instanceData.worldAABBs), index2);
						probeInstanceQueue[num3] = instanceHandle;
						probeQueryPosition[num3] = reference.center;
						compactTetrahedronCache[num3] = instanceData.tetrahedronCacheIndices[index2];
						num3++;
						num &= (ulong)(~(1L << num4));
						num4 = global::Unity.Mathematics.math.tzcnt(num);
					}
				}
			}
		}

		[global::Unity.Burst.BurstCompile(DisableSafetyChecks = true, OptimizeFor = global::Unity.Burst.OptimizeFor.Performance)]
		private struct MotionUpdateJob : global::Unity.Jobs.IJobParallelFor
		{
			public const int k_BatchSize = 16;

			[global::Unity.Collections.ReadOnly]
			public int queueWriteBase;

			[global::Unity.Collections.NativeDisableParallelForRestriction]
			public global::UnityEngine.Rendering.CPUInstanceData instanceData;

			[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
			public global::Unity.Collections.LowLevel.Unsafe.UnsafeAtomicCounter32 atomicUpdateQueueCount;

			[global::Unity.Collections.NativeDisableParallelForRestriction]
			[global::Unity.Collections.WriteOnly]
			public global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.InstanceHandle> transformUpdateInstanceQueue;

			public void Execute(int chunk_index)
			{
				int num = global::Unity.Mathematics.math.min(instanceData.instancesLength - 64 * chunk_index, 64);
				ulong num2 = ulong.MaxValue >> 64 - num;
				ulong num3 = instanceData.movedInCurrentFrameBits.GetChunk(chunk_index) & num2;
				ulong num4 = instanceData.movedInPreviousFrameBits.GetChunk(chunk_index) & num2;
				instanceData.movedInCurrentFrameBits.SetChunk(chunk_index, 0uL);
				instanceData.movedInPreviousFrameBits.SetChunk(chunk_index, num3);
				ulong num5 = num4 & ~num3;
				int num6 = global::Unity.Mathematics.math.countbits(num5);
				int num7 = queueWriteBase;
				if (num6 > 0)
				{
					num7 += atomicUpdateQueueCount.Add(num6);
				}
				for (int num8 = global::Unity.Mathematics.math.tzcnt(num5); num8 < 64; num8 = global::Unity.Mathematics.math.tzcnt(num5))
				{
					int index = 64 * chunk_index + num8;
					transformUpdateInstanceQueue[num7] = instanceData.IndexToInstance(index);
					num7++;
					num5 &= (ulong)(~(1L << num8));
				}
			}
		}

		[global::Unity.Burst.BurstCompile(DisableSafetyChecks = true, OptimizeFor = global::Unity.Burst.OptimizeFor.Performance)]
		private struct UpdateRendererInstancesJob : global::Unity.Jobs.IJobParallelFor
		{
			public const int k_BatchSize = 128;

			[global::Unity.Collections.ReadOnly]
			public bool implicitInstanceIndices;

			[global::Unity.Collections.ReadOnly]
			public global::UnityEngine.Rendering.GPUDrivenRendererGroupData rendererData;

			[global::Unity.Collections.ReadOnly]
			public global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.InstanceHandle> instances;

			[global::Unity.Collections.ReadOnly]
			public global::Unity.Collections.NativeParallelHashMap<int, global::UnityEngine.Rendering.GPUInstanceIndex> lodGroupDataMap;

			[global::Unity.Collections.NativeDisableParallelForRestriction]
			[global::Unity.Collections.LowLevel.Unsafe.NativeDisableContainerSafetyRestriction]
			[global::Unity.Burst.NoAlias]
			public global::UnityEngine.Rendering.CPUInstanceData instanceData;

			[global::Unity.Collections.NativeDisableParallelForRestriction]
			[global::Unity.Collections.LowLevel.Unsafe.NativeDisableContainerSafetyRestriction]
			[global::Unity.Burst.NoAlias]
			public global::UnityEngine.Rendering.CPUSharedInstanceData sharedInstanceData;

			[global::Unity.Collections.NativeDisableParallelForRestriction]
			[global::Unity.Collections.LowLevel.Unsafe.NativeDisableContainerSafetyRestriction]
			[global::Unity.Burst.NoAlias]
			public global::UnityEngine.Rendering.CPUPerCameraInstanceData perCameraInstanceData;

			public unsafe void Execute(int index)
			{
				global::UnityEngine.EntityId rendererGroupID = rendererData.rendererGroupID[index];
				int index2 = rendererData.meshIndex[index];
				global::UnityEngine.Rendering.GPUDrivenPackedRendererData gPUDrivenPackedRendererData = rendererData.packedRendererData[index];
				global::UnityEngine.EntityId entityId = rendererData.lodGroupID[index];
				int gameObjectLayer = rendererData.gameObjectLayer[index];
				int num = rendererData.lightmapIndex[index];
				global::UnityEngine.Rendering.AABB localAABB = rendererData.localBounds[index].ToAABB();
				int num2 = rendererData.materialsOffset[index];
				int num3 = rendererData.materialsCount[index];
				int meshID = rendererData.meshID[index2];
				global::UnityEngine.Rendering.GPUDrivenMeshLodInfo meshLodInfo = rendererData.meshLodInfo[index2];
				global::UnityEngine.Rendering.InstanceFlags instanceFlags = global::UnityEngine.Rendering.InstanceFlags.None;
				global::UnityEngine.Rendering.TransformUpdateFlags transformUpdateFlags = global::UnityEngine.Rendering.TransformUpdateFlags.None;
				int num4 = num & 0xFFFF;
				if (num4 >= 65534 && gPUDrivenPackedRendererData.lightProbeUsage == global::UnityEngine.Rendering.LightProbeUsage.BlendProbes)
				{
					transformUpdateFlags |= global::UnityEngine.Rendering.TransformUpdateFlags.HasLightProbeCombined;
				}
				if (gPUDrivenPackedRendererData.isPartOfStaticBatch)
				{
					transformUpdateFlags |= global::UnityEngine.Rendering.TransformUpdateFlags.IsPartOfStaticBatch;
				}
				switch (gPUDrivenPackedRendererData.shadowCastingMode)
				{
				case global::UnityEngine.Rendering.ShadowCastingMode.Off:
					instanceFlags |= global::UnityEngine.Rendering.InstanceFlags.IsShadowsOff;
					break;
				case global::UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly:
					instanceFlags |= global::UnityEngine.Rendering.InstanceFlags.IsShadowsOnly;
					break;
				}
				if (meshLodInfo.lodSelectionActive)
				{
					instanceFlags |= global::UnityEngine.Rendering.InstanceFlags.HasMeshLod;
				}
				if (num4 != 65535)
				{
					instanceFlags |= global::UnityEngine.Rendering.InstanceFlags.AffectsLightmaps;
				}
				if (gPUDrivenPackedRendererData.smallMeshCulling)
				{
					instanceFlags |= global::UnityEngine.Rendering.InstanceFlags.SmallMeshCulling;
				}
				uint lodGroupAndMask = uint.MaxValue;
				if (lodGroupDataMap.TryGetValue(entityId, out var item) && gPUDrivenPackedRendererData.lodMask > 0)
				{
					lodGroupAndMask = (uint)((item.index << 8) | gPUDrivenPackedRendererData.lodMask);
				}
				int num5;
				int num6;
				if (implicitInstanceIndices)
				{
					num5 = 1;
					num6 = index;
				}
				else
				{
					num5 = rendererData.instancesCount[index];
					num6 = rendererData.instancesOffset[index];
				}
				if (num5 > 0)
				{
					global::UnityEngine.Rendering.InstanceHandle instance = instances[num6];
					global::UnityEngine.Rendering.SharedInstanceHandle instance2 = instanceData.Get_SharedInstance(instance);
					global::UnityEngine.Rendering.SmallEntityIdArray materialIDs = new global::UnityEngine.Rendering.SmallEntityIdArray(num3, global::Unity.Collections.Allocator.Persistent);
					for (int i = 0; i < num3; i++)
					{
						int index3 = rendererData.materialIndex[num2 + i];
						global::UnityEngine.EntityId value = rendererData.materialID[index3];
						materialIDs[i] = value;
					}
					sharedInstanceData.Set(instance2, rendererGroupID, in materialIDs, meshID, in localAABB, transformUpdateFlags, instanceFlags, lodGroupAndMask, meshLodInfo, gameObjectLayer, sharedInstanceData.Get_RefCount(instance2));
					for (int j = 0; j < num5; j++)
					{
						int index4 = num6 + j;
						ref global::UnityEngine.Matrix4x4 reference = ref global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.ArrayElementAsRef<global::UnityEngine.Matrix4x4>(global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafeReadOnlyPtr(rendererData.localToWorldMatrix), index4);
						global::UnityEngine.Rendering.AABB value2 = global::UnityEngine.Rendering.AABB.Transform(reference, localAABB);
						instance = instances[index4];
						bool value3 = global::Unity.Mathematics.math.determinant((global::Unity.Mathematics.float3x3)reference) < 0f;
						int num7 = instanceData.InstanceToIndex(instance);
						perCameraInstanceData.SetDefault(num7);
						instanceData.localToWorldIsFlippedBits.Set(num7, value3);
						instanceData.worldAABBs[num7] = value2;
						instanceData.tetrahedronCacheIndices[num7] = -1;
						instanceData.meshLodData[num7] = rendererData.meshLodData[index];
					}
				}
			}
		}

		[global::Unity.Burst.BurstCompile(DisableSafetyChecks = true, OptimizeFor = global::Unity.Burst.OptimizeFor.Performance)]
		private struct CollectInstancesLODGroupsAndMasksJob : global::Unity.Jobs.IJobParallelFor
		{
			public const int k_BatchSize = 128;

			[global::Unity.Collections.ReadOnly]
			public global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.InstanceHandle> instances;

			[global::Unity.Collections.ReadOnly]
			public global::UnityEngine.Rendering.CPUInstanceData.ReadOnly instanceData;

			[global::Unity.Collections.ReadOnly]
			public global::UnityEngine.Rendering.CPUSharedInstanceData.ReadOnly sharedInstanceData;

			[global::Unity.Collections.WriteOnly]
			public global::Unity.Collections.NativeArray<uint> lodGroupAndMasks;

			public void Execute(int index)
			{
				global::UnityEngine.Rendering.InstanceHandle instance = instances[index];
				int index2 = sharedInstanceData.InstanceToIndex(in instanceData, instance);
				lodGroupAndMasks[index] = sharedInstanceData.lodGroupAndMasks[index2];
			}
		}

		[global::Unity.Burst.BurstCompile(DisableSafetyChecks = true, OptimizeFor = global::Unity.Burst.OptimizeFor.Performance)]
		private struct GetVisibleNonProcessedTreeInstancesJob : global::Unity.Jobs.IJobParallelForBatch
		{
			public const int k_BatchSize = 64;

			[global::Unity.Collections.ReadOnly]
			public global::UnityEngine.Rendering.CPUInstanceData instanceData;

			[global::Unity.Collections.ReadOnly]
			public global::UnityEngine.Rendering.CPUSharedInstanceData sharedInstanceData;

			[global::Unity.Collections.ReadOnly]
			[global::Unity.Collections.LowLevel.Unsafe.NativeDisableContainerSafetyRestriction]
			[global::Unity.Burst.NoAlias]
			public global::UnityEngine.Rendering.ParallelBitArray compactedVisibilityMasks;

			[global::Unity.Collections.ReadOnly]
			public bool becomeVisible;

			[global::Unity.Collections.NativeDisableParallelForRestriction]
			public global::UnityEngine.Rendering.ParallelBitArray processedBits;

			[global::Unity.Collections.NativeDisableParallelForRestriction]
			[global::Unity.Collections.WriteOnly]
			public global::Unity.Collections.NativeArray<int> rendererIDs;

			[global::Unity.Collections.NativeDisableParallelForRestriction]
			[global::Unity.Collections.WriteOnly]
			public global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.InstanceHandle> instances;

			[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
			public global::Unity.Collections.LowLevel.Unsafe.UnsafeAtomicCounter32 atomicTreeInstancesCount;

			public void Execute(int startIndex, int count)
			{
				int chunk_index = startIndex / 64;
				ulong chunk = instanceData.visibleInPreviousFrameBits.GetChunk(chunk_index);
				ulong chunk2 = processedBits.GetChunk(chunk_index);
				ulong num = 0uL;
				for (int i = 0; i < count; i++)
				{
					int index = startIndex + i;
					global::UnityEngine.Rendering.InstanceHandle instanceHandle = instanceData.IndexToInstance(index);
					if (instanceHandle.type != global::UnityEngine.Rendering.InstanceType.SpeedTree || !compactedVisibilityMasks.Get(instanceHandle.index))
					{
						continue;
					}
					ulong num2 = (ulong)(1L << i);
					if ((chunk2 & num2) != 0)
					{
						continue;
					}
					bool flag = (chunk & num2) != 0;
					if (becomeVisible)
					{
						if (!flag)
						{
							num |= num2;
						}
					}
					else if (flag)
					{
						num |= num2;
					}
				}
				int num3 = global::Unity.Mathematics.math.countbits(num);
				if (num3 > 0)
				{
					processedBits.SetChunk(chunk_index, chunk2 | num);
					int num4 = atomicTreeInstancesCount.Add(num3);
					int num5 = global::Unity.Mathematics.math.tzcnt(num);
					while (num != 0L)
					{
						int index2 = startIndex + num5;
						global::UnityEngine.Rendering.InstanceHandle instanceHandle2 = instanceData.IndexToInstance(index2);
						global::UnityEngine.Rendering.SharedInstanceHandle instance = instanceData.Get_SharedInstance(instanceHandle2);
						int value = sharedInstanceData.Get_RendererGroupID(instance);
						rendererIDs[num4] = value;
						instances[num4] = instanceHandle2;
						num4++;
						num &= (ulong)(~(1L << num5));
						num5 = global::Unity.Mathematics.math.tzcnt(num);
					}
				}
			}
		}

		[global::Unity.Burst.BurstCompile(DisableSafetyChecks = true, OptimizeFor = global::Unity.Burst.OptimizeFor.Performance)]
		private struct UpdateCompactedInstanceVisibilityJob : global::Unity.Jobs.IJobParallelForBatch
		{
			public const int k_BatchSize = 64;

			[global::Unity.Collections.ReadOnly]
			public global::UnityEngine.Rendering.ParallelBitArray compactedVisibilityMasks;

			[global::Unity.Collections.LowLevel.Unsafe.NativeDisableContainerSafetyRestriction]
			[global::Unity.Burst.NoAlias]
			[global::Unity.Collections.NativeDisableParallelForRestriction]
			public global::UnityEngine.Rendering.CPUInstanceData instanceData;

			public void Execute(int startIndex, int count)
			{
				ulong num = 0uL;
				for (int i = 0; i < count; i++)
				{
					int index = startIndex + i;
					global::UnityEngine.Rendering.InstanceHandle instanceHandle = instanceData.IndexToInstance(index);
					if (compactedVisibilityMasks.Get(instanceHandle.index))
					{
						num |= (ulong)(1L << i);
					}
				}
				instanceData.visibleInPreviousFrameBits.SetChunk(startIndex / 64, num);
			}
		}

		private global::UnityEngine.Rendering.InstanceAllocators m_InstanceAllocators;

		private global::UnityEngine.Rendering.CPUSharedInstanceData m_SharedInstanceData;

		private global::UnityEngine.Rendering.CPUInstanceData m_InstanceData;

		private global::UnityEngine.Rendering.CPUPerCameraInstanceData m_PerCameraInstanceData;

		private global::Unity.Collections.NativeParallelMultiHashMap<int, global::UnityEngine.Rendering.InstanceHandle> m_RendererGroupInstanceMultiHash;

		private global::UnityEngine.ComputeShader m_TransformUpdateCS;

		private global::UnityEngine.ComputeShader m_WindDataUpdateCS;

		private int m_TransformInitKernel;

		private int m_TransformUpdateKernel;

		private int m_MotionUpdateKernel;

		private int m_ProbeUpdateKernel;

		private int m_LODUpdateKernel;

		private int m_WindDataCopyHistoryKernel;

		private global::UnityEngine.ComputeBuffer m_UpdateIndexQueueBuffer;

		private global::UnityEngine.ComputeBuffer m_ProbeUpdateDataQueueBuffer;

		private global::UnityEngine.ComputeBuffer m_ProbeOcclusionUpdateDataQueueBuffer;

		private global::UnityEngine.ComputeBuffer m_TransformUpdateDataQueueBuffer;

		private global::UnityEngine.ComputeBuffer m_BoundingSpheresUpdateDataQueueBuffer;

		private bool m_EnableBoundingSpheres;

		private readonly int[] m_ScratchWindParamAddressArray = new int[64];

		public bool hasBoundingSpheres => m_EnableBoundingSpheres;

		public global::UnityEngine.Rendering.CPUInstanceData.ReadOnly instanceData => m_InstanceData.AsReadOnly();

		public global::UnityEngine.Rendering.CPUPerCameraInstanceData perCameraInstanceData => m_PerCameraInstanceData;

		public int cameraCount => m_PerCameraInstanceData.cameraCount;

		public global::UnityEngine.Rendering.CPUSharedInstanceData.ReadOnly sharedInstanceData => m_SharedInstanceData.AsReadOnly();

		public global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.InstanceHandle> aliveInstances => m_InstanceData.instances.GetSubArray(0, m_InstanceData.instancesLength);

		public InstanceDataSystem(int maxInstances, bool enableBoundingSpheres, global::UnityEngine.Rendering.GPUResidentDrawerResources resources)
		{
			m_InstanceAllocators = default(global::UnityEngine.Rendering.InstanceAllocators);
			m_SharedInstanceData = default(global::UnityEngine.Rendering.CPUSharedInstanceData);
			m_InstanceData = default(global::UnityEngine.Rendering.CPUInstanceData);
			m_PerCameraInstanceData = default(global::UnityEngine.Rendering.CPUPerCameraInstanceData);
			m_InstanceAllocators.Initialize();
			m_SharedInstanceData.Initialize(maxInstances);
			m_InstanceData.Initialize(maxInstances);
			m_PerCameraInstanceData.Initialize(maxInstances);
			m_RendererGroupInstanceMultiHash = new global::Unity.Collections.NativeParallelMultiHashMap<int, global::UnityEngine.Rendering.InstanceHandle>(maxInstances, global::Unity.Collections.Allocator.Persistent);
			m_TransformUpdateCS = resources.transformUpdaterKernels;
			m_WindDataUpdateCS = resources.windDataUpdaterKernels;
			m_TransformInitKernel = m_TransformUpdateCS.FindKernel("ScatterInitTransformMain");
			m_TransformUpdateKernel = m_TransformUpdateCS.FindKernel("ScatterUpdateTransformMain");
			m_MotionUpdateKernel = m_TransformUpdateCS.FindKernel("ScatterUpdateMotionMain");
			m_ProbeUpdateKernel = m_TransformUpdateCS.FindKernel("ScatterUpdateProbesMain");
			if (enableBoundingSpheres)
			{
				m_TransformUpdateCS.EnableKeyword("PROCESS_BOUNDING_SPHERES");
			}
			else
			{
				m_TransformUpdateCS.DisableKeyword("PROCESS_BOUNDING_SPHERES");
			}
			m_WindDataCopyHistoryKernel = m_WindDataUpdateCS.FindKernel("WindDataCopyHistoryMain");
			m_EnableBoundingSpheres = enableBoundingSpheres;
		}

		public void Dispose()
		{
			m_InstanceAllocators.Dispose();
			m_SharedInstanceData.Dispose();
			m_InstanceData.Dispose();
			m_PerCameraInstanceData.Dispose();
			m_RendererGroupInstanceMultiHash.Dispose();
			m_UpdateIndexQueueBuffer?.Dispose();
			m_ProbeUpdateDataQueueBuffer?.Dispose();
			m_ProbeOcclusionUpdateDataQueueBuffer?.Dispose();
			m_TransformUpdateDataQueueBuffer?.Dispose();
			m_BoundingSpheresUpdateDataQueueBuffer?.Dispose();
		}

		public int GetMaxInstancesOfType(global::UnityEngine.Rendering.InstanceType instanceType)
		{
			return m_InstanceAllocators.GetInstanceHandlesLength(instanceType);
		}

		public int GetAliveInstancesOfType(global::UnityEngine.Rendering.InstanceType instanceType)
		{
			return m_InstanceAllocators.GetInstancesLength(instanceType);
		}

		private void EnsureIndexQueueBufferCapacity(int capacity)
		{
			if (m_UpdateIndexQueueBuffer == null || m_UpdateIndexQueueBuffer.count < capacity)
			{
				m_UpdateIndexQueueBuffer?.Dispose();
				m_UpdateIndexQueueBuffer = new global::UnityEngine.ComputeBuffer(capacity, 4, global::UnityEngine.ComputeBufferType.Raw);
			}
		}

		private void EnsureProbeBuffersCapacity(int capacity)
		{
			EnsureIndexQueueBufferCapacity(capacity);
			if (m_ProbeUpdateDataQueueBuffer == null || m_ProbeUpdateDataQueueBuffer.count < capacity)
			{
				m_ProbeUpdateDataQueueBuffer?.Dispose();
				m_ProbeOcclusionUpdateDataQueueBuffer?.Dispose();
				m_ProbeUpdateDataQueueBuffer = new global::UnityEngine.ComputeBuffer(capacity, global::System.Runtime.InteropServices.Marshal.SizeOf<global::UnityEngine.Rendering.SHUpdatePacket>(), global::UnityEngine.ComputeBufferType.Structured);
				m_ProbeOcclusionUpdateDataQueueBuffer = new global::UnityEngine.ComputeBuffer(capacity, global::System.Runtime.InteropServices.Marshal.SizeOf<global::UnityEngine.Vector4>(), global::UnityEngine.ComputeBufferType.Structured);
			}
		}

		private void EnsureTransformBuffersCapacity(int capacity)
		{
			EnsureIndexQueueBufferCapacity(capacity);
			int num = capacity * 2;
			if (m_TransformUpdateDataQueueBuffer == null || m_TransformUpdateDataQueueBuffer.count < num)
			{
				m_TransformUpdateDataQueueBuffer?.Dispose();
				m_BoundingSpheresUpdateDataQueueBuffer?.Dispose();
				m_TransformUpdateDataQueueBuffer = new global::UnityEngine.ComputeBuffer(num, global::System.Runtime.InteropServices.Marshal.SizeOf<global::UnityEngine.Rendering.TransformUpdatePacket>(), global::UnityEngine.ComputeBufferType.Structured);
				if (m_EnableBoundingSpheres)
				{
					m_BoundingSpheresUpdateDataQueueBuffer = new global::UnityEngine.ComputeBuffer(capacity, global::System.Runtime.InteropServices.Marshal.SizeOf<global::Unity.Mathematics.float4>(), global::UnityEngine.ComputeBufferType.Structured);
				}
			}
		}

		private global::Unity.Jobs.JobHandle ScheduleInterpolateProbesAndUpdateTetrahedronCache(int queueCount, global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.InstanceHandle> probeUpdateInstanceQueue, global::Unity.Collections.NativeArray<int> compactTetrahedronCache, global::Unity.Collections.NativeArray<global::UnityEngine.Vector3> probeQueryPosition, global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.SphericalHarmonicsL2> probeUpdateDataQueue, global::Unity.Collections.NativeArray<global::UnityEngine.Vector4> probeOcclusionUpdateDataQueue)
		{
			global::UnityEngine.LightProbesQuery lightProbesQuery = new global::UnityEngine.LightProbesQuery(global::Unity.Collections.Allocator.TempJob);
			global::UnityEngine.Rendering.InstanceDataSystem.CalculateInterpolatedLightAndOcclusionProbesBatchJob jobData = new global::UnityEngine.Rendering.InstanceDataSystem.CalculateInterpolatedLightAndOcclusionProbesBatchJob
			{
				lightProbesQuery = lightProbesQuery,
				probesCount = queueCount,
				queryPostitions = probeQueryPosition,
				compactTetrahedronCache = compactTetrahedronCache,
				probesSphericalHarmonics = probeUpdateDataQueue,
				probesOcclusion = probeOcclusionUpdateDataQueue
			};
			int arrayLength = 1 + queueCount / 8;
			global::Unity.Jobs.JobHandle jobHandle = global::Unity.Jobs.IJobParallelForExtensions.Schedule(jobData, arrayLength, 1);
			lightProbesQuery.Dispose(jobHandle);
			return global::Unity.Jobs.IJobParallelForExtensions.Schedule(new global::UnityEngine.Rendering.InstanceDataSystem.ScatterTetrahedronCacheIndicesJob
			{
				compactTetrahedronCache = compactTetrahedronCache,
				probeInstances = probeUpdateInstanceQueue,
				instanceData = m_InstanceData
			}, queueCount, 128, jobHandle);
		}

		private void DispatchProbeUpdateCommand(int queueCount, global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.InstanceHandle> probeInstanceQueue, global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.SphericalHarmonicsL2> probeUpdateDataQueue, global::Unity.Collections.NativeArray<global::UnityEngine.Vector4> probeOcclusionUpdateDataQueue, global::UnityEngine.Rendering.RenderersParameters renderersParameters, global::UnityEngine.Rendering.GPUInstanceDataBuffer outputBuffer)
		{
			EnsureProbeBuffersCapacity(queueCount);
			global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.GPUInstanceIndex> nativeArray = new global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.GPUInstanceIndex>(queueCount, global::Unity.Collections.Allocator.TempJob, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			outputBuffer.CPUInstanceArrayToGPUInstanceArray(probeInstanceQueue.GetSubArray(0, queueCount), nativeArray);
			m_UpdateIndexQueueBuffer.SetData(nativeArray, 0, 0, queueCount);
			m_ProbeUpdateDataQueueBuffer.SetData(probeUpdateDataQueue, 0, 0, queueCount);
			m_ProbeOcclusionUpdateDataQueueBuffer.SetData(probeOcclusionUpdateDataQueue, 0, 0, queueCount);
			m_TransformUpdateCS.SetInt(global::UnityEngine.Rendering.InstanceDataSystem.InstanceTransformUpdateIDs._ProbeUpdateQueueCount, queueCount);
			m_TransformUpdateCS.SetInt(global::UnityEngine.Rendering.InstanceDataSystem.InstanceTransformUpdateIDs._SHUpdateVec4Offset, renderersParameters.shCoefficients.uintOffset);
			m_TransformUpdateCS.SetBuffer(m_ProbeUpdateKernel, global::UnityEngine.Rendering.InstanceDataSystem.InstanceTransformUpdateIDs._ProbeUpdateIndexQueue, m_UpdateIndexQueueBuffer);
			m_TransformUpdateCS.SetBuffer(m_ProbeUpdateKernel, global::UnityEngine.Rendering.InstanceDataSystem.InstanceTransformUpdateIDs._ProbeUpdateDataQueue, m_ProbeUpdateDataQueueBuffer);
			m_TransformUpdateCS.SetBuffer(m_ProbeUpdateKernel, global::UnityEngine.Rendering.InstanceDataSystem.InstanceTransformUpdateIDs._ProbeOcclusionUpdateDataQueue, m_ProbeOcclusionUpdateDataQueueBuffer);
			m_TransformUpdateCS.SetBuffer(m_ProbeUpdateKernel, global::UnityEngine.Rendering.InstanceDataSystem.InstanceTransformUpdateIDs._OutputProbeBuffer, outputBuffer.gpuBuffer);
			m_TransformUpdateCS.Dispatch(m_ProbeUpdateKernel, (queueCount + 63) / 64, 1, 1);
			nativeArray.Dispose();
		}

		private void DispatchMotionUpdateCommand(int motionQueueCount, global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.InstanceHandle> transformInstanceQueue, global::UnityEngine.Rendering.RenderersParameters renderersParameters, global::UnityEngine.Rendering.GPUInstanceDataBuffer outputBuffer)
		{
			EnsureTransformBuffersCapacity(motionQueueCount);
			global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.GPUInstanceIndex> nativeArray = new global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.GPUInstanceIndex>(motionQueueCount, global::Unity.Collections.Allocator.TempJob, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			outputBuffer.CPUInstanceArrayToGPUInstanceArray(transformInstanceQueue.GetSubArray(0, motionQueueCount), nativeArray);
			m_UpdateIndexQueueBuffer.SetData(nativeArray, 0, 0, motionQueueCount);
			m_TransformUpdateCS.SetInt(global::UnityEngine.Rendering.InstanceDataSystem.InstanceTransformUpdateIDs._TransformUpdateQueueCount, motionQueueCount);
			m_TransformUpdateCS.SetInt(global::UnityEngine.Rendering.InstanceDataSystem.InstanceTransformUpdateIDs._TransformUpdateOutputL2WVec4Offset, renderersParameters.localToWorld.uintOffset);
			m_TransformUpdateCS.SetInt(global::UnityEngine.Rendering.InstanceDataSystem.InstanceTransformUpdateIDs._TransformUpdateOutputW2LVec4Offset, renderersParameters.worldToLocal.uintOffset);
			m_TransformUpdateCS.SetInt(global::UnityEngine.Rendering.InstanceDataSystem.InstanceTransformUpdateIDs._TransformUpdateOutputPrevL2WVec4Offset, renderersParameters.matrixPreviousM.uintOffset);
			m_TransformUpdateCS.SetInt(global::UnityEngine.Rendering.InstanceDataSystem.InstanceTransformUpdateIDs._TransformUpdateOutputPrevW2LVec4Offset, renderersParameters.matrixPreviousMI.uintOffset);
			m_TransformUpdateCS.SetBuffer(m_MotionUpdateKernel, global::UnityEngine.Rendering.InstanceDataSystem.InstanceTransformUpdateIDs._TransformUpdateIndexQueue, m_UpdateIndexQueueBuffer);
			m_TransformUpdateCS.SetBuffer(m_MotionUpdateKernel, global::UnityEngine.Rendering.InstanceDataSystem.InstanceTransformUpdateIDs._OutputTransformBuffer, outputBuffer.gpuBuffer);
			m_TransformUpdateCS.Dispatch(m_MotionUpdateKernel, (motionQueueCount + 63) / 64, 1, 1);
			nativeArray.Dispose();
		}

		private void DispatchTransformUpdateCommand(bool initialize, int transformQueueCount, global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.InstanceHandle> transformInstanceQueue, global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.TransformUpdatePacket> updateDataQueue, global::Unity.Collections.NativeArray<global::Unity.Mathematics.float4> boundingSphereUpdateDataQueue, global::UnityEngine.Rendering.RenderersParameters renderersParameters, global::UnityEngine.Rendering.GPUInstanceDataBuffer outputBuffer)
		{
			EnsureTransformBuffersCapacity(transformQueueCount);
			int count;
			int kernelIndex;
			if (initialize)
			{
				count = transformQueueCount * 2;
				kernelIndex = m_TransformInitKernel;
			}
			else
			{
				count = transformQueueCount;
				kernelIndex = m_TransformUpdateKernel;
			}
			global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.GPUInstanceIndex> nativeArray = new global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.GPUInstanceIndex>(transformQueueCount, global::Unity.Collections.Allocator.TempJob, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			outputBuffer.CPUInstanceArrayToGPUInstanceArray(transformInstanceQueue.GetSubArray(0, transformQueueCount), nativeArray);
			m_UpdateIndexQueueBuffer.SetData(nativeArray, 0, 0, transformQueueCount);
			m_TransformUpdateDataQueueBuffer.SetData(updateDataQueue, 0, 0, count);
			if (m_EnableBoundingSpheres)
			{
				m_BoundingSpheresUpdateDataQueueBuffer.SetData(boundingSphereUpdateDataQueue, 0, 0, transformQueueCount);
			}
			m_TransformUpdateCS.SetInt(global::UnityEngine.Rendering.InstanceDataSystem.InstanceTransformUpdateIDs._TransformUpdateQueueCount, transformQueueCount);
			m_TransformUpdateCS.SetInt(global::UnityEngine.Rendering.InstanceDataSystem.InstanceTransformUpdateIDs._TransformUpdateOutputL2WVec4Offset, renderersParameters.localToWorld.uintOffset);
			m_TransformUpdateCS.SetInt(global::UnityEngine.Rendering.InstanceDataSystem.InstanceTransformUpdateIDs._TransformUpdateOutputW2LVec4Offset, renderersParameters.worldToLocal.uintOffset);
			m_TransformUpdateCS.SetInt(global::UnityEngine.Rendering.InstanceDataSystem.InstanceTransformUpdateIDs._TransformUpdateOutputPrevL2WVec4Offset, renderersParameters.matrixPreviousM.uintOffset);
			m_TransformUpdateCS.SetInt(global::UnityEngine.Rendering.InstanceDataSystem.InstanceTransformUpdateIDs._TransformUpdateOutputPrevW2LVec4Offset, renderersParameters.matrixPreviousMI.uintOffset);
			m_TransformUpdateCS.SetBuffer(kernelIndex, global::UnityEngine.Rendering.InstanceDataSystem.InstanceTransformUpdateIDs._TransformUpdateIndexQueue, m_UpdateIndexQueueBuffer);
			m_TransformUpdateCS.SetBuffer(kernelIndex, global::UnityEngine.Rendering.InstanceDataSystem.InstanceTransformUpdateIDs._TransformUpdateDataQueue, m_TransformUpdateDataQueueBuffer);
			if (m_EnableBoundingSpheres)
			{
				m_TransformUpdateCS.SetInt(global::UnityEngine.Rendering.InstanceDataSystem.InstanceTransformUpdateIDs._BoundingSphereOutputVec4Offset, renderersParameters.boundingSphere.uintOffset);
				m_TransformUpdateCS.SetBuffer(kernelIndex, global::UnityEngine.Rendering.InstanceDataSystem.InstanceTransformUpdateIDs._BoundingSphereDataQueue, m_BoundingSpheresUpdateDataQueueBuffer);
			}
			m_TransformUpdateCS.SetBuffer(kernelIndex, global::UnityEngine.Rendering.InstanceDataSystem.InstanceTransformUpdateIDs._OutputTransformBuffer, outputBuffer.gpuBuffer);
			m_TransformUpdateCS.Dispatch(kernelIndex, (transformQueueCount + 63) / 64, 1, 1);
			nativeArray.Dispose();
		}

		private void DispatchWindDataCopyHistoryCommand(global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.GPUInstanceIndex> gpuInstanceIndices, global::UnityEngine.Rendering.RenderersParameters renderersParameters, global::UnityEngine.Rendering.GPUInstanceDataBuffer outputBuffer)
		{
			int windDataCopyHistoryKernel = m_WindDataCopyHistoryKernel;
			int length = gpuInstanceIndices.Length;
			EnsureIndexQueueBufferCapacity(length);
			m_UpdateIndexQueueBuffer.SetData(gpuInstanceIndices, 0, 0, length);
			m_WindDataUpdateCS.SetInt(global::UnityEngine.Rendering.InstanceDataSystem.InstanceWindDataUpdateIDs._WindDataQueueCount, length);
			for (int i = 0; i < 16; i++)
			{
				m_ScratchWindParamAddressArray[i * 4] = renderersParameters.windParams[i].gpuAddress;
			}
			m_WindDataUpdateCS.SetInts(global::UnityEngine.Rendering.InstanceDataSystem.InstanceWindDataUpdateIDs._WindParamAddressArray, m_ScratchWindParamAddressArray);
			for (int j = 0; j < 16; j++)
			{
				m_ScratchWindParamAddressArray[j * 4] = renderersParameters.windHistoryParams[j].gpuAddress;
			}
			m_WindDataUpdateCS.SetInts(global::UnityEngine.Rendering.InstanceDataSystem.InstanceWindDataUpdateIDs._WindHistoryParamAddressArray, m_ScratchWindParamAddressArray);
			m_WindDataUpdateCS.SetBuffer(windDataCopyHistoryKernel, global::UnityEngine.Rendering.InstanceDataSystem.InstanceWindDataUpdateIDs._WindDataUpdateIndexQueue, m_UpdateIndexQueueBuffer);
			m_WindDataUpdateCS.SetBuffer(windDataCopyHistoryKernel, global::UnityEngine.Rendering.InstanceDataSystem.InstanceWindDataUpdateIDs._WindDataBuffer, outputBuffer.gpuBuffer);
			m_WindDataUpdateCS.Dispatch(windDataCopyHistoryKernel, (length + 63) / 64, 1, 1);
		}

		private unsafe void UpdateInstanceMotionsData(in global::UnityEngine.Rendering.RenderersParameters renderersParameters, global::UnityEngine.Rendering.GPUInstanceDataBuffer outputBuffer)
		{
			global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.InstanceHandle> nativeArray = new global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.InstanceHandle>(m_InstanceData.instancesLength, global::Unity.Collections.Allocator.TempJob, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			int num = 0;
			global::Unity.Jobs.IJobParallelForExtensions.Schedule(new global::UnityEngine.Rendering.InstanceDataSystem.MotionUpdateJob
			{
				queueWriteBase = 0,
				instanceData = m_InstanceData,
				atomicUpdateQueueCount = new global::Unity.Collections.LowLevel.Unsafe.UnsafeAtomicCounter32(&num),
				transformUpdateInstanceQueue = nativeArray
			}, (m_InstanceData.instancesLength + 63) / 64, 16).Complete();
			if (num > 0)
			{
				DispatchMotionUpdateCommand(num, nativeArray, renderersParameters, outputBuffer);
			}
			nativeArray.Dispose();
		}

		private unsafe void UpdateInstanceTransformsData(bool initialize, global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.InstanceHandle> instances, global::Unity.Collections.NativeArray<global::UnityEngine.Matrix4x4> localToWorldMatrices, global::Unity.Collections.NativeArray<global::UnityEngine.Matrix4x4> prevLocalToWorldMatrices, in global::UnityEngine.Rendering.RenderersParameters renderersParameters, global::UnityEngine.Rendering.GPUInstanceDataBuffer outputBuffer)
		{
			global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.InstanceHandle> nativeArray = new global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.InstanceHandle>(instances.Length, global::Unity.Collections.Allocator.TempJob, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.TransformUpdatePacket> nativeArray2 = new global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.TransformUpdatePacket>(initialize ? (instances.Length * 2) : instances.Length, global::Unity.Collections.Allocator.TempJob, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			global::Unity.Collections.NativeArray<global::Unity.Mathematics.float4> nativeArray3 = new global::Unity.Collections.NativeArray<global::Unity.Mathematics.float4>(m_EnableBoundingSpheres ? instances.Length : 0, global::Unity.Collections.Allocator.TempJob, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.InstanceHandle> nativeArray4 = new global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.InstanceHandle>(instances.Length, global::Unity.Collections.Allocator.TempJob, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			global::Unity.Collections.NativeArray<int> compactTetrahedronCache = new global::Unity.Collections.NativeArray<int>(instances.Length, global::Unity.Collections.Allocator.TempJob, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			global::Unity.Collections.NativeArray<global::UnityEngine.Vector3> probeQueryPosition = new global::Unity.Collections.NativeArray<global::UnityEngine.Vector3>(instances.Length, global::Unity.Collections.Allocator.TempJob, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.SphericalHarmonicsL2> probeUpdateDataQueue = new global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.SphericalHarmonicsL2>(instances.Length, global::Unity.Collections.Allocator.TempJob, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			global::Unity.Collections.NativeArray<global::UnityEngine.Vector4> probeOcclusionUpdateDataQueue = new global::Unity.Collections.NativeArray<global::UnityEngine.Vector4>(instances.Length, global::Unity.Collections.Allocator.TempJob, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			int num = 0;
			int num2 = 0;
			global::UnityEngine.Rendering.InstanceDataSystem.TransformUpdateJob jobData = new global::UnityEngine.Rendering.InstanceDataSystem.TransformUpdateJob
			{
				initialize = initialize,
				enableBoundingSpheres = m_EnableBoundingSpheres,
				instances = instances,
				localToWorldMatrices = localToWorldMatrices,
				prevLocalToWorldMatrices = prevLocalToWorldMatrices,
				atomicTransformQueueCount = new global::Unity.Collections.LowLevel.Unsafe.UnsafeAtomicCounter32(&num),
				sharedInstanceData = m_SharedInstanceData,
				instanceData = m_InstanceData,
				transformUpdateInstanceQueue = nativeArray,
				transformUpdateDataQueue = nativeArray2,
				boundingSpheresDataQueue = nativeArray3
			};
			global::Unity.Jobs.IJobParallelForBatchExtensions.ScheduleBatch(new global::UnityEngine.Rendering.InstanceDataSystem.ProbesUpdateJob
			{
				instances = instances,
				instanceData = m_InstanceData,
				sharedInstanceData = m_SharedInstanceData,
				atomicProbesQueueCount = new global::Unity.Collections.LowLevel.Unsafe.UnsafeAtomicCounter32(&num2),
				probeInstanceQueue = nativeArray4,
				compactTetrahedronCache = compactTetrahedronCache,
				probeQueryPosition = probeQueryPosition
			}, dependsOn: global::Unity.Jobs.IJobParallelForBatchExtensions.ScheduleBatch(jobData, instances.Length, 64), arrayLength: instances.Length, indicesPerJobCount: 64).Complete();
			if (num2 > 0)
			{
				ScheduleInterpolateProbesAndUpdateTetrahedronCache(num2, nativeArray4, compactTetrahedronCache, probeQueryPosition, probeUpdateDataQueue, probeOcclusionUpdateDataQueue).Complete();
				DispatchProbeUpdateCommand(num2, nativeArray4, probeUpdateDataQueue, probeOcclusionUpdateDataQueue, renderersParameters, outputBuffer);
			}
			if (num > 0)
			{
				DispatchTransformUpdateCommand(initialize, num, nativeArray, nativeArray2, nativeArray3, renderersParameters, outputBuffer);
			}
			nativeArray.Dispose();
			nativeArray2.Dispose();
			nativeArray3.Dispose();
			nativeArray4.Dispose();
			compactTetrahedronCache.Dispose();
			probeQueryPosition.Dispose();
			probeUpdateDataQueue.Dispose();
			probeOcclusionUpdateDataQueue.Dispose();
		}

		private unsafe void UpdateInstanceProbesData(global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.InstanceHandle> instances, in global::UnityEngine.Rendering.RenderersParameters renderersParameters, global::UnityEngine.Rendering.GPUInstanceDataBuffer outputBuffer)
		{
			global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.InstanceHandle> nativeArray = new global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.InstanceHandle>(instances.Length, global::Unity.Collections.Allocator.TempJob, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			global::Unity.Collections.NativeArray<int> compactTetrahedronCache = new global::Unity.Collections.NativeArray<int>(instances.Length, global::Unity.Collections.Allocator.TempJob, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			global::Unity.Collections.NativeArray<global::UnityEngine.Vector3> probeQueryPosition = new global::Unity.Collections.NativeArray<global::UnityEngine.Vector3>(instances.Length, global::Unity.Collections.Allocator.TempJob, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.SphericalHarmonicsL2> probeUpdateDataQueue = new global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.SphericalHarmonicsL2>(instances.Length, global::Unity.Collections.Allocator.TempJob, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			global::Unity.Collections.NativeArray<global::UnityEngine.Vector4> probeOcclusionUpdateDataQueue = new global::Unity.Collections.NativeArray<global::UnityEngine.Vector4>(instances.Length, global::Unity.Collections.Allocator.TempJob, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			int num = 0;
			global::Unity.Jobs.IJobParallelForBatchExtensions.ScheduleBatch(new global::UnityEngine.Rendering.InstanceDataSystem.ProbesUpdateJob
			{
				instances = instances,
				instanceData = m_InstanceData,
				sharedInstanceData = m_SharedInstanceData,
				atomicProbesQueueCount = new global::Unity.Collections.LowLevel.Unsafe.UnsafeAtomicCounter32(&num),
				probeInstanceQueue = nativeArray,
				compactTetrahedronCache = compactTetrahedronCache,
				probeQueryPosition = probeQueryPosition
			}, instances.Length, 64).Complete();
			if (num > 0)
			{
				ScheduleInterpolateProbesAndUpdateTetrahedronCache(num, nativeArray, compactTetrahedronCache, probeQueryPosition, probeUpdateDataQueue, probeOcclusionUpdateDataQueue).Complete();
				DispatchProbeUpdateCommand(num, nativeArray, probeUpdateDataQueue, probeOcclusionUpdateDataQueue, renderersParameters, outputBuffer);
			}
			nativeArray.Dispose();
			compactTetrahedronCache.Dispose();
			probeQueryPosition.Dispose();
			probeUpdateDataQueue.Dispose();
			probeOcclusionUpdateDataQueue.Dispose();
		}

		public void UpdateInstanceWindDataHistory(global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.GPUInstanceIndex> gpuInstanceIndices, global::UnityEngine.Rendering.RenderersParameters renderersParameters, global::UnityEngine.Rendering.GPUInstanceDataBuffer outputBuffer)
		{
			if (gpuInstanceIndices.Length != 0)
			{
				DispatchWindDataCopyHistoryCommand(gpuInstanceIndices, renderersParameters, outputBuffer);
			}
		}

		public unsafe void ReallocateAndGetInstances(in global::UnityEngine.Rendering.GPUDrivenRendererGroupData rendererData, global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.InstanceHandle> instances)
		{
			int instancesCount = 0;
			int num = 0;
			bool num2 = rendererData.instancesCount.Length == 0;
			if (num2)
			{
				global::Unity.Jobs.IJobParallelForBatchExtensions.ScheduleBatch(new global::UnityEngine.Rendering.InstanceDataSystem.QueryRendererGroupInstancesJob
				{
					rendererGroupInstanceMultiHash = m_RendererGroupInstanceMultiHash,
					rendererGroupIDs = rendererData.rendererGroupID,
					instances = instances,
					atomicNonFoundInstancesCount = new global::Unity.Collections.LowLevel.Unsafe.UnsafeAtomicCounter32(&num)
				}, rendererData.rendererGroupID.Length, 128).Complete();
				instancesCount = num;
			}
			else
			{
				global::Unity.Jobs.IJobParallelForBatchExtensions.ScheduleBatch(new global::UnityEngine.Rendering.InstanceDataSystem.QueryRendererGroupInstancesMultiJob
				{
					rendererGroupInstanceMultiHash = m_RendererGroupInstanceMultiHash,
					rendererGroupIDs = rendererData.rendererGroupID,
					instancesOffsets = rendererData.instancesOffset,
					instancesCounts = rendererData.instancesCount,
					instances = instances,
					atomicNonFoundSharedInstancesCount = new global::Unity.Collections.LowLevel.Unsafe.UnsafeAtomicCounter32(&instancesCount),
					atomicNonFoundInstancesCount = new global::Unity.Collections.LowLevel.Unsafe.UnsafeAtomicCounter32(&num)
				}, rendererData.rendererGroupID.Length, 128).Complete();
			}
			m_InstanceData.EnsureFreeInstances(num);
			m_PerCameraInstanceData.Grow(m_InstanceData.instancesCapacity);
			m_SharedInstanceData.EnsureFreeInstances(instancesCount);
			global::UnityEngine.Rendering.InstanceDataSystemBurst.ReallocateInstances(num2, in rendererData.rendererGroupID, in rendererData.packedRendererData, in rendererData.instancesOffset, in rendererData.instancesCount, ref m_InstanceAllocators, ref m_InstanceData, ref m_PerCameraInstanceData, ref m_SharedInstanceData, ref instances, ref m_RendererGroupInstanceMultiHash);
		}

		public void FreeRendererGroupInstances(global::Unity.Collections.NativeArray<global::UnityEngine.EntityId> rendererGroupsID)
		{
			global::UnityEngine.Rendering.InstanceDataSystemBurst.FreeRendererGroupInstances(rendererGroupsID.AsReadOnly(), ref m_InstanceAllocators, ref m_InstanceData, ref m_PerCameraInstanceData, ref m_SharedInstanceData, ref m_RendererGroupInstanceMultiHash);
		}

		public void FreeInstances(global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.InstanceHandle> instances)
		{
			global::UnityEngine.Rendering.InstanceDataSystemBurst.FreeInstances(instances.AsReadOnly(), ref m_InstanceAllocators, ref m_InstanceData, ref m_PerCameraInstanceData, ref m_SharedInstanceData, ref m_RendererGroupInstanceMultiHash);
		}

		public global::Unity.Jobs.JobHandle ScheduleUpdateInstanceDataJob(global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.InstanceHandle> instances, in global::UnityEngine.Rendering.GPUDrivenRendererGroupData rendererData, global::Unity.Collections.NativeParallelHashMap<int, global::UnityEngine.Rendering.GPUInstanceIndex> lodGroupDataMap)
		{
			bool implicitInstanceIndices = rendererData.instancesCount.Length == 0;
			return global::Unity.Jobs.IJobParallelForExtensions.Schedule(new global::UnityEngine.Rendering.InstanceDataSystem.UpdateRendererInstancesJob
			{
				implicitInstanceIndices = implicitInstanceIndices,
				instances = instances,
				rendererData = rendererData,
				lodGroupDataMap = lodGroupDataMap,
				instanceData = m_InstanceData,
				sharedInstanceData = m_SharedInstanceData,
				perCameraInstanceData = m_PerCameraInstanceData
			}, rendererData.rendererGroupID.Length, 128);
		}

		public void UpdateAllInstanceProbes(in global::UnityEngine.Rendering.RenderersParameters renderersParameters, global::UnityEngine.Rendering.GPUInstanceDataBuffer outputBuffer)
		{
			global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.InstanceHandle> subArray = m_InstanceData.instances.GetSubArray(0, m_InstanceData.instancesLength);
			if (subArray.Length != 0)
			{
				UpdateInstanceProbesData(subArray, in renderersParameters, outputBuffer);
			}
		}

		public void InitializeInstanceTransforms(global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.InstanceHandle> instances, global::Unity.Collections.NativeArray<global::UnityEngine.Matrix4x4> localToWorldMatrices, global::Unity.Collections.NativeArray<global::UnityEngine.Matrix4x4> prevLocalToWorldMatrices, in global::UnityEngine.Rendering.RenderersParameters renderersParameters, global::UnityEngine.Rendering.GPUInstanceDataBuffer outputBuffer)
		{
			if (instances.Length != 0)
			{
				UpdateInstanceTransformsData(initialize: true, instances, localToWorldMatrices, prevLocalToWorldMatrices, in renderersParameters, outputBuffer);
			}
		}

		public void UpdateInstanceTransforms(global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.InstanceHandle> instances, global::Unity.Collections.NativeArray<global::UnityEngine.Matrix4x4> localToWorldMatrices, in global::UnityEngine.Rendering.RenderersParameters renderersParameters, global::UnityEngine.Rendering.GPUInstanceDataBuffer outputBuffer)
		{
			if (instances.Length != 0)
			{
				UpdateInstanceTransformsData(initialize: false, instances, localToWorldMatrices, localToWorldMatrices, in renderersParameters, outputBuffer);
			}
		}

		public void UpdateInstanceMotions(in global::UnityEngine.Rendering.RenderersParameters renderersParameters, global::UnityEngine.Rendering.GPUInstanceDataBuffer outputBuffer)
		{
			if (m_InstanceData.instancesLength != 0)
			{
				UpdateInstanceMotionsData(in renderersParameters, outputBuffer);
			}
		}

		public global::Unity.Jobs.JobHandle ScheduleQueryRendererGroupInstancesJob(global::Unity.Collections.NativeArray<global::UnityEngine.EntityId> rendererGroupIDs, global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.InstanceHandle> instances)
		{
			if (rendererGroupIDs.Length == 0)
			{
				return default(global::Unity.Jobs.JobHandle);
			}
			return global::Unity.Jobs.IJobParallelForBatchExtensions.ScheduleBatch(new global::UnityEngine.Rendering.InstanceDataSystem.QueryRendererGroupInstancesJob
			{
				rendererGroupInstanceMultiHash = m_RendererGroupInstanceMultiHash,
				rendererGroupIDs = rendererGroupIDs,
				instances = instances
			}, rendererGroupIDs.Length, 128);
		}

		public global::Unity.Jobs.JobHandle ScheduleQueryRendererGroupInstancesJob(global::Unity.Collections.NativeArray<global::UnityEngine.EntityId> rendererGroupIDs, global::Unity.Collections.NativeList<global::UnityEngine.Rendering.InstanceHandle> instances)
		{
			if (rendererGroupIDs.Length == 0)
			{
				return default(global::Unity.Jobs.JobHandle);
			}
			global::Unity.Collections.NativeArray<int> instancesOffset = new global::Unity.Collections.NativeArray<int>(rendererGroupIDs.Length, global::Unity.Collections.Allocator.TempJob);
			global::Unity.Collections.NativeArray<int> instancesCount = new global::Unity.Collections.NativeArray<int>(rendererGroupIDs.Length, global::Unity.Collections.Allocator.TempJob);
			global::Unity.Jobs.JobHandle jobHandle = ScheduleQueryRendererGroupInstancesJob(rendererGroupIDs, instancesOffset, instancesCount, instances);
			instancesOffset.Dispose(jobHandle);
			instancesCount.Dispose(jobHandle);
			return jobHandle;
		}

		public global::Unity.Jobs.JobHandle ScheduleQueryRendererGroupInstancesJob(global::Unity.Collections.NativeArray<global::UnityEngine.EntityId> rendererGroupIDs, global::Unity.Collections.NativeArray<int> instancesOffset, global::Unity.Collections.NativeArray<int> instancesCount, global::Unity.Collections.NativeList<global::UnityEngine.Rendering.InstanceHandle> instances)
		{
			if (rendererGroupIDs.Length == 0)
			{
				return default(global::Unity.Jobs.JobHandle);
			}
			global::Unity.Jobs.JobHandle dependsOn = global::Unity.Jobs.IJobParallelForBatchExtensions.ScheduleBatch(new global::UnityEngine.Rendering.InstanceDataSystem.QueryRendererGroupInstancesCountJob
			{
				instanceData = m_InstanceData,
				sharedInstanceData = m_SharedInstanceData,
				rendererGroupInstanceMultiHash = m_RendererGroupInstanceMultiHash,
				rendererGroupIDs = rendererGroupIDs,
				instancesCount = instancesCount
			}, rendererGroupIDs.Length, 128);
			global::Unity.Jobs.JobHandle dependsOn2 = global::Unity.Jobs.IJobExtensions.Schedule(new global::UnityEngine.Rendering.InstanceDataSystem.ComputeInstancesOffsetAndResizeInstancesArrayJob
			{
				instancesCount = instancesCount,
				instancesOffset = instancesOffset,
				instances = instances
			}, dependsOn);
			return global::Unity.Jobs.IJobParallelForBatchExtensions.ScheduleBatch(new global::UnityEngine.Rendering.InstanceDataSystem.QueryRendererGroupInstancesMultiJob
			{
				rendererGroupInstanceMultiHash = m_RendererGroupInstanceMultiHash,
				rendererGroupIDs = rendererGroupIDs,
				instancesOffsets = instancesOffset,
				instancesCounts = instancesCount,
				instances = instances.AsDeferredJobArray()
			}, rendererGroupIDs.Length, 128, dependsOn2);
		}

		public global::Unity.Jobs.JobHandle ScheduleQuerySortedMeshInstancesJob(global::Unity.Collections.NativeArray<global::UnityEngine.EntityId> sortedMeshIDs, global::Unity.Collections.NativeList<global::UnityEngine.Rendering.InstanceHandle> instances)
		{
			if (sortedMeshIDs.Length == 0)
			{
				return default(global::Unity.Jobs.JobHandle);
			}
			instances.Capacity = m_InstanceData.instancesLength;
			return global::Unity.Jobs.IJobParallelForBatchExtensions.ScheduleBatch(new global::UnityEngine.Rendering.InstanceDataSystem.QuerySortedMeshInstancesJob
			{
				instanceData = m_InstanceData,
				sharedInstanceData = m_SharedInstanceData,
				sortedMeshID = sortedMeshIDs,
				instances = instances
			}, m_InstanceData.instancesLength, 64);
		}

		public global::Unity.Jobs.JobHandle ScheduleCollectInstancesLODGroupAndMasksJob(global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.InstanceHandle> instances, global::Unity.Collections.NativeArray<uint> lodGroupAndMasks)
		{
			return global::Unity.Jobs.IJobParallelForExtensions.Schedule(new global::UnityEngine.Rendering.InstanceDataSystem.CollectInstancesLODGroupsAndMasksJob
			{
				instanceData = instanceData,
				sharedInstanceData = sharedInstanceData,
				instances = instances,
				lodGroupAndMasks = lodGroupAndMasks
			}, instances.Length, 128);
		}

		public bool InternalSanityCheckStates()
		{
			global::Unity.Collections.NativeParallelHashMap<global::UnityEngine.Rendering.SharedInstanceHandle, int> nativeParallelHashMap = new global::Unity.Collections.NativeParallelHashMap<global::UnityEngine.Rendering.SharedInstanceHandle, int>(64, global::Unity.Collections.Allocator.Temp);
			int num = 0;
			for (int i = 0; i < m_InstanceData.handlesLength; i++)
			{
				global::UnityEngine.Rendering.InstanceHandle instance = global::UnityEngine.Rendering.InstanceHandle.FromInt(i);
				if (m_InstanceData.IsValidInstance(instance))
				{
					global::UnityEngine.Rendering.SharedInstanceHandle key = m_InstanceData.Get_SharedInstance(instance);
					if (nativeParallelHashMap.TryGetValue(key, out var item))
					{
						nativeParallelHashMap[key] = item + 1;
					}
					else
					{
						nativeParallelHashMap.Add(key, 1);
					}
					num++;
				}
			}
			if (m_InstanceData.instancesLength != num)
			{
				return false;
			}
			int num2 = 0;
			for (int j = 0; j < m_SharedInstanceData.handlesLength; j++)
			{
				global::UnityEngine.Rendering.SharedInstanceHandle sharedInstanceHandle = new global::UnityEngine.Rendering.SharedInstanceHandle
				{
					index = j
				};
				if (m_SharedInstanceData.IsValidInstance(sharedInstanceHandle))
				{
					int num3 = m_SharedInstanceData.Get_RefCount(sharedInstanceHandle);
					if (nativeParallelHashMap[sharedInstanceHandle] != num3)
					{
						return false;
					}
					num2++;
				}
			}
			if (m_SharedInstanceData.instancesLength != num2)
			{
				return false;
			}
			return true;
		}

		public unsafe void GetVisibleTreeInstances(in global::UnityEngine.Rendering.ParallelBitArray compactedVisibilityMasks, in global::UnityEngine.Rendering.ParallelBitArray processedBits, global::Unity.Collections.NativeList<int> visibeTreeRendererIDs, global::Unity.Collections.NativeList<global::UnityEngine.Rendering.InstanceHandle> visibeTreeInstances, bool becomeVisibleOnly, out int becomeVisibeTreeInstancesCount)
		{
			becomeVisibeTreeInstancesCount = 0;
			int aliveInstancesOfType = GetAliveInstancesOfType(global::UnityEngine.Rendering.InstanceType.SpeedTree);
			if (aliveInstancesOfType != 0)
			{
				visibeTreeRendererIDs.ResizeUninitialized(aliveInstancesOfType);
				visibeTreeInstances.ResizeUninitialized(aliveInstancesOfType);
				int num = 0;
				global::Unity.Jobs.IJobParallelForBatchExtensions.ScheduleBatch(new global::UnityEngine.Rendering.InstanceDataSystem.GetVisibleNonProcessedTreeInstancesJob
				{
					becomeVisible = true,
					instanceData = m_InstanceData,
					sharedInstanceData = m_SharedInstanceData,
					compactedVisibilityMasks = compactedVisibilityMasks,
					processedBits = processedBits,
					rendererIDs = visibeTreeRendererIDs.AsArray(),
					instances = visibeTreeInstances.AsArray(),
					atomicTreeInstancesCount = new global::Unity.Collections.LowLevel.Unsafe.UnsafeAtomicCounter32(&num)
				}, m_InstanceData.instancesLength, 64).Complete();
				becomeVisibeTreeInstancesCount = num;
				if (!becomeVisibleOnly)
				{
					global::Unity.Jobs.IJobParallelForBatchExtensions.ScheduleBatch(new global::UnityEngine.Rendering.InstanceDataSystem.GetVisibleNonProcessedTreeInstancesJob
					{
						becomeVisible = false,
						instanceData = m_InstanceData,
						sharedInstanceData = m_SharedInstanceData,
						compactedVisibilityMasks = compactedVisibilityMasks,
						processedBits = processedBits,
						rendererIDs = visibeTreeRendererIDs.AsArray(),
						instances = visibeTreeInstances.AsArray(),
						atomicTreeInstancesCount = new global::Unity.Collections.LowLevel.Unsafe.UnsafeAtomicCounter32(&num)
					}, m_InstanceData.instancesLength, 64).Complete();
				}
				visibeTreeRendererIDs.ResizeUninitialized(num);
				visibeTreeInstances.ResizeUninitialized(num);
			}
		}

		public void UpdatePerFrameInstanceVisibility(in global::UnityEngine.Rendering.ParallelBitArray compactedVisibilityMasks)
		{
			global::Unity.Jobs.IJobParallelForBatchExtensions.ScheduleBatch(new global::UnityEngine.Rendering.InstanceDataSystem.UpdateCompactedInstanceVisibilityJob
			{
				instanceData = m_InstanceData,
				compactedVisibilityMasks = compactedVisibilityMasks
			}, m_InstanceData.instancesLength, 64).Complete();
		}

		public void DeallocatePerCameraInstanceData(global::Unity.Collections.NativeArray<global::UnityEngine.EntityId> cameraIDs)
		{
			m_PerCameraInstanceData.DeallocateCameras(cameraIDs);
		}

		public void AllocatePerCameraInstanceData(global::Unity.Collections.NativeArray<global::UnityEngine.EntityId> cameraIDs)
		{
			m_PerCameraInstanceData.AllocateCameras(cameraIDs);
		}

		private unsafe static int AtomicAddLengthNoResize<T>(in global::Unity.Collections.NativeList<T> list, int count) where T : unmanaged
		{
			return global::System.Threading.Interlocked.Add(ref list.GetUnsafeList()->m_length, count) - count;
		}
	}
}
