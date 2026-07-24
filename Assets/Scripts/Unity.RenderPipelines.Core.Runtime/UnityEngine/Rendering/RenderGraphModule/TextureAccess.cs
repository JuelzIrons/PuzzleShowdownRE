namespace UnityEngine.Rendering.RenderGraphModule
{
	internal readonly struct TextureAccess
	{
		public readonly global::UnityEngine.Rendering.RenderGraphModule.TextureHandle textureHandle;

		public readonly int mipLevel;

		public readonly int depthSlice;

		public readonly global::UnityEngine.Rendering.RenderGraphModule.AccessFlags flags;

		public TextureAccess(in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle handle, global::UnityEngine.Rendering.RenderGraphModule.AccessFlags flags, int mipLevel, int depthSlice)
		{
			textureHandle = handle;
			this.flags = flags;
			this.mipLevel = mipLevel;
			this.depthSlice = depthSlice;
		}

		public TextureAccess(in global::UnityEngine.Rendering.RenderGraphModule.TextureAccess access, in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle handle)
		{
			textureHandle = handle;
			flags = access.flags;
			mipLevel = access.mipLevel;
			depthSlice = access.depthSlice;
		}
	}
}
