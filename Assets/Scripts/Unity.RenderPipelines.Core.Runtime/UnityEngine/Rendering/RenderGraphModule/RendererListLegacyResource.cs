namespace UnityEngine.Rendering.RenderGraphModule
{
	internal struct RendererListLegacyResource
	{
		public global::UnityEngine.Rendering.RendererList rendererList;

		public bool isActive;

		internal RendererListLegacyResource(in bool active = false)
		{
			rendererList = default(global::UnityEngine.Rendering.RendererList);
			isActive = active;
		}
	}
}
