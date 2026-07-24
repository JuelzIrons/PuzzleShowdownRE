namespace UnityEngine.AdaptivePerformance
{
	internal class CpuTimeProvider
	{
		private global::UnityEngine.FrameTiming[] m_FrameTimings = new global::UnityEngine.FrameTiming[1];

		public float CpuFrameTime
		{
			get
			{
				if (GetLatestTimings() >= 1)
				{
					double num = m_FrameTimings[0].cpuMainThreadFrameTime + m_FrameTimings[0].cpuRenderThreadFrameTime;
					if (num > 0.0)
					{
						return (float)(num * 0.001);
					}
				}
				return -1f;
			}
		}

		protected virtual uint GetLatestTimings()
		{
			return global::UnityEngine.FrameTimingManager.GetLatestTimings(1u, m_FrameTimings);
		}

		public void Measure()
		{
			global::UnityEngine.FrameTimingManager.CaptureFrameTimings();
		}
	}
}
