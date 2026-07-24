namespace UnityEngine.Rendering
{
	internal struct IndirectBufferContextHandles
	{
		public global::UnityEngine.Rendering.RenderGraphModule.BufferHandle instanceBuffer;

		public global::UnityEngine.Rendering.RenderGraphModule.BufferHandle instanceInfoBuffer;

		public global::UnityEngine.Rendering.RenderGraphModule.BufferHandle dispatchArgsBuffer;

		public global::UnityEngine.Rendering.RenderGraphModule.BufferHandle drawArgsBuffer;

		public global::UnityEngine.Rendering.RenderGraphModule.BufferHandle drawInfoBuffer;

		public void UseForOcclusionTest(global::UnityEngine.Rendering.RenderGraphModule.IBaseRenderGraphBuilder builder)
		{
			instanceBuffer = builder.UseBuffer(in instanceBuffer, global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.ReadWrite);
			instanceInfoBuffer = builder.UseBuffer(in instanceInfoBuffer);
			dispatchArgsBuffer = builder.UseBuffer(in dispatchArgsBuffer, global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.ReadWrite);
			drawArgsBuffer = builder.UseBuffer(in drawArgsBuffer, global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.ReadWrite);
			drawInfoBuffer = builder.UseBuffer(in drawInfoBuffer);
		}
	}
}
