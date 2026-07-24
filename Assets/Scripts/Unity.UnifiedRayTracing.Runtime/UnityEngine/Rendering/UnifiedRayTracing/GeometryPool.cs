namespace UnityEngine.Rendering.UnifiedRayTracing
{
	internal sealed class GeometryPool : global::System.IDisposable
	{
		private static class GeoPoolShaderIDs
		{
			public static readonly int _InputIBBaseOffset = global::UnityEngine.Shader.PropertyToID("_InputIBBaseOffset");

			public static readonly int _DispatchIndexOffset = global::UnityEngine.Shader.PropertyToID("_DispatchIndexOffset");

			public static readonly int _InputIBCount = global::UnityEngine.Shader.PropertyToID("_InputIBCount");

			public static readonly int _OutputIBOffset = global::UnityEngine.Shader.PropertyToID("_OutputIBOffset");

			public static readonly int _InputFirstVertex = global::UnityEngine.Shader.PropertyToID("_InputFirstVertex");

			public static readonly int _InputIndexBuffer = global::UnityEngine.Shader.PropertyToID("_InputIndexBuffer");

			public static readonly int _OutputIndexBuffer = global::UnityEngine.Shader.PropertyToID("_OutputIndexBuffer");

			public static readonly int _InputVBCount = global::UnityEngine.Shader.PropertyToID("_InputVBCount");

			public static readonly int _InputBaseVertexOffset = global::UnityEngine.Shader.PropertyToID("_InputBaseVertexOffset");

			public static readonly int _DispatchVertexOffset = global::UnityEngine.Shader.PropertyToID("_DispatchVertexOffset");

			public static readonly int _OutputVBSize = global::UnityEngine.Shader.PropertyToID("_OutputVBSize");

			public static readonly int _OutputVBOffset = global::UnityEngine.Shader.PropertyToID("_OutputVBOffset");

			public static readonly int _InputPosBufferStride = global::UnityEngine.Shader.PropertyToID("_InputPosBufferStride");

			public static readonly int _InputPosBufferOffset = global::UnityEngine.Shader.PropertyToID("_InputPosBufferOffset");

			public static readonly int _InputUv0BufferStride = global::UnityEngine.Shader.PropertyToID("_InputUv0BufferStride");

			public static readonly int _InputUv0BufferOffset = global::UnityEngine.Shader.PropertyToID("_InputUv0BufferOffset");

			public static readonly int _InputUv1BufferStride = global::UnityEngine.Shader.PropertyToID("_InputUv1BufferStride");

			public static readonly int _InputUv1BufferOffset = global::UnityEngine.Shader.PropertyToID("_InputUv1BufferOffset");

			public static readonly int _InputNormalBufferStride = global::UnityEngine.Shader.PropertyToID("_InputNormalBufferStride");

			public static readonly int _InputNormalBufferOffset = global::UnityEngine.Shader.PropertyToID("_InputNormalBufferOffset");

			public static readonly int _PosBuffer = global::UnityEngine.Shader.PropertyToID("_PosBuffer");

			public static readonly int _Uv0Buffer = global::UnityEngine.Shader.PropertyToID("_Uv0Buffer");

			public static readonly int _Uv1Buffer = global::UnityEngine.Shader.PropertyToID("_Uv1Buffer");

			public static readonly int _NormalBuffer = global::UnityEngine.Shader.PropertyToID("_NormalBuffer");

			public static readonly int _OutputVB = global::UnityEngine.Shader.PropertyToID("_OutputVB");

			public static readonly int _AttributesMask = global::UnityEngine.Shader.PropertyToID("_AttributesMask");
		}

		public struct MeshChunk
		{
			public global::UnityEngine.Rendering.UnifiedRayTracing.BlockAllocator.Allocation vertexAlloc;

			public global::UnityEngine.Rendering.UnifiedRayTracing.BlockAllocator.Allocation indexAlloc;

			public static global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPool.MeshChunk Invalid => new global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPool.MeshChunk
			{
				vertexAlloc = global::UnityEngine.Rendering.UnifiedRayTracing.BlockAllocator.Allocation.Invalid,
				indexAlloc = global::UnityEngine.Rendering.UnifiedRayTracing.BlockAllocator.Allocation.Invalid
			};

			public global::UnityEngine.Rendering.UnifiedRayTracing.GeoPoolMeshChunk EncodeGPUEntry()
			{
				return new global::UnityEngine.Rendering.UnifiedRayTracing.GeoPoolMeshChunk
				{
					indexOffset = indexAlloc.block.offset,
					indexCount = indexAlloc.block.count,
					vertexOffset = vertexAlloc.block.offset,
					vertexCount = vertexAlloc.block.count
				};
			}
		}

		public struct GeometrySlot
		{
			public uint refCount;

			public uint hash;

			public global::UnityEngine.Rendering.UnifiedRayTracing.BlockAllocator.Allocation meshChunkTableAlloc;

			public global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPool.MeshChunk> meshChunks;

			public bool hasGPUData;

			public static readonly global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPool.GeometrySlot Invalid = new global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPool.GeometrySlot
			{
				meshChunkTableAlloc = global::UnityEngine.Rendering.UnifiedRayTracing.BlockAllocator.Allocation.Invalid,
				hasGPUData = false
			};

			public bool valid => meshChunkTableAlloc.valid;
		}

		private struct GeoPoolEntrySlot
		{
			public uint refCount;

			public uint hash;

			public int geoSlotHandle;

			public static readonly global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPool.GeoPoolEntrySlot Invalid = new global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPool.GeoPoolEntrySlot
			{
				refCount = 0u,
				hash = 0u,
				geoSlotHandle = -1
			};

			public bool valid => geoSlotHandle != -1;
		}

		private struct VertexBufferAttribInfo
		{
			public global::UnityEngine.GraphicsBuffer buffer;

			public int stride;

			public int offset;

			public int byteCount;

			public bool valid => buffer != null;
		}

		private const int kMaxThreadGroupsPerDispatch = 65535;

		private const int kThreadGroupSize = 256;

		private const int InvalidHandle = -1;

		private const global::UnityEngine.GraphicsBuffer.Target VertexBufferTarget = global::UnityEngine.GraphicsBuffer.Target.Structured;

		private const global::UnityEngine.GraphicsBuffer.Target IndexBufferTarget = global::UnityEngine.GraphicsBuffer.Target.Structured;

		private global::UnityEngine.GraphicsBuffer m_GlobalIndexBuffer;

		private global::UnityEngine.GraphicsBuffer m_GlobalVertexBuffer;

		private global::UnityEngine.GraphicsBuffer m_GlobalMeshChunkTableEntryBuffer;

		private readonly global::UnityEngine.GraphicsBuffer m_DummyBuffer;

		private int m_MaxVertCounts;

		private int m_MaxIndexCounts;

		private int m_MaxMeshChunkTableEntriesCount;

		private global::UnityEngine.Rendering.UnifiedRayTracing.BlockAllocator m_VertexAllocator;

		private global::UnityEngine.Rendering.UnifiedRayTracing.BlockAllocator m_IndexAllocator;

		private global::UnityEngine.Rendering.UnifiedRayTracing.BlockAllocator m_MeshChunkTableAllocator;

		private global::Unity.Collections.NativeParallelHashMap<uint, int> m_MeshHashToGeoSlot;

		private global::System.Collections.Generic.List<global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPool.GeometrySlot> m_GeoSlots;

		private global::Unity.Collections.NativeList<int> m_FreeGeoSlots;

		private global::Unity.Collections.NativeParallelHashMap<uint, global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPoolHandle> m_GeoPoolEntryHashToSlot;

		private global::Unity.Collections.NativeList<global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPool.GeoPoolEntrySlot> m_GeoPoolEntrySlots;

		private global::Unity.Collections.NativeList<global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPoolHandle> m_FreeGeoPoolEntrySlots;

		private readonly global::System.Collections.Generic.List<global::UnityEngine.GraphicsBuffer> m_InputBufferReferences;

		private readonly global::UnityEngine.ComputeShader m_CopyShader;

		private global::UnityEngine.ComputeShader m_GeometryPoolKernelsCS;

		private int m_KernelMainUpdateIndexBuffer16;

		private int m_KernelMainUpdateIndexBuffer32;

		private int m_KernelMainUpdateVertexBuffer;

		private readonly global::UnityEngine.Rendering.CommandBuffer m_CmdBuffer;

		private bool m_MustClearCmdBuffer;

		private int m_PendingCmds;

		public global::UnityEngine.GraphicsBuffer globalIndexBuffer => m_GlobalIndexBuffer;

		public global::UnityEngine.GraphicsBuffer globalVertexBuffer => m_GlobalVertexBuffer;

		public int globalVertexBufferStrideBytes => GetVertexByteSize();

		public global::UnityEngine.GraphicsBuffer globalMeshChunkTableEntryBuffer => m_GlobalMeshChunkTableEntryBuffer;

		public int indicesCount => m_MaxIndexCounts;

		public int verticesCount => m_MaxVertCounts;

		public int meshChunkTablesEntryCount => m_MaxMeshChunkTableEntriesCount;

		public static int GetVertexByteSize()
		{
			return 32;
		}

		public static int GetIndexByteSize()
		{
			return 4;
		}

		public static int GetMeshChunkTableEntryByteSize()
		{
			return global::System.Runtime.InteropServices.Marshal.SizeOf<global::UnityEngine.Rendering.UnifiedRayTracing.GeoPoolMeshChunk>();
		}

		private int GetFormatByteCount(global::UnityEngine.Rendering.VertexAttributeFormat format)
		{
			return format switch
			{
				global::UnityEngine.Rendering.VertexAttributeFormat.Float32 => 4, 
				global::UnityEngine.Rendering.VertexAttributeFormat.Float16 => 2, 
				global::UnityEngine.Rendering.VertexAttributeFormat.UNorm8 => 1, 
				global::UnityEngine.Rendering.VertexAttributeFormat.SNorm8 => 1, 
				global::UnityEngine.Rendering.VertexAttributeFormat.UNorm16 => 2, 
				global::UnityEngine.Rendering.VertexAttributeFormat.SNorm16 => 2, 
				global::UnityEngine.Rendering.VertexAttributeFormat.UInt8 => 1, 
				global::UnityEngine.Rendering.VertexAttributeFormat.SInt8 => 1, 
				global::UnityEngine.Rendering.VertexAttributeFormat.UInt16 => 2, 
				global::UnityEngine.Rendering.VertexAttributeFormat.SInt16 => 2, 
				global::UnityEngine.Rendering.VertexAttributeFormat.UInt32 => 4, 
				global::UnityEngine.Rendering.VertexAttributeFormat.SInt32 => 4, 
				_ => 4, 
			};
		}

		private static int DivUp(int x, int y)
		{
			return (x + y - 1) / y;
		}

		public GeometryPool(in global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPoolDesc desc, global::UnityEngine.ComputeShader geometryPoolShader, global::UnityEngine.ComputeShader copyShader)
		{
			m_CopyShader = copyShader;
			LoadKernels(geometryPoolShader);
			m_CmdBuffer = new global::UnityEngine.Rendering.CommandBuffer();
			m_InputBufferReferences = new global::System.Collections.Generic.List<global::UnityEngine.GraphicsBuffer>();
			m_MustClearCmdBuffer = false;
			m_PendingCmds = 0;
			m_MaxVertCounts = CalcVertexCount(desc.vertexPoolByteSize);
			m_MaxIndexCounts = CalcIndexCount(desc.indexPoolByteSize);
			m_MaxMeshChunkTableEntriesCount = CalcMeshChunkTablesCount(desc.meshChunkTablesByteSize);
			m_GlobalVertexBuffer = new global::UnityEngine.GraphicsBuffer(global::UnityEngine.GraphicsBuffer.Target.Structured, DivUp(m_MaxVertCounts * GetVertexByteSize(), 4), 4);
			m_GlobalIndexBuffer = new global::UnityEngine.GraphicsBuffer(global::UnityEngine.GraphicsBuffer.Target.Structured, m_MaxIndexCounts, 4);
			m_GlobalMeshChunkTableEntryBuffer = new global::UnityEngine.GraphicsBuffer(global::UnityEngine.GraphicsBuffer.Target.Structured, m_MaxMeshChunkTableEntriesCount, GetMeshChunkTableEntryByteSize());
			m_DummyBuffer = new global::UnityEngine.GraphicsBuffer(global::UnityEngine.GraphicsBuffer.Target.Structured, 16, 4);
			int capacity = 4096;
			m_MeshHashToGeoSlot = new global::Unity.Collections.NativeParallelHashMap<uint, int>(capacity, global::Unity.Collections.Allocator.Persistent);
			m_GeoSlots = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPool.GeometrySlot>();
			m_FreeGeoSlots = new global::Unity.Collections.NativeList<int>(global::Unity.Collections.Allocator.Persistent);
			m_GeoPoolEntryHashToSlot = new global::Unity.Collections.NativeParallelHashMap<uint, global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPoolHandle>(capacity, global::Unity.Collections.Allocator.Persistent);
			m_GeoPoolEntrySlots = new global::Unity.Collections.NativeList<global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPool.GeoPoolEntrySlot>(global::Unity.Collections.Allocator.Persistent);
			m_FreeGeoPoolEntrySlots = new global::Unity.Collections.NativeList<global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPoolHandle>(global::Unity.Collections.Allocator.Persistent);
			m_VertexAllocator = default(global::UnityEngine.Rendering.UnifiedRayTracing.BlockAllocator);
			m_VertexAllocator.Initialize(m_MaxVertCounts);
			m_IndexAllocator = default(global::UnityEngine.Rendering.UnifiedRayTracing.BlockAllocator);
			m_IndexAllocator.Initialize(m_MaxIndexCounts);
			m_MeshChunkTableAllocator = default(global::UnityEngine.Rendering.UnifiedRayTracing.BlockAllocator);
			m_MeshChunkTableAllocator.Initialize(m_MaxMeshChunkTableEntriesCount);
		}

		private void DisposeInputBuffers()
		{
			if (m_InputBufferReferences.Count == 0)
			{
				return;
			}
			foreach (global::UnityEngine.GraphicsBuffer inputBufferReference in m_InputBufferReferences)
			{
				inputBufferReference.Dispose();
			}
			m_InputBufferReferences.Clear();
		}

		public void Dispose()
		{
			m_IndexAllocator.Dispose();
			m_VertexAllocator.Dispose();
			m_MeshChunkTableAllocator.Dispose();
			m_DummyBuffer.Dispose();
			m_MeshHashToGeoSlot.Dispose();
			foreach (global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPool.GeometrySlot geoSlot in m_GeoSlots)
			{
				if (geoSlot.valid)
				{
					global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPool.MeshChunk> meshChunks = geoSlot.meshChunks;
					meshChunks.Dispose();
				}
			}
			m_GeoSlots = null;
			m_FreeGeoSlots.Dispose();
			m_GeoPoolEntryHashToSlot.Dispose();
			m_GeoPoolEntrySlots.Dispose();
			m_FreeGeoPoolEntrySlots.Dispose();
			m_GlobalIndexBuffer.Dispose();
			m_GlobalVertexBuffer.Release();
			m_GlobalMeshChunkTableEntryBuffer.Dispose();
			m_CmdBuffer.Release();
			DisposeInputBuffers();
		}

		private void LoadKernels(global::UnityEngine.ComputeShader geometryPoolShader)
		{
			m_GeometryPoolKernelsCS = geometryPoolShader;
			m_KernelMainUpdateIndexBuffer16 = m_GeometryPoolKernelsCS.FindKernel("MainUpdateIndexBuffer16");
			m_KernelMainUpdateIndexBuffer32 = m_GeometryPoolKernelsCS.FindKernel("MainUpdateIndexBuffer32");
			m_KernelMainUpdateVertexBuffer = m_GeometryPoolKernelsCS.FindKernel("MainUpdateVertexBuffer");
		}

		private int CalcVertexCount(int bufferByteSize)
		{
			return DivUp(bufferByteSize, GetVertexByteSize());
		}

		private int CalcIndexCount(int bufferByteSize)
		{
			return DivUp(bufferByteSize, GetIndexByteSize());
		}

		private int CalcMeshChunkTablesCount(int bufferByteSize)
		{
			return DivUp(bufferByteSize, GetMeshChunkTableEntryByteSize());
		}

		private void DeallocateGeometrySlot(ref global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPool.GeometrySlot slot)
		{
			if (slot.meshChunkTableAlloc.valid)
			{
				m_MeshChunkTableAllocator.FreeAllocation(in slot.meshChunkTableAlloc);
				if (slot.meshChunks.IsCreated)
				{
					for (int i = 0; i < slot.meshChunks.Length; i++)
					{
						global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPool.MeshChunk meshChunk = slot.meshChunks[i];
						if (meshChunk.vertexAlloc.valid)
						{
							m_VertexAllocator.FreeAllocation(in meshChunk.vertexAlloc);
						}
						if (meshChunk.indexAlloc.valid)
						{
							m_IndexAllocator.FreeAllocation(in meshChunk.indexAlloc);
						}
					}
					slot.meshChunks.Dispose();
				}
			}
			slot = global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPool.GeometrySlot.Invalid;
		}

		private void DeallocateGeometrySlot(int geoSlotHandle)
		{
			global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPool.GeometrySlot slot = m_GeoSlots[geoSlotHandle];
			slot.refCount--;
			if (slot.refCount == 0)
			{
				m_MeshHashToGeoSlot.Remove(slot.hash);
				DeallocateGeometrySlot(ref slot);
				m_FreeGeoSlots.Add(in geoSlotHandle);
			}
			m_GeoSlots[geoSlotHandle] = slot;
		}

		private bool AllocateGeo(global::UnityEngine.Mesh mesh, out int allocationHandle)
		{
			uint hashCode = (uint)mesh.GetHashCode();
			int num = 0;
			for (int i = 0; i < mesh.subMeshCount; i++)
			{
				num += (int)mesh.GetIndexCount(i);
			}
			if (m_MeshHashToGeoSlot.TryGetValue(hashCode, out allocationHandle))
			{
				global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPool.GeometrySlot value = m_GeoSlots[allocationHandle];
				value.refCount++;
				m_GeoSlots[allocationHandle] = value;
				return true;
			}
			allocationHandle = -1;
			global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPool.GeometrySlot slot = global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPool.GeometrySlot.Invalid;
			slot.refCount = 1u;
			slot.hash = hashCode;
			bool flag = true;
			if (mesh.subMeshCount > 0)
			{
				slot.meshChunkTableAlloc = m_MeshChunkTableAllocator.Allocate(mesh.subMeshCount);
				if (!slot.meshChunkTableAlloc.valid)
				{
					slot.meshChunkTableAlloc = m_MeshChunkTableAllocator.GrowAndAllocate(mesh.subMeshCount, (int)(global::UnityEngine.Rendering.UnifiedRayTracing.GraphicsHelpers.MaxGraphicsBufferSizeInBytes / GetMeshChunkTableEntryByteSize()), out var oldCapacity, out var newCapacity);
					if (!slot.meshChunkTableAlloc.valid)
					{
						throw new global::UnityEngine.Rendering.UnifiedRayTracing.UnifiedRayTracingException($"Can't allocate a GraphicsBuffer bigger than {(global::UnityEngine.Rendering.UnifiedRayTracing.GraphicsHelpers.MaxGraphicsBufferSizeInGigaBytes):F1}GB", global::UnityEngine.Rendering.UnifiedRayTracing.UnifiedRayTracingError.GraphicsBufferAllocationFailed);
					}
					global::UnityEngine.Rendering.UnifiedRayTracing.GraphicsHelpers.ReallocateBuffer(m_CopyShader, oldCapacity, newCapacity, GetMeshChunkTableEntryByteSize(), ref m_GlobalMeshChunkTableEntryBuffer);
					m_MaxMeshChunkTableEntriesCount = newCapacity;
				}
				slot.meshChunks = new global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPool.MeshChunk>(mesh.subMeshCount, global::Unity.Collections.Allocator.Persistent);
				for (int j = 0; j < mesh.subMeshCount; j++)
				{
					global::UnityEngine.Rendering.SubMeshDescriptor subMesh = mesh.GetSubMesh(j);
					global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPool.MeshChunk invalid = global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPool.MeshChunk.Invalid;
					invalid.vertexAlloc = m_VertexAllocator.Allocate(subMesh.vertexCount);
					if (!invalid.vertexAlloc.valid)
					{
						invalid.vertexAlloc = m_VertexAllocator.GrowAndAllocate(subMesh.vertexCount, (int)(global::UnityEngine.Rendering.UnifiedRayTracing.GraphicsHelpers.MaxGraphicsBufferSizeInBytes / GetVertexByteSize()), out var oldCapacity2, out var newCapacity2);
						if (!invalid.vertexAlloc.valid)
						{
							throw new global::UnityEngine.Rendering.UnifiedRayTracing.UnifiedRayTracingException($"Can't allocate a GraphicsBuffer bigger than {(global::UnityEngine.Rendering.UnifiedRayTracing.GraphicsHelpers.MaxGraphicsBufferSizeInGigaBytes):F1}GB", global::UnityEngine.Rendering.UnifiedRayTracing.UnifiedRayTracingError.GraphicsBufferAllocationFailed);
						}
						global::UnityEngine.Rendering.UnifiedRayTracing.GraphicsHelpers.ReallocateBuffer(m_CopyShader, oldCapacity2, newCapacity2, GetVertexByteSize(), ref m_GlobalVertexBuffer);
						m_MaxVertCounts = newCapacity2;
					}
					invalid.indexAlloc = m_IndexAllocator.Allocate(subMesh.indexCount);
					if (!invalid.indexAlloc.valid)
					{
						invalid.indexAlloc = m_IndexAllocator.GrowAndAllocate(subMesh.indexCount, (int)(global::UnityEngine.Rendering.UnifiedRayTracing.GraphicsHelpers.MaxGraphicsBufferSizeInBytes / 4), out var oldCapacity3, out var newCapacity3);
						if (!invalid.indexAlloc.valid)
						{
							throw new global::UnityEngine.Rendering.UnifiedRayTracing.UnifiedRayTracingException($"Can't allocate a GraphicsBuffer bigger than {(global::UnityEngine.Rendering.UnifiedRayTracing.GraphicsHelpers.MaxGraphicsBufferSizeInGigaBytes):F1}GB", global::UnityEngine.Rendering.UnifiedRayTracing.UnifiedRayTracingError.GraphicsBufferAllocationFailed);
						}
						global::UnityEngine.Rendering.UnifiedRayTracing.GraphicsHelpers.ReallocateBuffer(m_CopyShader, oldCapacity3, newCapacity3, 4, ref m_GlobalIndexBuffer);
						m_MaxIndexCounts = newCapacity3;
					}
					slot.meshChunks[j] = invalid;
				}
			}
			if (!flag)
			{
				DeallocateGeometrySlot(ref slot);
				return false;
			}
			if (m_FreeGeoSlots.IsEmpty)
			{
				allocationHandle = m_GeoSlots.Count;
				m_GeoSlots.Add(slot);
			}
			else
			{
				allocationHandle = m_FreeGeoSlots[m_FreeGeoSlots.Length - 1];
				m_FreeGeoSlots.RemoveAtSwapBack(m_FreeGeoSlots.Length - 1);
				m_GeoSlots[allocationHandle] = slot;
			}
			m_MeshHashToGeoSlot.Add(slot.hash, allocationHandle);
			return true;
		}

		private void DeallocateGeoPoolEntrySlot(global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPoolHandle handle)
		{
			global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPool.GeoPoolEntrySlot geoPoolEntrySlot = m_GeoPoolEntrySlots[handle.index];
			geoPoolEntrySlot.refCount--;
			if (geoPoolEntrySlot.refCount == 0)
			{
				m_GeoPoolEntryHashToSlot.Remove(geoPoolEntrySlot.hash);
				DeallocateGeoPoolEntrySlot(ref geoPoolEntrySlot);
				m_FreeGeoPoolEntrySlots.Add(in handle);
			}
			m_GeoPoolEntrySlots[handle.index] = geoPoolEntrySlot;
		}

		private void DeallocateGeoPoolEntrySlot(ref global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPool.GeoPoolEntrySlot geoPoolEntrySlot)
		{
			if (geoPoolEntrySlot.geoSlotHandle != -1)
			{
				DeallocateGeometrySlot(geoPoolEntrySlot.geoSlotHandle);
			}
			geoPoolEntrySlot = global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPool.GeoPoolEntrySlot.Invalid;
		}

		public global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPoolEntryInfo GetEntryInfo(global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPoolHandle handle)
		{
			if (!handle.valid)
			{
				return global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPoolEntryInfo.NewDefault();
			}
			global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPool.GeoPoolEntrySlot geoPoolEntrySlot = m_GeoPoolEntrySlots[handle.index];
			if (!geoPoolEntrySlot.valid)
			{
				return global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPoolEntryInfo.NewDefault();
			}
			if (geoPoolEntrySlot.geoSlotHandle == -1)
			{
				global::UnityEngine.Debug.LogErrorFormat("Found invalid geometry slot handle with handle id {0}.", handle.index);
			}
			return new global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPoolEntryInfo
			{
				valid = geoPoolEntrySlot.valid,
				refCount = geoPoolEntrySlot.refCount
			};
		}

		public global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPool.GeometrySlot GetEntryGeomAllocation(global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPoolHandle handle)
		{
			global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPool.GeoPoolEntrySlot geoPoolEntrySlot = m_GeoPoolEntrySlots[handle.index];
			return m_GeoSlots[geoPoolEntrySlot.geoSlotHandle];
		}

		public int GetInstanceGeometryIndex(global::UnityEngine.Mesh mesh)
		{
			return GetEntryGeomAllocation(GetHandle(mesh)).meshChunkTableAlloc.block.offset;
		}

		private void UpdateGeoGpuState(global::UnityEngine.Mesh mesh, global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPoolHandle handle)
		{
			global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPool.GeoPoolEntrySlot geoPoolEntrySlot = m_GeoPoolEntrySlots[handle.index];
			global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPool.GeometrySlot value = m_GeoSlots[geoPoolEntrySlot.geoSlotHandle];
			global::UnityEngine.Rendering.CommandBuffer commandBuffer = AllocateCommandBuffer();
			if (!value.hasGPUData)
			{
				global::UnityEngine.GraphicsBuffer inputBuffer = LoadIndexBuffer(mesh);
				LoadVertexAttribInfo(mesh, global::UnityEngine.Rendering.VertexAttribute.Position, out var output);
				LoadVertexAttribInfo(mesh, global::UnityEngine.Rendering.VertexAttribute.TexCoord0, out var output2);
				LoadVertexAttribInfo(mesh, global::UnityEngine.Rendering.VertexAttribute.TexCoord1, out var output3);
				LoadVertexAttribInfo(mesh, global::UnityEngine.Rendering.VertexAttribute.Normal, out var output4);
				global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.UnifiedRayTracing.GeoPoolMeshChunk> data = new global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.UnifiedRayTracing.GeoPoolMeshChunk>(value.meshChunks.Length, global::Unity.Collections.Allocator.Temp);
				for (int i = 0; i < mesh.subMeshCount; i++)
				{
					global::UnityEngine.Rendering.SubMeshDescriptor subMesh = mesh.GetSubMesh(i);
					global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPool.MeshChunk meshChunk = value.meshChunks[i];
					AddVertexUpdateCommand(commandBuffer, subMesh.baseVertex + subMesh.firstVertex, in output, in output2, in output3, in output4, in meshChunk.vertexAlloc, m_GlobalVertexBuffer);
					AddIndexUpdateCommand(commandBuffer, mesh.indexFormat, in inputBuffer, in meshChunk.indexAlloc, subMesh.firstVertex, subMesh.indexStart, subMesh.indexCount, 0, m_GlobalIndexBuffer);
					data[i] = meshChunk.EncodeGPUEntry();
				}
				commandBuffer.SetBufferData(m_GlobalMeshChunkTableEntryBuffer, data, 0, value.meshChunkTableAlloc.block.offset, data.Length);
				data.Dispose();
				value.hasGPUData = true;
				m_GeoSlots[geoPoolEntrySlot.geoSlotHandle] = value;
			}
		}

		private uint FNVHash(uint prevHash, uint dword)
		{
			for (int i = 0; i < 4; i++)
			{
				prevHash ^= (dword >> i * 8) & 0xFF;
				prevHash *= 2166136261u;
			}
			return prevHash;
		}

		private uint CalculateClusterHash(global::UnityEngine.Mesh mesh, global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPoolSubmeshData[] submeshData)
		{
			uint num = (uint)mesh.GetHashCode();
			if (submeshData != null)
			{
				for (int i = 0; i < submeshData.Length; i++)
				{
					global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPoolSubmeshData geometryPoolSubmeshData = submeshData[i];
					num = FNVHash(num, (uint)geometryPoolSubmeshData.submeshIndex);
					num = FNVHash(num, (!(geometryPoolSubmeshData.material == null)) ? ((uint)geometryPoolSubmeshData.material.GetHashCode()) : 0u);
				}
			}
			return num;
		}

		public global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPoolHandle GetHandle(global::UnityEngine.Mesh mesh)
		{
			uint key = CalculateClusterHash(mesh, null);
			if (m_GeoPoolEntryHashToSlot.TryGetValue(key, out var item))
			{
				return item;
			}
			return global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPoolHandle.Invalid;
		}

		private static int FindSubmeshEntryInDesc(int submeshIndex, in global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPoolSubmeshData[] submeshData)
		{
			if (submeshData == null)
			{
				return -1;
			}
			for (int i = 0; i < submeshData.Length; i++)
			{
				if (submeshData[i].submeshIndex == submeshIndex)
				{
					return i;
				}
			}
			return -1;
		}

		public bool Register(global::UnityEngine.Mesh mesh, out global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPoolHandle outHandle)
		{
			global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPoolEntryDesc entryDesc = new global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPoolEntryDesc
			{
				mesh = mesh,
				submeshData = null
			};
			return Register(in entryDesc, out outHandle);
		}

		public bool Register(in global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPoolEntryDesc entryDesc, out global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPoolHandle outHandle)
		{
			outHandle = global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPoolHandle.Invalid;
			if (entryDesc.mesh == null)
			{
				return false;
			}
			global::UnityEngine.Mesh mesh = entryDesc.mesh;
			uint num = CalculateClusterHash(entryDesc.mesh, entryDesc.submeshData);
			if (m_GeoPoolEntryHashToSlot.TryGetValue(num, out outHandle))
			{
				global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPool.GeoPoolEntrySlot value = m_GeoPoolEntrySlots[outHandle.index];
				_ = m_GeoSlots[value.geoSlotHandle];
				value.refCount++;
				m_GeoPoolEntrySlots[outHandle.index] = value;
				return true;
			}
			global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPool.GeoPoolEntrySlot value2 = global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPool.GeoPoolEntrySlot.Invalid;
			value2.refCount = 1u;
			value2.hash = num;
			global::System.Collections.Generic.List<global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPoolSubmeshData> list = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPoolSubmeshData>(mesh.subMeshCount);
			if (mesh.subMeshCount > 0 && entryDesc.submeshData != null)
			{
				for (int i = 0; i < mesh.subMeshCount; i++)
				{
					int num2 = FindSubmeshEntryInDesc(i, in entryDesc.submeshData);
					if (num2 == -1)
					{
						global::UnityEngine.Debug.LogErrorFormat("Could not find submesh index {0} for mesh entry descriptor of mesh {1}.", i, mesh.name);
					}
					else
					{
						list.Add(entryDesc.submeshData[num2]);
					}
				}
			}
			if (!AllocateGeo(mesh, out value2.geoSlotHandle))
			{
				DeallocateGeoPoolEntrySlot(ref value2);
				return false;
			}
			if (m_FreeGeoPoolEntrySlots.IsEmpty)
			{
				outHandle = new global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPoolHandle
				{
					index = m_GeoPoolEntrySlots.Length
				};
				m_GeoPoolEntrySlots.Add(in value2);
			}
			else
			{
				outHandle = m_FreeGeoPoolEntrySlots[m_FreeGeoPoolEntrySlots.Length - 1];
				m_FreeGeoPoolEntrySlots.RemoveAtSwapBack(m_FreeGeoPoolEntrySlots.Length - 1);
				m_GeoPoolEntrySlots[outHandle.index] = value2;
			}
			m_GeoPoolEntryHashToSlot.Add(value2.hash, outHandle);
			UpdateGeoGpuState(mesh, outHandle);
			return true;
		}

		public void Unregister(global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPoolHandle handle)
		{
			_ = m_GeoPoolEntrySlots[handle.index];
			DeallocateGeoPoolEntrySlot(handle);
		}

		public void SendGpuCommands()
		{
			if (m_PendingCmds != 0)
			{
				global::UnityEngine.Graphics.ExecuteCommandBuffer(m_CmdBuffer);
				m_MustClearCmdBuffer = true;
				m_PendingCmds = 0;
			}
			DisposeInputBuffers();
		}

		private global::UnityEngine.GraphicsBuffer LoadIndexBuffer(global::UnityEngine.Mesh mesh)
		{
			mesh.indexBufferTarget |= global::UnityEngine.GraphicsBuffer.Target.Raw;
			mesh.vertexBufferTarget |= global::UnityEngine.GraphicsBuffer.Target.Raw;
			global::UnityEngine.GraphicsBuffer indexBuffer = mesh.GetIndexBuffer();
			m_InputBufferReferences.Add(indexBuffer);
			return indexBuffer;
		}

		private void LoadVertexAttribInfo(global::UnityEngine.Mesh mesh, global::UnityEngine.Rendering.VertexAttribute attribute, out global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPool.VertexBufferAttribInfo output)
		{
			if (!mesh.HasVertexAttribute(attribute))
			{
				output.buffer = null;
				output.stride = (output.offset = (output.byteCount = 0));
				return;
			}
			int vertexAttributeStream = mesh.GetVertexAttributeStream(attribute);
			output.stride = mesh.GetVertexBufferStride(vertexAttributeStream);
			output.offset = mesh.GetVertexAttributeOffset(attribute);
			output.byteCount = GetFormatByteCount(mesh.GetVertexAttributeFormat(attribute)) * mesh.GetVertexAttributeDimension(attribute);
			output.buffer = mesh.GetVertexBuffer(vertexAttributeStream);
			m_InputBufferReferences.Add(output.buffer);
		}

		private global::UnityEngine.Rendering.CommandBuffer AllocateCommandBuffer()
		{
			if (m_MustClearCmdBuffer)
			{
				m_CmdBuffer.Clear();
				m_MustClearCmdBuffer = false;
			}
			m_PendingCmds++;
			return m_CmdBuffer;
		}

		private void AddIndexUpdateCommand(global::UnityEngine.Rendering.CommandBuffer cmdBuffer, global::UnityEngine.Rendering.IndexFormat inputFormat, in global::UnityEngine.GraphicsBuffer inputBuffer, in global::UnityEngine.Rendering.UnifiedRayTracing.BlockAllocator.Allocation location, int firstVertex, int inputOffset, int indexCount, int outputOffset, global::UnityEngine.GraphicsBuffer outputIdxBuffer)
		{
			if (location.block.count != 0)
			{
				cmdBuffer.SetComputeIntParam(m_GeometryPoolKernelsCS, global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPool.GeoPoolShaderIDs._InputIBBaseOffset, inputOffset);
				cmdBuffer.SetComputeIntParam(m_GeometryPoolKernelsCS, global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPool.GeoPoolShaderIDs._InputIBCount, indexCount);
				cmdBuffer.SetComputeIntParam(m_GeometryPoolKernelsCS, global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPool.GeoPoolShaderIDs._InputFirstVertex, firstVertex);
				cmdBuffer.SetComputeIntParam(m_GeometryPoolKernelsCS, global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPool.GeoPoolShaderIDs._OutputIBOffset, location.block.offset + outputOffset);
				int kernelIndex = ((inputFormat == global::UnityEngine.Rendering.IndexFormat.UInt16) ? m_KernelMainUpdateIndexBuffer16 : m_KernelMainUpdateIndexBuffer32);
				cmdBuffer.SetComputeBufferParam(m_GeometryPoolKernelsCS, kernelIndex, global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPool.GeoPoolShaderIDs._InputIndexBuffer, inputBuffer);
				cmdBuffer.SetComputeBufferParam(m_GeometryPoolKernelsCS, kernelIndex, global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPool.GeoPoolShaderIDs._OutputIndexBuffer, outputIdxBuffer);
				int num = DivUp(location.block.count, 256);
				int num2 = DivUp(num, 65535);
				for (int i = 0; i < num2; i++)
				{
					int val = i * 65535 * 256;
					int threadGroupsX = global::System.Math.Min(65535, num - i * 65535);
					cmdBuffer.SetComputeIntParam(m_GeometryPoolKernelsCS, global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPool.GeoPoolShaderIDs._DispatchIndexOffset, val);
					cmdBuffer.DispatchCompute(m_GeometryPoolKernelsCS, kernelIndex, threadGroupsX, 1, 1);
				}
			}
		}

		private void AddVertexUpdateCommand(global::UnityEngine.Rendering.CommandBuffer cmdBuffer, int baseVertexOffset, in global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPool.VertexBufferAttribInfo pos, in global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPool.VertexBufferAttribInfo uv0, in global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPool.VertexBufferAttribInfo uv1, in global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPool.VertexBufferAttribInfo n, in global::UnityEngine.Rendering.UnifiedRayTracing.BlockAllocator.Allocation location, global::UnityEngine.GraphicsBuffer outputVertexBuffer)
		{
			if (location.block.count != 0)
			{
				global::UnityEngine.Rendering.UnifiedRayTracing.GeoPoolVertexAttribs geoPoolVertexAttribs = (global::UnityEngine.Rendering.UnifiedRayTracing.GeoPoolVertexAttribs)0;
				if (pos.valid)
				{
					geoPoolVertexAttribs |= global::UnityEngine.Rendering.UnifiedRayTracing.GeoPoolVertexAttribs.Position;
				}
				if (uv0.valid)
				{
					geoPoolVertexAttribs |= global::UnityEngine.Rendering.UnifiedRayTracing.GeoPoolVertexAttribs.Uv0;
				}
				if (uv1.valid)
				{
					geoPoolVertexAttribs |= global::UnityEngine.Rendering.UnifiedRayTracing.GeoPoolVertexAttribs.Uv1;
				}
				if (n.valid)
				{
					geoPoolVertexAttribs |= global::UnityEngine.Rendering.UnifiedRayTracing.GeoPoolVertexAttribs.Normal;
				}
				int count = location.block.count;
				cmdBuffer.SetComputeIntParam(m_GeometryPoolKernelsCS, global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPool.GeoPoolShaderIDs._InputVBCount, count);
				cmdBuffer.SetComputeIntParam(m_GeometryPoolKernelsCS, global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPool.GeoPoolShaderIDs._InputBaseVertexOffset, baseVertexOffset);
				cmdBuffer.SetComputeIntParam(m_GeometryPoolKernelsCS, global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPool.GeoPoolShaderIDs._OutputVBSize, m_MaxVertCounts);
				cmdBuffer.SetComputeIntParam(m_GeometryPoolKernelsCS, global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPool.GeoPoolShaderIDs._OutputVBOffset, location.block.offset);
				cmdBuffer.SetComputeIntParam(m_GeometryPoolKernelsCS, global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPool.GeoPoolShaderIDs._InputPosBufferStride, pos.stride);
				cmdBuffer.SetComputeIntParam(m_GeometryPoolKernelsCS, global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPool.GeoPoolShaderIDs._InputPosBufferOffset, pos.offset);
				cmdBuffer.SetComputeIntParam(m_GeometryPoolKernelsCS, global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPool.GeoPoolShaderIDs._InputUv0BufferStride, uv0.stride);
				cmdBuffer.SetComputeIntParam(m_GeometryPoolKernelsCS, global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPool.GeoPoolShaderIDs._InputUv0BufferOffset, uv0.offset);
				cmdBuffer.SetComputeIntParam(m_GeometryPoolKernelsCS, global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPool.GeoPoolShaderIDs._InputUv1BufferStride, uv1.stride);
				cmdBuffer.SetComputeIntParam(m_GeometryPoolKernelsCS, global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPool.GeoPoolShaderIDs._InputUv1BufferOffset, uv1.offset);
				cmdBuffer.SetComputeIntParam(m_GeometryPoolKernelsCS, global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPool.GeoPoolShaderIDs._InputNormalBufferStride, n.stride);
				cmdBuffer.SetComputeIntParam(m_GeometryPoolKernelsCS, global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPool.GeoPoolShaderIDs._InputNormalBufferOffset, n.offset);
				cmdBuffer.SetComputeIntParam(m_GeometryPoolKernelsCS, global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPool.GeoPoolShaderIDs._AttributesMask, (int)geoPoolVertexAttribs);
				int kernelMainUpdateVertexBuffer = m_KernelMainUpdateVertexBuffer;
				cmdBuffer.SetComputeBufferParam(m_GeometryPoolKernelsCS, kernelMainUpdateVertexBuffer, global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPool.GeoPoolShaderIDs._PosBuffer, pos.valid ? pos.buffer : m_DummyBuffer);
				cmdBuffer.SetComputeBufferParam(m_GeometryPoolKernelsCS, kernelMainUpdateVertexBuffer, global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPool.GeoPoolShaderIDs._Uv0Buffer, uv0.valid ? uv0.buffer : m_DummyBuffer);
				cmdBuffer.SetComputeBufferParam(m_GeometryPoolKernelsCS, kernelMainUpdateVertexBuffer, global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPool.GeoPoolShaderIDs._Uv1Buffer, uv1.valid ? uv1.buffer : m_DummyBuffer);
				cmdBuffer.SetComputeBufferParam(m_GeometryPoolKernelsCS, kernelMainUpdateVertexBuffer, global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPool.GeoPoolShaderIDs._NormalBuffer, n.valid ? n.buffer : m_DummyBuffer);
				cmdBuffer.SetComputeBufferParam(m_GeometryPoolKernelsCS, kernelMainUpdateVertexBuffer, global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPool.GeoPoolShaderIDs._OutputVB, outputVertexBuffer);
				int num = DivUp(count, 256);
				int num2 = DivUp(num, 65535);
				for (int i = 0; i < num2; i++)
				{
					int val = i * 65535 * 256;
					int threadGroupsX = global::System.Math.Min(65535, num - i * 65535);
					cmdBuffer.SetComputeIntParam(m_GeometryPoolKernelsCS, global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPool.GeoPoolShaderIDs._DispatchVertexOffset, val);
					cmdBuffer.DispatchCompute(m_GeometryPoolKernelsCS, kernelMainUpdateVertexBuffer, threadGroupsX, 1, 1);
				}
			}
		}
	}
}
