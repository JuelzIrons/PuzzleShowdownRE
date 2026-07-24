namespace UnityEngine.Rendering.UnifiedRayTracing
{
	internal class ComputeRayTracingAccelStruct : global::UnityEngine.Rendering.UnifiedRayTracing.IRayTracingAccelStruct, global::System.IDisposable
	{
		private struct Triangle
		{
			public global::Unity.Mathematics.float3 v0;

			public global::Unity.Mathematics.float3 v1;

			public global::Unity.Mathematics.float3 v2;
		}

		private sealed class RadeonRaysInstance
		{
			public (int mesh, int subMeshIndex) geomKey;

			public global::UnityEngine.Rendering.UnifiedRayTracing.ComputeRayTracingAccelStruct.MeshBlas blas;

			public uint instanceMask;

			public bool triangleCullingEnabled;

			public bool invertTriangleCulling;

			public uint userInstanceID;

			public bool opaqueGeometry;

			public global::UnityEngine.Rendering.RadeonRays.Transform localToWorldTransform;
		}

		private sealed class MeshBlas
		{
			public global::UnityEngine.Rendering.RadeonRays.MeshBuildInfo buildInfo;

			public global::UnityEngine.Rendering.UnifiedRayTracing.BlockAllocator.Allocation bvhAlloc;

			public global::UnityEngine.Rendering.UnifiedRayTracing.BlockAllocator.Allocation bvhLeavesAlloc;

			public global::UnityEngine.Rendering.UnifiedRayTracing.BlockAllocator.Allocation blasVertices;

			public bool bvhBuilt;

			private uint refCount;

			public void IncRef()
			{
				refCount++;
			}

			public void DecRef()
			{
				refCount--;
			}

			public bool IsUnreferenced()
			{
				return refCount == 0;
			}
		}

		private readonly uint m_HandleObfuscation = (uint)global::UnityEngine.Random.Range(int.MinValue, int.MaxValue);

		private readonly global::UnityEngine.Rendering.RadeonRays.RadeonRaysAPI m_RadeonRaysAPI;

		private readonly global::UnityEngine.Rendering.UnifiedRayTracing.BuildFlags m_BuildFlags;

		private readonly global::UnityEngine.Rendering.UnifiedRayTracing.ReferenceCounter m_Counter;

		private readonly global::System.Collections.Generic.Dictionary<(int mesh, int subMeshIndex), global::UnityEngine.Rendering.UnifiedRayTracing.ComputeRayTracingAccelStruct.MeshBlas> m_Blases;

		internal global::UnityEngine.Rendering.UnifiedRayTracing.BlockAllocator m_BlasAllocator;

		private global::UnityEngine.GraphicsBuffer m_BlasBuffer;

		internal global::UnityEngine.Rendering.UnifiedRayTracing.BlockAllocator m_BlasLeavesAllocator;

		private global::UnityEngine.GraphicsBuffer m_BlasLeavesBuffer;

		private readonly global::UnityEngine.Rendering.UnifiedRayTracing.BLASPositionsPool m_BlasPositions;

		private global::UnityEngine.Rendering.RadeonRays.TopLevelAccelStruct? m_TopLevelAccelStruct;

		private readonly global::UnityEngine.ComputeShader m_CopyShader;

		private readonly global::System.Collections.Generic.Dictionary<int, global::UnityEngine.Rendering.UnifiedRayTracing.ComputeRayTracingAccelStruct.RadeonRaysInstance> m_RadeonInstances = new global::System.Collections.Generic.Dictionary<int, global::UnityEngine.Rendering.UnifiedRayTracing.ComputeRayTracingAccelStruct.RadeonRaysInstance>();

		private readonly global::System.Collections.Generic.Queue<uint> m_FreeHandles = new global::System.Collections.Generic.Queue<uint>();

		internal global::UnityEngine.GraphicsBuffer topLevelBvhBuffer => m_TopLevelAccelStruct?.topLevelBvh;

		internal global::UnityEngine.GraphicsBuffer bottomLevelBvhBuffer => m_TopLevelAccelStruct?.bottomLevelBvhs;

		internal global::UnityEngine.GraphicsBuffer instanceInfoBuffer => m_TopLevelAccelStruct?.instanceInfos;

		internal ComputeRayTracingAccelStruct(global::UnityEngine.Rendering.UnifiedRayTracing.AccelerationStructureOptions options, global::UnityEngine.Rendering.UnifiedRayTracing.RayTracingResources resources, global::UnityEngine.Rendering.UnifiedRayTracing.ReferenceCounter counter, int blasBufferInitialSizeBytes = 67108864)
		{
			m_CopyShader = resources.copyBuffer;
			m_RadeonRaysAPI = new global::UnityEngine.Rendering.RadeonRays.RadeonRaysAPI(new global::UnityEngine.Rendering.RadeonRays.RadeonRaysShaders
			{
				bitHistogram = resources.bitHistogram,
				blockReducePart = resources.blockReducePart,
				blockScan = resources.blockScan,
				buildHlbvh = resources.buildHlbvh,
				restructureBvh = resources.restructureBvh,
				scatter = resources.scatter
			});
			m_BuildFlags = options.buildFlags;
			m_Blases = new global::System.Collections.Generic.Dictionary<(int, int), global::UnityEngine.Rendering.UnifiedRayTracing.ComputeRayTracingAccelStruct.MeshBlas>();
			int num = blasBufferInitialSizeBytes / global::UnityEngine.Rendering.RadeonRays.RadeonRaysAPI.BvhInternalNodeSizeInBytes();
			m_BlasBuffer = new global::UnityEngine.GraphicsBuffer(global::UnityEngine.GraphicsBuffer.Target.Structured, num, global::UnityEngine.Rendering.RadeonRays.RadeonRaysAPI.BvhInternalNodeSizeInBytes());
			m_BlasLeavesBuffer = new global::UnityEngine.GraphicsBuffer(global::UnityEngine.GraphicsBuffer.Target.Structured, num, global::UnityEngine.Rendering.RadeonRays.RadeonRaysAPI.BvhLeafNodeSizeInBytes());
			m_BlasPositions = new global::UnityEngine.Rendering.UnifiedRayTracing.BLASPositionsPool(resources.copyPositions, resources.copyBuffer);
			m_BlasAllocator = default(global::UnityEngine.Rendering.UnifiedRayTracing.BlockAllocator);
			m_BlasAllocator.Initialize(num);
			m_BlasLeavesAllocator = default(global::UnityEngine.Rendering.UnifiedRayTracing.BlockAllocator);
			m_BlasLeavesAllocator.Initialize(num);
			m_Counter = counter;
			m_Counter.Inc();
		}

		public void Dispose()
		{
			foreach (global::UnityEngine.Rendering.UnifiedRayTracing.ComputeRayTracingAccelStruct.MeshBlas value in m_Blases.Values)
			{
				if (value.buildInfo.triangleIndices != null)
				{
					value.buildInfo.triangleIndices.Dispose();
				}
			}
			m_Counter.Dec();
			m_RadeonRaysAPI.Dispose();
			m_BlasBuffer.Dispose();
			m_BlasLeavesBuffer.Dispose();
			m_BlasPositions.Dispose();
			m_BlasAllocator.Dispose();
			m_BlasLeavesAllocator.Dispose();
			m_TopLevelAccelStruct?.Dispose();
		}

		public int AddInstance(global::UnityEngine.Rendering.UnifiedRayTracing.MeshInstanceDesc meshInstance)
		{
			global::UnityEngine.Rendering.UnifiedRayTracing.ComputeRayTracingAccelStruct.MeshBlas orAllocateMeshBlas = GetOrAllocateMeshBlas(meshInstance.mesh, meshInstance.subMeshIndex);
			orAllocateMeshBlas.IncRef();
			FreeTopLevelAccelStruct();
			int num = NewHandle();
			m_RadeonInstances.Add(num, new global::UnityEngine.Rendering.UnifiedRayTracing.ComputeRayTracingAccelStruct.RadeonRaysInstance
			{
				geomKey = (mesh: meshInstance.mesh.GetHashCode(), subMeshIndex: meshInstance.subMeshIndex),
				blas = orAllocateMeshBlas,
				instanceMask = meshInstance.mask,
				triangleCullingEnabled = meshInstance.enableTriangleCulling,
				invertTriangleCulling = meshInstance.frontTriangleCounterClockwise,
				userInstanceID = ((meshInstance.instanceID == uint.MaxValue) ? ((uint)num) : meshInstance.instanceID),
				opaqueGeometry = meshInstance.opaqueGeometry,
				localToWorldTransform = ConvertTranform(meshInstance.localToWorldMatrix)
			});
			return num;
		}

		public void RemoveInstance(int instanceHandle)
		{
			ReleaseHandle(instanceHandle);
			m_RadeonInstances.Remove(instanceHandle, out var value);
			global::UnityEngine.Rendering.UnifiedRayTracing.ComputeRayTracingAccelStruct.MeshBlas blas = value.blas;
			blas.DecRef();
			if (blas.IsUnreferenced())
			{
				DeleteMeshBlas(value.geomKey, blas);
			}
			FreeTopLevelAccelStruct();
		}

		public void ClearInstances()
		{
			m_FreeHandles.Clear();
			m_RadeonInstances.Clear();
			foreach (global::UnityEngine.Rendering.UnifiedRayTracing.ComputeRayTracingAccelStruct.MeshBlas value in m_Blases.Values)
			{
				if (value.buildInfo.triangleIndices != null)
				{
					value.buildInfo.triangleIndices.Dispose();
				}
			}
			m_Blases.Clear();
			m_BlasPositions.Clear();
			int capacity = m_BlasAllocator.capacity;
			m_BlasAllocator.Dispose();
			m_BlasAllocator = default(global::UnityEngine.Rendering.UnifiedRayTracing.BlockAllocator);
			m_BlasAllocator.Initialize(capacity);
			capacity = m_BlasLeavesAllocator.capacity;
			m_BlasLeavesAllocator.Dispose();
			m_BlasLeavesAllocator = default(global::UnityEngine.Rendering.UnifiedRayTracing.BlockAllocator);
			m_BlasLeavesAllocator.Initialize(capacity);
			FreeTopLevelAccelStruct();
		}

		public void UpdateInstanceTransform(int instanceHandle, global::UnityEngine.Matrix4x4 localToWorldMatrix)
		{
			m_RadeonInstances[instanceHandle].localToWorldTransform = ConvertTranform(localToWorldMatrix);
			FreeTopLevelAccelStruct();
		}

		public void UpdateInstanceID(int instanceHandle, uint instanceID)
		{
			m_RadeonInstances[instanceHandle].userInstanceID = instanceID;
			FreeTopLevelAccelStruct();
		}

		public void UpdateInstanceMask(int instanceHandle, uint mask)
		{
			m_RadeonInstances[instanceHandle].instanceMask = mask;
			FreeTopLevelAccelStruct();
		}

		public void Build(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.GraphicsBuffer scratchBuffer)
		{
			GetBuildScratchBufferRequiredSizeInBytes();
			_ = 0;
			if (!m_TopLevelAccelStruct.HasValue)
			{
				CreateBvh(cmd, scratchBuffer);
			}
		}

		public ulong GetBuildScratchBufferRequiredSizeInBytes()
		{
			return GetBvhBuildScratchBufferSizeInDwords() * 4;
		}

		private void FreeTopLevelAccelStruct()
		{
			m_TopLevelAccelStruct?.Dispose();
			m_TopLevelAccelStruct = null;
		}

		private global::UnityEngine.Rendering.UnifiedRayTracing.ComputeRayTracingAccelStruct.MeshBlas GetOrAllocateMeshBlas(global::UnityEngine.Mesh mesh, int subMeshIndex)
		{
			if (m_Blases.TryGetValue((mesh.GetHashCode(), subMeshIndex), out var value))
			{
				return value;
			}
			value = new global::UnityEngine.Rendering.UnifiedRayTracing.ComputeRayTracingAccelStruct.MeshBlas();
			AllocateBlas(mesh, subMeshIndex, value);
			m_Blases[(mesh.GetHashCode(), subMeshIndex)] = value;
			return value;
		}

		private void AllocateBlas(global::UnityEngine.Mesh mesh, int submeshIndex, global::UnityEngine.Rendering.UnifiedRayTracing.ComputeRayTracingAccelStruct.MeshBlas blas)
		{
			blas.blasVertices = global::UnityEngine.Rendering.UnifiedRayTracing.BlockAllocator.Allocation.Invalid;
			blas.bvhAlloc = global::UnityEngine.Rendering.UnifiedRayTracing.BlockAllocator.Allocation.Invalid;
			blas.bvhLeavesAlloc = global::UnityEngine.Rendering.UnifiedRayTracing.BlockAllocator.Allocation.Invalid;
			int num = global::UnityEngine.Rendering.RadeonRays.RadeonRaysAPI.BvhInternalNodeSizeInDwords();
			mesh.indexBufferTarget |= global::UnityEngine.GraphicsBuffer.Target.Raw;
			mesh.vertexBufferTarget |= global::UnityEngine.GraphicsBuffer.Target.Raw;
			global::UnityEngine.Rendering.SubMeshDescriptor subMesh = mesh.GetSubMesh(submeshIndex);
			int stride;
			int offset;
			using global::UnityEngine.GraphicsBuffer vertices = LoadPositionBuffer(mesh, out stride, out offset);
			global::UnityEngine.GraphicsBuffer graphicsBuffer = null;
			graphicsBuffer = LoadIndexBuffer(mesh);
			global::UnityEngine.Rendering.UnifiedRayTracing.VertexBufferChunk info = new global::UnityEngine.Rendering.UnifiedRayTracing.VertexBufferChunk
			{
				vertices = vertices,
				verticesStartOffset = offset,
				baseVertex = subMesh.baseVertex + subMesh.firstVertex,
				vertexCount = (uint)subMesh.vertexCount,
				vertexStride = (uint)stride
			};
			m_BlasPositions.Add(info, out blas.blasVertices);
			global::UnityEngine.Rendering.RadeonRays.MeshBuildInfo buildInfo = new global::UnityEngine.Rendering.RadeonRays.MeshBuildInfo
			{
				vertices = m_BlasPositions.VertexBuffer,
				verticesStartOffset = blas.blasVertices.block.offset * 3,
				baseVertex = 0,
				triangleIndices = graphicsBuffer,
				vertexCount = (uint)blas.blasVertices.block.count,
				triangleCount = (uint)subMesh.indexCount / 3u,
				indicesStartOffset = subMesh.indexStart,
				baseIndex = -subMesh.firstVertex,
				indexFormat = ((mesh.indexFormat != global::UnityEngine.Rendering.IndexFormat.UInt32) ? global::UnityEngine.Rendering.RadeonRays.IndexFormat.Int16 : global::UnityEngine.Rendering.RadeonRays.IndexFormat.Int32),
				vertexStride = 3u
			};
			blas.buildInfo = buildInfo;
			try
			{
				ulong num2 = m_RadeonRaysAPI.GetMeshBuildMemoryRequirements(buildInfo, ConvertFlagsToGpuBuild(m_BuildFlags)).bvhSizeInDwords / (ulong)num;
				if (num2 > int.MaxValue)
				{
					throw new global::UnityEngine.Rendering.UnifiedRayTracing.UnifiedRayTracingException($"Can't allocate a GraphicsBuffer bigger than {(global::UnityEngine.Rendering.UnifiedRayTracing.GraphicsHelpers.MaxGraphicsBufferSizeInGigaBytes):F1}GB", global::UnityEngine.Rendering.UnifiedRayTracing.UnifiedRayTracingError.GraphicsBufferAllocationFailed);
				}
				blas.bvhAlloc = AllocateBlasInternalNodes((int)num2);
				blas.bvhLeavesAlloc = AllocateBlasLeafNodes((int)buildInfo.triangleCount);
			}
			catch (global::UnityEngine.Rendering.UnifiedRayTracing.UnifiedRayTracingException)
			{
				if (blas.blasVertices.valid)
				{
					m_BlasPositions.Remove(ref blas.blasVertices);
				}
				if (blas.bvhAlloc.valid)
				{
					m_BlasAllocator.FreeAllocation(in blas.bvhAlloc);
				}
				if (blas.bvhLeavesAlloc.valid)
				{
					m_BlasAllocator.FreeAllocation(in blas.bvhLeavesAlloc);
				}
				throw;
			}
		}

		private global::UnityEngine.GraphicsBuffer LoadIndexBuffer(global::UnityEngine.Mesh mesh)
		{
			return mesh.GetIndexBuffer();
		}

		private global::UnityEngine.GraphicsBuffer LoadPositionBuffer(global::UnityEngine.Mesh mesh, out int stride, out int offset)
		{
			global::UnityEngine.Rendering.VertexAttribute attr = global::UnityEngine.Rendering.VertexAttribute.Position;
			int vertexAttributeStream = mesh.GetVertexAttributeStream(attr);
			stride = mesh.GetVertexBufferStride(vertexAttributeStream) / 4;
			offset = mesh.GetVertexAttributeOffset(attr) / 4;
			return mesh.GetVertexBuffer(vertexAttributeStream);
		}

		private void DeleteMeshBlas((int mesh, int subMeshIndex) geomKey, global::UnityEngine.Rendering.UnifiedRayTracing.ComputeRayTracingAccelStruct.MeshBlas blas)
		{
			m_BlasAllocator.FreeAllocation(in blas.bvhAlloc);
			blas.bvhAlloc = global::UnityEngine.Rendering.UnifiedRayTracing.BlockAllocator.Allocation.Invalid;
			m_BlasLeavesAllocator.FreeAllocation(in blas.bvhLeavesAlloc);
			blas.bvhLeavesAlloc = global::UnityEngine.Rendering.UnifiedRayTracing.BlockAllocator.Allocation.Invalid;
			m_BlasPositions.Remove(ref blas.blasVertices);
			if (blas.buildInfo.triangleIndices != null)
			{
				blas.buildInfo.triangleIndices.Dispose();
			}
			m_Blases.Remove(geomKey);
		}

		private ulong GetBvhBuildScratchBufferSizeInDwords()
		{
			global::UnityEngine.Rendering.RadeonRays.RadeonRaysAPI.BvhInternalNodeSizeInDwords();
			ulong x = 0uL;
			foreach (global::System.Collections.Generic.KeyValuePair<(int, int), global::UnityEngine.Rendering.UnifiedRayTracing.ComputeRayTracingAccelStruct.MeshBlas> blase in m_Blases)
			{
				if (!blase.Value.bvhBuilt)
				{
					x = global::Unity.Mathematics.math.max(x, m_RadeonRaysAPI.GetMeshBuildMemoryRequirements(blase.Value.buildInfo, ConvertFlagsToGpuBuild(m_BuildFlags)).buildScratchSizeInDwords);
				}
			}
			ulong buildScratchSizeInDwords = m_RadeonRaysAPI.GetSceneBuildMemoryRequirements((uint)m_RadeonInstances.Count).buildScratchSizeInDwords;
			x = global::Unity.Mathematics.math.max(x, buildScratchSizeInDwords);
			return global::Unity.Mathematics.math.max(4uL, x);
		}

		private void CreateBvh(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.GraphicsBuffer scratchBuffer)
		{
			BuildMissingBottomLevelAccelStructs(cmd, scratchBuffer);
			BuildTopLevelAccelStruct(cmd, scratchBuffer);
		}

		private void BuildMissingBottomLevelAccelStructs(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.GraphicsBuffer scratchBuffer)
		{
			foreach (global::UnityEngine.Rendering.UnifiedRayTracing.ComputeRayTracingAccelStruct.MeshBlas value in m_Blases.Values)
			{
				if (!value.bvhBuilt)
				{
					value.buildInfo.vertices = m_BlasPositions.VertexBuffer;
					global::UnityEngine.Rendering.RadeonRays.BottomLevelLevelAccelStruct result = new global::UnityEngine.Rendering.RadeonRays.BottomLevelLevelAccelStruct
					{
						bvh = m_BlasBuffer,
						bvhOffset = (uint)value.bvhAlloc.block.offset,
						bvhLeaves = m_BlasLeavesBuffer,
						bvhLeavesOffset = (uint)value.bvhLeavesAlloc.block.offset
					};
					m_RadeonRaysAPI.BuildMeshAccelStruct(cmd, value.buildInfo, ConvertFlagsToGpuBuild(m_BuildFlags), scratchBuffer, in result);
					value.buildInfo.triangleIndices.Dispose();
					value.buildInfo.triangleIndices = null;
					value.bvhBuilt = true;
				}
			}
		}

		private void BuildTopLevelAccelStruct(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.GraphicsBuffer scratchBuffer)
		{
			global::UnityEngine.Rendering.RadeonRays.Instance[] array = new global::UnityEngine.Rendering.RadeonRays.Instance[m_RadeonInstances.Count];
			int num = 0;
			foreach (global::UnityEngine.Rendering.UnifiedRayTracing.ComputeRayTracingAccelStruct.RadeonRaysInstance value in m_RadeonInstances.Values)
			{
				array[num].meshAccelStructOffset = (uint)value.blas.bvhAlloc.block.offset;
				array[num].localToWorldTransform = value.localToWorldTransform;
				array[num].instanceMask = value.instanceMask;
				array[num].vertexOffset = (uint)(value.blas.blasVertices.block.offset * 3);
				array[num].meshAccelStructLeavesOffset = (uint)value.blas.bvhLeavesAlloc.block.offset;
				array[num].triangleCullingEnabled = value.triangleCullingEnabled;
				array[num].invertTriangleCulling = value.invertTriangleCulling;
				array[num].userInstanceID = value.userInstanceID;
				array[num].isOpaque = value.opaqueGeometry;
				num++;
			}
			m_TopLevelAccelStruct?.Dispose();
			m_TopLevelAccelStruct = m_RadeonRaysAPI.BuildSceneAccelStruct(cmd, m_BlasBuffer, array, scratchBuffer);
		}

		private global::UnityEngine.Rendering.RadeonRays.BuildFlags ConvertFlagsToGpuBuild(global::UnityEngine.Rendering.UnifiedRayTracing.BuildFlags flags)
		{
			if ((flags & global::UnityEngine.Rendering.UnifiedRayTracing.BuildFlags.PreferFastBuild) != global::UnityEngine.Rendering.UnifiedRayTracing.BuildFlags.None && (flags & global::UnityEngine.Rendering.UnifiedRayTracing.BuildFlags.PreferFastTrace) == 0)
			{
				return global::UnityEngine.Rendering.RadeonRays.BuildFlags.PreferFastBuild;
			}
			return global::UnityEngine.Rendering.RadeonRays.BuildFlags.None;
		}

		public void Bind(global::UnityEngine.Rendering.CommandBuffer cmd, string name, global::UnityEngine.Rendering.UnifiedRayTracing.IRayTracingShader shader)
		{
			shader.SetBufferParam(cmd, global::UnityEngine.Shader.PropertyToID(name + "bvh"), topLevelBvhBuffer);
			shader.SetBufferParam(cmd, global::UnityEngine.Shader.PropertyToID(name + "bottomBvhs"), bottomLevelBvhBuffer);
			shader.SetBufferParam(cmd, global::UnityEngine.Shader.PropertyToID(name + "bottomBvhLeaves"), m_BlasLeavesBuffer);
			shader.SetBufferParam(cmd, global::UnityEngine.Shader.PropertyToID(name + "instanceInfos"), instanceInfoBuffer);
			shader.SetBufferParam(cmd, global::UnityEngine.Shader.PropertyToID(name + "vertexBuffer"), m_BlasPositions.VertexBuffer);
		}

		public void Bind(global::UnityEngine.Rendering.CommandBuffer cmd, string name, global::UnityEngine.ComputeShader shader, int kernelIndex)
		{
			cmd.SetComputeBufferParam(shader, kernelIndex, global::UnityEngine.Shader.PropertyToID(name + "bvh"), topLevelBvhBuffer);
			cmd.SetComputeBufferParam(shader, kernelIndex, global::UnityEngine.Shader.PropertyToID(name + "bottomBvhs"), bottomLevelBvhBuffer);
			cmd.SetComputeBufferParam(shader, kernelIndex, global::UnityEngine.Shader.PropertyToID(name + "bottomBvhLeaves"), m_BlasLeavesBuffer);
			cmd.SetComputeBufferParam(shader, kernelIndex, global::UnityEngine.Shader.PropertyToID(name + "instanceInfos"), instanceInfoBuffer);
			cmd.SetComputeBufferParam(shader, kernelIndex, global::UnityEngine.Shader.PropertyToID(name + "vertexBuffer"), m_BlasPositions.VertexBuffer);
		}

		private static global::UnityEngine.Rendering.RadeonRays.Transform ConvertTranform(global::UnityEngine.Matrix4x4 input)
		{
			return new global::UnityEngine.Rendering.RadeonRays.Transform
			{
				row0 = input.GetRow(0),
				row1 = input.GetRow(1),
				row2 = input.GetRow(2)
			};
		}

		private static global::UnityEngine.Matrix4x4 ConvertTranform(global::UnityEngine.Rendering.RadeonRays.Transform input)
		{
			global::UnityEngine.Matrix4x4 result = default(global::UnityEngine.Matrix4x4);
			result.SetRow(0, input.row0);
			result.SetRow(1, input.row1);
			result.SetRow(2, input.row2);
			result.SetRow(3, new global::UnityEngine.Vector4(0f, 0f, 0f, 1f));
			return result;
		}

		private static global::Unity.Mathematics.int3 GetFaceIndices(global::System.Collections.Generic.List<int> indices, int triangleIdx)
		{
			return new global::Unity.Mathematics.int3(indices[3 * triangleIdx], indices[3 * triangleIdx + 1], indices[3 * triangleIdx + 2]);
		}

		private static global::UnityEngine.Rendering.UnifiedRayTracing.ComputeRayTracingAccelStruct.Triangle GetTriangle(global::System.Collections.Generic.List<global::UnityEngine.Vector3> vertices, global::Unity.Mathematics.int3 idx)
		{
			global::UnityEngine.Rendering.UnifiedRayTracing.ComputeRayTracingAccelStruct.Triangle result = default(global::UnityEngine.Rendering.UnifiedRayTracing.ComputeRayTracingAccelStruct.Triangle);
			result.v0 = vertices[idx.x];
			result.v1 = vertices[idx.y];
			result.v2 = vertices[idx.z];
			return result;
		}

		private global::UnityEngine.Rendering.UnifiedRayTracing.BlockAllocator.Allocation AllocateBlasInternalNodes(int allocationNodeCount)
		{
			global::UnityEngine.Rendering.UnifiedRayTracing.BlockAllocator.Allocation result = m_BlasAllocator.Allocate(allocationNodeCount);
			if (!result.valid)
			{
				int oldCapacity = m_BlasAllocator.capacity;
				if (!m_BlasAllocator.GetExpectedGrowthToFitAllocation(allocationNodeCount, (int)(global::UnityEngine.Rendering.UnifiedRayTracing.GraphicsHelpers.MaxGraphicsBufferSizeInBytes / global::UnityEngine.Rendering.RadeonRays.RadeonRaysAPI.BvhInternalNodeSizeInBytes()), out var newCapacity))
				{
					throw new global::UnityEngine.Rendering.UnifiedRayTracing.UnifiedRayTracingException($"Can't allocate a GraphicsBuffer bigger than {(global::UnityEngine.Rendering.UnifiedRayTracing.GraphicsHelpers.MaxGraphicsBufferSizeInGigaBytes):F1}GB", global::UnityEngine.Rendering.UnifiedRayTracing.UnifiedRayTracingError.GraphicsBufferAllocationFailed);
				}
				if (!global::UnityEngine.Rendering.UnifiedRayTracing.GraphicsHelpers.ReallocateBuffer(m_CopyShader, oldCapacity, newCapacity, global::UnityEngine.Rendering.RadeonRays.RadeonRaysAPI.BvhInternalNodeSizeInBytes(), ref m_BlasBuffer))
				{
					throw new global::UnityEngine.Rendering.UnifiedRayTracing.UnifiedRayTracingException($"Failed to allocate buffer of size: {(newCapacity * global::UnityEngine.Rendering.RadeonRays.RadeonRaysAPI.BvhInternalNodeSizeInBytes())} bytes", global::UnityEngine.Rendering.UnifiedRayTracing.UnifiedRayTracingError.GraphicsBufferAllocationFailed);
				}
				result = m_BlasAllocator.GrowAndAllocate(allocationNodeCount, (int)(global::UnityEngine.Rendering.UnifiedRayTracing.GraphicsHelpers.MaxGraphicsBufferSizeInBytes / global::UnityEngine.Rendering.RadeonRays.RadeonRaysAPI.BvhInternalNodeSizeInBytes()), out oldCapacity, out newCapacity);
			}
			return result;
		}

		private global::UnityEngine.Rendering.UnifiedRayTracing.BlockAllocator.Allocation AllocateBlasLeafNodes(int allocationNodeCount)
		{
			global::UnityEngine.Rendering.UnifiedRayTracing.BlockAllocator.Allocation result = m_BlasLeavesAllocator.Allocate(allocationNodeCount);
			if (!result.valid)
			{
				int oldCapacity = m_BlasLeavesAllocator.capacity;
				if (!m_BlasLeavesAllocator.GetExpectedGrowthToFitAllocation(allocationNodeCount, (int)(global::UnityEngine.Rendering.UnifiedRayTracing.GraphicsHelpers.MaxGraphicsBufferSizeInBytes / global::UnityEngine.Rendering.RadeonRays.RadeonRaysAPI.BvhLeafNodeSizeInBytes()), out var newCapacity))
				{
					throw new global::UnityEngine.Rendering.UnifiedRayTracing.UnifiedRayTracingException($"Can't allocate a GraphicsBuffer bigger than {(global::UnityEngine.Rendering.UnifiedRayTracing.GraphicsHelpers.MaxGraphicsBufferSizeInGigaBytes):F1}GB", global::UnityEngine.Rendering.UnifiedRayTracing.UnifiedRayTracingError.GraphicsBufferAllocationFailed);
				}
				if (!global::UnityEngine.Rendering.UnifiedRayTracing.GraphicsHelpers.ReallocateBuffer(m_CopyShader, oldCapacity, newCapacity, global::UnityEngine.Rendering.RadeonRays.RadeonRaysAPI.BvhLeafNodeSizeInBytes(), ref m_BlasLeavesBuffer))
				{
					throw new global::UnityEngine.Rendering.UnifiedRayTracing.UnifiedRayTracingException($"Failed to allocate buffer of size: {(newCapacity * global::UnityEngine.Rendering.RadeonRays.RadeonRaysAPI.BvhLeafNodeSizeInBytes())} bytes", global::UnityEngine.Rendering.UnifiedRayTracing.UnifiedRayTracingError.GraphicsBufferAllocationFailed);
				}
				result = m_BlasLeavesAllocator.GrowAndAllocate(allocationNodeCount, (int)(global::UnityEngine.Rendering.UnifiedRayTracing.GraphicsHelpers.MaxGraphicsBufferSizeInBytes / global::UnityEngine.Rendering.RadeonRays.RadeonRaysAPI.BvhLeafNodeSizeInBytes()), out oldCapacity, out newCapacity);
			}
			return result;
		}

		private int NewHandle()
		{
			if (m_FreeHandles.Count != 0)
			{
				return (int)(m_FreeHandles.Dequeue() ^ m_HandleObfuscation);
			}
			return m_RadeonInstances.Count ^ (int)m_HandleObfuscation;
		}

		private void ReleaseHandle(int handle)
		{
			m_FreeHandles.Enqueue((uint)handle ^ m_HandleObfuscation);
		}

		[global::System.Diagnostics.Conditional("UNITY_ASSERTIONS")]
		private void CheckInstanceHandleIsValid(int instanceHandle)
		{
			if (!m_RadeonInstances.ContainsKey(instanceHandle))
			{
				throw new global::System.ArgumentException($"accel struct does not contain instanceHandle {instanceHandle}", "instanceHandle");
			}
		}
	}
}
