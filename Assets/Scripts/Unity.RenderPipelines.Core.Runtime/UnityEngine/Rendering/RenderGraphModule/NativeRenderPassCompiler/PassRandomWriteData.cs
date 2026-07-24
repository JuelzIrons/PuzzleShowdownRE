namespace UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler
{
	[global::System.Diagnostics.DebuggerDisplay("PassRandomWriteData: Res({resource.index}):{index}:{preserveCounterValue}")]
	internal readonly struct PassRandomWriteData
	{
		public readonly global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle resource;

		public readonly int index;

		public readonly bool preserveCounterValue;

		public PassRandomWriteData(in global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle resource, int index, bool preserveCounterValue)
		{
			this.resource = resource;
			this.index = index;
			this.preserveCounterValue = preserveCounterValue;
		}

		public override int GetHashCode()
		{
			return resource.GetHashCode() * 23 + index.GetHashCode();
		}
	}
}
