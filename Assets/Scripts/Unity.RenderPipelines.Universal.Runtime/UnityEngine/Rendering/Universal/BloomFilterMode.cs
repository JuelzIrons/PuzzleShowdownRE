namespace UnityEngine.Rendering.Universal
{
	public enum BloomFilterMode
	{
		[global::UnityEngine.Tooltip("Best quality.")]
		Gaussian = 0,
		[global::UnityEngine.Tooltip("Balanced quality and speed.")]
		Dual = 1,
		[global::UnityEngine.Tooltip("Lowest quality. Fastest at low resolutions. Saves memory.")]
		Kawase = 2
	}
}
