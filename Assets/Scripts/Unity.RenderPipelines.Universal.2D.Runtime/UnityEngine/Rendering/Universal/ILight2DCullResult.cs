namespace UnityEngine.Rendering.Universal
{
	internal interface ILight2DCullResult
	{
		global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.Light2D> visibleLights { get; }

		global::System.Collections.Generic.HashSet<global::UnityEngine.Rendering.Universal.ShadowCasterGroup2D> visibleShadows { get; }

		global::UnityEngine.Rendering.Universal.LightStats GetLightStatsByLayer(int layerID, ref global::UnityEngine.Rendering.Universal.LayerBatch layer);

		bool IsSceneLit();
	}
}
