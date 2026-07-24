namespace UnityEngine.Rendering.Universal
{
	[global::UnityEngine.Scripting.APIUpdating.MovedFrom(false, "UnityEngine.Experimental.Rendering.Universal", "com.unity.render-pipelines.universal", null)]
	public abstract class ShadowCasterGroup2D : global::UnityEngine.MonoBehaviour
	{
		[global::UnityEngine.SerializeField]
		internal int m_ShadowGroup;

		[global::UnityEngine.SerializeField]
		internal int m_Priority;

		private global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.ShadowCaster2D> m_ShadowCasters;

		internal virtual void CacheValues()
		{
			if (m_ShadowCasters != null)
			{
				for (int i = 0; i < m_ShadowCasters.Count; i++)
				{
					m_ShadowCasters[i].CacheValues();
				}
			}
		}

		public global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.ShadowCaster2D> GetShadowCasters()
		{
			return m_ShadowCasters;
		}

		public int GetShadowGroup()
		{
			return m_ShadowGroup;
		}

		public void RegisterShadowCaster2D(global::UnityEngine.Rendering.Universal.ShadowCaster2D shadowCaster2D)
		{
			if (m_ShadowCasters == null)
			{
				m_ShadowCasters = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.ShadowCaster2D>();
			}
			int num = 0;
			for (num = 0; num < m_ShadowCasters.Count && shadowCaster2D.m_Priority < m_ShadowCasters[num].m_Priority; num++)
			{
			}
			m_ShadowCasters.Insert(num, shadowCaster2D);
		}

		public void UnregisterShadowCaster2D(global::UnityEngine.Rendering.Universal.ShadowCaster2D shadowCaster2D)
		{
			if (m_ShadowCasters != null)
			{
				m_ShadowCasters.Remove(shadowCaster2D);
			}
		}
	}
}
