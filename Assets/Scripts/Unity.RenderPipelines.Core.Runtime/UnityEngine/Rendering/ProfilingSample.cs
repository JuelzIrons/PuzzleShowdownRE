namespace UnityEngine.Rendering
{
	[global::System.Obsolete("Please use ProfilingScope. #from(2021.1)")]
	[global::Unity.Profiling.IgnoredByDeepProfiler]
	public struct ProfilingSample : global::System.IDisposable
	{
		private readonly global::UnityEngine.Rendering.CommandBuffer m_Cmd;

		private readonly string m_Name;

		private bool m_Disposed;

		private global::UnityEngine.Profiling.CustomSampler m_Sampler;

		public ProfilingSample(global::UnityEngine.Rendering.CommandBuffer cmd, string name, global::UnityEngine.Profiling.CustomSampler sampler = null)
		{
			m_Cmd = cmd;
			m_Name = name;
			m_Disposed = false;
			if (cmd != null && name != "")
			{
				cmd.BeginSample(name);
			}
			m_Sampler = sampler;
		}

		public ProfilingSample(global::UnityEngine.Rendering.CommandBuffer cmd, string format, object arg)
			: this(cmd, string.Format(format, arg))
		{
		}

		public ProfilingSample(global::UnityEngine.Rendering.CommandBuffer cmd, string format, params object[] args)
			: this(cmd, string.Format(format, args))
		{
		}

		public void Dispose()
		{
			Dispose(disposing: true);
		}

		private void Dispose(bool disposing)
		{
			if (!m_Disposed)
			{
				if (disposing && m_Cmd != null && m_Name != "")
				{
					m_Cmd.EndSample(m_Name);
				}
				m_Disposed = true;
			}
		}
	}
}
