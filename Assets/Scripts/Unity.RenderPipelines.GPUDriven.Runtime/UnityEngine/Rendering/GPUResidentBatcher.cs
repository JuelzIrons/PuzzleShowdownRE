namespace UnityEngine.Rendering
{
	internal class GPUResidentBatcher : global::System.IDisposable
	{
		private global::UnityEngine.Rendering.RenderersBatchersContext m_BatchersContext;

		private global::UnityEngine.Rendering.GPUDrivenProcessor m_GPUDrivenProcessor;

		private global::UnityEngine.Rendering.GPUDrivenRendererDataCallback m_UpdateRendererInstancesAndBatchesCallback;

		private global::UnityEngine.Rendering.GPUDrivenRendererDataCallback m_UpdateRendererBatchesCallback;

		private global::UnityEngine.Rendering.InstanceCullingBatcher m_InstanceCullingBatcher;

		private global::UnityEngine.Rendering.ParallelBitArray m_ProcessedThisFrameTreeBits;

		internal global::UnityEngine.Rendering.RenderersBatchersContext batchersContext => m_BatchersContext;

		internal global::UnityEngine.Rendering.OcclusionCullingCommon occlusionCullingCommon => m_BatchersContext.occlusionCullingCommon;

		internal global::UnityEngine.Rendering.InstanceCullingBatcher instanceCullingBatcher => m_InstanceCullingBatcher;

		public GPUResidentBatcher(global::UnityEngine.Rendering.RenderersBatchersContext batcherContext, global::UnityEngine.Rendering.InstanceCullingBatcherDesc instanceCullerBatcherDesc, global::UnityEngine.Rendering.GPUDrivenProcessor gpuDrivenProcessor)
		{
			m_BatchersContext = batcherContext;
			m_GPUDrivenProcessor = gpuDrivenProcessor;
			m_UpdateRendererInstancesAndBatchesCallback = UpdateRendererInstancesAndBatches;
			m_UpdateRendererBatchesCallback = UpdateRendererBatches;
			m_InstanceCullingBatcher = new global::UnityEngine.Rendering.InstanceCullingBatcher(batcherContext, instanceCullerBatcherDesc, OnFinishedCulling);
		}

		public void Dispose()
		{
			m_GPUDrivenProcessor.ClearMaterialFilters();
			m_InstanceCullingBatcher.Dispose();
			if (m_ProcessedThisFrameTreeBits.IsCreated)
			{
				m_ProcessedThisFrameTreeBits.Dispose();
			}
		}

		public void OnBeginContextRendering()
		{
			if (m_ProcessedThisFrameTreeBits.IsCreated)
			{
				m_ProcessedThisFrameTreeBits.Dispose();
			}
		}

		public void OnEndContextRendering()
		{
			m_InstanceCullingBatcher?.OnEndContextRendering();
		}

		public void OnBeginCameraRendering(global::UnityEngine.Camera camera)
		{
			m_InstanceCullingBatcher?.OnBeginCameraRendering(camera);
		}

		public void OnEndCameraRendering(global::UnityEngine.Camera camera)
		{
			m_InstanceCullingBatcher?.OnEndCameraRendering(camera);
		}

		public void UpdateFrame()
		{
			m_InstanceCullingBatcher.UpdateFrame();
			m_BatchersContext.UpdateFrame();
		}

		public void DestroyMaterials(global::Unity.Collections.NativeArray<global::UnityEngine.EntityId> destroyedMaterials)
		{
			m_InstanceCullingBatcher.DestroyMaterials(destroyedMaterials);
		}

		public void DestroyDrawInstances(global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.InstanceHandle> instances)
		{
			m_InstanceCullingBatcher.DestroyDrawInstances(instances);
		}

		public void DestroyMeshes(global::Unity.Collections.NativeArray<global::UnityEngine.EntityId> destroyedMeshes)
		{
			m_InstanceCullingBatcher.DestroyMeshes(destroyedMeshes);
		}

		internal void FreeRendererGroupInstances(global::Unity.Collections.NativeArray<global::UnityEngine.EntityId> rendererGroupIDs)
		{
			if (rendererGroupIDs.Length != 0)
			{
				global::Unity.Collections.NativeList<global::UnityEngine.Rendering.InstanceHandle> instances = new global::Unity.Collections.NativeList<global::UnityEngine.Rendering.InstanceHandle>(rendererGroupIDs.Length, global::Unity.Collections.Allocator.TempJob);
				m_BatchersContext.ScheduleQueryRendererGroupInstancesJob(rendererGroupIDs, instances).Complete();
				DestroyDrawInstances(instances.AsArray());
				instances.Dispose();
				m_BatchersContext.FreeRendererGroupInstances(rendererGroupIDs);
			}
		}

		public void InstanceOcclusionTest(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, in global::UnityEngine.Rendering.OcclusionCullingSettings settings, global::System.ReadOnlySpan<global::UnityEngine.Rendering.SubviewOcclusionTest> subviewOcclusionTests)
		{
			if (m_BatchersContext.hasBoundingSpheres)
			{
				m_InstanceCullingBatcher.culler.InstanceOcclusionTest(renderGraph, in settings, subviewOcclusionTests, m_BatchersContext);
			}
		}

		public void UpdateInstanceOccluders(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, in global::UnityEngine.Rendering.OccluderParameters occluderParams, global::System.ReadOnlySpan<global::UnityEngine.Rendering.OccluderSubviewUpdate> occluderSubviewUpdates)
		{
			if (m_BatchersContext.hasBoundingSpheres)
			{
				m_BatchersContext.occlusionCullingCommon.UpdateInstanceOccluders(renderGraph, in occluderParams, occluderSubviewUpdates);
			}
		}

		public void UpdateRenderers(global::Unity.Collections.NativeArray<global::UnityEngine.EntityId> renderersID, bool materialUpdateOnly = false)
		{
			if (renderersID.Length != 0)
			{
				m_GPUDrivenProcessor.enablePartialRendering = false;
				m_GPUDrivenProcessor.EnableGPUDrivenRenderingAndDispatchRendererData(renderersID, materialUpdateOnly ? m_UpdateRendererBatchesCallback : m_UpdateRendererInstancesAndBatchesCallback, materialUpdateOnly);
				m_GPUDrivenProcessor.enablePartialRendering = false;
			}
		}

		public global::Unity.Jobs.JobHandle SchedulePackedMaterialCacheUpdate(global::Unity.Collections.NativeArray<global::UnityEngine.EntityId> materialIDs, global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.GPUDrivenPackedMaterialData> packedMaterialDatas)
		{
			return m_InstanceCullingBatcher.SchedulePackedMaterialCacheUpdate(materialIDs, packedMaterialDatas);
		}

		public void PostCullBeginCameraRendering(global::UnityEngine.Rendering.RenderRequestBatcherContext context)
		{
			m_InstanceCullingBatcher.PostCullBeginCameraRendering(context);
		}

		public void OnSetupAmbientProbe()
		{
			m_BatchersContext.UpdateAmbientProbeAndGpuBuffer(forceUpdate: false);
		}

		private void UpdateRendererInstancesAndBatches(in global::UnityEngine.Rendering.GPUDrivenRendererGroupData rendererData, global::System.Collections.Generic.IList<global::UnityEngine.Mesh> meshes, global::System.Collections.Generic.IList<global::UnityEngine.Material> materials)
		{
			FreeRendererGroupInstances(rendererData.invalidRendererGroupID);
			if (rendererData.rendererGroupID.Length != 0)
			{
				global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.InstanceHandle> instances = new global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.InstanceHandle>(rendererData.localToWorldMatrix.Length, global::Unity.Collections.Allocator.TempJob, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
				m_BatchersContext.ReallocateAndGetInstances(in rendererData, instances);
				global::Unity.Jobs.JobHandle jobHandle = m_BatchersContext.ScheduleUpdateInstanceDataJob(instances, in rendererData);
				global::UnityEngine.Rendering.GPUInstanceDataBufferUploader uploader = m_BatchersContext.CreateDataBufferUploader(instances.Length, global::UnityEngine.Rendering.InstanceType.MeshRenderer);
				uploader.AllocateUploadHandles(instances.Length);
				global::Unity.Jobs.JobHandle job = uploader.WriteInstanceDataJob(m_BatchersContext.renderersParameters.lightmapScale.index, rendererData.lightmapScaleOffset, rendererData.rendererGroupIndex);
				global::Unity.Jobs.JobHandle job2 = uploader.WriteInstanceDataJob(m_BatchersContext.renderersParameters.rendererUserValues.index, rendererData.rendererUserValues, rendererData.rendererGroupIndex);
				global::Unity.Jobs.JobHandle.CombineDependencies(job, job2).Complete();
				m_BatchersContext.SubmitToGpu(instances, ref uploader, submitOnlyWrittenParams: true);
				m_BatchersContext.ChangeInstanceBufferVersion();
				uploader.Dispose();
				jobHandle.Complete();
				m_BatchersContext.InitializeInstanceTransforms(instances, rendererData.localToWorldMatrix, rendererData.prevLocalToWorldMatrix);
				m_InstanceCullingBatcher.BuildBatch(instances, in rendererData, registerMaterialsAndMeshes: true);
				instances.Dispose();
			}
		}

		private void UpdateRendererBatches(in global::UnityEngine.Rendering.GPUDrivenRendererGroupData rendererData, global::System.Collections.Generic.IList<global::UnityEngine.Mesh> meshes, global::System.Collections.Generic.IList<global::UnityEngine.Material> materials)
		{
			if (rendererData.rendererGroupID.Length != 0)
			{
				global::Unity.Collections.NativeList<global::UnityEngine.Rendering.InstanceHandle> instances = new global::Unity.Collections.NativeList<global::UnityEngine.Rendering.InstanceHandle>(rendererData.localToWorldMatrix.Length, global::Unity.Collections.Allocator.TempJob);
				m_BatchersContext.ScheduleQueryRendererGroupInstancesJob(rendererData.rendererGroupID, instances).Complete();
				m_InstanceCullingBatcher.BuildBatch(instances.AsArray(), in rendererData, registerMaterialsAndMeshes: false);
				instances.Dispose();
			}
		}

		private void OnFinishedCulling(global::System.IntPtr customCullingResult)
		{
			ProcessTrees();
			m_InstanceCullingBatcher.OnFinishedCulling(customCullingResult);
		}

		private void ProcessTrees()
		{
			if (m_BatchersContext.GetAliveInstancesOfType(global::UnityEngine.Rendering.InstanceType.SpeedTree) == 0)
			{
				return;
			}
			global::UnityEngine.Rendering.ParallelBitArray compactedVisibilityMasks = m_InstanceCullingBatcher.GetCompactedVisibilityMasks(syncCullingJobs: false);
			if (!compactedVisibilityMasks.IsCreated)
			{
				return;
			}
			int length = m_BatchersContext.aliveInstances.Length;
			if (!m_ProcessedThisFrameTreeBits.IsCreated)
			{
				m_ProcessedThisFrameTreeBits = new global::UnityEngine.Rendering.ParallelBitArray(length, global::Unity.Collections.Allocator.TempJob);
			}
			else if (m_ProcessedThisFrameTreeBits.Length < length)
			{
				m_ProcessedThisFrameTreeBits.Resize(length);
			}
			bool becomeVisibleOnly = !global::UnityEngine.Application.isPlaying;
			global::Unity.Collections.NativeList<int> visibeTreeRendererIDs = new global::Unity.Collections.NativeList<int>(global::Unity.Collections.Allocator.TempJob);
			global::Unity.Collections.NativeList<global::UnityEngine.Rendering.InstanceHandle> visibeTreeInstances = new global::Unity.Collections.NativeList<global::UnityEngine.Rendering.InstanceHandle>(global::Unity.Collections.Allocator.TempJob);
			m_BatchersContext.GetVisibleTreeInstances(in compactedVisibilityMasks, in m_ProcessedThisFrameTreeBits, visibeTreeRendererIDs, visibeTreeInstances, becomeVisibleOnly, out var becomeVisibeTreeInstancesCount);
			if (visibeTreeRendererIDs.Length > 0)
			{
				global::Unity.Collections.NativeArray<int> subArray = visibeTreeRendererIDs.AsArray().GetSubArray(0, becomeVisibeTreeInstancesCount);
				global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.InstanceHandle> subArray2 = visibeTreeInstances.AsArray().GetSubArray(0, becomeVisibeTreeInstancesCount);
				if (subArray.Length > 0)
				{
					UpdateSpeedTreeWindAndUploadWindParamsToGPU(subArray, subArray2, history: true);
				}
				UpdateSpeedTreeWindAndUploadWindParamsToGPU(visibeTreeRendererIDs.AsArray(), visibeTreeInstances.AsArray(), history: false);
			}
			visibeTreeRendererIDs.Dispose();
			visibeTreeInstances.Dispose();
		}

		private unsafe void UpdateSpeedTreeWindAndUploadWindParamsToGPU(global::Unity.Collections.NativeArray<int> treeRendererIDs, global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.InstanceHandle> treeInstances, bool history)
		{
			if (treeRendererIDs.Length != 0)
			{
				global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.GPUInstanceIndex> gpuInstanceIndices = new global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.GPUInstanceIndex>(treeInstances.Length, global::Unity.Collections.Allocator.TempJob, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
				m_BatchersContext.instanceDataBuffer.CPUInstanceArrayToGPUInstanceArray(treeInstances, gpuInstanceIndices);
				if (!history)
				{
					m_BatchersContext.UpdateInstanceWindDataHistory(gpuInstanceIndices);
				}
				global::UnityEngine.Rendering.GPUInstanceDataBufferUploader uploader = m_BatchersContext.CreateDataBufferUploader(treeInstances.Length, global::UnityEngine.Rendering.InstanceType.SpeedTree);
				uploader.AllocateUploadHandles(treeInstances.Length);
				global::UnityEngine.Rendering.SpeedTreeWindParamsBufferIterator windParams = new global::UnityEngine.Rendering.SpeedTreeWindParamsBufferIterator
				{
					bufferPtr = uploader.GetUploadBufferPtr()
				};
				for (int i = 0; i < 16; i++)
				{
					windParams.uintParamOffsets[i] = uploader.PrepareParamWrite<global::UnityEngine.Vector4>(m_BatchersContext.renderersParameters.windParams[i].index);
				}
				windParams.uintStride = uploader.GetUIntPerInstance();
				windParams.elementOffset = 0;
				windParams.elementsCount = treeInstances.Length;
				global::UnityEngine.Rendering.SpeedTreeWindManager.UpdateWindAndWriteBufferWindParams(treeRendererIDs, windParams, history);
				m_BatchersContext.SubmitToGpu(gpuInstanceIndices, ref uploader, submitOnlyWrittenParams: true);
				gpuInstanceIndices.Dispose();
				uploader.Dispose();
			}
		}
	}
}
