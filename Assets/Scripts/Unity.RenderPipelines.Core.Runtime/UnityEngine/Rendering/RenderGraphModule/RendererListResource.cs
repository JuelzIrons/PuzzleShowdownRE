namespace UnityEngine.Rendering.RenderGraphModule
{
	internal struct RendererListResource
	{
		public global::UnityEngine.Rendering.RendererListParams desc;

		public global::UnityEngine.Rendering.RendererList rendererList;

		internal RendererListResource(in global::UnityEngine.Rendering.RendererListParams desc)
		{
			this.desc = desc;
			rendererList = default(global::UnityEngine.Rendering.RendererList);
		}
	}
}
