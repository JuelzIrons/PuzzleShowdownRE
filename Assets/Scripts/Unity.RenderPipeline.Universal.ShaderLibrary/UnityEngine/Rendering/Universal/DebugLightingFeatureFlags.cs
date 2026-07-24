namespace UnityEngine.Rendering.Universal
{
	[global::UnityEngine.Rendering.GenerateHLSL(global::UnityEngine.Rendering.PackingRules.Exact, true, false, false, 1, false, false, false, -1, ".\\Library\\PackageCache\\com.unity.render-pipelines.universal@37e0d4fc2503\\ShaderLibrary\\Debug\\DebugViewEnums.cs")]
	[global::System.Flags]
	public enum DebugLightingFeatureFlags
	{
		None = 0,
		GlobalIllumination = 1,
		MainLight = 2,
		AdditionalLights = 4,
		VertexLighting = 8,
		Emission = 0x10,
		AmbientOcclusion = 0x20
	}
}
