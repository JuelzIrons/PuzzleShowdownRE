namespace UnityEngine.Rendering.RenderGraphModule
{
	[global::System.Diagnostics.DebuggerDisplay("Texture ({handle.index})")]
	[global::UnityEngine.Scripting.APIUpdating.MovedFrom(true, "UnityEngine.Experimental.Rendering.RenderGraphModule", "UnityEngine.Rendering.RenderGraphModule", null)]
	public readonly struct TextureHandle
	{
		private static global::UnityEngine.Rendering.RenderGraphModule.TextureHandle s_NullHandle;

		internal readonly global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle handle;

		private readonly bool builtin;

		public static global::UnityEngine.Rendering.RenderGraphModule.TextureHandle nullHandle => s_NullHandle;

		internal TextureHandle(in global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle h)
		{
			handle = h;
			builtin = false;
		}

		internal TextureHandle(int handle, bool shared = false, bool builtin = false)
		{
			this.handle = new global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle(handle, global::UnityEngine.Rendering.RenderGraphModule.RenderGraphResourceType.Texture, shared);
			this.builtin = builtin;
		}

		public static implicit operator global::UnityEngine.Rendering.RenderTargetIdentifier(global::UnityEngine.Rendering.RenderGraphModule.TextureHandle texture)
		{
			if (!texture.IsValid())
			{
				return default(global::UnityEngine.Rendering.RenderTargetIdentifier);
			}
			return global::UnityEngine.Rendering.RenderGraphModule.RenderGraphResourceRegistry.current.GetTexture(in texture);
		}

		public static implicit operator global::UnityEngine.Texture(global::UnityEngine.Rendering.RenderGraphModule.TextureHandle texture)
		{
			return texture.IsValid() ? global::UnityEngine.Rendering.RenderGraphModule.RenderGraphResourceRegistry.current.GetTexture(in texture) : null;
		}

		public static implicit operator global::UnityEngine.RenderTexture(global::UnityEngine.Rendering.RenderGraphModule.TextureHandle texture)
		{
			return texture.IsValid() ? global::UnityEngine.Rendering.RenderGraphModule.RenderGraphResourceRegistry.current.GetTexture(in texture) : null;
		}

		public static implicit operator global::UnityEngine.Rendering.RTHandle(global::UnityEngine.Rendering.RenderGraphModule.TextureHandle texture)
		{
			if (!texture.IsValid())
			{
				return null;
			}
			return global::UnityEngine.Rendering.RenderGraphModule.RenderGraphResourceRegistry.current.GetTexture(in texture);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public bool IsValid()
		{
			return handle.IsValid();
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		internal bool IsBuiltin()
		{
			return builtin;
		}

		public global::UnityEngine.Rendering.RenderGraphModule.TextureDesc GetDescriptor(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph)
		{
			return renderGraph.GetTextureDesc(in this);
		}
	}
}
