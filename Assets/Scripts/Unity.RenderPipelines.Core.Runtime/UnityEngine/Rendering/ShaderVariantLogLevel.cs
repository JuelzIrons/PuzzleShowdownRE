namespace UnityEngine.Rendering
{
	public enum ShaderVariantLogLevel
	{
		[global::UnityEngine.Tooltip("No shader variants are logged")]
		Disabled = 0,
		[global::UnityEngine.Tooltip("Only shaders that are compatible with SRPs (e.g., URP, HDRP) are logged")]
		OnlySRPShaders = 1,
		[global::UnityEngine.Tooltip("All shader variants are logged")]
		AllShaders = 2
	}
}
