namespace UnityEngine.Rendering
{
	[global::System.Serializable]
	[global::System.Diagnostics.DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	public class NoInterpMaxIntParameter : global::UnityEngine.Rendering.VolumeParameter<int>
	{
		[global::System.NonSerialized]
		public int max;

		public override int value
		{
			get
			{
				return m_Value;
			}
			set
			{
				m_Value = global::UnityEngine.Mathf.Min(value, max);
			}
		}

		public NoInterpMaxIntParameter(int value, int max, bool overrideState = false)
			: base(value, overrideState)
		{
			this.max = max;
		}
	}
}
