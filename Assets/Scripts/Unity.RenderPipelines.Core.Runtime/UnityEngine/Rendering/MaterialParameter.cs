namespace UnityEngine.Rendering
{
	[global::System.Serializable]
	[global::System.Diagnostics.DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	public class MaterialParameter : global::UnityEngine.Rendering.VolumeParameter<global::UnityEngine.Material>
	{
		public MaterialParameter(global::UnityEngine.Material value, bool overrideState = false)
			: base(value, overrideState)
		{
		}
	}
}
