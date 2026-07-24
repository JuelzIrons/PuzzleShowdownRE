namespace UnityEngine.Rendering.Universal
{
	public class UniversalShadowData : global::UnityEngine.Rendering.ContextItem
	{
		public bool supportsMainLightShadows;

		internal bool mainLightShadowsEnabled;

		public int mainLightShadowmapWidth;

		public int mainLightShadowmapHeight;

		public int mainLightShadowCascadesCount;

		public global::UnityEngine.Vector3 mainLightShadowCascadesSplit;

		public float mainLightShadowCascadeBorder;

		public bool supportsAdditionalLightShadows;

		internal bool additionalLightShadowsEnabled;

		public int additionalLightsShadowmapWidth;

		public int additionalLightsShadowmapHeight;

		public bool supportsSoftShadows;

		public int shadowmapDepthBufferBits;

		public global::System.Collections.Generic.List<global::UnityEngine.Vector4> bias;

		public global::System.Collections.Generic.List<int> resolution;

		internal bool isKeywordAdditionalLightShadowsEnabled;

		internal bool isKeywordSoftShadowsEnabled;

		internal int mainLightShadowResolution;

		internal int mainLightRenderTargetWidth;

		internal int mainLightRenderTargetHeight;

		internal global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.URPLightShadowCullingInfos> visibleLightsShadowCullingInfos;

		internal global::UnityEngine.Rendering.Universal.AdditionalLightsShadowAtlasLayout shadowAtlasLayout;

		public override void Reset()
		{
			supportsMainLightShadows = false;
			mainLightShadowmapWidth = 0;
			mainLightShadowmapHeight = 0;
			mainLightShadowCascadesCount = 0;
			mainLightShadowCascadesSplit = global::UnityEngine.Vector3.zero;
			mainLightShadowCascadeBorder = 0f;
			supportsAdditionalLightShadows = false;
			additionalLightsShadowmapWidth = 0;
			additionalLightsShadowmapHeight = 0;
			supportsSoftShadows = false;
			shadowmapDepthBufferBits = 0;
			bias?.Clear();
			resolution?.Clear();
			isKeywordAdditionalLightShadowsEnabled = false;
			isKeywordSoftShadowsEnabled = false;
			mainLightShadowResolution = 0;
			mainLightRenderTargetWidth = 0;
			mainLightRenderTargetHeight = 0;
			visibleLightsShadowCullingInfos = default(global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.URPLightShadowCullingInfos>);
			shadowAtlasLayout = default(global::UnityEngine.Rendering.Universal.AdditionalLightsShadowAtlasLayout);
		}
	}
}
