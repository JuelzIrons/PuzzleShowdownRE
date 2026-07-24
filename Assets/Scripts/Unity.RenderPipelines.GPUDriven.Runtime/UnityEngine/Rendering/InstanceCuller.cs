namespace UnityEngine.Rendering
{
	internal struct InstanceCuller : global::System.IDisposable
	{
		private struct AnimatedFadeData
		{
			public int cameraID;

			public global::Unity.Jobs.JobHandle jobHandle;
		}

		private static class ShaderIDs
		{
			public static readonly int InstanceOcclusionCullerShaderVariables = global::UnityEngine.Shader.PropertyToID("InstanceOcclusionCullerShaderVariables");

			public static readonly int _DrawInfo = global::UnityEngine.Shader.PropertyToID("_DrawInfo");

			public static readonly int _InstanceInfo = global::UnityEngine.Shader.PropertyToID("_InstanceInfo");

			public static readonly int _DispatchArgs = global::UnityEngine.Shader.PropertyToID("_DispatchArgs");

			public static readonly int _DrawArgs = global::UnityEngine.Shader.PropertyToID("_DrawArgs");

			public static readonly int _InstanceIndices = global::UnityEngine.Shader.PropertyToID("_InstanceIndices");

			public static readonly int _InstanceDataBuffer = global::UnityEngine.Shader.PropertyToID("_InstanceDataBuffer");

			public static readonly int _OccluderDepthPyramid = global::UnityEngine.Shader.PropertyToID("_OccluderDepthPyramid");

			public static readonly int _OcclusionDebugCounters = global::UnityEngine.Shader.PropertyToID("_OcclusionDebugCounters");
		}

		private class InstanceOcclusionTestPassData
		{
			public global::UnityEngine.Rendering.OcclusionCullingSettings settings;

			public global::UnityEngine.Rendering.InstanceOcclusionTestSubviewSettings subviewSettings;

			public global::UnityEngine.Rendering.OccluderHandles occluderHandles;

			public global::UnityEngine.Rendering.IndirectBufferContextHandles bufferHandles;
		}

		private global::Unity.Collections.NativeParallelHashMap<int, global::UnityEngine.Rendering.InstanceCuller.AnimatedFadeData> m_LODParamsToCameraID;

		private global::UnityEngine.Rendering.ParallelBitArray m_CompactedVisibilityMasks;

		private global::Unity.Jobs.JobHandle m_CompactedVisibilityMasksJobsHandle;

		private global::UnityEngine.Rendering.IndirectBufferContextStorage m_IndirectStorage;

		private global::UnityEngine.Rendering.OcclusionTestComputeShader m_OcclusionTestShader;

		private int m_ResetDrawArgsKernel;

		private int m_CopyInstancesKernel;

		private int m_CullInstancesKernel;

		private global::UnityEngine.Rendering.DebugRendererBatcherStats m_DebugStats;

		private global::UnityEngine.Rendering.InstanceCullerSplitDebugArray m_SplitDebugArray;

		private global::UnityEngine.Rendering.InstanceOcclusionEventDebugArray m_OcclusionEventDebugArray;

		private global::UnityEngine.Rendering.ProfilingSampler m_ProfilingSampleInstanceOcclusionTest;

		private global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.InstanceOcclusionCullerShaderVariables> m_ShaderVariables;

		private global::UnityEngine.ComputeBuffer m_ConstantBuffer;

		private global::UnityEngine.Rendering.CommandBuffer m_CommandBuffer;

		internal void Init(global::UnityEngine.Rendering.GPUResidentDrawerResources resources, global::UnityEngine.Rendering.DebugRendererBatcherStats debugStats = null)
		{
			m_IndirectStorage.Init();
			m_OcclusionTestShader.Init(resources.instanceOcclusionCullingKernels);
			m_ResetDrawArgsKernel = m_OcclusionTestShader.cs.FindKernel("ResetDrawArgs");
			m_CopyInstancesKernel = m_OcclusionTestShader.cs.FindKernel("CopyInstances");
			m_CullInstancesKernel = m_OcclusionTestShader.cs.FindKernel("CullInstances");
			m_DebugStats = debugStats;
			m_SplitDebugArray = default(global::UnityEngine.Rendering.InstanceCullerSplitDebugArray);
			m_SplitDebugArray.Init();
			m_OcclusionEventDebugArray = default(global::UnityEngine.Rendering.InstanceOcclusionEventDebugArray);
			m_OcclusionEventDebugArray.Init();
			m_ProfilingSampleInstanceOcclusionTest = new global::UnityEngine.Rendering.ProfilingSampler("InstanceOcclusionTest");
			m_ShaderVariables = new global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.InstanceOcclusionCullerShaderVariables>(1, global::Unity.Collections.Allocator.Persistent);
			m_ConstantBuffer = new global::UnityEngine.ComputeBuffer(1, global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<global::UnityEngine.Rendering.InstanceOcclusionCullerShaderVariables>(), global::UnityEngine.ComputeBufferType.Constant);
			m_CommandBuffer = new global::UnityEngine.Rendering.CommandBuffer();
			m_CommandBuffer.name = "EnsureValidOcclusionTestResults";
			m_LODParamsToCameraID = new global::Unity.Collections.NativeParallelHashMap<int, global::UnityEngine.Rendering.InstanceCuller.AnimatedFadeData>(16, global::Unity.Collections.Allocator.Persistent);
		}

		private global::Unity.Jobs.JobHandle AnimateCrossFades(global::UnityEngine.Rendering.CPUPerCameraInstanceData perCameraInstanceData, global::UnityEngine.Rendering.BatchCullingContext cc, out global::UnityEngine.Rendering.CPUPerCameraInstanceData.PerCameraInstanceDataArrays cameraInstanceData, out bool hasAnimatedCrossfade)
		{
			int hashCode = cc.lodParameters.GetHashCode();
			hasAnimatedCrossfade = m_LODParamsToCameraID.TryGetValue(hashCode, out var item);
			if (hasAnimatedCrossfade)
			{
				cameraInstanceData = perCameraInstanceData.perCameraData[item.cameraID];
				return item.jobHandle;
			}
			if (cc.viewType != global::UnityEngine.Rendering.BatchCullingViewType.Camera && !hasAnimatedCrossfade)
			{
				cameraInstanceData = default(global::UnityEngine.Rendering.CPUPerCameraInstanceData.PerCameraInstanceDataArrays);
				return default(global::Unity.Jobs.JobHandle);
			}
			int instanceID = cc.viewID.GetInstanceID();
			hasAnimatedCrossfade = perCameraInstanceData.perCameraData.TryGetValue(instanceID, out var item2);
			if (!hasAnimatedCrossfade)
			{
				cameraInstanceData = default(global::UnityEngine.Rendering.CPUPerCameraInstanceData.PerCameraInstanceDataArrays);
				return default(global::Unity.Jobs.JobHandle);
			}
			cameraInstanceData = item2;
			global::Unity.Jobs.JobHandle jobHandle = global::Unity.Jobs.IJobParallelForExtensions.Schedule(new global::UnityEngine.Rendering.AnimateCrossFadeJob
			{
				deltaTime = global::UnityEngine.Time.deltaTime,
				crossFadeArray = cameraInstanceData.crossFades
			}, perCameraInstanceData.instancesLength, 512);
			m_LODParamsToCameraID.TryAdd(hashCode, new global::UnityEngine.Rendering.InstanceCuller.AnimatedFadeData
			{
				cameraID = instanceID,
				jobHandle = jobHandle
			});
			return jobHandle;
		}

		private unsafe global::Unity.Jobs.JobHandle CreateFrustumCullingJob(in global::UnityEngine.Rendering.BatchCullingContext cc, in global::UnityEngine.Rendering.CPUInstanceData.ReadOnly instanceData, in global::UnityEngine.Rendering.CPUSharedInstanceData.ReadOnly sharedInstanceData, in global::UnityEngine.Rendering.CPUPerCameraInstanceData perCameraInstanceData, global::Unity.Collections.NativeList<global::UnityEngine.Rendering.LODGroupCullingData> lodGroupCullingData, in global::UnityEngine.Rendering.BinningConfig binningConfig, float smallMeshScreenPercentage, global::UnityEngine.Rendering.OcclusionCullingCommon occlusionCullingCommon, global::Unity.Collections.NativeArray<byte> rendererVisibilityMasks, global::Unity.Collections.NativeArray<byte> rendererMeshLodSettings, global::Unity.Collections.NativeArray<byte> rendererCrossFadeValues)
		{
			global::UnityEngine.Rendering.ReceiverPlanes receiverPlanes = default(global::UnityEngine.Rendering.ReceiverPlanes);
			global::UnityEngine.Rendering.ReceiverSphereCuller receiverSphereCuller = default(global::UnityEngine.Rendering.ReceiverSphereCuller);
			global::UnityEngine.Rendering.FrustumPlaneCuller frustumPlaneCuller = default(global::UnityEngine.Rendering.FrustumPlaneCuller);
			float num = default(float);
			float num2 = default(float);
			fixed (global::UnityEngine.Rendering.BatchCullingContext* context = &cc)
			{
				global::UnityEngine.Rendering.InstanceCullerBurst.SetupCullingJobInput(global::UnityEngine.QualitySettings.lodBias, global::UnityEngine.QualitySettings.meshLodThreshold, context, &receiverPlanes, &receiverSphereCuller, &frustumPlaneCuller, &num, &num2);
			}
			occlusionCullingCommon?.UpdateSilhouettePlanes(cc.viewID.GetInstanceID(), receiverPlanes.SilhouettePlaneSubArray());
			global::UnityEngine.Rendering.CPUPerCameraInstanceData.PerCameraInstanceDataArrays cameraInstanceData;
			bool hasAnimatedCrossfade;
			global::Unity.Jobs.JobHandle dependsOn = AnimateCrossFades(perCameraInstanceData, cc, out cameraInstanceData, out hasAnimatedCrossfade);
			global::Unity.Jobs.JobHandle jobHandle = global::Unity.Jobs.IJobParallelForExtensions.Schedule(new global::UnityEngine.Rendering.CullingJob
			{
				binningConfig = binningConfig,
				viewType = cc.viewType,
				frustumPlanePackets = frustumPlaneCuller.planePackets.AsArray(),
				frustumSplitInfos = frustumPlaneCuller.splitInfos.AsArray(),
				lightFacingFrustumPlanes = receiverPlanes.LightFacingFrustumPlaneSubArray(),
				receiverSplitInfos = receiverSphereCuller.splitInfos.AsArray(),
				worldToLightSpaceRotation = receiverSphereCuller.worldToLightSpaceRotation,
				cullLightmappedShadowCasters = ((cc.cullingFlags & global::UnityEngine.Rendering.BatchCullingFlags.CullLightmappedShadowCasters) != 0),
				cameraPosition = cc.lodParameters.cameraPosition,
				sqrMeshLodSelectionConstant = num2 * num2,
				sqrScreenRelativeMetric = num * num,
				minScreenRelativeHeight = smallMeshScreenPercentage * 0.01f,
				isOrtho = cc.lodParameters.isOrthographic,
				animateCrossFades = hasAnimatedCrossfade,
				instanceData = instanceData,
				sharedInstanceData = sharedInstanceData,
				cameraInstanceData = cameraInstanceData,
				lodGroupCullingData = lodGroupCullingData,
				occlusionBuffer = cc.occlusionBuffer,
				rendererVisibilityMasks = rendererVisibilityMasks,
				rendererMeshLodSettings = rendererMeshLodSettings,
				rendererCrossFadeValues = rendererCrossFadeValues,
				maxLOD = global::UnityEngine.QualitySettings.maximumLODLevel,
				cullingLayerMask = cc.cullingLayerMask,
				sceneCullingMask = cc.sceneCullingMask
			}, instanceData.instancesLength, 32, dependsOn);
			receiverPlanes.Dispose(jobHandle);
			frustumPlaneCuller.Dispose(jobHandle);
			receiverSphereCuller.Dispose(jobHandle);
			return jobHandle;
		}

		private int ComputeWorstCaseDrawCommandCount(in global::UnityEngine.Rendering.BatchCullingContext cc, global::UnityEngine.Rendering.BinningConfig binningConfig, global::UnityEngine.Rendering.CPUDrawInstanceData drawInstanceData)
		{
			int length = drawInstanceData.drawInstances.Length;
			int num = drawInstanceData.drawBatches.Length;
			if (binningConfig.supportsCrossFade)
			{
				num *= 2;
			}
			num *= 2;
			if (binningConfig.supportsMotionCheck)
			{
				num *= 2;
			}
			if (cc.cullingSplits.Length > 1)
			{
				num <<= cc.cullingSplits.Length - 1;
			}
			return global::Unity.Mathematics.math.min(num, length);
		}

		public unsafe global::Unity.Jobs.JobHandle CreateCullJobTree(in global::UnityEngine.Rendering.BatchCullingContext cc, global::UnityEngine.Rendering.BatchCullingOutput cullingOutput, in global::UnityEngine.Rendering.CPUInstanceData.ReadOnly instanceData, in global::UnityEngine.Rendering.CPUSharedInstanceData.ReadOnly sharedInstanceData, in global::UnityEngine.Rendering.CPUPerCameraInstanceData perCameraInstanceData, in global::UnityEngine.Rendering.GPUInstanceDataBuffer.ReadOnly instanceDataBuffer, global::Unity.Collections.NativeList<global::UnityEngine.Rendering.LODGroupCullingData> lodGroupCullingData, global::UnityEngine.Rendering.CPUDrawInstanceData drawInstanceData, global::Unity.Collections.NativeParallelHashMap<uint, global::UnityEngine.Rendering.BatchID> batchIDs, float smallMeshScreenPercentage, global::UnityEngine.Rendering.OcclusionCullingCommon occlusionCullingCommon)
		{
			global::UnityEngine.Rendering.BatchCullingOutputDrawCommands value = default(global::UnityEngine.Rendering.BatchCullingOutputDrawCommands);
			value.drawRangeCount = drawInstanceData.drawRanges.Length;
			value.drawRanges = global::UnityEngine.Rendering.MemoryUtilities.Malloc<global::UnityEngine.Rendering.BatchDrawRange>(value.drawRangeCount, global::Unity.Collections.Allocator.TempJob);
			for (int i = 0; i < value.drawRangeCount; i++)
			{
				value.drawRanges[i].drawCommandsCount = 0u;
			}
			cullingOutput.drawCommands[0] = value;
			cullingOutput.customCullingResult[0] = global::System.IntPtr.Zero;
			global::UnityEngine.Rendering.BinningConfig binningConfig = new global::UnityEngine.Rendering.BinningConfig
			{
				viewCount = cc.cullingSplits.Length,
				supportsCrossFade = global::UnityEngine.QualitySettings.enableLODCrossFade,
				supportsMotionCheck = (cc.viewType == global::UnityEngine.Rendering.BatchCullingViewType.Camera)
			};
			int handlesLength = instanceData.handlesLength;
			global::Unity.Collections.NativeArray<byte> rendererVisibilityMasks = new global::Unity.Collections.NativeArray<byte>(handlesLength, global::Unity.Collections.Allocator.TempJob, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			global::Unity.Collections.NativeArray<byte> rendererCrossFadeValues = new global::Unity.Collections.NativeArray<byte>(handlesLength, global::Unity.Collections.Allocator.TempJob, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			global::Unity.Collections.NativeArray<byte> rendererMeshLodSettings = new global::Unity.Collections.NativeArray<byte>(handlesLength, global::Unity.Collections.Allocator.TempJob, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			global::Unity.Jobs.JobHandle jobHandle = CreateFrustumCullingJob(in cc, in instanceData, in sharedInstanceData, in perCameraInstanceData, lodGroupCullingData, in binningConfig, smallMeshScreenPercentage, occlusionCullingCommon, rendererVisibilityMasks, rendererMeshLodSettings, rendererCrossFadeValues);
			if (cc.viewType == global::UnityEngine.Rendering.BatchCullingViewType.Camera || cc.viewType == global::UnityEngine.Rendering.BatchCullingViewType.Light || cc.viewType == global::UnityEngine.Rendering.BatchCullingViewType.SelectionOutline)
			{
				jobHandle = CreateCompactedVisibilityMaskJob(in instanceData, rendererVisibilityMasks, jobHandle);
				int num = -1;
				global::UnityEngine.Rendering.DebugRendererBatcherStats debugStats = m_DebugStats;
				if (debugStats != null && debugStats.enabled)
				{
					num = m_SplitDebugArray.TryAddSplits(cc.viewType, cc.viewID.GetInstanceID(), cc.cullingSplits.Length);
				}
				int length = drawInstanceData.drawBatches.Length;
				int length2 = ComputeWorstCaseDrawCommandCount(in cc, binningConfig, drawInstanceData);
				global::Unity.Collections.NativeArray<int> batchBinAllocOffsets = new global::Unity.Collections.NativeArray<int>(length, global::Unity.Collections.Allocator.TempJob, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
				global::Unity.Collections.NativeArray<int> batchBinCounts = new global::Unity.Collections.NativeArray<int>(length, global::Unity.Collections.Allocator.TempJob, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
				global::Unity.Collections.NativeArray<int> batchDrawCommandOffsets = new global::Unity.Collections.NativeArray<int>(length, global::Unity.Collections.Allocator.TempJob, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
				global::Unity.Collections.NativeArray<int> binAllocCounter = new global::Unity.Collections.NativeArray<int>(16, global::Unity.Collections.Allocator.TempJob);
				global::Unity.Collections.NativeArray<short> binConfigIndices = new global::Unity.Collections.NativeArray<short>(length2, global::Unity.Collections.Allocator.TempJob, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
				global::Unity.Collections.NativeArray<int> binVisibleInstanceCounts = new global::Unity.Collections.NativeArray<int>(length2, global::Unity.Collections.Allocator.TempJob, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
				global::Unity.Collections.NativeArray<int> binVisibleInstanceOffsets = new global::Unity.Collections.NativeArray<int>(length2, global::Unity.Collections.Allocator.TempJob, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
				int contextIndex = -1;
				int num2;
				if (occlusionCullingCommon != null)
				{
					num2 = (occlusionCullingCommon.HasOccluderContext(cc.viewID.GetInstanceID()) ? 1 : 0);
					if (num2 != 0)
					{
						int instanceID = cc.viewID.GetInstanceID();
						contextIndex = m_IndirectStorage.TryAllocateContext(instanceID);
						cullingOutput.customCullingResult[0] = (global::System.IntPtr)instanceID;
					}
				}
				else
				{
					num2 = 0;
				}
				global::UnityEngine.Rendering.IndirectBufferLimits limits = m_IndirectStorage.GetLimits(contextIndex);
				global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.IndirectBufferAllocInfo> allocInfoSubArray = m_IndirectStorage.GetAllocInfoSubArray(contextIndex);
				global::Unity.Jobs.JobHandle jobHandle2 = global::Unity.Jobs.IJobParallelForExtensions.Schedule(new global::UnityEngine.Rendering.AllocateBinsPerBatch
				{
					binningConfig = binningConfig,
					drawBatches = drawInstanceData.drawBatches,
					drawInstanceIndices = drawInstanceData.drawInstanceIndices,
					instanceData = instanceData,
					rendererVisibilityMasks = rendererVisibilityMasks,
					rendererMeshLodSettings = rendererMeshLodSettings,
					batchBinAllocOffsets = batchBinAllocOffsets,
					batchBinCounts = batchBinCounts,
					binAllocCounter = binAllocCounter,
					binConfigIndices = binConfigIndices,
					binVisibleInstanceCounts = binVisibleInstanceCounts,
					splitDebugCounters = m_SplitDebugArray.Counters,
					debugCounterIndexBase = num
				}, length, 1, jobHandle);
				m_SplitDebugArray.AddSync(num, jobHandle2);
				global::Unity.Jobs.JobHandle jobHandle3 = global::Unity.Jobs.IJobParallelForExtensions.Schedule(dependsOn: global::Unity.Jobs.IJobExtensions.Schedule(new global::UnityEngine.Rendering.PrefixSumDrawsAndInstances
				{
					drawRanges = drawInstanceData.drawRanges,
					drawBatchIndices = drawInstanceData.drawBatchIndices,
					batchBinAllocOffsets = batchBinAllocOffsets,
					batchBinCounts = batchBinCounts,
					binVisibleInstanceCounts = binVisibleInstanceCounts,
					batchDrawCommandOffsets = batchDrawCommandOffsets,
					binVisibleInstanceOffsets = binVisibleInstanceOffsets,
					cullingOutput = cullingOutput.drawCommands,
					indirectBufferLimits = limits,
					indirectBufferAllocInfo = allocInfoSubArray,
					indirectAllocationCounters = m_IndirectStorage.allocationCounters
				}, jobHandle2), jobData: new global::UnityEngine.Rendering.DrawCommandOutputPerBatch
				{
					binningConfig = binningConfig,
					batchIDs = batchIDs,
					instanceDataBuffer = instanceDataBuffer,
					drawBatches = drawInstanceData.drawBatches,
					drawInstanceIndices = drawInstanceData.drawInstanceIndices,
					instanceData = instanceData,
					rendererVisibilityMasks = rendererVisibilityMasks,
					rendererMeshLodSettings = rendererMeshLodSettings,
					rendererCrossFadeValues = rendererCrossFadeValues,
					batchBinAllocOffsets = batchBinAllocOffsets,
					batchBinCounts = batchBinCounts,
					batchDrawCommandOffsets = batchDrawCommandOffsets,
					binConfigIndices = binConfigIndices,
					binVisibleInstanceOffsets = binVisibleInstanceOffsets,
					binVisibleInstanceCounts = binVisibleInstanceCounts,
					cullingOutput = cullingOutput.drawCommands,
					indirectBufferLimits = limits,
					visibleInstancesBufferHandle = m_IndirectStorage.visibleInstanceBufferHandle,
					indirectArgsBufferHandle = m_IndirectStorage.indirectDrawArgsBufferHandle,
					indirectBufferAllocInfo = allocInfoSubArray,
					indirectInstanceInfoGlobalArray = m_IndirectStorage.instanceInfoGlobalArray,
					indirectDrawInfoGlobalArray = m_IndirectStorage.drawInfoGlobalArray
				}, arrayLength: length, innerloopBatchCount: 1);
				if (num2 != 0)
				{
					m_IndirectStorage.SetBufferContext(contextIndex, new global::UnityEngine.Rendering.IndirectBufferContext(jobHandle3));
				}
				jobHandle = jobHandle3;
			}
			jobHandle = rendererVisibilityMasks.Dispose(jobHandle);
			jobHandle = rendererCrossFadeValues.Dispose(jobHandle);
			return rendererMeshLodSettings.Dispose(jobHandle);
		}

		private global::Unity.Jobs.JobHandle CreateCompactedVisibilityMaskJob(in global::UnityEngine.Rendering.CPUInstanceData.ReadOnly instanceData, global::Unity.Collections.NativeArray<byte> rendererVisibilityMasks, global::Unity.Jobs.JobHandle cullingJobHandle)
		{
			if (!m_CompactedVisibilityMasks.IsCreated)
			{
				m_CompactedVisibilityMasks = new global::UnityEngine.Rendering.ParallelBitArray(instanceData.handlesLength, global::Unity.Collections.Allocator.TempJob);
			}
			global::Unity.Jobs.JobHandle jobHandle = global::Unity.Jobs.IJobParallelForBatchExtensions.ScheduleBatch(new global::UnityEngine.Rendering.CompactVisibilityMasksJob
			{
				rendererVisibilityMasks = rendererVisibilityMasks,
				compactedVisibilityMasks = m_CompactedVisibilityMasks
			}, rendererVisibilityMasks.Length, 64, cullingJobHandle);
			m_CompactedVisibilityMasksJobsHandle = global::Unity.Jobs.JobHandle.CombineDependencies(m_CompactedVisibilityMasksJobsHandle, jobHandle);
			return jobHandle;
		}

		public void InstanceOccludersUpdated(int viewInstanceID, int subviewMask, global::UnityEngine.Rendering.RenderersBatchersContext batchersContext)
		{
			global::UnityEngine.Rendering.DebugRendererBatcherStats debugStats = m_DebugStats;
			if (debugStats != null && debugStats.enabled && batchersContext.occlusionCullingCommon.GetOccluderContext(viewInstanceID, out var occluderContext))
			{
				m_OcclusionEventDebugArray.TryAdd(viewInstanceID, global::UnityEngine.Rendering.InstanceOcclusionEventType.OccluderUpdate, occluderContext.version, subviewMask, global::UnityEngine.Rendering.OcclusionTest.None);
			}
		}

		private void DisposeCompactVisibilityMasks()
		{
			if (m_CompactedVisibilityMasks.IsCreated)
			{
				m_CompactedVisibilityMasks.Dispose();
			}
		}

		private void DisposeSceneViewHiddenBits()
		{
		}

		public global::UnityEngine.Rendering.ParallelBitArray GetCompactedVisibilityMasks(bool syncCullingJobs)
		{
			if (syncCullingJobs)
			{
				m_CompactedVisibilityMasksJobsHandle.Complete();
			}
			return m_CompactedVisibilityMasks;
		}

		public void InstanceOcclusionTest(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, in global::UnityEngine.Rendering.OcclusionCullingSettings settings, global::System.ReadOnlySpan<global::UnityEngine.Rendering.SubviewOcclusionTest> subviewOcclusionTests, global::UnityEngine.Rendering.RenderersBatchersContext batchersContext)
		{
			if (!batchersContext.occlusionCullingCommon.GetOccluderContext(settings.viewInstanceID, out var occluderContext))
			{
				return;
			}
			global::UnityEngine.Rendering.OccluderHandles occluderHandles = occluderContext.Import(renderGraph);
			if (!occluderHandles.IsValid())
			{
				return;
			}
			global::UnityEngine.Rendering.InstanceCuller.InstanceOcclusionTestPassData passData;
			using global::UnityEngine.Rendering.RenderGraphModule.IComputeRenderGraphBuilder computeRenderGraphBuilder = renderGraph.AddComputePass<global::UnityEngine.Rendering.InstanceCuller.InstanceOcclusionTestPassData>("Instance Occlusion Test", out passData, m_ProfilingSampleInstanceOcclusionTest, ".\\Library\\PackageCache\\com.unity.render-pipelines.core@04ab0eefa0c3\\Runtime\\GPUDriven\\InstanceCuller.cs", 2327);
			computeRenderGraphBuilder.AllowGlobalStateModification(value: true);
			passData.settings = settings;
			passData.subviewSettings = global::UnityEngine.Rendering.InstanceOcclusionTestSubviewSettings.FromSpan(subviewOcclusionTests);
			passData.bufferHandles = m_IndirectStorage.ImportBuffers(renderGraph);
			passData.occluderHandles = occluderHandles;
			passData.bufferHandles.UseForOcclusionTest(computeRenderGraphBuilder);
			passData.occluderHandles.UseForOcclusionTest(computeRenderGraphBuilder);
			computeRenderGraphBuilder.SetRenderFunc(delegate(global::UnityEngine.Rendering.InstanceCuller.InstanceOcclusionTestPassData data, global::UnityEngine.Rendering.RenderGraphModule.ComputeGraphContext context)
			{
				global::UnityEngine.Rendering.GPUResidentBatcher batcher = global::UnityEngine.Rendering.GPUResidentDrawer.instance.batcher;
				batcher.instanceCullingBatcher.culler.AddOcclusionCullingDispatch(context.cmd, in data.settings, in data.subviewSettings, in data.bufferHandles, in data.occluderHandles, batcher.batchersContext);
			});
		}

		internal void EnsureValidOcclusionTestResults(int viewInstanceID)
		{
			int num = m_IndirectStorage.TryGetContextIndex(viewInstanceID);
			if (num >= 0)
			{
				global::UnityEngine.Rendering.IndirectBufferContext bufferContext = m_IndirectStorage.GetBufferContext(num);
				if (bufferContext.bufferState == global::UnityEngine.Rendering.IndirectBufferContext.BufferState.Pending)
				{
					bufferContext.cullingJobHandle.Complete();
				}
				global::UnityEngine.Rendering.IndirectBufferAllocInfo allocInfo = m_IndirectStorage.GetAllocInfo(num);
				if (!allocInfo.IsEmpty())
				{
					global::UnityEngine.Rendering.CommandBuffer commandBuffer = m_CommandBuffer;
					commandBuffer.Clear();
					m_IndirectStorage.CopyFromStaging(commandBuffer, in allocInfo);
					global::UnityEngine.ComputeShader cs = m_OcclusionTestShader.cs;
					m_ShaderVariables[0] = new global::UnityEngine.Rendering.InstanceOcclusionCullerShaderVariables
					{
						_DrawInfoAllocIndex = (uint)allocInfo.drawAllocIndex,
						_DrawInfoCount = (uint)allocInfo.drawCount,
						_InstanceInfoAllocIndex = (uint)(2 * allocInfo.instanceAllocIndex),
						_InstanceInfoCount = (uint)allocInfo.instanceCount,
						_BoundingSphereInstanceDataAddress = 0,
						_DebugCounterIndex = -1,
						_InstanceMultiplierShift = 0
					};
					commandBuffer.SetBufferData(m_ConstantBuffer, m_ShaderVariables);
					commandBuffer.SetComputeConstantBufferParam(cs, global::UnityEngine.Rendering.InstanceCuller.ShaderIDs.InstanceOcclusionCullerShaderVariables, m_ConstantBuffer, 0, m_ConstantBuffer.stride);
					int copyInstancesKernel = m_CopyInstancesKernel;
					commandBuffer.SetComputeBufferParam(cs, copyInstancesKernel, global::UnityEngine.Rendering.InstanceCuller.ShaderIDs._DrawInfo, m_IndirectStorage.drawInfoBuffer);
					commandBuffer.SetComputeBufferParam(cs, copyInstancesKernel, global::UnityEngine.Rendering.InstanceCuller.ShaderIDs._InstanceInfo, m_IndirectStorage.instanceInfoBuffer);
					commandBuffer.SetComputeBufferParam(cs, copyInstancesKernel, global::UnityEngine.Rendering.InstanceCuller.ShaderIDs._DrawArgs, m_IndirectStorage.drawArgsBuffer);
					commandBuffer.SetComputeBufferParam(cs, copyInstancesKernel, global::UnityEngine.Rendering.InstanceCuller.ShaderIDs._InstanceIndices, m_IndirectStorage.instanceBuffer);
					commandBuffer.DispatchCompute(cs, copyInstancesKernel, (allocInfo.instanceCount + 63) / 64, 1, 1);
					global::UnityEngine.Graphics.ExecuteCommandBuffer(commandBuffer);
					commandBuffer.Clear();
				}
			}
		}

		private void AddOcclusionCullingDispatch(global::UnityEngine.Rendering.ComputeCommandBuffer cmd, in global::UnityEngine.Rendering.OcclusionCullingSettings settings, in global::UnityEngine.Rendering.InstanceOcclusionTestSubviewSettings subviewSettings, in global::UnityEngine.Rendering.IndirectBufferContextHandles bufferHandles, in global::UnityEngine.Rendering.OccluderHandles occluderHandles, global::UnityEngine.Rendering.RenderersBatchersContext batchersContext)
		{
			global::UnityEngine.Rendering.OcclusionCullingCommon occlusionCullingCommon = batchersContext.occlusionCullingCommon;
			int num = m_IndirectStorage.TryGetContextIndex(settings.viewInstanceID);
			if (num < 0)
			{
				return;
			}
			global::UnityEngine.Rendering.IndirectBufferContext bufferContext = m_IndirectStorage.GetBufferContext(num);
			global::UnityEngine.Rendering.OccluderContext occluderContext;
			bool flag = occlusionCullingCommon.GetOccluderContext(settings.viewInstanceID, out occluderContext) && (subviewSettings.occluderSubviewMask & occluderContext.subviewValidMask) == subviewSettings.occluderSubviewMask;
			global::UnityEngine.Rendering.IndirectBufferContext.BufferState bufferState = global::UnityEngine.Rendering.IndirectBufferContext.BufferState.Zeroed;
			int occluderVersion = 0;
			int subviewMask = 0;
			switch (settings.occlusionTest)
			{
			case global::UnityEngine.Rendering.OcclusionTest.None:
				bufferState = global::UnityEngine.Rendering.IndirectBufferContext.BufferState.NoOcclusionTest;
				break;
			case global::UnityEngine.Rendering.OcclusionTest.TestAll:
				if (flag)
				{
					bufferState = global::UnityEngine.Rendering.IndirectBufferContext.BufferState.AllInstancesOcclusionTested;
					occluderVersion = occluderContext.version;
					subviewMask = subviewSettings.occluderSubviewMask;
				}
				else
				{
					bufferState = global::UnityEngine.Rendering.IndirectBufferContext.BufferState.NoOcclusionTest;
				}
				break;
			case global::UnityEngine.Rendering.OcclusionTest.TestCulled:
			{
				if (!flag)
				{
					break;
				}
				bool flag2 = true;
				switch (bufferContext.bufferState)
				{
				case global::UnityEngine.Rendering.IndirectBufferContext.BufferState.AllInstancesOcclusionTested:
				case global::UnityEngine.Rendering.IndirectBufferContext.BufferState.OccludedInstancesReTested:
					if (bufferContext.subviewMask != subviewSettings.occluderSubviewMask)
					{
						global::UnityEngine.Debug.Log("Expected an occlusion test of TestCulled to use the same subview mask as the previous occlusion test");
						flag2 = false;
					}
					break;
				case global::UnityEngine.Rendering.IndirectBufferContext.BufferState.Zeroed:
				case global::UnityEngine.Rendering.IndirectBufferContext.BufferState.NoOcclusionTest:
					flag2 = false;
					break;
				default:
					flag2 = false;
					global::UnityEngine.Debug.Log("Expected the previous occlusion test to be TestAll before using TestCulled");
					break;
				}
				if (flag2)
				{
					bufferState = global::UnityEngine.Rendering.IndirectBufferContext.BufferState.OccludedInstancesReTested;
					occluderVersion = occluderContext.version;
					subviewMask = subviewSettings.occluderSubviewMask;
				}
				break;
			}
			}
			if (!bufferContext.Matches(bufferState, occluderVersion, subviewMask))
			{
				bool flag3 = bufferState == global::UnityEngine.Rendering.IndirectBufferContext.BufferState.AllInstancesOcclusionTested;
				bool flag4 = bufferState == global::UnityEngine.Rendering.IndirectBufferContext.BufferState.OccludedInstancesReTested;
				bool num2 = bufferContext.bufferState == global::UnityEngine.Rendering.IndirectBufferContext.BufferState.Pending;
				bool flag5 = bufferState == global::UnityEngine.Rendering.IndirectBufferContext.BufferState.NoOcclusionTest;
				bool flag6 = bufferContext.bufferState != global::UnityEngine.Rendering.IndirectBufferContext.BufferState.Zeroed && !flag5;
				bool flag7 = bufferState != global::UnityEngine.Rendering.IndirectBufferContext.BufferState.Zeroed && !flag5;
				if (num2)
				{
					bufferContext.cullingJobHandle.Complete();
				}
				global::UnityEngine.Rendering.IndirectBufferAllocInfo allocInfo = m_IndirectStorage.GetAllocInfo(num);
				bufferContext.bufferState = bufferState;
				bufferContext.occluderVersion = occluderVersion;
				bufferContext.subviewMask = subviewMask;
				if (!allocInfo.IsEmpty())
				{
					int debugCounterIndex = -1;
					global::UnityEngine.Rendering.DebugRendererBatcherStats debugStats = m_DebugStats;
					if (debugStats != null && debugStats.enabled)
					{
						debugCounterIndex = m_OcclusionEventDebugArray.TryAdd(settings.viewInstanceID, global::UnityEngine.Rendering.InstanceOcclusionEventType.OcclusionTest, occluderVersion, subviewMask, flag3 ? global::UnityEngine.Rendering.OcclusionTest.TestAll : (flag4 ? global::UnityEngine.Rendering.OcclusionTest.TestCulled : global::UnityEngine.Rendering.OcclusionTest.None));
					}
					bool flag8 = false;
					if (flag3 || flag4)
					{
						flag8 = global::UnityEngine.Rendering.OcclusionCullingCommon.UseOcclusionDebug(in occluderContext) && occluderHandles.occlusionDebugOverlay.IsValid();
					}
					global::UnityEngine.ComputeShader cs = m_OcclusionTestShader.cs;
					global::UnityEngine.Rendering.LocalKeyword keyword = new global::UnityEngine.Rendering.LocalKeyword(cs, "OCCLUSION_FIRST_PASS");
					global::UnityEngine.Rendering.LocalKeyword keyword2 = new global::UnityEngine.Rendering.LocalKeyword(cs, "OCCLUSION_SECOND_PASS");
					global::UnityEngine.Rendering.OccluderContext.SetKeyword(cmd, cs, in keyword, flag3);
					global::UnityEngine.Rendering.OccluderContext.SetKeyword(cmd, cs, in keyword2, flag4);
					m_ShaderVariables[0] = new global::UnityEngine.Rendering.InstanceOcclusionCullerShaderVariables
					{
						_DrawInfoAllocIndex = (uint)allocInfo.drawAllocIndex,
						_DrawInfoCount = (uint)allocInfo.drawCount,
						_InstanceInfoAllocIndex = (uint)(2 * allocInfo.instanceAllocIndex),
						_InstanceInfoCount = (uint)allocInfo.instanceCount,
						_BoundingSphereInstanceDataAddress = batchersContext.renderersParameters.boundingSphere.gpuAddress,
						_DebugCounterIndex = debugCounterIndex,
						_InstanceMultiplierShift = ((settings.instanceMultiplier == 2) ? 1 : 0)
					};
					cmd.SetBufferData(m_ConstantBuffer, m_ShaderVariables);
					cmd.SetComputeConstantBufferParam(cs, global::UnityEngine.Rendering.InstanceCuller.ShaderIDs.InstanceOcclusionCullerShaderVariables, m_ConstantBuffer, 0, m_ConstantBuffer.stride);
					occlusionCullingCommon.PrepareCulling(cmd, in occluderContext, in settings, in subviewSettings, in m_OcclusionTestShader, flag8);
					if (flag5)
					{
						int copyInstancesKernel = m_CopyInstancesKernel;
						cmd.SetComputeBufferParam(cs, copyInstancesKernel, global::UnityEngine.Rendering.InstanceCuller.ShaderIDs._DrawInfo, bufferHandles.drawInfoBuffer);
						cmd.SetComputeBufferParam(cs, copyInstancesKernel, global::UnityEngine.Rendering.InstanceCuller.ShaderIDs._InstanceInfo, bufferHandles.instanceInfoBuffer);
						cmd.SetComputeBufferParam(cs, copyInstancesKernel, global::UnityEngine.Rendering.InstanceCuller.ShaderIDs._DrawArgs, bufferHandles.drawArgsBuffer);
						cmd.SetComputeBufferParam(cs, copyInstancesKernel, global::UnityEngine.Rendering.InstanceCuller.ShaderIDs._InstanceIndices, bufferHandles.instanceBuffer);
						cmd.DispatchCompute(cs, copyInstancesKernel, (allocInfo.instanceCount + 63) / 64, 1, 1);
					}
					if (flag6)
					{
						int resetDrawArgsKernel = m_ResetDrawArgsKernel;
						cmd.SetComputeBufferParam(cs, resetDrawArgsKernel, global::UnityEngine.Rendering.InstanceCuller.ShaderIDs._DrawInfo, bufferHandles.drawInfoBuffer);
						cmd.SetComputeBufferParam(cs, resetDrawArgsKernel, global::UnityEngine.Rendering.InstanceCuller.ShaderIDs._DrawArgs, bufferHandles.drawArgsBuffer);
						if (flag4)
						{
							cmd.SetComputeBufferParam(cs, resetDrawArgsKernel, global::UnityEngine.Rendering.InstanceCuller.ShaderIDs._DispatchArgs, bufferHandles.dispatchArgsBuffer);
						}
						cmd.DispatchCompute(cs, resetDrawArgsKernel, (allocInfo.drawCount + 63) / 64, 1, 1);
					}
					if (flag7)
					{
						int cullInstancesKernel = m_CullInstancesKernel;
						cmd.SetComputeBufferParam(cs, cullInstancesKernel, global::UnityEngine.Rendering.InstanceCuller.ShaderIDs._DrawInfo, bufferHandles.drawInfoBuffer);
						cmd.SetComputeBufferParam(cs, cullInstancesKernel, global::UnityEngine.Rendering.InstanceCuller.ShaderIDs._InstanceInfo, bufferHandles.instanceInfoBuffer);
						cmd.SetComputeBufferParam(cs, cullInstancesKernel, global::UnityEngine.Rendering.InstanceCuller.ShaderIDs._DrawArgs, bufferHandles.drawArgsBuffer);
						cmd.SetComputeBufferParam(cs, cullInstancesKernel, global::UnityEngine.Rendering.InstanceCuller.ShaderIDs._InstanceIndices, bufferHandles.instanceBuffer);
						cmd.SetComputeBufferParam(cs, cullInstancesKernel, global::UnityEngine.Rendering.InstanceCuller.ShaderIDs._InstanceDataBuffer, batchersContext.gpuInstanceDataBuffer);
						cmd.SetComputeBufferParam(cs, cullInstancesKernel, global::UnityEngine.Rendering.InstanceCuller.ShaderIDs._OcclusionDebugCounters, m_OcclusionEventDebugArray.CounterBuffer);
						if (flag3 || flag4)
						{
							global::UnityEngine.Rendering.OcclusionCullingCommon.SetDepthPyramid(cmd, in m_OcclusionTestShader, cullInstancesKernel, in occluderHandles);
						}
						if (flag8)
						{
							global::UnityEngine.Rendering.OcclusionCullingCommon.SetDebugPyramid(cmd, in m_OcclusionTestShader, cullInstancesKernel, in occluderHandles);
						}
						if (flag4)
						{
							cmd.DispatchCompute(cs, cullInstancesKernel, bufferHandles.dispatchArgsBuffer, 0u);
						}
						else
						{
							cmd.DispatchCompute(cs, cullInstancesKernel, (allocInfo.instanceCount + 63) / 64, 1, 1);
						}
					}
				}
			}
			m_IndirectStorage.SetBufferContext(num, bufferContext);
		}

		private void FlushDebugCounters()
		{
			global::UnityEngine.Rendering.DebugRendererBatcherStats debugStats = m_DebugStats;
			if (debugStats != null && debugStats.enabled)
			{
				m_SplitDebugArray.MoveToDebugStatsAndClear(m_DebugStats);
				m_OcclusionEventDebugArray.MoveToDebugStatsAndClear(m_DebugStats);
				m_DebugStats.FinalizeInstanceCullerViewStats();
			}
		}

		private void OnBeginSceneViewCameraRendering()
		{
		}

		private void OnEndSceneViewCameraRendering()
		{
		}

		public void UpdateFrame(int cameraCount)
		{
			DisposeSceneViewHiddenBits();
			DisposeCompactVisibilityMasks();
			if (cameraCount > m_LODParamsToCameraID.Capacity)
			{
				m_LODParamsToCameraID.Capacity = cameraCount;
			}
			m_LODParamsToCameraID.Clear();
			FlushDebugCounters();
			m_IndirectStorage.ClearContextsAndGrowBuffers();
		}

		public void OnBeginCameraRendering(global::UnityEngine.Camera camera)
		{
			if (camera.cameraType == global::UnityEngine.CameraType.SceneView)
			{
				OnBeginSceneViewCameraRendering();
			}
		}

		public void OnEndCameraRendering(global::UnityEngine.Camera camera)
		{
			if (camera.cameraType == global::UnityEngine.CameraType.SceneView)
			{
				OnEndSceneViewCameraRendering();
			}
		}

		public void Dispose()
		{
			DisposeSceneViewHiddenBits();
			DisposeCompactVisibilityMasks();
			m_IndirectStorage.Dispose();
			m_DebugStats = null;
			m_OcclusionEventDebugArray.Dispose();
			m_SplitDebugArray.Dispose();
			m_ShaderVariables.Dispose();
			m_ConstantBuffer.Release();
			m_CommandBuffer.Dispose();
			m_LODParamsToCameraID.Dispose();
		}
	}
}
