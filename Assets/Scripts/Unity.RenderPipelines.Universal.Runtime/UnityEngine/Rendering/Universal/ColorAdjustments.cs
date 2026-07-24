namespace UnityEngine.Rendering.Universal
{
	[global::System.Serializable]
	[global::UnityEngine.Rendering.VolumeComponentMenu("Post-processing/Color Adjustments")]
	[global::UnityEngine.Rendering.SupportedOnRenderPipeline(typeof(global::UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset))]
	public sealed class ColorAdjustments : global::UnityEngine.Rendering.VolumeComponent, global::UnityEngine.Rendering.IPostProcessComponent
	{
		[global::UnityEngine.Tooltip("Adjusts the overall exposure of the scene in EV100. This is applied after HDR effect and right before tonemapping so it won't affect previous effects in the chain.")]
		public global::UnityEngine.Rendering.FloatParameter postExposure = new global::UnityEngine.Rendering.FloatParameter(0f);

		[global::UnityEngine.Tooltip("Expands or shrinks the overall range of tonal values.")]
		public global::UnityEngine.Rendering.ClampedFloatParameter contrast = new global::UnityEngine.Rendering.ClampedFloatParameter(0f, -100f, 100f);

		[global::UnityEngine.Tooltip("Tint the render by multiplying a color.")]
		public global::UnityEngine.Rendering.ColorParameter colorFilter = new global::UnityEngine.Rendering.ColorParameter(global::UnityEngine.Color.white, hdr: true, showAlpha: false, showEyeDropper: true);

		[global::UnityEngine.Tooltip("Shift the hue of all colors.")]
		public global::UnityEngine.Rendering.ClampedFloatParameter hueShift = new global::UnityEngine.Rendering.ClampedFloatParameter(0f, -180f, 180f);

		[global::UnityEngine.Tooltip("Pushes the intensity of all colors.")]
		public global::UnityEngine.Rendering.ClampedFloatParameter saturation = new global::UnityEngine.Rendering.ClampedFloatParameter(0f, -100f, 100f);

		public bool IsActive()
		{
			if (postExposure.value == 0f && contrast.value == 0f && !(colorFilter != global::UnityEngine.Color.white) && !(hueShift != 0f))
			{
				return saturation != 0f;
			}
			return true;
		}

		[global::System.Obsolete("Unused. #from(2023.1)")]
		public bool IsTileCompatible()
		{
			return true;
		}
	}
}
