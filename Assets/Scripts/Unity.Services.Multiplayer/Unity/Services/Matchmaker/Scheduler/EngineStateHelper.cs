namespace Unity.Services.Matchmaker.Scheduler
{
	internal static class EngineStateHelper
	{
		public static bool IsPlaying;

		[global::UnityEngine.RuntimeInitializeOnLoadMethod(global::UnityEngine.RuntimeInitializeLoadType.BeforeSceneLoad)]
		public static void Init()
		{
			IsPlaying = global::UnityEngine.Application.isPlaying;
		}
	}
}
