namespace UnityEngine.Rendering
{
	[global::System.Serializable]
	[global::System.Diagnostics.DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	public class IntParameter : global::UnityEngine.Rendering.VolumeParameter<int>
	{
		public IntParameter(int value, bool overrideState = false)
			: base(value, overrideState)
		{
		}

		public sealed override void Interp(int from, int to, float t)
		{
			m_Value = (int)((float)from + (float)(to - from) * t);
		}
	}
}
