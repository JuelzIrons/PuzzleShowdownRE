namespace UnityEngine.Rendering.Universal
{
	[global::UnityEngine.Rendering.GenerateHLSL(global::UnityEngine.Rendering.PackingRules.Exact, true, false, false, 1, false, false, false, -1, ".\\Library\\PackageCache\\com.unity.render-pipelines.universal@37e0d4fc2503\\ShaderLibrary\\Debug\\DebugViewEnums.cs")]
	public enum DebugValidationMode
	{
		None = 0,
		[global::UnityEngine.InspectorName("Highlight NaN, Inf and Negative Values")]
		HighlightNanInfNegative = 1,
		[global::UnityEngine.InspectorName("Highlight Values Outside Range")]
		HighlightOutsideOfRange = 2
	}
}
