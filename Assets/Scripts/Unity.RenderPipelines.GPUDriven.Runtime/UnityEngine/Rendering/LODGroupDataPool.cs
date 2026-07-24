namespace UnityEngine.Rendering
{
	internal class LODGroupDataPool : global::System.IDisposable
	{
		private static class LodGroupShaderIDs
		{
			public static readonly int _SupportDitheringCrossFade = global::UnityEngine.Shader.PropertyToID("_SupportDitheringCrossFade");

			public static readonly int _LodGroupCullingDataGPUByteSize = global::UnityEngine.Shader.PropertyToID("_LodGroupCullingDataGPUByteSize");

			public static readonly int _LodGroupCullingDataStartOffset = global::UnityEngine.Shader.PropertyToID("_LodGroupCullingDataStartOffset");

			public static readonly int _LodCullingDataQueueCount = global::UnityEngine.Shader.PropertyToID("_LodCullingDataQueueCount");

			public static readonly int _InputLodCullingDataIndices = global::UnityEngine.Shader.PropertyToID("_InputLodCullingDataIndices");

			public static readonly int _InputLodCullingDataBuffer = global::UnityEngine.Shader.PropertyToID("_InputLodCullingDataBuffer");

			public static readonly int _LodGroupCullingData = global::UnityEngine.Shader.PropertyToID("_LodGroupCullingData");
		}

		private global::Unity.Collections.NativeList<global::UnityEngine.Rendering.LODGroupData> m_LODGroupData;

		private global::Unity.Collections.NativeParallelHashMap<int, global::UnityEngine.Rendering.GPUInstanceIndex> m_LODGroupDataHash;

		private global::Unity.Collections.NativeList<global::UnityEngine.Rendering.LODGroupCullingData> m_LODGroupCullingData;

		private global::Unity.Collections.NativeList<global::UnityEngine.Rendering.GPUInstanceIndex> m_FreeLODGroupDataHandles;

		private int m_CrossfadedRendererCount;

		private bool m_SupportDitheringCrossFade;

		public global::Unity.Collections.NativeParallelHashMap<int, global::UnityEngine.Rendering.GPUInstanceIndex> lodGroupDataHash => m_LODGroupDataHash;

		public global::Unity.Collections.NativeList<global::UnityEngine.Rendering.LODGroupCullingData> lodGroupCullingData => m_LODGroupCullingData;

		public int crossfadedRendererCount => m_CrossfadedRendererCount;

		public int activeLodGroupCount => m_LODGroupData.Length;

		public LODGroupDataPool(global::UnityEngine.Rendering.GPUResidentDrawerResources resources, int initialInstanceCount, bool supportDitheringCrossFade)
		{
			m_LODGroupData = new global::Unity.Collections.NativeList<global::UnityEngine.Rendering.LODGroupData>(global::Unity.Collections.Allocator.Persistent);
			m_LODGroupDataHash = new global::Unity.Collections.NativeParallelHashMap<int, global::UnityEngine.Rendering.GPUInstanceIndex>(64, global::Unity.Collections.Allocator.Persistent);
			m_LODGroupCullingData = new global::Unity.Collections.NativeList<global::UnityEngine.Rendering.LODGroupCullingData>(global::Unity.Collections.Allocator.Persistent);
			m_FreeLODGroupDataHandles = new global::Unity.Collections.NativeList<global::UnityEngine.Rendering.GPUInstanceIndex>(global::Unity.Collections.Allocator.Persistent);
			m_SupportDitheringCrossFade = supportDitheringCrossFade;
		}

		public void Dispose()
		{
			m_LODGroupData.Dispose();
			m_LODGroupDataHash.Dispose();
			m_LODGroupCullingData.Dispose();
			m_FreeLODGroupDataHandles.Dispose();
		}

		public unsafe void UpdateLODGroupTransformData(in global::UnityEngine.Rendering.GPUDrivenLODGroupData inputData)
		{
			int length = inputData.lodGroupID.Length;
			int num = 0;
			global::UnityEngine.Rendering.UpdateLODGroupTransformJob jobData = new global::UnityEngine.Rendering.UpdateLODGroupTransformJob
			{
				lodGroupDataHash = m_LODGroupDataHash,
				lodGroupIDs = inputData.lodGroupID,
				worldSpaceReferencePoints = inputData.worldSpaceReferencePoint,
				worldSpaceSizes = inputData.worldSpaceSize,
				lodGroupData = m_LODGroupData,
				lodGroupCullingData = m_LODGroupCullingData,
				supportDitheringCrossFade = m_SupportDitheringCrossFade,
				atomicUpdateCount = new global::Unity.Collections.LowLevel.Unsafe.UnsafeAtomicCounter32(&num)
			};
			if (length >= 256)
			{
				global::Unity.Jobs.IJobParallelForExtensions.Schedule(jobData, length, 256).Complete();
			}
			else
			{
				global::Unity.Jobs.IJobParallelForExtensions.Run(jobData, length);
			}
		}

		public unsafe void UpdateLODGroupData(in global::UnityEngine.Rendering.GPUDrivenLODGroupData inputData)
		{
			FreeLODGroupData(inputData.invalidLODGroupID);
			global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.GPUInstanceIndex> lodGroupInstances = new global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.GPUInstanceIndex>(inputData.lodGroupID.Length, global::Unity.Collections.Allocator.TempJob, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			int num = global::UnityEngine.Rendering.LODGroupDataPoolBurst.AllocateOrGetLODGroupDataInstances(in inputData.lodGroupID, ref m_LODGroupData, ref m_LODGroupCullingData, ref m_LODGroupDataHash, ref m_FreeLODGroupDataHandles, ref lodGroupInstances);
			m_CrossfadedRendererCount -= num;
			int num2 = 0;
			global::UnityEngine.Rendering.UpdateLODGroupDataJob jobData = new global::UnityEngine.Rendering.UpdateLODGroupDataJob
			{
				lodGroupInstances = lodGroupInstances,
				inputData = inputData,
				supportDitheringCrossFade = m_SupportDitheringCrossFade,
				lodGroupsData = m_LODGroupData.AsArray(),
				lodGroupsCullingData = m_LODGroupCullingData.AsArray(),
				rendererCount = new global::Unity.Collections.LowLevel.Unsafe.UnsafeAtomicCounter32(&num2)
			};
			if (lodGroupInstances.Length >= 256)
			{
				global::Unity.Jobs.IJobParallelForExtensions.Schedule(jobData, lodGroupInstances.Length, 256).Complete();
			}
			else
			{
				global::Unity.Jobs.IJobParallelForExtensions.Run(jobData, lodGroupInstances.Length);
			}
			m_CrossfadedRendererCount += num2;
			lodGroupInstances.Dispose();
		}

		public void FreeLODGroupData(global::Unity.Collections.NativeArray<global::UnityEngine.EntityId> destroyedLODGroupsID)
		{
			if (destroyedLODGroupsID.Length != 0)
			{
				int num = global::UnityEngine.Rendering.LODGroupDataPoolBurst.FreeLODGroupData(in destroyedLODGroupsID, ref m_LODGroupData, ref m_LODGroupDataHash, ref m_FreeLODGroupDataHandles);
				m_CrossfadedRendererCount -= num;
			}
		}
	}
}
