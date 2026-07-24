namespace UnityEngine.Rendering
{
	internal class RenderersBatchersContext : global::System.IDisposable
	{
		private global::UnityEngine.Rendering.InstanceDataSystem m_InstanceDataSystem;

		private global::UnityEngine.Rendering.GPUResidentDrawerResources m_Resources;

		private global::UnityEngine.Rendering.GPUDrivenProcessor m_GPUDrivenProcessor;

		private global::UnityEngine.Rendering.LODGroupDataPool m_LODGroupDataPool;

		internal global::UnityEngine.Rendering.GPUInstanceDataBuffer m_InstanceDataBuffer;

		private global::UnityEngine.Rendering.RenderersParameters m_RenderersParameters;

		private global::UnityEngine.Rendering.GPUInstanceDataBufferUploader.GPUResources m_UploadResources;

		private global::UnityEngine.Rendering.GPUInstanceDataBufferGrower.GPUResources m_GrowerResources;

		internal global::UnityEngine.Rendering.CommandBuffer m_CmdBuffer;

		private global::UnityEngine.Rendering.SphericalHarmonicsL2 m_CachedAmbientProbe;

		private float m_SmallMeshScreenPercentage;

		private global::UnityEngine.Rendering.GPUDrivenLODGroupDataCallback m_UpdateLODGroupCallback;

		private global::UnityEngine.Rendering.GPUDrivenLODGroupDataCallback m_TransformLODGroupCallback;

		private global::UnityEngine.Rendering.OcclusionCullingCommon m_OcclusionCullingCommon;

		private global::UnityEngine.Rendering.DebugRendererBatcherStats m_DebugStats;

		public global::UnityEngine.Rendering.RenderersParameters renderersParameters => m_RenderersParameters;

		public global::UnityEngine.GraphicsBuffer gpuInstanceDataBuffer => m_InstanceDataBuffer.gpuBuffer;

		public int activeLodGroupCount => m_LODGroupDataPool.activeLodGroupCount;

		public global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.GPUInstanceComponentDesc>.ReadOnly defaultDescriptions => m_InstanceDataBuffer.descriptions.AsReadOnly();

		public global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.MetadataValue> defaultMetadata => m_InstanceDataBuffer.defaultMetadata;

		public global::Unity.Collections.NativeList<global::UnityEngine.Rendering.LODGroupCullingData> lodGroupCullingData => m_LODGroupDataPool.lodGroupCullingData;

		public int instanceDataBufferVersion => m_InstanceDataBuffer.version;

		public int instanceDataBufferLayoutVersion => m_InstanceDataBuffer.layoutVersion;

		public global::UnityEngine.Rendering.SphericalHarmonicsL2 cachedAmbientProbe => m_CachedAmbientProbe;

		public bool hasBoundingSpheres => m_InstanceDataSystem.hasBoundingSpheres;

		public int cameraCount => m_InstanceDataSystem.cameraCount;

		public global::UnityEngine.Rendering.CPUInstanceData.ReadOnly instanceData => m_InstanceDataSystem.instanceData;

		public global::UnityEngine.Rendering.CPUSharedInstanceData.ReadOnly sharedInstanceData => m_InstanceDataSystem.sharedInstanceData;

		public global::UnityEngine.Rendering.CPUPerCameraInstanceData perCameraInstanceData => m_InstanceDataSystem.perCameraInstanceData;

		public global::UnityEngine.Rendering.GPUInstanceDataBuffer.ReadOnly instanceDataBuffer => m_InstanceDataBuffer.AsReadOnly();

		public global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.InstanceHandle> aliveInstances => m_InstanceDataSystem.aliveInstances;

		public float smallMeshScreenPercentage => m_SmallMeshScreenPercentage;

		public global::UnityEngine.Rendering.GPUResidentDrawerResources resources => m_Resources;

		internal global::UnityEngine.Rendering.OcclusionCullingCommon occlusionCullingCommon => m_OcclusionCullingCommon;

		internal global::UnityEngine.Rendering.DebugRendererBatcherStats debugStats => m_DebugStats;

		public RenderersBatchersContext(in global::UnityEngine.Rendering.RenderersBatchersContextDesc desc, global::UnityEngine.Rendering.GPUDrivenProcessor gpuDrivenProcessor, global::UnityEngine.Rendering.GPUResidentDrawerResources resources)
		{
			m_Resources = resources;
			m_GPUDrivenProcessor = gpuDrivenProcessor;
			global::UnityEngine.Rendering.RenderersParameters.Flags flags = global::UnityEngine.Rendering.RenderersParameters.Flags.None;
			if (desc.enableBoundingSpheresInstanceData)
			{
				flags |= global::UnityEngine.Rendering.RenderersParameters.Flags.UseBoundingSphereParameter;
			}
			m_InstanceDataBuffer = global::UnityEngine.Rendering.RenderersParameters.CreateInstanceDataBuffer(flags, in desc.instanceNumInfo);
			m_RenderersParameters = new global::UnityEngine.Rendering.RenderersParameters(in m_InstanceDataBuffer);
			m_LODGroupDataPool = new global::UnityEngine.Rendering.LODGroupDataPool(resources, desc.instanceNumInfo.GetInstanceNum(global::UnityEngine.Rendering.InstanceType.MeshRenderer), desc.supportDitheringCrossFade);
			m_UploadResources = default(global::UnityEngine.Rendering.GPUInstanceDataBufferUploader.GPUResources);
			m_UploadResources.LoadShaders(resources);
			m_GrowerResources = default(global::UnityEngine.Rendering.GPUInstanceDataBufferGrower.GPUResources);
			m_GrowerResources.LoadShaders(resources);
			m_CmdBuffer = new global::UnityEngine.Rendering.CommandBuffer();
			m_CmdBuffer.name = "GPUCullingCommands";
			m_CachedAmbientProbe = global::UnityEngine.RenderSettings.ambientProbe;
			m_InstanceDataSystem = new global::UnityEngine.Rendering.InstanceDataSystem(desc.instanceNumInfo.GetTotalInstanceNum(), desc.enableBoundingSpheresInstanceData, resources);
			m_SmallMeshScreenPercentage = desc.smallMeshScreenPercentage;
			m_UpdateLODGroupCallback = UpdateLODGroupData;
			m_TransformLODGroupCallback = TransformLODGroupData;
			m_OcclusionCullingCommon = new global::UnityEngine.Rendering.OcclusionCullingCommon();
			m_OcclusionCullingCommon.Init(resources);
			m_DebugStats = (desc.enableCullerDebugStats ? new global::UnityEngine.Rendering.DebugRendererBatcherStats() : null);
		}

		public void Dispose()
		{
			global::Unity.Collections.NativeArray<global::UnityEngine.EntityId>.ReadOnly source = m_InstanceDataSystem.sharedInstanceData.rendererGroupIDs;
			if (source.Length > 0)
			{
				m_GPUDrivenProcessor.DisableGPUDrivenRendering(source);
			}
			m_InstanceDataSystem.Dispose();
			m_CmdBuffer.Release();
			m_GrowerResources.Dispose();
			m_UploadResources.Dispose();
			m_LODGroupDataPool.Dispose();
			m_InstanceDataBuffer.Dispose();
			m_UpdateLODGroupCallback = null;
			m_TransformLODGroupCallback = null;
			m_DebugStats?.Dispose();
			m_DebugStats = null;
			m_OcclusionCullingCommon?.Dispose();
			m_OcclusionCullingCommon = null;
		}

		public int GetMaxInstancesOfType(global::UnityEngine.Rendering.InstanceType instanceType)
		{
			return m_InstanceDataSystem.GetMaxInstancesOfType(instanceType);
		}

		public int GetAliveInstancesOfType(global::UnityEngine.Rendering.InstanceType instanceType)
		{
			return m_InstanceDataSystem.GetAliveInstancesOfType(instanceType);
		}

		public void GrowInstanceBuffer(in global::UnityEngine.Rendering.InstanceNumInfo instanceNumInfo)
		{
			using (global::UnityEngine.Rendering.GPUInstanceDataBufferGrower gPUInstanceDataBufferGrower = new global::UnityEngine.Rendering.GPUInstanceDataBufferGrower(m_InstanceDataBuffer, in instanceNumInfo))
			{
				global::UnityEngine.Rendering.GPUInstanceDataBuffer gPUInstanceDataBuffer = gPUInstanceDataBufferGrower.SubmitToGpu(ref m_GrowerResources);
				if (gPUInstanceDataBuffer != m_InstanceDataBuffer)
				{
					if (m_InstanceDataBuffer != null)
					{
						m_InstanceDataBuffer.Dispose();
					}
					m_InstanceDataBuffer = gPUInstanceDataBuffer;
				}
			}
			m_RenderersParameters = new global::UnityEngine.Rendering.RenderersParameters(in m_InstanceDataBuffer);
		}

		private void EnsureInstanceBufferCapacity()
		{
			int maxInstancesOfType = m_InstanceDataSystem.GetMaxInstancesOfType(global::UnityEngine.Rendering.InstanceType.MeshRenderer);
			int maxInstancesOfType2 = m_InstanceDataSystem.GetMaxInstancesOfType(global::UnityEngine.Rendering.InstanceType.SpeedTree);
			int num = m_InstanceDataBuffer.instanceNumInfo.GetInstanceNum(global::UnityEngine.Rendering.InstanceType.MeshRenderer);
			int num2 = m_InstanceDataBuffer.instanceNumInfo.GetInstanceNum(global::UnityEngine.Rendering.InstanceType.SpeedTree);
			bool flag = false;
			if (maxInstancesOfType > num)
			{
				flag = true;
				num = maxInstancesOfType + 1024;
			}
			if (maxInstancesOfType2 > num2)
			{
				flag = true;
				num2 = maxInstancesOfType2 + 256;
			}
			if (flag)
			{
				GrowInstanceBuffer(new global::UnityEngine.Rendering.InstanceNumInfo(num, num2));
			}
		}

		private void UpdateLODGroupData(in global::UnityEngine.Rendering.GPUDrivenLODGroupData lodGroupData)
		{
			m_LODGroupDataPool.UpdateLODGroupData(in lodGroupData);
		}

		private void TransformLODGroupData(in global::UnityEngine.Rendering.GPUDrivenLODGroupData lodGroupData)
		{
			m_LODGroupDataPool.UpdateLODGroupTransformData(in lodGroupData);
		}

		public void DestroyLODGroups(global::Unity.Collections.NativeArray<global::UnityEngine.EntityId> destroyed)
		{
			if (destroyed.Length != 0)
			{
				m_LODGroupDataPool.FreeLODGroupData(destroyed);
			}
		}

		public void UpdateLODGroups(global::Unity.Collections.NativeArray<global::UnityEngine.EntityId> changedID)
		{
			if (changedID.Length != 0)
			{
				m_GPUDrivenProcessor.DispatchLODGroupData(changedID, m_UpdateLODGroupCallback);
			}
		}

		public void ReallocateAndGetInstances(in global::UnityEngine.Rendering.GPUDrivenRendererGroupData rendererData, global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.InstanceHandle> instances)
		{
			m_InstanceDataSystem.ReallocateAndGetInstances(in rendererData, instances);
			EnsureInstanceBufferCapacity();
		}

		public global::Unity.Jobs.JobHandle ScheduleUpdateInstanceDataJob(global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.InstanceHandle> instances, in global::UnityEngine.Rendering.GPUDrivenRendererGroupData rendererData)
		{
			return m_InstanceDataSystem.ScheduleUpdateInstanceDataJob(instances, in rendererData, m_LODGroupDataPool.lodGroupDataHash);
		}

		public void FreeRendererGroupInstances(global::Unity.Collections.NativeArray<global::UnityEngine.EntityId> rendererGroupsID)
		{
			m_InstanceDataSystem.FreeRendererGroupInstances(rendererGroupsID);
		}

		public void FreeInstances(global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.InstanceHandle> instances)
		{
			m_InstanceDataSystem.FreeInstances(instances);
		}

		public global::Unity.Jobs.JobHandle ScheduleQueryRendererGroupInstancesJob(global::Unity.Collections.NativeArray<global::UnityEngine.EntityId> rendererGroupIDs, global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.InstanceHandle> instances)
		{
			return m_InstanceDataSystem.ScheduleQueryRendererGroupInstancesJob(rendererGroupIDs, instances);
		}

		public global::Unity.Jobs.JobHandle ScheduleQueryRendererGroupInstancesJob(global::Unity.Collections.NativeArray<global::UnityEngine.EntityId> rendererGroupIDs, global::Unity.Collections.NativeList<global::UnityEngine.Rendering.InstanceHandle> instances)
		{
			return m_InstanceDataSystem.ScheduleQueryRendererGroupInstancesJob(rendererGroupIDs, instances);
		}

		public global::Unity.Jobs.JobHandle ScheduleQueryRendererGroupInstancesJob(global::Unity.Collections.NativeArray<global::UnityEngine.EntityId> rendererGroupIDs, global::Unity.Collections.NativeArray<int> instancesOffset, global::Unity.Collections.NativeArray<int> instancesCount, global::Unity.Collections.NativeList<global::UnityEngine.Rendering.InstanceHandle> instances)
		{
			return m_InstanceDataSystem.ScheduleQueryRendererGroupInstancesJob(rendererGroupIDs, instancesOffset, instancesCount, instances);
		}

		public global::Unity.Jobs.JobHandle ScheduleQueryMeshInstancesJob(global::Unity.Collections.NativeArray<global::UnityEngine.EntityId> sortedMeshIDs, global::Unity.Collections.NativeList<global::UnityEngine.Rendering.InstanceHandle> instances)
		{
			return m_InstanceDataSystem.ScheduleQuerySortedMeshInstancesJob(sortedMeshIDs, instances);
		}

		public void ChangeInstanceBufferVersion()
		{
			m_InstanceDataBuffer.version++;
		}

		public global::UnityEngine.Rendering.GPUInstanceDataBufferUploader CreateDataBufferUploader(int capacity, global::UnityEngine.Rendering.InstanceType instanceType)
		{
			return new global::UnityEngine.Rendering.GPUInstanceDataBufferUploader(in m_InstanceDataBuffer.descriptions, capacity, instanceType);
		}

		public void SubmitToGpu(global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.InstanceHandle> instances, ref global::UnityEngine.Rendering.GPUInstanceDataBufferUploader uploader, bool submitOnlyWrittenParams)
		{
			uploader.SubmitToGpu(m_InstanceDataBuffer, instances, ref m_UploadResources, submitOnlyWrittenParams);
		}

		public void SubmitToGpu(global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.GPUInstanceIndex> gpuInstanceIndices, ref global::UnityEngine.Rendering.GPUInstanceDataBufferUploader uploader, bool submitOnlyWrittenParams)
		{
			uploader.SubmitToGpu(m_InstanceDataBuffer, gpuInstanceIndices, ref m_UploadResources, submitOnlyWrittenParams);
		}

		public void InitializeInstanceTransforms(global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.InstanceHandle> instances, global::Unity.Collections.NativeArray<global::UnityEngine.Matrix4x4> localToWorldMatrices, global::Unity.Collections.NativeArray<global::UnityEngine.Matrix4x4> prevLocalToWorldMatrices)
		{
			if (instances.Length != 0)
			{
				m_InstanceDataSystem.InitializeInstanceTransforms(instances, localToWorldMatrices, prevLocalToWorldMatrices, in m_RenderersParameters, m_InstanceDataBuffer);
				ChangeInstanceBufferVersion();
			}
		}

		public void UpdateInstanceTransforms(global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.InstanceHandle> instances, global::Unity.Collections.NativeArray<global::UnityEngine.Matrix4x4> localToWorldMatrices)
		{
			if (instances.Length != 0)
			{
				m_InstanceDataSystem.UpdateInstanceTransforms(instances, localToWorldMatrices, in m_RenderersParameters, m_InstanceDataBuffer);
				ChangeInstanceBufferVersion();
			}
		}

		public void UpdateAmbientProbeAndGpuBuffer(bool forceUpdate)
		{
			if (forceUpdate || m_CachedAmbientProbe != global::UnityEngine.RenderSettings.ambientProbe)
			{
				m_CachedAmbientProbe = global::UnityEngine.RenderSettings.ambientProbe;
				m_InstanceDataSystem.UpdateAllInstanceProbes(in m_RenderersParameters, m_InstanceDataBuffer);
				ChangeInstanceBufferVersion();
			}
		}

		public void UpdateInstanceWindDataHistory(global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.GPUInstanceIndex> gpuInstanceIndices)
		{
			if (gpuInstanceIndices.Length != 0)
			{
				m_InstanceDataSystem.UpdateInstanceWindDataHistory(gpuInstanceIndices, m_RenderersParameters, m_InstanceDataBuffer);
				ChangeInstanceBufferVersion();
			}
		}

		public void UpdateInstanceMotions()
		{
			m_InstanceDataSystem.UpdateInstanceMotions(in m_RenderersParameters, m_InstanceDataBuffer);
			ChangeInstanceBufferVersion();
		}

		public void TransformLODGroups(global::Unity.Collections.NativeArray<global::UnityEngine.EntityId> lodGroupsID)
		{
			if (lodGroupsID.Length != 0)
			{
				m_GPUDrivenProcessor.DispatchLODGroupData(lodGroupsID, m_TransformLODGroupCallback);
			}
		}

		public void UpdatePerFrameInstanceVisibility(in global::UnityEngine.Rendering.ParallelBitArray compactedVisibilityMasks)
		{
			m_InstanceDataSystem.UpdatePerFrameInstanceVisibility(in compactedVisibilityMasks);
		}

		public global::Unity.Jobs.JobHandle ScheduleCollectInstancesLODGroupAndMasksJob(global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.InstanceHandle> instances, global::Unity.Collections.NativeArray<uint> lodGroupAndMasks)
		{
			return m_InstanceDataSystem.ScheduleCollectInstancesLODGroupAndMasksJob(instances, lodGroupAndMasks);
		}

		public global::UnityEngine.Rendering.InstanceHandle GetRendererInstanceHandle(global::UnityEngine.EntityId rendererID)
		{
			global::Unity.Collections.NativeArray<global::UnityEngine.EntityId> rendererGroupIDs = new global::Unity.Collections.NativeArray<global::UnityEngine.EntityId>(1, global::Unity.Collections.Allocator.TempJob);
			global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.InstanceHandle> instances = new global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.InstanceHandle>(1, global::Unity.Collections.Allocator.TempJob);
			rendererGroupIDs[0] = rendererID;
			m_InstanceDataSystem.ScheduleQueryRendererGroupInstancesJob(rendererGroupIDs, instances).Complete();
			global::UnityEngine.Rendering.InstanceHandle result = instances[0];
			rendererGroupIDs.Dispose();
			instances.Dispose();
			return result;
		}

		public void GetVisibleTreeInstances(in global::UnityEngine.Rendering.ParallelBitArray compactedVisibilityMasks, in global::UnityEngine.Rendering.ParallelBitArray processedBits, global::Unity.Collections.NativeList<int> visibeTreeRendererIDs, global::Unity.Collections.NativeList<global::UnityEngine.Rendering.InstanceHandle> visibeTreeInstances, bool becomeVisibleOnly, out int becomeVisibeTreeInstancesCount)
		{
			m_InstanceDataSystem.GetVisibleTreeInstances(in compactedVisibilityMasks, in processedBits, visibeTreeRendererIDs, visibeTreeInstances, becomeVisibleOnly, out becomeVisibeTreeInstancesCount);
		}

		public global::UnityEngine.Rendering.GPUInstanceDataBuffer GetInstanceDataBuffer()
		{
			return m_InstanceDataBuffer;
		}

		public void UpdateFrame()
		{
			m_OcclusionCullingCommon.UpdateFrame();
			if (m_DebugStats != null)
			{
				m_OcclusionCullingCommon.UpdateOccluderStats(m_DebugStats);
			}
		}

		public void FreePerCameraInstanceData(global::Unity.Collections.NativeArray<global::UnityEngine.EntityId> cameraIDs)
		{
			m_InstanceDataSystem.DeallocatePerCameraInstanceData(cameraIDs);
		}

		public void UpdateCameras(global::Unity.Collections.NativeArray<global::UnityEngine.EntityId> cameraIDs)
		{
			m_InstanceDataSystem.AllocatePerCameraInstanceData(cameraIDs);
		}
	}
}
