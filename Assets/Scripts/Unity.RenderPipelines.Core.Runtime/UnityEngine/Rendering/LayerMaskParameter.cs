namespace UnityEngine.Rendering
{
	[global::System.Serializable]
	[global::System.Diagnostics.DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	public class LayerMaskParameter : global::UnityEngine.Rendering.VolumeParameter<global::UnityEngine.LayerMask>
	{
		public LayerMaskParameter(global::UnityEngine.LayerMask value, bool overrideState = false)
			: base(value, overrideState)
		{
		}
	}
}
