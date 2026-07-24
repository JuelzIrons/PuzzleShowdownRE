namespace UnityEngine.Rendering
{
	[global::System.Serializable]
	[global::System.Diagnostics.DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	public class NoInterpVector2Parameter : global::UnityEngine.Rendering.VolumeParameter<global::UnityEngine.Vector2>
	{
		public NoInterpVector2Parameter(global::UnityEngine.Vector2 value, bool overrideState = false)
			: base(value, overrideState)
		{
		}
	}
}
