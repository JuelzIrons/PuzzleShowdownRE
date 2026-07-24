namespace UnityEngine.AdaptivePerformance
{
	public static class Holder
	{
		private static global::UnityEngine.AdaptivePerformance.IAdaptivePerformance m_Instance;

		public static global::UnityEngine.AdaptivePerformance.IAdaptivePerformance Instance
		{
			get
			{
				return m_Instance;
			}
			internal set
			{
				if (value == null)
				{
					global::UnityEngine.AdaptivePerformance.Holder.LifecycleEventHandler?.Invoke(m_Instance, global::UnityEngine.AdaptivePerformance.LifecycleChangeType.Destroyed);
				}
				else
				{
					global::UnityEngine.AdaptivePerformance.Holder.LifecycleEventHandler?.Invoke(value, global::UnityEngine.AdaptivePerformance.LifecycleChangeType.Created);
				}
				m_Instance = value;
			}
		}

		public static event global::UnityEngine.AdaptivePerformance.LifecycleEventHandler LifecycleEventHandler;

		public static void Initialize()
		{
			if (Instance == null)
			{
				global::UnityEngine.AdaptivePerformance.AdaptivePerformanceInitializer.Initialize();
				if (Instance != null)
				{
					Instance.InitializeAdaptivePerformance();
				}
			}
		}

		public static void Deinitialize()
		{
			if (Instance != null)
			{
				Instance.DeinitializeAdaptivePerformance();
			}
			global::UnityEngine.AdaptivePerformance.AdaptivePerformanceInitializer.Deinitialize();
			Instance = null;
		}
	}
}
