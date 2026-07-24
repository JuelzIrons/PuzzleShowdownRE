namespace UnityEngine.Rendering.Universal
{
	[global::System.Serializable]
	[global::UnityEngine.Rendering.VolumeComponentMenu("Post-processing/Tonemapping")]
	[global::UnityEngine.Rendering.SupportedOnRenderPipeline(typeof(global::UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset))]
	public sealed class Tonemapping : global::UnityEngine.Rendering.VolumeComponent, global::UnityEngine.Rendering.IPostProcessComponent
	{
		[global::UnityEngine.Tooltip("Select a tonemapping algorithm to use for the color grading process.")]
		public global::UnityEngine.Rendering.Universal.TonemappingModeParameter mode = new global::UnityEngine.Rendering.Universal.TonemappingModeParameter(global::UnityEngine.Rendering.Universal.TonemappingMode.None);

		[global::UnityEngine.Rendering.AdditionalProperty]
		[global::UnityEngine.Tooltip("Specifies the range reduction mode used when HDR output is enabled and Neutral tonemapping is enabled.")]
		public global::UnityEngine.Rendering.Universal.NeutralRangeReductionModeParameter neutralHDRRangeReductionMode = new global::UnityEngine.Rendering.Universal.NeutralRangeReductionModeParameter(global::UnityEngine.Rendering.Universal.NeutralRangeReductionMode.BT2390);

		[global::UnityEngine.Tooltip("Use the ACES preset for HDR displays.")]
		public global::UnityEngine.Rendering.Universal.HDRACESPresetParameter acesPreset = new global::UnityEngine.Rendering.Universal.HDRACESPresetParameter(global::UnityEngine.Rendering.Universal.HDRACESPreset.ACES1000Nits);

		[global::UnityEngine.Tooltip("Specify how much hue to preserve. Values closer to 0 are likely to preserve hue. As values get closer to 1, Unity doesn't correct hue shifts.")]
		public global::UnityEngine.Rendering.ClampedFloatParameter hueShiftAmount = new global::UnityEngine.Rendering.ClampedFloatParameter(0f, 0f, 1f);

		[global::UnityEngine.Tooltip("Enable to use values detected from the output device as paper white. When enabled, output images might differ between SDR and HDR. For best accuracy, set this value manually.")]
		public global::UnityEngine.Rendering.BoolParameter detectPaperWhite = new global::UnityEngine.Rendering.BoolParameter(value: false);

		[global::UnityEngine.Tooltip("The reference brightness of a paper white surface. This property determines the maximum brightness of UI. The brightness of the scene is scaled relative to this value. The value is in nits.")]
		public global::UnityEngine.Rendering.ClampedFloatParameter paperWhite = new global::UnityEngine.Rendering.ClampedFloatParameter(300f, 0f, 400f);

		[global::UnityEngine.Tooltip("Enable to use the minimum and maximum brightness values detected from the output device. For best accuracy, considering calibrating these values manually.")]
		public global::UnityEngine.Rendering.BoolParameter detectBrightnessLimits = new global::UnityEngine.Rendering.BoolParameter(value: true);

		[global::UnityEngine.Tooltip("The minimum brightness of the screen (in nits). This value is assumed to be 0.005f with ACES Tonemap.")]
		public global::UnityEngine.Rendering.ClampedFloatParameter minNits = new global::UnityEngine.Rendering.ClampedFloatParameter(0.005f, 0f, 50f);

		[global::UnityEngine.Tooltip("The maximum brightness of the screen (in nits). This value is defined by the preset when using ACES Tonemap.")]
		public global::UnityEngine.Rendering.ClampedFloatParameter maxNits = new global::UnityEngine.Rendering.ClampedFloatParameter(1000f, 0f, 5000f);

		public bool IsActive()
		{
			return mode.value != global::UnityEngine.Rendering.Universal.TonemappingMode.None;
		}

		[global::System.Obsolete("Unused. #from(2023.1)")]
		public bool IsTileCompatible()
		{
			return true;
		}
	}
}
