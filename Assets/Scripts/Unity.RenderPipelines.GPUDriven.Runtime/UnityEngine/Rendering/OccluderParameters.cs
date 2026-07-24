namespace UnityEngine.Rendering
{
	public struct OccluderParameters
	{
		public int viewInstanceID;

		public int subviewCount;

		public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle depthTexture;

		public global::UnityEngine.Vector2Int depthSize;

		public bool depthIsArray;

		public OccluderParameters(int viewInstanceID)
		{
			this.viewInstanceID = viewInstanceID;
			subviewCount = 1;
			depthTexture = global::UnityEngine.Rendering.RenderGraphModule.TextureHandle.nullHandle;
			depthSize = global::UnityEngine.Vector2Int.zero;
			depthIsArray = false;
		}
	}
}
