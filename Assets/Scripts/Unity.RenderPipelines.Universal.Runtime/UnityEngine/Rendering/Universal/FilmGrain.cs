namespace UnityEngine.Rendering.Universal
{
	[global::System.Serializable]
	[global::UnityEngine.Rendering.VolumeComponentMenu("Post-processing/Film Grain")]
	[global::UnityEngine.Rendering.SupportedOnRenderPipeline(typeof(global::UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset))]
	public sealed class FilmGrain : global::UnityEngine.Rendering.VolumeComponent, global::UnityEngine.Rendering.IPostProcessComponent
	{
		[global::UnityEngine.Tooltip("The type of grain to use. You can select a preset or provide your own texture by selecting Custom.")]
		public global::UnityEngine.Rendering.Universal.FilmGrainLookupParameter type = new global::UnityEngine.Rendering.Universal.FilmGrainLookupParameter(global::UnityEngine.Rendering.Universal.FilmGrainLookup.Thin1);

		[global::UnityEngine.Tooltip("Use the slider to set the strength of the Film Grain effect.")]
		public global::UnityEngine.Rendering.ClampedFloatParameter intensity = new global::UnityEngine.Rendering.ClampedFloatParameter(0f, 0f, 1f);

		[global::UnityEngine.Tooltip("Controls the noisiness response curve based on scene luminance. Higher values mean less noise in light areas.")]
		public global::UnityEngine.Rendering.ClampedFloatParameter response = new global::UnityEngine.Rendering.ClampedFloatParameter(0.8f, 0f, 1f);

		[global::UnityEngine.Tooltip("A tileable texture to use for the grain. The neutral value is 0.5 where no grain is applied.")]
		public global::UnityEngine.Rendering.NoInterpTextureParameter texture = new global::UnityEngine.Rendering.NoInterpTextureParameter(null);

		public bool IsActive()
		{
			if (intensity.value > 0f)
			{
				if (type.value == global::UnityEngine.Rendering.Universal.FilmGrainLookup.Custom)
				{
					return texture.value != null;
				}
				return true;
			}
			return false;
		}

		[global::System.Obsolete("Unused. #from(2023.1)")]
		public bool IsTileCompatible()
		{
			return true;
		}
	}
}
