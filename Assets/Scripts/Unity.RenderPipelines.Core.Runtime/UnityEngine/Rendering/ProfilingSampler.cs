namespace UnityEngine.Rendering
{
	[global::Unity.Profiling.IgnoredByDeepProfiler]
	public class ProfilingSampler
	{
		private global::UnityEngine.Profiling.Recorder m_Recorder;

		private global::UnityEngine.Profiling.Recorder m_InlineRecorder;

		internal global::UnityEngine.Profiling.CustomSampler sampler { get; private set; }

		internal global::UnityEngine.Profiling.CustomSampler inlineSampler { get; private set; }

		public string name { get; private set; }

		public bool enableRecording
		{
			set
			{
				m_Recorder.enabled = value;
				m_InlineRecorder.enabled = value;
			}
		}

		public float gpuElapsedTime
		{
			get
			{
				if (!m_Recorder.enabled)
				{
					return 0f;
				}
				return (float)m_Recorder.gpuElapsedNanoseconds / 1000000f;
			}
		}

		public int gpuSampleCount
		{
			get
			{
				if (!m_Recorder.enabled)
				{
					return 0;
				}
				return m_Recorder.gpuSampleBlockCount;
			}
		}

		public float cpuElapsedTime
		{
			get
			{
				if (!m_Recorder.enabled)
				{
					return 0f;
				}
				return (float)m_Recorder.elapsedNanoseconds / 1000000f;
			}
		}

		public int cpuSampleCount
		{
			get
			{
				if (!m_Recorder.enabled)
				{
					return 0;
				}
				return m_Recorder.sampleBlockCount;
			}
		}

		public float inlineCpuElapsedTime
		{
			get
			{
				if (!m_InlineRecorder.enabled)
				{
					return 0f;
				}
				return (float)m_InlineRecorder.elapsedNanoseconds / 1000000f;
			}
		}

		public int inlineCpuSampleCount
		{
			get
			{
				if (!m_InlineRecorder.enabled)
				{
					return 0;
				}
				return m_InlineRecorder.sampleBlockCount;
			}
		}

		public static global::UnityEngine.Rendering.ProfilingSampler Get<TEnum>(TEnum marker) where TEnum : global::System.Enum
		{
			return null;
		}

		public ProfilingSampler(string name)
		{
			sampler = global::UnityEngine.Profiling.CustomSampler.Create(name, collectGpuData: true);
			inlineSampler = global::UnityEngine.Profiling.CustomSampler.Create("Inl_" + name);
			this.name = name;
			m_Recorder = sampler.GetRecorder();
			m_Recorder.enabled = false;
			m_InlineRecorder = inlineSampler.GetRecorder();
			m_InlineRecorder.enabled = false;
		}

		public void Begin(global::UnityEngine.Rendering.CommandBuffer cmd)
		{
			if (cmd != null)
			{
				if (sampler != null && sampler.isValid)
				{
					cmd.BeginSample(sampler);
				}
				else
				{
					cmd.BeginSample(name);
				}
			}
		}

		public void End(global::UnityEngine.Rendering.CommandBuffer cmd)
		{
			if (cmd != null)
			{
				if (sampler != null && sampler.isValid)
				{
					cmd.EndSample(sampler);
				}
				else
				{
					cmd.EndSample(name);
				}
			}
		}

		internal bool IsValid()
		{
			if (sampler != null)
			{
				return inlineSampler != null;
			}
			return false;
		}

		private ProfilingSampler()
		{
		}
	}
}
