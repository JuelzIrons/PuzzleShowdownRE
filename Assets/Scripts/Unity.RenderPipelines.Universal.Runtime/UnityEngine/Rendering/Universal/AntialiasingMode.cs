namespace UnityEngine.Rendering.Universal
{
	public enum AntialiasingMode
	{
		[global::UnityEngine.InspectorName("No Anti-aliasing")]
		None = 0,
		[global::UnityEngine.InspectorName("Fast Approximate Anti-aliasing (FXAA)")]
		FastApproximateAntialiasing = 1,
		[global::UnityEngine.InspectorName("Subpixel Morphological Anti-aliasing (SMAA)")]
		SubpixelMorphologicalAntiAliasing = 2,
		[global::UnityEngine.InspectorName("Temporal Anti-aliasing (TAA)")]
		TemporalAntiAliasing = 3
	}
}
