namespace UnityEngine.Rendering
{
	[global::System.Serializable]
	[global::System.Diagnostics.DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	public class NoInterpVector3Parameter : global::UnityEngine.Rendering.VolumeParameter<global::UnityEngine.Vector3>
	{
		public NoInterpVector3Parameter(global::UnityEngine.Vector3 value, bool overrideState = false)
			: base(value, overrideState)
		{
		}
	}
}
