namespace Unity.Profiling
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Size = 1)]
	public readonly struct ProfilerCounterValue<T> where T : unmanaged
	{
		public T Value
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return default(T);
			}
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			set
			{
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public ProfilerCounterValue(string name)
		{
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public ProfilerCounterValue(string name, global::Unity.Profiling.ProfilerMarkerDataUnit dataUnit)
		{
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public ProfilerCounterValue(string name, global::Unity.Profiling.ProfilerMarkerDataUnit dataUnit, global::Unity.Profiling.ProfilerCounterOptions counterOptions)
		{
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public ProfilerCounterValue(global::Unity.Profiling.ProfilerCategory category, string name, global::Unity.Profiling.ProfilerMarkerDataUnit dataUnit)
		{
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public ProfilerCounterValue(global::Unity.Profiling.ProfilerCategory category, string name, global::Unity.Profiling.ProfilerMarkerDataUnit dataUnit, global::Unity.Profiling.ProfilerCounterOptions counterOptions)
		{
		}

		[global::System.Diagnostics.Conditional("ENABLE_PROFILER")]
		public void Sample()
		{
		}
	}
}
