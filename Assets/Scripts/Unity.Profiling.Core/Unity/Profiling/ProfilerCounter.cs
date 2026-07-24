namespace Unity.Profiling
{
	public readonly struct ProfilerCounter<T> where T : unmanaged
	{
		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public ProfilerCounter(global::Unity.Profiling.ProfilerCategory category, string name, global::Unity.Profiling.ProfilerMarkerDataUnit dataUnit)
		{
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::System.Diagnostics.Conditional("ENABLE_PROFILER")]
		public void Sample(T value)
		{
		}
	}
}
