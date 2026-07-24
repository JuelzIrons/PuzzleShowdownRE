namespace UnityEngine.Rendering
{
	internal class ProbeBrickPool
	{
		[global::System.Diagnostics.DebuggerDisplay("Chunk ({x}, {y}, {z})")]
		public struct BrickChunkAlloc
		{
			public int x;

			public int y;

			public int z;

			internal int flattenIndex(int sx, int sy)
			{
				return z * (sx * sy) + y * sx + x;
			}
		}

		public struct DataLocation
		{
			internal global::UnityEngine.Texture TexL0_L1rx;

			internal global::UnityEngine.Texture TexL1_G_ry;

			internal global::UnityEngine.Texture TexL1_B_rz;

			internal global::UnityEngine.Texture TexL2_0;

			internal global::UnityEngine.Texture TexL2_1;

			internal global::UnityEngine.Texture TexL2_2;

			internal global::UnityEngine.Texture TexL2_3;

			internal global::UnityEngine.Texture TexProbeOcclusion;

			internal global::UnityEngine.Texture TexValidity;

			internal global::UnityEngine.Texture TexSkyOcclusion;

			internal global::UnityEngine.Texture TexSkyShadingDirectionIndices;

			internal int width;

			internal int height;

			internal int depth;

			internal void Cleanup()
			{
				global::UnityEngine.Rendering.CoreUtils.Destroy(TexL0_L1rx);
				global::UnityEngine.Rendering.CoreUtils.Destroy(TexL1_G_ry);
				global::UnityEngine.Rendering.CoreUtils.Destroy(TexL1_B_rz);
				global::UnityEngine.Rendering.CoreUtils.Destroy(TexL2_0);
				global::UnityEngine.Rendering.CoreUtils.Destroy(TexL2_1);
				global::UnityEngine.Rendering.CoreUtils.Destroy(TexL2_2);
				global::UnityEngine.Rendering.CoreUtils.Destroy(TexL2_3);
				global::UnityEngine.Rendering.CoreUtils.Destroy(TexProbeOcclusion);
				global::UnityEngine.Rendering.CoreUtils.Destroy(TexValidity);
				global::UnityEngine.Rendering.CoreUtils.Destroy(TexSkyOcclusion);
				global::UnityEngine.Rendering.CoreUtils.Destroy(TexSkyShadingDirectionIndices);
				TexL0_L1rx = null;
				TexL1_G_ry = null;
				TexL1_B_rz = null;
				TexL2_0 = null;
				TexL2_1 = null;
				TexL2_2 = null;
				TexL2_3 = null;
				TexProbeOcclusion = null;
				TexValidity = null;
				TexSkyOcclusion = null;
				TexSkyShadingDirectionIndices = null;
			}
		}

		internal static readonly int _Out_L0_L1Rx = global::UnityEngine.Shader.PropertyToID("_Out_L0_L1Rx");

		internal static readonly int _Out_L1G_L1Ry = global::UnityEngine.Shader.PropertyToID("_Out_L1G_L1Ry");

		internal static readonly int _Out_L1B_L1Rz = global::UnityEngine.Shader.PropertyToID("_Out_L1B_L1Rz");

		internal static readonly int _Out_Shared = global::UnityEngine.Shader.PropertyToID("_Out_Shared");

		internal static readonly int _Out_ProbeOcclusion = global::UnityEngine.Shader.PropertyToID("_Out_ProbeOcclusion");

		internal static readonly int _Out_SkyOcclusionL0L1 = global::UnityEngine.Shader.PropertyToID("_Out_SkyOcclusionL0L1");

		internal static readonly int _Out_SkyShadingDirectionIndices = global::UnityEngine.Shader.PropertyToID("_Out_SkyShadingDirectionIndices");

		internal static readonly int _Out_L2_0 = global::UnityEngine.Shader.PropertyToID("_Out_L2_0");

		internal static readonly int _Out_L2_1 = global::UnityEngine.Shader.PropertyToID("_Out_L2_1");

		internal static readonly int _Out_L2_2 = global::UnityEngine.Shader.PropertyToID("_Out_L2_2");

		internal static readonly int _Out_L2_3 = global::UnityEngine.Shader.PropertyToID("_Out_L2_3");

		internal static readonly int _ProbeVolumeScratchBufferLayout = global::UnityEngine.Shader.PropertyToID("CellStreamingScratchBufferLayout");

		internal static readonly int _ProbeVolumeScratchBuffer = global::UnityEngine.Shader.PropertyToID("_ScratchBuffer");

		private const int kChunkSizeInBricks = 128;

		internal const int kBrickCellCount = 3;

		internal const int kBrickProbeCountPerDim = 4;

		internal const int kBrickProbeCountTotal = 64;

		internal const int kChunkProbeCountPerDim = 512;

		private const int kMaxPoolWidth = 2048;

		internal global::UnityEngine.Rendering.ProbeBrickPool.DataLocation m_Pool;

		private global::UnityEngine.Rendering.ProbeBrickPool.BrickChunkAlloc m_NextFreeChunk;

		private global::System.Collections.Generic.Stack<global::UnityEngine.Rendering.ProbeBrickPool.BrickChunkAlloc> m_FreeList;

		private int m_AvailableChunkCount;

		private global::UnityEngine.Rendering.ProbeVolumeSHBands m_SHBands;

		private bool m_ContainsValidity;

		private bool m_ContainsProbeOcclusion;

		private bool m_ContainsRenderingLayers;

		private bool m_ContainsSkyOcclusion;

		private bool m_ContainsSkyShadingDirection;

		private static global::UnityEngine.ComputeShader s_DataUploadCS;

		private static int s_DataUploadKernel;

		private static global::UnityEngine.ComputeShader s_DataUploadL2CS;

		private static int s_DataUploadL2Kernel;

		private static global::UnityEngine.Rendering.LocalKeyword s_DataUpload_Shared;

		private static global::UnityEngine.Rendering.LocalKeyword s_DataUpload_ProbeOcclusion;

		private static global::UnityEngine.Rendering.LocalKeyword s_DataUpload_SkyOcclusion;

		private static global::UnityEngine.Rendering.LocalKeyword s_DataUpload_SkyShadingDirection;

		internal int estimatedVMemCost { get; private set; }

		internal static int DivRoundUp(int x, int y)
		{
			return (x + y - 1) / y;
		}

		internal static void Initialize()
		{
			if (global::UnityEngine.SystemInfo.supportsComputeShaders)
			{
				s_DataUploadCS = global::UnityEngine.Rendering.GraphicsSettings.GetRenderPipelineSettings<global::UnityEngine.Rendering.ProbeVolumeRuntimeResources>()?.probeVolumeUploadDataCS;
				s_DataUploadL2CS = global::UnityEngine.Rendering.GraphicsSettings.GetRenderPipelineSettings<global::UnityEngine.Rendering.ProbeVolumeRuntimeResources>()?.probeVolumeUploadDataL2CS;
				if (s_DataUploadCS != null)
				{
					s_DataUploadKernel = (s_DataUploadCS ? s_DataUploadCS.FindKernel("UploadData") : (-1));
					s_DataUpload_Shared = new global::UnityEngine.Rendering.LocalKeyword(s_DataUploadCS, "PROBE_VOLUMES_SHARED_DATA");
					s_DataUpload_ProbeOcclusion = new global::UnityEngine.Rendering.LocalKeyword(s_DataUploadCS, "PROBE_VOLUMES_PROBE_OCCLUSION");
					s_DataUpload_SkyOcclusion = new global::UnityEngine.Rendering.LocalKeyword(s_DataUploadCS, "PROBE_VOLUMES_SKY_OCCLUSION");
					s_DataUpload_SkyShadingDirection = new global::UnityEngine.Rendering.LocalKeyword(s_DataUploadCS, "PROBE_VOLUMES_SKY_SHADING_DIRECTION");
				}
				if (s_DataUploadL2CS != null)
				{
					s_DataUploadL2Kernel = (s_DataUploadL2CS ? s_DataUploadL2CS.FindKernel("UploadDataL2") : (-1));
				}
			}
		}

		internal global::UnityEngine.Texture GetValidityTexture()
		{
			return m_Pool.TexValidity;
		}

		internal global::UnityEngine.Texture GetSkyOcclusionTexture()
		{
			return m_Pool.TexSkyOcclusion;
		}

		internal global::UnityEngine.Texture GetSkyShadingDirectionIndicesTexture()
		{
			return m_Pool.TexSkyShadingDirectionIndices;
		}

		internal global::UnityEngine.Texture GetProbeOcclusionTexture()
		{
			return m_Pool.TexProbeOcclusion;
		}

		internal ProbeBrickPool(global::UnityEngine.Rendering.ProbeVolumeTextureMemoryBudget memoryBudget, global::UnityEngine.Rendering.ProbeVolumeSHBands shBands, bool allocateValidityData = false, bool allocateRenderingLayerData = false, bool allocateSkyOcclusion = false, bool allocateSkyShadingData = false, bool allocateProbeOcclusionData = false)
		{
			m_NextFreeChunk.x = (m_NextFreeChunk.y = (m_NextFreeChunk.z = 0));
			m_SHBands = shBands;
			m_ContainsValidity = allocateValidityData;
			m_ContainsProbeOcclusion = allocateProbeOcclusionData;
			m_ContainsRenderingLayers = allocateRenderingLayerData;
			m_ContainsSkyOcclusion = allocateSkyOcclusion;
			m_ContainsSkyShadingDirection = allocateSkyShadingData;
			m_FreeList = new global::System.Collections.Generic.Stack<global::UnityEngine.Rendering.ProbeBrickPool.BrickChunkAlloc>(256);
			DerivePoolSizeFromBudget(memoryBudget, out var width, out var height, out var depth);
			AllocatePool(width, height, depth);
			m_AvailableChunkCount = m_Pool.width / 512 * (m_Pool.height / 4) * (m_Pool.depth / 4);
		}

		internal void AllocatePool(int width, int height, int depth)
		{
			m_Pool = CreateDataLocation(width * height * depth, compressed: false, m_SHBands, "APV", allocateRendertexture: true, m_ContainsValidity, m_ContainsRenderingLayers, m_ContainsSkyOcclusion, m_ContainsSkyShadingDirection, m_ContainsProbeOcclusion, out var allocatedBytes);
			estimatedVMemCost = allocatedBytes;
		}

		public int GetRemainingChunkCount()
		{
			return m_AvailableChunkCount;
		}

		internal void EnsureTextureValidity()
		{
			if (m_Pool.TexL0_L1rx == null)
			{
				m_Pool.Cleanup();
				AllocatePool(m_Pool.width, m_Pool.height, m_Pool.depth);
			}
		}

		internal bool EnsureTextureValidity(bool renderingLayers, bool skyOcclusion, bool skyDirection, bool probeOcclusion)
		{
			if (m_ContainsRenderingLayers != renderingLayers || m_ContainsSkyOcclusion != skyOcclusion || m_ContainsSkyShadingDirection != skyDirection || m_ContainsProbeOcclusion != probeOcclusion)
			{
				m_Pool.Cleanup();
				m_ContainsRenderingLayers = renderingLayers;
				m_ContainsSkyOcclusion = skyOcclusion;
				m_ContainsSkyShadingDirection = skyDirection;
				m_ContainsProbeOcclusion = probeOcclusion;
				AllocatePool(m_Pool.width, m_Pool.height, m_Pool.depth);
				return false;
			}
			return true;
		}

		internal static int GetChunkSizeInBrickCount()
		{
			return 128;
		}

		internal static int GetChunkSizeInProbeCount()
		{
			return 8192;
		}

		internal int GetPoolWidth()
		{
			return m_Pool.width;
		}

		internal int GetPoolHeight()
		{
			return m_Pool.height;
		}

		internal global::UnityEngine.Vector3Int GetPoolDimensions()
		{
			return new global::UnityEngine.Vector3Int(m_Pool.width, m_Pool.height, m_Pool.depth);
		}

		internal void GetRuntimeResources(ref global::UnityEngine.Rendering.ProbeReferenceVolume.RuntimeResources rr)
		{
			rr.L0_L1rx = m_Pool.TexL0_L1rx as global::UnityEngine.RenderTexture;
			rr.L1_G_ry = m_Pool.TexL1_G_ry as global::UnityEngine.RenderTexture;
			rr.L1_B_rz = m_Pool.TexL1_B_rz as global::UnityEngine.RenderTexture;
			rr.L2_0 = m_Pool.TexL2_0 as global::UnityEngine.RenderTexture;
			rr.L2_1 = m_Pool.TexL2_1 as global::UnityEngine.RenderTexture;
			rr.L2_2 = m_Pool.TexL2_2 as global::UnityEngine.RenderTexture;
			rr.L2_3 = m_Pool.TexL2_3 as global::UnityEngine.RenderTexture;
			rr.ProbeOcclusion = m_Pool.TexProbeOcclusion as global::UnityEngine.RenderTexture;
			rr.Validity = m_Pool.TexValidity as global::UnityEngine.RenderTexture;
			rr.SkyOcclusionL0L1 = m_Pool.TexSkyOcclusion as global::UnityEngine.RenderTexture;
			rr.SkyShadingDirectionIndices = m_Pool.TexSkyShadingDirectionIndices as global::UnityEngine.RenderTexture;
		}

		internal void Clear()
		{
			m_FreeList.Clear();
			m_NextFreeChunk.x = (m_NextFreeChunk.y = (m_NextFreeChunk.z = 0));
		}

		internal static int GetChunkCount(int brickCount)
		{
			int num = 128;
			return (brickCount + num - 1) / num;
		}

		internal bool Allocate(int numberOfBrickChunks, global::System.Collections.Generic.List<global::UnityEngine.Rendering.ProbeBrickPool.BrickChunkAlloc> outAllocations, bool ignoreErrorLog)
		{
			while (m_FreeList.Count > 0 && numberOfBrickChunks > 0)
			{
				outAllocations.Add(m_FreeList.Pop());
				numberOfBrickChunks--;
				m_AvailableChunkCount--;
			}
			for (uint num = 0u; num < numberOfBrickChunks; num++)
			{
				if (m_NextFreeChunk.z >= m_Pool.depth)
				{
					if (!ignoreErrorLog)
					{
						global::UnityEngine.Debug.LogError("Cannot allocate more brick chunks, probe volume brick pool is full.");
					}
					Deallocate(outAllocations);
					outAllocations.Clear();
					return false;
				}
				outAllocations.Add(m_NextFreeChunk);
				m_AvailableChunkCount--;
				m_NextFreeChunk.x += 512;
				if (m_NextFreeChunk.x >= m_Pool.width)
				{
					m_NextFreeChunk.x = 0;
					m_NextFreeChunk.y += 4;
					if (m_NextFreeChunk.y >= m_Pool.height)
					{
						m_NextFreeChunk.y = 0;
						m_NextFreeChunk.z += 4;
					}
				}
			}
			return true;
		}

		internal void Deallocate(global::System.Collections.Generic.List<global::UnityEngine.Rendering.ProbeBrickPool.BrickChunkAlloc> allocations)
		{
			m_AvailableChunkCount += allocations.Count;
			foreach (global::UnityEngine.Rendering.ProbeBrickPool.BrickChunkAlloc allocation in allocations)
			{
				m_FreeList.Push(allocation);
			}
		}

		internal void Update(global::UnityEngine.Rendering.ProbeBrickPool.DataLocation source, global::System.Collections.Generic.List<global::UnityEngine.Rendering.ProbeBrickPool.BrickChunkAlloc> srcLocations, global::System.Collections.Generic.List<global::UnityEngine.Rendering.ProbeBrickPool.BrickChunkAlloc> dstLocations, int destStartIndex, global::UnityEngine.Rendering.ProbeVolumeSHBands bands)
		{
			for (int i = 0; i < srcLocations.Count; i++)
			{
				global::UnityEngine.Rendering.ProbeBrickPool.BrickChunkAlloc brickChunkAlloc = srcLocations[i];
				global::UnityEngine.Rendering.ProbeBrickPool.BrickChunkAlloc brickChunkAlloc2 = dstLocations[destStartIndex + i];
				for (int j = 0; j < 4; j++)
				{
					int srcWidth = global::UnityEngine.Mathf.Min(512, source.width - brickChunkAlloc.x);
					global::UnityEngine.Graphics.CopyTexture(source.TexL0_L1rx, brickChunkAlloc.z + j, 0, brickChunkAlloc.x, brickChunkAlloc.y, srcWidth, 4, m_Pool.TexL0_L1rx, brickChunkAlloc2.z + j, 0, brickChunkAlloc2.x, brickChunkAlloc2.y);
					global::UnityEngine.Graphics.CopyTexture(source.TexL1_G_ry, brickChunkAlloc.z + j, 0, brickChunkAlloc.x, brickChunkAlloc.y, srcWidth, 4, m_Pool.TexL1_G_ry, brickChunkAlloc2.z + j, 0, brickChunkAlloc2.x, brickChunkAlloc2.y);
					global::UnityEngine.Graphics.CopyTexture(source.TexL1_B_rz, brickChunkAlloc.z + j, 0, brickChunkAlloc.x, brickChunkAlloc.y, srcWidth, 4, m_Pool.TexL1_B_rz, brickChunkAlloc2.z + j, 0, brickChunkAlloc2.x, brickChunkAlloc2.y);
					if (m_ContainsValidity)
					{
						global::UnityEngine.Graphics.CopyTexture(source.TexValidity, brickChunkAlloc.z + j, 0, brickChunkAlloc.x, brickChunkAlloc.y, srcWidth, 4, m_Pool.TexValidity, brickChunkAlloc2.z + j, 0, brickChunkAlloc2.x, brickChunkAlloc2.y);
					}
					if (m_ContainsSkyOcclusion)
					{
						global::UnityEngine.Graphics.CopyTexture(source.TexSkyOcclusion, brickChunkAlloc.z + j, 0, brickChunkAlloc.x, brickChunkAlloc.y, srcWidth, 4, m_Pool.TexSkyOcclusion, brickChunkAlloc2.z + j, 0, brickChunkAlloc2.x, brickChunkAlloc2.y);
						if (m_ContainsSkyShadingDirection)
						{
							global::UnityEngine.Graphics.CopyTexture(source.TexSkyShadingDirectionIndices, brickChunkAlloc.z + j, 0, brickChunkAlloc.x, brickChunkAlloc.y, srcWidth, 4, m_Pool.TexSkyShadingDirectionIndices, brickChunkAlloc2.z + j, 0, brickChunkAlloc2.x, brickChunkAlloc2.y);
						}
					}
					if (bands == global::UnityEngine.Rendering.ProbeVolumeSHBands.SphericalHarmonicsL2)
					{
						global::UnityEngine.Graphics.CopyTexture(source.TexL2_0, brickChunkAlloc.z + j, 0, brickChunkAlloc.x, brickChunkAlloc.y, srcWidth, 4, m_Pool.TexL2_0, brickChunkAlloc2.z + j, 0, brickChunkAlloc2.x, brickChunkAlloc2.y);
						global::UnityEngine.Graphics.CopyTexture(source.TexL2_1, brickChunkAlloc.z + j, 0, brickChunkAlloc.x, brickChunkAlloc.y, srcWidth, 4, m_Pool.TexL2_1, brickChunkAlloc2.z + j, 0, brickChunkAlloc2.x, brickChunkAlloc2.y);
						global::UnityEngine.Graphics.CopyTexture(source.TexL2_2, brickChunkAlloc.z + j, 0, brickChunkAlloc.x, brickChunkAlloc.y, srcWidth, 4, m_Pool.TexL2_2, brickChunkAlloc2.z + j, 0, brickChunkAlloc2.x, brickChunkAlloc2.y);
						global::UnityEngine.Graphics.CopyTexture(source.TexL2_3, brickChunkAlloc.z + j, 0, brickChunkAlloc.x, brickChunkAlloc.y, srcWidth, 4, m_Pool.TexL2_3, brickChunkAlloc2.z + j, 0, brickChunkAlloc2.x, brickChunkAlloc2.y);
					}
					if (m_ContainsProbeOcclusion)
					{
						global::UnityEngine.Graphics.CopyTexture(source.TexProbeOcclusion, brickChunkAlloc.z + j, 0, brickChunkAlloc.x, brickChunkAlloc.y, srcWidth, 4, m_Pool.TexProbeOcclusion, brickChunkAlloc2.z + j, 0, brickChunkAlloc2.x, brickChunkAlloc2.y);
					}
				}
			}
		}

		internal void Update(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Rendering.ProbeReferenceVolume.CellStreamingScratchBuffer dataBuffer, global::UnityEngine.Rendering.ProbeReferenceVolume.CellStreamingScratchBufferLayout layout, global::System.Collections.Generic.List<global::UnityEngine.Rendering.ProbeBrickPool.BrickChunkAlloc> dstLocations, bool updateSharedData, global::UnityEngine.Texture validityTexture, global::UnityEngine.Rendering.ProbeVolumeSHBands bands, bool skyOcclusion, global::UnityEngine.Texture skyOcclusionTexture, bool skyShadingDirections, global::UnityEngine.Texture skyShadingDirectionsTexture, bool probeOcclusion)
		{
			using (new global::UnityEngine.Rendering.ProfilingScope(cmd, global::UnityEngine.Rendering.ProfilingSampler.Get(global::UnityEngine.Rendering.CoreProfileId.APVDiskStreamingUpdatePool)))
			{
				int count = dstLocations.Count;
				cmd.SetComputeTextureParam(s_DataUploadCS, s_DataUploadKernel, _Out_L0_L1Rx, m_Pool.TexL0_L1rx);
				cmd.SetComputeTextureParam(s_DataUploadCS, s_DataUploadKernel, _Out_L1G_L1Ry, m_Pool.TexL1_G_ry);
				cmd.SetComputeTextureParam(s_DataUploadCS, s_DataUploadKernel, _Out_L1B_L1Rz, m_Pool.TexL1_B_rz);
				if (updateSharedData)
				{
					cmd.EnableKeyword(s_DataUploadCS, in s_DataUpload_Shared);
					cmd.SetComputeTextureParam(s_DataUploadCS, s_DataUploadKernel, _Out_Shared, validityTexture);
					if (skyOcclusion)
					{
						cmd.EnableKeyword(s_DataUploadCS, in s_DataUpload_SkyOcclusion);
						cmd.SetComputeTextureParam(s_DataUploadCS, s_DataUploadKernel, _Out_SkyOcclusionL0L1, skyOcclusionTexture);
						if (skyShadingDirections)
						{
							cmd.SetComputeTextureParam(s_DataUploadCS, s_DataUploadKernel, _Out_SkyShadingDirectionIndices, skyShadingDirectionsTexture);
							cmd.EnableKeyword(s_DataUploadCS, in s_DataUpload_SkyShadingDirection);
						}
						else
						{
							cmd.DisableKeyword(s_DataUploadCS, in s_DataUpload_SkyShadingDirection);
						}
					}
				}
				else
				{
					cmd.DisableKeyword(s_DataUploadCS, in s_DataUpload_Shared);
					cmd.DisableKeyword(s_DataUploadCS, in s_DataUpload_SkyOcclusion);
					cmd.DisableKeyword(s_DataUploadCS, in s_DataUpload_SkyShadingDirection);
				}
				if (bands == global::UnityEngine.Rendering.ProbeVolumeSHBands.SphericalHarmonicsL2)
				{
					cmd.SetComputeTextureParam(s_DataUploadL2CS, s_DataUploadL2Kernel, _Out_L2_0, m_Pool.TexL2_0);
					cmd.SetComputeTextureParam(s_DataUploadL2CS, s_DataUploadL2Kernel, _Out_L2_1, m_Pool.TexL2_1);
					cmd.SetComputeTextureParam(s_DataUploadL2CS, s_DataUploadL2Kernel, _Out_L2_2, m_Pool.TexL2_2);
					cmd.SetComputeTextureParam(s_DataUploadL2CS, s_DataUploadL2Kernel, _Out_L2_3, m_Pool.TexL2_3);
				}
				if (probeOcclusion)
				{
					cmd.EnableKeyword(s_DataUploadCS, in s_DataUpload_ProbeOcclusion);
					cmd.SetComputeTextureParam(s_DataUploadCS, s_DataUploadKernel, _Out_ProbeOcclusion, m_Pool.TexProbeOcclusion);
				}
				else
				{
					cmd.DisableKeyword(s_DataUploadCS, in s_DataUpload_ProbeOcclusion);
				}
				int threadGroupsX = DivRoundUp(2048, 64);
				global::UnityEngine.Rendering.ConstantBuffer.Push(cmd, in layout, s_DataUploadCS, _ProbeVolumeScratchBufferLayout);
				cmd.SetComputeBufferParam(s_DataUploadCS, s_DataUploadKernel, _ProbeVolumeScratchBuffer, dataBuffer.buffer);
				cmd.DispatchCompute(s_DataUploadCS, s_DataUploadKernel, threadGroupsX, 1, count);
				if (bands == global::UnityEngine.Rendering.ProbeVolumeSHBands.SphericalHarmonicsL2)
				{
					global::UnityEngine.Rendering.ConstantBuffer.Push(cmd, in layout, s_DataUploadL2CS, _ProbeVolumeScratchBufferLayout);
					cmd.SetComputeBufferParam(s_DataUploadL2CS, s_DataUploadL2Kernel, _ProbeVolumeScratchBuffer, dataBuffer.buffer);
					cmd.DispatchCompute(s_DataUploadL2CS, s_DataUploadL2Kernel, threadGroupsX, 1, count);
				}
			}
		}

		internal void UpdateValidity(global::UnityEngine.Rendering.ProbeBrickPool.DataLocation source, global::System.Collections.Generic.List<global::UnityEngine.Rendering.ProbeBrickPool.BrickChunkAlloc> srcLocations, global::System.Collections.Generic.List<global::UnityEngine.Rendering.ProbeBrickPool.BrickChunkAlloc> dstLocations, int destStartIndex)
		{
			for (int i = 0; i < srcLocations.Count; i++)
			{
				global::UnityEngine.Rendering.ProbeBrickPool.BrickChunkAlloc brickChunkAlloc = srcLocations[i];
				global::UnityEngine.Rendering.ProbeBrickPool.BrickChunkAlloc brickChunkAlloc2 = dstLocations[destStartIndex + i];
				for (int j = 0; j < 4; j++)
				{
					int srcWidth = global::UnityEngine.Mathf.Min(512, source.width - brickChunkAlloc.x);
					global::UnityEngine.Graphics.CopyTexture(source.TexValidity, brickChunkAlloc.z + j, 0, brickChunkAlloc.x, brickChunkAlloc.y, srcWidth, 4, m_Pool.TexValidity, brickChunkAlloc2.z + j, 0, brickChunkAlloc2.x, brickChunkAlloc2.y);
				}
			}
		}

		internal static global::UnityEngine.Vector3Int ProbeCountToDataLocSize(int numProbes)
		{
			int num = numProbes / 64;
			int num2 = 512;
			int num3 = (num + num2 * num2 - 1) / (num2 * num2);
			int num4;
			int num5;
			if (num3 > 1)
			{
				num4 = (num5 = num2);
			}
			else
			{
				num5 = (num + num2 - 1) / num2;
				num4 = ((num5 <= 1) ? num : num2);
			}
			num4 *= 4;
			num5 *= 4;
			num3 *= 4;
			return new global::UnityEngine.Vector3Int(num4, num5, num3);
		}

		private static int EstimateMemoryCost(int width, int height, int depth, global::UnityEngine.Experimental.Rendering.GraphicsFormat format)
		{
			return width * height * depth * format switch
			{
				global::UnityEngine.Experimental.Rendering.GraphicsFormat.R8G8B8A8_UNorm => 4, 
				global::UnityEngine.Experimental.Rendering.GraphicsFormat.R16G16B16A16_SFloat => 8, 
				_ => 1, 
			};
		}

		internal static int EstimateMemoryCostForBlending(global::UnityEngine.Rendering.ProbeVolumeTextureMemoryBudget memoryBudget, bool compressed, global::UnityEngine.Rendering.ProbeVolumeSHBands bands)
		{
			if (memoryBudget == (global::UnityEngine.Rendering.ProbeVolumeTextureMemoryBudget)0)
			{
				return 0;
			}
			DerivePoolSizeFromBudget(memoryBudget, out var width, out var height, out var depth);
			global::UnityEngine.Vector3Int vector3Int = ProbeCountToDataLocSize(width * height * depth);
			width = vector3Int.x;
			height = vector3Int.y;
			depth = vector3Int.z;
			int num = 0;
			global::UnityEngine.Experimental.Rendering.GraphicsFormat format = global::UnityEngine.Experimental.Rendering.GraphicsFormat.R16G16B16A16_SFloat;
			global::UnityEngine.Experimental.Rendering.GraphicsFormat format2 = (compressed ? global::UnityEngine.Experimental.Rendering.GraphicsFormat.RGBA_BC7_UNorm : global::UnityEngine.Experimental.Rendering.GraphicsFormat.R8G8B8A8_UNorm);
			num += EstimateMemoryCost(width, height, depth, format);
			num += EstimateMemoryCost(width, height, depth, format2) * 2;
			if (bands == global::UnityEngine.Rendering.ProbeVolumeSHBands.SphericalHarmonicsL2)
			{
				num += EstimateMemoryCost(width, height, depth, format2) * 3;
			}
			return num;
		}

		public static global::UnityEngine.Texture CreateDataTexture(int width, int height, int depth, global::UnityEngine.Experimental.Rendering.GraphicsFormat format, string name, bool allocateRendertexture, ref int allocatedBytes)
		{
			allocatedBytes += EstimateMemoryCost(width, height, depth, format);
			global::UnityEngine.Texture texture = ((!allocateRendertexture) ? ((global::UnityEngine.Texture)new global::UnityEngine.Texture3D(width, height, depth, format, global::UnityEngine.Experimental.Rendering.TextureCreationFlags.None, 1)) : ((global::UnityEngine.Texture)new global::UnityEngine.RenderTexture(new global::UnityEngine.RenderTextureDescriptor
			{
				width = width,
				height = height,
				volumeDepth = depth,
				graphicsFormat = format,
				mipCount = 1,
				enableRandomWrite = global::UnityEngine.SystemInfo.supportsComputeShaders,
				dimension = global::UnityEngine.Rendering.TextureDimension.Tex3D,
				msaaSamples = 1
			})));
			texture.hideFlags = global::UnityEngine.HideFlags.HideAndDontSave;
			texture.name = name;
			if (allocateRendertexture)
			{
				(texture as global::UnityEngine.RenderTexture).Create();
			}
			return texture;
		}

		public static global::UnityEngine.Rendering.ProbeBrickPool.DataLocation CreateDataLocation(int numProbes, bool compressed, global::UnityEngine.Rendering.ProbeVolumeSHBands bands, string name, bool allocateRendertexture, bool allocateValidityData, bool allocateRenderingLayers, bool allocateSkyOcclusionData, bool allocateSkyShadingDirectionData, bool allocateProbeOcclusionData, out int allocatedBytes)
		{
			global::UnityEngine.Vector3Int vector3Int = ProbeCountToDataLocSize(numProbes);
			int x = vector3Int.x;
			int y = vector3Int.y;
			int z = vector3Int.z;
			global::UnityEngine.Experimental.Rendering.GraphicsFormat format = global::UnityEngine.Experimental.Rendering.GraphicsFormat.R16G16B16A16_SFloat;
			global::UnityEngine.Experimental.Rendering.GraphicsFormat format2 = (compressed ? global::UnityEngine.Experimental.Rendering.GraphicsFormat.RGBA_BC7_UNorm : global::UnityEngine.Experimental.Rendering.GraphicsFormat.R8G8B8A8_UNorm);
			global::UnityEngine.Experimental.Rendering.GraphicsFormat format3 = (allocateRenderingLayers ? global::UnityEngine.Experimental.Rendering.GraphicsFormat.R32_SFloat : (global::UnityEngine.SystemInfo.IsFormatSupported(global::UnityEngine.Experimental.Rendering.GraphicsFormat.R8_UNorm, global::UnityEngine.Experimental.Rendering.GraphicsFormatUsage.Sample | global::UnityEngine.Experimental.Rendering.GraphicsFormatUsage.LoadStore) ? global::UnityEngine.Experimental.Rendering.GraphicsFormat.R8_UNorm : global::UnityEngine.Experimental.Rendering.GraphicsFormat.R8G8B8A8_UNorm));
			allocatedBytes = 0;
			global::UnityEngine.Rendering.ProbeBrickPool.DataLocation result = default(global::UnityEngine.Rendering.ProbeBrickPool.DataLocation);
			result.TexL0_L1rx = CreateDataTexture(x, y, z, format, name + "_TexL0_L1rx", allocateRendertexture, ref allocatedBytes);
			result.TexL1_G_ry = CreateDataTexture(x, y, z, format2, name + "_TexL1_G_ry", allocateRendertexture, ref allocatedBytes);
			result.TexL1_B_rz = CreateDataTexture(x, y, z, format2, name + "_TexL1_B_rz", allocateRendertexture, ref allocatedBytes);
			if (allocateValidityData)
			{
				result.TexValidity = CreateDataTexture(x, y, z, format3, name + "_Validity", allocateRendertexture, ref allocatedBytes);
			}
			else
			{
				result.TexValidity = null;
			}
			if (allocateSkyOcclusionData)
			{
				result.TexSkyOcclusion = CreateDataTexture(x, y, z, global::UnityEngine.Experimental.Rendering.GraphicsFormat.R16G16B16A16_SFloat, name + "_SkyOcclusion", allocateRendertexture, ref allocatedBytes);
			}
			else
			{
				result.TexSkyOcclusion = null;
			}
			if (allocateSkyShadingDirectionData)
			{
				result.TexSkyShadingDirectionIndices = CreateDataTexture(x, y, z, global::UnityEngine.Experimental.Rendering.GraphicsFormat.R8_UNorm, name + "_SkyShadingDirectionIndices", allocateRendertexture, ref allocatedBytes);
			}
			else
			{
				result.TexSkyShadingDirectionIndices = null;
			}
			if (allocateProbeOcclusionData)
			{
				result.TexProbeOcclusion = CreateDataTexture(x, y, z, global::UnityEngine.Experimental.Rendering.GraphicsFormat.R8G8B8A8_UNorm, name + "_ProbeOcclusion", allocateRendertexture, ref allocatedBytes);
			}
			else
			{
				result.TexProbeOcclusion = null;
			}
			if (bands == global::UnityEngine.Rendering.ProbeVolumeSHBands.SphericalHarmonicsL2)
			{
				result.TexL2_0 = CreateDataTexture(x, y, z, format2, name + "_TexL2_0", allocateRendertexture, ref allocatedBytes);
				result.TexL2_1 = CreateDataTexture(x, y, z, format2, name + "_TexL2_1", allocateRendertexture, ref allocatedBytes);
				result.TexL2_2 = CreateDataTexture(x, y, z, format2, name + "_TexL2_2", allocateRendertexture, ref allocatedBytes);
				result.TexL2_3 = CreateDataTexture(x, y, z, format2, name + "_TexL2_3", allocateRendertexture, ref allocatedBytes);
			}
			else
			{
				result.TexL2_0 = null;
				result.TexL2_1 = null;
				result.TexL2_2 = null;
				result.TexL2_3 = null;
			}
			result.width = x;
			result.height = y;
			result.depth = z;
			return result;
		}

		private static void DerivePoolSizeFromBudget(global::UnityEngine.Rendering.ProbeVolumeTextureMemoryBudget memoryBudget, out int width, out int height, out int depth)
		{
			width = (int)memoryBudget;
			height = (int)memoryBudget;
			depth = 4;
		}

		internal void Cleanup()
		{
			m_Pool.Cleanup();
		}
	}
}
