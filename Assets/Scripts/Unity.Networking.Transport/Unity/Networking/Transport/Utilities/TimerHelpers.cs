namespace Unity.Networking.Transport.Utilities
{
	internal static class TimerHelpers
	{
		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		internal static ulong GetTicks()
		{
			return global::Unity.Baselib.LowLevel.Binding.Baselib_Timer_GetHighPrecisionTimerTicks();
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		internal static long GetCurrentTimestampMS()
		{
			return (long)(global::Unity.Baselib.LowLevel.Binding.Baselib_Timer_GetTimeSinceStartupInSeconds() * 1000.0);
		}

		internal static void Sleep(uint ms)
		{
			global::Unity.Baselib.LowLevel.Binding.Baselib_Timer_WaitForAtLeast(ms);
		}
	}
}
