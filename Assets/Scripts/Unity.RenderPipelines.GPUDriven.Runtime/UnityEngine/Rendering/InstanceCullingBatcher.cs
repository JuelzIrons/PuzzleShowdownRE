namespace UnityEngine.Rendering
{
	internal class InstanceCullingBatcher : global::System.IDisposable
	{
		private global::UnityEngine.Rendering.RenderersBatchersContext m_BatchersContext;

		private global::UnityEngine.Rendering.CPUDrawInstanceData m_DrawInstanceData;

		private global::UnityEngine.Rendering.BatchRendererGroup m_BRG;

		private global::Unity.Collections.NativeParallelHashMap<uint, global::UnityEngine.Rendering.BatchID> m_GlobalBatchIDs;

		private global::UnityEngine.Rendering.InstanceCuller m_Culler;

		private global::Unity.Collections.NativeParallelHashMap<global::UnityEngine.EntityId, global::UnityEngine.Rendering.BatchMaterialID> m_BatchMaterialHash;

		private global::Unity.Collections.NativeParallelHashMap<global::UnityEngine.EntityId, global::UnityEngine.Rendering.GPUDrivenPackedMaterialData> m_PackedMaterialHash;

		private global::Unity.Collections.NativeParallelHashMap<global::UnityEngine.EntityId, global::UnityEngine.Rendering.BatchMeshID> m_BatchMeshHash;

		private int m_CachedInstanceDataBufferLayoutVersion;

		private global::UnityEngine.Rendering.OnCullingCompleteCallback m_OnCompleteCallback;

		public global::Unity.Collections.NativeParallelHashMap<global::UnityEngine.EntityId, global::UnityEngine.Rendering.BatchMaterialID> batchMaterialHash => m_BatchMaterialHash;

		public global::Unity.Collections.NativeParallelHashMap<global::UnityEngine.EntityId, global::UnityEngine.Rendering.GPUDrivenPackedMaterialData> packedMaterialHash => m_PackedMaterialHash;

		internal ref global::UnityEngine.Rendering.InstanceCuller culler => ref m_Culler;

		public InstanceCullingBatcher(global::UnityEngine.Rendering.RenderersBatchersContext batcherContext, global::UnityEngine.Rendering.InstanceCullingBatcherDesc desc, global::UnityEngine.Rendering.BatchRendererGroup.OnFinishedCulling onFinishedCulling)
		{
			m_BatchersContext = batcherContext;
			m_DrawInstanceData = new global::UnityEngine.Rendering.CPUDrawInstanceData();
			m_DrawInstanceData.Initialize();
			m_BRG = new global::UnityEngine.Rendering.BatchRendererGroup(new global::UnityEngine.Rendering.BatchRendererGroupCreateInfo
			{
				cullingCallback = OnPerformCulling,
				finishedCullingCallback = onFinishedCulling,
				userContext = global::System.IntPtr.Zero
			});
			m_Culler = default(global::UnityEngine.Rendering.InstanceCuller);
			m_Culler.Init(batcherContext.resources, batcherContext.debugStats);
			m_CachedInstanceDataBufferLayoutVersion = -1;
			m_OnCompleteCallback = desc.onCompleteCallback;
			m_BatchMaterialHash = new global::Unity.Collections.NativeParallelHashMap<global::UnityEngine.EntityId, global::UnityEngine.Rendering.BatchMaterialID>(64, global::Unity.Collections.Allocator.Persistent);
			m_PackedMaterialHash = new global::Unity.Collections.NativeParallelHashMap<global::UnityEngine.EntityId, global::UnityEngine.Rendering.GPUDrivenPackedMaterialData>(64, global::Unity.Collections.Allocator.Persistent);
			m_BatchMeshHash = new global::Unity.Collections.NativeParallelHashMap<global::UnityEngine.EntityId, global::UnityEngine.Rendering.BatchMeshID>(64, global::Unity.Collections.Allocator.Persistent);
			m_GlobalBatchIDs = new global::Unity.Collections.NativeParallelHashMap<uint, global::UnityEngine.Rendering.BatchID>(6, global::Unity.Collections.Allocator.Persistent);
			m_GlobalBatchIDs.Add(1u, GetBatchID(global::UnityEngine.Rendering.InstanceComponentGroup.Default));
			m_GlobalBatchIDs.Add(3u, GetBatchID(global::UnityEngine.Rendering.InstanceComponentGroup.DefaultWind));
			m_GlobalBatchIDs.Add(5u, GetBatchID(global::UnityEngine.Rendering.InstanceComponentGroup.DefaultLightProbe));
			m_GlobalBatchIDs.Add(9u, GetBatchID(global::UnityEngine.Rendering.InstanceComponentGroup.DefaultLightmap));
			m_GlobalBatchIDs.Add(7u, GetBatchID(global::UnityEngine.Rendering.InstanceComponentGroup.DefaultWindLightProbe));
			m_GlobalBatchIDs.Add(11u, GetBatchID(global::UnityEngine.Rendering.InstanceComponentGroup.DefaultWindLightmap));
		}

		public void Dispose()
		{
			m_OnCompleteCallback = null;
			m_Culler.Dispose();
			foreach (global::Unity.Collections.LowLevel.Unsafe.KeyValue<uint, global::UnityEngine.Rendering.BatchID> globalBatchID in m_GlobalBatchIDs)
			{
				if (!globalBatchID.Value.Equals(global::UnityEngine.Rendering.BatchID.Null))
				{
					m_BRG.RemoveBatch(globalBatchID.Value);
				}
			}
			m_GlobalBatchIDs.Dispose();
			if (m_BRG != null)
			{
				m_BRG.Dispose();
			}
			m_DrawInstanceData.Dispose();
			m_DrawInstanceData = null;
			m_BatchMaterialHash.Dispose();
			m_PackedMaterialHash.Dispose();
			m_BatchMeshHash.Dispose();
		}

		private global::UnityEngine.Rendering.BatchID GetBatchID(global::UnityEngine.Rendering.InstanceComponentGroup componentsOverriden)
		{
			if (m_CachedInstanceDataBufferLayoutVersion != m_BatchersContext.instanceDataBufferLayoutVersion)
			{
				return global::UnityEngine.Rendering.BatchID.Null;
			}
			global::Unity.Collections.NativeList<global::UnityEngine.Rendering.MetadataValue> nativeList = new global::Unity.Collections.NativeList<global::UnityEngine.Rendering.MetadataValue>(m_BatchersContext.defaultMetadata.Length, global::Unity.Collections.Allocator.Temp);
			for (int i = 0; i < m_BatchersContext.defaultDescriptions.Length; i++)
			{
				global::UnityEngine.Rendering.InstanceComponentGroup componentGroup = m_BatchersContext.defaultDescriptions[i].componentGroup;
				global::UnityEngine.Rendering.MetadataValue metadataValue = m_BatchersContext.defaultMetadata[i];
				uint num = metadataValue.Value;
				if ((componentsOverriden & componentGroup) == 0)
				{
					num &= 0x4FFFFFFF;
				}
				nativeList.Add(new global::UnityEngine.Rendering.MetadataValue
				{
					NameID = metadataValue.NameID,
					Value = num
				});
			}
			return m_BRG.AddBatch(nativeList.AsArray(), m_BatchersContext.gpuInstanceDataBuffer.bufferHandle);
		}

		private void UpdateInstanceDataBufferLayoutVersion()
		{
			if (m_CachedInstanceDataBufferLayoutVersion == m_BatchersContext.instanceDataBufferLayoutVersion)
			{
				return;
			}
			m_CachedInstanceDataBufferLayoutVersion = m_BatchersContext.instanceDataBufferLayoutVersion;
			foreach (global::Unity.Collections.LowLevel.Unsafe.KeyValue<uint, global::UnityEngine.Rendering.BatchID> globalBatchID in m_GlobalBatchIDs)
			{
				global::UnityEngine.Rendering.BatchID value = globalBatchID.Value;
				if (!value.Equals(global::UnityEngine.Rendering.BatchID.Null))
				{
					m_BRG.RemoveBatch(value);
				}
				global::UnityEngine.Rendering.InstanceComponentGroup key = (global::UnityEngine.Rendering.InstanceComponentGroup)globalBatchID.Key;
				globalBatchID.Value = GetBatchID(key);
			}
		}

		public global::UnityEngine.Rendering.CPUDrawInstanceData GetDrawInstanceData()
		{
			return m_DrawInstanceData;
		}

		public global::Unity.Jobs.JobHandle OnPerformCulling(global::UnityEngine.Rendering.BatchRendererGroup rendererGroup, global::UnityEngine.Rendering.BatchCullingContext cc, global::UnityEngine.Rendering.BatchCullingOutput cullingOutput, global::System.IntPtr userContext)
		{
			foreach (global::Unity.Collections.LowLevel.Unsafe.KeyValue<uint, global::UnityEngine.Rendering.BatchID> globalBatchID in m_GlobalBatchIDs)
			{
				if (globalBatchID.Value.Equals(global::UnityEngine.Rendering.BatchID.Null))
				{
					return default(global::Unity.Jobs.JobHandle);
				}
			}
			m_DrawInstanceData.RebuildDrawListsIfNeeded();
			bool hasBoundingSpheres = m_BatchersContext.hasBoundingSpheres;
			global::Unity.Jobs.JobHandle jobHandle = m_Culler.CreateCullJobTree(in cc, cullingOutput, m_BatchersContext.instanceData, m_BatchersContext.sharedInstanceData, m_BatchersContext.perCameraInstanceData, m_BatchersContext.instanceDataBuffer, m_BatchersContext.lodGroupCullingData, m_DrawInstanceData, m_GlobalBatchIDs, m_BatchersContext.smallMeshScreenPercentage, hasBoundingSpheres ? m_BatchersContext.occlusionCullingCommon : null);
			if (m_OnCompleteCallback != null)
			{
				m_OnCompleteCallback(jobHandle, in cc, in cullingOutput);
			}
			return jobHandle;
		}

		public void OnFinishedCulling(global::System.IntPtr customCullingResult)
		{
			int viewInstanceID = (int)customCullingResult;
			m_Culler.EnsureValidOcclusionTestResults(viewInstanceID);
		}

		public void DestroyDrawInstances(global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.InstanceHandle> instances)
		{
			if (instances.Length != 0)
			{
				m_DrawInstanceData.DestroyDrawInstances(instances);
			}
		}

		public void DestroyMaterials(global::Unity.Collections.NativeArray<global::UnityEngine.EntityId> destroyedMaterials)
		{
			if (destroyedMaterials.Length == 0)
			{
				return;
			}
			global::Unity.Collections.NativeList<uint> nativeList = new global::Unity.Collections.NativeList<uint>(destroyedMaterials.Length, global::Unity.Collections.Allocator.TempJob);
			foreach (global::UnityEngine.EntityId item2 in destroyedMaterials)
			{
				int num = item2;
				if (m_BatchMaterialHash.TryGetValue(num, out var item))
				{
					nativeList.Add(in item.value);
					m_BatchMaterialHash.Remove(num);
					m_PackedMaterialHash.Remove(num);
					m_BRG.UnregisterMaterial(item);
				}
			}
			m_DrawInstanceData.DestroyMaterialDrawInstances(nativeList.AsArray());
			nativeList.Dispose();
		}

		public void DestroyMeshes(global::Unity.Collections.NativeArray<global::UnityEngine.EntityId> destroyedMeshes)
		{
			if (destroyedMeshes.Length == 0)
			{
				return;
			}
			foreach (global::UnityEngine.EntityId item2 in destroyedMeshes)
			{
				int num = item2;
				if (m_BatchMeshHash.TryGetValue(num, out var item))
				{
					m_BatchMeshHash.Remove(num);
					m_BRG.UnregisterMesh(item);
				}
			}
		}

		public void PostCullBeginCameraRendering(global::UnityEngine.Rendering.RenderRequestBatcherContext context)
		{
		}

		private void RegisterBatchMeshes(global::Unity.Collections.NativeArray<global::UnityEngine.EntityId> meshIDs)
		{
			global::Unity.Collections.NativeList<global::UnityEngine.EntityId> nativeList = new global::Unity.Collections.NativeList<global::UnityEngine.EntityId>(meshIDs.Length, global::Unity.Collections.Allocator.TempJob);
			global::Unity.Jobs.IJobParallelForBatchExtensions.ScheduleBatch(new global::UnityEngine.Rendering.FindNonRegisteredMeshesJob
			{
				instanceIDs = meshIDs,
				hashMap = m_BatchMeshHash,
				outInstancesWriter = nativeList.AsParallelWriter()
			}, meshIDs.Length, 128).Complete();
			global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.BatchMeshID> source = new global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.BatchMeshID>(nativeList.Length, global::Unity.Collections.Allocator.TempJob, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			m_BRG.RegisterMeshes(nativeList.AsArray(), source);
			int num = m_BatchMeshHash.Count() + source.Length;
			m_BatchMeshHash.Capacity = global::System.Math.Max(m_BatchMeshHash.Capacity, global::UnityEngine.Mathf.CeilToInt((float)num / 1023f) * 1024);
			global::Unity.Jobs.IJobParallelForExtensions.Schedule(new global::UnityEngine.Rendering.RegisterNewMeshesJob
			{
				instanceIDs = nativeList.AsArray(),
				batchIDs = source,
				hashMap = m_BatchMeshHash.AsParallelWriter()
			}, nativeList.Length, 128).Complete();
			nativeList.Dispose();
			source.Dispose();
		}

		private void RegisterBatchMaterials(in global::Unity.Collections.NativeArray<global::UnityEngine.EntityId> usedMaterialIDs, in global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.GPUDrivenPackedMaterialData> usedPackedMaterialDatas)
		{
			global::Unity.Collections.NativeList<global::UnityEngine.EntityId> nativeList = new global::Unity.Collections.NativeList<global::UnityEngine.EntityId>(usedMaterialIDs.Length, global::Unity.Collections.Allocator.TempJob);
			global::Unity.Collections.NativeList<global::UnityEngine.Rendering.GPUDrivenPackedMaterialData> nativeList2 = new global::Unity.Collections.NativeList<global::UnityEngine.Rendering.GPUDrivenPackedMaterialData>(usedMaterialIDs.Length, global::Unity.Collections.Allocator.TempJob);
			global::Unity.Jobs.IJobParallelForBatchExtensions.ScheduleBatch(new global::UnityEngine.Rendering.FindNonRegisteredMaterialsJob
			{
				instanceIDs = usedMaterialIDs,
				packedMaterialDatas = usedPackedMaterialDatas,
				hashMap = m_BatchMaterialHash,
				outInstancesWriter = nativeList.AsParallelWriter(),
				outPackedMaterialDatasWriter = nativeList2.AsParallelWriter()
			}, usedMaterialIDs.Length, 128).Complete();
			global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.BatchMaterialID> source = new global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.BatchMaterialID>(nativeList.Length, global::Unity.Collections.Allocator.TempJob, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			m_BRG.RegisterMaterials(nativeList.AsArray(), source);
			int num = m_BatchMaterialHash.Count() + nativeList.Length;
			m_BatchMaterialHash.Capacity = global::System.Math.Max(m_BatchMaterialHash.Capacity, global::UnityEngine.Mathf.CeilToInt((float)num / 1023f) * 1024);
			m_PackedMaterialHash.Capacity = m_BatchMaterialHash.Capacity;
			global::Unity.Jobs.IJobParallelForExtensions.Schedule(new global::UnityEngine.Rendering.RegisterNewMaterialsJob
			{
				instanceIDs = nativeList.AsArray(),
				packedMaterialDatas = nativeList2.AsArray(),
				batchIDs = source,
				batchMaterialHashMap = m_BatchMaterialHash.AsParallelWriter(),
				packedMaterialHashMap = m_PackedMaterialHash.AsParallelWriter()
			}, nativeList.Length, 128).Complete();
			nativeList.Dispose();
			nativeList2.Dispose();
			source.Dispose();
		}

		public global::Unity.Jobs.JobHandle SchedulePackedMaterialCacheUpdate(global::Unity.Collections.NativeArray<global::UnityEngine.EntityId> materialIDs, global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.GPUDrivenPackedMaterialData> packedMaterialDatas)
		{
			return global::Unity.Jobs.IJobExtensions.Schedule(new global::UnityEngine.Rendering.UpdatePackedMaterialDataCacheJob
			{
				materialIDs = materialIDs.AsReadOnly(),
				packedMaterialDatas = packedMaterialDatas.AsReadOnly(),
				packedMaterialHash = m_PackedMaterialHash
			});
		}

		public void BuildBatch(global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.InstanceHandle> instances, in global::UnityEngine.Rendering.GPUDrivenRendererGroupData rendererData, bool registerMaterialsAndMeshes)
		{
			if (registerMaterialsAndMeshes)
			{
				RegisterBatchMaterials(in rendererData.materialID, in rendererData.packedMaterialData);
				RegisterBatchMeshes(rendererData.meshID);
			}
			global::Unity.Collections.NativeParallelHashMap<global::UnityEngine.Rendering.RangeKey, int> rangeHash = m_DrawInstanceData.rangeHash;
			global::Unity.Collections.NativeList<global::UnityEngine.Rendering.DrawRange> drawRanges = m_DrawInstanceData.drawRanges;
			global::Unity.Collections.NativeParallelHashMap<global::UnityEngine.Rendering.DrawKey, int> batchHash = m_DrawInstanceData.batchHash;
			global::Unity.Collections.NativeList<global::UnityEngine.Rendering.DrawBatch> drawBatches = m_DrawInstanceData.drawBatches;
			global::Unity.Collections.NativeList<global::UnityEngine.Rendering.DrawInstance> drawInstances = m_DrawInstanceData.drawInstances;
			global::UnityEngine.Rendering.InstanceCullingBatcherBurst.CreateDrawBatches(rendererData.instancesCount.Length == 0, in instances, in rendererData, in m_BatchMeshHash, in m_BatchMaterialHash, in m_PackedMaterialHash, ref rangeHash, ref drawRanges, ref batchHash, ref drawBatches, ref drawInstances);
			m_DrawInstanceData.NeedsRebuild();
			UpdateInstanceDataBufferLayoutVersion();
		}

		public void InstanceOccludersUpdated(int viewInstanceID, int subviewMask)
		{
			m_Culler.InstanceOccludersUpdated(viewInstanceID, subviewMask, m_BatchersContext);
		}

		public void UpdateFrame()
		{
			m_Culler.UpdateFrame(m_BatchersContext.cameraCount);
		}

		public global::UnityEngine.Rendering.ParallelBitArray GetCompactedVisibilityMasks(bool syncCullingJobs)
		{
			return m_Culler.GetCompactedVisibilityMasks(syncCullingJobs);
		}

		public void OnEndContextRendering()
		{
			global::UnityEngine.Rendering.ParallelBitArray compactedVisibilityMasks = GetCompactedVisibilityMasks(syncCullingJobs: true);
			if (compactedVisibilityMasks.IsCreated)
			{
				m_BatchersContext.UpdatePerFrameInstanceVisibility(in compactedVisibilityMasks);
			}
		}

		public void OnBeginCameraRendering(global::UnityEngine.Camera camera)
		{
			m_Culler.OnBeginCameraRendering(camera);
		}

		public void OnEndCameraRendering(global::UnityEngine.Camera camera)
		{
			m_Culler.OnEndCameraRendering(camera);
		}
	}
}
