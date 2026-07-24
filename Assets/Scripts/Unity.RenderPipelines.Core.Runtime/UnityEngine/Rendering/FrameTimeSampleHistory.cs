namespace UnityEngine.Rendering
{
	internal class FrameTimeSampleHistory
	{
		private global::System.Collections.Generic.List<global::UnityEngine.Rendering.FrameTimeSample> m_Samples = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.FrameTimeSample>();

		internal global::UnityEngine.Rendering.FrameTimeSample SampleAverage;

		internal global::UnityEngine.Rendering.FrameTimeSample SampleMin;

		internal global::UnityEngine.Rendering.FrameTimeSample SampleMax;

		private static global::System.Func<float, float, float> s_SampleValueAdd = (float value, float other) => value + other;

		private static global::System.Func<float, float, float> s_SampleValueMin = (float value, float other) => (!(other > 0f)) ? value : global::UnityEngine.Mathf.Min(value, other);

		private static global::System.Func<float, float, float> s_SampleValueMax = (float value, float other) => global::UnityEngine.Mathf.Max(value, other);

		private static global::System.Func<float, float, float> s_SampleValueCountValid = (float value, float other) => (!(other > 0f)) ? value : (value + 1f);

		private static global::System.Func<float, float, float> s_SampleValueEnsureValid = (float value, float other) => (!(other > 0f)) ? 0f : value;

		private static global::System.Func<float, float, float> s_SampleValueDivide = (float value, float other) => (!(other > 0f)) ? 0f : (value / other);

		public FrameTimeSampleHistory(int initialCapacity)
		{
			m_Samples.Capacity = initialCapacity;
		}

		internal void Add(global::UnityEngine.Rendering.FrameTimeSample sample)
		{
			m_Samples.Add(sample);
		}

		internal void ComputeAggregateValues()
		{
			global::UnityEngine.Rendering.FrameTimeSample aggregate = default(global::UnityEngine.Rendering.FrameTimeSample);
			global::UnityEngine.Rendering.FrameTimeSample aggregate2 = new global::UnityEngine.Rendering.FrameTimeSample(float.MaxValue);
			global::UnityEngine.Rendering.FrameTimeSample aggregate3 = new global::UnityEngine.Rendering.FrameTimeSample(float.MinValue);
			global::UnityEngine.Rendering.FrameTimeSample aggregate4 = default(global::UnityEngine.Rendering.FrameTimeSample);
			for (int i = 0; i < m_Samples.Count; i++)
			{
				global::UnityEngine.Rendering.FrameTimeSample sample = m_Samples[i];
				ForEachSampleMember(ref aggregate2, sample, s_SampleValueMin);
				ForEachSampleMember(ref aggregate3, sample, s_SampleValueMax);
				ForEachSampleMember(ref aggregate, sample, s_SampleValueAdd);
				ForEachSampleMember(ref aggregate4, sample, s_SampleValueCountValid);
			}
			ForEachSampleMember(ref aggregate2, aggregate4, s_SampleValueEnsureValid);
			ForEachSampleMember(ref aggregate3, aggregate4, s_SampleValueEnsureValid);
			ForEachSampleMember(ref aggregate, aggregate4, s_SampleValueDivide);
			SampleAverage = aggregate;
			SampleMin = aggregate2;
			SampleMax = aggregate3;
			static void ForEachSampleMember(ref global::UnityEngine.Rendering.FrameTimeSample reference, global::UnityEngine.Rendering.FrameTimeSample frameTimeSample, global::System.Func<float, float, float> func)
			{
				reference.FramesPerSecond = func(reference.FramesPerSecond, frameTimeSample.FramesPerSecond);
				reference.FullFrameTime = func(reference.FullFrameTime, frameTimeSample.FullFrameTime);
				reference.MainThreadCPUFrameTime = func(reference.MainThreadCPUFrameTime, frameTimeSample.MainThreadCPUFrameTime);
				reference.MainThreadCPUPresentWaitTime = func(reference.MainThreadCPUPresentWaitTime, frameTimeSample.MainThreadCPUPresentWaitTime);
				reference.RenderThreadCPUFrameTime = func(reference.RenderThreadCPUFrameTime, frameTimeSample.RenderThreadCPUFrameTime);
				reference.GPUFrameTime = func(reference.GPUFrameTime, frameTimeSample.GPUFrameTime);
			}
		}

		internal void DiscardOldSamples(int sampleHistorySize)
		{
			while (m_Samples.Count >= sampleHistorySize)
			{
				m_Samples.RemoveAt(0);
			}
			m_Samples.Capacity = sampleHistorySize;
		}

		internal void Clear()
		{
			m_Samples.Clear();
		}
	}
}
