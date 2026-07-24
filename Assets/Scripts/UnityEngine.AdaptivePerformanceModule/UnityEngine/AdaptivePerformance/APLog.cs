namespace UnityEngine.AdaptivePerformance
{
	internal static class APLog
	{
		public static bool enabled;

		public static void Debug(string format, params object[] args)
		{
			if (enabled && global::UnityEngine.Debug.isDebugBuild)
			{
				global::UnityEngine.Debug.Log(string.Format("[Adaptive Performance] " + format, args));
			}
		}
	}
}
