namespace UnityEngine.Rendering.Universal
{
	[global::System.Serializable]
	[global::UnityEngine.Rendering.VolumeComponentMenu("Post-processing/Depth Of Field")]
	[global::UnityEngine.Rendering.SupportedOnRenderPipeline(typeof(global::UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset))]
	public sealed class DepthOfField : global::UnityEngine.Rendering.VolumeComponent, global::UnityEngine.Rendering.IPostProcessComponent
	{
		[global::UnityEngine.Tooltip("Use \"Gaussian\" for a faster but non physical depth of field; \"Bokeh\" for a more realistic but slower depth of field.")]
		public global::UnityEngine.Rendering.Universal.DepthOfFieldModeParameter mode = new global::UnityEngine.Rendering.Universal.DepthOfFieldModeParameter(global::UnityEngine.Rendering.Universal.DepthOfFieldMode.Off);

		[global::UnityEngine.Tooltip("The distance at which the blurring will start.")]
		public global::UnityEngine.Rendering.MinFloatParameter gaussianStart = new global::UnityEngine.Rendering.MinFloatParameter(10f, 0f);

		[global::UnityEngine.Tooltip("The distance at which the blurring will reach its maximum radius.")]
		public global::UnityEngine.Rendering.MinFloatParameter gaussianEnd = new global::UnityEngine.Rendering.MinFloatParameter(30f, 0f);

		[global::UnityEngine.Tooltip("The maximum radius of the gaussian blur. Values above 1 may show under-sampling artifacts.")]
		public global::UnityEngine.Rendering.ClampedFloatParameter gaussianMaxRadius = new global::UnityEngine.Rendering.ClampedFloatParameter(1f, 0.5f, 1.5f);

		[global::UnityEngine.Tooltip("Use higher quality sampling to reduce flickering and improve the overall blur smoothness.")]
		public global::UnityEngine.Rendering.BoolParameter highQualitySampling = new global::UnityEngine.Rendering.BoolParameter(value: false);

		[global::UnityEngine.Tooltip("The distance to the point of focus.")]
		public global::UnityEngine.Rendering.MinFloatParameter focusDistance = new global::UnityEngine.Rendering.MinFloatParameter(10f, 0.1f);

		[global::UnityEngine.Tooltip("The ratio of aperture (known as f-stop or f-number). The smaller the value is, the shallower the depth of field is.")]
		public global::UnityEngine.Rendering.ClampedFloatParameter aperture = new global::UnityEngine.Rendering.ClampedFloatParameter(5.6f, 1f, 32f);

		[global::UnityEngine.Tooltip("The distance between the lens and the film. The larger the value is, the shallower the depth of field is.")]
		public global::UnityEngine.Rendering.ClampedFloatParameter focalLength = new global::UnityEngine.Rendering.ClampedFloatParameter(50f, 1f, 300f);

		[global::UnityEngine.Tooltip("The number of aperture blades.")]
		public global::UnityEngine.Rendering.ClampedIntParameter bladeCount = new global::UnityEngine.Rendering.ClampedIntParameter(5, 3, 9);

		[global::UnityEngine.Tooltip("The curvature of aperture blades. The smaller the value is, the more visible aperture blades are. A value of 1 will make the bokeh perfectly circular.")]
		public global::UnityEngine.Rendering.ClampedFloatParameter bladeCurvature = new global::UnityEngine.Rendering.ClampedFloatParameter(1f, 0f, 1f);

		[global::UnityEngine.Tooltip("The rotation of aperture blades in degrees.")]
		public global::UnityEngine.Rendering.ClampedFloatParameter bladeRotation = new global::UnityEngine.Rendering.ClampedFloatParameter(0f, -180f, 180f);

		public bool IsActive()
		{
			if (mode.value == global::UnityEngine.Rendering.Universal.DepthOfFieldMode.Off || global::UnityEngine.SystemInfo.graphicsShaderLevel < 35)
			{
				return false;
			}
			if (mode.value == global::UnityEngine.Rendering.Universal.DepthOfFieldMode.Gaussian)
			{
				return global::UnityEngine.SystemInfo.supportedRenderTargetCount > 1;
			}
			return true;
		}

		[global::System.Obsolete("Unused. #from(2023.1)")]
		public bool IsTileCompatible()
		{
			return false;
		}
	}
}
