namespace Unity.Multiplayer.Tools.NetStatsMonitor
{
	[global::System.Serializable]
	public sealed class LineGraphConfiguration
	{
		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Range(1f, 5f)]
		private float m_LineThickness = 1f;

		public float LineThickness
		{
			get
			{
				return m_LineThickness;
			}
			set
			{
				m_LineThickness = global::UnityEngine.Mathf.Clamp(value, 1f, 5f);
			}
		}

		internal int ComputeHashCode()
		{
			return LineThickness.GetHashCode();
		}
	}
}
