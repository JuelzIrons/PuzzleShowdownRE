namespace Unity.VisualScripting
{
	public static class RuntimeVSUsageUtility
	{
		[global::UnityEngine.RuntimeInitializeOnLoadMethod(global::UnityEngine.RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void RuntimeInitializeOnLoadBeforeSceneLoad()
		{
			global::Unity.VisualScripting.UnityThread.RuntimeInitialize();
			global::Unity.VisualScripting.Ensure.OnRuntimeMethodLoad();
			global::Unity.VisualScripting.Recursion.OnRuntimeMethodLoad();
			global::Unity.VisualScripting.OptimizedReflection.OnRuntimeMethodLoad();
			global::Unity.VisualScripting.SavedVariables.OnEnterPlayMode();
			global::Unity.VisualScripting.ApplicationVariables.OnEnterPlayMode();
			global::Unity.VisualScripting.ReferenceCollector.Initialize();
		}
	}
}
