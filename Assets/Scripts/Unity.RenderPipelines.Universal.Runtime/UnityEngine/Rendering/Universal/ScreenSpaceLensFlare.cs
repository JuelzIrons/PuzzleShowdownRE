namespace UnityEngine.Rendering.Universal
{
	[global::System.Serializable]
	[global::UnityEngine.Rendering.VolumeComponentMenu("Post-processing/Screen Space Lens Flare")]
	[global::UnityEngine.Rendering.SupportedOnRenderPipeline(typeof(global::UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset))]
	[global::UnityEngine.Rendering.DisplayInfo(name = "Screen Space Lens Flare")]
	public class ScreenSpaceLensFlare : global::UnityEngine.Rendering.VolumeComponent, global::UnityEngine.Rendering.IPostProcessComponent
	{
		public global::UnityEngine.Rendering.MinFloatParameter intensity = new global::UnityEngine.Rendering.MinFloatParameter(0f, 0f);

		public global::UnityEngine.Rendering.ColorParameter tintColor = new global::UnityEngine.Rendering.ColorParameter(global::UnityEngine.Color.white);

		[global::UnityEngine.Rendering.AdditionalProperty]
		public global::UnityEngine.Rendering.ClampedIntParameter bloomMip = new global::UnityEngine.Rendering.ClampedIntParameter(1, 0, 5);

		[global::UnityEngine.Header("Flares")]
		public global::UnityEngine.Rendering.MinFloatParameter firstFlareIntensity = new global::UnityEngine.Rendering.MinFloatParameter(1f, 0f);

		public global::UnityEngine.Rendering.MinFloatParameter secondaryFlareIntensity = new global::UnityEngine.Rendering.MinFloatParameter(1f, 0f);

		public global::UnityEngine.Rendering.MinFloatParameter warpedFlareIntensity = new global::UnityEngine.Rendering.MinFloatParameter(1f, 0f);

		[global::UnityEngine.Rendering.AdditionalProperty]
		public global::UnityEngine.Rendering.Vector2Parameter warpedFlareScale = new global::UnityEngine.Rendering.Vector2Parameter(new global::UnityEngine.Vector2(1f, 1f));

		public global::UnityEngine.Rendering.ClampedIntParameter samples = new global::UnityEngine.Rendering.ClampedIntParameter(1, 1, 3);

		[global::UnityEngine.Rendering.AdditionalProperty]
		public global::UnityEngine.Rendering.ClampedFloatParameter sampleDimmer = new global::UnityEngine.Rendering.ClampedFloatParameter(0.5f, 0.1f, 1f);

		public global::UnityEngine.Rendering.ClampedFloatParameter vignetteEffect = new global::UnityEngine.Rendering.ClampedFloatParameter(1f, 0f, 1f);

		public global::UnityEngine.Rendering.ClampedFloatParameter startingPosition = new global::UnityEngine.Rendering.ClampedFloatParameter(1.25f, 1f, 3f);

		public global::UnityEngine.Rendering.ClampedFloatParameter scale = new global::UnityEngine.Rendering.ClampedFloatParameter(1.5f, 1f, 4f);

		[global::UnityEngine.Header("Streaks")]
		public global::UnityEngine.Rendering.MinFloatParameter streaksIntensity = new global::UnityEngine.Rendering.MinFloatParameter(0f, 0f);

		public global::UnityEngine.Rendering.ClampedFloatParameter streaksLength = new global::UnityEngine.Rendering.ClampedFloatParameter(0.5f, 0f, 1f);

		public global::UnityEngine.Rendering.FloatParameter streaksOrientation = new global::UnityEngine.Rendering.FloatParameter(0f);

		public global::UnityEngine.Rendering.ClampedFloatParameter streaksThreshold = new global::UnityEngine.Rendering.ClampedFloatParameter(0.25f, 0f, 1f);

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Rendering.AdditionalProperty]
		public global::UnityEngine.Rendering.Universal.ScreenSpaceLensFlareResolutionParameter resolution = new global::UnityEngine.Rendering.Universal.ScreenSpaceLensFlareResolutionParameter(global::UnityEngine.Rendering.Universal.ScreenSpaceLensFlareResolution.Quarter);

		[global::UnityEngine.Header("Chromatic Abberation")]
		public global::UnityEngine.Rendering.ClampedFloatParameter chromaticAbberationIntensity = new global::UnityEngine.Rendering.ClampedFloatParameter(0.5f, 0f, 1f);

		public bool IsActive()
		{
			return intensity.value > 0f;
		}

		public bool IsStreaksActive()
		{
			return streaksIntensity.value > 0f;
		}

		[global::System.Obsolete("Unused. #from(2023.1)")]
		public bool IsTileCompatible()
		{
			return false;
		}
	}
}
