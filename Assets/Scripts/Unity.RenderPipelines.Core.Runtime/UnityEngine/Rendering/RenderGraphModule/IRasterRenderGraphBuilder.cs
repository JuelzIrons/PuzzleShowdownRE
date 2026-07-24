namespace UnityEngine.Rendering.RenderGraphModule
{
	[global::UnityEngine.Scripting.APIUpdating.MovedFrom(true, "UnityEngine.Experimental.Rendering.RenderGraphModule", "UnityEngine.Rendering.RenderGraphModule", null)]
	public interface IRasterRenderGraphBuilder : global::UnityEngine.Rendering.RenderGraphModule.IRenderAttachmentRenderGraphBuilder, global::UnityEngine.Rendering.RenderGraphModule.IBaseRenderGraphBuilder, global::System.IDisposable
	{
		void SetInputAttachment(global::UnityEngine.Rendering.RenderGraphModule.TextureHandle tex, int index, global::UnityEngine.Rendering.RenderGraphModule.AccessFlags flags = global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.Read)
		{
			SetInputAttachment(tex, index, flags, 0, -1);
		}

		void SetInputAttachment(global::UnityEngine.Rendering.RenderGraphModule.TextureHandle tex, int index, global::UnityEngine.Rendering.RenderGraphModule.AccessFlags flags, int mipLevel, int depthSlice);

		void SetShadingRateImageAttachment(in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle tex);

		void SetShadingRateFragmentSize(global::UnityEngine.Rendering.ShadingRateFragmentSize shadingRateFragmentSize);

		void SetShadingRateCombiner(global::UnityEngine.Rendering.ShadingRateCombinerStage stage, global::UnityEngine.Rendering.ShadingRateCombiner combiner);

		void SetExtendedFeatureFlags(global::UnityEngine.Rendering.RenderGraphModule.ExtendedFeatureFlags extendedFeatureFlags);

		void SetRenderFunc<PassData>(global::UnityEngine.Rendering.RenderGraphModule.BaseRenderFunc<PassData, global::UnityEngine.Rendering.RenderGraphModule.RasterGraphContext> renderFunc) where PassData : class, new();
	}
}
