namespace UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler
{
	[global::System.Diagnostics.DebuggerDisplay("Res({handle.index}) : {loadAction} : {storeAction} : {memoryless}")]
	internal readonly struct NativePassAttachment
	{
		public readonly global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle handle;

		public readonly global::UnityEngine.Rendering.RenderBufferLoadAction loadAction;

		public readonly global::UnityEngine.Rendering.RenderBufferStoreAction storeAction;

		public readonly bool memoryless;

		public readonly int mipLevel;

		public readonly int depthSlice;

		public NativePassAttachment(in global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle handle, global::UnityEngine.Rendering.RenderBufferLoadAction loadAction, global::UnityEngine.Rendering.RenderBufferStoreAction storeAction, bool memoryless, int mipLevel, int depthSlice)
		{
			this.handle = handle;
			this.loadAction = loadAction;
			this.storeAction = storeAction;
			this.memoryless = memoryless;
			this.mipLevel = mipLevel;
			this.depthSlice = depthSlice;
		}
	}
}
