namespace UnityEngine.U2D.IK
{
	[global::UnityEngine.ExecuteInEditMode]
	internal class CullingManager : global::UnityEngine.MonoBehaviour
	{
		private static global::UnityEngine.U2D.IK.CullingManager s_Instance;

		private global::Unity.Profiling.ProfilerMarker m_ProfilerMarker = new global::Unity.Profiling.ProfilerMarker("CullingManager.OnUpdate");

		private global::System.Collections.Generic.Dictionary<global::System.Type, global::UnityEngine.U2D.IK.BaseCullingStrategy> m_CullingStrategies;

		public static global::UnityEngine.U2D.IK.CullingManager instance
		{
			get
			{
				if (s_Instance == null)
				{
					global::UnityEngine.U2D.IK.CullingManager[] array = global::UnityEngine.Object.FindObjectsByType<global::UnityEngine.U2D.IK.CullingManager>(global::UnityEngine.FindObjectsSortMode.None);
					s_Instance = ((array.Length != 0) ? array[0] : CreateNewManager());
					s_Instance.Initialize();
				}
				return s_Instance;
			}
		}

		private static global::UnityEngine.U2D.IK.CullingManager CreateNewManager()
		{
			global::UnityEngine.GameObject obj = new global::UnityEngine.GameObject("Culling Manager")
			{
				hideFlags = global::UnityEngine.HideFlags.HideAndDontSave
			};
			global::UnityEngine.Object.DontDestroyOnLoad(obj);
			return obj.AddComponent<global::UnityEngine.U2D.IK.CullingManager>();
		}

		private void Initialize()
		{
			m_CullingStrategies = new global::System.Collections.Generic.Dictionary<global::System.Type, global::UnityEngine.U2D.IK.BaseCullingStrategy>();
			AddCullingStrategy(new global::UnityEngine.U2D.IK.AlwaysUpdateCullingStrategy());
			AddCullingStrategy(new global::UnityEngine.U2D.IK.SpriteSkinVisibilityCullingStrategy());
		}

		private void Update()
		{
			OnUpdate();
		}

		private void OnUpdate()
		{
			if (m_CullingStrategies == null)
			{
				return;
			}
			foreach (global::UnityEngine.U2D.IK.BaseCullingStrategy value in m_CullingStrategies.Values)
			{
				if (value.enabled)
				{
					value.Update();
				}
			}
		}

		public void AddCullingStrategy(global::UnityEngine.U2D.IK.BaseCullingStrategy newCullingStrategy)
		{
			global::System.Type type = newCullingStrategy.GetType();
			if (!m_CullingStrategies.ContainsKey(type))
			{
				m_CullingStrategies[newCullingStrategy.GetType()] = newCullingStrategy;
			}
		}

		public void RemoveCullingStrategy(global::UnityEngine.U2D.IK.BaseCullingStrategy strategyToRemove)
		{
			global::System.Type type = strategyToRemove.GetType();
			if (m_CullingStrategies.ContainsKey(type) && m_CullingStrategies[type] == strategyToRemove)
			{
				m_CullingStrategies.Remove(type);
			}
		}

		public T GetCullingStrategy<T>() where T : global::UnityEngine.U2D.IK.BaseCullingStrategy
		{
			global::System.Type typeFromHandle = typeof(T);
			if (!m_CullingStrategies.ContainsKey(typeFromHandle))
			{
				return null;
			}
			return (T)m_CullingStrategies[typeFromHandle];
		}
	}
}
