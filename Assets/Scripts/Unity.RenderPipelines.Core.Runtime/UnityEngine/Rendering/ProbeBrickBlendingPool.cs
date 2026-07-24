namespace UnityEngine.Rendering
{
	internal class ProbeBrickBlendingPool
	{
		private static global::UnityEngine.ComputeShader stateBlendShader;

		private static int scenarioBlendingKernel = -1;

		private static readonly int _PoolDim_LerpFactor = global::UnityEngine.Shader.PropertyToID("_PoolDim_LerpFactor");

		private static readonly int _ChunkList = global::UnityEngine.Shader.PropertyToID("_ChunkList");

		private static readonly int _State0_L0_L1Rx = global::UnityEngine.Shader.PropertyToID("_State0_L0_L1Rx");

		private static readonly int _State0_L1G_L1Ry = global::UnityEngine.Shader.PropertyToID("_State0_L1G_L1Ry");

		private static readonly int _State0_L1B_L1Rz = global::UnityEngine.Shader.PropertyToID("_State0_L1B_L1Rz");

		private static readonly int _State0_L2_0 = global::UnityEngine.Shader.PropertyToID("_State0_L2_0");

		private static readonly int _State0_L2_1 = global::UnityEngine.Shader.PropertyToID("_State0_L2_1");

		private static readonly int _State0_L2_2 = global::UnityEngine.Shader.PropertyToID("_State0_L2_2");

		private static readonly int _State0_L2_3 = global::UnityEngine.Shader.PropertyToID("_State0_L2_3");

		private static readonly int _State0_ProbeOcclusion = global::UnityEngine.Shader.PropertyToID("_State0_ProbeOcclusion");

		private static readonly int _State1_L0_L1Rx = global::UnityEngine.Shader.PropertyToID("_State1_L0_L1Rx");

		private static readonly int _State1_L1G_L1Ry = global::UnityEngine.Shader.PropertyToID("_State1_L1G_L1Ry");

		private static readonly int _State1_L1B_L1Rz = global::UnityEngine.Shader.PropertyToID("_State1_L1B_L1Rz");

		private static readonly int _State1_L2_0 = global::UnityEngine.Shader.PropertyToID("_State1_L2_0");

		private static readonly int _State1_L2_1 = global::UnityEngine.Shader.PropertyToID("_State1_L2_1");

		private static readonly int _State1_L2_2 = global::UnityEngine.Shader.PropertyToID("_State1_L2_2");

		private static readonly int _State1_L2_3 = global::UnityEngine.Shader.PropertyToID("_State1_L2_3");

		private static readonly int _State1_ProbeOcclusion = global::UnityEngine.Shader.PropertyToID("_State1_ProbeOcclusion");

		private global::UnityEngine.Vector4[] m_ChunkList;

		private int m_MappedChunks;

		private global::UnityEngine.Rendering.ProbeBrickPool m_State0;

		private global::UnityEngine.Rendering.ProbeBrickPool m_State1;

		private global::UnityEngine.Rendering.ProbeVolumeTextureMemoryBudget m_MemoryBudget;

		private global::UnityEngine.Rendering.ProbeVolumeSHBands m_ShBands;

		private bool m_ProbeOcclusion;

		internal bool isAllocated => m_State0 != null;

		internal int estimatedVMemCost
		{
			get
			{
				if (!global::UnityEngine.Rendering.ProbeReferenceVolume.instance.supportScenarioBlending)
				{
					return 0;
				}
				if (isAllocated)
				{
					return m_State0.estimatedVMemCost + m_State1.estimatedVMemCost;
				}
				return global::UnityEngine.Rendering.ProbeBrickPool.EstimateMemoryCostForBlending(m_MemoryBudget, compressed: false, m_ShBands) * 2;
			}
		}

		internal static void Initialize()
		{
			if (global::UnityEngine.SystemInfo.supportsComputeShaders)
			{
				stateBlendShader = global::UnityEngine.Rendering.GraphicsSettings.GetRenderPipelineSettings<global::UnityEngine.Rendering.ProbeVolumeRuntimeResources>()?.probeVolumeBlendStatesCS;
				scenarioBlendingKernel = (stateBlendShader ? stateBlendShader.FindKernel("BlendScenarios") : (-1));
			}
		}

		internal int GetPoolWidth()
		{
			return m_State0.m_Pool.width;
		}

		internal int GetPoolHeight()
		{
			return m_State0.m_Pool.height;
		}

		internal int GetPoolDepth()
		{
			return m_State0.m_Pool.depth;
		}

		internal ProbeBrickBlendingPool(global::UnityEngine.Rendering.ProbeVolumeBlendingTextureMemoryBudget memoryBudget, global::UnityEngine.Rendering.ProbeVolumeSHBands shBands, bool probeOcclusion)
		{
			m_MemoryBudget = (global::UnityEngine.Rendering.ProbeVolumeTextureMemoryBudget)memoryBudget;
			m_ShBands = shBands;
			m_ProbeOcclusion = probeOcclusion;
		}

		internal void AllocateResourcesIfNeeded()
		{
			if (!isAllocated)
			{
				m_State0 = new global::UnityEngine.Rendering.ProbeBrickPool(m_MemoryBudget, m_ShBands, allocateValidityData: false, allocateRenderingLayerData: false, allocateSkyOcclusion: false, allocateSkyShadingData: false, m_ProbeOcclusion);
				m_State1 = new global::UnityEngine.Rendering.ProbeBrickPool(m_MemoryBudget, m_ShBands, allocateValidityData: false, allocateRenderingLayerData: false, allocateSkyOcclusion: false, allocateSkyShadingData: false, m_ProbeOcclusion);
				int num = GetPoolWidth() / 512 * (GetPoolHeight() / 4) * (GetPoolDepth() / 4);
				m_ChunkList = new global::UnityEngine.Vector4[num];
				m_MappedChunks = 0;
			}
		}

		internal void Update(global::UnityEngine.Rendering.ProbeBrickPool.DataLocation source, global::System.Collections.Generic.List<global::UnityEngine.Rendering.ProbeBrickPool.BrickChunkAlloc> srcLocations, global::System.Collections.Generic.List<global::UnityEngine.Rendering.ProbeBrickPool.BrickChunkAlloc> dstLocations, int destStartIndex, global::UnityEngine.Rendering.ProbeVolumeSHBands bands, int state)
		{
			((state == 0) ? m_State0 : m_State1).Update(source, srcLocations, dstLocations, destStartIndex, bands);
		}

		internal void Update(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Rendering.ProbeReferenceVolume.CellStreamingScratchBuffer dataBuffer, global::UnityEngine.Rendering.ProbeReferenceVolume.CellStreamingScratchBufferLayout layout, global::System.Collections.Generic.List<global::UnityEngine.Rendering.ProbeBrickPool.BrickChunkAlloc> dstLocations, global::UnityEngine.Rendering.ProbeVolumeSHBands bands, int state, global::UnityEngine.Texture validityTexture, bool skyOcclusion, global::UnityEngine.Texture skyOcclusionTexture, bool skyShadingDirections, global::UnityEngine.Texture skyShadingDirectionsTexture, bool probeOcclusion)
		{
			bool flag = state == 0;
			((state == 0) ? m_State0 : m_State1).Update(cmd, dataBuffer, layout, dstLocations, flag, validityTexture, bands, flag && skyOcclusion, skyOcclusionTexture, flag && skyShadingDirections, skyShadingDirectionsTexture, probeOcclusion);
		}

		internal void PerformBlending(global::UnityEngine.Rendering.CommandBuffer cmd, float factor, global::UnityEngine.Rendering.ProbeBrickPool dstPool)
		{
			if (m_MappedChunks != 0)
			{
				cmd.SetComputeTextureParam(stateBlendShader, scenarioBlendingKernel, _State0_L0_L1Rx, m_State0.m_Pool.TexL0_L1rx);
				cmd.SetComputeTextureParam(stateBlendShader, scenarioBlendingKernel, _State0_L1G_L1Ry, m_State0.m_Pool.TexL1_G_ry);
				cmd.SetComputeTextureParam(stateBlendShader, scenarioBlendingKernel, _State0_L1B_L1Rz, m_State0.m_Pool.TexL1_B_rz);
				cmd.SetComputeTextureParam(stateBlendShader, scenarioBlendingKernel, _State1_L0_L1Rx, m_State1.m_Pool.TexL0_L1rx);
				cmd.SetComputeTextureParam(stateBlendShader, scenarioBlendingKernel, _State1_L1G_L1Ry, m_State1.m_Pool.TexL1_G_ry);
				cmd.SetComputeTextureParam(stateBlendShader, scenarioBlendingKernel, _State1_L1B_L1Rz, m_State1.m_Pool.TexL1_B_rz);
				cmd.SetComputeTextureParam(stateBlendShader, scenarioBlendingKernel, global::UnityEngine.Rendering.ProbeBrickPool._Out_L0_L1Rx, dstPool.m_Pool.TexL0_L1rx);
				cmd.SetComputeTextureParam(stateBlendShader, scenarioBlendingKernel, global::UnityEngine.Rendering.ProbeBrickPool._Out_L1G_L1Ry, dstPool.m_Pool.TexL1_G_ry);
				cmd.SetComputeTextureParam(stateBlendShader, scenarioBlendingKernel, global::UnityEngine.Rendering.ProbeBrickPool._Out_L1B_L1Rz, dstPool.m_Pool.TexL1_B_rz);
				if (m_ShBands == global::UnityEngine.Rendering.ProbeVolumeSHBands.SphericalHarmonicsL2)
				{
					stateBlendShader.EnableKeyword("PROBE_VOLUMES_L2");
					cmd.SetComputeTextureParam(stateBlendShader, scenarioBlendingKernel, _State0_L2_0, m_State0.m_Pool.TexL2_0);
					cmd.SetComputeTextureParam(stateBlendShader, scenarioBlendingKernel, _State0_L2_1, m_State0.m_Pool.TexL2_1);
					cmd.SetComputeTextureParam(stateBlendShader, scenarioBlendingKernel, _State0_L2_2, m_State0.m_Pool.TexL2_2);
					cmd.SetComputeTextureParam(stateBlendShader, scenarioBlendingKernel, _State0_L2_3, m_State0.m_Pool.TexL2_3);
					cmd.SetComputeTextureParam(stateBlendShader, scenarioBlendingKernel, _State1_L2_0, m_State1.m_Pool.TexL2_0);
					cmd.SetComputeTextureParam(stateBlendShader, scenarioBlendingKernel, _State1_L2_1, m_State1.m_Pool.TexL2_1);
					cmd.SetComputeTextureParam(stateBlendShader, scenarioBlendingKernel, _State1_L2_2, m_State1.m_Pool.TexL2_2);
					cmd.SetComputeTextureParam(stateBlendShader, scenarioBlendingKernel, _State1_L2_3, m_State1.m_Pool.TexL2_3);
					cmd.SetComputeTextureParam(stateBlendShader, scenarioBlendingKernel, global::UnityEngine.Rendering.ProbeBrickPool._Out_L2_0, dstPool.m_Pool.TexL2_0);
					cmd.SetComputeTextureParam(stateBlendShader, scenarioBlendingKernel, global::UnityEngine.Rendering.ProbeBrickPool._Out_L2_1, dstPool.m_Pool.TexL2_1);
					cmd.SetComputeTextureParam(stateBlendShader, scenarioBlendingKernel, global::UnityEngine.Rendering.ProbeBrickPool._Out_L2_2, dstPool.m_Pool.TexL2_2);
					cmd.SetComputeTextureParam(stateBlendShader, scenarioBlendingKernel, global::UnityEngine.Rendering.ProbeBrickPool._Out_L2_3, dstPool.m_Pool.TexL2_3);
				}
				else
				{
					stateBlendShader.DisableKeyword("PROBE_VOLUMES_L2");
				}
				if (m_ProbeOcclusion)
				{
					stateBlendShader.EnableKeyword("USE_APV_PROBE_OCCLUSION");
					cmd.SetComputeTextureParam(stateBlendShader, scenarioBlendingKernel, _State0_ProbeOcclusion, m_State0.m_Pool.TexProbeOcclusion);
					cmd.SetComputeTextureParam(stateBlendShader, scenarioBlendingKernel, _State1_ProbeOcclusion, m_State1.m_Pool.TexProbeOcclusion);
					cmd.SetComputeTextureParam(stateBlendShader, scenarioBlendingKernel, global::UnityEngine.Rendering.ProbeBrickPool._Out_ProbeOcclusion, dstPool.m_Pool.TexProbeOcclusion);
				}
				else
				{
					stateBlendShader.DisableKeyword("USE_APV_PROBE_OCCLUSION");
				}
				global::UnityEngine.Vector4 val = new global::UnityEngine.Vector4(dstPool.GetPoolWidth(), dstPool.GetPoolHeight(), factor, 0f);
				int threadGroupsX = global::UnityEngine.Rendering.ProbeBrickPool.DivRoundUp(512, 4);
				int threadGroupsY = global::UnityEngine.Rendering.ProbeBrickPool.DivRoundUp(4, 4);
				int num = global::UnityEngine.Rendering.ProbeBrickPool.DivRoundUp(4, 4);
				cmd.SetComputeVectorArrayParam(stateBlendShader, _ChunkList, m_ChunkList);
				cmd.SetComputeVectorParam(stateBlendShader, _PoolDim_LerpFactor, val);
				cmd.DispatchCompute(stateBlendShader, scenarioBlendingKernel, threadGroupsX, threadGroupsY, num * m_MappedChunks);
				m_MappedChunks = 0;
			}
		}

		internal void BlendChunks(global::UnityEngine.Rendering.ProbeReferenceVolume.Cell cell, global::UnityEngine.Rendering.ProbeBrickPool dstPool)
		{
			for (int i = 0; i < cell.blendingInfo.chunkList.Count; i++)
			{
				global::UnityEngine.Rendering.ProbeBrickPool.BrickChunkAlloc brickChunkAlloc = cell.blendingInfo.chunkList[i];
				int num = cell.poolInfo.chunkList[i].flattenIndex(dstPool.GetPoolWidth(), dstPool.GetPoolHeight());
				m_ChunkList[m_MappedChunks++] = new global::UnityEngine.Vector4(brickChunkAlloc.x, brickChunkAlloc.y, brickChunkAlloc.z, num);
			}
		}

		internal void Clear()
		{
			m_State0?.Clear();
		}

		internal bool Allocate(int numberOfBrickChunks, global::System.Collections.Generic.List<global::UnityEngine.Rendering.ProbeBrickPool.BrickChunkAlloc> outAllocations)
		{
			AllocateResourcesIfNeeded();
			if (numberOfBrickChunks > m_State0.GetRemainingChunkCount())
			{
				return false;
			}
			return m_State0.Allocate(numberOfBrickChunks, outAllocations, ignoreErrorLog: false);
		}

		internal void Deallocate(global::System.Collections.Generic.List<global::UnityEngine.Rendering.ProbeBrickPool.BrickChunkAlloc> allocations)
		{
			if (allocations.Count != 0)
			{
				m_State0.Deallocate(allocations);
			}
		}

		internal void EnsureTextureValidity()
		{
			if (isAllocated)
			{
				m_State0.EnsureTextureValidity();
				m_State1.EnsureTextureValidity();
			}
		}

		internal void Cleanup()
		{
			if (isAllocated)
			{
				m_State0.Cleanup();
				m_State1.Cleanup();
			}
		}
	}
}
