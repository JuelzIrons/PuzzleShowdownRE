namespace UnityEngine.AdaptivePerformance
{
	public static class AdaptivePerformanceProfilerStats
	{
		[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Size = 1)]
		public readonly struct CustomProfilerMarker<T> where T : unmanaged
		{
			public CustomProfilerMarker(string name, global::Unity.Profiling.ProfilerMarkerDataUnit dataUnit)
			{
			}

			public void Sample(T value)
			{
			}

			private static byte GetProfilerMarkerDataType()
			{
				return global::System.Type.GetTypeCode(typeof(T)) switch
				{
					global::System.TypeCode.Int32 => 2, 
					global::System.TypeCode.UInt32 => 3, 
					global::System.TypeCode.Int64 => 4, 
					global::System.TypeCode.UInt64 => 5, 
					global::System.TypeCode.Single => 6, 
					global::System.TypeCode.Double => 7, 
					global::System.TypeCode.String => 9, 
					_ => throw new global::System.ArgumentException($"Type {typeof(T)} is unsupported by ProfilerCounter."), 
				};
			}
		}

		public struct ScalerInfo
		{
			public unsafe fixed byte scalerName[320];

			public uint enabled;

			public int overrideLevel;

			public int currentLevel;

			public int maxLevel;

			public float scale;

			public uint applied;
		}

		public static readonly global::Unity.Profiling.ProfilerCategory AdaptivePerformanceProfilerCategory = global::Unity.Profiling.ProfilerCategory.Scripts;

		public static global::UnityEngine.AdaptivePerformance.AdaptivePerformanceProfilerStats.CustomProfilerMarker<float> CurrentCPUMarker = new global::UnityEngine.AdaptivePerformance.AdaptivePerformanceProfilerStats.CustomProfilerMarker<float>("CPU frametime", global::Unity.Profiling.ProfilerMarkerDataUnit.TimeNanoseconds);

		public static global::UnityEngine.AdaptivePerformance.AdaptivePerformanceProfilerStats.CustomProfilerMarker<float> AvgCPUMarker = new global::UnityEngine.AdaptivePerformance.AdaptivePerformanceProfilerStats.CustomProfilerMarker<float>("CPU avg frametime", global::Unity.Profiling.ProfilerMarkerDataUnit.TimeNanoseconds);

		public static global::UnityEngine.AdaptivePerformance.AdaptivePerformanceProfilerStats.CustomProfilerMarker<float> CurrentGPUMarker = new global::UnityEngine.AdaptivePerformance.AdaptivePerformanceProfilerStats.CustomProfilerMarker<float>("GPU frametime", global::Unity.Profiling.ProfilerMarkerDataUnit.TimeNanoseconds);

		public static global::UnityEngine.AdaptivePerformance.AdaptivePerformanceProfilerStats.CustomProfilerMarker<float> AvgGPUMarker = new global::UnityEngine.AdaptivePerformance.AdaptivePerformanceProfilerStats.CustomProfilerMarker<float>("GPU avg frametime", global::Unity.Profiling.ProfilerMarkerDataUnit.TimeNanoseconds);

		public static global::UnityEngine.AdaptivePerformance.AdaptivePerformanceProfilerStats.CustomProfilerMarker<int> CurrentCPULevelMarker = new global::UnityEngine.AdaptivePerformance.AdaptivePerformanceProfilerStats.CustomProfilerMarker<int>("CPU performance level", global::Unity.Profiling.ProfilerMarkerDataUnit.Count);

		public static global::UnityEngine.AdaptivePerformance.AdaptivePerformanceProfilerStats.CustomProfilerMarker<int> CurrentGPULevelMarker = new global::UnityEngine.AdaptivePerformance.AdaptivePerformanceProfilerStats.CustomProfilerMarker<int>("GPU performance level", global::Unity.Profiling.ProfilerMarkerDataUnit.Count);

		public static global::UnityEngine.AdaptivePerformance.AdaptivePerformanceProfilerStats.CustomProfilerMarker<float> CurrentFrametimeMarker = new global::UnityEngine.AdaptivePerformance.AdaptivePerformanceProfilerStats.CustomProfilerMarker<float>("Frametime", global::Unity.Profiling.ProfilerMarkerDataUnit.TimeNanoseconds);

		public static global::UnityEngine.AdaptivePerformance.AdaptivePerformanceProfilerStats.CustomProfilerMarker<float> AvgFrametimeMarker = new global::UnityEngine.AdaptivePerformance.AdaptivePerformanceProfilerStats.CustomProfilerMarker<float>("Avg frametime", global::Unity.Profiling.ProfilerMarkerDataUnit.TimeNanoseconds);

		public static global::UnityEngine.AdaptivePerformance.AdaptivePerformanceProfilerStats.CustomProfilerMarker<int> WarningLevelMarker = new global::UnityEngine.AdaptivePerformance.AdaptivePerformanceProfilerStats.CustomProfilerMarker<int>("Thermal Warning Level", global::Unity.Profiling.ProfilerMarkerDataUnit.Count);

		public static global::UnityEngine.AdaptivePerformance.AdaptivePerformanceProfilerStats.CustomProfilerMarker<float> TemperatureLevelMarker = new global::UnityEngine.AdaptivePerformance.AdaptivePerformanceProfilerStats.CustomProfilerMarker<float>("Temperature Level", global::Unity.Profiling.ProfilerMarkerDataUnit.Count);

		public static global::UnityEngine.AdaptivePerformance.AdaptivePerformanceProfilerStats.CustomProfilerMarker<float> TemperatureTrendMarker = new global::UnityEngine.AdaptivePerformance.AdaptivePerformanceProfilerStats.CustomProfilerMarker<float>("Temperature Trend", global::Unity.Profiling.ProfilerMarkerDataUnit.Count);

		public static global::UnityEngine.AdaptivePerformance.AdaptivePerformanceProfilerStats.CustomProfilerMarker<int> BottleneckMarker = new global::UnityEngine.AdaptivePerformance.AdaptivePerformanceProfilerStats.CustomProfilerMarker<int>("Bottleneck", global::Unity.Profiling.ProfilerMarkerDataUnit.Count);

		public static global::UnityEngine.AdaptivePerformance.AdaptivePerformanceProfilerStats.CustomProfilerMarker<int> PerformanceModeMarker = new global::UnityEngine.AdaptivePerformance.AdaptivePerformanceProfilerStats.CustomProfilerMarker<int>("Performance Mode", global::Unity.Profiling.ProfilerMarkerDataUnit.Count);

		public static readonly global::System.Guid kAdaptivePerformanceProfilerModuleGuid = new global::System.Guid("42c5aeb7-fb77-4172-a384-34063f1bd332");

		public static readonly int kScalerDataTag = 0;

		private static global::System.Collections.Generic.Dictionary<string, global::UnityEngine.AdaptivePerformance.AdaptivePerformanceProfilerStats.ScalerInfo> scalerInfos = new global::System.Collections.Generic.Dictionary<string, global::UnityEngine.AdaptivePerformance.AdaptivePerformanceProfilerStats.ScalerInfo>();

		[global::System.Diagnostics.Conditional("ENABLE_PROFILER")]
		public unsafe static void EmitScalerDataToProfilerStream(string scalerName, bool enabled, int overrideLevel, int currentLevel, float scale, bool applied, int maxLevel)
		{
			if (global::UnityEngine.Profiling.Profiler.enabled && scalerName.Length != 0)
			{
				global::UnityEngine.AdaptivePerformance.AdaptivePerformanceProfilerStats.ScalerInfo value;
				bool flag = scalerInfos.TryGetValue(scalerName, out value);
				if (!flag)
				{
					value = default(global::UnityEngine.AdaptivePerformance.AdaptivePerformanceProfilerStats.ScalerInfo);
				}
				value.enabled = (enabled ? 1u : 0u);
				value.overrideLevel = overrideLevel;
				value.currentLevel = currentLevel;
				value.scale = scale;
				value.maxLevel = maxLevel;
				value.applied = (applied ? 1u : 0u);
				global::System.Text.Encoding.ASCII.GetBytes(global::System.MemoryExtensions.AsSpan(scalerName), new global::System.Span<byte>(value.scalerName, 320));
				if (!flag)
				{
					scalerInfos.Add(scalerName, value);
				}
				else
				{
					scalerInfos[scalerName] = value;
				}
			}
		}

		public static void FlushScalerDataToProfilerStream()
		{
			global::UnityEngine.AdaptivePerformance.AdaptivePerformanceProfilerStats.ScalerInfo[] array = new global::UnityEngine.AdaptivePerformance.AdaptivePerformanceProfilerStats.ScalerInfo[scalerInfos.Count];
			scalerInfos.Values.CopyTo(array, 0);
		}
	}
}
