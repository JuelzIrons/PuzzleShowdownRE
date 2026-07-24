namespace Unity.Multiplayer.Tools.NetStatsMonitor
{
	[global::System.Serializable]
	public class PositionConfiguration
	{
		[global::UnityEngine.Tooltip("The position of the Net Stats Monitor from left to right in the range from 0 to 1. 0 is flush left, 0.5 is centered, and 1 is flush right.")]
		[global::UnityEngine.Range(0f, 1f)]
		[global::UnityEngine.SerializeField]
		private float m_PositionLeftToRight;

		[global::UnityEngine.Tooltip("The position of the Net Stats Monitor from top to bottom in the range from 0 to 1. 0 is flush to the top, 0.5 is centered, and 1 is flush to the bottom.")]
		[global::UnityEngine.Range(0f, 1f)]
		[global::UnityEngine.SerializeField]
		private float m_PositionTopToBottom;

		[field: global::UnityEngine.Tooltip("If enabled, the position here will override the position set by the USS styling. Disable this options if you would like to use the position from the USS styling instead.")]
		[field: global::UnityEngine.SerializeField]
		public bool OverridePosition { get; set; } = true;

		public float PositionLeftToRight
		{
			get
			{
				return m_PositionLeftToRight;
			}
			set
			{
				m_PositionLeftToRight = global::UnityEngine.Mathf.Clamp(value, 0f, 1f);
			}
		}

		public float PositionTopToBottom
		{
			get
			{
				return m_PositionTopToBottom;
			}
			set
			{
				m_PositionTopToBottom = global::UnityEngine.Mathf.Clamp(value, 0f, 1f);
			}
		}
	}
}
