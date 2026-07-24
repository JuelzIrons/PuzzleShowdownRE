namespace UnityEngine.Rendering
{
	[global::System.Serializable]
	[global::System.Diagnostics.DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	public class TextureParameter : global::UnityEngine.Rendering.VolumeParameter<global::UnityEngine.Texture>
	{
		public global::UnityEngine.Rendering.TextureDimension dimension;

		public TextureParameter(global::UnityEngine.Texture value, bool overrideState = false)
			: this(value, global::UnityEngine.Rendering.TextureDimension.Any, overrideState)
		{
		}

		public TextureParameter(global::UnityEngine.Texture value, global::UnityEngine.Rendering.TextureDimension dimension, bool overrideState = false)
			: base(value, overrideState)
		{
			this.dimension = dimension;
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
