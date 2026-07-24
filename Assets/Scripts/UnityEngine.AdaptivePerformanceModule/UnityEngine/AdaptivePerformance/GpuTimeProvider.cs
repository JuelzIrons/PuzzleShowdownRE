namespace UnityEngine.AdaptivePerformance
{
	internal class GpuTimeProvider
	{
		private global::UnityEngine.FrameTiming[] m_FrameTiming = new global::UnityEngine.FrameTiming[1];

		public float GpuFrameTime
		{
			get
			{
				if (GetLatestTimings() >= 1)
				{
					double gpuFrameTime = m_FrameTiming[0].gpuFrameTime;
					if (gpuFrameTime > 0.0)
					{
						return (float)(gpuFrameTime * 0.001);
					}
				}
				return -1f;
			}
		}

		protected virtual uint GetLatestTimings()
		{
			return global::UnityEngine.FrameTimingManager.GetLatestTimings(1u, m_FrameTiming);
		}

		public void Measure()
		{
			global::UnityEngine.FrameTimingManager.CaptureFrameTimings();
		}
	}
}
