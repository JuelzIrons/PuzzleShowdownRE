namespace UnityEngine.Rendering
{
	internal class ProbeVolumeBakingSetWeakReference
	{
		public int m_InstanceID;

		public ProbeVolumeBakingSetWeakReference(global::UnityEngine.Rendering.ProbeVolumeBakingSet bakingSet)
		{
			Set(bakingSet);
		}

		public ProbeVolumeBakingSetWeakReference()
		{
			m_InstanceID = 0;
		}

		public void Set(global::UnityEngine.Rendering.ProbeVolumeBakingSet bakingSet)
		{
			if (bakingSet == null)
			{
				m_InstanceID = 0;
			}
			else
			{
				m_InstanceID = bakingSet.GetInstanceID();
			}
		}

		public global::UnityEngine.Rendering.ProbeVolumeBakingSet Get()
		{
			return global::UnityEngine.Resources.EntityIdToObject(m_InstanceID) as global::UnityEngine.Rendering.ProbeVolumeBakingSet;
		}

		public bool IsLoaded()
		{
			return global::UnityEngine.Resources.EntityIdIsValid(m_InstanceID);
		}

		public void Unload()
		{
			if (IsLoaded())
			{
				global::UnityEngine.Resources.UnloadAsset(Get());
			}
		}
	}
}
