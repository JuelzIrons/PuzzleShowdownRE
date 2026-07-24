namespace Unity.Multiplayer.Tools.NetStats
{
	[global::System.Serializable]
	internal class Timer : global::Unity.Multiplayer.Tools.NetStats.Metric<global::System.TimeSpan>
	{
		public readonly struct TimerScope : global::System.IDisposable
		{
			private readonly global::System.Action<global::System.TimeSpan> m_Callback;

			private readonly global::System.Diagnostics.Stopwatch m_Stopwatch;

			internal TimerScope(global::System.Action<global::System.TimeSpan> callback)
			{
				m_Callback = callback;
				m_Stopwatch = new global::System.Diagnostics.Stopwatch();
				m_Stopwatch.Start();
			}

			public void Dispose()
			{
				m_Callback?.Invoke(m_Stopwatch.Elapsed);
			}
		}

		public override global::Unity.Multiplayer.Tools.NetStats.MetricContainerType MetricContainerType => global::Unity.Multiplayer.Tools.NetStats.MetricContainerType.Timer;

		public Timer(global::Unity.Multiplayer.Tools.NetStats.MetricId metricId, global::System.TimeSpan defaultValue = default(global::System.TimeSpan))
			: base(metricId, defaultValue)
		{
		}

		public void Set(global::System.TimeSpan value)
		{
			base.Value = value;
		}

		public global::Unity.Multiplayer.Tools.NetStats.Timer.TimerScope Time()
		{
			return new global::Unity.Multiplayer.Tools.NetStats.Timer.TimerScope(Set);
		}
	}
}
