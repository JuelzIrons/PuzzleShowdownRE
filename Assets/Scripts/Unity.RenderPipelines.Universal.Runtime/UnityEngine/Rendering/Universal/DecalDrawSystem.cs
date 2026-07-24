namespace UnityEngine.Rendering.Universal
{
	internal abstract class DecalDrawSystem
	{
		internal static readonly uint MaxBatchSize = 250u;

		protected global::UnityEngine.Rendering.Universal.DecalEntityManager m_EntityManager;

		private global::UnityEngine.Matrix4x4[] m_WorldToDecals;

		private global::UnityEngine.Matrix4x4[] m_NormalToDecals;

		private float[] m_DecalLayerMasks;

		private global::UnityEngine.Rendering.ProfilingSampler m_Sampler;

		public global::UnityEngine.Material overrideMaterial { get; set; }

		public DecalDrawSystem(string sampler, global::UnityEngine.Rendering.Universal.DecalEntityManager entityManager)
		{
			m_EntityManager = entityManager;
			m_WorldToDecals = new global::UnityEngine.Matrix4x4[MaxBatchSize];
			m_NormalToDecals = new global::UnityEngine.Matrix4x4[MaxBatchSize];
			m_DecalLayerMasks = new float[MaxBatchSize];
			m_Sampler = new global::UnityEngine.Rendering.ProfilingSampler(sampler);
		}

		public void Execute(global::UnityEngine.Rendering.CommandBuffer cmd)
		{
			Execute(global::UnityEngine.Rendering.CommandBufferHelpers.GetRasterCommandBuffer(cmd));
		}

		internal void Execute(global::UnityEngine.Rendering.RasterCommandBuffer cmd)
		{
			using (new global::UnityEngine.Rendering.ProfilingScope(cmd, m_Sampler))
			{
				for (int i = 0; i < m_EntityManager.chunkCount; i++)
				{
					Execute(cmd, m_EntityManager.entityChunks[i], m_EntityManager.cachedChunks[i], m_EntityManager.drawCallChunks[i], m_EntityManager.entityChunks[i].count);
				}
			}
		}

		protected virtual global::UnityEngine.Material GetMaterial(global::UnityEngine.Rendering.Universal.DecalEntityChunk decalEntityChunk)
		{
			return decalEntityChunk.material;
		}

		protected abstract int GetPassIndex(global::UnityEngine.Rendering.Universal.DecalCachedChunk decalCachedChunk);

		private void Execute(global::UnityEngine.Rendering.RasterCommandBuffer cmd, global::UnityEngine.Rendering.Universal.DecalEntityChunk decalEntityChunk, global::UnityEngine.Rendering.Universal.DecalCachedChunk decalCachedChunk, global::UnityEngine.Rendering.Universal.DecalDrawCallChunk decalDrawCallChunk, int count)
		{
			decalCachedChunk.currentJobHandle.Complete();
			decalDrawCallChunk.currentJobHandle.Complete();
			global::UnityEngine.Material material = GetMaterial(decalEntityChunk);
			int passIndex = GetPassIndex(decalCachedChunk);
			if (count != 0 && passIndex != -1 && !(material == null))
			{
				if (global::UnityEngine.SystemInfo.supportsInstancing && material.enableInstancing)
				{
					DrawInstanced(cmd, decalEntityChunk, decalCachedChunk, decalDrawCallChunk, passIndex);
				}
				else
				{
					Draw(cmd, decalEntityChunk, decalCachedChunk, decalDrawCallChunk, passIndex);
				}
			}
		}

		private void Draw(global::UnityEngine.Rendering.RasterCommandBuffer cmd, global::UnityEngine.Rendering.Universal.DecalEntityChunk decalEntityChunk, global::UnityEngine.Rendering.Universal.DecalCachedChunk decalCachedChunk, global::UnityEngine.Rendering.Universal.DecalDrawCallChunk decalDrawCallChunk, int passIndex)
		{
			global::UnityEngine.Mesh decalProjectorMesh = m_EntityManager.decalProjectorMesh;
			global::UnityEngine.Material material = GetMaterial(decalEntityChunk);
			decalCachedChunk.propertyBlock.SetVector("unity_LightData", new global::UnityEngine.Vector4(1f, 1f, 1f, 0f));
			int subCallCount = decalDrawCallChunk.subCallCount;
			for (int i = 0; i < subCallCount; i++)
			{
				global::UnityEngine.Rendering.Universal.DecalSubDrawCall decalSubDrawCall = decalDrawCallChunk.subCalls[i];
				for (int j = decalSubDrawCall.start; j < decalSubDrawCall.end; j++)
				{
					decalCachedChunk.propertyBlock.SetMatrix("_NormalToWorld", decalDrawCallChunk.normalToDecals[j]);
					decalCachedChunk.propertyBlock.SetFloat("_DecalLayerMaskFromDecal", decalDrawCallChunk.renderingLayerMasks[j]);
					cmd.DrawMesh(decalProjectorMesh, decalDrawCallChunk.decalToWorlds[j], material, 0, passIndex, decalCachedChunk.propertyBlock);
				}
			}
		}

		private void DrawInstanced(global::UnityEngine.Rendering.RasterCommandBuffer cmd, global::UnityEngine.Rendering.Universal.DecalEntityChunk decalEntityChunk, global::UnityEngine.Rendering.Universal.DecalCachedChunk decalCachedChunk, global::UnityEngine.Rendering.Universal.DecalDrawCallChunk decalDrawCallChunk, int passIndex)
		{
			global::UnityEngine.Mesh decalProjectorMesh = m_EntityManager.decalProjectorMesh;
			global::UnityEngine.Material material = GetMaterial(decalEntityChunk);
			decalCachedChunk.propertyBlock.SetVector("unity_LightData", new global::UnityEngine.Vector4(1f, 1f, 1f, 0f));
			int subCallCount = decalDrawCallChunk.subCallCount;
			for (int i = 0; i < subCallCount; i++)
			{
				global::UnityEngine.Rendering.Universal.DecalSubDrawCall decalSubDrawCall = decalDrawCallChunk.subCalls[i];
				global::Unity.Collections.NativeArray<global::UnityEngine.Matrix4x4>.Copy(decalDrawCallChunk.decalToWorlds.Reinterpret<global::UnityEngine.Matrix4x4>(), decalSubDrawCall.start, m_WorldToDecals, 0, decalSubDrawCall.count);
				global::Unity.Collections.NativeArray<global::UnityEngine.Matrix4x4>.Copy(decalDrawCallChunk.normalToDecals.Reinterpret<global::UnityEngine.Matrix4x4>(), decalSubDrawCall.start, m_NormalToDecals, 0, decalSubDrawCall.count);
				global::Unity.Collections.NativeArray<float>.Copy(decalDrawCallChunk.renderingLayerMasks.Reinterpret<float>(), decalSubDrawCall.start, m_DecalLayerMasks, 0, decalSubDrawCall.count);
				decalCachedChunk.propertyBlock.SetMatrixArray("_NormalToWorld", m_NormalToDecals);
				decalCachedChunk.propertyBlock.SetFloatArray("_DecalLayerMaskFromDecal", m_DecalLayerMasks);
				cmd.DrawMeshInstanced(decalProjectorMesh, 0, material, passIndex, m_WorldToDecals, decalSubDrawCall.end - decalSubDrawCall.start, decalCachedChunk.propertyBlock);
			}
		}

		public void Execute(in global::UnityEngine.Rendering.Universal.CameraData cameraData)
		{
			using (new global::UnityEngine.Rendering.ProfilingScope(m_Sampler))
			{
				for (int i = 0; i < m_EntityManager.chunkCount; i++)
				{
					Execute(in cameraData, m_EntityManager.entityChunks[i], m_EntityManager.cachedChunks[i], m_EntityManager.drawCallChunks[i], m_EntityManager.entityChunks[i].count);
				}
			}
		}

		private void Execute(in global::UnityEngine.Rendering.Universal.CameraData cameraData, global::UnityEngine.Rendering.Universal.DecalEntityChunk decalEntityChunk, global::UnityEngine.Rendering.Universal.DecalCachedChunk decalCachedChunk, global::UnityEngine.Rendering.Universal.DecalDrawCallChunk decalDrawCallChunk, int count)
		{
			decalCachedChunk.currentJobHandle.Complete();
			decalDrawCallChunk.currentJobHandle.Complete();
			global::UnityEngine.Material material = GetMaterial(decalEntityChunk);
			int passIndex = GetPassIndex(decalCachedChunk);
			if (count != 0 && passIndex != -1 && !(material == null))
			{
				if (global::UnityEngine.SystemInfo.supportsInstancing && material.enableInstancing)
				{
					DrawInstanced(in cameraData, decalEntityChunk, decalCachedChunk, decalDrawCallChunk);
				}
				else
				{
					Draw(in cameraData, decalEntityChunk, decalCachedChunk, decalDrawCallChunk);
				}
			}
		}

		private void Draw(in global::UnityEngine.Rendering.Universal.CameraData cameraData, global::UnityEngine.Rendering.Universal.DecalEntityChunk decalEntityChunk, global::UnityEngine.Rendering.Universal.DecalCachedChunk decalCachedChunk, global::UnityEngine.Rendering.Universal.DecalDrawCallChunk decalDrawCallChunk)
		{
			global::UnityEngine.Mesh decalProjectorMesh = m_EntityManager.decalProjectorMesh;
			global::UnityEngine.Material material = GetMaterial(decalEntityChunk);
			int subCallCount = decalDrawCallChunk.subCallCount;
			for (int i = 0; i < subCallCount; i++)
			{
				global::UnityEngine.Rendering.Universal.DecalSubDrawCall decalSubDrawCall = decalDrawCallChunk.subCalls[i];
				for (int j = decalSubDrawCall.start; j < decalSubDrawCall.end; j++)
				{
					decalCachedChunk.propertyBlock.SetMatrix("_NormalToWorld", decalDrawCallChunk.normalToDecals[j]);
					decalCachedChunk.propertyBlock.SetFloat("_DecalLayerMaskFromDecal", decalDrawCallChunk.renderingLayerMasks[j]);
					global::UnityEngine.Graphics.DrawMesh(decalProjectorMesh, decalDrawCallChunk.decalToWorlds[j], material, decalCachedChunk.layerMasks[j], cameraData.camera, 0, decalCachedChunk.propertyBlock);
				}
			}
		}

		private void DrawInstanced(in global::UnityEngine.Rendering.Universal.CameraData cameraData, global::UnityEngine.Rendering.Universal.DecalEntityChunk decalEntityChunk, global::UnityEngine.Rendering.Universal.DecalCachedChunk decalCachedChunk, global::UnityEngine.Rendering.Universal.DecalDrawCallChunk decalDrawCallChunk)
		{
			global::UnityEngine.Mesh decalProjectorMesh = m_EntityManager.decalProjectorMesh;
			global::UnityEngine.Material material = GetMaterial(decalEntityChunk);
			decalCachedChunk.propertyBlock.SetVector("unity_LightData", new global::UnityEngine.Vector4(1f, 1f, 1f, 0f));
			int subCallCount = decalDrawCallChunk.subCallCount;
			for (int i = 0; i < subCallCount; i++)
			{
				global::UnityEngine.Rendering.Universal.DecalSubDrawCall decalSubDrawCall = decalDrawCallChunk.subCalls[i];
				global::Unity.Collections.NativeArray<global::UnityEngine.Matrix4x4>.Copy(decalDrawCallChunk.decalToWorlds.Reinterpret<global::UnityEngine.Matrix4x4>(), decalSubDrawCall.start, m_WorldToDecals, 0, decalSubDrawCall.count);
				global::Unity.Collections.NativeArray<global::UnityEngine.Matrix4x4>.Copy(decalDrawCallChunk.normalToDecals.Reinterpret<global::UnityEngine.Matrix4x4>(), decalSubDrawCall.start, m_NormalToDecals, 0, decalSubDrawCall.count);
				global::Unity.Collections.NativeArray<float>.Copy(decalDrawCallChunk.renderingLayerMasks.Reinterpret<float>(), decalSubDrawCall.start, m_DecalLayerMasks, 0, decalSubDrawCall.count);
				decalCachedChunk.propertyBlock.SetMatrixArray("_NormalToWorld", m_NormalToDecals);
				decalCachedChunk.propertyBlock.SetFloatArray("_DecalLayerMaskFromDecal", m_DecalLayerMasks);
				global::UnityEngine.Graphics.DrawMeshInstanced(decalProjectorMesh, 0, material, m_WorldToDecals, decalSubDrawCall.count, decalCachedChunk.propertyBlock, global::UnityEngine.Rendering.ShadowCastingMode.On, receiveShadows: true, 0, cameraData.camera);
			}
		}
	}
}
