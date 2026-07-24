namespace UnityEngine.AdaptivePerformance
{
	internal static class AdaptivePerformanceInitializer
	{
		private static global::UnityEngine.AdaptivePerformance.AdaptivePerformanceManagerSpawner s_Spawner;

		[global::UnityEngine.Scripting.RequiredByNativeCode(false)]
		public static void AutoInitializeAdaptivePerformanceManaged()
		{
			InitializeSpawner(isAuto: true);
		}

		public static void Initialize()
		{
			InitializeSpawner(isAuto: false);
		}

		public static void Deinitialize()
		{
			if (!(s_Spawner == null))
			{
				s_Spawner.Deinitialize();
				global::UnityEngine.Object.Destroy(s_Spawner);
				s_Spawner = null;
			}
		}

		private static void InitializeSpawner(bool isAuto)
		{
			if (s_Spawner == null)
			{
				s_Spawner = global::UnityEngine.ScriptableObject.CreateInstance<global::UnityEngine.AdaptivePerformance.AdaptivePerformanceManagerSpawner>();
			}
			if (!(s_Spawner != null) || !(s_Spawner.ManagerGameObject != null))
			{
				s_Spawner.Initialize(isAuto);
			}
		}
	}
}
