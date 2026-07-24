namespace UnityEngine.Rendering.Universal
{
	public class UniversalRenderingData : global::UnityEngine.Rendering.ContextItem
	{
		public global::UnityEngine.Rendering.CullingResults cullResults;

		public bool supportsDynamicBatching;

		public global::UnityEngine.Rendering.PerObjectData perObjectData;

		public global::UnityEngine.Rendering.Universal.RenderingMode renderingMode { get; internal set; }

		public global::UnityEngine.LayerMask prepassLayerMask { get; internal set; }

		public global::UnityEngine.LayerMask opaqueLayerMask { get; internal set; }

		public global::UnityEngine.LayerMask transparentLayerMask { get; internal set; }

		public bool stencilLodCrossFadeEnabled { get; internal set; }

		public override void Reset()
		{
			cullResults = default(global::UnityEngine.Rendering.CullingResults);
			supportsDynamicBatching = false;
			perObjectData = global::UnityEngine.Rendering.PerObjectData.None;
			renderingMode = global::UnityEngine.Rendering.Universal.RenderingMode.Forward;
			stencilLodCrossFadeEnabled = false;
			prepassLayerMask = -1;
			opaqueLayerMask = -1;
			transparentLayerMask = -1;
		}
	}
}
