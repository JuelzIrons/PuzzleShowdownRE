namespace Unity.Multiplayer.Tools.NetVis.Configuration
{
	internal class BandwidthStats : global::Unity.Multiplayer.Tools.NetVis.Configuration.IReadonlyBandwidthStats
	{
		public const int k_MinBandwidth = 0;

		public const int k_MinimumMaxValue = 1;

		private float m_MaxBandwidth = 1f;

		public float MinBandwidth => 0f;

		public float MaxBandwidth
		{
			get
			{
				return global::System.Math.Max(m_MaxBandwidth, 1f);
			}
			set
			{
				bool num = m_MaxBandwidth != value;
				m_MaxBandwidth = value;
				if (num)
				{
					this.OnBandwidthStatsUpdated?.Invoke();
				}
			}
		}

		public event global::System.Action OnBandwidthStatsUpdated;
	}
}
