namespace UnityEngine.Rendering
{
	[global::System.Serializable]
	public class TextureGradientParameter : global::UnityEngine.Rendering.VolumeParameter<global::UnityEngine.Rendering.TextureGradient>
	{
		public TextureGradientParameter(global::UnityEngine.Rendering.TextureGradient value, bool overrideState = false)
			: base(value, overrideState)
		{
		}

		public override void Release()
		{
			m_Value.Release();
		}
	}
}
