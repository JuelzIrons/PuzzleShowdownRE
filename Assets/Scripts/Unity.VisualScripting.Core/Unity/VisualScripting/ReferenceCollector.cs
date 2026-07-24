namespace Unity.VisualScripting
{
	public static class ReferenceCollector
	{
		public static event global::System.Action onSceneUnloaded;

		internal static void Initialize()
		{
			global::UnityEngine.SceneManagement.SceneManager.sceneUnloaded += delegate
			{
				global::Unity.VisualScripting.ReferenceCollector.onSceneUnloaded?.Invoke();
			};
		}
	}
}
