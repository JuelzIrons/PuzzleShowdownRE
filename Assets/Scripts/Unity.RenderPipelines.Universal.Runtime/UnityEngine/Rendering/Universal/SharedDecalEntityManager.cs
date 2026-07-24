namespace UnityEngine.Rendering.Universal
{
	internal class SharedDecalEntityManager : global::System.IDisposable
	{
		private global::UnityEngine.Rendering.Universal.DecalEntityManager m_DecalEntityManager;

		private int m_ReferenceCounter;

		public global::UnityEngine.Rendering.Universal.DecalEntityManager Get()
		{
			if (m_DecalEntityManager == null)
			{
				m_DecalEntityManager = new global::UnityEngine.Rendering.Universal.DecalEntityManager();
				global::UnityEngine.Rendering.Universal.DecalProjector[] array = global::UnityEngine.Object.FindObjectsByType<global::UnityEngine.Rendering.Universal.DecalProjector>(global::UnityEngine.FindObjectsSortMode.InstanceID);
				foreach (global::UnityEngine.Rendering.Universal.DecalProjector decalProjector in array)
				{
					if (decalProjector.isActiveAndEnabled && !m_DecalEntityManager.IsValid(decalProjector.decalEntity))
					{
						decalProjector.decalEntity = m_DecalEntityManager.CreateDecalEntity(decalProjector);
					}
				}
				global::UnityEngine.Rendering.Universal.DecalProjector.onDecalAdd += OnDecalAdd;
				global::UnityEngine.Rendering.Universal.DecalProjector.onDecalRemove += OnDecalRemove;
				global::UnityEngine.Rendering.Universal.DecalProjector.onDecalPropertyChange += OnDecalPropertyChange;
				global::UnityEngine.Rendering.Universal.DecalProjector.onDecalMaterialChange += OnDecalMaterialChange;
				global::UnityEngine.Rendering.Universal.DecalProjector.onAllDecalPropertyChange += OnAllDecalPropertyChange;
			}
			m_ReferenceCounter++;
			return m_DecalEntityManager;
		}

		public void Release(global::UnityEngine.Rendering.Universal.DecalEntityManager decalEntityManager)
		{
			if (m_ReferenceCounter != 0)
			{
				m_ReferenceCounter--;
				if (m_ReferenceCounter == 0)
				{
					Dispose();
				}
			}
		}

		public void Dispose()
		{
			m_DecalEntityManager.Dispose();
			m_DecalEntityManager = null;
			m_ReferenceCounter = 0;
			global::UnityEngine.Rendering.Universal.DecalProjector.onDecalAdd -= OnDecalAdd;
			global::UnityEngine.Rendering.Universal.DecalProjector.onDecalRemove -= OnDecalRemove;
			global::UnityEngine.Rendering.Universal.DecalProjector.onDecalPropertyChange -= OnDecalPropertyChange;
			global::UnityEngine.Rendering.Universal.DecalProjector.onDecalMaterialChange -= OnDecalMaterialChange;
			global::UnityEngine.Rendering.Universal.DecalProjector.onAllDecalPropertyChange -= OnAllDecalPropertyChange;
		}

		private void OnDecalAdd(global::UnityEngine.Rendering.Universal.DecalProjector decalProjector)
		{
			if (!m_DecalEntityManager.IsValid(decalProjector.decalEntity))
			{
				decalProjector.decalEntity = m_DecalEntityManager.CreateDecalEntity(decalProjector);
			}
		}

		private void OnDecalRemove(global::UnityEngine.Rendering.Universal.DecalProjector decalProjector)
		{
			m_DecalEntityManager.DestroyDecalEntity(decalProjector.decalEntity);
		}

		private void OnDecalPropertyChange(global::UnityEngine.Rendering.Universal.DecalProjector decalProjector)
		{
			if (m_DecalEntityManager.IsValid(decalProjector.decalEntity))
			{
				m_DecalEntityManager.UpdateDecalEntityData(decalProjector.decalEntity, decalProjector);
			}
		}

		private void OnAllDecalPropertyChange()
		{
			m_DecalEntityManager.UpdateAllDecalEntitiesData();
		}

		private void OnDecalMaterialChange(global::UnityEngine.Rendering.Universal.DecalProjector decalProjector)
		{
			OnDecalRemove(decalProjector);
			OnDecalAdd(decalProjector);
		}
	}
}
