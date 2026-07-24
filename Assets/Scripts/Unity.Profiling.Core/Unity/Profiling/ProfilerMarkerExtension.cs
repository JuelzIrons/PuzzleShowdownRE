namespace Unity.Profiling
{
	public static class ProfilerMarkerExtension
	{
		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::System.Diagnostics.Conditional("ENABLE_PROFILER")]
		public unsafe static void Begin(this global::Unity.Profiling.ProfilerMarker marker, int metadata)
		{
			global::Unity.Profiling.LowLevel.Unsafe.ProfilerMarkerData profilerMarkerData = new global::Unity.Profiling.LowLevel.Unsafe.ProfilerMarkerData
			{
				Type = 2,
				Size = (uint)global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<int>(),
				Ptr = &metadata
			};
			global::Unity.Profiling.LowLevel.Unsafe.ProfilerUnsafeUtility.BeginSampleWithMetadata(marker.Handle, 1, &profilerMarkerData);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::System.Diagnostics.Conditional("ENABLE_PROFILER")]
		public unsafe static void Begin(this global::Unity.Profiling.ProfilerMarker marker, uint metadata)
		{
			global::Unity.Profiling.LowLevel.Unsafe.ProfilerMarkerData profilerMarkerData = new global::Unity.Profiling.LowLevel.Unsafe.ProfilerMarkerData
			{
				Type = 3,
				Size = (uint)global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<uint>(),
				Ptr = &metadata
			};
			global::Unity.Profiling.LowLevel.Unsafe.ProfilerUnsafeUtility.BeginSampleWithMetadata(marker.Handle, 1, &profilerMarkerData);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::System.Diagnostics.Conditional("ENABLE_PROFILER")]
		public unsafe static void Begin(this global::Unity.Profiling.ProfilerMarker marker, long metadata)
		{
			global::Unity.Profiling.LowLevel.Unsafe.ProfilerMarkerData profilerMarkerData = new global::Unity.Profiling.LowLevel.Unsafe.ProfilerMarkerData
			{
				Type = 4,
				Size = (uint)global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<long>(),
				Ptr = &metadata
			};
			global::Unity.Profiling.LowLevel.Unsafe.ProfilerUnsafeUtility.BeginSampleWithMetadata(marker.Handle, 1, &profilerMarkerData);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::System.Diagnostics.Conditional("ENABLE_PROFILER")]
		public unsafe static void Begin(this global::Unity.Profiling.ProfilerMarker marker, ulong metadata)
		{
			global::Unity.Profiling.LowLevel.Unsafe.ProfilerMarkerData profilerMarkerData = new global::Unity.Profiling.LowLevel.Unsafe.ProfilerMarkerData
			{
				Type = 5,
				Size = (uint)global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<ulong>(),
				Ptr = &metadata
			};
			global::Unity.Profiling.LowLevel.Unsafe.ProfilerUnsafeUtility.BeginSampleWithMetadata(marker.Handle, 1, &profilerMarkerData);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::System.Diagnostics.Conditional("ENABLE_PROFILER")]
		public unsafe static void Begin(this global::Unity.Profiling.ProfilerMarker marker, float metadata)
		{
			global::Unity.Profiling.LowLevel.Unsafe.ProfilerMarkerData profilerMarkerData = new global::Unity.Profiling.LowLevel.Unsafe.ProfilerMarkerData
			{
				Type = 6,
				Size = (uint)global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<float>(),
				Ptr = &metadata
			};
			global::Unity.Profiling.LowLevel.Unsafe.ProfilerUnsafeUtility.BeginSampleWithMetadata(marker.Handle, 1, &profilerMarkerData);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::System.Diagnostics.Conditional("ENABLE_PROFILER")]
		public unsafe static void Begin(this global::Unity.Profiling.ProfilerMarker marker, double metadata)
		{
			global::Unity.Profiling.LowLevel.Unsafe.ProfilerMarkerData profilerMarkerData = new global::Unity.Profiling.LowLevel.Unsafe.ProfilerMarkerData
			{
				Type = 7,
				Size = (uint)global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<double>(),
				Ptr = &metadata
			};
			global::Unity.Profiling.LowLevel.Unsafe.ProfilerUnsafeUtility.BeginSampleWithMetadata(marker.Handle, 1, &profilerMarkerData);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::System.Diagnostics.Conditional("ENABLE_PROFILER")]
		public unsafe static void Begin(this global::Unity.Profiling.ProfilerMarker marker, string metadata)
		{
			global::Unity.Profiling.LowLevel.Unsafe.ProfilerMarkerData profilerMarkerData = new global::Unity.Profiling.LowLevel.Unsafe.ProfilerMarkerData
			{
				Type = 9
			};
			fixed (char* ptr = metadata)
			{
				profilerMarkerData.Size = (uint)((metadata.Length + 1) * 2);
				profilerMarkerData.Ptr = ptr;
				global::Unity.Profiling.LowLevel.Unsafe.ProfilerUnsafeUtility.BeginSampleWithMetadata(marker.Handle, 1, &profilerMarkerData);
			}
		}
	}
}
