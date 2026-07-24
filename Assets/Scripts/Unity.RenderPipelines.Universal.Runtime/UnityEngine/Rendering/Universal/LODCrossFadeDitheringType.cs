namespace UnityEngine.Rendering.Universal
{
	public enum LODCrossFadeDitheringType
	{
		BayerMatrix = 0,
		BlueNoise = 1,
		[global::UnityEngine.InspectorName("2x2 Stencil")]
		[global::UnityEngine.Tooltip("2x2 pixel dithering pattern by stencil test with 2 stencil bits (4 and 8). This option decreases the number of the shader variants.")]
		Stencil = 2
	}
}
