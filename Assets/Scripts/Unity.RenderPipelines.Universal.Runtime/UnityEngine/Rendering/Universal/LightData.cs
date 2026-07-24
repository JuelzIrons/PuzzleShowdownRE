namespace UnityEngine.Rendering.Universal
{
	public struct LightData
	{
		private global::UnityEngine.Rendering.ContextContainer frameData;

		internal global::UnityEngine.Rendering.Universal.UniversalLightData universalLightData => frameData.Get<global::UnityEngine.Rendering.Universal.UniversalLightData>();

		public ref int mainLightIndex => ref frameData.Get<global::UnityEngine.Rendering.Universal.UniversalLightData>().mainLightIndex;

		public ref int additionalLightsCount => ref frameData.Get<global::UnityEngine.Rendering.Universal.UniversalLightData>().additionalLightsCount;

		public ref int maxPerObjectAdditionalLightsCount => ref frameData.Get<global::UnityEngine.Rendering.Universal.UniversalLightData>().maxPerObjectAdditionalLightsCount;

		public ref global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.VisibleLight> visibleLights => ref frameData.Get<global::UnityEngine.Rendering.Universal.UniversalLightData>().visibleLights;

		public ref bool shadeAdditionalLightsPerVertex => ref frameData.Get<global::UnityEngine.Rendering.Universal.UniversalLightData>().shadeAdditionalLightsPerVertex;

		public ref bool supportsMixedLighting => ref frameData.Get<global::UnityEngine.Rendering.Universal.UniversalLightData>().supportsMixedLighting;

		public ref bool reflectionProbeBoxProjection => ref frameData.Get<global::UnityEngine.Rendering.Universal.UniversalLightData>().reflectionProbeBoxProjection;

		public ref bool reflectionProbeBlending => ref frameData.Get<global::UnityEngine.Rendering.Universal.UniversalLightData>().reflectionProbeBlending;

		public ref bool reflectionProbeAtlas => ref frameData.Get<global::UnityEngine.Rendering.Universal.UniversalLightData>().reflectionProbeAtlas;

		public ref bool supportsLightLayers => ref frameData.Get<global::UnityEngine.Rendering.Universal.UniversalLightData>().supportsLightLayers;

		public ref bool supportsAdditionalLights => ref frameData.Get<global::UnityEngine.Rendering.Universal.UniversalLightData>().supportsAdditionalLights;

		internal LightData(global::UnityEngine.Rendering.ContextContainer frameData)
		{
			this.frameData = frameData;
		}
	}
}
