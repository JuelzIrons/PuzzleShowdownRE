namespace UnityEngine.Rendering.RenderGraphModule
{
	[global::System.Diagnostics.DebuggerDisplay("Buffer ({handle.index})")]
	[global::UnityEngine.Scripting.APIUpdating.MovedFrom(true, "UnityEngine.Experimental.Rendering.RenderGraphModule", "UnityEngine.Rendering.RenderGraphModule", null)]
	public readonly struct BufferHandle
	{
		private static global::UnityEngine.Rendering.RenderGraphModule.BufferHandle s_NullHandle;

		internal readonly global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle handle;

		public static global::UnityEngine.Rendering.RenderGraphModule.BufferHandle nullHandle => s_NullHandle;

		internal BufferHandle(in global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle h)
		{
			handle = h;
		}

		internal BufferHandle(int handle, bool shared = false)
		{
			this.handle = new global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle(handle, global::UnityEngine.Rendering.RenderGraphModule.RenderGraphResourceType.Buffer, shared);
		}

		public static implicit operator global::UnityEngine.GraphicsBuffer(global::UnityEngine.Rendering.RenderGraphModule.BufferHandle buffer)
		{
			if (!buffer.IsValid())
			{
				return null;
			}
			return global::UnityEngine.Rendering.RenderGraphModule.RenderGraphResourceRegistry.current.GetBuffer(in buffer);
		}

		public bool IsValid()
		{
			return handle.IsValid();
		}
	}
}
