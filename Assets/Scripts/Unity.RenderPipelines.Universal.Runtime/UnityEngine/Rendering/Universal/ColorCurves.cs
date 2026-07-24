namespace UnityEngine.Rendering.Universal
{
	[global::System.Serializable]
	[global::UnityEngine.Rendering.VolumeComponentMenu("Post-processing/Color Curves")]
	[global::UnityEngine.Rendering.SupportedOnRenderPipeline(typeof(global::UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset))]
	public sealed class ColorCurves : global::UnityEngine.Rendering.VolumeComponent, global::UnityEngine.Rendering.IPostProcessComponent
	{
		[global::UnityEngine.Tooltip("Affects the luminance across the whole image.")]
		public global::UnityEngine.Rendering.TextureCurveParameter master = new global::UnityEngine.Rendering.TextureCurveParameter(new global::UnityEngine.Rendering.TextureCurve(new global::UnityEngine.Keyframe[2]
		{
			new global::UnityEngine.Keyframe(0f, 0f, 1f, 1f),
			new global::UnityEngine.Keyframe(1f, 1f, 1f, 1f)
		}, 0f, loop: false, new global::UnityEngine.Vector2(0f, 1f)));

		[global::UnityEngine.Tooltip("Affects the red channel intensity across the whole image.")]
		public global::UnityEngine.Rendering.TextureCurveParameter red = new global::UnityEngine.Rendering.TextureCurveParameter(new global::UnityEngine.Rendering.TextureCurve(new global::UnityEngine.Keyframe[2]
		{
			new global::UnityEngine.Keyframe(0f, 0f, 1f, 1f),
			new global::UnityEngine.Keyframe(1f, 1f, 1f, 1f)
		}, 0f, loop: false, new global::UnityEngine.Vector2(0f, 1f)));

		[global::UnityEngine.Tooltip("Affects the green channel intensity across the whole image.")]
		public global::UnityEngine.Rendering.TextureCurveParameter green = new global::UnityEngine.Rendering.TextureCurveParameter(new global::UnityEngine.Rendering.TextureCurve(new global::UnityEngine.Keyframe[2]
		{
			new global::UnityEngine.Keyframe(0f, 0f, 1f, 1f),
			new global::UnityEngine.Keyframe(1f, 1f, 1f, 1f)
		}, 0f, loop: false, new global::UnityEngine.Vector2(0f, 1f)));

		[global::UnityEngine.Tooltip("Affects the blue channel intensity across the whole image.")]
		public global::UnityEngine.Rendering.TextureCurveParameter blue = new global::UnityEngine.Rendering.TextureCurveParameter(new global::UnityEngine.Rendering.TextureCurve(new global::UnityEngine.Keyframe[2]
		{
			new global::UnityEngine.Keyframe(0f, 0f, 1f, 1f),
			new global::UnityEngine.Keyframe(1f, 1f, 1f, 1f)
		}, 0f, loop: false, new global::UnityEngine.Vector2(0f, 1f)));

		[global::UnityEngine.Tooltip("Shifts the input hue (x-axis) according to the output hue (y-axis).")]
		public global::UnityEngine.Rendering.TextureCurveParameter hueVsHue = new global::UnityEngine.Rendering.TextureCurveParameter(new global::UnityEngine.Rendering.TextureCurve(new global::UnityEngine.Keyframe[0], 0.5f, loop: true, new global::UnityEngine.Vector2(0f, 1f)));

		[global::UnityEngine.Tooltip("Adjusts saturation (y-axis) according to the input hue (x-axis).")]
		public global::UnityEngine.Rendering.TextureCurveParameter hueVsSat = new global::UnityEngine.Rendering.TextureCurveParameter(new global::UnityEngine.Rendering.TextureCurve(new global::UnityEngine.Keyframe[0], 0.5f, loop: true, new global::UnityEngine.Vector2(0f, 1f)));

		[global::UnityEngine.Tooltip("Adjusts saturation (y-axis) according to the input saturation (x-axis).")]
		public global::UnityEngine.Rendering.TextureCurveParameter satVsSat = new global::UnityEngine.Rendering.TextureCurveParameter(new global::UnityEngine.Rendering.TextureCurve(new global::UnityEngine.Keyframe[0], 0.5f, loop: false, new global::UnityEngine.Vector2(0f, 1f)));

		[global::UnityEngine.Tooltip("Adjusts saturation (y-axis) according to the input luminance (x-axis).")]
		public global::UnityEngine.Rendering.TextureCurveParameter lumVsSat = new global::UnityEngine.Rendering.TextureCurveParameter(new global::UnityEngine.Rendering.TextureCurve(new global::UnityEngine.Keyframe[0], 0.5f, loop: false, new global::UnityEngine.Vector2(0f, 1f)));

		public bool IsActive()
		{
			return true;
		}

		[global::System.Obsolete("Unused. #from(2023.1)")]
		public bool IsTileCompatible()
		{
			return true;
		}
	}
}
