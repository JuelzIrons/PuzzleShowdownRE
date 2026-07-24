namespace Unity.Multiplayer.Tools.NetStatsMonitor
{
	[global::System.Serializable]
	public sealed class SimpleMovingAverageParams
	{
		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Tooltip("The number of samples that are maintained for the purpose of smoothing.The value is clamped to the range [8, 512].")]
		[global::UnityEngine.Range(8f, 512f)]
		private int m_SampleCount = 64;

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

		[global::UnityEngine.Tooltip("The sample rate of the counter. If the sample rate is Per Second then each sample in the counter is collected over a full second, whereas if the sample rate is Per Frame then each sample in the counter is collected during a single frame.")]
		[field: global::UnityEngine.SerializeField]
		public global::Unity.Multiplayer.Tools.NetStatsMonitor.SampleRate SampleRate { get; set; }

		internal int ComputeHashCode()
		{
			return global::System.HashCode.Combine(SampleCount, SampleRate);
		}
	}
}
