namespace Unity.Services.Wire.Internal
{
	internal class Logger
	{
		private const string k_Tag = "[Wire]";

		private const string k_VerboseLoggingDefine = "ENABLE_UNITY_WIRE_VERBOSE_LOGGING";

		[global::System.Diagnostics.Conditional("ENABLE_UNITY_WIRE_VERBOSE_LOGGING")]
		public static void Log(object message)
		{
			global::UnityEngine.Debug.unityLogger.Log("[Wire]", message);
		}

		[global::System.Diagnostics.Conditional("ENABLE_UNITY_WIRE_VERBOSE_LOGGING")]
		public static void LogWarning(object message)
		{
			global::UnityEngine.Debug.unityLogger.LogWarning("[Wire]", message);
		}

		[global::System.Diagnostics.Conditional("ENABLE_UNITY_WIRE_VERBOSE_LOGGING")]
		public static void LogError(object message)
		{
			global::UnityEngine.Debug.unityLogger.LogError("[Wire]", message);
		}

		[global::System.Diagnostics.Conditional("ENABLE_UNITY_WIRE_VERBOSE_LOGGING")]
		public static void LogException(global::System.Exception exception)
		{
			global::UnityEngine.Debug.unityLogger.Log(global::UnityEngine.LogType.Exception, "[Wire]", exception);
		}

		[global::System.Diagnostics.Conditional("UNITY_ASSERTIONS")]
		public static void LogAssertion(object message)
		{
			global::UnityEngine.Debug.unityLogger.Log(global::UnityEngine.LogType.Assert, "[Wire]", message);
		}
	}
}
