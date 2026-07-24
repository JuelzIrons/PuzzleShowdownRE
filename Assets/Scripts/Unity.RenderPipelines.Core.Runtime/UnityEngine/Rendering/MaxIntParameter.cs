namespace UnityEngine.Rendering
{
	[global::System.Serializable]
	[global::System.Diagnostics.DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	public class MaxIntParameter : global::UnityEngine.Rendering.IntParameter
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

		public MaxIntParameter(int value, int max, bool overrideState = false)
			: base(value, overrideState)
		{
			this.max = max;
		}
	}
}
