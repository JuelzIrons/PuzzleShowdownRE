namespace Unity.Profiling
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Size = 1)]
	public readonly struct ProfilerMarker<TP1> where TP1 : unmanaged
	{
		[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Size = 1)]
		public readonly struct AutoScope : global::System.IDisposable
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			internal AutoScope(global::Unity.Profiling.ProfilerMarker<TP1> marker, TP1 p1)
			{
			}

			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public void Dispose()
			{
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public ProfilerMarker(string name, string param1Name)
		{
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public ProfilerMarker(global::Unity.Profiling.ProfilerCategory category, string name, string param1Name)
		{
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::System.Diagnostics.Conditional("ENABLE_PROFILER")]
		public void Begin(TP1 p1)
		{
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::System.Diagnostics.Conditional("ENABLE_PROFILER")]
		public void End()
		{
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public global::Unity.Profiling.ProfilerMarker<TP1>.AutoScope Auto(TP1 p1)
		{
			return default(global::Unity.Profiling.ProfilerMarker<TP1>.AutoScope);
		}
	}
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Size = 1)]
	public readonly struct ProfilerMarker<TP1, TP2> where TP1 : unmanaged where TP2 : unmanaged
	{
		[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Size = 1)]
		public readonly struct AutoScope : global::System.IDisposable
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			internal AutoScope(global::Unity.Profiling.ProfilerMarker<TP1, TP2> marker, TP1 p1, TP2 p2)
			{
			}

			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public void Dispose()
			{
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public ProfilerMarker(string name, string param1Name, string param2Name)
		{
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public ProfilerMarker(global::Unity.Profiling.ProfilerCategory category, string name, string param1Name, string param2Name)
		{
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::System.Diagnostics.Conditional("ENABLE_PROFILER")]
		public void Begin(TP1 p1, TP2 p2)
		{
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::System.Diagnostics.Conditional("ENABLE_PROFILER")]
		public void End()
		{
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public global::Unity.Profiling.ProfilerMarker<TP1, TP2>.AutoScope Auto(TP1 p1, TP2 p2)
		{
			return default(global::Unity.Profiling.ProfilerMarker<TP1, TP2>.AutoScope);
		}
	}
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Size = 1)]
	public readonly struct ProfilerMarker<TP1, TP2, TP3> where TP1 : unmanaged where TP2 : unmanaged where TP3 : unmanaged
	{
		[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Size = 1)]
		public readonly struct AutoScope : global::System.IDisposable
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			internal AutoScope(global::Unity.Profiling.ProfilerMarker<TP1, TP2, TP3> marker, TP1 p1, TP2 p2, TP3 p3)
			{
			}

			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public void Dispose()
			{
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public ProfilerMarker(string name, string param1Name, string param2Name, string param3Name)
		{
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public ProfilerMarker(global::Unity.Profiling.ProfilerCategory category, string name, string param1Name, string param2Name, string param3Name)
		{
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::System.Diagnostics.Conditional("ENABLE_PROFILER")]
		public void Begin(TP1 p1, TP2 p2, TP3 p3)
		{
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::System.Diagnostics.Conditional("ENABLE_PROFILER")]
		public void End()
		{
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public global::Unity.Profiling.ProfilerMarker<TP1, TP2, TP3>.AutoScope Auto(TP1 p1, TP2 p2, TP3 p3)
		{
			return default(global::Unity.Profiling.ProfilerMarker<TP1, TP2, TP3>.AutoScope);
		}
	}
}
