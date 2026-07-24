namespace UnityEngine.Rendering.RenderGraphModule
{
	[global::UnityEngine.Scripting.APIUpdating.MovedFrom(true, "UnityEngine.Experimental.Rendering.RenderGraphModule", "UnityEngine.Rendering.RenderGraphModule", null)]
	public struct RenderGraphParameters
	{
		[global::System.Obsolete("Not used anymore. The debugging tools use the name of the object identified by executionId. #from(6000.3)")]
		public string executionName;

		public global::UnityEngine.EntityId executionId;

		public bool generateDebugData;

		public int currentFrameIndex;

		public bool rendererListCulling;

		public global::UnityEngine.Rendering.ScriptableRenderContext scriptableRenderContext;

		public global::UnityEngine.Rendering.CommandBuffer commandBuffer;

		internal bool invalidContextForTesting;

		public global::UnityEngine.Rendering.RenderGraphModule.RenderTextureUVOriginStrategy renderTextureUVOriginStrategy;
	}
}
