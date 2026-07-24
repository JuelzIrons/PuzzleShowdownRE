namespace UnityEngine.Rendering
{
	[global::System.Serializable]
	[global::System.Diagnostics.DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	public class NoInterpVector4Parameter : global::UnityEngine.Rendering.VolumeParameter<global::UnityEngine.Vector4>
	{
		public NoInterpVector4Parameter(global::UnityEngine.Vector4 value, bool overrideState = false)
			: base(value, overrideState)
		{
		}
	}
}
