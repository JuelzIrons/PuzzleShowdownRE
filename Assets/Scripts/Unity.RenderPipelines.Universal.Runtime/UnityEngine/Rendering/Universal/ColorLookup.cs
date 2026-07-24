namespace UnityEngine.Rendering.Universal
{
	[global::System.Serializable]
	[global::UnityEngine.Rendering.VolumeComponentMenu("Post-processing/Color Lookup")]
	[global::UnityEngine.Rendering.SupportedOnRenderPipeline(typeof(global::UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset))]
	public sealed class ColorLookup : global::UnityEngine.Rendering.VolumeComponent, global::UnityEngine.Rendering.IPostProcessComponent
	{
		[global::UnityEngine.Tooltip("A 2D Lookup Texture (LUT) to use for color grading.")]
		public global::UnityEngine.Rendering.TextureParameter texture = new global::UnityEngine.Rendering.TextureParameter(null);

		[global::UnityEngine.Tooltip("How much of the lookup texture will contribute to the color grading effect.")]
		public global::UnityEngine.Rendering.ClampedFloatParameter contribution = new global::UnityEngine.Rendering.ClampedFloatParameter(0f, 0f, 1f);

		public bool IsActive()
		{
			if (contribution.value > 0f)
			{
				return ValidateLUT();
			}
			return false;
		}

		[global::System.Obsolete("Unused. #from(2023.1)")]
		public bool IsTileCompatible()
		{
			return true;
		}

		public bool ValidateLUT()
		{
			global::UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset asset = global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.asset;
			if (asset == null || texture.value == null)
			{
				return false;
			}
			int colorGradingLutSize = asset.colorGradingLutSize;
			if (texture.value.height != colorGradingLutSize)
			{
				return false;
			}
			bool flag = false;
			global::UnityEngine.Texture value = texture.value;
			if (!(value is global::UnityEngine.Texture2D texture2D))
			{
				if (value is global::UnityEngine.RenderTexture renderTexture)
				{
					flag |= renderTexture.dimension == global::UnityEngine.Rendering.TextureDimension.Tex2D && renderTexture.width == colorGradingLutSize * colorGradingLutSize && !renderTexture.sRGB;
				}
			}
			else
			{
				flag |= texture2D.width == colorGradingLutSize * colorGradingLutSize && !global::UnityEngine.Experimental.Rendering.GraphicsFormatUtility.IsSRGBFormat(texture2D.graphicsFormat);
			}
			return flag;
		}
	}
}
