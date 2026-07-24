namespace UnityEngine.Rendering
{
	[global::UnityEngine.Rendering.GenerateHLSL(global::UnityEngine.Rendering.PackingRules.Exact, true, false, false, 1, false, false, false, -1, ".\\Library\\PackageCache\\com.unity.render-pipelines.core@04ab0eefa0c3\\Runtime\\Lighting\\ProbeVolume\\ShaderVariablesProbeVolumes.cs")]
	public enum APVLeakReductionMode
	{
		None = 0,
		Performance = 1,
		Quality = 2,
		[global::System.Obsolete("Performance #from(6000.0)")]
		ValidityBased = 1,
		[global::System.Obsolete("Quality #from(6000.0)")]
		ValidityAndNormalBased = 2
	}
}
