namespace UnityEngine.Rendering
{
	[global::System.Serializable]
	[global::System.Diagnostics.DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	public class NoInterpFloatRangeParameter : global::UnityEngine.Rendering.VolumeParameter<global::UnityEngine.Vector2>
	{
		[global::System.NonSerialized]
		public float min;

		[global::System.NonSerialized]
		public float max;

		public override global::UnityEngine.Vector2 value
		{
			get
			{
				return m_Value;
			}
			set
			{
				m_Value.x = global::UnityEngine.Mathf.Max(value.x, min);
				m_Value.y = global::UnityEngine.Mathf.Min(value.y, max);
			}
		}

		public NoInterpFloatRangeParameter(global::UnityEngine.Vector2 value, float min, float max, bool overrideState = false)
			: base(value, overrideState)
		{
			this.min = min;
			this.max = max;
		}
	}
}
