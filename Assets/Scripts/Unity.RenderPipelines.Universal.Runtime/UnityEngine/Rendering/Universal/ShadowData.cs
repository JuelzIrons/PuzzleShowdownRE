namespace UnityEngine.Rendering.Universal
{
	public struct ShadowData
	{
		private global::UnityEngine.Rendering.ContextContainer frameData;

		internal global::UnityEngine.Rendering.Universal.UniversalShadowData universalShadowData => frameData.Get<global::UnityEngine.Rendering.Universal.UniversalShadowData>();

		public ref bool supportsMainLightShadows => ref frameData.Get<global::UnityEngine.Rendering.Universal.UniversalShadowData>().supportsMainLightShadows;

		internal ref bool mainLightShadowsEnabled => ref frameData.Get<global::UnityEngine.Rendering.Universal.UniversalShadowData>().mainLightShadowsEnabled;

		public ref int mainLightShadowmapWidth => ref frameData.Get<global::UnityEngine.Rendering.Universal.UniversalShadowData>().mainLightShadowmapWidth;

		public ref int mainLightShadowmapHeight => ref frameData.Get<global::UnityEngine.Rendering.Universal.UniversalShadowData>().mainLightShadowmapHeight;

		public ref int mainLightShadowCascadesCount => ref frameData.Get<global::UnityEngine.Rendering.Universal.UniversalShadowData>().mainLightShadowCascadesCount;

		public ref global::UnityEngine.Vector3 mainLightShadowCascadesSplit => ref frameData.Get<global::UnityEngine.Rendering.Universal.UniversalShadowData>().mainLightShadowCascadesSplit;

		public ref float mainLightShadowCascadeBorder => ref frameData.Get<global::UnityEngine.Rendering.Universal.UniversalShadowData>().mainLightShadowCascadeBorder;

		public ref bool supportsAdditionalLightShadows => ref frameData.Get<global::UnityEngine.Rendering.Universal.UniversalShadowData>().supportsAdditionalLightShadows;

		internal ref bool additionalLightShadowsEnabled => ref frameData.Get<global::UnityEngine.Rendering.Universal.UniversalShadowData>().additionalLightShadowsEnabled;

		public ref int additionalLightsShadowmapWidth => ref frameData.Get<global::UnityEngine.Rendering.Universal.UniversalShadowData>().additionalLightsShadowmapWidth;

		public ref int additionalLightsShadowmapHeight => ref frameData.Get<global::UnityEngine.Rendering.Universal.UniversalShadowData>().additionalLightsShadowmapHeight;

		public ref bool supportsSoftShadows => ref frameData.Get<global::UnityEngine.Rendering.Universal.UniversalShadowData>().supportsSoftShadows;

		public ref int shadowmapDepthBufferBits => ref frameData.Get<global::UnityEngine.Rendering.Universal.UniversalShadowData>().shadowmapDepthBufferBits;

		public ref global::System.Collections.Generic.List<global::UnityEngine.Vector4> bias => ref frameData.Get<global::UnityEngine.Rendering.Universal.UniversalShadowData>().bias;

		public ref global::System.Collections.Generic.List<int> resolution => ref frameData.Get<global::UnityEngine.Rendering.Universal.UniversalShadowData>().resolution;

		internal ref bool isKeywordAdditionalLightShadowsEnabled => ref frameData.Get<global::UnityEngine.Rendering.Universal.UniversalShadowData>().isKeywordAdditionalLightShadowsEnabled;

		internal ref bool isKeywordSoftShadowsEnabled => ref frameData.Get<global::UnityEngine.Rendering.Universal.UniversalShadowData>().isKeywordSoftShadowsEnabled;

		internal ref int mainLightShadowResolution => ref frameData.Get<global::UnityEngine.Rendering.Universal.UniversalShadowData>().mainLightShadowResolution;

		internal ref int mainLightRenderTargetWidth => ref frameData.Get<global::UnityEngine.Rendering.Universal.UniversalShadowData>().mainLightRenderTargetWidth;

		internal ref int mainLightRenderTargetHeight => ref frameData.Get<global::UnityEngine.Rendering.Universal.UniversalShadowData>().mainLightRenderTargetHeight;

		internal ref global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.URPLightShadowCullingInfos> visibleLightsShadowCullingInfos => ref frameData.Get<global::UnityEngine.Rendering.Universal.UniversalShadowData>().visibleLightsShadowCullingInfos;

		internal ref global::UnityEngine.Rendering.Universal.AdditionalLightsShadowAtlasLayout shadowAtlasLayout => ref frameData.Get<global::UnityEngine.Rendering.Universal.UniversalShadowData>().shadowAtlasLayout;

		internal ShadowData(global::UnityEngine.Rendering.ContextContainer frameData)
		{
			this.frameData = frameData;
		}
	}
}
