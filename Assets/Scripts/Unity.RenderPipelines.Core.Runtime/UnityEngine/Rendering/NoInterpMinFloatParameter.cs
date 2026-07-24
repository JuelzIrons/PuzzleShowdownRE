namespace UnityEngine.Rendering
{
	[global::System.Serializable]
	[global::System.Diagnostics.DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	public class NoInterpMinFloatParameter : global::UnityEngine.Rendering.VolumeParameter<float>
	{
		[global::System.NonSerialized]
		public float min;

		public override float value
		{
			get
			{
				return m_Value;
			}
			set
			{
				m_Value = global::UnityEngine.Mathf.Max(value, min);
			}
		}

		public NoInterpMinFloatParameter(float value, float min, bool overrideState = false)
			: base(value, overrideState)
		{
			this.min = min;
		}
	}
}
