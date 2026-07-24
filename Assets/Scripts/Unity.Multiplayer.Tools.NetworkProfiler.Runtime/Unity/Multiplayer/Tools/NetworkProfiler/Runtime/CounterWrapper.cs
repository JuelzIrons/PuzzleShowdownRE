namespace Unity.Multiplayer.Tools.NetworkProfiler.Runtime
{
	internal class CounterWrapper : global::Unity.Multiplayer.Tools.NetworkProfiler.Runtime.ICounter
	{
		private global::Unity.Profiling.ProfilerCounter<long> m_Counter;

		public CounterWrapper(global::Unity.Profiling.ProfilerCounter<long> counter)
		{
			m_Counter = counter;
		}

		public void Sample(long inValue)
		{
		}
	}
}
