namespace UnityEngine.Rendering
{
	[global::System.Serializable]
	[global::System.Diagnostics.DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	public class NoInterpColorParameter : global::UnityEngine.Rendering.VolumeParameter<global::UnityEngine.Color>
	{
		public bool hdr;

		[global::System.NonSerialized]
		public bool showAlpha = true;

		[global::System.NonSerialized]
		public bool showEyeDropper = true;

		public NoInterpColorParameter(global::UnityEngine.Color value, bool overrideState = false)
			: base(value, overrideState)
		{
		}

		public NoInterpColorParameter(global::UnityEngine.Color value, bool hdr, bool showAlpha, bool showEyeDropper, bool overrideState = false)
			: base(value, overrideState)
		{
			this.hdr = hdr;
			this.showAlpha = showAlpha;
			this.showEyeDropper = showEyeDropper;
			this.overrideState = overrideState;
		}
	}
}
