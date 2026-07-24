namespace UnityEngine.Rendering
{
	public enum DynamicResUpscaleFilter : byte
	{
		[global::System.Obsolete("Bilinear upscale filter is considered obsolete and is not supported anymore, please use CatmullRom for a very cheap, but blurry filter. #from(2022.1)")]
		Bilinear = 0,
		CatmullRom = 1,
		[global::System.Obsolete("Lanczos upscale filter is considered obsolete and is not supported anymore, please use Contrast Adaptive Sharpening for very sharp filter or FidelityFX Super Resolution 1.0. #from(2022.1)")]
		Lanczos = 2,
		ContrastAdaptiveSharpen = 3,
		[global::UnityEngine.InspectorName("FidelityFX Super Resolution 1.0")]
		EdgeAdaptiveScalingUpres = 4,
		[global::UnityEngine.InspectorName("TAA Upscale")]
		TAAU = 5
	}
}
