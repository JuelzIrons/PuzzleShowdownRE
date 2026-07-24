namespace UnityEngine.Rendering
{
	[global::System.Serializable]
	[global::System.Diagnostics.DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	public class ColorParameter : global::UnityEngine.Rendering.VolumeParameter<global::UnityEngine.Color>
	{
		[global::System.NonSerialized]
		public bool hdr;

		[global::System.NonSerialized]
		public bool showAlpha = true;

		[global::System.NonSerialized]
		public bool showEyeDropper = true;

		public ColorParameter(global::UnityEngine.Color value, bool overrideState = false)
			: base(value, overrideState)
		{
		}

		public ColorParameter(global::UnityEngine.Color value, bool hdr, bool showAlpha, bool showEyeDropper, bool overrideState = false)
			: base(value, overrideState)
		{
			this.hdr = hdr;
			this.showAlpha = showAlpha;
			this.showEyeDropper = showEyeDropper;
			this.overrideState = overrideState;
		}

		public override void Interp(global::UnityEngine.Color from, global::UnityEngine.Color to, float t)
		{
			m_Value.r = from.r + (to.r - from.r) * t;
			m_Value.g = from.g + (to.g - from.g) * t;
			m_Value.b = from.b + (to.b - from.b) * t;
			m_Value.a = from.a + (to.a - from.a) * t;
		}
	}
}
