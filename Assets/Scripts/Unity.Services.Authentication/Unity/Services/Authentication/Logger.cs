namespace Unity.Services.Authentication
{
	internal static class Logger
	{
		private const string k_Tag = "[Authentication]";

		internal const string k_GlobalVerboseLoggingDefine = "ENABLE_UNITY_SERVICES_VERBOSE_LOGGING";

		internal const string k_AuthenticationVerboseLoggingDefine = "ENABLE_UNITY_AUTHENTICATION_VERBOSE_LOGGING";

		public static void Log(object message)
		{
			global::UnityEngine.Debug.unityLogger.Log("[Authentication]", message);
		}

		public static void LogWarning(object message)
		{
			global::UnityEngine.Debug.unityLogger.LogWarning("[Authentication]", message);
		}

		public static void LogError(object message)
		{
			global::UnityEngine.Debug.unityLogger.LogError("[Authentication]", message);
		}

		public static void LogException(global::System.Exception exception)
		{
			global::UnityEngine.Debug.unityLogger.Log(global::UnityEngine.LogType.Exception, "[Authentication]", exception);
		}

		[global::System.Diagnostics.Conditional("UNITY_ASSERTIONS")]
		public static void LogAssertion(object message)
		{
			global::UnityEngine.Debug.unityLogger.Log(global::UnityEngine.LogType.Assert, "[Authentication]", message);
		}

		[global::System.Diagnostics.Conditional("ENABLE_UNITY_SERVICES_VERBOSE_LOGGING")]
		[global::System.Diagnostics.Conditional("ENABLE_UNITY_AUTHENTICATION_VERBOSE_LOGGING")]
		public static void LogVerbose(object message)
		{
			global::UnityEngine.Debug.unityLogger.Log("[Authentication]", message);
		}
	}
}
