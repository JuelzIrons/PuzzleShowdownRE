namespace Unity.Services.Core.Internal
{
	internal static class CoreLogger
	{
		internal const string Tag = "[ServicesCore]";

		internal const string VerboseLoggingDefine = "ENABLE_UNITY_SERVICES_CORE_VERBOSE_LOGGING";

		private const string k_TelemetryLoggingDefine = "ENABLE_UNITY_SERVICES_CORE_TELEMETRY_LOGGING";

		public static void Log(object message)
		{
			global::UnityEngine.Debug.unityLogger.Log("[ServicesCore]", message);
		}

		public static void LogWarning(object message)
		{
			global::UnityEngine.Debug.unityLogger.LogWarning("[ServicesCore]", message);
		}

		public static void LogError(object message)
		{
			global::UnityEngine.Debug.unityLogger.LogError("[ServicesCore]", message);
		}

		public static void LogException(global::System.Exception exception)
		{
			global::UnityEngine.Debug.unityLogger.Log(global::UnityEngine.LogType.Exception, "[ServicesCore]", exception);
		}

		[global::System.Diagnostics.Conditional("UNITY_ASSERTIONS")]
		public static void LogAssertion(object message)
		{
			global::UnityEngine.Debug.unityLogger.Log(global::UnityEngine.LogType.Assert, "[ServicesCore]", message);
		}

		[global::System.Diagnostics.Conditional("ENABLE_UNITY_SERVICES_CORE_VERBOSE_LOGGING")]
		public static void LogVerbose(object message)
		{
			global::UnityEngine.Debug.unityLogger.Log("[ServicesCore]", message);
		}

		[global::System.Diagnostics.Conditional("ENABLE_UNITY_SERVICES_CORE_TELEMETRY_LOGGING")]
		public static void LogTelemetry(object message)
		{
			global::UnityEngine.Debug.unityLogger.Log("[ServicesCore]", message);
		}
	}
}
