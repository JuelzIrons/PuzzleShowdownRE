namespace UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler
{
	[global::System.Diagnostics.DebuggerDisplay("PassInputData: Res({resource.index})")]
	internal readonly struct PassInputData
	{
		public readonly global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle resource;

		public PassInputData(in global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle resource)
		{
			this.resource = resource;
		}
	}
}
