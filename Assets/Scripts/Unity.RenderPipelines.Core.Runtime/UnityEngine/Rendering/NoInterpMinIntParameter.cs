namespace UnityEngine.Rendering
{
	[global::System.Serializable]
	[global::System.Diagnostics.DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	public class NoInterpMinIntParameter : global::UnityEngine.Rendering.VolumeParameter<int>
	{
		[global::System.NonSerialized]
		public int min;

		public override int value
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

		public NoInterpMinIntParameter(int value, int min, bool overrideState = false)
			: base(value, overrideState)
		{
			this.min = min;
		}
	}
}
