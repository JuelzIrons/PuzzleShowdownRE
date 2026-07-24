namespace UnityEngine.Rendering.Universal
{
	[global::System.Serializable]
	[global::UnityEngine.Rendering.VolumeComponentMenu("Post-processing/Bloom")]
	[global::UnityEngine.Rendering.SupportedOnRenderPipeline(typeof(global::UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset))]
	public sealed class Bloom : global::UnityEngine.Rendering.VolumeComponent, global::UnityEngine.Rendering.IPostProcessComponent
	{
		[global::System.Obsolete("This is obsolete, please use maxIterations instead. #from(2022.2) #breakingFrom(2023.1)", true)]
		[global::UnityEngine.Tooltip("The number of final iterations to skip in the effect processing sequence.")]
		public global::UnityEngine.Rendering.ClampedIntParameter skipIterations = new global::UnityEngine.Rendering.ClampedIntParameter(1, 0, 16);

		[global::UnityEngine.Header("Bloom")]
		[global::UnityEngine.Tooltip("Filters out pixels under this level of brightness. Value is in gamma-space.")]
		public global::UnityEngine.Rendering.MinFloatParameter threshold = new global::UnityEngine.Rendering.MinFloatParameter(0.9f, 0f);

		[global::UnityEngine.Tooltip("Strength of the bloom filter.")]
		public global::UnityEngine.Rendering.MinFloatParameter intensity = new global::UnityEngine.Rendering.MinFloatParameter(0f, 0f);

		[global::UnityEngine.Tooltip("Set the radius of the bloom effect.")]
		public global::UnityEngine.Rendering.ClampedFloatParameter scatter = new global::UnityEngine.Rendering.ClampedFloatParameter(0.7f, 0f, 1f);

		[global::UnityEngine.Tooltip("Set the maximum intensity that Unity uses to calculate Bloom. If pixels in your Scene are more intense than this, URP renders them at their current intensity, but uses this intensity value for the purposes of Bloom calculations.")]
		public global::UnityEngine.Rendering.MinFloatParameter clamp = new global::UnityEngine.Rendering.MinFloatParameter(65472f, 0f);

		[global::UnityEngine.Tooltip("Use the color picker to select a color for the Bloom effect to tint to.")]
		public global::UnityEngine.Rendering.ColorParameter tint = new global::UnityEngine.Rendering.ColorParameter(global::UnityEngine.Color.white, hdr: false, showAlpha: false, showEyeDropper: true);

		[global::UnityEngine.Tooltip("Use bicubic sampling instead of bilinear sampling for the upsampling passes. This is slightly more expensive but helps getting smoother visuals.")]
		public global::UnityEngine.Rendering.BoolParameter highQualityFiltering = new global::UnityEngine.Rendering.BoolParameter(value: false);

		[global::UnityEngine.Tooltip("Set the filtering algorithm for the Bloom effect.")]
		public global::UnityEngine.Rendering.Universal.BloomFilterModeParameter filter = new global::UnityEngine.Rendering.Universal.BloomFilterModeParameter(global::UnityEngine.Rendering.Universal.BloomFilterMode.Gaussian);

		[global::UnityEngine.Tooltip("The starting resolution that this effect begins processing.")]
		[global::UnityEngine.Rendering.AdditionalProperty]
		public global::UnityEngine.Rendering.Universal.DownscaleParameter downscale = new global::UnityEngine.Rendering.Universal.DownscaleParameter(global::UnityEngine.Rendering.Universal.BloomDownscaleMode.Half);

		[global::UnityEngine.Tooltip("The maximum number of iterations in the effect processing sequence.")]
		[global::UnityEngine.Rendering.AdditionalProperty]
		public global::UnityEngine.Rendering.ClampedIntParameter maxIterations = new global::UnityEngine.Rendering.ClampedIntParameter(6, 2, 8);

		[global::UnityEngine.Header("Lens Dirt")]
		[global::UnityEngine.Tooltip("Dirtiness texture to add smudges or dust to the bloom effect.")]
		public global::UnityEngine.Rendering.TextureParameter dirtTexture = new global::UnityEngine.Rendering.TextureParameter(null);

		[global::UnityEngine.Tooltip("Amount of dirtiness.")]
		public global::UnityEngine.Rendering.MinFloatParameter dirtIntensity = new global::UnityEngine.Rendering.MinFloatParameter(0f, 0f);

		public bool IsActive()
		{
			return intensity.value > 0f;
		}

		[global::System.Obsolete("Unused. #from(2023.1)")]
		public bool IsTileCompatible()
		{
			return false;
		}
	}
}
