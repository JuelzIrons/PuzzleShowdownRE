namespace UnityEngine.Rendering
{
	public class ProbeReferenceVolume
	{
		internal static class ShaderIDs
		{
			public static readonly int _APVResIndex = global::UnityEngine.Shader.PropertyToID("_APVResIndex");

			public static readonly int _APVResCellIndices = global::UnityEngine.Shader.PropertyToID("_APVResCellIndices");

			public static readonly int _APVResL0_L1Rx = global::UnityEngine.Shader.PropertyToID("_APVResL0_L1Rx");

			public static readonly int _APVResL1G_L1Ry = global::UnityEngine.Shader.PropertyToID("_APVResL1G_L1Ry");

			public static readonly int _APVResL1B_L1Rz = global::UnityEngine.Shader.PropertyToID("_APVResL1B_L1Rz");

			public static readonly int _APVResL2_0 = global::UnityEngine.Shader.PropertyToID("_APVResL2_0");

			public static readonly int _APVResL2_1 = global::UnityEngine.Shader.PropertyToID("_APVResL2_1");

			public static readonly int _APVResL2_2 = global::UnityEngine.Shader.PropertyToID("_APVResL2_2");

			public static readonly int _APVResL2_3 = global::UnityEngine.Shader.PropertyToID("_APVResL2_3");

			public static readonly int _APVProbeOcclusion = global::UnityEngine.Shader.PropertyToID("_APVProbeOcclusion");

			public static readonly int _APVResValidity = global::UnityEngine.Shader.PropertyToID("_APVResValidity");

			public static readonly int _SkyOcclusionTexL0L1 = global::UnityEngine.Shader.PropertyToID("_SkyOcclusionTexL0L1");

			public static readonly int _SkyShadingDirectionIndicesTex = global::UnityEngine.Shader.PropertyToID("_SkyShadingDirectionIndicesTex");

			public static readonly int _SkyPrecomputedDirections = global::UnityEngine.Shader.PropertyToID("_SkyPrecomputedDirections");

			public static readonly int _AntiLeakData = global::UnityEngine.Shader.PropertyToID("_AntiLeakData");
		}

		[global::System.Serializable]
		internal struct IndirectionEntryInfo
		{
			public global::UnityEngine.Vector3Int positionInBricks;

			public int minSubdiv;

			public global::UnityEngine.Vector3Int minBrickPos;

			public global::UnityEngine.Vector3Int maxBrickPosPlusOne;

			public bool hasMinMax;

			public bool hasOnlyBiggerBricks;
		}

		[global::System.Serializable]
		internal class CellDesc
		{
			public global::UnityEngine.Vector3Int position;

			public int index;

			public int probeCount;

			public int minSubdiv;

			public int indexChunkCount;

			public int shChunkCount;

			public int bricksCount;

			public global::UnityEngine.Rendering.ProbeReferenceVolume.IndirectionEntryInfo[] indirectionEntryInfo;

			public override string ToString()
			{
				return $"Index = {index} position = {position}";
			}
		}

		internal class CellData
		{
			public struct PerScenarioData
			{
				public global::Unity.Collections.NativeArray<ushort> shL0L1RxData;

				public global::Unity.Collections.NativeArray<byte> shL1GL1RyData;

				public global::Unity.Collections.NativeArray<byte> shL1BL1RzData;

				public global::Unity.Collections.NativeArray<byte> shL2Data_0;

				public global::Unity.Collections.NativeArray<byte> shL2Data_1;

				public global::Unity.Collections.NativeArray<byte> shL2Data_2;

				public global::Unity.Collections.NativeArray<byte> shL2Data_3;

				public global::Unity.Collections.NativeArray<byte> probeOcclusion;
			}

			public global::Unity.Collections.NativeArray<byte> validityNeighMaskData;

			public global::System.Collections.Generic.Dictionary<string, global::UnityEngine.Rendering.ProbeReferenceVolume.CellData.PerScenarioData> scenarios = new global::System.Collections.Generic.Dictionary<string, global::UnityEngine.Rendering.ProbeReferenceVolume.CellData.PerScenarioData>();

			public global::Unity.Collections.NativeArray<ushort> skyOcclusionDataL0L1 { get; internal set; }

			public global::Unity.Collections.NativeArray<byte> skyShadingDirectionIndices { get; internal set; }

			public global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.ProbeBrickIndex.Brick> bricks { get; internal set; }

			public global::Unity.Collections.NativeArray<global::UnityEngine.Vector3> probePositions { get; internal set; }

			public global::Unity.Collections.NativeArray<float> touchupVolumeInteraction { get; internal set; }

			public global::Unity.Collections.NativeArray<global::UnityEngine.Vector3> offsetVectors { get; internal set; }

			public global::Unity.Collections.NativeArray<float> validity { get; internal set; }

			public global::Unity.Collections.NativeArray<byte> layer { get; internal set; }

			public void CleanupPerScenarioData(in global::UnityEngine.Rendering.ProbeReferenceVolume.CellData.PerScenarioData data)
			{
				if (data.shL0L1RxData.IsCreated)
				{
					data.shL0L1RxData.Dispose();
					data.shL1GL1RyData.Dispose();
					data.shL1BL1RzData.Dispose();
				}
				if (data.shL2Data_0.IsCreated)
				{
					data.shL2Data_0.Dispose();
					data.shL2Data_1.Dispose();
					data.shL2Data_2.Dispose();
					data.shL2Data_3.Dispose();
				}
				if (data.probeOcclusion.IsCreated)
				{
					data.probeOcclusion.Dispose();
				}
			}

			public void Cleanup(bool cleanScenarioList)
			{
				if (validityNeighMaskData.IsCreated)
				{
					validityNeighMaskData.Dispose();
					validityNeighMaskData = default(global::Unity.Collections.NativeArray<byte>);
					foreach (global::UnityEngine.Rendering.ProbeReferenceVolume.CellData.PerScenarioData value in scenarios.Values)
					{
						CleanupPerScenarioData(value);
					}
				}
				if (cleanScenarioList)
				{
					scenarios.Clear();
				}
				if (bricks.IsCreated)
				{
					bricks.Dispose();
					bricks = default(global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.ProbeBrickIndex.Brick>);
				}
				if (skyOcclusionDataL0L1.IsCreated)
				{
					skyOcclusionDataL0L1.Dispose();
					skyOcclusionDataL0L1 = default(global::Unity.Collections.NativeArray<ushort>);
				}
				if (skyShadingDirectionIndices.IsCreated)
				{
					skyShadingDirectionIndices.Dispose();
					skyShadingDirectionIndices = default(global::Unity.Collections.NativeArray<byte>);
				}
				if (probePositions.IsCreated)
				{
					probePositions.Dispose();
					probePositions = default(global::Unity.Collections.NativeArray<global::UnityEngine.Vector3>);
				}
				if (touchupVolumeInteraction.IsCreated)
				{
					touchupVolumeInteraction.Dispose();
					touchupVolumeInteraction = default(global::Unity.Collections.NativeArray<float>);
				}
				if (validity.IsCreated)
				{
					validity.Dispose();
					validity = default(global::Unity.Collections.NativeArray<float>);
				}
				if (layer.IsCreated)
				{
					layer.Dispose();
					layer = default(global::Unity.Collections.NativeArray<byte>);
				}
				if (offsetVectors.IsCreated)
				{
					offsetVectors.Dispose();
					offsetVectors = default(global::Unity.Collections.NativeArray<global::UnityEngine.Vector3>);
				}
			}
		}

		internal class CellPoolInfo
		{
			public global::System.Collections.Generic.List<global::UnityEngine.Rendering.ProbeBrickPool.BrickChunkAlloc> chunkList = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.ProbeBrickPool.BrickChunkAlloc>();

			public int shChunkCount;

			public void Clear()
			{
				chunkList.Clear();
			}
		}

		internal class CellIndexInfo
		{
			public int[] flatIndicesInGlobalIndirection;

			public global::UnityEngine.Rendering.ProbeBrickIndex.CellIndexUpdateInfo updateInfo;

			public bool indexUpdated;

			public global::UnityEngine.Rendering.ProbeReferenceVolume.IndirectionEntryInfo[] indirectionEntryInfo;

			public int indexChunkCount;

			public void Clear()
			{
				flatIndicesInGlobalIndirection = null;
				updateInfo = default(global::UnityEngine.Rendering.ProbeBrickIndex.CellIndexUpdateInfo);
				indexUpdated = false;
				indirectionEntryInfo = null;
			}
		}

		internal class CellBlendingInfo
		{
			public global::System.Collections.Generic.List<global::UnityEngine.Rendering.ProbeBrickPool.BrickChunkAlloc> chunkList = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.ProbeBrickPool.BrickChunkAlloc>();

			public float blendingScore;

			public float blendingFactor;

			public bool blending;

			public void MarkUpToDate()
			{
				blendingScore = float.MaxValue;
			}

			public bool IsUpToDate()
			{
				return blendingScore == float.MaxValue;
			}

			public void ForceReupload()
			{
				blendingFactor = -1f;
			}

			public bool ShouldReupload()
			{
				return blendingFactor == -1f;
			}

			public void Prioritize()
			{
				blendingFactor = -2f;
			}

			public bool ShouldPrioritize()
			{
				return blendingFactor == -2f;
			}

			public void Clear()
			{
				chunkList.Clear();
				blendingScore = 0f;
				blendingFactor = 0f;
				blending = false;
			}
		}

		internal class CellStreamingInfo
		{
			public global::UnityEngine.Rendering.ProbeReferenceVolume.CellStreamingRequest request;

			public global::UnityEngine.Rendering.ProbeReferenceVolume.CellStreamingRequest blendingRequest0;

			public global::UnityEngine.Rendering.ProbeReferenceVolume.CellStreamingRequest blendingRequest1;

			public float streamingScore;

			public bool IsStreaming()
			{
				if (request != null)
				{
					return request.IsStreaming();
				}
				return false;
			}

			public bool IsBlendingStreaming()
			{
				if (blendingRequest0 == null || !blendingRequest0.IsStreaming())
				{
					if (blendingRequest1 != null)
					{
						return blendingRequest1.IsStreaming();
					}
					return false;
				}
				return true;
			}

			public void Clear()
			{
				request = null;
				blendingRequest0 = null;
				blendingRequest1 = null;
				streamingScore = 0f;
			}
		}

		[global::System.Diagnostics.DebuggerDisplay("Index = {desc.index} Loaded = {loaded}")]
		internal class Cell : global::System.IComparable<global::UnityEngine.Rendering.ProbeReferenceVolume.Cell>
		{
			public global::UnityEngine.Rendering.ProbeReferenceVolume.CellDesc desc;

			public global::UnityEngine.Rendering.ProbeReferenceVolume.CellData data;

			public global::UnityEngine.Rendering.ProbeReferenceVolume.CellPoolInfo poolInfo = new global::UnityEngine.Rendering.ProbeReferenceVolume.CellPoolInfo();

			public global::UnityEngine.Rendering.ProbeReferenceVolume.CellIndexInfo indexInfo = new global::UnityEngine.Rendering.ProbeReferenceVolume.CellIndexInfo();

			public global::UnityEngine.Rendering.ProbeReferenceVolume.CellBlendingInfo blendingInfo = new global::UnityEngine.Rendering.ProbeReferenceVolume.CellBlendingInfo();

			public global::UnityEngine.Rendering.ProbeReferenceVolume.CellStreamingInfo streamingInfo = new global::UnityEngine.Rendering.ProbeReferenceVolume.CellStreamingInfo();

			public int referenceCount;

			public bool loaded;

			public global::UnityEngine.Rendering.ProbeReferenceVolume.CellData.PerScenarioData scenario0;

			public global::UnityEngine.Rendering.ProbeReferenceVolume.CellData.PerScenarioData scenario1;

			public bool hasTwoScenarios;

			public global::UnityEngine.Rendering.ProbeReferenceVolume.CellInstancedDebugProbes debugProbes;

			public int CompareTo(global::UnityEngine.Rendering.ProbeReferenceVolume.Cell other)
			{
				if (streamingInfo.streamingScore < other.streamingInfo.streamingScore)
				{
					return -1;
				}
				if (streamingInfo.streamingScore > other.streamingInfo.streamingScore)
				{
					return 1;
				}
				return 0;
			}

			public bool UpdateCellScenarioData(string scenario0, string scenario1)
			{
				if (!data.scenarios.TryGetValue(scenario0, out this.scenario0))
				{
					return false;
				}
				hasTwoScenarios = false;
				if (!string.IsNullOrEmpty(scenario1) && data.scenarios.TryGetValue(scenario1, out this.scenario1))
				{
					hasTwoScenarios = true;
				}
				return true;
			}

			public void Clear()
			{
				desc = null;
				data = null;
				poolInfo.Clear();
				indexInfo.Clear();
				blendingInfo.Clear();
				streamingInfo.Clear();
				referenceCount = 0;
				loaded = false;
				scenario0 = default(global::UnityEngine.Rendering.ProbeReferenceVolume.CellData.PerScenarioData);
				scenario1 = default(global::UnityEngine.Rendering.ProbeReferenceVolume.CellData.PerScenarioData);
				hasTwoScenarios = false;
				debugProbes = null;
			}
		}

		internal struct Volume : global::System.IEquatable<global::UnityEngine.Rendering.ProbeReferenceVolume.Volume>
		{
			internal global::UnityEngine.Vector3 corner;

			internal global::UnityEngine.Vector3 X;

			internal global::UnityEngine.Vector3 Y;

			internal global::UnityEngine.Vector3 Z;

			internal float maxSubdivisionMultiplier;

			internal float minSubdivisionMultiplier;

			public Volume(global::UnityEngine.Matrix4x4 trs, float maxSubdivision, float minSubdivision)
			{
				X = trs.GetColumn(0);
				Y = trs.GetColumn(1);
				Z = trs.GetColumn(2);
				corner = (global::UnityEngine.Vector3)trs.GetColumn(3) - X * 0.5f - Y * 0.5f - Z * 0.5f;
				maxSubdivisionMultiplier = maxSubdivision;
				minSubdivisionMultiplier = minSubdivision;
			}

			public Volume(global::UnityEngine.Vector3 corner, global::UnityEngine.Vector3 X, global::UnityEngine.Vector3 Y, global::UnityEngine.Vector3 Z, float maxSubdivision = 1f, float minSubdivision = 0f)
			{
				this.corner = corner;
				this.X = X;
				this.Y = Y;
				this.Z = Z;
				maxSubdivisionMultiplier = maxSubdivision;
				minSubdivisionMultiplier = minSubdivision;
			}

			public Volume(global::UnityEngine.Rendering.ProbeReferenceVolume.Volume copy)
			{
				X = copy.X;
				Y = copy.Y;
				Z = copy.Z;
				corner = copy.corner;
				maxSubdivisionMultiplier = copy.maxSubdivisionMultiplier;
				minSubdivisionMultiplier = copy.minSubdivisionMultiplier;
			}

			public Volume(global::UnityEngine.Bounds bounds)
			{
				global::UnityEngine.Vector3 size = bounds.size;
				corner = bounds.center - size * 0.5f;
				X = new global::UnityEngine.Vector3(size.x, 0f, 0f);
				Y = new global::UnityEngine.Vector3(0f, size.y, 0f);
				Z = new global::UnityEngine.Vector3(0f, 0f, size.z);
				maxSubdivisionMultiplier = (minSubdivisionMultiplier = 0f);
			}

			public global::UnityEngine.Bounds CalculateAABB()
			{
				global::UnityEngine.Vector3 vector = new global::UnityEngine.Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
				global::UnityEngine.Vector3 vector2 = new global::UnityEngine.Vector3(float.MinValue, float.MinValue, float.MinValue);
				for (int i = 0; i < 2; i++)
				{
					for (int j = 0; j < 2; j++)
					{
						for (int k = 0; k < 2; k++)
						{
							global::UnityEngine.Vector3 vector3 = new global::UnityEngine.Vector3(i, j, k);
							global::UnityEngine.Vector3 rhs = corner + X * vector3.x + Y * vector3.y + Z * vector3.z;
							vector = global::UnityEngine.Vector3.Min(vector, rhs);
							vector2 = global::UnityEngine.Vector3.Max(vector2, rhs);
						}
					}
				}
				return new global::UnityEngine.Bounds((vector + vector2) / 2f, vector2 - vector);
			}

			public void CalculateCenterAndSize(out global::UnityEngine.Vector3 center, out global::UnityEngine.Vector3 size)
			{
				size = new global::UnityEngine.Vector3(X.magnitude, Y.magnitude, Z.magnitude);
				center = corner + X * 0.5f + Y * 0.5f + Z * 0.5f;
			}

			public void Transform(global::UnityEngine.Matrix4x4 trs)
			{
				corner = trs.MultiplyPoint(corner);
				X = trs.MultiplyVector(X);
				Y = trs.MultiplyVector(Y);
				Z = trs.MultiplyVector(Z);
			}

			public override string ToString()
			{
				return $"Corner: {corner}, X: {X}, Y: {Y}, Z: {Z}, MaxSubdiv: {maxSubdivisionMultiplier}";
			}

			public bool Equals(global::UnityEngine.Rendering.ProbeReferenceVolume.Volume other)
			{
				if (corner == other.corner && X == other.X && Y == other.Y && Z == other.Z && minSubdivisionMultiplier == other.minSubdivisionMultiplier)
				{
					return maxSubdivisionMultiplier == other.maxSubdivisionMultiplier;
				}
				return false;
			}
		}

		internal struct RefVolTransform
		{
			public global::UnityEngine.Vector3 posWS;

			public global::UnityEngine.Quaternion rot;

			public float scale;
		}

		public struct RuntimeResources
		{
			public global::UnityEngine.ComputeBuffer index;

			public global::UnityEngine.ComputeBuffer cellIndices;

			public global::UnityEngine.RenderTexture L0_L1rx;

			public global::UnityEngine.RenderTexture L1_G_ry;

			public global::UnityEngine.RenderTexture L1_B_rz;

			public global::UnityEngine.RenderTexture L2_0;

			public global::UnityEngine.RenderTexture L2_1;

			public global::UnityEngine.RenderTexture L2_2;

			public global::UnityEngine.RenderTexture L2_3;

			public global::UnityEngine.RenderTexture ProbeOcclusion;

			public global::UnityEngine.RenderTexture Validity;

			public global::UnityEngine.RenderTexture SkyOcclusionL0L1;

			public global::UnityEngine.RenderTexture SkyShadingDirectionIndices;

			public global::UnityEngine.ComputeBuffer SkyPrecomputedDirections;

			public global::UnityEngine.ComputeBuffer QualityLeakReductionData;
		}

		[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Size = 1)]
		public struct ExtraDataActionInput
		{
		}

		internal class CellInstancedDebugProbes
		{
			public global::System.Collections.Generic.List<global::UnityEngine.Matrix4x4[]> probeBuffers;

			public global::System.Collections.Generic.List<global::UnityEngine.Matrix4x4[]> offsetBuffers;

			public global::System.Collections.Generic.List<global::UnityEngine.MaterialPropertyBlock> props;
		}

		private class RenderFragmentationOverlayPassData
		{
			public global::UnityEngine.Material debugFragmentationMaterial;

			public global::UnityEngine.Rendering.DebugOverlay debugOverlay;

			public int chunkCount;

			public global::UnityEngine.ComputeBuffer debugFragmentationData;

			public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle colorBuffer;

			public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle depthBuffer;
		}

		internal class DiskStreamingRequest
		{
			private global::Unity.IO.LowLevel.Unsafe.ReadHandle m_ReadHandle;

			private global::Unity.IO.LowLevel.Unsafe.ReadCommandArray m_ReadCommandArray;

			private global::Unity.Collections.NativeArray<global::Unity.IO.LowLevel.Unsafe.ReadCommand> m_ReadCommandBuffer;

			private int m_BytesWritten;

			public DiskStreamingRequest(int maxRequestCount)
			{
				m_ReadCommandBuffer = new global::Unity.Collections.NativeArray<global::Unity.IO.LowLevel.Unsafe.ReadCommand>(maxRequestCount, global::Unity.Collections.Allocator.Persistent);
			}

			public unsafe void AddReadCommand(int offset, int size, byte* dest)
			{
				m_ReadCommandBuffer[m_ReadCommandArray.CommandCount++] = new global::Unity.IO.LowLevel.Unsafe.ReadCommand
				{
					Buffer = dest,
					Offset = offset,
					Size = size
				};
				m_BytesWritten += size;
			}

			public unsafe int RunCommands(global::Unity.IO.LowLevel.Unsafe.FileHandle file)
			{
				m_ReadCommandArray.ReadCommands = (global::Unity.IO.LowLevel.Unsafe.ReadCommand*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(m_ReadCommandBuffer);
				m_ReadHandle = global::Unity.IO.LowLevel.Unsafe.AsyncReadManager.Read(in file, m_ReadCommandArray);
				return m_BytesWritten;
			}

			public void Clear()
			{
				if (m_ReadHandle.IsValid())
				{
					m_ReadHandle.JobHandle.Complete();
				}
				m_ReadHandle = default(global::Unity.IO.LowLevel.Unsafe.ReadHandle);
				m_ReadCommandArray.CommandCount = 0;
				m_BytesWritten = 0;
			}

			public void Cancel()
			{
				if (m_ReadHandle.IsValid())
				{
					m_ReadHandle.Cancel();
				}
			}

			public void Wait()
			{
				if (m_ReadHandle.IsValid())
				{
					m_ReadHandle.JobHandle.Complete();
				}
			}

			public void Dispose()
			{
				m_ReadCommandBuffer.Dispose();
			}

			public global::Unity.IO.LowLevel.Unsafe.ReadStatus GetStatus()
			{
				if (!m_ReadHandle.IsValid())
				{
					return global::Unity.IO.LowLevel.Unsafe.ReadStatus.Complete;
				}
				return m_ReadHandle.Status;
			}
		}

		[global::UnityEngine.Rendering.GenerateHLSL(global::UnityEngine.Rendering.PackingRules.Exact, true, false, false, 1, false, false, false, -1, ".\\Library\\PackageCache\\com.unity.render-pipelines.core@04ab0eefa0c3\\Runtime\\Lighting\\ProbeVolume\\ProbeReferenceVolume.Streaming.cs", needAccessors = false, generateCBuffer = true)]
		internal struct CellStreamingScratchBufferLayout
		{
			public int _SharedDestChunksOffset;

			public int _L0L1rxOffset;

			public int _L1GryOffset;

			public int _L1BrzOffset;

			public int _ValidityOffset;

			public int _ProbeOcclusionOffset;

			public int _SkyOcclusionOffset;

			public int _SkyShadingDirectionOffset;

			public int _L2_0Offset;

			public int _L2_1Offset;

			public int _L2_2Offset;

			public int _L2_3Offset;

			public int _L0Size;

			public int _L0ProbeSize;

			public int _L1Size;

			public int _L1ProbeSize;

			public int _ValiditySize;

			public int _ValidityProbeSize;

			public int _ProbeOcclusionSize;

			public int _ProbeOcclusionProbeSize;

			public int _SkyOcclusionSize;

			public int _SkyOcclusionProbeSize;

			public int _SkyShadingDirectionSize;

			public int _SkyShadingDirectionProbeSize;

			public int _L2Size;

			public int _L2ProbeSize;

			public int _ProbeCountInChunkLine;

			public int _ProbeCountInChunkSlice;
		}

		internal class CellStreamingScratchBuffer
		{
			public global::Unity.Collections.NativeArray<byte> stagingBuffer;

			private int m_CurrentBuffer;

			private global::UnityEngine.GraphicsBuffer[] m_GraphicsBuffers = new global::UnityEngine.GraphicsBuffer[2];

			public global::UnityEngine.GraphicsBuffer buffer => m_GraphicsBuffers[m_CurrentBuffer];

			public int chunkCount { get; }

			public int chunkSize { get; }

			public CellStreamingScratchBuffer(int chunkCount, int chunkSize, bool allocateGraphicsBuffers)
			{
				this.chunkCount = chunkCount;
				this.chunkSize = chunkSize;
				int num = chunkCount * chunkSize / 4 + chunkCount * 4;
				num += 2 * chunkCount * 4;
				if (allocateGraphicsBuffers)
				{
					for (int i = 0; i < 2; i++)
					{
						m_GraphicsBuffers[i] = new global::UnityEngine.GraphicsBuffer(global::UnityEngine.GraphicsBuffer.Target.Raw, global::UnityEngine.GraphicsBuffer.UsageFlags.LockBufferForWrite, num, 4);
					}
				}
				m_CurrentBuffer = 0;
				stagingBuffer = new global::Unity.Collections.NativeArray<byte>(num * 4, global::Unity.Collections.Allocator.Persistent);
			}

			public void Swap()
			{
				m_CurrentBuffer = (m_CurrentBuffer + 1) % 2;
			}

			public void Dispose()
			{
				for (int i = 0; i < 2; i++)
				{
					m_GraphicsBuffers[i]?.Dispose();
				}
				stagingBuffer.Dispose();
			}
		}

		[global::System.Diagnostics.DebuggerDisplay("Index = {cell.desc.index} State = {state}")]
		internal class CellStreamingRequest
		{
			public enum State
			{
				Pending = 0,
				Active = 1,
				Canceled = 2,
				Invalid = 3,
				Complete = 4
			}

			public delegate void OnStreamingCompleteDelegate(global::UnityEngine.Rendering.ProbeReferenceVolume.CellStreamingRequest request, global::UnityEngine.Rendering.CommandBuffer cmd);

			public global::UnityEngine.Rendering.ProbeReferenceVolume.CellStreamingRequest.OnStreamingCompleteDelegate onStreamingComplete;

			public global::UnityEngine.Rendering.ProbeReferenceVolume.DiskStreamingRequest cellDataStreamingRequest = new global::UnityEngine.Rendering.ProbeReferenceVolume.DiskStreamingRequest(1);

			public global::UnityEngine.Rendering.ProbeReferenceVolume.DiskStreamingRequest cellOptionalDataStreamingRequest = new global::UnityEngine.Rendering.ProbeReferenceVolume.DiskStreamingRequest(1);

			public global::UnityEngine.Rendering.ProbeReferenceVolume.DiskStreamingRequest cellSharedDataStreamingRequest = new global::UnityEngine.Rendering.ProbeReferenceVolume.DiskStreamingRequest(1);

			public global::UnityEngine.Rendering.ProbeReferenceVolume.DiskStreamingRequest cellProbeOcclusionDataStreamingRequest = new global::UnityEngine.Rendering.ProbeReferenceVolume.DiskStreamingRequest(1);

			public global::UnityEngine.Rendering.ProbeReferenceVolume.DiskStreamingRequest brickStreamingRequest = new global::UnityEngine.Rendering.ProbeReferenceVolume.DiskStreamingRequest(1);

			public global::UnityEngine.Rendering.ProbeReferenceVolume.DiskStreamingRequest supportStreamingRequest = new global::UnityEngine.Rendering.ProbeReferenceVolume.DiskStreamingRequest(5);

			public int bytesWritten;

			public global::UnityEngine.Rendering.ProbeReferenceVolume.Cell cell { get; set; }

			public global::UnityEngine.Rendering.ProbeReferenceVolume.CellStreamingRequest.State state { get; set; }

			public global::UnityEngine.Rendering.ProbeReferenceVolume.CellStreamingScratchBuffer scratchBuffer { get; set; }

			public global::UnityEngine.Rendering.ProbeReferenceVolume.CellStreamingScratchBufferLayout scratchBufferLayout { get; set; }

			public global::UnityEngine.Rendering.ProbeVolumeBakingSet.PerScenarioDataInfo scenarioData { get; set; }

			public int poolIndex { get; set; }

			public bool streamSharedData { get; set; }

			public bool IsStreaming()
			{
				if (state != global::UnityEngine.Rendering.ProbeReferenceVolume.CellStreamingRequest.State.Pending)
				{
					return state == global::UnityEngine.Rendering.ProbeReferenceVolume.CellStreamingRequest.State.Active;
				}
				return true;
			}

			public void Cancel()
			{
				if (state == global::UnityEngine.Rendering.ProbeReferenceVolume.CellStreamingRequest.State.Active)
				{
					brickStreamingRequest.Cancel();
					supportStreamingRequest.Cancel();
					cellDataStreamingRequest.Cancel();
					cellOptionalDataStreamingRequest.Cancel();
					cellSharedDataStreamingRequest.Cancel();
					cellProbeOcclusionDataStreamingRequest.Cancel();
				}
				state = global::UnityEngine.Rendering.ProbeReferenceVolume.CellStreamingRequest.State.Canceled;
			}

			public void WaitAll()
			{
				if (state == global::UnityEngine.Rendering.ProbeReferenceVolume.CellStreamingRequest.State.Active)
				{
					brickStreamingRequest.Wait();
					supportStreamingRequest.Wait();
					cellDataStreamingRequest.Wait();
					cellOptionalDataStreamingRequest.Wait();
					cellSharedDataStreamingRequest.Wait();
					cellProbeOcclusionDataStreamingRequest.Wait();
				}
			}

			public bool UpdateRequestState(global::UnityEngine.Rendering.ProbeReferenceVolume.DiskStreamingRequest request, ref bool isComplete)
			{
				global::Unity.IO.LowLevel.Unsafe.ReadStatus status = request.GetStatus();
				if (status == global::Unity.IO.LowLevel.Unsafe.ReadStatus.Failed)
				{
					return false;
				}
				isComplete &= status == global::Unity.IO.LowLevel.Unsafe.ReadStatus.Complete;
				return true;
			}

			public void UpdateState()
			{
				if (state == global::UnityEngine.Rendering.ProbeReferenceVolume.CellStreamingRequest.State.Active)
				{
					bool isComplete = true;
					if (!(UpdateRequestState(brickStreamingRequest, ref isComplete) & UpdateRequestState(supportStreamingRequest, ref isComplete) & UpdateRequestState(cellDataStreamingRequest, ref isComplete) & UpdateRequestState(cellOptionalDataStreamingRequest, ref isComplete) & UpdateRequestState(cellSharedDataStreamingRequest, ref isComplete) & UpdateRequestState(cellProbeOcclusionDataStreamingRequest, ref isComplete)))
					{
						Cancel();
						state = global::UnityEngine.Rendering.ProbeReferenceVolume.CellStreamingRequest.State.Invalid;
					}
					else if (isComplete)
					{
						state = global::UnityEngine.Rendering.ProbeReferenceVolume.CellStreamingRequest.State.Complete;
					}
				}
			}

			public void Clear()
			{
				cell = null;
				Reset();
			}

			public void Reset()
			{
				state = global::UnityEngine.Rendering.ProbeReferenceVolume.CellStreamingRequest.State.Pending;
				scratchBuffer = null;
				brickStreamingRequest.Clear();
				supportStreamingRequest.Clear();
				cellDataStreamingRequest.Clear();
				cellOptionalDataStreamingRequest.Clear();
				cellSharedDataStreamingRequest.Clear();
				cellProbeOcclusionDataStreamingRequest.Clear();
				bytesWritten = 0;
			}

			public void Dispose()
			{
				brickStreamingRequest.Dispose();
				supportStreamingRequest.Dispose();
				cellDataStreamingRequest.Dispose();
				cellOptionalDataStreamingRequest.Dispose();
				cellSharedDataStreamingRequest.Dispose();
				cellProbeOcclusionDataStreamingRequest.Dispose();
			}
		}

		private global::UnityEngine.ComputeBuffer m_EmptyIndexBuffer;

		private bool m_IsInitialized;

		private bool m_SupportScenarios;

		private bool m_SupportScenarioBlending;

		private bool m_ForceNoDiskStreaming;

		private bool m_SupportDiskStreaming;

		private bool m_SupportGPUStreaming;

		private bool m_UseStreamingAssets = true;

		private float m_MinBrickSize;

		private int m_MaxSubdivision;

		private global::UnityEngine.Vector3 m_ProbeOffset;

		private global::UnityEngine.Rendering.ProbeBrickPool m_Pool;

		private global::UnityEngine.Rendering.ProbeBrickIndex m_Index;

		private global::UnityEngine.Rendering.ProbeGlobalIndirection m_CellIndices;

		private global::UnityEngine.Rendering.ProbeBrickBlendingPool m_BlendingPool;

		private global::System.Collections.Generic.List<global::UnityEngine.Rendering.ProbeBrickPool.BrickChunkAlloc> m_TmpSrcChunks = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.ProbeBrickPool.BrickChunkAlloc>();

		private float[] m_PositionOffsets = new float[4];

		private global::UnityEngine.Bounds m_CurrGlobalBounds;

		internal global::System.Collections.Generic.Dictionary<int, global::UnityEngine.Rendering.ProbeReferenceVolume.Cell> cells = new global::System.Collections.Generic.Dictionary<int, global::UnityEngine.Rendering.ProbeReferenceVolume.Cell>();

		private global::UnityEngine.Rendering.ObjectPool<global::UnityEngine.Rendering.ProbeReferenceVolume.Cell> m_CellPool = new global::UnityEngine.Rendering.ObjectPool<global::UnityEngine.Rendering.ProbeReferenceVolume.Cell>(delegate(global::UnityEngine.Rendering.ProbeReferenceVolume.Cell x)
		{
			x.Clear();
		}, null, collectionCheck: false);

		private global::UnityEngine.Rendering.ProbeBrickPool.DataLocation m_TemporaryDataLocation;

		private int m_TemporaryDataLocationMemCost;

		[global::System.Obsolete("This field is only kept for migration purpose. #from(2023.3)")]
		internal global::UnityEngine.Rendering.ProbeVolumeSceneData sceneData;

		private global::UnityEngine.Vector3Int minLoadedCellPos = new global::UnityEngine.Vector3Int(int.MaxValue, int.MaxValue, int.MaxValue);

		private global::UnityEngine.Vector3Int maxLoadedCellPos = new global::UnityEngine.Vector3Int(int.MinValue, int.MinValue, int.MinValue);

		public global::System.Action<global::UnityEngine.Rendering.ProbeReferenceVolume.ExtraDataActionInput> retrieveExtraDataAction;

		public global::System.Action checksDuringBakeAction;

		private global::System.Collections.Generic.Dictionary<string, (global::UnityEngine.Rendering.ProbeVolumeBakingSet, global::System.Collections.Generic.List<int>)> m_PendingScenesToBeLoaded = new global::System.Collections.Generic.Dictionary<string, (global::UnityEngine.Rendering.ProbeVolumeBakingSet, global::System.Collections.Generic.List<int>)>();

		private global::System.Collections.Generic.Dictionary<string, global::System.Collections.Generic.List<int>> m_PendingScenesToBeUnloaded = new global::System.Collections.Generic.Dictionary<string, global::System.Collections.Generic.List<int>>();

		private global::System.Collections.Generic.List<string> m_ActiveScenes = new global::System.Collections.Generic.List<string>();

		private global::UnityEngine.Rendering.ProbeVolumeBakingSetWeakReference m_CurrentBakingSetReference = new global::UnityEngine.Rendering.ProbeVolumeBakingSetWeakReference();

		private global::UnityEngine.Rendering.ProbeVolumeBakingSetWeakReference m_LazyBakingSetReference = new global::UnityEngine.Rendering.ProbeVolumeBakingSetWeakReference();

		private bool m_NeedLoadAsset;

		private bool m_ProbeReferenceVolumeInit;

		private bool m_EnabledBySRP;

		private bool m_VertexSampling;

		private bool m_NeedsIndexRebuild;

		private bool m_HasChangedIndex;

		private int m_CBShaderID = global::UnityEngine.Shader.PropertyToID("ShaderVariablesProbeVolumes");

		private global::UnityEngine.Rendering.ProbeVolumeTextureMemoryBudget m_MemoryBudget;

		private global::UnityEngine.Rendering.ProbeVolumeBlendingTextureMemoryBudget m_BlendingMemoryBudget;

		private global::UnityEngine.Rendering.ProbeVolumeSHBands m_SHBands;

		internal bool clearAssetsOnVolumeClear;

		internal static string defaultLightingScenario = "Default";

		private static global::UnityEngine.Rendering.ProbeReferenceVolume _instance = new global::UnityEngine.Rendering.ProbeReferenceVolume();

		private const int kProbesPerBatch = 511;

		public static readonly string k_DebugPanelName = "Probe Volumes";

		private global::UnityEngine.Mesh m_DebugMesh;

		private global::UnityEngine.Rendering.DebugUI.Widget[] m_DebugItems;

		private global::UnityEngine.Material m_DebugMaterial;

		private global::UnityEngine.Mesh m_DebugProbeSamplingMesh;

		private global::UnityEngine.Material m_ProbeSamplingDebugMaterial;

		private global::UnityEngine.Material m_ProbeSamplingDebugMaterial02;

		private global::UnityEngine.Texture m_DisplayNumbersTexture;

		internal static global::UnityEngine.Rendering.ProbeSamplingDebugData probeSamplingDebugData = new global::UnityEngine.Rendering.ProbeSamplingDebugData();

		private global::UnityEngine.Mesh m_DebugOffsetMesh;

		private global::UnityEngine.Material m_DebugOffsetMaterial;

		private global::UnityEngine.Material m_DebugFragmentationMaterial;

		private global::UnityEngine.Plane[] m_DebugFrustumPlanes = new global::UnityEngine.Plane[6];

		private global::UnityEngine.GUIContent[] m_DebugScenarioNames = new global::UnityEngine.GUIContent[0];

		private int[] m_DebugScenarioValues = new int[0];

		private string m_DebugActiveSceneGUID;

		private string m_DebugActiveScenario;

		private global::UnityEngine.Rendering.DebugUI.EnumField m_DebugScenarioField;

		internal global::System.Collections.Generic.Dictionary<global::UnityEngine.Bounds, global::UnityEngine.Rendering.ProbeBrickIndex.Brick[]> realtimeSubdivisionInfo = new global::System.Collections.Generic.Dictionary<global::UnityEngine.Bounds, global::UnityEngine.Rendering.ProbeBrickIndex.Brick[]>();

		private bool m_MaxSubdivVisualizedIsMaxAvailable;

		private static global::UnityEngine.Vector4[] s_BoundsArray = new global::UnityEngine.Vector4[48];

		private bool m_LoadMaxCellsPerFrame;

		private const int kMaxCellLoadedPerFrame = 10;

		private int m_NumberOfCellsLoadedPerFrame = 1;

		private int m_NumberOfCellsBlendedPerFrame = 10000;

		private float m_TurnoverRate = 0.1f;

		private global::UnityEngine.Rendering.DynamicArray<global::UnityEngine.Rendering.ProbeReferenceVolume.Cell> m_LoadedCells = new global::UnityEngine.Rendering.DynamicArray<global::UnityEngine.Rendering.ProbeReferenceVolume.Cell>();

		private global::UnityEngine.Rendering.DynamicArray<global::UnityEngine.Rendering.ProbeReferenceVolume.Cell> m_ToBeLoadedCells = new global::UnityEngine.Rendering.DynamicArray<global::UnityEngine.Rendering.ProbeReferenceVolume.Cell>();

		private global::UnityEngine.Rendering.DynamicArray<global::UnityEngine.Rendering.ProbeReferenceVolume.Cell> m_WorseLoadedCells = new global::UnityEngine.Rendering.DynamicArray<global::UnityEngine.Rendering.ProbeReferenceVolume.Cell>();

		private global::UnityEngine.Rendering.DynamicArray<global::UnityEngine.Rendering.ProbeReferenceVolume.Cell> m_BestToBeLoadedCells = new global::UnityEngine.Rendering.DynamicArray<global::UnityEngine.Rendering.ProbeReferenceVolume.Cell>();

		private global::UnityEngine.Rendering.DynamicArray<global::UnityEngine.Rendering.ProbeReferenceVolume.Cell> m_TempCellToLoadList = new global::UnityEngine.Rendering.DynamicArray<global::UnityEngine.Rendering.ProbeReferenceVolume.Cell>();

		private global::UnityEngine.Rendering.DynamicArray<global::UnityEngine.Rendering.ProbeReferenceVolume.Cell> m_TempCellToUnloadList = new global::UnityEngine.Rendering.DynamicArray<global::UnityEngine.Rendering.ProbeReferenceVolume.Cell>();

		private global::UnityEngine.Rendering.DynamicArray<global::UnityEngine.Rendering.ProbeReferenceVolume.Cell> m_LoadedBlendingCells = new global::UnityEngine.Rendering.DynamicArray<global::UnityEngine.Rendering.ProbeReferenceVolume.Cell>();

		private global::UnityEngine.Rendering.DynamicArray<global::UnityEngine.Rendering.ProbeReferenceVolume.Cell> m_ToBeLoadedBlendingCells = new global::UnityEngine.Rendering.DynamicArray<global::UnityEngine.Rendering.ProbeReferenceVolume.Cell>();

		private global::UnityEngine.Rendering.DynamicArray<global::UnityEngine.Rendering.ProbeReferenceVolume.Cell> m_TempBlendingCellToLoadList = new global::UnityEngine.Rendering.DynamicArray<global::UnityEngine.Rendering.ProbeReferenceVolume.Cell>();

		private global::UnityEngine.Rendering.DynamicArray<global::UnityEngine.Rendering.ProbeReferenceVolume.Cell> m_TempBlendingCellToUnloadList = new global::UnityEngine.Rendering.DynamicArray<global::UnityEngine.Rendering.ProbeReferenceVolume.Cell>();

		private global::UnityEngine.Vector3 m_FrozenCameraPosition;

		private global::UnityEngine.Vector3 m_FrozenCameraDirection;

		private const float kIndexFragmentationThreshold = 0.2f;

		private bool m_IndexDefragmentationInProgress;

		private global::UnityEngine.Rendering.ProbeBrickIndex m_DefragIndex;

		private global::UnityEngine.Rendering.ProbeGlobalIndirection m_DefragCellIndices;

		private global::UnityEngine.Rendering.DynamicArray<global::UnityEngine.Rendering.ProbeReferenceVolume.Cell> m_IndexDefragCells = new global::UnityEngine.Rendering.DynamicArray<global::UnityEngine.Rendering.ProbeReferenceVolume.Cell>();

		private global::UnityEngine.Rendering.DynamicArray<global::UnityEngine.Rendering.ProbeReferenceVolume.Cell> m_TempIndexDefragCells = new global::UnityEngine.Rendering.DynamicArray<global::UnityEngine.Rendering.ProbeReferenceVolume.Cell>();

		internal float minStreamingScore;

		internal float maxStreamingScore;

		private global::System.Collections.Generic.Queue<global::UnityEngine.Rendering.ProbeReferenceVolume.CellStreamingRequest> m_StreamingQueue = new global::System.Collections.Generic.Queue<global::UnityEngine.Rendering.ProbeReferenceVolume.CellStreamingRequest>();

		private global::System.Collections.Generic.List<global::UnityEngine.Rendering.ProbeReferenceVolume.CellStreamingRequest> m_ActiveStreamingRequests = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.ProbeReferenceVolume.CellStreamingRequest>();

		private global::UnityEngine.Rendering.ObjectPool<global::UnityEngine.Rendering.ProbeReferenceVolume.CellStreamingRequest> m_StreamingRequestsPool = new global::UnityEngine.Rendering.ObjectPool<global::UnityEngine.Rendering.ProbeReferenceVolume.CellStreamingRequest>(null, delegate(global::UnityEngine.Rendering.ProbeReferenceVolume.CellStreamingRequest val)
		{
			val.Clear();
		});

		private bool m_DiskStreamingUseCompute;

		private global::UnityEngine.Rendering.ProbeVolumeScratchBufferPool m_ScratchBufferPool;

		private global::UnityEngine.Rendering.ProbeReferenceVolume.CellStreamingRequest.OnStreamingCompleteDelegate m_OnStreamingComplete;

		private global::UnityEngine.Rendering.ProbeReferenceVolume.CellStreamingRequest.OnStreamingCompleteDelegate m_OnBlendingStreamingComplete;

		private static global::UnityEngine.Rendering.DynamicArray<global::UnityEngine.Rendering.ProbeReferenceVolume.Cell>.SortComparer s_BlendingComparer = BlendingComparer;

		private static global::UnityEngine.Rendering.DynamicArray<global::UnityEngine.Rendering.ProbeReferenceVolume.Cell>.SortComparer s_DefragComparer = DefragComparer;

		internal global::UnityEngine.Bounds globalBounds
		{
			get
			{
				return m_CurrGlobalBounds;
			}
			set
			{
				m_CurrGlobalBounds = value;
			}
		}

		private global::UnityEngine.Rendering.ProbeVolumeBakingSet m_CurrentBakingSet
		{
			get
			{
				return m_CurrentBakingSetReference.Get();
			}
			set
			{
				m_CurrentBakingSetReference.Set(value);
			}
		}

		private global::UnityEngine.Rendering.ProbeVolumeBakingSet m_LazyBakingSet
		{
			get
			{
				return m_LazyBakingSetReference.Get();
			}
			set
			{
				m_LazyBakingSetReference.Set(value);
			}
		}

		public bool isInitialized => m_IsInitialized;

		internal bool enabledBySRP => m_EnabledBySRP;

		internal bool vertexSampling => m_VertexSampling;

		internal bool hasUnloadedCells => m_ToBeLoadedCells.size != 0;

		internal bool supportLightingScenarios => m_SupportScenarios;

		internal bool supportScenarioBlending => m_SupportScenarioBlending;

		internal bool gpuStreamingEnabled => m_SupportGPUStreaming;

		internal bool diskStreamingEnabled
		{
			get
			{
				if (m_SupportDiskStreaming)
				{
					return !m_ForceNoDiskStreaming;
				}
				return false;
			}
		}

		public bool probeOcclusion
		{
			get
			{
				if (!m_CurrentBakingSet)
				{
					return false;
				}
				return m_CurrentBakingSet.bakedProbeOcclusion;
			}
		}

		public bool skyOcclusion
		{
			get
			{
				if (!m_CurrentBakingSet)
				{
					return false;
				}
				return m_CurrentBakingSet.bakedSkyOcclusion;
			}
		}

		public bool skyOcclusionShadingDirection
		{
			get
			{
				if (!m_CurrentBakingSet)
				{
					return false;
				}
				return m_CurrentBakingSet.bakedSkyShadingDirection;
			}
		}

		private bool useRenderingLayers => m_CurrentBakingSet.bakedMaskCount != 1;

		public global::UnityEngine.Rendering.ProbeVolumeSHBands shBands => m_SHBands;

		public global::UnityEngine.Rendering.ProbeVolumeBakingSet currentBakingSet => m_CurrentBakingSet;

		public string lightingScenario
		{
			get
			{
				if (!m_CurrentBakingSet)
				{
					return null;
				}
				return m_CurrentBakingSet.lightingScenario;
			}
			set
			{
				SetActiveScenario(value);
			}
		}

		public string otherScenario
		{
			get
			{
				if (!m_CurrentBakingSet)
				{
					return null;
				}
				return m_CurrentBakingSet.otherScenario;
			}
		}

		public float scenarioBlendingFactor
		{
			get
			{
				if (!m_CurrentBakingSet)
				{
					return 0f;
				}
				return m_CurrentBakingSet.scenarioBlendingFactor;
			}
			set
			{
				if (m_CurrentBakingSet != null)
				{
					m_CurrentBakingSet.BlendLightingScenario(m_CurrentBakingSet.otherScenario, value);
				}
			}
		}

		public global::UnityEngine.Rendering.ProbeVolumeTextureMemoryBudget memoryBudget => m_MemoryBudget;

		internal global::System.Collections.Generic.List<global::UnityEngine.Rendering.ProbeVolumePerSceneData> perSceneDataList { get; private set; } = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.ProbeVolumePerSceneData>();

		internal float indexFragmentationRate
		{
			get
			{
				if (!m_ProbeReferenceVolumeInit)
				{
					return 0f;
				}
				return m_Index.fragmentationRate;
			}
		}

		public static global::UnityEngine.Rendering.ProbeReferenceVolume instance => _instance;

		internal global::UnityEngine.Rendering.ProbeVolumeDebug probeVolumeDebug { get; } = new global::UnityEngine.Rendering.ProbeVolumeDebug();

		public global::UnityEngine.Color[] subdivisionDebugColors { get; } = new global::UnityEngine.Color[7];

		private global::UnityEngine.Mesh debugMesh
		{
			get
			{
				if (m_DebugMesh == null)
				{
					m_DebugMesh = global::UnityEngine.Rendering.DebugShapes.instance.BuildCustomSphereMesh(0.5f, 9u, 8u);
					m_DebugMesh.bounds = new global::UnityEngine.Bounds(global::UnityEngine.Vector3.zero, global::UnityEngine.Vector3.one * 10000000f);
				}
				return m_DebugMesh;
			}
		}

		public bool loadMaxCellsPerFrame
		{
			get
			{
				return m_LoadMaxCellsPerFrame;
			}
			set
			{
				m_LoadMaxCellsPerFrame = value;
			}
		}

		private int numberOfCellsLoadedPerFrame
		{
			get
			{
				if (!m_LoadMaxCellsPerFrame)
				{
					return m_NumberOfCellsLoadedPerFrame;
				}
				return cells.Count;
			}
		}

		public int numberOfCellsBlendedPerFrame
		{
			get
			{
				return m_NumberOfCellsBlendedPerFrame;
			}
			set
			{
				m_NumberOfCellsBlendedPerFrame = global::UnityEngine.Mathf.Max(1, value);
			}
		}

		public float turnoverRate
		{
			get
			{
				return m_TurnoverRate;
			}
			set
			{
				m_TurnoverRate = global::UnityEngine.Mathf.Clamp01(value);
			}
		}

		public void BindAPVRuntimeResources(global::UnityEngine.Rendering.CommandBuffer cmdBuffer, bool isProbeVolumeEnabled)
		{
			bool flag = true;
			global::UnityEngine.Rendering.ProbeReferenceVolume probeReferenceVolume = instance;
			if (isProbeVolumeEnabled && m_ProbeReferenceVolumeInit)
			{
				global::UnityEngine.Rendering.ProbeReferenceVolume.RuntimeResources runtimeResources = probeReferenceVolume.GetRuntimeResources();
				if ((runtimeResources.index != null && runtimeResources.L0_L1rx != null && runtimeResources.L1_G_ry != null && runtimeResources.L1_B_rz != null) & ((probeReferenceVolume.shBands == global::UnityEngine.Rendering.ProbeVolumeSHBands.SphericalHarmonicsL2 && runtimeResources.L2_0 != null) || probeReferenceVolume.shBands == global::UnityEngine.Rendering.ProbeVolumeSHBands.SphericalHarmonicsL1))
				{
					cmdBuffer.SetGlobalBuffer(global::UnityEngine.Rendering.ProbeReferenceVolume.ShaderIDs._APVResIndex, runtimeResources.index);
					cmdBuffer.SetGlobalBuffer(global::UnityEngine.Rendering.ProbeReferenceVolume.ShaderIDs._APVResCellIndices, runtimeResources.cellIndices);
					cmdBuffer.SetGlobalTexture(global::UnityEngine.Rendering.ProbeReferenceVolume.ShaderIDs._APVResL0_L1Rx, runtimeResources.L0_L1rx);
					cmdBuffer.SetGlobalTexture(global::UnityEngine.Rendering.ProbeReferenceVolume.ShaderIDs._APVResL1G_L1Ry, runtimeResources.L1_G_ry);
					cmdBuffer.SetGlobalTexture(global::UnityEngine.Rendering.ProbeReferenceVolume.ShaderIDs._APVResL1B_L1Rz, runtimeResources.L1_B_rz);
					cmdBuffer.SetGlobalTexture(global::UnityEngine.Rendering.ProbeReferenceVolume.ShaderIDs._APVResValidity, runtimeResources.Validity);
					int skyOcclusionTexL0L = global::UnityEngine.Rendering.ProbeReferenceVolume.ShaderIDs._SkyOcclusionTexL0L1;
					global::UnityEngine.RenderTexture skyOcclusionL0L = runtimeResources.SkyOcclusionL0L1;
					cmdBuffer.SetGlobalTexture(skyOcclusionTexL0L, ((object)skyOcclusionL0L != null) ? ((global::UnityEngine.Rendering.RenderTargetIdentifier)skyOcclusionL0L) : ((global::UnityEngine.Rendering.RenderTargetIdentifier)global::UnityEngine.Rendering.CoreUtils.blackVolumeTexture));
					int skyShadingDirectionIndicesTex = global::UnityEngine.Rendering.ProbeReferenceVolume.ShaderIDs._SkyShadingDirectionIndicesTex;
					skyOcclusionL0L = runtimeResources.SkyShadingDirectionIndices;
					cmdBuffer.SetGlobalTexture(skyShadingDirectionIndicesTex, ((object)skyOcclusionL0L != null) ? ((global::UnityEngine.Rendering.RenderTargetIdentifier)skyOcclusionL0L) : ((global::UnityEngine.Rendering.RenderTargetIdentifier)global::UnityEngine.Rendering.CoreUtils.blackVolumeTexture));
					cmdBuffer.SetGlobalBuffer(global::UnityEngine.Rendering.ProbeReferenceVolume.ShaderIDs._SkyPrecomputedDirections, runtimeResources.SkyPrecomputedDirections);
					cmdBuffer.SetGlobalBuffer(global::UnityEngine.Rendering.ProbeReferenceVolume.ShaderIDs._AntiLeakData, runtimeResources.QualityLeakReductionData);
					if (probeReferenceVolume.shBands == global::UnityEngine.Rendering.ProbeVolumeSHBands.SphericalHarmonicsL2)
					{
						cmdBuffer.SetGlobalTexture(global::UnityEngine.Rendering.ProbeReferenceVolume.ShaderIDs._APVResL2_0, runtimeResources.L2_0);
						cmdBuffer.SetGlobalTexture(global::UnityEngine.Rendering.ProbeReferenceVolume.ShaderIDs._APVResL2_1, runtimeResources.L2_1);
						cmdBuffer.SetGlobalTexture(global::UnityEngine.Rendering.ProbeReferenceVolume.ShaderIDs._APVResL2_2, runtimeResources.L2_2);
						cmdBuffer.SetGlobalTexture(global::UnityEngine.Rendering.ProbeReferenceVolume.ShaderIDs._APVResL2_3, runtimeResources.L2_3);
					}
					int aPVProbeOcclusion = global::UnityEngine.Rendering.ProbeReferenceVolume.ShaderIDs._APVProbeOcclusion;
					skyOcclusionL0L = runtimeResources.ProbeOcclusion;
					cmdBuffer.SetGlobalTexture(aPVProbeOcclusion, ((object)skyOcclusionL0L != null) ? ((global::UnityEngine.Rendering.RenderTargetIdentifier)skyOcclusionL0L) : ((global::UnityEngine.Rendering.RenderTargetIdentifier)global::UnityEngine.Rendering.CoreUtils.whiteVolumeTexture));
					flag = false;
				}
			}
			if (flag)
			{
				if (m_EmptyIndexBuffer == null)
				{
					m_EmptyIndexBuffer = new global::UnityEngine.ComputeBuffer(1, 12, global::UnityEngine.ComputeBufferType.Structured);
				}
				cmdBuffer.SetGlobalBuffer(global::UnityEngine.Rendering.ProbeReferenceVolume.ShaderIDs._APVResIndex, m_EmptyIndexBuffer);
				cmdBuffer.SetGlobalBuffer(global::UnityEngine.Rendering.ProbeReferenceVolume.ShaderIDs._APVResCellIndices, m_EmptyIndexBuffer);
				cmdBuffer.SetGlobalTexture(global::UnityEngine.Rendering.ProbeReferenceVolume.ShaderIDs._APVResL0_L1Rx, global::UnityEngine.Rendering.CoreUtils.blackVolumeTexture);
				cmdBuffer.SetGlobalTexture(global::UnityEngine.Rendering.ProbeReferenceVolume.ShaderIDs._APVResL1G_L1Ry, global::UnityEngine.Rendering.CoreUtils.blackVolumeTexture);
				cmdBuffer.SetGlobalTexture(global::UnityEngine.Rendering.ProbeReferenceVolume.ShaderIDs._APVResL1B_L1Rz, global::UnityEngine.Rendering.CoreUtils.blackVolumeTexture);
				cmdBuffer.SetGlobalTexture(global::UnityEngine.Rendering.ProbeReferenceVolume.ShaderIDs._APVResValidity, global::UnityEngine.Rendering.CoreUtils.blackVolumeTexture);
				cmdBuffer.SetGlobalTexture(global::UnityEngine.Rendering.ProbeReferenceVolume.ShaderIDs._SkyOcclusionTexL0L1, global::UnityEngine.Rendering.CoreUtils.blackVolumeTexture);
				cmdBuffer.SetGlobalTexture(global::UnityEngine.Rendering.ProbeReferenceVolume.ShaderIDs._SkyShadingDirectionIndicesTex, global::UnityEngine.Rendering.CoreUtils.blackVolumeTexture);
				cmdBuffer.SetGlobalBuffer(global::UnityEngine.Rendering.ProbeReferenceVolume.ShaderIDs._SkyPrecomputedDirections, m_EmptyIndexBuffer);
				cmdBuffer.SetGlobalBuffer(global::UnityEngine.Rendering.ProbeReferenceVolume.ShaderIDs._AntiLeakData, m_EmptyIndexBuffer);
				if (probeReferenceVolume.shBands == global::UnityEngine.Rendering.ProbeVolumeSHBands.SphericalHarmonicsL2)
				{
					cmdBuffer.SetGlobalTexture(global::UnityEngine.Rendering.ProbeReferenceVolume.ShaderIDs._APVResL2_0, global::UnityEngine.Rendering.CoreUtils.blackVolumeTexture);
					cmdBuffer.SetGlobalTexture(global::UnityEngine.Rendering.ProbeReferenceVolume.ShaderIDs._APVResL2_1, global::UnityEngine.Rendering.CoreUtils.blackVolumeTexture);
					cmdBuffer.SetGlobalTexture(global::UnityEngine.Rendering.ProbeReferenceVolume.ShaderIDs._APVResL2_2, global::UnityEngine.Rendering.CoreUtils.blackVolumeTexture);
					cmdBuffer.SetGlobalTexture(global::UnityEngine.Rendering.ProbeReferenceVolume.ShaderIDs._APVResL2_3, global::UnityEngine.Rendering.CoreUtils.blackVolumeTexture);
				}
				cmdBuffer.SetGlobalTexture(global::UnityEngine.Rendering.ProbeReferenceVolume.ShaderIDs._APVProbeOcclusion, global::UnityEngine.Rendering.CoreUtils.whiteVolumeTexture);
			}
		}

		public bool UpdateShaderVariablesProbeVolumes(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Rendering.ProbeVolumesOptions probeVolumeOptions, int taaFrameIndex, bool supportRenderingLayers = false)
		{
			bool num = DataHasBeenLoaded();
			if (num)
			{
				global::UnityEngine.Rendering.ProbeVolumeShadingParameters parameters = default(global::UnityEngine.Rendering.ProbeVolumeShadingParameters);
				parameters.normalBias = probeVolumeOptions.normalBias.value;
				parameters.viewBias = probeVolumeOptions.viewBias.value;
				parameters.scaleBiasByMinDistanceBetweenProbes = probeVolumeOptions.scaleBiasWithMinProbeDistance.value;
				parameters.samplingNoise = probeVolumeOptions.samplingNoise.value;
				parameters.weight = probeVolumeOptions.intensityMultiplier.value;
				parameters.leakReductionMode = probeVolumeOptions.leakReductionMode.value;
				parameters.frameIndexForNoise = taaFrameIndex * (probeVolumeOptions.animateSamplingNoise.value ? 1 : 0);
				parameters.reflNormalizationLowerClamp = 0.005f;
				parameters.reflNormalizationUpperClamp = (probeVolumeOptions.occlusionOnlyReflectionNormalization.value ? 1f : 7f);
				parameters.skyOcclusionIntensity = (skyOcclusion ? probeVolumeOptions.skyOcclusionIntensityMultiplier.value : 0f);
				parameters.skyOcclusionShadingDirection = skyOcclusion && skyOcclusionShadingDirection;
				parameters.regionCount = ((m_CurrentBakingSet != null) ? m_CurrentBakingSet.bakedMaskCount : 0);
				parameters.regionLayerMasks = ((supportRenderingLayers && m_CurrentBakingSet != null) ? m_CurrentBakingSet.bakedLayerMasks : ((global::Unity.Mathematics.uint4)uint.MaxValue));
				parameters.worldOffset = probeVolumeOptions.worldOffset.value;
				UpdateConstantBuffer(cmd, parameters);
			}
			return num;
		}

		internal static string GetSceneGUID(global::UnityEngine.SceneManagement.Scene scene)
		{
			return scene.GetGUID();
		}

		internal void SetActiveScenario(string scenario, bool verbose = true)
		{
			if (m_CurrentBakingSet != null)
			{
				m_CurrentBakingSet.SetActiveScenario(scenario, verbose);
			}
		}

		public void BlendLightingScenario(string otherScenario, float blendingFactor)
		{
			if (m_CurrentBakingSet != null)
			{
				m_CurrentBakingSet.BlendLightingScenario(otherScenario, blendingFactor);
			}
		}

		internal void RegisterPerSceneData(global::UnityEngine.Rendering.ProbeVolumePerSceneData data)
		{
			if (!perSceneDataList.Contains(data))
			{
				perSceneDataList.Add(data);
				if (m_IsInitialized)
				{
					data.Initialize();
				}
			}
		}

		internal bool ScheduleBakingSet(global::UnityEngine.Rendering.ProbeVolumeBakingSet bakingSet)
		{
			if (m_IsInitialized)
			{
				return false;
			}
			m_LazyBakingSet = bakingSet;
			return true;
		}

		internal bool ProcessScheduledBakingSet()
		{
			if (m_LazyBakingSet == null)
			{
				return false;
			}
			SetActiveBakingSet(m_LazyBakingSet);
			m_LazyBakingSet = null;
			return true;
		}

		public void SetActiveScene(global::UnityEngine.SceneManagement.Scene scene)
		{
			if (TryGetPerSceneData(GetSceneGUID(scene), out var perSceneData))
			{
				SetActiveBakingSet(perSceneData.serializedBakingSet);
			}
		}

		public void SetActiveBakingSet(global::UnityEngine.Rendering.ProbeVolumeBakingSet bakingSet)
		{
			if (m_CurrentBakingSet == bakingSet || ScheduleBakingSet(bakingSet))
			{
				return;
			}
			foreach (global::UnityEngine.Rendering.ProbeVolumePerSceneData perSceneData in perSceneDataList)
			{
				perSceneData.QueueSceneRemoval();
			}
			UnloadBakingSet();
			SetBakingSetAsCurrent(bakingSet);
			if (!(m_CurrentBakingSet != null))
			{
				return;
			}
			foreach (global::UnityEngine.Rendering.ProbeVolumePerSceneData perSceneData2 in perSceneDataList)
			{
				perSceneData2.QueueSceneLoading();
			}
		}

		private void SetBakingSetAsCurrent(global::UnityEngine.Rendering.ProbeVolumeBakingSet bakingSet)
		{
			m_CurrentBakingSet = bakingSet;
			if (m_CurrentBakingSet != null)
			{
				InitProbeReferenceVolume();
				m_CurrentBakingSet.Initialize(m_UseStreamingAssets);
				m_CurrGlobalBounds = m_CurrentBakingSet.globalBounds;
				SetSubdivisionDimensions(bakingSet.minBrickSize, bakingSet.maxSubdivision, bakingSet.bakedProbeOffset);
				m_NeedsIndexRebuild = true;
			}
		}

		internal void RegisterBakingSet(global::UnityEngine.Rendering.ProbeVolumePerSceneData data)
		{
			if (m_CurrentBakingSet == null)
			{
				SetBakingSetAsCurrent(data.serializedBakingSet);
			}
		}

		internal void UnloadBakingSet()
		{
			PerformPendingOperations();
			if (m_CurrentBakingSet != null)
			{
				m_CurrentBakingSet.Cleanup();
			}
			m_CurrentBakingSet = null;
			m_CurrGlobalBounds = default(global::UnityEngine.Bounds);
			if (m_ScratchBufferPool != null)
			{
				m_ScratchBufferPool.Cleanup();
				m_ScratchBufferPool = null;
			}
		}

		internal void UnregisterPerSceneData(global::UnityEngine.Rendering.ProbeVolumePerSceneData data)
		{
			perSceneDataList.Remove(data);
			if (perSceneDataList.Count == 0)
			{
				UnloadBakingSet();
			}
		}

		internal bool TryGetPerSceneData(string sceneGUID, out global::UnityEngine.Rendering.ProbeVolumePerSceneData perSceneData)
		{
			foreach (global::UnityEngine.Rendering.ProbeVolumePerSceneData perSceneData2 in perSceneDataList)
			{
				if (GetSceneGUID(perSceneData2.gameObject.scene) == sceneGUID)
				{
					perSceneData = perSceneData2;
					return true;
				}
			}
			perSceneData = null;
			return false;
		}

		public void Initialize(in global::UnityEngine.Rendering.ProbeVolumeSystemParameters parameters)
		{
			if (m_IsInitialized)
			{
				global::UnityEngine.Debug.LogError("Probe Volume System has already been initialized.");
				return;
			}
			global::UnityEngine.Rendering.ProbeVolumeGlobalSettings renderPipelineSettings = global::UnityEngine.Rendering.GraphicsSettings.GetRenderPipelineSettings<global::UnityEngine.Rendering.ProbeVolumeGlobalSettings>();
			m_MemoryBudget = parameters.memoryBudget;
			m_BlendingMemoryBudget = parameters.blendingMemoryBudget;
			m_SupportScenarios = parameters.supportScenarios;
			m_SupportScenarioBlending = parameters.supportScenarios && parameters.supportScenarioBlending && global::UnityEngine.SystemInfo.supportsComputeShaders && m_BlendingMemoryBudget != (global::UnityEngine.Rendering.ProbeVolumeBlendingTextureMemoryBudget)0;
			m_SHBands = parameters.shBands;
			m_UseStreamingAssets = !renderPipelineSettings.probeVolumeDisableStreamingAssets;
			m_SupportGPUStreaming = parameters.supportGPUStreaming;
			global::UnityEngine.ComputeShader computeShader = global::UnityEngine.Rendering.GraphicsSettings.GetRenderPipelineSettings<global::UnityEngine.Rendering.ProbeVolumeRuntimeResources>()?.probeVolumeUploadDataCS;
			global::UnityEngine.ComputeShader computeShader2 = global::UnityEngine.Rendering.GraphicsSettings.GetRenderPipelineSettings<global::UnityEngine.Rendering.ProbeVolumeRuntimeResources>()?.probeVolumeUploadDataL2CS;
			m_SupportDiskStreaming = parameters.supportDiskStreaming && global::UnityEngine.SystemInfo.supportsComputeShaders && m_SupportGPUStreaming && m_UseStreamingAssets && computeShader != null && computeShader2 != null;
			m_DiskStreamingUseCompute = global::UnityEngine.SystemInfo.supportsComputeShaders && computeShader != null && computeShader2 != null;
			InitializeDebug();
			global::UnityEngine.Rendering.ProbeVolumeConstantRuntimeResources.Initialize();
			global::UnityEngine.Rendering.ProbeBrickPool.Initialize();
			global::UnityEngine.Rendering.ProbeBrickBlendingPool.Initialize();
			InitStreaming();
			m_IsInitialized = true;
			m_NeedsIndexRebuild = true;
			sceneData = parameters.sceneData;
			m_EnabledBySRP = true;
			foreach (global::UnityEngine.Rendering.ProbeVolumePerSceneData perSceneData in perSceneDataList)
			{
				perSceneData.Initialize();
			}
			ProcessScheduledBakingSet();
		}

		public void SetEnableStateFromSRP(bool srpEnablesPV)
		{
			m_EnabledBySRP = srpEnablesPV;
		}

		public void SetVertexSamplingEnabled(bool value)
		{
			m_VertexSampling = value;
		}

		internal void ForceMemoryBudget(global::UnityEngine.Rendering.ProbeVolumeTextureMemoryBudget budget)
		{
			m_MemoryBudget = budget;
		}

		internal void ForceSHBand(global::UnityEngine.Rendering.ProbeVolumeSHBands shBands)
		{
			m_SHBands = shBands;
			DeinitProbeReferenceVolume();
			foreach (global::UnityEngine.Rendering.ProbeVolumePerSceneData perSceneData in perSceneDataList)
			{
				perSceneData.Initialize();
			}
			PerformPendingOperations();
		}

		internal void ForceNoDiskStreaming(bool state)
		{
			m_ForceNoDiskStreaming = state;
		}

		public void Cleanup()
		{
			global::UnityEngine.Rendering.CoreUtils.SafeRelease(m_EmptyIndexBuffer);
			m_EmptyIndexBuffer = null;
			global::UnityEngine.Rendering.ProbeVolumeConstantRuntimeResources.Cleanup();
			if (!m_IsInitialized)
			{
				global::UnityEngine.Debug.LogError("Adaptive Probe Volumes have not been initialized before calling Cleanup.");
				return;
			}
			CleanupLoadedData();
			CleanupDebug();
			CleanupStreaming();
			DeinitProbeReferenceVolume();
			m_IsInitialized = false;
		}

		public int GetVideoMemoryCost()
		{
			if (!m_ProbeReferenceVolumeInit)
			{
				return 0;
			}
			return m_Pool.estimatedVMemCost + m_Index.estimatedVMemCost + m_CellIndices.estimatedVMemCost + m_BlendingPool.estimatedVMemCost + m_TemporaryDataLocationMemCost;
		}

		private void RemoveCell(int cellIndex)
		{
			if (!cells.TryGetValue(cellIndex, out var value))
			{
				return;
			}
			value.referenceCount--;
			if (value.referenceCount <= 0)
			{
				cells.Remove(cellIndex);
				if (value.loaded)
				{
					m_LoadedCells.Remove(value);
					UnloadCell(value);
				}
				else
				{
					m_ToBeLoadedCells.Remove(value);
				}
				m_CurrentBakingSet.ReleaseCell(cellIndex);
				m_CellPool.Release(value);
			}
		}

		internal void UnloadCell(global::UnityEngine.Rendering.ProbeReferenceVolume.Cell cell)
		{
			if (!cell.loaded)
			{
				return;
			}
			if (cell.blendingInfo.blending)
			{
				m_LoadedBlendingCells.Remove(cell);
				UnloadBlendingCell(cell);
			}
			else
			{
				m_ToBeLoadedBlendingCells.Remove(cell);
			}
			if (cell.indexInfo.flatIndicesInGlobalIndirection != null)
			{
				m_CellIndices.MarkEntriesAsUnloaded(cell.indexInfo.flatIndicesInGlobalIndirection);
			}
			if (diskStreamingEnabled)
			{
				if (cell.streamingInfo.IsStreaming())
				{
					CancelStreamingRequest(cell);
				}
				else
				{
					ReleaseBricks(cell);
					cell.data.Cleanup(!diskStreamingEnabled);
				}
			}
			else
			{
				ReleaseBricks(cell);
			}
			cell.loaded = false;
			cell.debugProbes = null;
			ClearDebugData();
		}

		internal void UnloadBlendingCell(global::UnityEngine.Rendering.ProbeReferenceVolume.Cell cell)
		{
			if (diskStreamingEnabled && cell.streamingInfo.IsBlendingStreaming())
			{
				CancelBlendingStreamingRequest(cell);
			}
			if (cell.blendingInfo.blending)
			{
				m_BlendingPool.Deallocate(cell.blendingInfo.chunkList);
				cell.blendingInfo.chunkList.Clear();
				cell.blendingInfo.blending = false;
			}
		}

		internal void UnloadAllCells()
		{
			for (int i = 0; i < m_LoadedCells.size; i++)
			{
				UnloadCell(m_LoadedCells[i]);
			}
			m_ToBeLoadedCells.AddRange(m_LoadedCells);
			m_LoadedCells.Clear();
		}

		internal void UnloadAllBlendingCells()
		{
			for (int i = 0; i < m_LoadedBlendingCells.size; i++)
			{
				UnloadBlendingCell(m_LoadedBlendingCells[i]);
			}
			m_ToBeLoadedBlendingCells.AddRange(m_LoadedBlendingCells);
			m_LoadedBlendingCells.Clear();
		}

		private void AddCell(int cellIndex)
		{
			if (!cells.TryGetValue(cellIndex, out var value))
			{
				global::UnityEngine.Rendering.ProbeReferenceVolume.CellDesc cellDesc = m_CurrentBakingSet.GetCellDesc(cellIndex);
				if (cellDesc != null)
				{
					value = m_CellPool.Get();
					value.desc = cellDesc;
					value.data = m_CurrentBakingSet.GetCellData(cellIndex);
					value.poolInfo.shChunkCount = value.desc.shChunkCount;
					value.indexInfo.flatIndicesInGlobalIndirection = m_CellIndices.GetFlatIndicesForCell(cellDesc.position);
					value.indexInfo.indexChunkCount = value.desc.indexChunkCount;
					value.indexInfo.indirectionEntryInfo = value.desc.indirectionEntryInfo;
					value.indexInfo.updateInfo.entriesInfo = new global::UnityEngine.Rendering.ProbeBrickIndex.IndirectionEntryUpdateInfo[cellDesc.indirectionEntryInfo.Length];
					value.referenceCount = 1;
					cells[cellIndex] = value;
					m_ToBeLoadedCells.Add(in value);
				}
			}
			else
			{
				value.referenceCount++;
			}
		}

		internal bool LoadCell(global::UnityEngine.Rendering.ProbeReferenceVolume.Cell cell, bool ignoreErrorLog = false)
		{
			if (ReservePoolChunks(cell.desc.bricksCount, cell.poolInfo.chunkList, ignoreErrorLog))
			{
				int num = cell.indexInfo.indirectionEntryInfo.Length;
				global::UnityEngine.Rendering.ProbeReferenceVolume.CellIndexInfo indexInfo = cell.indexInfo;
				for (int i = 0; i < num; i++)
				{
					if (!cell.indexInfo.indirectionEntryInfo[i].hasMinMax)
					{
						if (cell.data.bricks.IsCreated)
						{
							ComputeEntryMinMax(ref cell.indexInfo.indirectionEntryInfo[i], cell.data.bricks);
						}
						else
						{
							int num2 = CellSize(GetEntrySubdivLevel());
							cell.indexInfo.indirectionEntryInfo[i].minBrickPos = global::UnityEngine.Vector3Int.zero;
							cell.indexInfo.indirectionEntryInfo[i].maxBrickPosPlusOne = new global::UnityEngine.Vector3Int(num2 + 1, num2 + 1, num2 + 1);
							cell.indexInfo.indirectionEntryInfo[i].hasMinMax = true;
						}
					}
					int numberOfBricksAtSubdiv = GetNumberOfBricksAtSubdiv(cell.indexInfo.indirectionEntryInfo[i]);
					indexInfo.updateInfo.entriesInfo[i].numberOfChunks = m_Index.GetNumberOfChunks(numberOfBricksAtSubdiv);
				}
				if (m_Index.FindSlotsForEntries(ref indexInfo.updateInfo.entriesInfo))
				{
					bool flag = cell.UpdateCellScenarioData(lightingScenario, otherScenario);
					m_Index.ReserveChunks(indexInfo.updateInfo.entriesInfo, ignoreErrorLog);
					for (int j = 0; j < num; j++)
					{
						indexInfo.updateInfo.entriesInfo[j].minValidBrickIndexForCellAtMaxRes = indexInfo.indirectionEntryInfo[j].minBrickPos;
						indexInfo.updateInfo.entriesInfo[j].maxValidBrickIndexForCellAtMaxResPlusOne = indexInfo.indirectionEntryInfo[j].maxBrickPosPlusOne;
						indexInfo.updateInfo.entriesInfo[j].entryPositionInBricksAtMaxRes = indexInfo.indirectionEntryInfo[j].positionInBricks;
						indexInfo.updateInfo.entriesInfo[j].minSubdivInCell = indexInfo.indirectionEntryInfo[j].minSubdiv;
						indexInfo.updateInfo.entriesInfo[j].hasOnlyBiggerBricks = indexInfo.indirectionEntryInfo[j].hasOnlyBiggerBricks;
					}
					cell.loaded = true;
					if (flag)
					{
						AddBricks(cell);
					}
					minLoadedCellPos = global::UnityEngine.Vector3Int.Min(minLoadedCellPos, cell.desc.position);
					maxLoadedCellPos = global::UnityEngine.Vector3Int.Max(maxLoadedCellPos, cell.desc.position);
					ClearDebugData();
					return true;
				}
				ReleasePoolChunks(cell.poolInfo.chunkList);
				StartIndexDefragmentation();
				return false;
			}
			return false;
		}

		internal void LoadAllCells()
		{
			int size = m_LoadedCells.size;
			for (int i = 0; i < m_ToBeLoadedCells.size; i++)
			{
				global::UnityEngine.Rendering.ProbeReferenceVolume.Cell value = m_ToBeLoadedCells[i];
				if (LoadCell(value, ignoreErrorLog: true))
				{
					m_LoadedCells.Add(in value);
				}
			}
			for (int j = size; j < m_LoadedCells.size; j++)
			{
				m_ToBeLoadedCells.Remove(m_LoadedCells[j]);
			}
		}

		private void ComputeCellGlobalInfo()
		{
			minLoadedCellPos = new global::UnityEngine.Vector3Int(int.MaxValue, int.MaxValue, int.MaxValue);
			maxLoadedCellPos = new global::UnityEngine.Vector3Int(int.MinValue, int.MinValue, int.MinValue);
			foreach (global::UnityEngine.Rendering.ProbeReferenceVolume.Cell value in cells.Values)
			{
				if (value.loaded)
				{
					minLoadedCellPos = global::UnityEngine.Vector3Int.Min(value.desc.position, minLoadedCellPos);
					maxLoadedCellPos = global::UnityEngine.Vector3Int.Max(value.desc.position, maxLoadedCellPos);
				}
			}
		}

		internal void AddPendingSceneLoading(string sceneGUID, global::UnityEngine.Rendering.ProbeVolumeBakingSet bakingSet)
		{
			if (m_PendingScenesToBeLoaded.ContainsKey(sceneGUID))
			{
				m_PendingScenesToBeLoaded.Remove(sceneGUID);
			}
			if (bakingSet == null && m_CurrentBakingSet != null && m_CurrentBakingSet.singleSceneMode)
			{
				return;
			}
			if (bakingSet.chunkSizeInBricks != global::UnityEngine.Rendering.ProbeBrickPool.GetChunkSizeInBrickCount())
			{
				global::UnityEngine.Debug.LogError("Trying to load Adaptive Probe Volumes data (" + bakingSet.name + ") baked with an older incompatible version of APV. Please rebake your data.");
			}
			else
			{
				if (m_CurrentBakingSet != null && !m_CurrentBakingSet.HasSameSceneGUIDs(bakingSet))
				{
					return;
				}
				if (m_PendingScenesToBeLoaded.Count != 0)
				{
					using global::System.Collections.Generic.Dictionary<string, (global::UnityEngine.Rendering.ProbeVolumeBakingSet, global::System.Collections.Generic.List<int>)>.ValueCollection.Enumerator enumerator = m_PendingScenesToBeLoaded.Values.GetEnumerator();
					if (enumerator.MoveNext() && bakingSet != enumerator.Current.Item1)
					{
						global::UnityEngine.Debug.LogError("Trying to load Adaptive Probe Volumes data for a scene from a different baking set from other scenes that are being loaded. Please make sure all loaded scenes are in the same baking set.");
						return;
					}
				}
				m_PendingScenesToBeLoaded.Add(sceneGUID, (bakingSet, m_CurrentBakingSet.GetSceneCellIndexList(sceneGUID)));
				m_NeedLoadAsset = true;
			}
		}

		internal void AddPendingSceneRemoval(string sceneGUID)
		{
			if (m_PendingScenesToBeLoaded.ContainsKey(sceneGUID))
			{
				m_PendingScenesToBeLoaded.Remove(sceneGUID);
			}
			if (m_ActiveScenes.Contains(sceneGUID) && m_CurrentBakingSet != null)
			{
				m_PendingScenesToBeUnloaded.TryAdd(sceneGUID, m_CurrentBakingSet.GetSceneCellIndexList(sceneGUID));
			}
		}

		internal void RemovePendingScene(string sceneGUID, global::System.Collections.Generic.List<int> cellList)
		{
			if (m_ActiveScenes.Contains(sceneGUID))
			{
				m_ActiveScenes.Remove(sceneGUID);
			}
			foreach (int cell in cellList)
			{
				RemoveCell(cell);
			}
			ClearDebugData();
			ComputeCellGlobalInfo();
		}

		private void PerformPendingIndexChangeAndInit()
		{
			if (m_NeedsIndexRebuild)
			{
				CleanupLoadedData();
				InitializeGlobalIndirection();
				m_HasChangedIndex = true;
				m_NeedsIndexRebuild = false;
			}
			else
			{
				m_HasChangedIndex = false;
			}
		}

		internal void SetSubdivisionDimensions(float minBrickSize, int maxSubdiv, global::UnityEngine.Vector3 offset)
		{
			m_MinBrickSize = minBrickSize;
			SetMaxSubdivision(maxSubdiv);
			m_ProbeOffset = offset;
		}

		private bool LoadCells(global::System.Collections.Generic.List<int> cellIndices)
		{
			if (m_CurrentBakingSet.ResolveCellData(cellIndices))
			{
				ClearDebugData();
				for (int i = 0; i < cellIndices.Count; i++)
				{
					AddCell(cellIndices[i]);
				}
				return true;
			}
			return false;
		}

		private void PerformPendingLoading()
		{
			if ((m_PendingScenesToBeLoaded.Count == 0 && m_ActiveScenes.Count == 0) || !m_NeedLoadAsset || !m_ProbeReferenceVolumeInit)
			{
				return;
			}
			m_Pool.EnsureTextureValidity();
			m_BlendingPool.EnsureTextureValidity();
			if (m_HasChangedIndex)
			{
				foreach (string activeScene in m_ActiveScenes)
				{
					LoadCells(m_CurrentBakingSet.GetSceneCellIndexList(activeScene));
				}
			}
			foreach (global::System.Collections.Generic.KeyValuePair<string, (global::UnityEngine.Rendering.ProbeVolumeBakingSet, global::System.Collections.Generic.List<int>)> item in m_PendingScenesToBeLoaded)
			{
				string key = item.Key;
				if (LoadCells(item.Value.Item2) && !m_ActiveScenes.Contains(key))
				{
					m_ActiveScenes.Add(key);
				}
			}
			m_PendingScenesToBeLoaded.Clear();
			m_NeedLoadAsset = false;
		}

		private void PerformPendingDeletion()
		{
			foreach (global::System.Collections.Generic.KeyValuePair<string, global::System.Collections.Generic.List<int>> item in m_PendingScenesToBeUnloaded)
			{
				RemovePendingScene(item.Key, item.Value);
			}
			m_PendingScenesToBeUnloaded.Clear();
		}

		internal void ComputeEntryMinMax(ref global::UnityEngine.Rendering.ProbeReferenceVolume.IndirectionEntryInfo entryInfo, global::System.ReadOnlySpan<global::UnityEngine.Rendering.ProbeBrickIndex.Brick> bricks)
		{
			int num = CellSize(GetEntrySubdivLevel());
			global::UnityEngine.Vector3Int positionInBricks = entryInfo.positionInBricks;
			global::UnityEngine.Vector3Int vector3Int = entryInfo.positionInBricks + new global::UnityEngine.Vector3Int(num, num, num);
			if (entryInfo.hasOnlyBiggerBricks)
			{
				entryInfo.minBrickPos = positionInBricks;
				entryInfo.maxBrickPosPlusOne = vector3Int;
			}
			else
			{
				entryInfo.minBrickPos = (entryInfo.maxBrickPosPlusOne = global::UnityEngine.Vector3Int.zero);
				bool flag = false;
				for (int i = 0; i < bricks.Length; i++)
				{
					int num2 = CellSize(bricks[i].subdivisionLevel);
					global::UnityEngine.Vector3Int position = bricks[i].position;
					global::UnityEngine.Vector3Int vector3Int2 = bricks[i].position + new global::UnityEngine.Vector3Int(num2, num2, num2);
					if (global::UnityEngine.Rendering.ProbeBrickIndex.BrickOverlapEntry(position, vector3Int2, positionInBricks, vector3Int))
					{
						position = global::UnityEngine.Vector3Int.Max(position, positionInBricks);
						vector3Int2 = global::UnityEngine.Vector3Int.Min(vector3Int2, vector3Int);
						if (flag)
						{
							entryInfo.minBrickPos = global::UnityEngine.Vector3Int.Min(position, entryInfo.minBrickPos);
							entryInfo.maxBrickPosPlusOne = global::UnityEngine.Vector3Int.Max(vector3Int2, entryInfo.maxBrickPosPlusOne);
						}
						else
						{
							entryInfo.minBrickPos = position;
							entryInfo.maxBrickPosPlusOne = vector3Int2;
							flag = true;
						}
					}
				}
			}
			entryInfo.minBrickPos -= positionInBricks;
			entryInfo.maxBrickPosPlusOne = global::UnityEngine.Vector3Int.one + entryInfo.maxBrickPosPlusOne - positionInBricks;
			entryInfo.hasMinMax = true;
		}

		internal static int GetNumberOfBricksAtSubdiv(global::UnityEngine.Rendering.ProbeReferenceVolume.IndirectionEntryInfo entryInfo)
		{
			if (entryInfo.hasOnlyBiggerBricks)
			{
				return 1;
			}
			global::UnityEngine.Vector3Int vector3Int = (entryInfo.maxBrickPosPlusOne - entryInfo.minBrickPos) / CellSize(entryInfo.minSubdiv);
			return vector3Int.x * vector3Int.y * vector3Int.z;
		}

		public void PerformPendingOperations()
		{
			PerformPendingDeletion();
			PerformPendingIndexChangeAndInit();
			PerformPendingLoading();
		}

		internal void InitializeGlobalIndirection()
		{
			global::UnityEngine.Vector3Int cellMin = (m_CurrentBakingSet ? m_CurrentBakingSet.minCellPosition : global::UnityEngine.Vector3Int.zero);
			global::UnityEngine.Vector3Int cellMax = (m_CurrentBakingSet ? m_CurrentBakingSet.maxCellPosition : global::UnityEngine.Vector3Int.zero);
			if (m_CellIndices != null)
			{
				m_CellIndices.Cleanup();
			}
			m_CellIndices = new global::UnityEngine.Rendering.ProbeGlobalIndirection(cellMin, cellMax, global::UnityEngine.Mathf.Max(1, (int)global::UnityEngine.Mathf.Pow(3f, m_MaxSubdivision - 1)));
			if (m_SupportGPUStreaming)
			{
				if (m_DefragCellIndices != null)
				{
					m_DefragCellIndices.Cleanup();
				}
				m_DefragCellIndices = new global::UnityEngine.Rendering.ProbeGlobalIndirection(cellMin, cellMax, global::UnityEngine.Mathf.Max(1, (int)global::UnityEngine.Mathf.Pow(3f, m_MaxSubdivision - 1)));
			}
		}

		private void InitProbeReferenceVolume()
		{
			if (m_ProbeReferenceVolumeInit && !m_Pool.EnsureTextureValidity(useRenderingLayers, skyOcclusion, skyOcclusionShadingDirection, probeOcclusion))
			{
				m_TemporaryDataLocation.Cleanup();
				m_TemporaryDataLocation = global::UnityEngine.Rendering.ProbeBrickPool.CreateDataLocation(global::UnityEngine.Rendering.ProbeBrickPool.GetChunkSizeInProbeCount(), compressed: false, m_SHBands, "APV_Intermediate", allocateRendertexture: false, allocateValidityData: true, useRenderingLayers, skyOcclusion, skyOcclusionShadingDirection, probeOcclusion, out m_TemporaryDataLocationMemCost);
			}
			if (!m_ProbeReferenceVolumeInit)
			{
				m_Pool = new global::UnityEngine.Rendering.ProbeBrickPool(m_MemoryBudget, m_SHBands, allocateValidityData: true, useRenderingLayers, skyOcclusion, skyOcclusionShadingDirection, probeOcclusion);
				m_BlendingPool = new global::UnityEngine.Rendering.ProbeBrickBlendingPool(m_BlendingMemoryBudget, m_SHBands, probeOcclusion);
				m_Index = new global::UnityEngine.Rendering.ProbeBrickIndex(m_MemoryBudget);
				if (m_SupportGPUStreaming)
				{
					m_DefragIndex = new global::UnityEngine.Rendering.ProbeBrickIndex(m_MemoryBudget);
				}
				InitializeGlobalIndirection();
				m_TemporaryDataLocation = global::UnityEngine.Rendering.ProbeBrickPool.CreateDataLocation(global::UnityEngine.Rendering.ProbeBrickPool.GetChunkSizeInProbeCount(), compressed: false, m_SHBands, "APV_Intermediate", allocateRendertexture: false, allocateValidityData: true, useRenderingLayers, skyOcclusion, skyOcclusionShadingDirection, probeOcclusion, out m_TemporaryDataLocationMemCost);
				m_PositionOffsets[0] = 0f;
				float num = 1f / 3f;
				for (int i = 1; i < 3; i++)
				{
					m_PositionOffsets[i] = (float)i * num;
				}
				m_PositionOffsets[m_PositionOffsets.Length - 1] = 1f;
				m_ProbeReferenceVolumeInit = true;
				ClearDebugData();
				m_NeedLoadAsset = true;
			}
			if (global::UnityEngine.Rendering.DebugManager.instance.GetPanel(k_DebugPanelName) != null)
			{
				instance.UnregisterDebug(destroyPanel: false);
				instance.RegisterDebug();
			}
		}

		private ProbeReferenceVolume()
		{
			m_MinBrickSize = 1f;
		}

		public global::UnityEngine.Rendering.ProbeReferenceVolume.RuntimeResources GetRuntimeResources()
		{
			if (!m_ProbeReferenceVolumeInit)
			{
				return default(global::UnityEngine.Rendering.ProbeReferenceVolume.RuntimeResources);
			}
			global::UnityEngine.Rendering.ProbeReferenceVolume.RuntimeResources rr = default(global::UnityEngine.Rendering.ProbeReferenceVolume.RuntimeResources);
			m_Index.GetRuntimeResources(ref rr);
			m_CellIndices.GetRuntimeResources(ref rr);
			m_Pool.GetRuntimeResources(ref rr);
			global::UnityEngine.Rendering.ProbeVolumeConstantRuntimeResources.GetRuntimeResources(ref rr);
			return rr;
		}

		internal void SetMaxSubdivision(int maxSubdivision)
		{
			if (global::System.Math.Min(maxSubdivision, 7) != m_MaxSubdivision)
			{
				m_MaxSubdivision = global::System.Math.Min(maxSubdivision, 7);
				if (m_CellIndices != null)
				{
					m_CellIndices.Cleanup();
				}
				if (m_SupportGPUStreaming && m_DefragCellIndices != null)
				{
					m_DefragCellIndices.Cleanup();
				}
				InitializeGlobalIndirection();
			}
		}

		internal static int CellSize(int subdivisionLevel)
		{
			return global::UnityEngine.Rendering.ProbeVolumeUtil.CellSize(subdivisionLevel);
		}

		internal float BrickSize(int subdivisionLevel)
		{
			return global::UnityEngine.Rendering.ProbeVolumeUtil.BrickSize(m_MinBrickSize, subdivisionLevel);
		}

		internal float MinBrickSize()
		{
			return m_MinBrickSize;
		}

		internal float MaxBrickSize()
		{
			return global::UnityEngine.Rendering.ProbeVolumeUtil.MaxBrickSize(m_MinBrickSize, m_MaxSubdivision);
		}

		internal global::UnityEngine.Vector3 ProbeOffset()
		{
			return m_ProbeOffset;
		}

		internal int GetMaxSubdivision()
		{
			return m_MaxSubdivision;
		}

		internal int GetMaxSubdivision(float multiplier)
		{
			return global::UnityEngine.Mathf.CeilToInt((float)m_MaxSubdivision * multiplier);
		}

		internal float GetDistanceBetweenProbes(int subdivisionLevel)
		{
			return BrickSize(subdivisionLevel) / 3f;
		}

		internal float MinDistanceBetweenProbes()
		{
			return GetDistanceBetweenProbes(0);
		}

		internal int GetGlobalIndirectionEntryMaxSubdiv()
		{
			return 3;
		}

		internal int GetEntrySubdivLevel()
		{
			return global::UnityEngine.Mathf.Min(3, m_MaxSubdivision - 1);
		}

		internal float GetEntrySize()
		{
			return BrickSize(GetEntrySubdivLevel());
		}

		public bool DataHasBeenLoaded()
		{
			return m_LoadedCells.size != 0;
		}

		internal void Clear()
		{
			if (m_ProbeReferenceVolumeInit)
			{
				try
				{
					PerformPendingOperations();
				}
				finally
				{
					UnloadAllCells();
					m_ToBeLoadedCells.Clear();
					m_Pool.Clear();
					m_BlendingPool.Clear();
					m_Index.Clear();
					cells.Clear();
				}
			}
			if (clearAssetsOnVolumeClear)
			{
				m_PendingScenesToBeLoaded.Clear();
				m_ActiveScenes.Clear();
			}
		}

		private global::System.Collections.Generic.List<global::UnityEngine.Rendering.ProbeBrickPool.BrickChunkAlloc> GetSourceLocations(int count, int chunkSize, global::UnityEngine.Rendering.ProbeBrickPool.DataLocation dataLoc)
		{
			global::UnityEngine.Rendering.ProbeBrickPool.BrickChunkAlloc item = default(global::UnityEngine.Rendering.ProbeBrickPool.BrickChunkAlloc);
			m_TmpSrcChunks.Clear();
			m_TmpSrcChunks.Add(item);
			for (int i = 1; i < count; i++)
			{
				item.x += chunkSize * 4;
				if (item.x >= dataLoc.width)
				{
					item.x = 0;
					item.y += 4;
					if (item.y >= dataLoc.height)
					{
						item.y = 0;
						item.z += 4;
					}
				}
				m_TmpSrcChunks.Add(item);
			}
			return m_TmpSrcChunks;
		}

		private void UpdateDataLocationTexture<T>(global::UnityEngine.Texture output, global::Unity.Collections.NativeArray<T> input) where T : struct
		{
			(output as global::UnityEngine.Texture3D).GetPixelData<T>(0).GetSubArray(0, input.Length).CopyFrom(input);
			(output as global::UnityEngine.Texture3D).Apply();
		}

		private void UpdateValidityTextureWithoutMask(global::UnityEngine.Texture output, global::Unity.Collections.NativeArray<byte> input)
		{
			if (global::UnityEngine.Experimental.Rendering.GraphicsFormatUtility.GetComponentCount(output.graphicsFormat) == 1)
			{
				UpdateDataLocationTexture(output, input);
				return;
			}
			global::Unity.Collections.NativeArray<(byte, byte, byte, byte)> pixelData = (output as global::UnityEngine.Texture3D).GetPixelData<(byte, byte, byte, byte)>(0);
			for (int i = 0; i < input.Length; i++)
			{
				pixelData[i] = (input[i], input[i], input[i], input[i]);
			}
			(output as global::UnityEngine.Texture3D).Apply();
		}

		private void UpdatePool(global::System.Collections.Generic.List<global::UnityEngine.Rendering.ProbeBrickPool.BrickChunkAlloc> chunkList, global::UnityEngine.Rendering.ProbeReferenceVolume.CellData.PerScenarioData data, global::Unity.Collections.NativeArray<byte> validityNeighMaskData, global::Unity.Collections.NativeArray<ushort> skyOcclusionL0L1Data, global::Unity.Collections.NativeArray<byte> skyShadingDirectionIndices, int chunkIndex, int poolIndex)
		{
			int chunkSizeInProbeCount = global::UnityEngine.Rendering.ProbeBrickPool.GetChunkSizeInProbeCount();
			UpdateDataLocationTexture(m_TemporaryDataLocation.TexL0_L1rx, data.shL0L1RxData.GetSubArray(chunkIndex * chunkSizeInProbeCount * 4, chunkSizeInProbeCount * 4));
			UpdateDataLocationTexture(m_TemporaryDataLocation.TexL1_G_ry, data.shL1GL1RyData.GetSubArray(chunkIndex * chunkSizeInProbeCount * 4, chunkSizeInProbeCount * 4));
			UpdateDataLocationTexture(m_TemporaryDataLocation.TexL1_B_rz, data.shL1BL1RzData.GetSubArray(chunkIndex * chunkSizeInProbeCount * 4, chunkSizeInProbeCount * 4));
			if (m_SHBands == global::UnityEngine.Rendering.ProbeVolumeSHBands.SphericalHarmonicsL2 && data.shL2Data_0.Length > 0)
			{
				UpdateDataLocationTexture(m_TemporaryDataLocation.TexL2_0, data.shL2Data_0.GetSubArray(chunkIndex * chunkSizeInProbeCount * 4, chunkSizeInProbeCount * 4));
				UpdateDataLocationTexture(m_TemporaryDataLocation.TexL2_1, data.shL2Data_1.GetSubArray(chunkIndex * chunkSizeInProbeCount * 4, chunkSizeInProbeCount * 4));
				UpdateDataLocationTexture(m_TemporaryDataLocation.TexL2_2, data.shL2Data_2.GetSubArray(chunkIndex * chunkSizeInProbeCount * 4, chunkSizeInProbeCount * 4));
				UpdateDataLocationTexture(m_TemporaryDataLocation.TexL2_3, data.shL2Data_3.GetSubArray(chunkIndex * chunkSizeInProbeCount * 4, chunkSizeInProbeCount * 4));
			}
			if (probeOcclusion && data.probeOcclusion.Length > 0)
			{
				UpdateDataLocationTexture(m_TemporaryDataLocation.TexProbeOcclusion, data.probeOcclusion.GetSubArray(chunkIndex * chunkSizeInProbeCount * 4, chunkSizeInProbeCount * 4));
			}
			if (poolIndex == -1)
			{
				if (validityNeighMaskData.Length > 0)
				{
					if (m_CurrentBakingSet.bakedMaskCount == 1)
					{
						UpdateValidityTextureWithoutMask(m_TemporaryDataLocation.TexValidity, validityNeighMaskData.GetSubArray(chunkIndex * chunkSizeInProbeCount, chunkSizeInProbeCount));
					}
					else
					{
						UpdateDataLocationTexture(m_TemporaryDataLocation.TexValidity, validityNeighMaskData.Reinterpret<uint>(1).GetSubArray(chunkIndex * chunkSizeInProbeCount, chunkSizeInProbeCount));
					}
				}
				if (skyOcclusion && skyOcclusionL0L1Data.Length > 0)
				{
					UpdateDataLocationTexture(m_TemporaryDataLocation.TexSkyOcclusion, skyOcclusionL0L1Data.GetSubArray(chunkIndex * chunkSizeInProbeCount * 4, chunkSizeInProbeCount * 4));
				}
				if (skyOcclusionShadingDirection && skyShadingDirectionIndices.Length > 0)
				{
					UpdateDataLocationTexture(m_TemporaryDataLocation.TexSkyShadingDirectionIndices, skyShadingDirectionIndices.GetSubArray(chunkIndex * chunkSizeInProbeCount, chunkSizeInProbeCount));
				}
			}
			global::System.Collections.Generic.List<global::UnityEngine.Rendering.ProbeBrickPool.BrickChunkAlloc> sourceLocations = GetSourceLocations(1, global::UnityEngine.Rendering.ProbeBrickPool.GetChunkSizeInBrickCount(), m_TemporaryDataLocation);
			if (poolIndex == -1)
			{
				m_Pool.Update(m_TemporaryDataLocation, sourceLocations, chunkList, chunkIndex, m_SHBands);
			}
			else
			{
				m_BlendingPool.Update(m_TemporaryDataLocation, sourceLocations, chunkList, chunkIndex, m_SHBands, poolIndex);
			}
		}

		private void UpdatePool(global::UnityEngine.Rendering.CommandBuffer cmd, global::System.Collections.Generic.List<global::UnityEngine.Rendering.ProbeBrickPool.BrickChunkAlloc> chunkList, global::UnityEngine.Rendering.ProbeReferenceVolume.CellStreamingScratchBuffer dataBuffer, global::UnityEngine.Rendering.ProbeReferenceVolume.CellStreamingScratchBufferLayout layout, int poolIndex)
		{
			if (poolIndex == -1)
			{
				m_Pool.Update(cmd, dataBuffer, layout, chunkList, updateSharedData: true, m_Pool.GetValidityTexture(), m_SHBands, skyOcclusion, m_Pool.GetSkyOcclusionTexture(), skyOcclusionShadingDirection, m_Pool.GetSkyShadingDirectionIndicesTexture(), probeOcclusion);
			}
			else
			{
				m_BlendingPool.Update(cmd, dataBuffer, layout, chunkList, m_SHBands, poolIndex, m_Pool.GetValidityTexture(), skyOcclusion, m_Pool.GetSkyOcclusionTexture(), skyOcclusionShadingDirection, m_Pool.GetSkyShadingDirectionIndicesTexture(), probeOcclusion);
			}
		}

		private void UpdateSharedData(global::System.Collections.Generic.List<global::UnityEngine.Rendering.ProbeBrickPool.BrickChunkAlloc> chunkList, global::Unity.Collections.NativeArray<byte> validityNeighMaskData, global::Unity.Collections.NativeArray<ushort> skyOcclusionData, global::Unity.Collections.NativeArray<byte> skyShadingDirectionIndices, int chunkIndex)
		{
			int num = global::UnityEngine.Rendering.ProbeBrickPool.GetChunkSizeInBrickCount() * 64;
			if (m_CurrentBakingSet.bakedMaskCount == 1)
			{
				UpdateValidityTextureWithoutMask(m_TemporaryDataLocation.TexValidity, validityNeighMaskData.GetSubArray(chunkIndex * num, num));
			}
			else
			{
				UpdateDataLocationTexture(m_TemporaryDataLocation.TexValidity, validityNeighMaskData.Reinterpret<uint>(1).GetSubArray(chunkIndex * num, num));
			}
			if (skyOcclusion && skyOcclusionData.Length > 0)
			{
				UpdateDataLocationTexture(m_TemporaryDataLocation.TexSkyOcclusion, skyOcclusionData.GetSubArray(chunkIndex * num * 4, num * 4));
			}
			if (skyOcclusion && skyOcclusionShadingDirection && skyShadingDirectionIndices.Length > 0)
			{
				UpdateDataLocationTexture(m_TemporaryDataLocation.TexSkyShadingDirectionIndices, skyShadingDirectionIndices.GetSubArray(chunkIndex * num, num));
			}
			global::System.Collections.Generic.List<global::UnityEngine.Rendering.ProbeBrickPool.BrickChunkAlloc> sourceLocations = GetSourceLocations(1, global::UnityEngine.Rendering.ProbeBrickPool.GetChunkSizeInBrickCount(), m_TemporaryDataLocation);
			m_Pool.UpdateValidity(m_TemporaryDataLocation, sourceLocations, chunkList, chunkIndex);
		}

		private bool AddBlendingBricks(global::UnityEngine.Rendering.ProbeReferenceVolume.Cell cell)
		{
			using (new global::Unity.Profiling.ProfilerMarker("AddBlendingBricks").Auto())
			{
				bool flag = m_CurrentBakingSet.otherScenario == null || !cell.hasTwoScenarios;
				if (!flag && !m_BlendingPool.Allocate(cell.poolInfo.shChunkCount, cell.blendingInfo.chunkList))
				{
					return false;
				}
				if (diskStreamingEnabled)
				{
					if (flag)
					{
						if (cell.blendingInfo.blendingFactor != scenarioBlendingFactor)
						{
							PushDiskStreamingRequest(cell, lightingScenario, -1, m_OnStreamingComplete);
						}
						cell.blendingInfo.MarkUpToDate();
					}
					else
					{
						PushDiskStreamingRequest(cell, lightingScenario, 0, m_OnBlendingStreamingComplete);
						PushDiskStreamingRequest(cell, otherScenario, 1, m_OnBlendingStreamingComplete);
					}
				}
				else
				{
					if (!cell.indexInfo.indexUpdated)
					{
						UpdateCellIndex(cell);
						global::System.Collections.Generic.List<global::UnityEngine.Rendering.ProbeBrickPool.BrickChunkAlloc> chunkList = cell.poolInfo.chunkList;
						for (int i = 0; i < chunkList.Count; i++)
						{
							UpdateSharedData(chunkList, cell.data.validityNeighMaskData, cell.data.skyOcclusionDataL0L1, cell.data.skyShadingDirectionIndices, i);
						}
					}
					if (flag)
					{
						if (cell.blendingInfo.blendingFactor != scenarioBlendingFactor)
						{
							global::System.Collections.Generic.List<global::UnityEngine.Rendering.ProbeBrickPool.BrickChunkAlloc> chunkList2 = cell.poolInfo.chunkList;
							for (int j = 0; j < chunkList2.Count; j++)
							{
								UpdatePool(chunkList2, cell.scenario0, cell.data.validityNeighMaskData, cell.data.skyOcclusionDataL0L1, cell.data.skyShadingDirectionIndices, j, -1);
							}
						}
						cell.blendingInfo.MarkUpToDate();
					}
					else
					{
						global::System.Collections.Generic.List<global::UnityEngine.Rendering.ProbeBrickPool.BrickChunkAlloc> chunkList3 = cell.blendingInfo.chunkList;
						for (int k = 0; k < chunkList3.Count; k++)
						{
							UpdatePool(chunkList3, cell.scenario0, cell.data.validityNeighMaskData, cell.data.skyOcclusionDataL0L1, cell.data.skyShadingDirectionIndices, k, 0);
							UpdatePool(chunkList3, cell.scenario1, cell.data.validityNeighMaskData, cell.data.skyOcclusionDataL0L1, cell.data.skyShadingDirectionIndices, k, 1);
						}
					}
				}
				cell.blendingInfo.blending = true;
				return true;
			}
		}

		private bool ReservePoolChunks(int brickCount, global::System.Collections.Generic.List<global::UnityEngine.Rendering.ProbeBrickPool.BrickChunkAlloc> chunkList, bool ignoreErrorLog)
		{
			int chunkCount = global::UnityEngine.Rendering.ProbeBrickPool.GetChunkCount(brickCount);
			chunkList.Clear();
			return m_Pool.Allocate(chunkCount, chunkList, ignoreErrorLog);
		}

		private void ReleasePoolChunks(global::System.Collections.Generic.List<global::UnityEngine.Rendering.ProbeBrickPool.BrickChunkAlloc> chunkList)
		{
			m_Pool.Deallocate(chunkList);
			chunkList.Clear();
		}

		private void UpdatePoolAndIndex(global::UnityEngine.Rendering.ProbeReferenceVolume.Cell cell, global::UnityEngine.Rendering.ProbeReferenceVolume.CellStreamingScratchBuffer dataBuffer, global::UnityEngine.Rendering.ProbeReferenceVolume.CellStreamingScratchBufferLayout layout, int poolIndex, global::UnityEngine.Rendering.CommandBuffer cmd)
		{
			if (diskStreamingEnabled)
			{
				if (m_DiskStreamingUseCompute)
				{
					UpdatePool(cmd, cell.poolInfo.chunkList, dataBuffer, layout, poolIndex);
				}
				else
				{
					int count = cell.poolInfo.chunkList.Count;
					int num = -2 * (count * 4 * 4);
					global::UnityEngine.Rendering.ProbeReferenceVolume.CellData.PerScenarioData data = new global::UnityEngine.Rendering.ProbeReferenceVolume.CellData.PerScenarioData
					{
						shL0L1RxData = dataBuffer.stagingBuffer.GetSubArray(layout._L0L1rxOffset + num, count * layout._L0Size).Reinterpret<ushort>(1),
						shL1GL1RyData = dataBuffer.stagingBuffer.GetSubArray(layout._L1GryOffset + num, count * layout._L1Size),
						shL1BL1RzData = dataBuffer.stagingBuffer.GetSubArray(layout._L1BrzOffset + num, count * layout._L1Size)
					};
					global::Unity.Collections.NativeArray<byte> subArray = dataBuffer.stagingBuffer.GetSubArray(layout._ValidityOffset + num, count * layout._ValiditySize);
					if (m_SHBands == global::UnityEngine.Rendering.ProbeVolumeSHBands.SphericalHarmonicsL2)
					{
						data.shL2Data_0 = dataBuffer.stagingBuffer.GetSubArray(layout._L2_0Offset + num, count * layout._L2Size);
						data.shL2Data_1 = dataBuffer.stagingBuffer.GetSubArray(layout._L2_1Offset + num, count * layout._L2Size);
						data.shL2Data_2 = dataBuffer.stagingBuffer.GetSubArray(layout._L2_2Offset + num, count * layout._L2Size);
						data.shL2Data_3 = dataBuffer.stagingBuffer.GetSubArray(layout._L2_3Offset + num, count * layout._L2Size);
					}
					if (probeOcclusion && layout._ProbeOcclusionSize > 0)
					{
						data.probeOcclusion = dataBuffer.stagingBuffer.GetSubArray(layout._ProbeOcclusionOffset + num, count * layout._ProbeOcclusionSize);
					}
					global::Unity.Collections.NativeArray<ushort> skyOcclusionL0L1Data = default(global::Unity.Collections.NativeArray<ushort>);
					if (skyOcclusion && layout._SkyOcclusionSize > 0)
					{
						skyOcclusionL0L1Data = dataBuffer.stagingBuffer.GetSubArray(layout._SkyOcclusionOffset + num, count * layout._SkyOcclusionSize).Reinterpret<ushort>(1);
					}
					global::Unity.Collections.NativeArray<byte> skyShadingDirectionIndices = default(global::Unity.Collections.NativeArray<byte>);
					if (skyOcclusion && skyOcclusionShadingDirection && layout._SkyShadingDirectionSize > 0)
					{
						skyShadingDirectionIndices = dataBuffer.stagingBuffer.GetSubArray(layout._SkyShadingDirectionOffset + num, count * layout._SkyShadingDirectionSize);
					}
					for (int i = 0; i < count; i++)
					{
						UpdatePool(cell.poolInfo.chunkList, data, subArray, skyOcclusionL0L1Data, skyShadingDirectionIndices, i, poolIndex);
					}
				}
			}
			else
			{
				for (int j = 0; j < cell.poolInfo.chunkList.Count; j++)
				{
					UpdatePool(cell.poolInfo.chunkList, cell.scenario0, cell.data.validityNeighMaskData, cell.data.skyOcclusionDataL0L1, cell.data.skyShadingDirectionIndices, j, poolIndex);
				}
			}
			if (!cell.indexInfo.indexUpdated)
			{
				UpdateCellIndex(cell);
			}
		}

		private bool AddBricks(global::UnityEngine.Rendering.ProbeReferenceVolume.Cell cell)
		{
			using (new global::Unity.Profiling.ProfilerMarker("AddBricks").Auto())
			{
				if (supportScenarioBlending)
				{
					m_ToBeLoadedBlendingCells.Add(in cell);
				}
				if (!supportScenarioBlending || scenarioBlendingFactor == 0f || !cell.hasTwoScenarios)
				{
					if (diskStreamingEnabled)
					{
						PushDiskStreamingRequest(cell, m_CurrentBakingSet.lightingScenario, -1, m_OnStreamingComplete);
					}
					else
					{
						UpdatePoolAndIndex(cell, null, default(global::UnityEngine.Rendering.ProbeReferenceVolume.CellStreamingScratchBufferLayout), -1, null);
					}
					cell.blendingInfo.blendingFactor = 0f;
				}
				else if (supportScenarioBlending)
				{
					cell.blendingInfo.Prioritize();
					cell.indexInfo.indexUpdated = false;
				}
				cell.loaded = true;
				ClearDebugData();
				return true;
			}
		}

		private void UpdateCellIndex(global::UnityEngine.Rendering.ProbeReferenceVolume.Cell cell)
		{
			cell.indexInfo.indexUpdated = true;
			global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.ProbeBrickIndex.Brick> bricks = cell.data.bricks;
			m_Index.AddBricks(cell.indexInfo, bricks, cell.poolInfo.chunkList, global::UnityEngine.Rendering.ProbeBrickPool.GetChunkSizeInBrickCount(), m_Pool.GetPoolWidth(), m_Pool.GetPoolHeight());
			m_CellIndices.UpdateCell(cell.indexInfo);
		}

		private void ReleaseBricks(global::UnityEngine.Rendering.ProbeReferenceVolume.Cell cell)
		{
			if (cell.poolInfo.chunkList.Count == 0)
			{
				global::UnityEngine.Debug.Log("Tried to release bricks from an empty Cell.");
				return;
			}
			m_Index.RemoveBricks(cell.indexInfo);
			cell.indexInfo.indexUpdated = false;
			m_Pool.Deallocate(cell.poolInfo.chunkList);
			cell.poolInfo.chunkList.Clear();
		}

		internal void UpdateConstantBuffer(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Rendering.ProbeVolumeShadingParameters parameters)
		{
			float num = parameters.normalBias;
			float num2 = parameters.viewBias;
			global::UnityEngine.Rendering.APVLeakReductionMode leakReductionMode = parameters.leakReductionMode;
			if (parameters.scaleBiasByMinDistanceBetweenProbes)
			{
				num *= MinDistanceBetweenProbes();
				num2 *= MinDistanceBetweenProbes();
			}
			global::UnityEngine.Vector3Int globalIndirectionDimension = m_CellIndices.GetGlobalIndirectionDimension();
			global::UnityEngine.Vector3Int poolDimensions = m_Pool.GetPoolDimensions();
			m_CellIndices.GetMinMaxEntry(out var minEntry, out var _);
			int entriesPerCellDimension = m_CellIndices.entriesPerCellDimension;
			float w = (parameters.skyOcclusionShadingDirection ? 1f : 0f);
			global::UnityEngine.Vector3 vector = ProbeOffset() + parameters.worldOffset;
			global::UnityEngine.Rendering.ShaderVariablesProbeVolumes data = default(global::UnityEngine.Rendering.ShaderVariablesProbeVolumes);
			data._Offset_LayerCount = new global::UnityEngine.Vector4(vector.x, vector.y, vector.z, parameters.regionCount);
			data._MinLoadedCellInEntries_IndirectionEntryDim = new global::UnityEngine.Vector4(minLoadedCellPos.x * entriesPerCellDimension, minLoadedCellPos.y * entriesPerCellDimension, minLoadedCellPos.z * entriesPerCellDimension, GetEntrySize());
			data._MaxLoadedCellInEntries_RcpIndirectionEntryDim = new global::UnityEngine.Vector4((maxLoadedCellPos.x + 1) * entriesPerCellDimension - 1, (maxLoadedCellPos.y + 1) * entriesPerCellDimension - 1, (maxLoadedCellPos.z + 1) * entriesPerCellDimension - 1, 1f / GetEntrySize());
			data._PoolDim_MinBrickSize = new global::UnityEngine.Vector4(poolDimensions.x, poolDimensions.y, poolDimensions.z, MinBrickSize());
			data._RcpPoolDim_XY = new global::UnityEngine.Vector4(1f / (float)poolDimensions.x, 1f / (float)poolDimensions.y, 1f / (float)poolDimensions.z, 1f / (float)(poolDimensions.x * poolDimensions.y));
			data._MinEntryPos_Noise = new global::UnityEngine.Vector4(minEntry.x, minEntry.y, minEntry.z, parameters.samplingNoise);
			data._EntryCount_X_XY_LeakReduction = new global::Unity.Mathematics.uint4((uint)globalIndirectionDimension.x, (uint)(globalIndirectionDimension.x * globalIndirectionDimension.y), (uint)leakReductionMode, 0u);
			data._Biases_NormalizationClamp = new global::UnityEngine.Vector4(num, num2, parameters.reflNormalizationLowerClamp, parameters.reflNormalizationUpperClamp);
			data._FrameIndex_Weights = new global::UnityEngine.Vector4(parameters.frameIndexForNoise, parameters.weight, parameters.skyOcclusionIntensity, w);
			data._ProbeVolumeLayerMask = parameters.regionLayerMasks;
			global::UnityEngine.Rendering.ConstantBuffer.PushGlobal(cmd, in data, m_CBShaderID);
		}

		private void DeinitProbeReferenceVolume()
		{
			if (m_ProbeReferenceVolumeInit)
			{
				foreach (global::UnityEngine.Rendering.ProbeVolumePerSceneData perSceneData in perSceneDataList)
				{
					AddPendingSceneRemoval(perSceneData.sceneGUID);
				}
				PerformPendingDeletion();
				m_Index.Cleanup();
				m_CellIndices.Cleanup();
				if (m_SupportGPUStreaming)
				{
					m_DefragIndex.Cleanup();
					m_DefragCellIndices.Cleanup();
				}
				if (m_Pool != null)
				{
					m_Pool.Cleanup();
					m_BlendingPool.Cleanup();
				}
				m_TemporaryDataLocation.Cleanup();
				m_ProbeReferenceVolumeInit = false;
				if (m_CurrentBakingSet != null)
				{
					m_CurrentBakingSet.Cleanup();
				}
				m_CurrentBakingSet = null;
			}
			else
			{
				m_CellIndices?.Cleanup();
				m_DefragCellIndices?.Cleanup();
			}
			ClearDebugData();
		}

		private void CleanupLoadedData()
		{
			UnloadAllCells();
		}

		[global::System.Obsolete("Use the other override to support sampling offset in debug modes. #from(6000.0)")]
		public void RenderDebug(global::UnityEngine.Camera camera, global::UnityEngine.Texture exposureTexture)
		{
			RenderDebug(camera, null, exposureTexture);
		}

		public void RenderDebug(global::UnityEngine.Camera camera, global::UnityEngine.Rendering.ProbeVolumesOptions options, global::UnityEngine.Texture exposureTexture)
		{
			if (camera.cameraType != global::UnityEngine.CameraType.Reflection && camera.cameraType != global::UnityEngine.CameraType.Preview)
			{
				if (options != null)
				{
					global::UnityEngine.Rendering.ProbeVolumeDebug.currentOffset = options.worldOffset.value;
				}
				DrawProbeDebug(camera, exposureTexture);
			}
		}

		public bool IsProbeSamplingDebugEnabled()
		{
			return probeSamplingDebugData.update != global::UnityEngine.Rendering.ProbeSamplingDebugUpdate.Never;
		}

		public bool GetProbeSamplingDebugResources(global::UnityEngine.Camera camera, out global::UnityEngine.GraphicsBuffer resultBuffer, out global::UnityEngine.Vector2 coords)
		{
			resultBuffer = probeSamplingDebugData.positionNormalBuffer;
			coords = probeSamplingDebugData.coordinates;
			if (!probeVolumeDebug.drawProbeSamplingDebug)
			{
				return false;
			}
			if (probeSamplingDebugData.update == global::UnityEngine.Rendering.ProbeSamplingDebugUpdate.Never)
			{
				return false;
			}
			if (probeSamplingDebugData.update == global::UnityEngine.Rendering.ProbeSamplingDebugUpdate.Once)
			{
				probeSamplingDebugData.update = global::UnityEngine.Rendering.ProbeSamplingDebugUpdate.Never;
				probeSamplingDebugData.forceScreenCenterCoordinates = false;
			}
			return true;
		}

		private bool TryCreateDebugRenderData()
		{
			if (!global::UnityEngine.Rendering.GraphicsSettings.TryGetRenderPipelineSettings<global::UnityEngine.Rendering.ProbeVolumeDebugResources>(out var settings))
			{
				return false;
			}
			if (global::UnityEngine.Rendering.GraphicsSettings.TryGetRenderPipelineSettings<global::UnityEngine.Rendering.ShaderStrippingSetting>(out var settings2) && settings2.stripRuntimeDebugShaders)
			{
				return false;
			}
			m_DebugMaterial = global::UnityEngine.Rendering.CoreUtils.CreateEngineMaterial(settings.probeVolumeDebugShader);
			m_DebugMaterial.enableInstancing = true;
			m_DebugProbeSamplingMesh = settings.probeSamplingDebugMesh;
			m_DebugProbeSamplingMesh.bounds = new global::UnityEngine.Bounds(global::UnityEngine.Vector3.zero, global::UnityEngine.Vector3.one * 10000000f);
			m_ProbeSamplingDebugMaterial = global::UnityEngine.Rendering.CoreUtils.CreateEngineMaterial(settings.probeVolumeSamplingDebugShader);
			m_ProbeSamplingDebugMaterial02 = global::UnityEngine.Rendering.CoreUtils.CreateEngineMaterial(settings.probeVolumeDebugShader);
			m_ProbeSamplingDebugMaterial02.enableInstancing = true;
			probeSamplingDebugData.positionNormalBuffer = new global::UnityEngine.GraphicsBuffer(global::UnityEngine.GraphicsBuffer.Target.Structured, 2, global::System.Runtime.InteropServices.Marshal.SizeOf(typeof(global::UnityEngine.Vector4)));
			m_DisplayNumbersTexture = settings.numbersDisplayTex;
			m_DebugOffsetMesh = global::UnityEngine.Resources.GetBuiltinResource<global::UnityEngine.Mesh>("pyramid.fbx");
			m_DebugOffsetMesh.bounds = new global::UnityEngine.Bounds(global::UnityEngine.Vector3.zero, global::UnityEngine.Vector3.one * 10000000f);
			m_DebugOffsetMaterial = global::UnityEngine.Rendering.CoreUtils.CreateEngineMaterial(settings.probeVolumeOffsetDebugShader);
			m_DebugOffsetMaterial.enableInstancing = true;
			m_DebugFragmentationMaterial = global::UnityEngine.Rendering.CoreUtils.CreateEngineMaterial(settings.probeVolumeFragmentationDebugShader);
			subdivisionDebugColors[0] = global::UnityEngine.Rendering.ProbeVolumeDebugColorPreferences.s_DetailSubdivision;
			subdivisionDebugColors[1] = global::UnityEngine.Rendering.ProbeVolumeDebugColorPreferences.s_MediumSubdivision;
			subdivisionDebugColors[2] = global::UnityEngine.Rendering.ProbeVolumeDebugColorPreferences.s_LowSubdivision;
			subdivisionDebugColors[3] = global::UnityEngine.Rendering.ProbeVolumeDebugColorPreferences.s_VeryLowSubdivision;
			subdivisionDebugColors[4] = global::UnityEngine.Rendering.ProbeVolumeDebugColorPreferences.s_SparseSubdivision;
			subdivisionDebugColors[5] = global::UnityEngine.Rendering.ProbeVolumeDebugColorPreferences.s_SparsestSubdivision;
			subdivisionDebugColors[6] = global::UnityEngine.Rendering.ProbeVolumeDebugColorPreferences.s_DetailSubdivision;
			return true;
		}

		private void InitializeDebug()
		{
			if (TryCreateDebugRenderData())
			{
				RegisterDebug();
			}
		}

		private void CleanupDebug()
		{
			UnregisterDebug(destroyPanel: true);
			global::UnityEngine.Rendering.CoreUtils.Destroy(m_DebugMaterial);
			global::UnityEngine.Rendering.CoreUtils.Destroy(m_ProbeSamplingDebugMaterial);
			global::UnityEngine.Rendering.CoreUtils.Destroy(m_ProbeSamplingDebugMaterial02);
			global::UnityEngine.Rendering.CoreUtils.Destroy(m_DebugOffsetMaterial);
			global::UnityEngine.Rendering.CoreUtils.Destroy(m_DebugFragmentationMaterial);
			global::UnityEngine.Rendering.CoreUtils.SafeRelease(probeSamplingDebugData?.positionNormalBuffer);
		}

		private void DebugCellIndexChanged<T>(global::UnityEngine.Rendering.DebugUI.Field<T> field, T value)
		{
			ClearDebugData();
		}

		private void RegisterDebug()
		{
			global::System.Collections.Generic.List<global::UnityEngine.Rendering.DebugUI.Widget> list = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.DebugUI.Widget>();
			list.Add(new global::UnityEngine.Rendering.DebugUI.RuntimeDebugShadersMessageBox());
			global::UnityEngine.Rendering.DebugUI.Container container = new global::UnityEngine.Rendering.DebugUI.Container
			{
				displayName = "Subdivision Visualization",
				isHiddenCallback = () => false
			};
			container.children.Add(new global::UnityEngine.Rendering.DebugUI.BoolField
			{
				displayName = "Display Cells",
				tooltip = "Draw Cells used for loading and streaming.",
				getter = () => probeVolumeDebug.drawCells,
				setter = delegate(bool value)
				{
					probeVolumeDebug.drawCells = value;
				},
				onValueChanged = RefreshDebug<bool>
			});
			container.children.Add(new global::UnityEngine.Rendering.DebugUI.BoolField
			{
				displayName = "Display Bricks",
				tooltip = "Display Subdivision bricks.",
				getter = () => probeVolumeDebug.drawBricks,
				setter = delegate(bool value)
				{
					probeVolumeDebug.drawBricks = value;
				},
				onValueChanged = RefreshDebug<bool>
			});
			container.children.Add(new global::UnityEngine.Rendering.DebugUI.FloatField
			{
				displayName = "Debug Draw Distance",
				tooltip = "How far from the Scene Camera to draw debug visualization for Cells and Bricks. Large distances can impact Editor performance.",
				getter = () => probeVolumeDebug.subdivisionViewCullingDistance,
				setter = delegate(float value)
				{
					probeVolumeDebug.subdivisionViewCullingDistance = value;
				},
				min = () => 0f
			});
			list.Add(container);
			list.Add(new global::UnityEngine.Rendering.DebugUI.RuntimeDebugShadersMessageBox());
			global::UnityEngine.Rendering.DebugUI.Container container2 = new global::UnityEngine.Rendering.DebugUI.Container
			{
				displayName = "Probe Visualization"
			};
			container2.children.Add(new global::UnityEngine.Rendering.DebugUI.BoolField
			{
				displayName = "Display Probes",
				tooltip = "Render the debug view showing probe positions. Use the shading mode to determine which type of lighting data to visualize.",
				getter = () => probeVolumeDebug.drawProbes,
				setter = delegate(bool value)
				{
					probeVolumeDebug.drawProbes = value;
				},
				onValueChanged = RefreshDebug<bool>
			});
			global::UnityEngine.Rendering.DebugUI.Container container3 = new global::UnityEngine.Rendering.DebugUI.Container
			{
				isHiddenCallback = () => !probeVolumeDebug.drawProbes
			};
			container3.children.Add(new global::UnityEngine.Rendering.DebugUI.EnumField
			{
				displayName = "Probe Shading Mode",
				tooltip = "Choose which lighting data to show in the probe debug visualization.",
				getter = () => (int)probeVolumeDebug.probeShading,
				setter = delegate(int value)
				{
					probeVolumeDebug.probeShading = (global::UnityEngine.Rendering.DebugProbeShadingMode)value;
				},
				autoEnum = typeof(global::UnityEngine.Rendering.DebugProbeShadingMode),
				getIndex = () => (int)probeVolumeDebug.probeShading,
				setIndex = delegate(int value)
				{
					probeVolumeDebug.probeShading = (global::UnityEngine.Rendering.DebugProbeShadingMode)value;
				}
			});
			container3.children.Add(new global::UnityEngine.Rendering.DebugUI.FloatField
			{
				displayName = "Debug Size",
				tooltip = "The size of probes shown in the debug view.",
				getter = () => probeVolumeDebug.probeSize,
				setter = delegate(float value)
				{
					probeVolumeDebug.probeSize = value;
				},
				min = () => 0.05f,
				max = () => 10f
			});
			global::UnityEngine.Rendering.DebugUI.FloatField item = new global::UnityEngine.Rendering.DebugUI.FloatField
			{
				displayName = "Exposure Compensation",
				tooltip = "Modify the brightness of probe visualizations. Decrease this number to make very bright probes more visible.",
				getter = () => probeVolumeDebug.exposureCompensation,
				setter = delegate(float value)
				{
					probeVolumeDebug.exposureCompensation = value;
				},
				isHiddenCallback = () => probeVolumeDebug.probeShading switch
				{
					global::UnityEngine.Rendering.DebugProbeShadingMode.SH => false, 
					global::UnityEngine.Rendering.DebugProbeShadingMode.SHL0 => false, 
					global::UnityEngine.Rendering.DebugProbeShadingMode.SHL0L1 => false, 
					global::UnityEngine.Rendering.DebugProbeShadingMode.SkyOcclusionSH => false, 
					global::UnityEngine.Rendering.DebugProbeShadingMode.SkyDirection => false, 
					global::UnityEngine.Rendering.DebugProbeShadingMode.ProbeOcclusion => false, 
					_ => true, 
				}
			};
			container3.children.Add(item);
			container3.children.Add(new global::UnityEngine.Rendering.DebugUI.IntField
			{
				displayName = "Max Subdivisions Displayed",
				tooltip = "The highest (most dense) probe subdivision level displayed in the debug view.",
				getter = () => probeVolumeDebug.maxSubdivToVisualize,
				setter = delegate(int v)
				{
					probeVolumeDebug.maxSubdivToVisualize = ((GetMaxSubdivision() == 0) ? 7 : global::UnityEngine.Mathf.Max(0, global::UnityEngine.Mathf.Min(v, GetMaxSubdivision() - 1)));
				},
				min = () => 0,
				max = () => global::UnityEngine.Mathf.Max(0, GetMaxSubdivision() - 1)
			});
			container3.children.Add(new global::UnityEngine.Rendering.DebugUI.IntField
			{
				displayName = "Min Subdivisions Displayed",
				tooltip = "The lowest (least dense) probe subdivision level displayed in the debug view.",
				getter = () => probeVolumeDebug.minSubdivToVisualize,
				setter = delegate(int v)
				{
					probeVolumeDebug.minSubdivToVisualize = global::UnityEngine.Mathf.Max(v, 0);
				},
				min = () => 0,
				max = () => global::UnityEngine.Mathf.Max(0, GetMaxSubdivision() - 1)
			});
			container2.children.Add(container3);
			container2.children.Add(new global::UnityEngine.Rendering.DebugUI.BoolField
			{
				displayName = "Debug Probe Sampling",
				tooltip = "Render the debug view displaying how probes are sampled for a selected pixel. Use the viewport overlay 'SelectPixel' button or Ctrl+Click on the viewport to select the debugged pixel",
				getter = () => probeVolumeDebug.drawProbeSamplingDebug,
				setter = delegate(bool value)
				{
					probeVolumeDebug.drawProbeSamplingDebug = value;
					probeSamplingDebugData.update = global::UnityEngine.Rendering.ProbeSamplingDebugUpdate.Once;
					probeSamplingDebugData.forceScreenCenterCoordinates = true;
				}
			});
			global::UnityEngine.Rendering.DebugUI.Container container4 = new global::UnityEngine.Rendering.DebugUI.Container
			{
				isHiddenCallback = () => !probeVolumeDebug.drawProbeSamplingDebug
			};
			container4.children.Add(new global::UnityEngine.Rendering.DebugUI.FloatField
			{
				displayName = "Debug Size",
				tooltip = "The size of gizmos shown in the debug view.",
				getter = () => probeVolumeDebug.probeSamplingDebugSize,
				setter = delegate(float value)
				{
					probeVolumeDebug.probeSamplingDebugSize = value;
				},
				min = () => 0.05f,
				max = () => 10f
			});
			container4.children.Add(new global::UnityEngine.Rendering.DebugUI.BoolField
			{
				displayName = "Debug With Sampling Noise",
				tooltip = "Enable Sampling Noise for this debug view. It should be enabled for accuracy but it can make results more difficult to read",
				getter = () => probeVolumeDebug.debugWithSamplingNoise,
				setter = delegate(bool value)
				{
					probeVolumeDebug.debugWithSamplingNoise = value;
				},
				onValueChanged = RefreshDebug<bool>
			});
			container2.children.Add(container4);
			container2.children.Add(new global::UnityEngine.Rendering.DebugUI.BoolField
			{
				displayName = "Virtual Offset Debug",
				tooltip = "Enable Virtual Offset debug visualization. Indicates the offsets applied to probe positions. These are used to capture lighting when probes are considered invalid.",
				getter = () => probeVolumeDebug.drawVirtualOffsetPush,
				setter = delegate(bool value)
				{
					probeVolumeDebug.drawVirtualOffsetPush = value;
					if (probeVolumeDebug.drawVirtualOffsetPush && probeVolumeDebug.drawProbes && m_CurrentBakingSet != null)
					{
						float value2 = (float)CellSize(0) * MinBrickSize() / 3f * m_CurrentBakingSet.settings.virtualOffsetSettings.searchMultiplier + m_CurrentBakingSet.settings.virtualOffsetSettings.outOfGeoOffset;
						probeVolumeDebug.probeSize = global::UnityEngine.Mathf.Min(probeVolumeDebug.probeSize, global::UnityEngine.Mathf.Clamp(value2, 0.05f, 10f));
					}
				}
			});
			global::UnityEngine.Rendering.DebugUI.Container container5 = new global::UnityEngine.Rendering.DebugUI.Container
			{
				isHiddenCallback = () => !probeVolumeDebug.drawVirtualOffsetPush
			};
			global::UnityEngine.Rendering.DebugUI.FloatField item2 = new global::UnityEngine.Rendering.DebugUI.FloatField
			{
				displayName = "Debug Size",
				tooltip = "Modify the size of the arrows used in the virtual offset debug visualization.",
				getter = () => probeVolumeDebug.offsetSize,
				setter = delegate(float value)
				{
					probeVolumeDebug.offsetSize = value;
				},
				min = () => 0.001f,
				max = () => 0.1f,
				isHiddenCallback = () => !probeVolumeDebug.drawVirtualOffsetPush
			};
			container5.children.Add(item2);
			container2.children.Add(container5);
			container2.children.Add(new global::UnityEngine.Rendering.DebugUI.FloatField
			{
				displayName = "Debug Draw Distance",
				tooltip = "How far from the Scene Camera to draw probe debug visualizations. Large distances can impact Editor performance.",
				getter = () => probeVolumeDebug.probeCullingDistance,
				setter = delegate(float value)
				{
					probeVolumeDebug.probeCullingDistance = value;
				},
				min = () => 0f
			});
			list.Add(container2);
			global::UnityEngine.Rendering.DebugUI.Container container6 = new global::UnityEngine.Rendering.DebugUI.Container
			{
				displayName = "Probe Adjustment Volumes"
			};
			container6.children.Add(new global::UnityEngine.Rendering.DebugUI.BoolField
			{
				displayName = "Auto Display Probes",
				tooltip = "When enabled and a Probe Adjustment Volumes is selected, automatically display the probes.",
				getter = () => probeVolumeDebug.autoDrawProbes,
				setter = delegate(bool value)
				{
					probeVolumeDebug.autoDrawProbes = value;
				},
				onValueChanged = RefreshDebug<bool>
			});
			container6.children.Add(new global::UnityEngine.Rendering.DebugUI.BoolField
			{
				displayName = "Isolate Affected",
				tooltip = "When enabled, only displayed probes in the influence of the currently selected Probe Adjustment Volumes.",
				getter = () => probeVolumeDebug.isolationProbeDebug,
				setter = delegate(bool value)
				{
					probeVolumeDebug.isolationProbeDebug = value;
				},
				onValueChanged = RefreshDebug<bool>
			});
			list.Add(container6);
			global::UnityEngine.Rendering.DebugUI.Container container7 = new global::UnityEngine.Rendering.DebugUI.Container
			{
				displayName = "Streaming",
				isHiddenCallback = () => !gpuStreamingEnabled && !diskStreamingEnabled
			};
			container7.children.Add(new global::UnityEngine.Rendering.DebugUI.BoolField
			{
				displayName = "Freeze Streaming",
				tooltip = "Stop Unity from streaming probe data in or out of GPU memory.",
				getter = () => probeVolumeDebug.freezeStreaming,
				setter = delegate(bool value)
				{
					probeVolumeDebug.freezeStreaming = value;
				}
			});
			container7.children.Add(new global::UnityEngine.Rendering.DebugUI.BoolField
			{
				displayName = "Display Streaming Score",
				getter = () => probeVolumeDebug.displayCellStreamingScore,
				setter = delegate(bool value)
				{
					probeVolumeDebug.displayCellStreamingScore = value;
				}
			});
			container7.children.Add(new global::UnityEngine.Rendering.DebugUI.BoolField
			{
				displayName = "Maximum cell streaming",
				tooltip = "Enable streaming as many cells as possible every frame.",
				getter = () => instance.loadMaxCellsPerFrame,
				setter = delegate(bool value)
				{
					instance.loadMaxCellsPerFrame = value;
				}
			});
			global::UnityEngine.Rendering.DebugUI.Container container8 = new global::UnityEngine.Rendering.DebugUI.Container
			{
				isHiddenCallback = () => instance.loadMaxCellsPerFrame
			};
			container8.children.Add(new global::UnityEngine.Rendering.DebugUI.IntField
			{
				displayName = "Loaded Cells Per Frame",
				tooltip = "Determines the maximum number of Cells Unity streams per frame. Loading more Cells per frame can impact performance.",
				getter = () => instance.numberOfCellsLoadedPerFrame,
				setter = delegate(int value)
				{
					instance.SetNumberOfCellsLoadedPerFrame(value);
				},
				min = () => 1,
				max = () => 10
			});
			container7.children.Add(container8);
			if (global::UnityEngine.Debug.isDebugBuild)
			{
				container7.children.Add(new global::UnityEngine.Rendering.DebugUI.BoolField
				{
					displayName = "Display Index Fragmentation",
					getter = () => probeVolumeDebug.displayIndexFragmentation,
					setter = delegate(bool value)
					{
						probeVolumeDebug.displayIndexFragmentation = value;
					}
				});
				global::UnityEngine.Rendering.DebugUI.Container container9 = new global::UnityEngine.Rendering.DebugUI.Container
				{
					isHiddenCallback = () => !probeVolumeDebug.displayIndexFragmentation
				};
				container9.children.Add(new global::UnityEngine.Rendering.DebugUI.Value
				{
					displayName = "Index Fragmentation Rate",
					getter = () => instance.indexFragmentationRate
				});
				container7.children.Add(container9);
				container7.children.Add(new global::UnityEngine.Rendering.DebugUI.BoolField
				{
					displayName = "Verbose Log",
					getter = () => probeVolumeDebug.verboseStreamingLog,
					setter = delegate(bool value)
					{
						probeVolumeDebug.verboseStreamingLog = value;
					}
				});
				container7.children.Add(new global::UnityEngine.Rendering.DebugUI.BoolField
				{
					displayName = "Debug Streaming",
					getter = () => probeVolumeDebug.debugStreaming,
					setter = delegate(bool value)
					{
						probeVolumeDebug.debugStreaming = value;
					}
				});
			}
			list.Add(container7);
			if (supportScenarioBlending && m_CurrentBakingSet != null)
			{
				global::UnityEngine.Rendering.DebugUI.Container container10 = new global::UnityEngine.Rendering.DebugUI.Container
				{
					displayName = "Scenario Blending"
				};
				container10.children.Add(new global::UnityEngine.Rendering.DebugUI.IntField
				{
					displayName = "Number Of Cells Blended Per Frame",
					getter = () => instance.numberOfCellsBlendedPerFrame,
					setter = delegate(int value)
					{
						instance.numberOfCellsBlendedPerFrame = value;
					},
					min = () => 0
				});
				container10.children.Add(new global::UnityEngine.Rendering.DebugUI.FloatField
				{
					displayName = "Turnover Rate",
					getter = () => instance.turnoverRate,
					setter = delegate(float value)
					{
						instance.turnoverRate = value;
					},
					min = () => 0f,
					max = () => 1f
				});
				m_DebugScenarioField = new global::UnityEngine.Rendering.DebugUI.EnumField
				{
					displayName = "Scenario Blend Target",
					tooltip = "Select another lighting scenario to blend with the active lighting scenario.",
					enumNames = m_DebugScenarioNames,
					enumValues = m_DebugScenarioValues,
					getIndex = delegate
					{
						if (m_CurrentBakingSet == null)
						{
							return 0;
						}
						RefreshScenarioNames(GetSceneGUID(global::UnityEngine.SceneManagement.SceneManager.GetActiveScene()));
						probeVolumeDebug.otherStateIndex = 0;
						if (!string.IsNullOrEmpty(m_CurrentBakingSet.otherScenario))
						{
							for (int i = 1; i < m_DebugScenarioNames.Length; i++)
							{
								if (m_DebugScenarioNames[i].text == m_CurrentBakingSet.otherScenario)
								{
									probeVolumeDebug.otherStateIndex = i;
									break;
								}
							}
						}
						return probeVolumeDebug.otherStateIndex;
					},
					setIndex = delegate(int value)
					{
						string text = ((value == 0) ? null : m_DebugScenarioNames[value].text);
						m_CurrentBakingSet.BlendLightingScenario(text, m_CurrentBakingSet.scenarioBlendingFactor);
						probeVolumeDebug.otherStateIndex = value;
					},
					getter = () => probeVolumeDebug.otherStateIndex,
					setter = delegate(int value)
					{
						probeVolumeDebug.otherStateIndex = value;
					}
				};
				container10.children.Add(m_DebugScenarioField);
				container10.children.Add(new global::UnityEngine.Rendering.DebugUI.FloatField
				{
					displayName = "Scenario Blending Factor",
					tooltip = "Blend between lighting scenarios by adjusting this slider.",
					getter = () => instance.scenarioBlendingFactor,
					setter = delegate(float value)
					{
						instance.scenarioBlendingFactor = value;
					},
					min = () => 0f,
					max = () => 1f
				});
				list.Add(container10);
			}
			if (list.Count > 0)
			{
				m_DebugItems = list.ToArray();
				global::UnityEngine.Rendering.DebugManager.instance.GetPanel(k_DebugPanelName, createIfNull: true).children.Add(m_DebugItems);
			}
			global::UnityEngine.Rendering.DebugManager.instance.RegisterData(probeVolumeDebug);
			void RefreshDebug<T>(global::UnityEngine.Rendering.DebugUI.Field<T> field, T value)
			{
				UnregisterDebug(destroyPanel: false);
				RegisterDebug();
			}
			void RefreshScenarioNames(string guid)
			{
				global::System.Collections.Generic.HashSet<string> hashSet = new global::System.Collections.Generic.HashSet<string>();
				global::UnityEngine.Rendering.ProbeVolumeBakingSet[] array = global::UnityEngine.Resources.FindObjectsOfTypeAll<global::UnityEngine.Rendering.ProbeVolumeBakingSet>();
				foreach (global::UnityEngine.Rendering.ProbeVolumeBakingSet probeVolumeBakingSet in array)
				{
					if (global::System.Linq.Enumerable.Contains(probeVolumeBakingSet.sceneGUIDs, guid))
					{
						foreach (string lightingScenario in probeVolumeBakingSet.lightingScenarios)
						{
							hashSet.Add(lightingScenario);
						}
					}
				}
				hashSet.Remove(m_CurrentBakingSet.lightingScenario);
				if (!(m_DebugActiveSceneGUID == guid) || hashSet.Count + 1 != m_DebugScenarioNames.Length || !(m_DebugActiveScenario == m_CurrentBakingSet.lightingScenario))
				{
					int num = 0;
					global::UnityEngine.Rendering.ArrayExtensions.ResizeArray(ref m_DebugScenarioNames, hashSet.Count + 1);
					global::UnityEngine.Rendering.ArrayExtensions.ResizeArray(ref m_DebugScenarioValues, hashSet.Count + 1);
					m_DebugScenarioNames[0] = new global::UnityEngine.GUIContent("None");
					m_DebugScenarioValues[0] = 0;
					foreach (string item3 in hashSet)
					{
						num++;
						m_DebugScenarioNames[num] = new global::UnityEngine.GUIContent(item3);
						m_DebugScenarioValues[num] = num;
					}
					m_DebugActiveSceneGUID = guid;
					m_DebugActiveScenario = m_CurrentBakingSet.lightingScenario;
					m_DebugScenarioField.enumNames = m_DebugScenarioNames;
					m_DebugScenarioField.enumValues = m_DebugScenarioValues;
					if (probeVolumeDebug.otherStateIndex >= m_DebugScenarioNames.Length)
					{
						probeVolumeDebug.otherStateIndex = 0;
					}
				}
			}
		}

		private void UnregisterDebug(bool destroyPanel)
		{
			if (destroyPanel)
			{
				global::UnityEngine.Rendering.DebugManager.instance.RemovePanel(k_DebugPanelName);
			}
			else
			{
				global::UnityEngine.Rendering.DebugManager.instance.GetPanel(k_DebugPanelName).children.Remove(m_DebugItems);
			}
		}

		public void RenderFragmentationOverlay(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle colorBuffer, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle depthBuffer, global::UnityEngine.Rendering.DebugOverlay debugOverlay)
		{
			if (!m_ProbeReferenceVolumeInit || !probeVolumeDebug.displayIndexFragmentation)
			{
				return;
			}
			global::UnityEngine.Rendering.ProbeReferenceVolume.RenderFragmentationOverlayPassData passData;
			using global::UnityEngine.Rendering.RenderGraphModule.IUnsafeRenderGraphBuilder unsafeRenderGraphBuilder = renderGraph.AddUnsafePass<global::UnityEngine.Rendering.ProbeReferenceVolume.RenderFragmentationOverlayPassData>("APVFragmentationOverlay", out passData, ".\\Library\\PackageCache\\com.unity.render-pipelines.core@04ab0eefa0c3\\Runtime\\Lighting\\ProbeVolume\\ProbeReferenceVolume.Debug.cs", 830);
			passData.debugOverlay = debugOverlay;
			passData.debugFragmentationMaterial = m_DebugFragmentationMaterial;
			passData.colorBuffer = colorBuffer;
			unsafeRenderGraphBuilder.SetRenderAttachment(colorBuffer, 0);
			passData.depthBuffer = depthBuffer;
			unsafeRenderGraphBuilder.SetRenderAttachmentDepth(depthBuffer, global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.ReadWrite);
			passData.debugFragmentationData = m_Index.GetDebugFragmentationBuffer();
			passData.chunkCount = passData.debugFragmentationData.count;
			unsafeRenderGraphBuilder.SetRenderFunc(delegate(global::UnityEngine.Rendering.ProbeReferenceVolume.RenderFragmentationOverlayPassData data, global::UnityEngine.Rendering.RenderGraphModule.UnsafeGraphContext ctx)
			{
				global::UnityEngine.Rendering.CommandBuffer nativeCommandBuffer = global::UnityEngine.Rendering.CommandBufferHelpers.GetNativeCommandBuffer(ctx.cmd);
				global::UnityEngine.MaterialPropertyBlock tempMaterialPropertyBlock = ctx.renderGraphPool.GetTempMaterialPropertyBlock();
				data.debugOverlay.SetViewport(nativeCommandBuffer);
				tempMaterialPropertyBlock.SetInt("_ChunkCount", data.chunkCount);
				tempMaterialPropertyBlock.SetBuffer("_DebugFragmentation", data.debugFragmentationData);
				nativeCommandBuffer.DrawProcedural(global::UnityEngine.Matrix4x4.identity, data.debugFragmentationMaterial, 0, global::UnityEngine.MeshTopology.Triangles, 3, 1, tempMaterialPropertyBlock);
				data.debugOverlay.Next();
			});
		}

		private bool ShouldCullCell(global::UnityEngine.Vector3 cellPosition, global::UnityEngine.Transform cameraTransform, global::UnityEngine.Plane[] frustumPlanes)
		{
			global::UnityEngine.Bounds cellBounds = GetCellBounds(cellPosition);
			float num = MaxBrickSize();
			float num2 = (float)global::UnityEngine.Mathf.CeilToInt(probeVolumeDebug.probeCullingDistance / num) * num;
			if (global::UnityEngine.Vector3.Distance(cameraTransform.position, cellBounds.center) > num2)
			{
				return true;
			}
			return !global::UnityEngine.GeometryUtility.TestPlanesAABB(frustumPlanes, cellBounds);
		}

		private static void UpdateDebugFromSelection(ref global::UnityEngine.Vector4[] _AdjustmentVolumeBounds, ref int _AdjustmentVolumeCount)
		{
			_ = global::UnityEngine.Rendering.ProbeVolumeDebug.s_ActiveAdjustmentVolumes;
		}

		private global::UnityEngine.Bounds GetCellBounds(global::UnityEngine.Vector3 cellPosition)
		{
			float num = MaxBrickSize();
			return new global::UnityEngine.Bounds(ProbeOffset() + global::UnityEngine.Rendering.ProbeVolumeDebug.currentOffset + cellPosition * num + global::UnityEngine.Vector3.one * (num / 2f), num * global::UnityEngine.Vector3.one);
		}

		private bool ShouldCullCell(global::UnityEngine.Vector3 cellPosition, global::UnityEngine.Vector4[] adjustmentVolumeBounds, int adjustmentVolumeCount)
		{
			global::UnityEngine.Bounds b = GetCellBounds(cellPosition);
			for (int i = 0; i < adjustmentVolumeCount; i++)
			{
				global::UnityEngine.Vector3 vector = adjustmentVolumeBounds[i * 3];
				if (adjustmentVolumeBounds[i * 3].w == float.MaxValue)
				{
					float num = adjustmentVolumeBounds[i * 3 + 1].x * 2f;
					if (new global::UnityEngine.Bounds(vector, new global::UnityEngine.Vector3(num, num, num)).Intersects(b))
					{
						return false;
					}
					continue;
				}
				global::UnityEngine.Rendering.ProbeReferenceVolume.Volume a = default(global::UnityEngine.Rendering.ProbeReferenceVolume.Volume);
				a.X = adjustmentVolumeBounds[i * 3 + 1];
				a.Y = adjustmentVolumeBounds[i * 3 + 2];
				a.Z = new global::UnityEngine.Vector3(adjustmentVolumeBounds[i * 3].w, adjustmentVolumeBounds[i * 3 + 1].w, adjustmentVolumeBounds[i * 3 + 2].w);
				a.corner = vector - a.X - a.Y - a.Z;
				a.X *= 2f;
				a.Y *= 2.05f;
				a.Z *= 2f;
				if (global::UnityEngine.Rendering.ProbeVolumePositioning.OBBAABBIntersect(in a, in b, a.CalculateAABB()))
				{
					return false;
				}
			}
			return true;
		}

		private void DrawProbeDebug(global::UnityEngine.Camera camera, global::UnityEngine.Texture exposureTexture)
		{
			if (!enabledBySRP || !isInitialized)
			{
				return;
			}
			bool flag = probeVolumeDebug.drawProbes;
			bool num = flag || probeVolumeDebug.drawVirtualOffsetPush || probeVolumeDebug.drawProbeSamplingDebug;
			int _AdjustmentVolumeCount = 0;
			global::UnityEngine.Vector4[] _AdjustmentVolumeBounds = s_BoundsArray;
			if (!num && probeVolumeDebug.autoDrawProbes)
			{
				UpdateDebugFromSelection(ref _AdjustmentVolumeBounds, ref _AdjustmentVolumeCount);
				flag = flag || _AdjustmentVolumeCount != 0;
			}
			if (!num && !flag)
			{
				return;
			}
			global::UnityEngine.GeometryUtility.CalculateFrustumPlanes(camera, m_DebugFrustumPlanes);
			m_DebugMaterial.shaderKeywords = null;
			if (m_SHBands == global::UnityEngine.Rendering.ProbeVolumeSHBands.SphericalHarmonicsL1)
			{
				m_DebugMaterial.EnableKeyword("PROBE_VOLUMES_L1");
			}
			else if (m_SHBands == global::UnityEngine.Rendering.ProbeVolumeSHBands.SphericalHarmonicsL2)
			{
				m_DebugMaterial.EnableKeyword("PROBE_VOLUMES_L2");
			}
			m_DebugMaterial.renderQueue = 3000;
			m_DebugOffsetMaterial.renderQueue = 3000;
			m_ProbeSamplingDebugMaterial.renderQueue = 3000;
			m_ProbeSamplingDebugMaterial02.renderQueue = 3000;
			m_DebugMaterial.SetVector("_DebugEmptyProbeData", global::UnityEngine.Rendering.APVDefinitions.debugEmptyColor);
			if (probeVolumeDebug.drawProbeSamplingDebug)
			{
				m_ProbeSamplingDebugMaterial.SetInt("_ShadingMode", (int)probeVolumeDebug.probeShading);
				m_ProbeSamplingDebugMaterial.SetInt("_RenderingLayerMask", (int)probeVolumeDebug.samplingRenderingLayer);
				m_ProbeSamplingDebugMaterial.SetVector("_DebugArrowColor", new global::UnityEngine.Vector4(1f, 1f, 1f, 1f));
				m_ProbeSamplingDebugMaterial.SetVector("_DebugLocator01Color", new global::UnityEngine.Vector4(1f, 1f, 1f, 1f));
				m_ProbeSamplingDebugMaterial.SetVector("_DebugLocator02Color", new global::UnityEngine.Vector4(0.3f, 0.3f, 0.3f, 1f));
				m_ProbeSamplingDebugMaterial.SetFloat("_ProbeSize", probeVolumeDebug.probeSamplingDebugSize);
				m_ProbeSamplingDebugMaterial.SetTexture("_NumbersTex", m_DisplayNumbersTexture);
				m_ProbeSamplingDebugMaterial.SetInt("_DebugSamplingNoise", global::System.Convert.ToInt32(probeVolumeDebug.debugWithSamplingNoise));
				m_ProbeSamplingDebugMaterial.SetInt("_ForceDebugNormalViewBias", 0);
				m_ProbeSamplingDebugMaterial.SetBuffer("_positionNormalBuffer", probeSamplingDebugData.positionNormalBuffer);
				global::UnityEngine.Graphics.DrawMesh(m_DebugProbeSamplingMesh, new global::UnityEngine.Vector4(0f, 0f, 0f, 1f), global::UnityEngine.Quaternion.identity, m_ProbeSamplingDebugMaterial, 0, camera);
				global::UnityEngine.Graphics.ClearRandomWriteTargets();
			}
			int num2 = ((cells.Count > 0) ? (GetMaxSubdivision() - 1) : 0);
			foreach (global::UnityEngine.Rendering.ProbeReferenceVolume.Cell value2 in cells.Values)
			{
				num2 = global::UnityEngine.Mathf.Min(num2, value2.desc.minSubdiv);
			}
			int num3 = global::UnityEngine.Mathf.Max(0, global::UnityEngine.Mathf.Min(probeVolumeDebug.maxSubdivToVisualize, GetMaxSubdivision() - 1));
			int value = global::UnityEngine.Mathf.Clamp(probeVolumeDebug.minSubdivToVisualize, num2, num3);
			m_MaxSubdivVisualizedIsMaxAvailable = num3 == GetMaxSubdivision() - 1;
			bool flag2 = flag && !probeVolumeDebug.drawProbes && probeVolumeDebug.isolationProbeDebug;
			foreach (global::UnityEngine.Rendering.ProbeReferenceVolume.Cell value3 in cells.Values)
			{
				if (ShouldCullCell(value3.desc.position, camera.transform, m_DebugFrustumPlanes) || (flag2 && ShouldCullCell(value3.desc.position, _AdjustmentVolumeBounds, _AdjustmentVolumeCount)))
				{
					continue;
				}
				global::UnityEngine.Rendering.ProbeReferenceVolume.CellInstancedDebugProbes cellInstancedDebugProbes = CreateInstancedProbes(value3);
				if (cellInstancedDebugProbes == null)
				{
					continue;
				}
				for (int i = 0; i < cellInstancedDebugProbes.probeBuffers.Count; i++)
				{
					global::UnityEngine.MaterialPropertyBlock materialPropertyBlock = cellInstancedDebugProbes.props[i];
					materialPropertyBlock.SetInt("_ShadingMode", (int)probeVolumeDebug.probeShading);
					materialPropertyBlock.SetFloat("_ExposureCompensation", probeVolumeDebug.exposureCompensation);
					materialPropertyBlock.SetFloat("_ProbeSize", probeVolumeDebug.probeSize);
					materialPropertyBlock.SetFloat("_CullDistance", probeVolumeDebug.probeCullingDistance);
					materialPropertyBlock.SetInt("_MaxAllowedSubdiv", num3);
					materialPropertyBlock.SetInt("_MinAllowedSubdiv", value);
					materialPropertyBlock.SetFloat("_ValidityThreshold", m_CurrentBakingSet.settings.dilationSettings.dilationValidityThreshold);
					materialPropertyBlock.SetInt("_RenderingLayerMask", probeVolumeDebug.visibleLayers);
					materialPropertyBlock.SetFloat("_OffsetSize", probeVolumeDebug.offsetSize);
					materialPropertyBlock.SetTexture("_ExposureTexture", exposureTexture);
					if (flag)
					{
						m_DebugMaterial.SetVectorArray("_TouchupVolumeBounds", _AdjustmentVolumeBounds);
						m_DebugMaterial.SetInt("_AdjustmentVolumeCount", probeVolumeDebug.isolationProbeDebug ? _AdjustmentVolumeCount : 0);
						m_DebugMaterial.SetVector("_ScreenSize", new global::UnityEngine.Vector4(camera.pixelWidth, camera.pixelHeight, 1f / (float)camera.pixelWidth, 1f / (float)camera.pixelHeight));
						global::UnityEngine.Matrix4x4[] array = cellInstancedDebugProbes.probeBuffers[i];
						m_DebugMaterial.SetInt("_DebugProbeVolumeSampling", 0);
						m_DebugMaterial.SetBuffer("_positionNormalBuffer", probeSamplingDebugData.positionNormalBuffer);
						global::UnityEngine.Graphics.DrawMeshInstanced(debugMesh, 0, m_DebugMaterial, array, array.Length, materialPropertyBlock, global::UnityEngine.Rendering.ShadowCastingMode.Off, receiveShadows: false, 0, camera, global::UnityEngine.Rendering.LightProbeUsage.Off, null);
					}
					if (probeVolumeDebug.drawProbeSamplingDebug)
					{
						global::UnityEngine.Matrix4x4[] array2 = cellInstancedDebugProbes.probeBuffers[i];
						m_ProbeSamplingDebugMaterial02.SetInt("_DebugProbeVolumeSampling", 1);
						materialPropertyBlock.SetInt("_ShadingMode", 0);
						materialPropertyBlock.SetFloat("_ProbeSize", probeVolumeDebug.probeSamplingDebugSize);
						materialPropertyBlock.SetInt("_DebugSamplingNoise", global::System.Convert.ToInt32(probeVolumeDebug.debugWithSamplingNoise));
						materialPropertyBlock.SetInt("_RenderingLayerMask", (int)probeVolumeDebug.samplingRenderingLayer);
						m_ProbeSamplingDebugMaterial02.SetBuffer("_positionNormalBuffer", probeSamplingDebugData.positionNormalBuffer);
						global::UnityEngine.Graphics.DrawMeshInstanced(debugMesh, 0, m_ProbeSamplingDebugMaterial02, array2, array2.Length, materialPropertyBlock, global::UnityEngine.Rendering.ShadowCastingMode.Off, receiveShadows: false, 0, camera, global::UnityEngine.Rendering.LightProbeUsage.Off, null);
					}
					if (probeVolumeDebug.drawVirtualOffsetPush)
					{
						m_DebugOffsetMaterial.SetVectorArray("_TouchupVolumeBounds", _AdjustmentVolumeBounds);
						m_DebugOffsetMaterial.SetInt("_AdjustmentVolumeCount", probeVolumeDebug.isolationProbeDebug ? _AdjustmentVolumeCount : 0);
						global::UnityEngine.Matrix4x4[] array3 = cellInstancedDebugProbes.offsetBuffers[i];
						global::UnityEngine.Graphics.DrawMeshInstanced(m_DebugOffsetMesh, 0, m_DebugOffsetMaterial, array3, array3.Length, materialPropertyBlock, global::UnityEngine.Rendering.ShadowCastingMode.Off, receiveShadows: false, 0, camera, global::UnityEngine.Rendering.LightProbeUsage.Off, null);
					}
				}
			}
		}

		internal void ResetDebugViewToMaxSubdiv()
		{
			if (m_MaxSubdivVisualizedIsMaxAvailable)
			{
				probeVolumeDebug.maxSubdivToVisualize = GetMaxSubdivision() - 1;
			}
		}

		private void ClearDebugData()
		{
			realtimeSubdivisionInfo.Clear();
		}

		private static void DecompressSH(ref global::UnityEngine.Rendering.SphericalHarmonicsL2 shv)
		{
			for (int i = 0; i < 3; i++)
			{
				float num = shv[i, 0];
				float num2 = 2f;
				float num3 = 3.5777087f;
				shv[i, 1] = (shv[i, 1] - 0.5f) * (num * num2 * 2f);
				shv[i, 2] = (shv[i, 2] - 0.5f) * (num * num2 * 2f);
				shv[i, 3] = (shv[i, 3] - 0.5f) * (num * num2 * 2f);
				shv[i, 4] = (shv[i, 4] - 0.5f) * (num * num3 * 2f);
				shv[i, 5] = (shv[i, 5] - 0.5f) * (num * num3 * 2f);
				shv[i, 6] = (shv[i, 6] - 0.5f) * (num * num3 * 2f);
				shv[i, 7] = (shv[i, 7] - 0.5f) * (num * num3 * 2f);
				shv[i, 8] = (shv[i, 8] - 0.5f) * (num * num3 * 2f);
			}
		}

		internal static global::UnityEngine.Vector3 DecodeSkyShadingDirection(uint directionIndex)
		{
			global::UnityEngine.Vector3[] skySamplingDirections = global::UnityEngine.Rendering.ProbeVolumeConstantRuntimeResources.GetSkySamplingDirections();
			if (directionIndex != skySamplingDirections.Length)
			{
				return skySamplingDirections[directionIndex];
			}
			return new global::UnityEngine.Vector3(0f, 0f, 0f);
		}

		internal bool GetFlattenedProbeData(string scenario, out global::UnityEngine.Vector3[] positions, out global::UnityEngine.Rendering.SphericalHarmonicsL2[] irradiance, out float[] validity, out global::UnityEngine.Vector4[] occlusion, out global::UnityEngine.Vector4[] skyOcclusion, out global::UnityEngine.Vector3[] skyOcclusionDirections, out global::UnityEngine.Vector3[] virtualOffset)
		{
			positions = null;
			irradiance = null;
			validity = null;
			occlusion = null;
			skyOcclusion = null;
			skyOcclusionDirections = null;
			virtualOffset = null;
			global::System.Collections.Generic.List<global::UnityEngine.Vector3> list = new global::System.Collections.Generic.List<global::UnityEngine.Vector3>();
			global::System.Collections.Generic.List<global::UnityEngine.Rendering.SphericalHarmonicsL2> list2 = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.SphericalHarmonicsL2>();
			global::System.Collections.Generic.List<float> list3 = new global::System.Collections.Generic.List<float>();
			global::System.Collections.Generic.List<global::UnityEngine.Vector4> list4 = new global::System.Collections.Generic.List<global::UnityEngine.Vector4>();
			global::System.Collections.Generic.List<global::UnityEngine.Vector4> list5 = new global::System.Collections.Generic.List<global::UnityEngine.Vector4>();
			global::System.Collections.Generic.List<global::UnityEngine.Vector3> list6 = new global::System.Collections.Generic.List<global::UnityEngine.Vector3>();
			global::System.Collections.Generic.List<global::UnityEngine.Vector3> list7 = new global::System.Collections.Generic.List<global::UnityEngine.Vector3>();
			foreach (global::UnityEngine.Rendering.ProbeReferenceVolume.Cell value2 in cells.Values)
			{
				if (HasActiveStreamingRequest(value2))
				{
					return false;
				}
				if (!value2.data.bricks.IsCreated || value2.data.bricks.Length == 0 || !value2.data.probePositions.IsCreated || !value2.loaded)
				{
					return false;
				}
				if (!value2.data.scenarios.TryGetValue(scenario, out var value))
				{
					return false;
				}
				global::System.Collections.Generic.List<global::UnityEngine.Rendering.ProbeBrickPool.BrickChunkAlloc> chunkList = value2.poolInfo.chunkList;
				int chunkSizeInProbeCount = global::UnityEngine.Rendering.ProbeBrickPool.GetChunkSizeInProbeCount();
				global::UnityEngine.Vector3Int vector3Int = global::UnityEngine.Rendering.ProbeBrickPool.ProbeCountToDataLocSize(chunkSizeInProbeCount);
				int num = value2.desc.probeCount / 64;
				int num2 = 0;
				int num3 = 0;
				int num4 = 0;
				for (int i = 0; i < num; i++)
				{
					int num5 = i / global::UnityEngine.Rendering.ProbeBrickPool.GetChunkSizeInBrickCount();
					global::UnityEngine.Rendering.ProbeBrickPool.BrickChunkAlloc brickChunkAlloc = chunkList[num5];
					global::UnityEngine.Vector3Int vector3Int2 = new global::UnityEngine.Vector3Int(brickChunkAlloc.x + num2, brickChunkAlloc.y + num3, brickChunkAlloc.z + num4);
					for (int j = 0; j < 4; j++)
					{
						for (int k = 0; k < 4; k++)
						{
							for (int l = 0; l < 4; l++)
							{
								new global::UnityEngine.Vector3Int(vector3Int2.x + l, vector3Int2.y + k, vector3Int2.z + j);
								int num6 = num5 * chunkSizeInProbeCount + (num2 + l) + vector3Int.x * (num3 + k + vector3Int.y * (num4 + j));
								global::UnityEngine.Vector3 item = value2.data.probePositions[num6] - ProbeOffset();
								list.Add(item);
								list3.Add(value2.data.validity[num6]);
								int num7 = num6 * 4;
								if (value.probeOcclusion.Length != 0)
								{
									float x = (float)(int)value.probeOcclusion[num7] / 255f;
									float y = (float)(int)value.probeOcclusion[num7 + 1] / 255f;
									float z = (float)(int)value.probeOcclusion[num7 + 2] / 255f;
									float w = (float)(int)value.probeOcclusion[num7 + 3] / 255f;
									list4.Add(new global::UnityEngine.Vector4(x, y, z, w));
								}
								if (value2.data.skyOcclusionDataL0L1.Length > 0)
								{
									float x2 = global::UnityEngine.Mathf.HalfToFloat(value2.data.skyOcclusionDataL0L1[num6 * 4]);
									float y2 = global::UnityEngine.Mathf.HalfToFloat(value2.data.skyOcclusionDataL0L1[num6 * 4 + 1]);
									float z2 = global::UnityEngine.Mathf.HalfToFloat(value2.data.skyOcclusionDataL0L1[num6 * 4 + 2]);
									float w2 = global::UnityEngine.Mathf.HalfToFloat(value2.data.skyOcclusionDataL0L1[num6 * 4 + 3]);
									list5.Add(new global::UnityEngine.Vector4(x2, y2, z2, w2));
								}
								if (value2.data.skyShadingDirectionIndices.Length > 0)
								{
									global::UnityEngine.Vector3 item2 = DecodeSkyShadingDirection(value2.data.skyShadingDirectionIndices[num6]);
									list6.Add(item2);
								}
								if (value2.data.offsetVectors.Length > 0)
								{
									global::UnityEngine.Vector3 item3 = value2.data.offsetVectors[num6];
									list7.Add(item3);
								}
								global::UnityEngine.Vector4 zero = global::UnityEngine.Vector4.zero;
								global::UnityEngine.Vector4 zero2 = global::UnityEngine.Vector4.zero;
								global::UnityEngine.Vector4 zero3 = global::UnityEngine.Vector4.zero;
								global::UnityEngine.Vector4 zero4 = global::UnityEngine.Vector4.zero;
								global::UnityEngine.Vector4 zero5 = global::UnityEngine.Vector4.zero;
								global::UnityEngine.Vector4 zero6 = global::UnityEngine.Vector4.zero;
								global::UnityEngine.Vector4 zero7 = global::UnityEngine.Vector4.zero;
								for (int m = 0; m < 4; m++)
								{
									zero[m] = global::UnityEngine.Mathf.HalfToFloat(value.shL0L1RxData[num6 * 4 + m]);
									zero2[m] = (float)(int)value.shL1GL1RyData[num6 * 4 + m] / 255f;
									zero3[m] = (float)(int)value.shL1BL1RzData[num6 * 4 + m] / 255f;
									if (instance.shBands == global::UnityEngine.Rendering.ProbeVolumeSHBands.SphericalHarmonicsL2)
									{
										zero4[m] = (float)(int)value.shL2Data_0[num6 * 4 + m] / 255f;
										zero5[m] = (float)(int)value.shL2Data_1[num6 * 4 + m] / 255f;
										zero6[m] = (float)(int)value.shL2Data_2[num6 * 4 + m] / 255f;
										zero7[m] = (float)(int)value.shL2Data_3[num6 * 4 + m] / 255f;
									}
								}
								global::UnityEngine.Vector3 vector = new global::UnityEngine.Vector3(zero.x, zero.y, zero.z);
								global::UnityEngine.Vector3 vector2 = new global::UnityEngine.Vector3(zero2.w, zero3.w, zero.w);
								global::UnityEngine.Vector3 vector3 = new global::UnityEngine.Vector3(zero2.y, zero2.z, zero2.x);
								global::UnityEngine.Vector3 vector4 = new global::UnityEngine.Vector3(zero3.y, zero3.z, zero3.x);
								global::UnityEngine.Rendering.SphericalHarmonicsL2 shv = default(global::UnityEngine.Rendering.SphericalHarmonicsL2);
								for (int n = 0; n < 3; n++)
								{
									shv[n, 0] = vector[n];
									shv[0, n + 1] = vector2[n];
									shv[1, n + 1] = vector3[n];
									shv[2, n + 1] = vector4[n];
								}
								shv[0, 4] = zero4.x;
								shv[0, 5] = zero4.y;
								shv[0, 6] = zero4.z;
								shv[0, 7] = zero4.w;
								shv[0, 8] = zero7.x;
								shv[1, 4] = zero5.x;
								shv[1, 5] = zero5.y;
								shv[1, 6] = zero5.z;
								shv[1, 7] = zero5.w;
								shv[1, 8] = zero7.y;
								shv[2, 4] = zero6.x;
								shv[2, 5] = zero6.y;
								shv[2, 6] = zero6.z;
								shv[2, 7] = zero6.w;
								shv[2, 8] = zero7.z;
								DecompressSH(ref shv);
								if (instance.shBands != global::UnityEngine.Rendering.ProbeVolumeSHBands.SphericalHarmonicsL2)
								{
									for (int num8 = 0; num8 < 5; num8++)
									{
										shv[0, num8 + 4] = 0f;
										shv[1, num8 + 4] = 0f;
										shv[2, num8 + 4] = 0f;
									}
								}
								list2.Add(shv);
							}
						}
					}
					num2 += 4;
					if (num2 < vector3Int.x)
					{
						continue;
					}
					num2 = 0;
					num3 += 4;
					if (num3 >= vector3Int.y)
					{
						num3 = 0;
						num4 += 4;
						if (num4 >= vector3Int.z)
						{
							num2 = 0;
							num3 = 0;
							num4 = 0;
						}
					}
				}
			}
			positions = list.ToArray();
			irradiance = list2.ToArray();
			validity = list3.ToArray();
			occlusion = list4.ToArray();
			skyOcclusion = list5.ToArray();
			skyOcclusionDirections = list6.ToArray();
			virtualOffset = list7.ToArray();
			return true;
		}

		private global::UnityEngine.Rendering.ProbeReferenceVolume.CellInstancedDebugProbes CreateInstancedProbes(global::UnityEngine.Rendering.ProbeReferenceVolume.Cell cell)
		{
			if (cell.debugProbes != null)
			{
				return cell.debugProbes;
			}
			if (HasActiveStreamingRequest(cell))
			{
				return null;
			}
			int num = GetMaxSubdivision() - 1;
			if (!cell.data.bricks.IsCreated || cell.data.bricks.Length == 0 || !cell.data.probePositions.IsCreated || !cell.loaded)
			{
				return null;
			}
			global::System.Collections.Generic.List<global::UnityEngine.Matrix4x4[]> list = new global::System.Collections.Generic.List<global::UnityEngine.Matrix4x4[]>();
			global::System.Collections.Generic.List<global::UnityEngine.Matrix4x4[]> list2 = new global::System.Collections.Generic.List<global::UnityEngine.Matrix4x4[]>();
			global::System.Collections.Generic.List<global::UnityEngine.MaterialPropertyBlock> list3 = new global::System.Collections.Generic.List<global::UnityEngine.MaterialPropertyBlock>();
			global::System.Collections.Generic.List<global::UnityEngine.Rendering.ProbeBrickPool.BrickChunkAlloc> chunkList = cell.poolInfo.chunkList;
			global::UnityEngine.Vector4[] array = new global::UnityEngine.Vector4[511];
			float[] array2 = new float[511];
			float[] array3 = new float[511];
			float[] array4 = new float[511];
			float[] array5 = new float[511];
			float[] array6 = ((cell.data.touchupVolumeInteraction.Length > 0) ? new float[511] : null);
			global::UnityEngine.Vector4[] array7 = ((cell.data.offsetVectors.Length > 0) ? new global::UnityEngine.Vector4[511] : null);
			global::System.Collections.Generic.List<global::UnityEngine.Matrix4x4> list4 = new global::System.Collections.Generic.List<global::UnityEngine.Matrix4x4>();
			global::System.Collections.Generic.List<global::UnityEngine.Matrix4x4> list5 = new global::System.Collections.Generic.List<global::UnityEngine.Matrix4x4>();
			global::UnityEngine.Rendering.ProbeReferenceVolume.CellInstancedDebugProbes cellInstancedDebugProbes = new global::UnityEngine.Rendering.ProbeReferenceVolume.CellInstancedDebugProbes();
			cellInstancedDebugProbes.probeBuffers = list;
			cellInstancedDebugProbes.offsetBuffers = list2;
			cellInstancedDebugProbes.props = list3;
			int chunkSizeInProbeCount = global::UnityEngine.Rendering.ProbeBrickPool.GetChunkSizeInProbeCount();
			global::UnityEngine.Vector3Int vector3Int = global::UnityEngine.Rendering.ProbeBrickPool.ProbeCountToDataLocSize(chunkSizeInProbeCount);
			float dilationValidityThreshold = m_CurrentBakingSet.settings.dilationSettings.dilationValidityThreshold;
			int num2 = 0;
			int num3 = 0;
			int num4 = cell.desc.probeCount / 64;
			int num5 = 0;
			int num6 = 0;
			int num7 = 0;
			for (int i = 0; i < num4; i++)
			{
				int subdivisionLevel = cell.data.bricks[i].subdivisionLevel;
				int num8 = i / global::UnityEngine.Rendering.ProbeBrickPool.GetChunkSizeInBrickCount();
				global::UnityEngine.Rendering.ProbeBrickPool.BrickChunkAlloc brickChunkAlloc = chunkList[num8];
				global::UnityEngine.Vector3Int vector3Int2 = new global::UnityEngine.Vector3Int(brickChunkAlloc.x + num5, brickChunkAlloc.y + num6, brickChunkAlloc.z + num7);
				for (int j = 0; j < 4; j++)
				{
					for (int k = 0; k < 4; k++)
					{
						for (int l = 0; l < 4; l++)
						{
							global::UnityEngine.Vector3Int vector3Int3 = new global::UnityEngine.Vector3Int(vector3Int2.x + l, vector3Int2.y + k, vector3Int2.z + j);
							int index = num8 * chunkSizeInProbeCount + (num5 + l) + vector3Int.x * (num6 + k + vector3Int.y * (num7 + j));
							global::UnityEngine.Vector3 vector = cell.data.probePositions[index] - ProbeOffset();
							list4.Add(global::UnityEngine.Matrix4x4.TRS(vector, global::UnityEngine.Quaternion.identity, global::UnityEngine.Vector3.one * (0.3f * (float)(subdivisionLevel + 1))));
							array3[num2] = cell.data.validity[index];
							array4[num2] = dilationValidityThreshold;
							array[num2] = new global::UnityEngine.Vector4(vector3Int3.x, vector3Int3.y, vector3Int3.z, subdivisionLevel);
							array5[num2] = (float)subdivisionLevel / (float)num;
							array2[num2] = global::Unity.Mathematics.math.asfloat((cell.data.layer.Length > 0) ? cell.data.layer[index] : uint.MaxValue);
							if (array6 != null)
							{
								array6[num2] = cell.data.touchupVolumeInteraction[index];
								array4[num2] = ((array6[num2] > 1f) ? (array6[num2] - 1f) : dilationValidityThreshold);
							}
							if (array7 != null)
							{
								global::UnityEngine.Vector3 vector2 = cell.data.offsetVectors[index];
								array7[num2] = vector2;
								if (vector2.sqrMagnitude < 1E-06f)
								{
									list5.Add(global::UnityEngine.Matrix4x4.identity);
								}
								else
								{
									global::UnityEngine.Quaternion q = global::UnityEngine.Quaternion.LookRotation(-vector2);
									list5.Add(global::UnityEngine.Matrix4x4.TRS(s: new global::UnityEngine.Vector3(0.5f, 0.5f, vector2.magnitude), pos: vector + vector2, q: q));
								}
							}
							num2++;
							if (list4.Count >= 511 || num3 == cell.desc.probeCount - 1)
							{
								num2 = 0;
								global::UnityEngine.MaterialPropertyBlock materialPropertyBlock = new global::UnityEngine.MaterialPropertyBlock();
								materialPropertyBlock.SetFloatArray("_Validity", array3);
								materialPropertyBlock.SetFloatArray("_RenderingLayer", array2);
								materialPropertyBlock.SetFloatArray("_DilationThreshold", array4);
								materialPropertyBlock.SetFloatArray("_TouchupedByVolume", array6);
								materialPropertyBlock.SetFloatArray("_RelativeSize", array5);
								materialPropertyBlock.SetVectorArray("_IndexInAtlas", array);
								if (array7 != null)
								{
									materialPropertyBlock.SetVectorArray("_Offset", array7);
								}
								list3.Add(materialPropertyBlock);
								list.Add(list4.ToArray());
								list4.Clear();
								list2.Add(list5.ToArray());
								list5.Clear();
							}
							num3++;
						}
					}
				}
				num5 += 4;
				if (num5 < vector3Int.x)
				{
					continue;
				}
				num5 = 0;
				num6 += 4;
				if (num6 >= vector3Int.y)
				{
					num6 = 0;
					num7 += 4;
					if (num7 >= vector3Int.z)
					{
						num5 = 0;
						num6 = 0;
						num7 = 0;
					}
				}
			}
			cell.debugProbes = cellInstancedDebugProbes;
			return cellInstancedDebugProbes;
		}

		private void OnClearLightingdata()
		{
			ClearDebugData();
		}

		public void EnableMaxCellStreaming(bool value)
		{
			m_LoadMaxCellsPerFrame = value;
		}

		public void SetNumberOfCellsLoadedPerFrame(int numberOfCells)
		{
			m_NumberOfCellsLoadedPerFrame = global::UnityEngine.Mathf.Min(10, global::UnityEngine.Mathf.Max(1, numberOfCells));
		}

		private void InitStreaming()
		{
			m_OnStreamingComplete = OnStreamingComplete;
			m_OnBlendingStreamingComplete = OnBlendingStreamingComplete;
		}

		private void CleanupStreaming()
		{
			ProcessNewRequests();
			UpdateActiveRequests(null);
			for (int i = 0; i < m_StreamingRequestsPool.countAll; i++)
			{
				m_StreamingRequestsPool.Get().Dispose();
			}
			if (m_ScratchBufferPool != null)
			{
				m_ScratchBufferPool.Cleanup();
				m_ScratchBufferPool = null;
			}
			m_StreamingRequestsPool = new global::UnityEngine.Rendering.ObjectPool<global::UnityEngine.Rendering.ProbeReferenceVolume.CellStreamingRequest>(delegate(global::UnityEngine.Rendering.ProbeReferenceVolume.CellStreamingRequest val)
			{
				val.Clear();
			}, null);
			m_ActiveStreamingRequests.Clear();
			m_StreamingQueue.Clear();
			m_OnStreamingComplete = null;
			m_OnBlendingStreamingComplete = null;
		}

		internal void ScenarioBlendingChanged(bool scenarioChanged)
		{
			if (scenarioChanged)
			{
				UnloadAllBlendingCells();
				for (int i = 0; i < m_ToBeLoadedBlendingCells.size; i++)
				{
					m_ToBeLoadedBlendingCells[i].blendingInfo.ForceReupload();
				}
			}
		}

		private static void ComputeCellStreamingScore(global::UnityEngine.Rendering.ProbeReferenceVolume.Cell cell, global::UnityEngine.Vector3 cameraPosition, global::UnityEngine.Vector3 cameraDirection)
		{
			global::UnityEngine.Vector3 normalized = (cell.desc.position - cameraPosition).normalized;
			cell.streamingInfo.streamingScore = global::UnityEngine.Vector3.Distance(cameraPosition, cell.desc.position);
			cell.streamingInfo.streamingScore *= 2f - global::UnityEngine.Vector3.Dot(cameraDirection, normalized);
		}

		private void ComputeStreamingScore(global::UnityEngine.Vector3 cameraPosition, global::UnityEngine.Vector3 cameraDirection, global::UnityEngine.Rendering.DynamicArray<global::UnityEngine.Rendering.ProbeReferenceVolume.Cell> cells)
		{
			for (int i = 0; i < cells.size; i++)
			{
				ComputeCellStreamingScore(cells[i], cameraPosition, cameraDirection);
			}
		}

		private void ComputeBestToBeLoadedCells(global::UnityEngine.Vector3 cameraPosition, global::UnityEngine.Vector3 cameraDirection)
		{
			m_BestToBeLoadedCells.Clear();
			m_BestToBeLoadedCells.Reserve(m_ToBeLoadedCells.size);
			global::UnityEngine.Rendering.DynamicArray<global::UnityEngine.Rendering.ProbeReferenceVolume.Cell>.Iterator enumerator = m_ToBeLoadedCells.GetEnumerator();
			while (enumerator.MoveNext())
			{
				global::UnityEngine.Rendering.ProbeReferenceVolume.Cell current = enumerator.Current;
				ComputeCellStreamingScore(current, cameraPosition, cameraDirection);
				minStreamingScore = global::UnityEngine.Mathf.Min(minStreamingScore, current.streamingInfo.streamingScore);
				maxStreamingScore = global::UnityEngine.Mathf.Max(maxStreamingScore, current.streamingInfo.streamingScore);
				int num = global::System.Math.Min(m_BestToBeLoadedCells.size, numberOfCellsLoadedPerFrame);
				int i;
				for (i = 0; i < num && !(current.streamingInfo.streamingScore < m_BestToBeLoadedCells[i].streamingInfo.streamingScore); i++)
				{
				}
				if (i < numberOfCellsLoadedPerFrame)
				{
					m_BestToBeLoadedCells.Insert(i, current);
				}
				if (m_BestToBeLoadedCells.size > numberOfCellsLoadedPerFrame)
				{
					m_BestToBeLoadedCells.Resize(numberOfCellsLoadedPerFrame);
				}
			}
		}

		private void ComputeStreamingScoreAndWorseLoadedCells(global::UnityEngine.Vector3 cameraPosition, global::UnityEngine.Vector3 cameraDirection)
		{
			m_WorseLoadedCells.Clear();
			m_WorseLoadedCells.Reserve(m_LoadedCells.size);
			int num = 0;
			int num2 = 0;
			global::UnityEngine.Rendering.DynamicArray<global::UnityEngine.Rendering.ProbeReferenceVolume.Cell>.Iterator enumerator = m_BestToBeLoadedCells.GetEnumerator();
			while (enumerator.MoveNext())
			{
				global::UnityEngine.Rendering.ProbeReferenceVolume.Cell current = enumerator.Current;
				num += current.desc.shChunkCount;
				num2 += current.desc.indexChunkCount;
			}
			enumerator = m_LoadedCells.GetEnumerator();
			while (enumerator.MoveNext())
			{
				global::UnityEngine.Rendering.ProbeReferenceVolume.Cell current2 = enumerator.Current;
				ComputeCellStreamingScore(current2, cameraPosition, cameraDirection);
				minStreamingScore = global::UnityEngine.Mathf.Min(minStreamingScore, current2.streamingInfo.streamingScore);
				maxStreamingScore = global::UnityEngine.Mathf.Max(maxStreamingScore, current2.streamingInfo.streamingScore);
				int size = m_WorseLoadedCells.size;
				int i;
				for (i = 0; i < size && !(current2.streamingInfo.streamingScore > m_WorseLoadedCells[i].streamingInfo.streamingScore); i++)
				{
				}
				m_WorseLoadedCells.Insert(i, current2);
				int num3 = 0;
				int num4 = 0;
				int num5 = 0;
				for (int j = 0; j < m_WorseLoadedCells.size; j++)
				{
					global::UnityEngine.Rendering.ProbeReferenceVolume.Cell cell = m_WorseLoadedCells[j];
					num3 += cell.desc.shChunkCount;
					num4 += cell.desc.indexChunkCount;
					if (num3 >= num && num4 >= num2)
					{
						num5 = j + 1;
						break;
					}
				}
				if (num5 != 0)
				{
					m_WorseLoadedCells.Resize(num5);
				}
			}
		}

		private void ComputeBlendingScore(global::UnityEngine.Rendering.DynamicArray<global::UnityEngine.Rendering.ProbeReferenceVolume.Cell> cells, float worstScore)
		{
			float num = scenarioBlendingFactor;
			for (int i = 0; i < cells.size; i++)
			{
				global::UnityEngine.Rendering.ProbeReferenceVolume.Cell cell = cells[i];
				global::UnityEngine.Rendering.ProbeReferenceVolume.CellBlendingInfo blendingInfo = cell.blendingInfo;
				if (num != blendingInfo.blendingFactor)
				{
					blendingInfo.blendingScore = cell.streamingInfo.streamingScore;
					if (blendingInfo.ShouldPrioritize())
					{
						blendingInfo.blendingScore -= worstScore;
					}
				}
			}
		}

		private bool TryLoadCell(global::UnityEngine.Rendering.ProbeReferenceVolume.Cell cell, ref int shBudget, ref int indexBudget, global::UnityEngine.Rendering.DynamicArray<global::UnityEngine.Rendering.ProbeReferenceVolume.Cell> loadedCells)
		{
			if (cell.poolInfo.shChunkCount <= shBudget && cell.indexInfo.indexChunkCount <= indexBudget && LoadCell(cell, ignoreErrorLog: true))
			{
				loadedCells.Add(in cell);
				shBudget -= cell.poolInfo.shChunkCount;
				indexBudget -= cell.indexInfo.indexChunkCount;
				return true;
			}
			return false;
		}

		private void UnloadBlendingCell(global::UnityEngine.Rendering.ProbeReferenceVolume.Cell cell, global::UnityEngine.Rendering.DynamicArray<global::UnityEngine.Rendering.ProbeReferenceVolume.Cell> unloadedCells)
		{
			UnloadBlendingCell(cell);
			unloadedCells.Add(in cell);
		}

		private bool TryLoadBlendingCell(global::UnityEngine.Rendering.ProbeReferenceVolume.Cell cell, global::UnityEngine.Rendering.DynamicArray<global::UnityEngine.Rendering.ProbeReferenceVolume.Cell> loadedCells)
		{
			if (!cell.UpdateCellScenarioData(lightingScenario, m_CurrentBakingSet.otherScenario))
			{
				return false;
			}
			if (!AddBlendingBricks(cell))
			{
				return false;
			}
			loadedCells.Add(in cell);
			return true;
		}

		private void ComputeMinMaxStreamingScore()
		{
			minStreamingScore = float.MaxValue;
			maxStreamingScore = float.MinValue;
			if (m_ToBeLoadedCells.size != 0)
			{
				minStreamingScore = global::UnityEngine.Mathf.Min(minStreamingScore, m_ToBeLoadedCells[0].streamingInfo.streamingScore);
				maxStreamingScore = global::UnityEngine.Mathf.Max(maxStreamingScore, m_ToBeLoadedCells[m_ToBeLoadedCells.size - 1].streamingInfo.streamingScore);
			}
			if (m_LoadedCells.size != 0)
			{
				minStreamingScore = global::UnityEngine.Mathf.Min(minStreamingScore, m_LoadedCells[0].streamingInfo.streamingScore);
				maxStreamingScore = global::UnityEngine.Mathf.Max(maxStreamingScore, m_LoadedCells[m_LoadedCells.size - 1].streamingInfo.streamingScore);
			}
		}

		public void UpdateCellStreaming(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Camera camera)
		{
			UpdateCellStreaming(cmd, camera, null);
		}

		public void UpdateCellStreaming(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Camera camera, global::UnityEngine.Rendering.ProbeVolumesOptions options)
		{
			if (!isInitialized || m_CurrentBakingSet == null)
			{
				return;
			}
			using (new global::UnityEngine.Rendering.ProfilingScope(global::UnityEngine.Rendering.ProfilingSampler.Get(global::UnityEngine.Rendering.CoreProfileId.APVCellStreamingUpdate)))
			{
				global::UnityEngine.Vector3 position = camera.transform.position;
				if (!probeVolumeDebug.freezeStreaming)
				{
					m_FrozenCameraPosition = position;
					m_FrozenCameraDirection = camera.transform.forward;
				}
				global::UnityEngine.Vector3 vector = ProbeOffset() + ((options != null) ? options.worldOffset.value : global::UnityEngine.Vector3.zero);
				global::UnityEngine.Vector3 cameraPosition = (m_FrozenCameraPosition - vector) / MaxBrickSize() - global::UnityEngine.Vector3.one * 0.5f;
				global::UnityEngine.Rendering.DynamicArray<global::UnityEngine.Rendering.ProbeReferenceVolume.Cell> dynamicArray;
				if (m_LoadMaxCellsPerFrame)
				{
					ComputeStreamingScore(cameraPosition, m_FrozenCameraDirection, m_ToBeLoadedCells);
					m_ToBeLoadedCells.QuickSort();
					dynamicArray = m_ToBeLoadedCells;
				}
				else
				{
					minStreamingScore = float.MaxValue;
					maxStreamingScore = float.MinValue;
					ComputeBestToBeLoadedCells(cameraPosition, m_FrozenCameraDirection);
					dynamicArray = m_BestToBeLoadedCells;
				}
				int indexBudget = m_Index.GetRemainingChunkCount();
				int shBudget = m_Pool.GetRemainingChunkCount();
				int num = global::UnityEngine.Mathf.Min(numberOfCellsLoadedPerFrame, dynamicArray.size);
				bool flag = false;
				if (m_SupportGPUStreaming)
				{
					if (m_IndexDefragmentationInProgress)
					{
						UpdateIndexDefragmentation();
					}
					else
					{
						bool flag2 = false;
						while (m_TempCellToLoadList.size < num)
						{
							global::UnityEngine.Rendering.ProbeReferenceVolume.Cell cell = dynamicArray[m_TempCellToLoadList.size];
							if (!TryLoadCell(cell, ref shBudget, ref indexBudget, m_TempCellToLoadList))
							{
								break;
							}
						}
						if (m_TempCellToLoadList.size != num && !m_IndexDefragmentationInProgress)
						{
							global::UnityEngine.Rendering.DynamicArray<global::UnityEngine.Rendering.ProbeReferenceVolume.Cell> dynamicArray2;
							if (m_LoadMaxCellsPerFrame)
							{
								ComputeStreamingScore(cameraPosition, m_FrozenCameraDirection, m_LoadedCells);
								m_LoadedCells.QuickSort();
								dynamicArray2 = m_LoadedCells;
							}
							else
							{
								ComputeStreamingScoreAndWorseLoadedCells(cameraPosition, m_FrozenCameraDirection);
								dynamicArray2 = m_WorseLoadedCells;
							}
							flag = true;
							int num2 = 0;
							while (m_TempCellToLoadList.size < num && dynamicArray2.size - num2 != 0)
							{
								int index = (m_LoadMaxCellsPerFrame ? (dynamicArray2.size - num2 - 1) : num2);
								global::UnityEngine.Rendering.ProbeReferenceVolume.Cell value = dynamicArray2[index];
								global::UnityEngine.Rendering.ProbeReferenceVolume.Cell cell2 = dynamicArray[m_TempCellToLoadList.size];
								if (value.streamingInfo.streamingScore <= cell2.streamingInfo.streamingScore)
								{
									break;
								}
								while (num2 < dynamicArray2.size && value.streamingInfo.streamingScore > cell2.streamingInfo.streamingScore && (shBudget < cell2.desc.shChunkCount || indexBudget < cell2.desc.indexChunkCount))
								{
									_ = probeVolumeDebug.verboseStreamingLog;
									num2++;
									UnloadCell(value);
									shBudget += value.desc.shChunkCount;
									indexBudget += value.desc.indexChunkCount;
									m_TempCellToUnloadList.Add(in value);
									index = (m_LoadMaxCellsPerFrame ? (dynamicArray2.size - num2 - 1) : num2);
									if (num2 < dynamicArray2.size)
									{
										value = dynamicArray2[index];
									}
								}
								if (shBudget >= cell2.desc.shChunkCount && indexBudget >= cell2.desc.indexChunkCount && !TryLoadCell(cell2, ref shBudget, ref indexBudget, m_TempCellToLoadList))
								{
									flag2 = true;
									break;
								}
							}
						}
						if (flag2)
						{
							m_Index.ComputeFragmentationRate();
						}
						if (m_Index.fragmentationRate >= 0.2f)
						{
							StartIndexDefragmentation();
						}
					}
				}
				else
				{
					for (int i = 0; i < num; i++)
					{
						global::UnityEngine.Rendering.ProbeReferenceVolume.Cell cell3 = m_ToBeLoadedCells[m_TempCellToLoadList.size];
						if (!TryLoadCell(cell3, ref shBudget, ref indexBudget, m_TempCellToLoadList))
						{
							if (i > 0)
							{
								global::UnityEngine.Debug.LogWarning("Max Memory Budget for Adaptive Probe Volumes has been reached, but there is still more data to load. Consider either increasing the Memory Budget, enabling GPU Streaming, or reducing the probe count.");
							}
							break;
						}
					}
				}
				if (!flag && supportScenarioBlending)
				{
					ComputeStreamingScore(cameraPosition, m_FrozenCameraDirection, m_LoadedCells);
				}
				if (m_LoadMaxCellsPerFrame)
				{
					ComputeMinMaxStreamingScore();
				}
				global::UnityEngine.Rendering.DynamicArray<global::UnityEngine.Rendering.ProbeReferenceVolume.Cell>.Iterator enumerator = m_TempCellToLoadList.GetEnumerator();
				while (enumerator.MoveNext())
				{
					global::UnityEngine.Rendering.ProbeReferenceVolume.Cell current = enumerator.Current;
					m_ToBeLoadedCells.Remove(current);
				}
				m_LoadedCells.AddRange(m_TempCellToLoadList);
				if (m_TempCellToUnloadList.size > 0)
				{
					enumerator = m_TempCellToUnloadList.GetEnumerator();
					while (enumerator.MoveNext())
					{
						global::UnityEngine.Rendering.ProbeReferenceVolume.Cell current2 = enumerator.Current;
						m_LoadedCells.Remove(current2);
					}
					ComputeCellGlobalInfo();
				}
				m_ToBeLoadedCells.AddRange(m_TempCellToUnloadList);
				m_TempCellToLoadList.Clear();
				m_TempCellToUnloadList.Clear();
				UpdateDiskStreaming(cmd);
			}
			if (!supportScenarioBlending)
			{
				return;
			}
			using (new global::UnityEngine.Rendering.ProfilingScope(cmd, global::UnityEngine.Rendering.ProfilingSampler.Get(global::UnityEngine.Rendering.CoreProfileId.APVScenarioBlendingUpdate)))
			{
				UpdateBlendingCellStreaming(cmd);
			}
		}

		private int FindWorstBlendingCellToBeLoaded()
		{
			int result = -1;
			float num = -1f;
			float num2 = scenarioBlendingFactor;
			for (int i = m_TempBlendingCellToLoadList.size; i < m_ToBeLoadedBlendingCells.size; i++)
			{
				float num3 = global::UnityEngine.Mathf.Abs(m_ToBeLoadedBlendingCells[i].blendingInfo.blendingFactor - num2);
				if (num3 > num)
				{
					result = i;
					if (m_ToBeLoadedBlendingCells[i].blendingInfo.ShouldReupload())
					{
						break;
					}
					num = num3;
				}
			}
			return result;
		}

		private static int BlendingComparer(global::UnityEngine.Rendering.ProbeReferenceVolume.Cell a, global::UnityEngine.Rendering.ProbeReferenceVolume.Cell b)
		{
			if (a.blendingInfo.blendingScore < b.blendingInfo.blendingScore)
			{
				return -1;
			}
			if (a.blendingInfo.blendingScore > b.blendingInfo.blendingScore)
			{
				return 1;
			}
			return 0;
		}

		private void UpdateBlendingCellStreaming(global::UnityEngine.Rendering.CommandBuffer cmd)
		{
			float a = ((m_LoadedCells.size != 0) ? m_LoadedCells[m_LoadedCells.size - 1].streamingInfo.streamingScore : 0f);
			float b = ((m_ToBeLoadedCells.size != 0) ? m_ToBeLoadedCells[m_ToBeLoadedCells.size - 1].streamingInfo.streamingScore : 0f);
			float worstScore = global::UnityEngine.Mathf.Max(a, b);
			ComputeBlendingScore(m_ToBeLoadedBlendingCells, worstScore);
			ComputeBlendingScore(m_LoadedBlendingCells, worstScore);
			m_ToBeLoadedBlendingCells.QuickSort(s_BlendingComparer);
			m_LoadedBlendingCells.QuickSort(s_BlendingComparer);
			int num = global::UnityEngine.Mathf.Min(numberOfCellsLoadedPerFrame, m_ToBeLoadedBlendingCells.size);
			while (m_TempBlendingCellToLoadList.size < num)
			{
				global::UnityEngine.Rendering.ProbeReferenceVolume.Cell cell = m_ToBeLoadedBlendingCells[m_TempBlendingCellToLoadList.size];
				if (!TryLoadBlendingCell(cell, m_TempBlendingCellToLoadList))
				{
					break;
				}
			}
			if (m_TempBlendingCellToLoadList.size != num)
			{
				int num2 = -1;
				int num3 = (int)((float)m_LoadedBlendingCells.size * (1f - turnoverRate));
				global::UnityEngine.Rendering.ProbeReferenceVolume.Cell cell2 = ((num3 < m_LoadedBlendingCells.size) ? m_LoadedBlendingCells[num3] : null);
				while (m_TempBlendingCellToLoadList.size < num && m_LoadedBlendingCells.size - m_TempBlendingCellToUnloadList.size != 0)
				{
					global::UnityEngine.Rendering.ProbeReferenceVolume.Cell cell3 = m_LoadedBlendingCells[m_LoadedBlendingCells.size - m_TempBlendingCellToUnloadList.size - 1];
					global::UnityEngine.Rendering.ProbeReferenceVolume.Cell cell4 = m_ToBeLoadedBlendingCells[m_TempBlendingCellToLoadList.size];
					if (cell4.blendingInfo.blendingScore >= (cell2 ?? cell3).blendingInfo.blendingScore)
					{
						if (cell2 == null)
						{
							break;
						}
						if (num2 == -1)
						{
							num2 = FindWorstBlendingCellToBeLoaded();
						}
						cell4 = m_ToBeLoadedBlendingCells[num2];
						if (cell4.blendingInfo.IsUpToDate())
						{
							break;
						}
					}
					if (cell3.streamingInfo.IsBlendingStreaming())
					{
						break;
					}
					UnloadBlendingCell(cell3, m_TempBlendingCellToUnloadList);
					_ = probeVolumeDebug.verboseStreamingLog;
					if (TryLoadBlendingCell(cell4, m_TempBlendingCellToLoadList) && num2 != -1)
					{
						m_ToBeLoadedBlendingCells[num2] = m_ToBeLoadedBlendingCells[m_TempBlendingCellToLoadList.size - 1];
						m_ToBeLoadedBlendingCells[m_TempBlendingCellToLoadList.size - 1] = cell4;
						if (++num2 >= m_ToBeLoadedBlendingCells.size)
						{
							num2 = m_TempBlendingCellToLoadList.size;
						}
					}
				}
				m_LoadedBlendingCells.RemoveRange(m_LoadedBlendingCells.size - m_TempBlendingCellToUnloadList.size, m_TempBlendingCellToUnloadList.size);
			}
			m_ToBeLoadedBlendingCells.RemoveRange(0, m_TempBlendingCellToLoadList.size);
			m_LoadedBlendingCells.AddRange(m_TempBlendingCellToLoadList);
			m_TempBlendingCellToLoadList.Clear();
			m_ToBeLoadedBlendingCells.AddRange(m_TempBlendingCellToUnloadList);
			m_TempBlendingCellToUnloadList.Clear();
			if (m_LoadedBlendingCells.size == 0)
			{
				return;
			}
			float num4 = scenarioBlendingFactor;
			int num5 = 0;
			int num6 = 0;
			while (num6 < numberOfCellsBlendedPerFrame && num5 < m_LoadedBlendingCells.size)
			{
				global::UnityEngine.Rendering.ProbeReferenceVolume.Cell cell5 = m_LoadedBlendingCells[num5++];
				if (!cell5.streamingInfo.IsBlendingStreaming() && !cell5.blendingInfo.IsUpToDate())
				{
					_ = probeVolumeDebug.verboseStreamingLog;
					cell5.blendingInfo.blendingFactor = num4;
					cell5.blendingInfo.MarkUpToDate();
					m_BlendingPool.BlendChunks(cell5, m_Pool);
					num6++;
				}
			}
			m_BlendingPool.PerformBlending(cmd, num4, m_Pool);
		}

		private static int DefragComparer(global::UnityEngine.Rendering.ProbeReferenceVolume.Cell a, global::UnityEngine.Rendering.ProbeReferenceVolume.Cell b)
		{
			if (a.indexInfo.updateInfo.GetNumberOfChunks() > b.indexInfo.updateInfo.GetNumberOfChunks())
			{
				return 1;
			}
			if (a.indexInfo.updateInfo.GetNumberOfChunks() < b.indexInfo.updateInfo.GetNumberOfChunks())
			{
				return -1;
			}
			return 0;
		}

		private void StartIndexDefragmentation()
		{
			if (m_SupportGPUStreaming)
			{
				m_IndexDefragmentationInProgress = true;
				m_IndexDefragCells.Clear();
				m_IndexDefragCells.AddRange(m_LoadedCells);
				m_IndexDefragCells.QuickSort(s_DefragComparer);
				m_DefragIndex.Clear();
			}
		}

		private void UpdateIndexDefragmentation()
		{
			using (new global::UnityEngine.Rendering.ProfilingScope(global::UnityEngine.Rendering.ProfilingSampler.Get(global::UnityEngine.Rendering.CoreProfileId.APVIndexDefragUpdate)))
			{
				m_TempIndexDefragCells.Clear();
				int num = global::UnityEngine.Mathf.Min(m_IndexDefragCells.size, numberOfCellsLoadedPerFrame);
				int i = 0;
				int num2 = 0;
				for (; i < m_IndexDefragCells.size; i++)
				{
					if (num2 >= num)
					{
						break;
					}
					global::UnityEngine.Rendering.ProbeReferenceVolume.Cell value = m_IndexDefragCells[m_IndexDefragCells.size - i - 1];
					m_DefragIndex.FindSlotsForEntries(ref value.indexInfo.updateInfo.entriesInfo);
					m_DefragIndex.ReserveChunks(value.indexInfo.updateInfo.entriesInfo, ignoreErrorLog: false);
					if (!value.streamingInfo.IsStreaming() && !value.streamingInfo.IsBlendingStreaming())
					{
						m_DefragIndex.AddBricks(value.indexInfo, value.data.bricks, value.poolInfo.chunkList, global::UnityEngine.Rendering.ProbeBrickPool.GetChunkSizeInBrickCount(), m_Pool.GetPoolWidth(), m_Pool.GetPoolHeight());
						m_DefragCellIndices.UpdateCell(value.indexInfo);
						num2++;
					}
					else
					{
						m_TempIndexDefragCells.Add(in value);
					}
				}
				m_IndexDefragCells.Resize(m_IndexDefragCells.size - i);
				m_IndexDefragCells.AddRange(m_TempIndexDefragCells);
				if (m_IndexDefragCells.size == 0)
				{
					global::UnityEngine.Rendering.ProbeBrickIndex defragIndex = m_DefragIndex;
					m_DefragIndex = m_Index;
					m_Index = defragIndex;
					global::UnityEngine.Rendering.ProbeGlobalIndirection defragCellIndices = m_DefragCellIndices;
					m_DefragCellIndices = m_CellIndices;
					m_CellIndices = defragCellIndices;
					m_IndexDefragmentationInProgress = false;
				}
			}
		}

		private void OnStreamingComplete(global::UnityEngine.Rendering.ProbeReferenceVolume.CellStreamingRequest request, global::UnityEngine.Rendering.CommandBuffer cmd)
		{
			request.cell.streamingInfo.request = null;
			UpdatePoolAndIndex(request.cell, request.scratchBuffer, request.scratchBufferLayout, request.poolIndex, cmd);
		}

		private void OnBlendingStreamingComplete(global::UnityEngine.Rendering.ProbeReferenceVolume.CellStreamingRequest request, global::UnityEngine.Rendering.CommandBuffer cmd)
		{
			UpdatePool(cmd, request.cell.blendingInfo.chunkList, request.scratchBuffer, request.scratchBufferLayout, request.poolIndex);
			if (request.poolIndex == 0)
			{
				request.cell.streamingInfo.blendingRequest0 = null;
			}
			else
			{
				request.cell.streamingInfo.blendingRequest1 = null;
			}
			if (request.cell.streamingInfo.blendingRequest0 == null && request.cell.streamingInfo.blendingRequest1 == null && !request.cell.indexInfo.indexUpdated)
			{
				UpdateCellIndex(request.cell);
			}
		}

		private void PushDiskStreamingRequest(global::UnityEngine.Rendering.ProbeReferenceVolume.Cell cell, string scenario, int poolIndex, global::UnityEngine.Rendering.ProbeReferenceVolume.CellStreamingRequest.OnStreamingCompleteDelegate onStreamingComplete)
		{
			global::UnityEngine.Rendering.ProbeReferenceVolume.CellStreamingRequest cellStreamingRequest = m_StreamingRequestsPool.Get();
			cellStreamingRequest.cell = cell;
			cellStreamingRequest.state = global::UnityEngine.Rendering.ProbeReferenceVolume.CellStreamingRequest.State.Pending;
			cellStreamingRequest.scenarioData = m_CurrentBakingSet.scenarios[scenario];
			cellStreamingRequest.poolIndex = poolIndex;
			cellStreamingRequest.onStreamingComplete = onStreamingComplete;
			if (poolIndex == -1 || poolIndex == 0)
			{
				cellStreamingRequest.streamSharedData = true;
			}
			if (probeVolumeDebug.verboseStreamingLog)
			{
				_ = -1;
			}
			switch (poolIndex)
			{
			case -1:
				cell.streamingInfo.request = cellStreamingRequest;
				break;
			case 0:
				cell.streamingInfo.blendingRequest0 = cellStreamingRequest;
				break;
			case 1:
				cell.streamingInfo.blendingRequest1 = cellStreamingRequest;
				break;
			}
			m_StreamingQueue.Enqueue(cellStreamingRequest);
		}

		private void CancelStreamingRequest(global::UnityEngine.Rendering.ProbeReferenceVolume.Cell cell)
		{
			m_Index.RemoveBricks(cell.indexInfo);
			m_Pool.Deallocate(cell.poolInfo.chunkList);
			if (cell.streamingInfo.request != null)
			{
				cell.streamingInfo.request.Cancel();
			}
		}

		private void CancelBlendingStreamingRequest(global::UnityEngine.Rendering.ProbeReferenceVolume.Cell cell)
		{
			if (cell.streamingInfo.blendingRequest0 != null)
			{
				cell.streamingInfo.blendingRequest0.Cancel();
			}
			if (cell.streamingInfo.blendingRequest1 != null)
			{
				cell.streamingInfo.blendingRequest1.Cancel();
			}
		}

		private unsafe bool ProcessDiskStreamingRequest(global::UnityEngine.Rendering.ProbeReferenceVolume.CellStreamingRequest request)
		{
			int index = request.cell.desc.index;
			global::UnityEngine.Rendering.ProbeReferenceVolume.Cell cell = cells[index];
			global::UnityEngine.Rendering.ProbeReferenceVolume.CellDesc desc = cell.desc;
			global::UnityEngine.Rendering.ProbeReferenceVolume.CellData data = cell.data;
			if (!m_ScratchBufferPool.AllocateScratchBuffer(desc.shChunkCount, out var scratchBuffer, out var layout, m_DiskStreamingUseCompute))
			{
				return false;
			}
			if (!m_CurrentBakingSet.HasValidSharedData())
			{
				global::UnityEngine.Debug.LogError("One or more data file missing for baking set " + m_CurrentBakingSet.name + ". Cannot load shared data.");
				return false;
			}
			if (!request.scenarioData.HasValidData(m_SHBands))
			{
				global::UnityEngine.Debug.LogError("One or more data file missing for baking set " + m_CurrentBakingSet.name + " scenario " + lightingScenario + ". Cannot load scenario data.");
				return false;
			}
			if (probeVolumeDebug.verboseStreamingLog)
			{
				_ = request.poolIndex;
				_ = -1;
			}
			request.scratchBuffer = scratchBuffer;
			request.scratchBufferLayout = layout;
			request.bytesWritten = 0;
			byte* unsafePtr = (byte*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(request.scratchBuffer.stagingBuffer);
			byte* ptr = unsafePtr;
			uint* ptr2 = (uint*)ptr;
			global::System.Collections.Generic.List<global::UnityEngine.Rendering.ProbeBrickPool.BrickChunkAlloc> list = ((request.poolIndex == -1) ? request.cell.poolInfo.chunkList : request.cell.blendingInfo.chunkList);
			int count = list.Count;
			for (int i = 0; i < count; i++)
			{
				global::UnityEngine.Rendering.ProbeBrickPool.BrickChunkAlloc brickChunkAlloc = list[i];
				ptr2[i * 4] = (uint)brickChunkAlloc.x;
				ptr2[i * 4 + 1] = (uint)brickChunkAlloc.y;
				ptr2[i * 4 + 2] = (uint)brickChunkAlloc.z;
				ptr2[i * 4 + 3] = 0u;
			}
			ptr += count * 4 * 4;
			ptr2 = (uint*)ptr;
			list = request.cell.poolInfo.chunkList;
			for (int j = 0; j < count; j++)
			{
				global::UnityEngine.Rendering.ProbeBrickPool.BrickChunkAlloc brickChunkAlloc2 = list[j];
				ptr2[j * 4] = (uint)brickChunkAlloc2.x;
				ptr2[j * 4 + 1] = (uint)brickChunkAlloc2.y;
				ptr2[j * 4 + 2] = (uint)brickChunkAlloc2.z;
				ptr2[j * 4 + 3] = 0u;
			}
			ptr += count * 4 * 4;
			global::UnityEngine.Rendering.ProbeVolumeStreamableAsset cellDataAsset = request.scenarioData.cellDataAsset;
			global::UnityEngine.Rendering.ProbeVolumeStreamableAsset.StreamableCellDesc streamableCellDesc = cellDataAsset.streamableCellDescs[index];
			int shChunkCount = desc.shChunkCount;
			int num = m_CurrentBakingSet.L0ChunkSize * shChunkCount;
			int num2 = m_CurrentBakingSet.L1ChunkSize * shChunkCount;
			int num3 = num + 2 * num2;
			request.cellDataStreamingRequest.AddReadCommand(streamableCellDesc.offset, num3, ptr);
			ptr += num3;
			request.bytesWritten += request.cellDataStreamingRequest.RunCommands(cellDataAsset.OpenFile());
			if (request.streamSharedData)
			{
				global::UnityEngine.Rendering.ProbeVolumeStreamableAsset cellSharedDataAsset = m_CurrentBakingSet.cellSharedDataAsset;
				streamableCellDesc = cellSharedDataAsset.streamableCellDescs[index];
				int sharedDataChunkSize = m_CurrentBakingSet.sharedDataChunkSize;
				request.cellSharedDataStreamingRequest.AddReadCommand(streamableCellDesc.offset, sharedDataChunkSize * shChunkCount, ptr);
				ptr += sharedDataChunkSize * shChunkCount;
				request.bytesWritten += request.cellSharedDataStreamingRequest.RunCommands(cellSharedDataAsset.OpenFile());
			}
			if (m_SHBands == global::UnityEngine.Rendering.ProbeVolumeSHBands.SphericalHarmonicsL2)
			{
				global::UnityEngine.Rendering.ProbeVolumeStreamableAsset cellOptionalDataAsset = request.scenarioData.cellOptionalDataAsset;
				streamableCellDesc = cellOptionalDataAsset.streamableCellDescs[index];
				int num4 = m_CurrentBakingSet.L2TextureChunkSize * shChunkCount * 4;
				request.cellOptionalDataStreamingRequest.AddReadCommand(streamableCellDesc.offset, num4, ptr);
				ptr += num4;
				request.bytesWritten += request.cellOptionalDataStreamingRequest.RunCommands(cellOptionalDataAsset.OpenFile());
			}
			if (m_CurrentBakingSet.bakedProbeOcclusion)
			{
				global::UnityEngine.Rendering.ProbeVolumeStreamableAsset cellProbeOcclusionDataAsset = request.scenarioData.cellProbeOcclusionDataAsset;
				streamableCellDesc = cellProbeOcclusionDataAsset.streamableCellDescs[index];
				int num5 = m_CurrentBakingSet.ProbeOcclusionChunkSize * shChunkCount;
				request.cellProbeOcclusionDataStreamingRequest.AddReadCommand(streamableCellDesc.offset, num5, ptr);
				ptr += num5;
				request.bytesWritten += request.cellProbeOcclusionDataStreamingRequest.RunCommands(cellProbeOcclusionDataAsset.OpenFile());
			}
			data.bricks = new global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.ProbeBrickIndex.Brick>(desc.bricksCount, global::Unity.Collections.Allocator.Persistent, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			global::UnityEngine.Rendering.ProbeVolumeStreamableAsset cellBricksDataAsset = m_CurrentBakingSet.cellBricksDataAsset;
			streamableCellDesc = cellBricksDataAsset.streamableCellDescs[index];
			request.brickStreamingRequest.AddReadCommand(streamableCellDesc.offset, cellBricksDataAsset.elementSize * global::UnityEngine.Mathf.Min(streamableCellDesc.elementCount, desc.bricksCount), (byte*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(data.bricks));
			request.brickStreamingRequest.RunCommands(cellBricksDataAsset.OpenFile());
			if (m_CurrentBakingSet.HasSupportData())
			{
				global::UnityEngine.Rendering.ProbeVolumeStreamableAsset cellSupportDataAsset = m_CurrentBakingSet.cellSupportDataAsset;
				streamableCellDesc = cellSupportDataAsset.streamableCellDescs[index];
				int offset = streamableCellDesc.offset;
				int num6 = streamableCellDesc.elementCount * m_CurrentBakingSet.supportPositionChunkSize;
				int num7 = streamableCellDesc.elementCount * m_CurrentBakingSet.supportTouchupChunkSize;
				int num8 = streamableCellDesc.elementCount * m_CurrentBakingSet.supportOffsetsChunkSize;
				int num9 = streamableCellDesc.elementCount * m_CurrentBakingSet.supportLayerMaskChunkSize;
				int num10 = streamableCellDesc.elementCount * m_CurrentBakingSet.supportValidityChunkSize;
				data.probePositions = new global::Unity.Collections.NativeArray<byte>(num6, global::Unity.Collections.Allocator.Persistent, global::Unity.Collections.NativeArrayOptions.UninitializedMemory).Reinterpret<global::UnityEngine.Vector3>(1);
				data.validity = new global::Unity.Collections.NativeArray<byte>(num10, global::Unity.Collections.Allocator.Persistent, global::Unity.Collections.NativeArrayOptions.UninitializedMemory).Reinterpret<float>(1);
				data.layer = new global::Unity.Collections.NativeArray<byte>(num9, global::Unity.Collections.Allocator.Persistent, global::Unity.Collections.NativeArrayOptions.UninitializedMemory).Reinterpret<byte>(1);
				data.touchupVolumeInteraction = new global::Unity.Collections.NativeArray<byte>(num7, global::Unity.Collections.Allocator.Persistent, global::Unity.Collections.NativeArrayOptions.UninitializedMemory).Reinterpret<float>(1);
				data.offsetVectors = new global::Unity.Collections.NativeArray<byte>(num8, global::Unity.Collections.Allocator.Persistent, global::Unity.Collections.NativeArrayOptions.UninitializedMemory).Reinterpret<global::UnityEngine.Vector3>(1);
				request.supportStreamingRequest.AddReadCommand(offset, num6, (byte*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(data.probePositions));
				offset += num6;
				request.supportStreamingRequest.AddReadCommand(offset, num10, (byte*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(data.validity));
				offset += num10;
				request.supportStreamingRequest.AddReadCommand(offset, num7, (byte*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(data.touchupVolumeInteraction));
				offset += num7;
				request.supportStreamingRequest.AddReadCommand(offset, num9, (byte*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(data.layer));
				offset += num9;
				request.supportStreamingRequest.AddReadCommand(offset, num8, (byte*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(data.offsetVectors));
				request.supportStreamingRequest.RunCommands(cellSupportDataAsset.OpenFile());
			}
			request.state = global::UnityEngine.Rendering.ProbeReferenceVolume.CellStreamingRequest.State.Active;
			m_ActiveStreamingRequests.Add(request);
			return true;
		}

		private void AllocateScratchBufferPoolIfNeeded()
		{
			if (!m_SupportDiskStreaming)
			{
				return;
			}
			int chunkGPUMemory = m_CurrentBakingSet.GetChunkGPUMemory(m_SHBands);
			int maxSHChunkCount = m_CurrentBakingSet.maxSHChunkCount;
			if (m_ScratchBufferPool == null || m_ScratchBufferPool.chunkSize != chunkGPUMemory || m_ScratchBufferPool.maxChunkCount != maxSHChunkCount)
			{
				_ = probeVolumeDebug.verboseStreamingLog;
				if (m_ScratchBufferPool != null)
				{
					m_ScratchBufferPool.Cleanup();
				}
				m_ScratchBufferPool = new global::UnityEngine.Rendering.ProbeVolumeScratchBufferPool(m_CurrentBakingSet, m_SHBands);
			}
		}

		private void UpdateActiveRequests(global::UnityEngine.Rendering.CommandBuffer cmd)
		{
			if (m_ActiveStreamingRequests.Count <= 0)
			{
				return;
			}
			for (int num = m_ActiveStreamingRequests.Count - 1; num >= 0; num--)
			{
				global::UnityEngine.Rendering.ProbeReferenceVolume.CellStreamingRequest cellStreamingRequest = m_ActiveStreamingRequests[num];
				bool flag = false;
				if (cellStreamingRequest.state == global::UnityEngine.Rendering.ProbeReferenceVolume.CellStreamingRequest.State.Canceled)
				{
					_ = probeVolumeDebug.verboseStreamingLog;
					m_ScratchBufferPool.ReleaseScratchBuffer(cellStreamingRequest.scratchBuffer);
					flag = true;
				}
				else
				{
					cellStreamingRequest.UpdateState();
					if (cellStreamingRequest.state == global::UnityEngine.Rendering.ProbeReferenceVolume.CellStreamingRequest.State.Complete)
					{
						if (probeVolumeDebug.verboseStreamingLog)
						{
							_ = cellStreamingRequest.poolIndex;
							_ = -1;
						}
						if (cellStreamingRequest.scratchBuffer.buffer != null)
						{
							cellStreamingRequest.scratchBuffer.buffer.LockBufferForWrite<byte>(0, cellStreamingRequest.scratchBuffer.stagingBuffer.Length).CopyFrom(cellStreamingRequest.scratchBuffer.stagingBuffer);
							cellStreamingRequest.scratchBuffer.buffer.UnlockBufferAfterWrite<byte>(cellStreamingRequest.scratchBuffer.stagingBuffer.Length);
						}
						cellStreamingRequest.onStreamingComplete(cellStreamingRequest, cmd);
						m_ScratchBufferPool.ReleaseScratchBuffer(cellStreamingRequest.scratchBuffer);
						flag = true;
					}
					else if (cellStreamingRequest.state == global::UnityEngine.Rendering.ProbeReferenceVolume.CellStreamingRequest.State.Invalid)
					{
						_ = probeVolumeDebug.verboseStreamingLog;
						m_ScratchBufferPool.ReleaseScratchBuffer(cellStreamingRequest.scratchBuffer);
						cellStreamingRequest.Reset();
						m_ActiveStreamingRequests.RemoveAt(num);
						m_StreamingQueue.Enqueue(cellStreamingRequest);
					}
				}
				if (flag)
				{
					m_ActiveStreamingRequests.RemoveAt(num);
					m_StreamingRequestsPool.Release(cellStreamingRequest);
				}
			}
		}

		private void ProcessNewRequests()
		{
			global::UnityEngine.Rendering.ProbeReferenceVolume.CellStreamingRequest result;
			while (m_StreamingQueue.TryPeek(out result))
			{
				if (result.state == global::UnityEngine.Rendering.ProbeReferenceVolume.CellStreamingRequest.State.Canceled)
				{
					if (probeVolumeDebug.verboseStreamingLog)
					{
						_ = result.poolIndex;
						_ = -1;
					}
					m_StreamingRequestsPool.Release(result);
					m_StreamingQueue.Dequeue();
				}
				else
				{
					if (!ProcessDiskStreamingRequest(result))
					{
						break;
					}
					m_StreamingQueue.Dequeue();
				}
			}
		}

		private void UpdateDiskStreaming(global::UnityEngine.Rendering.CommandBuffer cmd)
		{
			if (!diskStreamingEnabled)
			{
				return;
			}
			using (new global::UnityEngine.Rendering.ProfilingScope(global::UnityEngine.Rendering.ProfilingSampler.Get(global::UnityEngine.Rendering.CoreProfileId.APVDiskStreamingUpdate)))
			{
				AllocateScratchBufferPoolIfNeeded();
				ProcessNewRequests();
				UpdateActiveRequests(cmd);
				if (m_ActiveStreamingRequests.Count == 0 && m_StreamingQueue.Count == 0 && m_CurrentBakingSet.cellBricksDataAsset != null && m_CurrentBakingSet.cellBricksDataAsset.IsOpen())
				{
					_ = probeVolumeDebug.verboseStreamingLog;
					m_CurrentBakingSet.cellBricksDataAsset.CloseFile();
					m_CurrentBakingSet.cellSupportDataAsset.CloseFile();
					m_CurrentBakingSet.cellSharedDataAsset.CloseFile();
					if (m_CurrentBakingSet.scenarios.TryGetValue(lightingScenario, out var value))
					{
						value.cellDataAsset.CloseFile();
						value.cellOptionalDataAsset.CloseFile();
						value.cellProbeOcclusionDataAsset.CloseFile();
					}
					if (!string.IsNullOrEmpty(otherScenario) && m_CurrentBakingSet.scenarios.TryGetValue(lightingScenario, out var value2))
					{
						value2.cellDataAsset.CloseFile();
						value2.cellOptionalDataAsset.CloseFile();
						value2.cellProbeOcclusionDataAsset.CloseFile();
					}
				}
			}
			if (probeVolumeDebug.debugStreaming && m_ToBeLoadedCells.size == 0 && m_ActiveStreamingRequests.Count == 0)
			{
				UnloadAllCells();
			}
		}

		private bool HasActiveStreamingRequest(global::UnityEngine.Rendering.ProbeReferenceVolume.Cell cell)
		{
			if (diskStreamingEnabled)
			{
				return m_ActiveStreamingRequests.Exists((global::UnityEngine.Rendering.ProbeReferenceVolume.CellStreamingRequest x) => x.cell == cell);
			}
			return false;
		}

		[global::System.Diagnostics.Conditional("UNITY_EDITOR")]
		[global::System.Diagnostics.Conditional("DEVELOPMENT_BUILD")]
		private void LogStreaming(string log)
		{
			global::UnityEngine.Debug.Log(log);
		}
	}
}
