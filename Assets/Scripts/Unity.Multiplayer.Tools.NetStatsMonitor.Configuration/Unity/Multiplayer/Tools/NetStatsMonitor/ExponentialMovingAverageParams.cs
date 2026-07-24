namespace Unity.Multiplayer.Tools.NetStatsMonitor
{
	[global::System.Serializable]
	public sealed class ExponentialMovingAverageParams
	{
		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Min(0f)]
		private double m_HalfLife = 1.0;

		public double HalfLife
		{
			get
			{
				return m_HalfLife;
			}
			set
			{
				m_HalfLife = global::System.Math.Max(value, 0.0);
			}
		}

		internal int ComputeHashCode()
		{
			return HalfLife.GetHashCode();
		}
	}
}
