namespace UnityEngine.Rendering.UnifiedRayTracing
{
	internal sealed class AccelStructInstances : global::System.IDisposable
	{
		public struct RTInstance
		{
			public global::Unity.Mathematics.float4x4 localToWorld;

			public global::Unity.Mathematics.float4x4 previousLocalToWorld;

			public global::Unity.Mathematics.float4x4 localToWorldNormals;

			public uint renderingLayerMask;

			public uint instanceMask;

			public uint userMaterialID;

			public uint geometryIndex;
		}

		public class InstanceEntry
		{
			public global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPoolHandle geometryPoolHandle;

			public global::UnityEngine.Rendering.UnifiedRayTracing.BlockAllocator.Allocation indexInInstanceBuffer;

			public uint instanceMask;

			public uint vertexOffset;

			public uint indexOffset;
		}

		private readonly global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPool m_GeometryPool;

		private readonly global::UnityEngine.Rendering.UnifiedRayTracing.PersistentGpuArray<global::UnityEngine.Rendering.UnifiedRayTracing.AccelStructInstances.RTInstance> m_InstanceBuffer = new global::UnityEngine.Rendering.UnifiedRayTracing.PersistentGpuArray<global::UnityEngine.Rendering.UnifiedRayTracing.AccelStructInstances.RTInstance>(100);

		private readonly global::System.Collections.Generic.Dictionary<int, global::UnityEngine.Rendering.UnifiedRayTracing.AccelStructInstances.InstanceEntry> m_Instances = new global::System.Collections.Generic.Dictionary<int, global::UnityEngine.Rendering.UnifiedRayTracing.AccelStructInstances.InstanceEntry>();

		private uint m_FrameTimestamp;

		private uint m_TransformTouchedLastTimestamp;

		public global::UnityEngine.Rendering.UnifiedRayTracing.PersistentGpuArray<global::UnityEngine.Rendering.UnifiedRayTracing.AccelStructInstances.RTInstance> instanceBuffer => m_InstanceBuffer;

		public global::System.Collections.Generic.IReadOnlyCollection<global::UnityEngine.Rendering.UnifiedRayTracing.AccelStructInstances.InstanceEntry> instances => m_Instances.Values;

		public global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPool geometryPool => m_GeometryPool;

		public global::UnityEngine.GraphicsBuffer indexBuffer => m_GeometryPool.globalIndexBuffer;

		public global::UnityEngine.GraphicsBuffer vertexBuffer => m_GeometryPool.globalVertexBuffer;

		public bool instanceListValid => m_InstanceBuffer != null;

		internal AccelStructInstances(global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPool geometryPool)
		{
			m_GeometryPool = geometryPool;
		}

		public void Dispose()
		{
			foreach (global::UnityEngine.Rendering.UnifiedRayTracing.AccelStructInstances.InstanceEntry value in m_Instances.Values)
			{
				global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPoolHandle geometryPoolHandle = value.geometryPoolHandle;
				m_GeometryPool.Unregister(geometryPoolHandle);
			}
			m_GeometryPool.SendGpuCommands();
			m_InstanceBuffer?.Dispose();
			m_GeometryPool.Dispose();
		}

		public int AddInstance(global::UnityEngine.Rendering.UnifiedRayTracing.MeshInstanceDesc meshInstance, uint materialID, uint renderingLayerMask)
		{
			global::UnityEngine.Rendering.UnifiedRayTracing.BlockAllocator.Allocation slotAllocation = m_InstanceBuffer.Add(1)[0];
			AddInstance(slotAllocation, in meshInstance, materialID, renderingLayerMask);
			return slotAllocation.block.offset;
		}

		public int AddInstances(global::System.Span<global::UnityEngine.Rendering.UnifiedRayTracing.MeshInstanceDesc> meshInstances, global::System.Span<uint> materialIDs, global::System.Span<uint> renderingLayerMask)
		{
			global::UnityEngine.Rendering.UnifiedRayTracing.BlockAllocator.Allocation[] array = m_InstanceBuffer.Add(meshInstances.Length);
			for (int i = 0; i < meshInstances.Length; i++)
			{
				AddInstance(array[i], in meshInstances[i], materialIDs[i], renderingLayerMask[i]);
			}
			return array[0].block.offset;
		}

		private void AddInstance(global::UnityEngine.Rendering.UnifiedRayTracing.BlockAllocator.Allocation slotAllocation, in global::UnityEngine.Rendering.UnifiedRayTracing.MeshInstanceDesc meshInstance, uint materialID, uint renderingLayerMask)
		{
			if (!m_GeometryPool.Register(meshInstance.mesh, out var outHandle))
			{
				throw new global::System.InvalidOperationException("Failed to allocate geometry data for instance");
			}
			m_GeometryPool.SendGpuCommands();
			m_InstanceBuffer.Set(slotAllocation, new global::UnityEngine.Rendering.UnifiedRayTracing.AccelStructInstances.RTInstance
			{
				localToWorld = meshInstance.localToWorldMatrix,
				localToWorldNormals = NormalMatrix(meshInstance.localToWorldMatrix),
				previousLocalToWorld = meshInstance.localToWorldMatrix,
				userMaterialID = materialID,
				instanceMask = meshInstance.mask,
				renderingLayerMask = renderingLayerMask,
				geometryIndex = (uint)(m_GeometryPool.GetEntryGeomAllocation(outHandle).meshChunkTableAlloc.block.offset + meshInstance.subMeshIndex)
			});
			global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPool.MeshChunk meshChunk = m_GeometryPool.GetEntryGeomAllocation(outHandle).meshChunks[meshInstance.subMeshIndex];
			global::UnityEngine.Rendering.UnifiedRayTracing.AccelStructInstances.InstanceEntry value = new global::UnityEngine.Rendering.UnifiedRayTracing.AccelStructInstances.InstanceEntry
			{
				geometryPoolHandle = outHandle,
				indexInInstanceBuffer = slotAllocation,
				instanceMask = meshInstance.mask,
				vertexOffset = (uint)meshChunk.vertexAlloc.block.offset * ((uint)global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPool.GetVertexByteSize() / 4u),
				indexOffset = (uint)meshChunk.indexAlloc.block.offset
			};
			m_Instances.Add(slotAllocation.block.offset, value);
		}

