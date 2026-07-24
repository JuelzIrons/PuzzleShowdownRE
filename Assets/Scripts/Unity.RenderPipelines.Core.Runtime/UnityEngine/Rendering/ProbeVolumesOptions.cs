namespace UnityEngine.Rendering
{
	[global::System.Serializable]
	[global::UnityEngine.Rendering.VolumeComponentMenu("Lighting/Adaptive Probe Volumes Options")]
	[global::UnityEngine.Rendering.SupportedOnRenderPipeline(new global::System.Type[] { })]
	[global::UnityEngine.Rendering.DisplayInfo(name = "Adaptive Probe Volumes Options")]
	public sealed class ProbeVolumesOptions : global::UnityEngine.Rendering.VolumeComponent
	{
		[global::UnityEngine.Tooltip("The overridden normal bias to be applied to the world position when sampling the Adaptive Probe Volumes data structure. Unit is meters.")]
		public global::UnityEngine.Rendering.ClampedFloatParameter normalBias = new global::UnityEngine.Rendering.ClampedFloatParameter(0.05f, 0f, 2f);

		[global::UnityEngine.Tooltip("A bias alongside the view vector to be applied to the world position when sampling the Adaptive Probe Volumes data structure. Unit is meters.")]
		public global::UnityEngine.Rendering.ClampedFloatParameter viewBias = new global::UnityEngine.Rendering.ClampedFloatParameter(0.1f, 0f, 2f);

		[global::UnityEngine.Tooltip("Whether to scale the bias for Adaptive Probe Volumes by the minimum distance between probes.")]
		public global::UnityEngine.Rendering.BoolParameter scaleBiasWithMinProbeDistance = new global::UnityEngine.Rendering.BoolParameter(value: false);

		[global::UnityEngine.Tooltip("Noise to be applied to the sampling position. It can hide seams issues between subdivision levels, but introduces noise.")]
		public global::UnityEngine.Rendering.ClampedFloatParameter samplingNoise = new global::UnityEngine.Rendering.ClampedFloatParameter(0.1f, 0f, 1f);

		[global::UnityEngine.Tooltip("Whether to animate the noise when TAA is enabled. It can potentially remove the visible noise patterns.")]
		public global::UnityEngine.Rendering.BoolParameter animateSamplingNoise = new global::UnityEngine.Rendering.BoolParameter(value: true);

		[global::UnityEngine.Tooltip("Method used to reduce leaks. Currently available modes are crude, but cheap methods.")]
		public global::UnityEngine.Rendering.APVLeakReductionModeParameter leakReductionMode = new global::UnityEngine.Rendering.APVLeakReductionModeParameter(global::UnityEngine.Rendering.APVLeakReductionMode.Quality);

		[global::System.Obsolete("This parameter isn't used anymore. #from(6000.0)")]
		public global::UnityEngine.Rendering.ClampedFloatParameter minValidDotProductValue = new global::UnityEngine.Rendering.ClampedFloatParameter(0.1f, -1f, 0.33f);

		[global::UnityEngine.Tooltip("When enabled, reflection probe normalization can only decrease the reflection intensity.")]
		public global::UnityEngine.Rendering.BoolParameter occlusionOnlyReflectionNormalization = new global::UnityEngine.Rendering.BoolParameter(value: true);

		[global::UnityEngine.Rendering.AdditionalProperty]
		[global::UnityEngine.Tooltip("Global probe volumes weight. Allows for fading out probe volumes influence falling back to ambient probe.")]
		public global::UnityEngine.Rendering.ClampedFloatParameter intensityMultiplier = new global::UnityEngine.Rendering.ClampedFloatParameter(1f, 0f, 1f);

		[global::UnityEngine.Rendering.AdditionalProperty]
		[global::UnityEngine.Tooltip("Multiplier applied on the sky lighting when using sky occlusion.")]
		public global::UnityEngine.Rendering.ClampedFloatParameter skyOcclusionIntensityMultiplier = new global::UnityEngine.Rendering.ClampedFloatParameter(1f, 0f, 5f);

		[global::UnityEngine.Rendering.AdditionalProperty]
		[global::UnityEngine.Tooltip("Offset applied at runtime to probe positions in world space.\nThis is not considered while baking.")]
		public global::UnityEngine.Rendering.Vector3Parameter worldOffset = new global::UnityEngine.Rendering.Vector3Parameter(global::UnityEngine.Vector3.zero);
	}
}
