namespace Unity.Multiplayer.Tools.NetworkProfiler.Runtime
{
	internal class EventCounterFactory : global::Unity.Multiplayer.Tools.NetworkProfiler.Runtime.ICounterFactory
	{
		public global::Unity.Multiplayer.Tools.NetworkProfiler.Runtime.ICounter Construct(string name)
		{
			return new global::Unity.Multiplayer.Tools.NetworkProfiler.Runtime.CounterWrapper(new global::Unity.Profiling.ProfilerCounter<long>(global::Unity.Profiling.ProfilerCategory.Network, name, global::Unity.Profiling.ProfilerMarkerDataUnit.Count));
		}
	}
}
