namespace UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler
{
	[global::System.Diagnostics.DebuggerDisplay("PassFragmentData: Res({resource.index}):{accessFlags}")]
	internal readonly struct PassFragmentData
	{
		public readonly global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle resource;

		public readonly global::UnityEngine.Rendering.RenderGraphModule.AccessFlags accessFlags;

		public readonly int mipLevel;

		public readonly int depthSlice;

		public PassFragmentData(in global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle handle, global::UnityEngine.Rendering.RenderGraphModule.AccessFlags flags, int mipLevel, int depthSlice)
		{
			resource = handle;
			accessFlags = flags;
			this.mipLevel = mipLevel;
			this.depthSlice = depthSlice;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return ((resource.GetHashCode() * 23 + accessFlags.GetHashCode()) * 23 + mipLevel.GetHashCode()) * 23 + depthSlice.GetHashCode();
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static bool SameSubResource(in global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassFragmentData x, in global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassFragmentData y)
		{
			if (x.resource.index == y.resource.index && x.mipLevel == y.mipLevel)
			{
				return x.depthSlice == y.depthSlice;
			}
			return false;
		}
	}
}
