namespace UnityEngine.Rendering
{
	[global::System.Serializable]
	[global::System.Diagnostics.DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	public class NoInterpRenderTextureParameter : global::UnityEngine.Rendering.VolumeParameter<global::UnityEngine.RenderTexture>
	{
		public NoInterpRenderTextureParameter(global::UnityEngine.RenderTexture value, bool overrideState = false)
			: base(value, overrideState)
		{
		}

		public override int GetHashCode()
		{
			int result = base.GetHashCode();
			if (value != null)
			{
				result = 23 * global::UnityEngine.Rendering.CoreUtils.GetTextureHash(value);
			}
			return result;
		}
	}
}
