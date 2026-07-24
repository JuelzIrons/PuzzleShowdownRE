namespace UnityEngine.Rendering.Universal
{
	internal class DecalSkipCulledSystem
	{
		private global::UnityEngine.Rendering.Universal.DecalEntityManager m_EntityManager;

		private global::UnityEngine.Rendering.ProfilingSampler m_Sampler;

		private global::UnityEngine.Camera m_Camera;

		public DecalSkipCulledSystem(global::UnityEngine.Rendering.Universal.DecalEntityManager entityManager)
		{
			m_EntityManager = entityManager;
			m_Sampler = new global::UnityEngine.Rendering.ProfilingSampler("DecalSkipCulledSystem.Execute");
		}

		public void Execute(global::UnityEngine.Camera camera)
		{
			using (new global::UnityEngine.Rendering.ProfilingScope(m_Sampler))
			{
				m_Camera = camera;
				for (int i = 0; i < m_EntityManager.chunkCount; i++)
				{
					Execute(m_EntityManager.culledChunks[i], m_EntityManager.culledChunks[i].count);
				}
			}
		}

		private void Execute(global::UnityEngine.Rendering.Universal.DecalCulledChunk culledChunk, int count)
		{
			if (count != 0)
			{
				culledChunk.currentJobHandle.Complete();
				for (int i = 0; i < count; i++)
				{
					culledChunk.visibleDecalIndices[i] = i;
				}
				culledChunk.visibleDecalCount = count;
				culledChunk.cameraPosition = m_Camera.transform.position;
				culledChunk.cullingMask = m_Camera.cullingMask;
			}
		}

		internal static ulong GetSceneCullingMaskFromCamera(global::UnityEngine.Camera camera)
		{
			return 0uL;
		}
	}
}
