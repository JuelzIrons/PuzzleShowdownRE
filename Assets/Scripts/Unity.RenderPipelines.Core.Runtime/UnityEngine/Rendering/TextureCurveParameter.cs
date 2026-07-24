namespace UnityEngine.Rendering
{
	[global::System.Serializable]
	public class TextureCurveParameter : global::UnityEngine.Rendering.VolumeParameter<global::UnityEngine.Rendering.TextureCurve>
	{
		public TextureCurveParameter(global::UnityEngine.Rendering.TextureCurve value, bool overrideState = false)
			: base(value, overrideState)
		{
		}

		public override void Release()
		{
			m_Value.Release();
		}
	}
}
