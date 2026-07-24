namespace Unity.Multiplayer.Tools.NetStatsMonitor
{
	[global::System.Serializable]
	public sealed class CounterConfiguration
	{
		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Range(1f, 7f)]
		[global::UnityEngine.Tooltip("The number of significant digits to display for this counter.")]
		private int m_SignificantDigits = 3;

		[field: global::UnityEngine.SerializeField]
		public global::Unity.Multiplayer.Tools.NetStatsMonitor.SmoothingMethod SmoothingMethod { get; set; }

		[field: global::UnityEngine.SerializeField]
		public global::Unity.Multiplayer.Tools.NetStatsMonitor.AggregationMethod AggregationMethod { get; set; }

		public int SignificantDigits
		{
			get
			{
				return m_SignificantDigits;
			}
			set
			{
				m_SignificantDigits = global::UnityEngine.Mathf.Clamp(value, 1, 7);
			}
		}

		[field: global::UnityEngine.SerializeField]
		[field: global::UnityEngine.Tooltip("Values below this threshold will be highlighted by the default styling, and can be highlighted by custom styling using the following USS classes: \"rnsm-counter-out-of-bounds\", or \"rnsm-counter-below-threshold\"")]
		public float HighlightLowerBound { get; set; } = float.NegativeInfinity;

		[field: global::UnityEngine.SerializeField]
		[field: global::UnityEngine.Tooltip("Values above this threshold will be highlighted by the default styling, and can be highlighted by custom styling using the following USS classes: \"rnsm-counter-out-of-bounds\", or \"rnsm-counter-above-threshold\"")]
		public float HighlightUpperBound { get; set; } = float.PositiveInfinity;

		[field: global::UnityEngine.SerializeField]
		public global::Unity.Multiplayer.Tools.NetStatsMonitor.ExponentialMovingAverageParams ExponentialMovingAverageParams { get; set; } = new global::Unity.Multiplayer.Tools.NetStatsMonitor.ExponentialMovingAverageParams();

		[field: global::UnityEngine.SerializeField]
		public global::Unity.Multiplayer.Tools.NetStatsMonitor.SimpleMovingAverageParams SimpleMovingAverageParams { get; set; } = new global::Unity.Multiplayer.Tools.NetStatsMonitor.SimpleMovingAverageParams();

		public int SampleCount
		{
			get
			{
				if (SmoothingMethod != global::Unity.Multiplayer.Tools.NetStatsMonitor.SmoothingMethod.SimpleMovingAverage)
				{
					return 0;
				}
				return SimpleMovingAverageParams.SampleCount;
			}
		}

		internal global::Unity.Multiplayer.Tools.NetStatsMonitor.SampleRate SampleRate
		{
			get
			{
				if (SmoothingMethod != global::Unity.Multiplayer.Tools.NetStatsMonitor.SmoothingMethod.SimpleMovingAverage)
				{
					return global::Unity.Multiplayer.Tools.NetStatsMonitor.SampleRate.PerFrame;
				}
				return SimpleMovingAverageParams.SampleRate;
			}
		}

		internal int ComputeHashCode()
		{
			return global::System.HashCode.Combine((int)SmoothingMethod, (int)AggregationMethod, SignificantDigits, HighlightLowerBound, HighlightUpperBound, ExponentialMovingAverageParams.ComputeHashCode(), SimpleMovingAverageParams.ComputeHashCode());
		}
	}
}
