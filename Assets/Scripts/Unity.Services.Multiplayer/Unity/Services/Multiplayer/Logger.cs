namespace Unity.Services.Multiplayer
{
	internal class Logger
	{
		private const string k_Tag = "[Multiplayer]";

		private const string k_UnityAssertions = "UNITY_ASSERTIONS";

		private const string k_VerboseLoggingDefine = "ENABLE_UNITY_MULTIPLAYER_VERBOSE_LOGGING";

		private static void Log(global::UnityEngine.LogType level, object message)
		{
			global::UnityEngine.Debug.unityLogger.Log(level, "[Multiplayer]", message);
		}

		public static void Log(object message)
		{
			Log(global::UnityEngine.LogType.Log, message);
		}

		public static void LogWarning(object message)
		{
			Log(global::UnityEngine.LogType.Warning, message);
		}

		public static void LogCallWarning(string enclosingType, string message, [global::System.Runtime.CompilerServices.CallerMemberName] string method = "")
		{
			LogWarning(enclosingType + "." + method + ": " + message);
		}

		public static void LogError(object message)
		{
			Log(global::UnityEngine.LogType.Error, message);
		}

		public static void LogCallError(string enclosingType, string message, [global::System.Runtime.CompilerServices.CallerMemberName] string method = "")
		{
			LogError(enclosingType + "." + method + ": " + message);
		}

		public static void LogException(global::System.Exception exception)
		{
			Log(global::UnityEngine.LogType.Exception, exception);
		}

		[global::System.Diagnostics.Conditional("UNITY_ASSERTIONS")]
		public static void LogAssertion(object message)
		{
			Log(global::UnityEngine.LogType.Assert, message);
		}

		[global::System.Diagnostics.Conditional("ENABLE_UNITY_MULTIPLAYER_VERBOSE_LOGGING")]
		public static void LogVerbose(object message)
		{
			Log(message);
		}

		[global::System.Diagnostics.Conditional("ENABLE_UNITY_MULTIPLAYER_VERBOSE_LOGGING")]
		public static void LogCallVerbose(string enclosingType, [global::System.Runtime.CompilerServices.CallerMemberName] string method = "")
		{
		}

		[global::System.Diagnostics.Conditional("ENABLE_UNITY_MULTIPLAYER_VERBOSE_LOGGING")]
		public static void LogCallVerboseWithMessage(string enclosingType, string message, [global::System.Runtime.CompilerServices.CallerMemberName] string method = "")
		{
		}
	}
}
