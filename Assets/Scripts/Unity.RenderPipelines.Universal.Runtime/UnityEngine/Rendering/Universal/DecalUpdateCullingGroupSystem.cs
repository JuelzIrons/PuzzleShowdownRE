namespace UnityEngine.Rendering.Universal
{
	internal class DecalUpdateCullingGroupSystem
	{
		private float[] m_BoundingDistance = new float[1];

		private global::UnityEngine.Camera m_Camera;

		private global::UnityEngine.Rendering.Universal.DecalEntityManager m_EntityManager;

		private global::UnityEngine.Rendering.ProfilingSampler m_Sampler;

		public float boundingDistance
		{
			get
			{
				return m_BoundingDistance[0];
			}
			set
			{
				m_BoundingDistance[0] = value;
			}
		}

		public DecalUpdateCullingGroupSystem(global::UnityEngine.Rendering.Universal.DecalEntityManager entityManager, float drawDistance)
		{
			m_EntityManager = entityManager;
			m_BoundingDistance[0] = drawDistance;
			m_Sampler = new global::UnityEngine.Rendering.ProfilingSampler("DecalUpdateCullingGroupsSystem.Execute");
		}

		public void Execute(global::UnityEngine.Camera camera)
		{
			using (new global::UnityEngine.Rendering.ProfilingScope(m_Sampler))
			{
				m_Camera = camera;
				for (int i = 0; i < m_EntityManager.chunkCount; i++)
				{
					Execute(m_EntityManager.cachedChunks[i], m_EntityManager.culledChunks[i], m_EntityManager.culledChunks[i].count);
				}
			}
		}

		public void Execute(global::UnityEngine.Rendering.Universal.DecalCachedChunk cachedChunk, global::UnityEngine.Rendering.Universal.DecalCulledChunk culledChunk, int count)
		{
			cachedChunk.currentJobHandle.Complete();
			global::UnityEngine.CullingGroup cullingGroups = culledChunk.cullingGroups;
			cullingGroups.targetCamera = m_Camera;
			cullingGroups.SetDistanceReferencePoint(m_Camera.transform.position);
			cullingGroups.SetBoundingDistances(m_BoundingDistance);
			cachedChunk.boundingSpheres.CopyTo(cachedChunk.boundingSphereArray);
			cullingGroups.SetBoundingSpheres(cachedChunk.boundingSphereArray);
			cullingGroups.SetBoundingSphereCount(count);
			culledChunk.cameraPosition = m_Camera.transform.position;
			culledChunk.cullingMask = m_Camera.cullingMask;
		}

		internal static ulong GetSceneCullingMaskFromCamera(global::UnityEngine.Camera camera)
		{
			return 0uL;
		}
	}
}
