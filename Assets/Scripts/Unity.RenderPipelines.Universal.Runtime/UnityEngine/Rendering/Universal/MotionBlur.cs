namespace UnityEngine.Rendering.Universal
{
	[global::System.Serializable]
	[global::UnityEngine.Rendering.VolumeComponentMenu("Post-processing/Motion Blur")]
	[global::UnityEngine.Rendering.SupportedOnRenderPipeline(typeof(global::UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset))]
	public sealed class MotionBlur : global::UnityEngine.Rendering.VolumeComponent, global::UnityEngine.Rendering.IPostProcessComponent
	{
		[global::UnityEngine.Tooltip("The motion blur technique to use. If you don't need object motion blur, CameraOnly will result in better performance.")]
		public global::UnityEngine.Rendering.Universal.MotionBlurModeParameter mode = new global::UnityEngine.Rendering.Universal.MotionBlurModeParameter(global::UnityEngine.Rendering.Universal.MotionBlurMode.CameraOnly);

		[global::UnityEngine.Tooltip("The quality of the effect. Lower presets will result in better performance at the expense of visual quality.")]
		public global::UnityEngine.Rendering.Universal.MotionBlurQualityParameter quality = new global::UnityEngine.Rendering.Universal.MotionBlurQualityParameter(global::UnityEngine.Rendering.Universal.MotionBlurQuality.Low);

		[global::UnityEngine.Tooltip("The strength of the motion blur filter. Acts as a multiplier for velocities.")]
		public global::UnityEngine.Rendering.ClampedFloatParameter intensity = new global::UnityEngine.Rendering.ClampedFloatParameter(0f, 0f, 1f);

		[global::UnityEngine.Tooltip("Sets the maximum length, as a fraction of the screen's full resolution, that the velocity resulting from Camera rotation can have. Lower values will improve performance.")]
		public global::UnityEngine.Rendering.ClampedFloatParameter clamp = new global::UnityEngine.Rendering.ClampedFloatParameter(0.05f, 0f, 0.2f);

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
