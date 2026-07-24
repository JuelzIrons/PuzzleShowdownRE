namespace Unity.Multiplayer.Tools.NetStatsMonitor
{
	[global::System.Serializable]
	public sealed class GraphConfiguration
	{
		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Tooltip("The number of samples that are maintained for the purpose of graphing. The value is clamped to the range [8, 512].")]
		[global::UnityEngine.Range(8f, 512f)]
		private int m_SampleCount = 256;

		public int SampleCount
		{
			get
			{
				return m_SampleCount;
			}
			set
			{
				m_SampleCount = global::UnityEngine.Mathf.Clamp(value, 8, 512);
			}
		}

		[global::UnityEngine.Tooltip("The sample rate of the graph. If the sample rate is Per Second then each point in the graph corresponds to data collected over a full second, whereas if the sample rate is Per Frame then each point in the graph corresponds to data collected within a single frame.")]
		[field: global::UnityEngine.SerializeField]
		public global::Unity.Multiplayer.Tools.NetStatsMonitor.SampleRate SampleRate { get; set; } = global::Unity.Multiplayer.Tools.NetStatsMonitor.SampleRate.PerSecond;

		[field: global::UnityEngine.SerializeField]
		public global::System.Collections.Generic.List<global::UnityEngine.Color> VariableColors { get; set; } = new global::System.Collections.Generic.List<global::UnityEngine.Color>();

		[field: global::UnityEngine.SerializeField]
		public global::Unity.Multiplayer.Tools.NetStatsMonitor.GraphXAxisType XAxisType { get; set; }

		[field: global::UnityEngine.SerializeField]
		public global::Unity.Multiplayer.Tools.NetStatsMonitor.LineGraphConfiguration LineGraphConfiguration { get; set; } = new global::Unity.Multiplayer.Tools.NetStatsMonitor.LineGraphConfiguration();

		internal int ComputeHashCode()
		{
			int num = global::System.HashCode.Combine(SampleCount, (int)SampleRate, (int)XAxisType, LineGraphConfiguration.ComputeHashCode());
			if (VariableColors != null)
			{
				foreach (global::UnityEngine.Color variableColor in VariableColors)
				{
					num = global::System.HashCode.Combine(num, variableColor);
				}
			}
			return num;
		}
	}
}
