namespace UnityEngine.Rendering.Universal
{
	public struct RenderingData
	{
		internal global::UnityEngine.Rendering.ContextContainer frameData;

		public global::UnityEngine.Rendering.Universal.CameraData cameraData;

		public global::UnityEngine.Rendering.Universal.LightData lightData;

		public global::UnityEngine.Rendering.Universal.ShadowData shadowData;

		public global::UnityEngine.Rendering.Universal.PostProcessingData postProcessingData;

		internal global::UnityEngine.Rendering.Universal.UniversalRenderingData universalRenderingData => frameData.Get<global::UnityEngine.Rendering.Universal.UniversalRenderingData>();

		public ref global::UnityEngine.Rendering.CullingResults cullResults => ref frameData.Get<global::UnityEngine.Rendering.Universal.UniversalRenderingData>().cullResults;

		public ref bool supportsDynamicBatching => ref frameData.Get<global::UnityEngine.Rendering.Universal.UniversalRenderingData>().supportsDynamicBatching;

		public ref global::UnityEngine.Rendering.PerObjectData perObjectData => ref frameData.Get<global::UnityEngine.Rendering.Universal.UniversalRenderingData>().perObjectData;

		public ref bool postProcessingEnabled => ref frameData.Get<global::UnityEngine.Rendering.Universal.UniversalPostProcessingData>().isEnabled;

		internal RenderingData(global::UnityEngine.Rendering.ContextContainer frameData)
		{
			this.frameData = frameData;
			cameraData = new global::UnityEngine.Rendering.Universal.CameraData(frameData);
			lightData = new global::UnityEngine.Rendering.Universal.LightData(frameData);
			shadowData = new global::UnityEngine.Rendering.Universal.ShadowData(frameData);
			postProcessingData = new global::UnityEngine.Rendering.Universal.PostProcessingData(frameData);
		}
	}
}
