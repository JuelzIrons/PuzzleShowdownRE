namespace UnityEngine.Rendering.Universal
{
	internal class DecalUpdateCulledSystem
	{
		private global::UnityEngine.Rendering.Universal.DecalEntityManager m_EntityManager;

		private global::UnityEngine.Rendering.ProfilingSampler m_Sampler;

		public DecalUpdateCulledSystem(global::UnityEngine.Rendering.Universal.DecalEntityManager entityManager)
		{
			m_EntityManager = entityManager;
			m_Sampler = new global::UnityEngine.Rendering.ProfilingSampler("DecalUpdateCulledSystem.Execute");
		}

		public void Execute()
		{
			using (new global::UnityEngine.Rendering.ProfilingScope(m_Sampler))
			{
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
				global::UnityEngine.CullingGroup cullingGroups = culledChunk.cullingGroups;
				culledChunk.visibleDecalCount = cullingGroups.QueryIndices(visible: true, culledChunk.visibleDecalIndexArray, 0);
				culledChunk.visibleDecalIndices.CopyFrom(culledChunk.visibleDecalIndexArray);
			}
		}
	}
}
