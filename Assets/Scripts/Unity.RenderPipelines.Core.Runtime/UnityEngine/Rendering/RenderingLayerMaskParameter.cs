namespace UnityEngine.Rendering
{
	[global::System.Serializable]
	[global::System.Diagnostics.DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	public class RenderingLayerMaskParameter : global::UnityEngine.Rendering.VolumeParameter<global::UnityEngine.RenderingLayerMask>
	{
		public RenderingLayerMaskParameter(global::UnityEngine.RenderingLayerMask value, bool overrideState = false)
			: base(value, overrideState)
		{
		}
	}
}
