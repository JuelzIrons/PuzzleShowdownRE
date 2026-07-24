namespace UnityEngine.Rendering.Universal
{
	internal class DecalDrawErrorSystem : global::UnityEngine.Rendering.Universal.DecalDrawSystem
	{
		private global::UnityEngine.Rendering.Universal.DecalTechnique m_Technique;

		public DecalDrawErrorSystem(global::UnityEngine.Rendering.Universal.DecalEntityManager entityManager, global::UnityEngine.Rendering.Universal.DecalTechnique technique)
			: base("DecalDrawErrorSystem.Execute", entityManager)
		{
			m_Technique = technique;
		}

		protected override int GetPassIndex(global::UnityEngine.Rendering.Universal.DecalCachedChunk decalCachedChunk)
		{
			switch (m_Technique)
			{
			case global::UnityEngine.Rendering.Universal.DecalTechnique.DBuffer:
				if (decalCachedChunk.passIndexDBuffer != -1 || decalCachedChunk.passIndexEmissive != -1)
				{
					return -1;
				}
				return 0;
			case global::UnityEngine.Rendering.Universal.DecalTechnique.ScreenSpace:
				if (decalCachedChunk.passIndexScreenSpace != -1)
				{
					return -1;
				}
				return 0;
			case global::UnityEngine.Rendering.Universal.DecalTechnique.GBuffer:
				if (decalCachedChunk.passIndexGBuffer != -1)
				{
					return -1;
				}
				return 0;
			case global::UnityEngine.Rendering.Universal.DecalTechnique.Invalid:
				return 0;
			default:
				return 0;
			}
		}

		protected override global::UnityEngine.Material GetMaterial(global::UnityEngine.Rendering.Universal.DecalEntityChunk decalEntityChunk)
		{
			return m_EntityManager.errorMaterial;
		}
	}
}
