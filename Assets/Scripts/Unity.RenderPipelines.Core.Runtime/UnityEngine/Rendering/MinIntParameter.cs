namespace UnityEngine.Rendering
{
	[global::System.Serializable]
	[global::System.Diagnostics.DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	public class MinIntParameter : global::UnityEngine.Rendering.IntParameter
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

		public MinIntParameter(int value, int min, bool overrideState = false)
			: base(value, overrideState)
		{
			this.min = min;
		}
	}
}
