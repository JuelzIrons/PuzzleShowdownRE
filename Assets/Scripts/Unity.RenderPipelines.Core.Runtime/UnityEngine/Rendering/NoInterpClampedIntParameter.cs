namespace UnityEngine.Rendering
{
	[global::System.Serializable]
	[global::System.Diagnostics.DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	public class NoInterpClampedIntParameter : global::UnityEngine.Rendering.VolumeParameter<int>
	{
		[global::System.NonSerialized]
		public int min;

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
				m_Value = global::UnityEngine.Mathf.Clamp(value, min, max);
			}
		}

		public NoInterpClampedIntParameter(int value, int min, int max, bool overrideState = false)
			: base(value, overrideState)
		{
			this.min = min;
			this.max = max;
		}
	}
}
