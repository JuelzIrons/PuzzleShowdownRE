namespace UnityEngine.Rendering
{
	[global::System.Serializable]
	[global::System.Diagnostics.DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	public class NoInterpFloatParameter : global::UnityEngine.Rendering.VolumeParameter<float>
	{
		public NoInterpFloatParameter(float value, bool overrideState = false)
			: base(value, overrideState)
		{
		}
	}
}
