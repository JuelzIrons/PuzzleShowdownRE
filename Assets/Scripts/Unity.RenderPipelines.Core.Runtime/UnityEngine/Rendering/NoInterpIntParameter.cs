namespace UnityEngine.Rendering
{
	[global::System.Serializable]
	[global::System.Diagnostics.DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	public class NoInterpIntParameter : global::UnityEngine.Rendering.VolumeParameter<int>
	{
		public NoInterpIntParameter(int value, bool overrideState = false)
			: base(value, overrideState)
		{
		}
	}
}
