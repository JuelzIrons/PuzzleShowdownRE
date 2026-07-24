namespace UnityEngine.Rendering.UnifiedRayTracing
{
	internal sealed class AccelStructAdapter : global::System.IDisposable
	{
		private struct InstanceIDs
		{
			public int InstanceID;

			public int AccelStructID;
		}

		private global::UnityEngine.Rendering.UnifiedRayTracing.IRayTracingAccelStruct _accelStruct;

		private global::UnityEngine.Rendering.UnifiedRayTracing.AccelStructInstances _instances;

		private readonly global::System.Collections.Generic.Dictionary<int, global::UnityEngine.Rendering.UnifiedRayTracing.AccelStructAdapter.InstanceIDs[]> _objectHandleToInstances = new global::System.Collections.Generic.Dictionary<int, global::UnityEngine.Rendering.UnifiedRayTracing.AccelStructAdapter.InstanceIDs[]>();

		internal global::UnityEngine.Rendering.UnifiedRayTracing.AccelStructInstances Instances => _instances;

		public global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPool GeometryPool => _instances.geometryPool;

		public AccelStructAdapter(global::UnityEngine.Rendering.UnifiedRayTracing.IRayTracingAccelStruct accelStruct, global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPool geometryPool)
		{
			_accelStruct = accelStruct;
			_instances = new global::UnityEngine.Rendering.UnifiedRayTracing.AccelStructInstances(geometryPool);
		}

		public AccelStructAdapter(global::UnityEngine.Rendering.UnifiedRayTracing.IRayTracingAccelStruct accelStruct, global::UnityEngine.Rendering.UnifiedRayTracing.RayTracingResources resources)
			: this(accelStruct, new global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPool(global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPoolDesc.NewDefault(), resources.geometryPoolKernels, resources.copyBuffer))
		{
		}

		public global::UnityEngine.Rendering.UnifiedRayTracing.IRayTracingAccelStruct GetAccelerationStructure()
		{
			return _accelStruct;
		}

		public void Bind(global::UnityEngine.Rendering.CommandBuffer cmd, string propertyName, global::UnityEngine.Rendering.UnifiedRayTracing.IRayTracingShader shader)
		{
			shader.SetAccelerationStructure(cmd, propertyName, _accelStruct);
			_instances.Bind(cmd, shader);
		}

		public void Dispose()
		{
			_instances?.Dispose();
			_instances = null;
			_accelStruct?.Dispose();
			_accelStruct = null;
			_objectHandleToInstances.Clear();
		}

		public void AddInstance(int objectHandle, global::UnityEngine.Component meshRendererOrTerrain, global::System.Span<uint> perSubMeshMask, global::System.Span<uint> perSubMeshMaterialIDs, global::System.Span<bool> perSubMeshIsOpaque, uint renderingLayerMask)
		{
			if (meshRendererOrTerrain is global::UnityEngine.Terrain terrain)
			{
				global::UnityEngine.Rendering.UnifiedRayTracing.TerrainDesc terrainDesc = default(global::UnityEngine.Rendering.UnifiedRayTracing.TerrainDesc);
				terrainDesc.terrain = terrain;
				terrainDesc.localToWorldMatrix = terrain.transform.localToWorldMatrix;
				terrainDesc.mask = perSubMeshMask[0];
				terrainDesc.renderingLayerMask = renderingLayerMask;
				terrainDesc.materialID = perSubMeshMaterialIDs[0];
				terrainDesc.enableTriangleCulling = true;
				terrainDesc.frontTriangleCounterClockwise = false;
				AddInstance(objectHandle, terrainDesc);
			}
			else
			{
				global::UnityEngine.MeshRenderer meshRenderer = (global::UnityEngine.MeshRenderer)meshRendererOrTerrain;
				global::UnityEngine.Mesh sharedMesh = meshRenderer.GetComponent<global::UnityEngine.MeshFilter>().sharedMesh;
				AddInstance(objectHandle, sharedMesh, meshRenderer.transform.localToWorldMatrix, perSubMeshMask, perSubMeshMaterialIDs, perSubMeshIsOpaque, renderingLayerMask);
			}
		}

		public void AddInstance(int objectHandle, global::UnityEngine.Mesh mesh, global::UnityEngine.Matrix4x4 localToWorldMatrix, global::System.Span<uint> perSubMeshMask, global::System.Span<uint> perSubMeshMaterialIDs, global::System.Span<bool> perSubMeshIsOpaque, uint renderingLayerMask)
		{
			int subMeshCount = mesh.subMeshCount;
			global::UnityEngine.Rendering.UnifiedRayTracing.AccelStructAdapter.InstanceIDs[] array = new global::UnityEngine.Rendering.UnifiedRayTracing.AccelStructAdapter.InstanceIDs[subMeshCount];
			for (int i = 0; i < subMeshCount; i++)
			{
				global::UnityEngine.Rendering.UnifiedRayTracing.MeshInstanceDesc meshInstanceDesc = new global::UnityEngine.Rendering.UnifiedRayTracing.MeshInstanceDesc(mesh, i);
				meshInstanceDesc.localToWorldMatrix = localToWorldMatrix;
				meshInstanceDesc.mask = perSubMeshMask[i];
				meshInstanceDesc.opaqueGeometry = perSubMeshIsOpaque[i];
				global::UnityEngine.Rendering.UnifiedRayTracing.MeshInstanceDesc meshInstance = meshInstanceDesc;
				array[i].InstanceID = _instances.AddInstance(meshInstance, perSubMeshMaterialIDs[i], renderingLayerMask);
				meshInstance.instanceID = (uint)array[i].InstanceID;
				array[i].AccelStructID = _accelStruct.AddInstance(meshInstance);
			}
			_objectHandleToInstances.Add(objectHandle, array);
		}

		private void AddInstance(int objectHandle, global::UnityEngine.Rendering.UnifiedRayTracing.TerrainDesc terrainDesc)
		{
			global::System.Collections.Generic.List<global::UnityEngine.Rendering.UnifiedRayTracing.AccelStructAdapter.InstanceIDs> instanceHandles = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.UnifiedRayTracing.AccelStructAdapter.InstanceIDs>();
			AddHeightmap(terrainDesc, ref instanceHandles);
			AddTrees(terrainDesc, ref instanceHandles);
			_objectHandleToInstances.Add(objectHandle, instanceHandles.ToArray());
		}

		private void AddHeightmap(global::UnityEngine.Rendering.UnifiedRayTracing.TerrainDesc terrainDesc, ref global::System.Collections.Generic.List<global::UnityEngine.Rendering.UnifiedRayTracing.AccelStructAdapter.InstanceIDs> instanceHandles)
		{
			global::UnityEngine.Mesh mesh = global::UnityEngine.Rendering.UnifiedRayTracing.TerrainToMesh.Convert(terrainDesc.terrain);
			global::UnityEngine.Rendering.UnifiedRayTracing.MeshInstanceDesc instanceDesc = new global::UnityEngine.Rendering.UnifiedRayTracing.MeshInstanceDesc(mesh);
			instanceDesc.localToWorldMatrix = terrainDesc.localToWorldMatrix;
			instanceDesc.mask = terrainDesc.mask;
			instanceDesc.enableTriangleCulling = terrainDesc.enableTriangleCulling;
			instanceDesc.frontTriangleCounterClockwise = terrainDesc.frontTriangleCounterClockwise;
			instanceHandles.Add(AddInstance(instanceDesc, terrainDesc.materialID, terrainDesc.renderingLayerMask));
		}

		private void AddTrees(global::UnityEngine.Rendering.UnifiedRayTracing.TerrainDesc terrainDesc, ref global::System.Collections.Generic.List<global::UnityEngine.Rendering.UnifiedRayTracing.AccelStructAdapter.InstanceIDs> instanceHandles)
		{
			global::UnityEngine.TerrainData terrainData = terrainDesc.terrain.terrainData;
			global::UnityEngine.Matrix4x4 localToWorldMatrix = terrainDesc.localToWorldMatrix;
			global::UnityEngine.Vector3 b = global::UnityEngine.Vector3.Scale(new global::UnityEngine.Vector3(terrainData.heightmapResolution, 1f, terrainData.heightmapResolution), terrainData.heightmapScale);
			global::UnityEngine.Vector3 position = localToWorldMatrix.GetPosition();
			global::UnityEngine.TreeInstance[] treeInstances = terrainData.treeInstances;
			for (int i = 0; i < treeInstances.Length; i++)
			{
				global::UnityEngine.TreeInstance treeInstance = treeInstances[i];
				global::UnityEngine.Matrix4x4 localToWorldMatrix2 = global::UnityEngine.Matrix4x4.TRS(position + global::UnityEngine.Vector3.Scale(treeInstance.position, b), global::UnityEngine.Quaternion.AngleAxis(treeInstance.rotation, global::UnityEngine.Vector3.up), new global::UnityEngine.Vector3(treeInstance.widthScale, treeInstance.heightScale, treeInstance.widthScale));
				global::UnityEngine.GameObject prefab = terrainData.treePrototypes[treeInstance.prototypeIndex].prefab;
				global::UnityEngine.GameObject gameObject = prefab.gameObject;
				if (prefab.TryGetComponent<global::UnityEngine.LODGroup>(out var component))
				{
					global::UnityEngine.LOD[] lODs = component.GetLODs();
					if (lODs.Length != 0 && lODs[0].renderers.Length != 0)
					{
						gameObject = (lODs[0].renderers[0] as global::UnityEngine.MeshRenderer).gameObject;
					}
				}
				if (gameObject.TryGetComponent<global::UnityEngine.MeshFilter>(out var component2))
				{
					global::UnityEngine.Mesh sharedMesh = component2.sharedMesh;
					for (int j = 0; j < sharedMesh.subMeshCount; j++)
					{
						global::UnityEngine.Rendering.UnifiedRayTracing.MeshInstanceDesc instanceDesc = new global::UnityEngine.Rendering.UnifiedRayTracing.MeshInstanceDesc(sharedMesh, j);
						instanceDesc.localToWorldMatrix = localToWorldMatrix2;
						instanceDesc.mask = terrainDesc.mask;
						instanceDesc.enableTriangleCulling = terrainDesc.enableTriangleCulling;
						instanceDesc.frontTriangleCounterClockwise = terrainDesc.frontTriangleCounterClockwise;
						instanceHandles.Add(AddInstance(instanceDesc, terrainDesc.materialID, (uint)(1 << prefab.gameObject.layer)));
					}
				}
			}
		}

		private global::UnityEngine.Rendering.UnifiedRayTracing.AccelStructAdapter.InstanceIDs AddInstance(global::UnityEngine.Rendering.UnifiedRayTracing.MeshInstanceDesc instanceDesc, uint materialID, uint renderingLayerMask)
		{
			global::UnityEngine.Rendering.UnifiedRayTracing.AccelStructAdapter.InstanceIDs result = new global::UnityEngine.Rendering.UnifiedRayTracing.AccelStructAdapter.InstanceIDs
			{
				InstanceID = _instances.AddInstance(instanceDesc, materialID, renderingLayerMask)
			};
			instanceDesc.instanceID = (uint)result.InstanceID;
			result.AccelStructID = _accelStruct.AddInstance(instanceDesc);
			return result;
		}

		public void RemoveInstance(int objectHandle)
		{
			_objectHandleToInstances.TryGetValue(objectHandle, out var value);
			global::UnityEngine.Rendering.UnifiedRayTracing.AccelStructAdapter.InstanceIDs[] array = value;
			for (int i = 0; i < array.Length; i++)
			{
				global::UnityEngine.Rendering.UnifiedRayTracing.AccelStructAdapter.InstanceIDs instanceIDs = array[i];
				_instances.RemoveInstance(instanceIDs.InstanceID);
				_accelStruct.RemoveInstance(instanceIDs.AccelStructID);
			}
			_objectHandleToInstances.Remove(objectHandle);
		}

		public void UpdateInstanceTransform(int objectHandle, global::UnityEngine.Matrix4x4 localToWorldMatrix)
		{
			_objectHandleToInstances.TryGetValue(objectHandle, out var value);
			global::UnityEngine.Rendering.UnifiedRayTracing.AccelStructAdapter.InstanceIDs[] array = value;
			for (int i = 0; i < array.Length; i++)
			{
				global::UnityEngine.Rendering.UnifiedRayTracing.AccelStructAdapter.InstanceIDs instanceIDs = array[i];
				_instances.UpdateInstanceTransform(instanceIDs.InstanceID, localToWorldMatrix);
				_accelStruct.UpdateInstanceTransform(instanceIDs.AccelStructID, localToWorldMatrix);
			}
		}

		public void UpdateInstanceMaterialIDs(int objectHandle, global::System.Span<uint> perSubMeshMaterialIDs)
		{
			_objectHandleToInstances.TryGetValue(objectHandle, out var value);
			int num = 0;
			global::UnityEngine.Rendering.UnifiedRayTracing.AccelStructAdapter.InstanceIDs[] array = value;
			for (int i = 0; i < array.Length; i++)
			{
				global::UnityEngine.Rendering.UnifiedRayTracing.AccelStructAdapter.InstanceIDs instanceIDs = array[i];
				_instances.UpdateInstanceMaterialID(instanceIDs.InstanceID, perSubMeshMaterialIDs[num++]);
			}
		}

		public void UpdateInstanceMask(int objectHandle, global::System.Span<uint> perSubMeshMask)
		{
			_objectHandleToInstances.TryGetValue(objectHandle, out var value);
			int num = 0;
			global::UnityEngine.Rendering.UnifiedRayTracing.AccelStructAdapter.InstanceIDs[] array = value;
			for (int i = 0; i < array.Length; i++)
			{
				global::UnityEngine.Rendering.UnifiedRayTracing.AccelStructAdapter.InstanceIDs instanceIDs = array[i];
				_instances.UpdateInstanceMask(instanceIDs.InstanceID, perSubMeshMask[num]);
				_accelStruct.UpdateInstanceMask(instanceIDs.AccelStructID, perSubMeshMask[num]);
				num++;
			}
		}

		public void UpdateInstanceMask(int objectHandle, uint mask)
		{
			_objectHandleToInstances.TryGetValue(objectHandle, out var value);
			uint[] array = new uint[value.Length];
			global::System.Array.Fill(array, mask);
			int num = 0;
			global::UnityEngine.Rendering.UnifiedRayTracing.AccelStructAdapter.InstanceIDs[] array2 = value;
			for (int i = 0; i < array2.Length; i++)
			{
				global::UnityEngine.Rendering.UnifiedRayTracing.AccelStructAdapter.InstanceIDs instanceIDs = array2[i];
				_instances.UpdateInstanceMask(instanceIDs.InstanceID, array[num]);
				_accelStruct.UpdateInstanceMask(instanceIDs.AccelStructID, array[num]);
				num++;
			}
		}

		public void Build(global::UnityEngine.Rendering.CommandBuffer cmd, ref global::UnityEngine.GraphicsBuffer scratchBuffer)
		{
			global::UnityEngine.Rendering.UnifiedRayTracing.RayTracingHelper.ResizeScratchBufferForBuild(_accelStruct, ref scratchBuffer);
			_accelStruct.Build(cmd, scratchBuffer);
		}

		public void NextFrame()
		{
			_instances.NextFrame();
		}

		public bool GetInstanceIDs(int rendererID, out int[] instanceIDs)
		{
			if (!_objectHandleToInstances.TryGetValue(rendererID, out var value))
			{
				instanceIDs = null;
				return false;
			}
			instanceIDs = global::System.Array.ConvertAll(value, (global::UnityEngine.Rendering.UnifiedRayTracing.AccelStructAdapter.InstanceIDs item) => item.InstanceID);
			return true;
		}
	}
}
