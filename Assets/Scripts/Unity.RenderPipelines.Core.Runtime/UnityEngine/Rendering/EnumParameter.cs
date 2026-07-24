namespace UnityEngine.Rendering
{
	[global::System.Serializable]
	[global::System.Diagnostics.DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	public sealed class EnumParameter<T> : global::UnityEngine.Rendering.VolumeParameter<T>
	{
		public EnumParameter(T value, bool overrideState = false)
			: base(value, overrideState)
		{
		}
	}
}
