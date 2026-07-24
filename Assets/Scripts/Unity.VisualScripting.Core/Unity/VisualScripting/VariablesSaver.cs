namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.Singleton(Name = "VisualScripting SavedVariablesSerializer", Automatic = true, Persistent = true)]
	[global::UnityEngine.AddComponentMenu("")]
	[global::Unity.VisualScripting.DisableAnnotation]
	[global::Unity.VisualScripting.IncludeInSettings(false)]
	public class VariablesSaver : global::UnityEngine.MonoBehaviour, global::Unity.VisualScripting.ISingleton
	{
		public static global::Unity.VisualScripting.VariablesSaver instance => global::Unity.VisualScripting.Singleton<global::Unity.VisualScripting.VariablesSaver>.instance;

		private void Awake()
		{
			global::Unity.VisualScripting.Singleton<global::Unity.VisualScripting.VariablesSaver>.Awake(this);
		}

		private void OnDestroy()
		{
			global::Unity.VisualScripting.Singleton<global::Unity.VisualScripting.VariablesSaver>.OnDestroy(this);
		}

		private void OnApplicationQuit()
		{
			global::Unity.VisualScripting.SavedVariables.OnExitPlayMode();
			global::Unity.VisualScripting.ApplicationVariables.OnExitPlayMode();
		}

		private void OnApplicationPause(bool isPaused)
		{
			if (isPaused)
			{
				global::Unity.VisualScripting.SavedVariables.OnExitPlayMode();
				global::Unity.VisualScripting.ApplicationVariables.OnExitPlayMode();
			}
		}

		public static void Instantiate()
		{
			global::Unity.VisualScripting.Singleton<global::Unity.VisualScripting.VariablesSaver>.Instantiate();
		}
	}
}
