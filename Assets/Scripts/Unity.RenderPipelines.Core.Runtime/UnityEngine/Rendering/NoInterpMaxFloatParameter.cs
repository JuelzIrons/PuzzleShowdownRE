namespace UnityEngine.Rendering
{
	[global::System.Serializable]
	[global::System.Diagnostics.DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	public class NoInterpMaxFloatParameter : global::UnityEngine.Rendering.VolumeParameter<float>
	{
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
				m_Value = global::UnityEngine.Mathf.Min(value, max);
			}
		}

		public NoInterpMaxFloatParameter(float value, float max, bool overrideState = false)
			: base(value, overrideState)
		{
			this.max = max;
		}
	}
}
