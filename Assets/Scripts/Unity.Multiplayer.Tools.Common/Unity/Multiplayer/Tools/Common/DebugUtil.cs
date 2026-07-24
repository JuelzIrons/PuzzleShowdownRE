namespace Unity.Multiplayer.Tools.Common
{
	internal static class DebugUtil
	{
		[global::System.Diagnostics.Conditional("UNITY_MP_TOOLS_DEBUG_TRACE")]
		public static void Trace(string message)
		{
			global::UnityEngine.Debug.Log(message);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
		[global::System.Diagnostics.Conditional("UNITY_MP_TOOLS_DEBUG_TRACE")]
		public static void TraceMethodName([global::System.Runtime.CompilerServices.CallerFilePath] string filepath = "", [global::System.Runtime.CompilerServices.CallerMemberName] string methodName = "")
		{
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
		[global::System.Diagnostics.Conditional("UNITY_MP_TOOLS_DEBUG_TRACE")]
		public static void TraceMethodNameUsingStackFrame()
		{
		}
	}
}
