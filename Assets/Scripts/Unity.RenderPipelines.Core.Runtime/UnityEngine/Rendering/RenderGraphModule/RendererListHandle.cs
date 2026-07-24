namespace UnityEngine.Rendering.RenderGraphModule
{
	[global::System.Diagnostics.DebuggerDisplay("RendererList ({handle})")]
	[global::UnityEngine.Scripting.APIUpdating.MovedFrom(true, "UnityEngine.Experimental.Rendering.RenderGraphModule", "UnityEngine.Rendering.RenderGraphModule", null)]
	public struct RendererListHandle
	{
		internal global::UnityEngine.Rendering.RenderGraphModule.RendererListHandleType type;

		private bool m_IsValid;

		internal int handle { get; private set; }

		internal RendererListHandle(int handle, global::UnityEngine.Rendering.RenderGraphModule.RendererListHandleType type = global::UnityEngine.Rendering.RenderGraphModule.RendererListHandleType.Renderers)
		{
			this.handle = handle;
			m_IsValid = true;
			this.type = type;
		}

		public static implicit operator int(global::UnityEngine.Rendering.RenderGraphModule.RendererListHandle handle)
		{
			return handle.handle;
		}

		public static implicit operator global::UnityEngine.Rendering.RendererList(global::UnityEngine.Rendering.RenderGraphModule.RendererListHandle rendererList)
		{
			if (!rendererList.IsValid())
			{
				return global::UnityEngine.Rendering.RendererList.nullRendererList;
			}
			return global::UnityEngine.Rendering.RenderGraphModule.RenderGraphResourceRegistry.current.GetRendererList(in rendererList);
		}

		public bool IsValid()
		{
			return m_IsValid;
		}
	}
}
