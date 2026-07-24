namespace UnityEngine.Rendering.UnifiedRayTracing
{
	internal sealed class HardwareRayTracingAccelStruct : global::UnityEngine.Rendering.UnifiedRayTracing.IRayTracingAccelStruct, global::System.IDisposable
	{
		private readonly global::UnityEngine.Rendering.RayTracingAccelerationStructureBuildFlags m_BuildFlags;

		private readonly global::System.Collections.Generic.Dictionary<int, global::UnityEngine.Mesh> m_Meshes = new global::System.Collections.Generic.Dictionary<int, global::UnityEngine.Mesh>();

		private readonly global::UnityEngine.Rendering.UnifiedRayTracing.ReferenceCounter m_Counter;

		public global::UnityEngine.Rendering.RayTracingAccelerationStructure accelStruct { get; }

		internal HardwareRayTracingAccelStruct(global::UnityEngine.Rendering.UnifiedRayTracing.AccelerationStructureOptions options, global::UnityEngine.Rendering.UnifiedRayTracing.ReferenceCounter counter)
		{
			m_BuildFlags = (global::UnityEngine.Rendering.RayTracingAccelerationStructureBuildFlags)options.buildFlags;
			accelStruct = new global::UnityEngine.Rendering.RayTracingAccelerationStructure(new global::UnityEngine.Rendering.RayTracingAccelerationStructure.Settings
			{
				rayTracingModeMask = global::UnityEngine.Rendering.RayTracingAccelerationStructure.RayTracingModeMask.Everything,
				managementMode = global::UnityEngine.Rendering.RayTracingAccelerationStructure.ManagementMode.Manual,
				enableCompaction = false,
				layerMask = 255,
				buildFlagsStaticGeometries = m_BuildFlags
			});
			m_Counter = counter;
			m_Counter.Inc();
		}

		public void Dispose()
		{
			m_Counter.Dec();
			accelStruct?.Dispose();
		}

		public int AddInstance(global::UnityEngine.Rendering.UnifiedRayTracing.MeshInstanceDesc meshInstance)
		{
			global::UnityEngine.Rendering.RayTracingMeshInstanceConfig config = new global::UnityEngine.Rendering.RayTracingMeshInstanceConfig(meshInstance.mesh, (uint)meshInstance.subMeshIndex, null);
			config.mask = meshInstance.mask;
			config.enableTriangleCulling = meshInstance.enableTriangleCulling;
			config.frontTriangleCounterClockwise = meshInstance.frontTriangleCounterClockwise;
			config.subMeshFlags = (meshInstance.opaqueGeometry ? (global::UnityEngine.Rendering.RayTracingSubMeshFlags.Enabled | global::UnityEngine.Rendering.RayTracingSubMeshFlags.ClosestHitOnly) : (global::UnityEngine.Rendering.RayTracingSubMeshFlags.Enabled | global::UnityEngine.Rendering.RayTracingSubMeshFlags.UniqueAnyHitCalls));
			int num = accelStruct.AddInstance(in config, meshInstance.localToWorldMatrix, null, meshInstance.instanceID);
			if (meshInstance.instanceID == uint.MaxValue)
			{
				accelStruct.UpdateInstanceID(num, (uint)num);
			}
			m_Meshes.Add(num, meshInstance.mesh);
			return num;
		}

		public void RemoveInstance(int instanceHandle)
		{
			m_Meshes.Remove(instanceHandle);
			accelStruct.RemoveInstance(instanceHandle);
		}

		public void ClearInstances()
		{
			m_Meshes.Clear();
			accelStruct.ClearInstances();
		}

		public void UpdateInstanceTransform(int instanceHandle, global::UnityEngine.Matrix4x4 localToWorldMatrix)
		{
			accelStruct.UpdateInstanceTransform(instanceHandle, localToWorldMatrix);
		}

		public void UpdateInstanceID(int instanceHandle, uint instanceID)
		{
			accelStruct.UpdateInstanceID(instanceHandle, instanceID);
		}

		public void UpdateInstanceMask(int instanceHandle, uint mask)
		{
			accelStruct.UpdateInstanceMask(instanceHandle, mask);
		}

		public void Build(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.GraphicsBuffer scratchBuffer)
		{
			global::UnityEngine.Rendering.RayTracingAccelerationStructure.BuildSettings buildSettings = new global::UnityEngine.Rendering.RayTracingAccelerationStructure.BuildSettings();
			buildSettings.buildFlags = m_BuildFlags;
			buildSettings.relativeOrigin = global::UnityEngine.Vector3.zero;
			global::UnityEngine.Rendering.RayTracingAccelerationStructure.BuildSettings buildSettings2 = buildSettings;
			cmd.BuildRayTracingAccelerationStructure(accelStruct, buildSettings2);
		}

		public ulong GetBuildScratchBufferRequiredSizeInBytes()
		{
			return 0uL;
		}

		[global::System.Diagnostics.Conditional("UNITY_ASSERTIONS")]
		private void CheckInstanceHandleIsValid(int instanceHandle)
		{
		}
	}
}
