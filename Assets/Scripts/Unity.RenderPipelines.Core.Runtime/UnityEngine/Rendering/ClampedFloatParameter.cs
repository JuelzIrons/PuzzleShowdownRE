namespace UnityEngine.Rendering
{
	[global::System.Serializable]
	[global::System.Diagnostics.DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	public class ClampedFloatParameter : global::UnityEngine.Rendering.FloatParameter
	{
		[global::System.NonSerialized]
		public float min;

		[global::System.NonSerialized]
		public float max;

		public override float value
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

		public ClampedFloatParameter(float value, float min, float max, bool overrideState = false)
			: base(value, overrideState)
		{
			this.min = min;
			this.max = max;
		}
	}
}
