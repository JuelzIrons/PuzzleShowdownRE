namespace UnityEngine.Rendering
{
	internal class BottleneckHistory
	{
		private global::System.Collections.Generic.List<global::UnityEngine.Rendering.PerformanceBottleneck> m_Bottlenecks = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.PerformanceBottleneck>();

		internal global::UnityEngine.Rendering.BottleneckHistogram Histogram;

		public BottleneckHistory(int initialCapacity)
		{
			m_Bottlenecks.Capacity = initialCapacity;
		}

		internal void DiscardOldSamples(int historySize)
		{
			while (m_Bottlenecks.Count >= historySize)
			{
				m_Bottlenecks.RemoveAt(0);
			}
			m_Bottlenecks.Capacity = historySize;
		}

		internal void AddBottleneckFromAveragedSample(global::UnityEngine.Rendering.FrameTimeSample frameHistorySampleAverage)
		{
			global::UnityEngine.Rendering.PerformanceBottleneck item = DetermineBottleneck(frameHistorySampleAverage);
			m_Bottlenecks.Add(item);
		}

		internal void ComputeHistogram()
		{
			global::UnityEngine.Rendering.BottleneckHistogram histogram = default(global::UnityEngine.Rendering.BottleneckHistogram);
			for (int i = 0; i < m_Bottlenecks.Count; i++)
			{
				switch (m_Bottlenecks[i])
				{
				case global::UnityEngine.Rendering.PerformanceBottleneck.Balanced:
					histogram.Balanced += 1f;
					break;
				case global::UnityEngine.Rendering.PerformanceBottleneck.CPU:
					histogram.CPU += 1f;
					break;
				case global::UnityEngine.Rendering.PerformanceBottleneck.GPU:
					histogram.GPU += 1f;
					break;
				case global::UnityEngine.Rendering.PerformanceBottleneck.PresentLimited:
					histogram.PresentLimited += 1f;
					break;
				}
			}
			histogram.Balanced /= m_Bottlenecks.Count;
			histogram.CPU /= m_Bottlenecks.Count;
			histogram.GPU /= m_Bottlenecks.Count;
			histogram.PresentLimited /= m_Bottlenecks.Count;
			Histogram = histogram;
		}

		private static global::UnityEngine.Rendering.PerformanceBottleneck DetermineBottleneck(global::UnityEngine.Rendering.FrameTimeSample s)
		{
			if (s.GPUFrameTime == 0f || s.MainThreadCPUFrameTime == 0f)
			{
				return global::UnityEngine.Rendering.PerformanceBottleneck.Indeterminate;
			}
			float num = 0.8f * s.FullFrameTime;
			if (s.GPUFrameTime > num && s.MainThreadCPUFrameTime < num && s.RenderThreadCPUFrameTime < num)
			{
				return global::UnityEngine.Rendering.PerformanceBottleneck.GPU;
			}
			if (s.GPUFrameTime < num && (s.MainThreadCPUFrameTime > num || s.RenderThreadCPUFrameTime > num))
			{
				return global::UnityEngine.Rendering.PerformanceBottleneck.CPU;
			}
			if (s.MainThreadCPUPresentWaitTime > 0.5f && s.GPUFrameTime < num && s.MainThreadCPUFrameTime < num && s.RenderThreadCPUFrameTime < num)
			{
				return global::UnityEngine.Rendering.PerformanceBottleneck.PresentLimited;
			}
			return global::UnityEngine.Rendering.PerformanceBottleneck.Balanced;
		}

		internal void Clear()
		{
			m_Bottlenecks.Clear();
			Histogram = default(global::UnityEngine.Rendering.BottleneckHistogram);
		}
	}
}
