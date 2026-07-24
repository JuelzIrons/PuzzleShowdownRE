namespace UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler
{
	[global::System.Diagnostics.DebuggerDisplay("PassOutputData: Res({resource.index})")]
	internal readonly struct PassOutputData
	{
		public readonly global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle resource;

		public PassOutputData(in global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle resource)
		{
			this.resource = resource;
		}
	}
}