		public global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPool.MeshChunk GetEntryGeomAllocation(global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPoolHandle handle, int submeshIndex)
		{
			return m_GeometryPool.GetEntryGeomAllocation(handle).meshChunks[submeshIndex];
		}

		public void RemoveInstance(int instanceHandle)
		{
			m_Instances.TryGetValue(instanceHandle, out var value);
			m_Instances.Remove(instanceHandle);
			m_InstanceBuffer.Remove(value.indexInInstanceBuffer);
			global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPoolHandle geometryPoolHandle = value.geometryPoolHandle;
			m_GeometryPool.Unregister(geometryPoolHandle);
			m_GeometryPool.SendGpuCommands();
		}

		public void ClearInstances()
		{
			foreach (global::UnityEngine.Rendering.UnifiedRayTracing.AccelStructInstances.InstanceEntry value in m_Instances.Values)
			{
				global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPoolHandle geometryPoolHandle = value.geometryPoolHandle;
				m_GeometryPool.Unregister(geometryPoolHandle);
			}
			m_GeometryPool.SendGpuCommands();
			m_Instances.Clear();
			m_InstanceBuffer.Clear();
		}

		public void UpdateInstanceTransform(int instanceHandle, global::UnityEngine.Matrix4x4 localToWorldMatrix)
		{
			m_Instances.TryGetValue(instanceHandle, out var value);
			global::UnityEngine.Rendering.UnifiedRayTracing.AccelStructInstances.RTInstance element = m_InstanceBuffer.Get(value.indexInInstanceBuffer);
			element.localToWorld = localToWorldMatrix;
			element.localToWorldNormals = NormalMatrix(localToWorldMatrix);
			m_InstanceBuffer.Set(value.indexInInstanceBuffer, element);
			m_TransformTouchedLastTimestamp = m_FrameTimestamp;
		}

		public void UpdateInstanceMaterialID(int instanceHandle, uint materialID)
		{
			m_Instances.TryGetValue(instanceHandle, out var value);
			global::UnityEngine.Rendering.UnifiedRayTracing.AccelStructInstances.RTInstance element = m_InstanceBuffer.Get(value.indexInInstanceBuffer);
			element.userMaterialID = materialID;
			m_InstanceBuffer.Set(value.indexInInstanceBuffer, element);
		}

		public void UpdateRenderingLayerMask(int instanceHandle, uint renderingLayerMask)
		{
			m_Instances.TryGetValue(instanceHandle, out var value);
			global::UnityEngine.Rendering.UnifiedRayTracing.AccelStructInstances.RTInstance element = m_InstanceBuffer.Get(value.indexInInstanceBuffer);
			element.renderingLayerMask = renderingLayerMask;
			m_InstanceBuffer.Set(value.indexInInstanceBuffer, element);
		}

		public void UpdateInstanceMask(int instanceHandle, uint mask)
		{
			m_Instances.TryGetValue(instanceHandle, out var value);
			value.instanceMask = mask;
			global::UnityEngine.Rendering.UnifiedRayTracing.AccelStructInstances.RTInstance element = m_InstanceBuffer.Get(value.indexInInstanceBuffer);
			element.instanceMask = mask;
			m_InstanceBuffer.Set(value.indexInInstanceBuffer, element);
		}

		public void NextFrame()
		{
			if (m_FrameTimestamp - m_TransformTouchedLastTimestamp <= 1)
			{
				m_InstanceBuffer.ModifyForEach(delegate(global::UnityEngine.Rendering.UnifiedRayTracing.AccelStructInstances.RTInstance instance)
				{
					instance.previousLocalToWorld = instance.localToWorld;
					return instance;
				});
			}
			m_FrameTimestamp++;
		}

		public void Bind(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Rendering.UnifiedRayTracing.IRayTracingShader shader)
		{
			global::UnityEngine.ComputeBuffer gpuBuffer = m_InstanceBuffer.GetGpuBuffer(cmd);
			shader.SetBufferParam(cmd, global::UnityEngine.Shader.PropertyToID("g_AccelStructInstanceList"), gpuBuffer);
			shader.SetBufferParam(cmd, global::UnityEngine.Shader.PropertyToID("g_globalIndexBuffer"), m_GeometryPool.globalIndexBuffer);
			shader.SetBufferParam(cmd, global::UnityEngine.Shader.PropertyToID("g_globalVertexBuffer"), m_GeometryPool.globalVertexBuffer);
			shader.SetIntParam(cmd, global::UnityEngine.Shader.PropertyToID("g_globalVertexBufferStride"), m_GeometryPool.globalVertexBufferStrideBytes / 4);
			shader.SetBufferParam(cmd, global::UnityEngine.Shader.PropertyToID("g_MeshList"), m_GeometryPool.globalMeshChunkTableEntryBuffer);
		}

		public int GetInstanceCount()
		{
			return m_Instances.Count;
		}

		private static global::Unity.Mathematics.float4x4 NormalMatrix(global::Unity.Mathematics.float4x4 m)
		{
			return new global::Unity.Mathematics.float4x4(global::Unity.Mathematics.math.inverse(global::Unity.Mathematics.math.transpose(new global::Unity.Mathematics.float3x3(m))), new global::Unity.Mathematics.float3(0.0));
		}
	}
}
